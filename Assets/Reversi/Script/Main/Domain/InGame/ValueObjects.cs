using System;

namespace Pommel.Reversi.Domain.InGame
{
    public static class ValueObjects
    {
        [Serializable]
        public readonly struct Game
        {
            public string Id { get; }

            public Room Room { get; }

            public Stone[] Stones { get; }

            public State State { get; }

            public Game(
                string id,
                Room room,
                Stone[] stones,
                State state
                )
            {
                Id = id;
                Room = room;
                Stones = stones;
                State = state;
            }
        }

        [Serializable]
        public readonly struct Room
        {
            public Player FirstPlayer { get; }

            public Player SecondPlayer { get; }

            public Room(
                Player firstPlayer,
                Player secondPlayer
                )
            {
                FirstPlayer = firstPlayer;
                SecondPlayer = secondPlayer;
            }

            [Serializable]
            public readonly struct Player
            {
                public string Id { get; }

                public string Name { get; }

                public Stone.Color StoneColor { get; }

                public bool IsTurnPlayer { get; }

                public Player(string id, string name, Stone.Color stoneColor, bool isTurnPlayer)
                {
                    Id = id;
                    Name = name;
                    StoneColor = stoneColor;
                    IsTurnPlayer = isTurnPlayer;
                }
            }
        }

        [Serializable]
        public readonly struct Stone
        {
            public Point Point { get; }

            public Color StoneColor { get; }

            public Stone(Point point, Color color = Color.None)
            {
                Point = point;
                StoneColor = color;
            }

            public enum Color
            {
                Light,
                Dark,
                None
            }
        }

        [Serializable]
        public readonly struct Point
        {
            public int X { get; }

            public int Y { get; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public enum State
        {
            NotYet,
            Playing,
            Finished
        }
    }
}