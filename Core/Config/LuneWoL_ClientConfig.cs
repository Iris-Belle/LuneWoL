namespace LuneWoL.Core.Config;

[BackgroundColor(35, 80, 130, 200)]
public class LuneWoL_ClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [BackgroundColor(180, 215, 255, 255), DefaultValue(false)]
    public bool STFUCHAT { get; set; }

    public override void OnLoaded() => ClientConfig = this;
}
