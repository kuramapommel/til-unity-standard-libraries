using UniRx;

namespace Pommel.Reversi.Presentation.ViewModel.InGame
{
    public interface IPlayerViewModel
    {
        string Id { get; }

        IReadOnlyReactiveProperty<string> Name { get; }

        IReadOnlyReactiveProperty<bool> IsTurnPlayer { get; }

        void SetIsNextTurn(bool isNextTurn);
    }

    public interface IPlayerStateFactory
    {
        IPlayerViewModel Create(string id, string name, bool isTurnPlayer);
    }

    public sealed class PlayerViewModel : IPlayerViewModel
    {
        private readonly IReactiveProperty<string> m_name;

        private readonly IReactiveProperty<bool> m_isTurnPlayer;

        public string Id { get; }

        public IReadOnlyReactiveProperty<string> Name => m_name;

        public IReadOnlyReactiveProperty<bool> IsTurnPlayer => m_isTurnPlayer;

        public void SetIsNextTurn(bool isNextTurn) => m_isTurnPlayer.Value = isNextTurn;

        public PlayerViewModel(string id, string name, bool isTurnPlayer)
        {
            Id = id;
            m_name = new ReactiveProperty<string>(name);
            m_isTurnPlayer = new ReactiveProperty<bool>(isTurnPlayer);
        }
    }
}