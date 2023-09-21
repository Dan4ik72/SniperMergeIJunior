using VContainer;

public class UIPanelTransitServiceGameSceneInstaller : Installer
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<UIPanelTransitService>(container =>
        {
            var allPanelsList = container.Resolve<UIBootstrapService>().AllPanels;
            
            return new UIPanelTransitService(allPanelsList);
            
        }, Lifetime.Scoped);
    }
}