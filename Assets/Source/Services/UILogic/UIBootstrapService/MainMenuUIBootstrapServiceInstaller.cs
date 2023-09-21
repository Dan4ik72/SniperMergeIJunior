using VContainer;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class MainMenuUIBootstrapServiceInstaller : Installer
{
    [SerializeField] private ShopOverlayPanel _shopOverlayPanel;
    [SerializeField] private MainMenuBasicOverlayPanel _basicOverlayPanel;
    [SerializeField] private TabTransitionOverlay _tabTransitionOverlay;
    [SerializeField] private LevelSelectionPanel _levelSelectionPanel;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<UIBootstrapService>(container =>
        {
            var panels = new List<IUiPanel>
            {
                container.Resolve<ShopOverlayPanel>(),
                container.Resolve<MainMenuBasicOverlayPanel>(),
                container.Resolve<TabTransitionOverlay>(),
                container.Resolve<LevelSelectionPanel>(),
            };

            return new UIBootstrapService(panels);

        }, Lifetime.Scoped);
        
        //panels
        builder.RegisterComponent(_shopOverlayPanel);
        builder.RegisterComponent(_basicOverlayPanel);
        builder.RegisterComponent(_tabTransitionOverlay);
        builder.RegisterComponent(_levelSelectionPanel);
    }
}