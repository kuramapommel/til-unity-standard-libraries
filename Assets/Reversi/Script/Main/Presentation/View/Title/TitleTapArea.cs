using Cysharp.Threading.Tasks;
using Pommel.Reversi.Presentation.State.InGame;
using Pommel.Reversi.Presentation.State.System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using _Scene = Pommel.Reversi.Domain.Transition.Scene;

namespace Pommel.Reversi.Presentation.View.Title
{
    public interface ITitleTapArea
    {
    }

    [RequireComponent(typeof(Button))]
    public sealed class TitleTapArea : MonoBehaviour, ITitleTapArea
    {
        private Button m_tapArea;

        [Inject]
        public void Construct(IGameState state, ITransitionState transitionState)
        {
            m_tapArea = GetComponent<Button>();
            m_tapArea
                .OnClickAsObservable()
                .TakeUntilDestroy(this)
                .SelectMany(_ => state.CreateGameAsync().AsUniTask().ToObservable())
                .SelectMany(_ => transitionState.AddAsync(_Scene.InGame, container => container.Bind<IGameState>().FromInstance(state).AsCached()).AsUniTask().ToObservable())
                .Subscribe(_ => transitionState.RemoveAsync(_Scene.Title).AsUniTask().Forget());
        }
    }
}
