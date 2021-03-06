using System;
using System.Collections.Generic;
using System.Linq;

namespace Pommel.Server.Domain.InGame
{
    // 試合集約
    public interface IGame
    {
        // todo 型作る
        string Id { get; }

        // todo 型作る
        string ResultId { get; }

        string MatchingId { get; }

        string NextTurnPlayerId { get; }

        IEnumerable<string> HistoryIds { get; }

        State State { get; }

        Turn Turn { get; }

        IEnumerable<Piece> Pieces { get; }

        IGame LayPiece(Point point, IMatching matching);

        IGame Start(string firstPlayerId);
    }

    public interface IGameFactory
    {
        IGame Create(string id, string resultId, string matchingId);
    }

    public sealed class Game : IGame
    {
        public string Id { get; }

        public string ResultId { get; }

        public string MatchingId { get; }

        public string NextTurnPlayerId { get; } = string.Empty;

        public IEnumerable<string> HistoryIds { get; } = Enumerable.Empty<string>();

        public State State { get; } = State.NotYet;

        public Turn Turn { get; } = Turn.First;

        public IEnumerable<Piece> Pieces { get; } = Enumerable.Range(0, 8)
            .SelectMany(x => Enumerable.Range(0, 8)
                .Select(y => new Piece(new Point(x, y))));

        public Game(string id, string resultId, string matchingId)
        {
            Id = id;
            ResultId = resultId;
            MatchingId = matchingId;
        }

        public IGame LayPiece(Point point, IMatching matching)
        {
            // todo 妥当な例外に置き換える
            if (State != State.Playing) throw new ArgumentException("このゲームはプレイ中ではありません");

            if (MatchingId != matching.Id) throw new ArgumentException("このゲームに紐付かないマッチングが指定されました");

            // 配置済みの箇所を指定された場合は例外
            if (Pieces.First(piece => piece.Point.X == point.X && piece.Point.Y == point.Y).Color != Color.None) throw new ArgumentException();

            var (playerColor, opponentColor, nextTurn, playerId, opponentId) = Turn == Turn.First
                ? (Color.Dark, Color.Light, Turn.Second, matching.FirstPlayer.Id, matching.SecondPlayer.Id)
                : (Color.Light, Color.Dark, Turn.First, matching.SecondPlayer.Id, matching.FirstPlayer.Id);

            var flipTargets = this.CreateFlipTargets(Turn, point, Pieces);

            // 反転対象の駒が存在しない場合は例外
            if (!flipTargets.Any()) throw new ArgumentException();

            // 判定処理
            var flippedPieces = Pieces
                .GroupJoin(
                    flipTargets,
                    piece => (piece.Point.X, piece.Point.Y),
                    target => (target.Point.X, target.Point.Y),
                    (piece, targets) => (piece, targets)
                )
                .SelectMany(
                    aggregate => aggregate.targets.DefaultIfEmpty(),
                    (aggregate, target) => target?.SetColor(playerColor)
                        ?? (aggregate.piece.Point.X == point.X && aggregate.piece.Point.Y == point.Y
                            ? aggregate.piece.SetColor(playerColor)
                            : aggregate.piece)
                )
                .ToArray();

            // todo ID Generator 的なものをかませる
            var laidLogId = Guid.NewGuid().ToString();
            var nonePoints = flippedPieces.Where(flippedPiece => flippedPiece.Color == Color.None).ToArray();

            // 相手側が駒を置くことができるかチェック、できる場合はターンきりかえて return
            if (nonePoints.Any(nonePoint => this.IsValid(nextTurn, nonePoint.Point, flippedPieces)))
                return new Game(Id, ResultId, MatchingId, opponentId, HistoryIds.Append(laidLogId), State, nextTurn, flippedPieces);

            // 相手側が駒を置けない場合はプレイヤー側が続けて駒を置くことができるかチェック、できる場合はターンを保持したまま return
            if (nonePoints.Any(nonePoint => this.IsValid(Turn, nonePoint.Point, flippedPieces)))
                return new Game(Id, ResultId, MatchingId, playerId, HistoryIds.Append(laidLogId), State, Turn, flippedPieces);

            // 両者置けない場合はゲームセットとして return
            return new Game(Id, ResultId, MatchingId, opponentId, HistoryIds.Append(laidLogId), State.GameSet, nextTurn, flippedPieces);
        }

        public IGame Start(string firstPlayerId) => new Game(Id, ResultId, MatchingId, firstPlayerId, HistoryIds, State.Playing, Turn,
            Pieces.Select(piece =>
            {
                if (Point.InitialDarkPoints.Contains(piece.Point)) return piece.SetColor(Color.Dark);
                if (Point.InitialLightPoints.Contains(piece.Point)) return piece.SetColor(Color.Light);
                return piece;
            })
            .ToArray());

        private Game(string id, string resultId, string matchingId, string nextTurnPlayerId, IEnumerable<string> historyIds, State state, Turn turn, IEnumerable<Piece> pieces)
        {
            Id = id;
            ResultId = resultId;
            MatchingId = matchingId;
            NextTurnPlayerId = nextTurnPlayerId;
            State = state;
            Turn = turn;
            Pieces = pieces;
            HistoryIds = historyIds;
        }
    }

    public static class GameExtension
    {
        public static bool IsValid(this IGame source, Turn turn, Point point, IEnumerable<Piece> pieces)
        {
            if (pieces.First(piece => piece.Point.X == point.X && piece.Point.Y == point.Y).Color != Color.None) return false;

            return source.CreateFlipTargets(turn, point, pieces).Any();
        }

        public static IEnumerable<Piece> CreateFlipTargets(this IGame source, Turn turn, Point layPoint, IEnumerable<Piece> basePieces)
        {
            var (playerColor, opponentColor) = turn == Turn.First
                ? (Color.Dark, Color.Light)
                : (Color.Light, Color.Dark);

            return layPoint.AdjacentPoints
                    .Join(
                        basePieces,
                        aroundPoint => (aroundPoint, opponentColor),
                        piece => (piece.Point, piece.Color),
                        (aroundPoint, piece) => piece
                    )
                    .SelectMany(opponentPiece =>
                    {
                        var vectorPieces = layPoint.CreateSpecifiedVectorPoints(opponentPiece.Point)
                            .Join(
                                basePieces,
                                vectorPiece => (vectorPiece.X, vectorPiece.Y),
                                piece => (piece.Point.X, piece.Point.Y),
                                (vectorPiece, piece) => piece)
                            .ToArray();
                        var targets = vectorPieces.TakeWhile(piece => piece.Color != playerColor);
                        var betweenOther = vectorPieces.ElementAtOrDefault(targets.Count());

                        if (targets.Any(piece => piece.Color == Color.None)
                            || betweenOther == null
                            || betweenOther.Color != playerColor
                        ) return Enumerable.Empty<Piece>();

                        return targets;
                    })
                    .ToArray();
        }
    }

    public enum State
    {
        NotYet,
        Playing,
        GameSet
    }

    public enum Turn
    {
        First,
        Second
    }
}