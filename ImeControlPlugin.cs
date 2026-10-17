using LoupixDeck.PluginSdk;

namespace LoupixDeck.Plugin.ImeControl;

public sealed class ImeControlPlugin : LoupixPlugin
{
    public override PluginMetadata Metadata { get; } = new()
    {
        Id = "imecontrol",
        Name = "IME Control",
        Version = new Version(1, 0, 0),
        SdkVersion = SdkInfo.Version,
        Author = "dokugasu",
        Description = "Turn the IME of the active window on or off.\n\n--\n\nアクティブなウィンドウの IME を ON / OFF します。"
    };

    public override void Initialize(IPluginHost host)
    {
    }

    public override IEnumerable<IPluginCommand> GetCommands()
    {
        yield return new SetImeCommand("ImeControl.Off", "IME OFF", open: false);
        yield return new SetImeCommand("ImeControl.On", "IME ON", open: true);
    }
}

internal sealed class SetImeCommand(string commandName, string displayName, bool open) : IPluginCommand
{
    public CommandDescriptor Descriptor { get; } = new()
    {
        CommandName = commandName,
        DisplayName = displayName,
        Group = "IME Control"
    };

    public ButtonTargets SupportedTargets => ButtonTargets.All;

    public Task Execute(CommandContext ctx)
    {
        try
        {
            if (!OperatingSystem.IsWindows())
                return Task.CompletedTask;

            var changed = ImeControl.SetOpenStatus(open);
            ctx.Host.Logger.Info($"{displayName}: {(changed ? "changed" : "no change")}");
        }
        catch (Exception ex)
        {
            ctx.Host.Logger.Error($"{displayName} failed", ex);
        }
        return Task.CompletedTask;
    }
}
