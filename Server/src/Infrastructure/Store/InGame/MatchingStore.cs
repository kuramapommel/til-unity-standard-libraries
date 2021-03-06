using System;
using System.Collections.Generic;
using Pommel.Server.Domain.InGame;

namespace Pommel.Server.Infrastructure.Store.InGame
{
    public interface IMatchingStore : IDictionary<string, IMatching>
    {

    }

    public static class MatchingStore
    {
        private static readonly Lazy<IMatchingStore> m_instance = new Lazy<IMatchingStore>(() => new Impl());

        public static IMatchingStore Instance => m_instance.Value;

        private sealed class Impl : Dictionary<string, IMatching>, IMatchingStore
        {
        }
    }
}