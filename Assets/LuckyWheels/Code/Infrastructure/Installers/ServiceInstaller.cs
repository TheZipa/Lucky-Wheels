using LuckyWheels.Code.Services.CoroutineRunner;
using LuckyWheels.Code.Services.EntityContainer;
using LuckyWheels.Code.Services.Factories.PersistentEntityFactory;
using LuckyWheels.Code.Services.Factories.UIFactory;
using LuckyWheels.Code.Services.PersistentProgress;
using LuckyWheels.Code.Services.SaveLoad;
using LuckyWheels.Code.Services.SceneLoader;
using LuckyWheels.Code.Services.Sound;
using LuckyWheels.Code.Services.StaticData;
using LuckyWheels.Code.Services.StaticData.StaticDataProvider;
using UnityEngine;
using Zenject;

namespace LuckyWheels.Code.Infrastructure.Installers
{
    public class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private SoundService _soundService;
        
        public override void InstallBindings()
        {
            RegisterSceneLoader();
            RegisterStaticDataProvider();
            RegisterCoroutineRunner();
            RegisterEntityContainer();
            RegisterSoundService();
            RegisterSaveLoad();
            RegisterPersistentProgress();
            RegisterStaticData();
            RegisterUIFactory();
            RegisterPersistentEntityFactory();
        }

        private void RegisterSceneLoader() =>
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

        private void RegisterStaticDataProvider() =>
            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle();

        private void RegisterCoroutineRunner() =>
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

        private void RegisterEntityContainer() =>
            Container.BindInterfacesTo<EntityContainer>().AsSingle();
        
        private void RegisterSoundService() =>
            Container.Bind<ISoundService>().FromInstance(_soundService).AsSingle();
        
        private void RegisterSaveLoad() =>
            Container.Bind<ISaveLoad>().To<PrefsSaveLoad>().AsSingle();

        private void RegisterPersistentProgress() =>
            Container.Bind<IPersistentProgress>().To<PersistentPlayerProgress>().AsSingle();

        private void RegisterStaticData() =>
            Container.Bind<IStaticData>().To<StaticData>().AsSingle();
        
        private void RegisterUIFactory() =>
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        
        private void RegisterPersistentEntityFactory() =>
            Container.Bind<IPersistentEntityFactory>().To<PersistentEntityFactory>().AsSingle();
    }
}