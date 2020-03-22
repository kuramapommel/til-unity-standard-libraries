﻿using Unidux;
using UniRx;
using UnityEngine;

namespace Pommel.Reversi.Presentation.Scene.InGame
{
    public sealed class Unidux : SingletonMonoBehaviour<Unidux>, IStoreAccessor
    {
        public TextAsset InitialStateJson;

        private Store<State> _store;

        public IStoreObject StoreObject
        {
            get { return Store; }
        }

        public static State State
        {
            get { return Store.State; }
        }

        public static Subject<State> Subject
        {
            get { return Store.Subject; }
        }

        private static IReducer[] Reducers => new IReducer[]
        {
            new StoneAction.Reducer(),
        };

        private static IMiddlewares Middlewares => new Middlewares(State);

        private static State InitialState
        {
            get
            {
                return Instance.InitialStateJson != null
                    ? UniduxSetting.Serializer.Deserialize(
                        System.Text.Encoding.UTF8.GetBytes(Instance.InitialStateJson.text),
                        typeof(State)
                    ) as State
                    : new State();
            }
        }

        public static Store<State> Store
        {
            get { return Instance._store = Instance._store ?? new Store<State>(InitialState, Reducers); }
        }

        public static object Dispatch<TAction>(TAction action)
        {
            return Store.Dispatch(action);
        }
        
        void Start()
        {
            Store.ApplyMiddlewares(Middlewares.Collection);
        }

        void Update()
        {
            Store.Update();
        }
    }
}
