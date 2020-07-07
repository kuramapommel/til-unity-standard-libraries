// <auto-generated />
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace Pommel.Generated
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::MagicOnion;
    using global::MagicOnion.Client;

    public static partial class MagicOnionInitializer
    {
        static bool isRegistered = false;

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            if(isRegistered) return;
            isRegistered = true;

            MagicOnionClientRegistry<Pommel.Api.Services.IInGameService>.Register((x, y, z) => new Pommel.Api.Services.InGameServiceClient(x, y, z));

            StreamingHubClientRegistry<Pommel.Api.Hubs.IInGameHub, Pommel.Api.Hubs.IInGameReceiver>.Register((a, _, b, c, d, e) => new Pommel.Api.Hubs.InGameHubClient(a, b, c, d, e));
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace Pommel.Generated.Resolvers
{
    using System;
    using MessagePack;

    public class MagicOnionResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new MagicOnionResolver();

        MagicOnionResolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = MagicOnionResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class MagicOnionResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static MagicOnionResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(4)
            {
                {typeof(global::MagicOnion.DynamicArgumentTuple<int, int, int>), 0 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<string, int, int>), 1 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<string, string, string>), 2 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<string, string>), 3 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new global::MagicOnion.DynamicArgumentTupleFormatter<int, int, int>(default(int), default(int), default(int));
                case 1: return new global::MagicOnion.DynamicArgumentTupleFormatter<string, int, int>(default(string), default(int), default(int));
                case 2: return new global::MagicOnion.DynamicArgumentTupleFormatter<string, string, string>(default(string), default(string), default(string));
                case 3: return new global::MagicOnion.DynamicArgumentTupleFormatter<string, string>(default(string), default(string));
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace Pommel.Api.Services {
    using System;
    using MagicOnion;
    using MagicOnion.Client;
    using Grpc.Core;
    using MessagePack;

    [Ignore]
    public class InGameServiceClient : MagicOnionClientBase<global::Pommel.Api.Services.IInGameService>, global::Pommel.Api.Services.IInGameService
    {
        static readonly Method<byte[], byte[]> CreateMatchingAsyncMethod;
        static readonly Func<RequestContext, ResponseContext> CreateMatchingAsyncDelegate;
        static readonly Method<byte[], byte[]> CreateGameAsyncMethod;
        static readonly Func<RequestContext, ResponseContext> CreateGameAsyncDelegate;
        static readonly Method<byte[], byte[]> SaveGameAsyncMethod;
        static readonly Func<RequestContext, ResponseContext> SaveGameAsyncDelegate;

        static InGameServiceClient()
        {
            CreateMatchingAsyncMethod = new Method<byte[], byte[]>(MethodType.Unary, "IInGameService", "CreateMatchingAsync", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);
            CreateMatchingAsyncDelegate = _CreateMatchingAsync;
            CreateGameAsyncMethod = new Method<byte[], byte[]>(MethodType.Unary, "IInGameService", "CreateGameAsync", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);
            CreateGameAsyncDelegate = _CreateGameAsync;
            SaveGameAsyncMethod = new Method<byte[], byte[]>(MethodType.Unary, "IInGameService", "SaveGameAsync", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);
            SaveGameAsyncDelegate = _SaveGameAsync;
        }

        InGameServiceClient()
        {
        }

        public InGameServiceClient(CallInvoker callInvoker, MessagePackSerializerOptions serializerOptions, IClientFilter[] filters)
            : base(callInvoker, serializerOptions, filters)
        {
        }

        protected override MagicOnionClientBase<IInGameService> Clone()
        {
            var clone = new InGameServiceClient();
            clone.host = this.host;
            clone.option = this.option;
            clone.callInvoker = this.callInvoker;
            clone.serializerOptions = this.serializerOptions;
            clone.filters = filters;
            return clone;
        }

        public new IInGameService WithHeaders(Metadata headers)
        {
            return base.WithHeaders(headers);
        }

        public new IInGameService WithCancellationToken(System.Threading.CancellationToken cancellationToken)
        {
            return base.WithCancellationToken(cancellationToken);
        }

        public new IInGameService WithDeadline(System.DateTime deadline)
        {
            return base.WithDeadline(deadline);
        }

        public new IInGameService WithHost(string host)
        {
            return base.WithHost(host);
        }

        public new IInGameService WithOptions(CallOptions option)
        {
            return base.WithOptions(option);
        }
   
        static ResponseContext _CreateMatchingAsync(RequestContext __context)
        {
            return CreateResponseContext<DynamicArgumentTuple<string, string>, string>(__context, CreateMatchingAsyncMethod);
        }

        public global::MagicOnion.UnaryResult<string> CreateMatchingAsync(string playerId, string playerName)
        {
            return InvokeAsync<DynamicArgumentTuple<string, string>, string>("IInGameService/CreateMatchingAsync", new DynamicArgumentTuple<string, string>(playerId, playerName), CreateMatchingAsyncDelegate);
        }
        static ResponseContext _CreateGameAsync(RequestContext __context)
        {
            return CreateResponseContext<string, string>(__context, CreateGameAsyncMethod);
        }

        public global::MagicOnion.UnaryResult<string> CreateGameAsync(string matchingId)
        {
            return InvokeAsync<string, string>("IInGameService/CreateGameAsync", matchingId, CreateGameAsyncDelegate);
        }
        static ResponseContext _SaveGameAsync(RequestContext __context)
        {
            return CreateResponseContext<global::Pommel.Api.Protocol.InGame.Game, global::Pommel.Api.Protocol.InGame.Game>(__context, SaveGameAsyncMethod);
        }

        public global::MagicOnion.UnaryResult<global::Pommel.Api.Protocol.InGame.Game> SaveGameAsync(global::Pommel.Api.Protocol.InGame.Game game)
        {
            return InvokeAsync<global::Pommel.Api.Protocol.InGame.Game, global::Pommel.Api.Protocol.InGame.Game>("IInGameService/SaveGameAsync", game, SaveGameAsyncDelegate);
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace Pommel.Api.Hubs {
    using Grpc.Core;
    using Grpc.Core.Logging;
    using MagicOnion;
    using MagicOnion.Client;
    using MessagePack;
    using System;
    using System.Threading.Tasks;

    [Ignore]
    public class InGameHubClient : StreamingHubClientBase<global::Pommel.Api.Hubs.IInGameHub, global::Pommel.Api.Hubs.IInGameReceiver>, global::Pommel.Api.Hubs.IInGameHub
    {
        static readonly Method<byte[], byte[]> method = new Method<byte[], byte[]>(MethodType.DuplexStreaming, "IInGameHub", "Connect", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);

        protected override Method<byte[], byte[]> DuplexStreamingAsyncMethod { get { return method; } }

        readonly global::Pommel.Api.Hubs.IInGameHub __fireAndForgetClient;

        public InGameHubClient(CallInvoker callInvoker, string host, CallOptions option, MessagePackSerializerOptions serializerOptions, ILogger logger)
            : base(callInvoker, host, option, serializerOptions, logger)
        {
            this.__fireAndForgetClient = new FireAndForgetClient(this);
        }
        
        public global::Pommel.Api.Hubs.IInGameHub FireAndForget()
        {
            return __fireAndForgetClient;
        }

        protected override void OnBroadcastEvent(int methodId, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -180705133: // OnCreateMatching
                {
                    var result = MessagePackSerializer.Deserialize<string>(data, serializerOptions);
                    receiver.OnCreateMatching(result); break;
                }
                case -1297457280: // OnJoin
                {
                    var result = MessagePackSerializer.Deserialize<DynamicArgumentTuple<string, string, string>>(data, serializerOptions);
                    receiver.OnJoin(result.Item1, result.Item2, result.Item3); break;
                }
                case 2034815902: // OnCreateGame
                {
                    var result = MessagePackSerializer.Deserialize<DynamicArgumentTuple<string, string>>(data, serializerOptions);
                    receiver.OnCreateGame(result.Item1, result.Item2); break;
                }
                case 995394406: // OnStartGame
                {
                    var result = MessagePackSerializer.Deserialize<global::Pommel.Api.Protocol.InGame.Game>(data, serializerOptions);
                    receiver.OnStartGame(result); break;
                }
                case -237469886: // OnLay
                {
                    var result = MessagePackSerializer.Deserialize<global::Pommel.Api.Protocol.InGame.Game>(data, serializerOptions);
                    receiver.OnLay(result); break;
                }
                case 1937311401: // OnResult
                {
                    var result = MessagePackSerializer.Deserialize<DynamicArgumentTuple<int, int, int>>(data, serializerOptions);
                    receiver.OnResult(result.Item1, result.Item2, result.Item3); break;
                }
                default:
                    break;
            }
        }

        protected override void OnResponseEvent(int methodId, object taskCompletionSource, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -733403293: // JoinAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -1612571457: // StartGameAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 1849616495: // LayAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                default:
                    break;
            }
        }
   
        public global::System.Threading.Tasks.Task JoinAsync(string matchingId, string playerId, string playerName)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<string, string, string>, Nil>(-733403293, new DynamicArgumentTuple<string, string, string>(matchingId, playerId, playerName));
        }

        public global::System.Threading.Tasks.Task StartGameAsync(string gameId)
        {
            return WriteMessageWithResponseAsync<string, Nil>(-1612571457, gameId);
        }

        public global::System.Threading.Tasks.Task LayAsync(string gameId, int x, int y)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<string, int, int>, Nil>(1849616495, new DynamicArgumentTuple<string, int, int>(gameId, x, y));
        }


        class FireAndForgetClient : global::Pommel.Api.Hubs.IInGameHub
        {
            readonly InGameHubClient __parent;

            public FireAndForgetClient(InGameHubClient parentClient)
            {
                this.__parent = parentClient;
            }

            public global::Pommel.Api.Hubs.IInGameHub FireAndForget()
            {
                throw new NotSupportedException();
            }

            public Task DisposeAsync()
            {
                throw new NotSupportedException();
            }

            public Task WaitForDisconnect()
            {
                throw new NotSupportedException();
            }

            public global::System.Threading.Tasks.Task JoinAsync(string matchingId, string playerId, string playerName)
            {
                return __parent.WriteMessageAsync<DynamicArgumentTuple<string, string, string>>(-733403293, new DynamicArgumentTuple<string, string, string>(matchingId, playerId, playerName));
            }

            public global::System.Threading.Tasks.Task StartGameAsync(string gameId)
            {
                return __parent.WriteMessageAsync<string>(-1612571457, gameId);
            }

            public global::System.Threading.Tasks.Task LayAsync(string gameId, int x, int y)
            {
                return __parent.WriteMessageAsync<DynamicArgumentTuple<string, int, int>>(1849616495, new DynamicArgumentTuple<string, int, int>(gameId, x, y));
            }

        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
