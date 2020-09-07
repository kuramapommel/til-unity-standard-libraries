using Pommel.Reversi.Components.InGame;
using UnityEngine;
using Zenject;

namespace Pommel.Reversi.Installer.Scene.Ingame
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Piece m_piecePrefab = default;

        //[SerializeField]
        //private GameBoard m_gameBoard = default;

        [SerializeField]
        private ResultMessage m_resultMessage = default;

        [SerializeField]
        private PlayerInfo m_firstPlayerInfo = default;

        [SerializeField]
        private PlayerInfo m_secondPlayerInfo = default;

        public override void InstallBindings()
        {
            // factories
            //Container.BindIFactory<IPieceViewModel, IPiece>().To<_Piece>()
            //    .FromComponentInNewPrefab(m_piecePrefab)
            //    .UnderTransform(m_gameBoard.GetComponent<RectTransform>())
            //    .AsCached();

            //// views
            //Container.BindInterfacesTo<GameBoard>().FromInstance(m_gameBoard).AsCached();
            Container.BindInterfacesTo<ResultMessage>().FromInstance(m_resultMessage).AsCached();
            Container.Bind<IFirstPlayerInfo>().FromInstance(m_firstPlayerInfo).AsCached();
            Container.Bind<ISecondPlayerInfo>().FromInstance(m_secondPlayerInfo).AsCached();
        }
    }
}