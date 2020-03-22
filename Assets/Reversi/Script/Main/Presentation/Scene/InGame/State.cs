﻿using System;
using System.Collections.Generic;
using System.Linq;
using Unidux;

namespace Pommel.Reversi.Presentation.Scene.InGame
{
    public interface IState
    {
        StoneStateElement[][] Stones { get; }

        TurnStateElement Turn { get; }

        WinnerStateElement Result { get; }
    }

    [Serializable]
    public sealed class State : StateBase, IState
    {
        public StoneStateElement[][] Stones { get; } = Enumerable.Range(0, 8)
            .Select(row => Enumerable.Range(0, 8)
                .Select(column =>
                {
                    switch ((row, column))
                    {
                        case var position when !(position.row == 3 || position.row == 4) || !(position.column == 3 || position.column == 4):
                            return new StoneStateElement();
                        case var position when position.row == 3 && position.column == 3:
                            return new StoneStateElement(StoneStateElement.State.White);
                        case var position when position.row == 3 && position.column == 4:
                            return new StoneStateElement(StoneStateElement.State.Black);
                        case var position when position.row == 4 && position.column == 3:
                            return new StoneStateElement(StoneStateElement.State.Black);
                        case var position when position.row == 4 && position.column == 4:
                            return new StoneStateElement(StoneStateElement.State.White);
                    }

                    return new StoneStateElement();
                })
            .ToArray())
        .ToArray();

        public TurnStateElement Turn { get; } = new TurnStateElement();

        public WinnerStateElement Result { get; } = new WinnerStateElement();
    }

    public static class StoneStateElementExtension
    {
        public static bool CanPut(this StoneStateElement[][] source, int x, int y, bool isBlackTurn) => SearchTargets
            .Any(target => target.CanPut(source, x, y, isBlackTurn));

        public static bool CanPutBalck(this StoneStateElement[][] source)
        {
            var targets = SearchTargets;
            return source
                .SelectMany((elements, x) => elements
                    .Where(element => element.Color == StoneStateElement.State.None)
                    .Select((element, y) => (element, x, y)))
                .Any(aggregate => targets.Any(target => target.CanPut(source, aggregate.x, aggregate.y, true)));
        }

        public static bool CanPutWhite(this StoneStateElement[][] source)
        {
            var targets = SearchTargets;
            return source
                .SelectMany((elements, x) => elements
                    .Where(element => element.Color == StoneStateElement.State.None)
                    .Select((element, y) => (element, x, y)))
                .Any(aggregate => targets.Any(target => target.CanPut(source, aggregate.x, aggregate.y, false)));
        }

        public static void Flip(this StoneStateElement[][] source, int x, int y, bool isBlackTurn)
        {
            var list = new List<(int x, int y)>();
            foreach (var target in SearchTargets
                .Where(target => target.CanPut(source, x, y, isBlackTurn)))
            {
                const int min = 0;
                const int max = 8;

                for (var index = (x, y); index.x >= min && index.y >= min && index.x < max && index.y < max; index = index.Next(target))
                {
                    if (index.x == x && index.y == y)
                    {
                        list.Add((index.x, index.y));
                        continue;
                    }

                    var color = source.ElementAtOrDefault(index.x)?
                        .ElementAtOrDefault(index.y)?
                        .Color ?? StoneStateElement.State.None;

                    switch (color)
                    {
                        case StoneStateElement.State.None: break;
                        case StoneStateElement.State.White when isBlackTurn:
                            list.Add((index.x, index.y));
                            continue;
                        case StoneStateElement.State.Black when !isBlackTurn:
                            list.Add((index.x, index.y));
                            continue;
                        case StoneStateElement.State.White: break;
                        case StoneStateElement.State.Black: break;
                    }

                    break;
                }
            }

            foreach (var index in list)
            {
                source[index.x][index.y].Color = isBlackTurn
                    ? StoneStateElement.State.Black
                    : StoneStateElement.State.White;
            }
        }

        private static (int x, int y) Next(this (int x, int y) source, SearchTarget searchTarget)
        {
            switch (searchTarget)
            {
                case SearchTarget.UpperLeft: return (source.x - 1, source.y - 1);
                case SearchTarget.Upper: return (source.x, source.y - 1);
                case SearchTarget.UpperRight: return (source.x + 1, source.y - 1);
                case SearchTarget.Left: return (source.x - 1, source.y);
                case SearchTarget.Right: return (source.x + 1, source.y);
                case SearchTarget.LowerLeft: return (source.x - 1, source.y + 1);
                case SearchTarget.Lower: return (source.x, source.y + 1);
                case SearchTarget.LowerRight: return (source.x + 1, source.y + 1);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private enum SearchTarget
        {
            UpperLeft,
            Upper,
            UpperRight,
            Left,
            Right,
            LowerLeft,
            Lower,
            LowerRight
        }

        private static IEnumerable<SearchTarget> SearchTargets => searchTargets.Value;

        private static readonly Lazy<IEnumerable<SearchTarget>> searchTargets = new Lazy<IEnumerable<SearchTarget>>(() => new[]
        {
            SearchTarget.UpperLeft,
            SearchTarget.Upper,
            SearchTarget.UpperRight,
            SearchTarget.Left,
            SearchTarget.Right,
            SearchTarget.LowerLeft,
            SearchTarget.Lower,
            SearchTarget.LowerRight
        });

        private static bool CanPut(this SearchTarget target, StoneStateElement[][] source, int x, int y, bool isBlackTurn)
        {
            var stone = source[x][y];
            if ((isBlackTurn && stone.Color == StoneStateElement.State.Black)
                || (!isBlackTurn && stone.Color == StoneStateElement.State.White)) return false;

            const int min = 0;
            const int max = 8;

            var oppentColorCount = 0;

            for (var index = (x, y); index.x >= min && index.y >= min && index.x < max && index.y < max; index = index.Next(target))
            {
                if (index.x == x && index.y == y) continue;

                var color = source.ElementAtOrDefault(index.x)?.ElementAtOrDefault(index.y)?.Color ?? StoneStateElement.State.None;

                switch (color)
                {
                    case StoneStateElement.State.None: return false;
                    case StoneStateElement.State.White when isBlackTurn:
                        oppentColorCount++;
                        continue;
                    case StoneStateElement.State.Black when !isBlackTurn:
                        oppentColorCount++;
                        continue;
                    case StoneStateElement.State.White: return oppentColorCount > 0;
                    case StoneStateElement.State.Black: return oppentColorCount > 0;
                }
            }

            return false;
        }
    }
}
