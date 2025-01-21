using PlayerContent;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Movement>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerLook>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
        }
    }
}
