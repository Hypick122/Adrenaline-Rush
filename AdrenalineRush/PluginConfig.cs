using BepInEx.Configuration;

namespace Hypick;

public static class Category
{
	public const string General = "1 >> General << 1";
}

public class PluginConfig
{
	public float Multiplier { get; }

	public bool ChangeSpeed { get; }
	public float MaxSpeed { get; }

	public bool ChangeFOV { get; }
	public float MaxFOV { get; }

	public PluginConfig(ConfigFile cfg)
	{
		Multiplier = cfg.Bind<float>(Category.General, nameof(Multiplier), 200f, new ConfigDescription("multiplier to increase the player's speed and FOV (formula: fearLevel * (multiplier / 100))", new AcceptableValueRange<float>(150f, 999f))).Value;

		ChangeSpeed = cfg.Bind<bool>(Category.General, nameof(ChangeSpeed), true, "Should the player's running speed increase when entering fear?").Value;
		MaxSpeed = cfg.Bind<float>(Category.General, nameof(MaxSpeed), 2.5f, "Maximum speed to increase").Value;

		ChangeFOV = cfg.Bind<bool>(Category.General, nameof(ChangeFOV), true, "Should the player's FOV increase when entering fear?").Value;
		MaxFOV = cfg.Bind<float>(Category.General, nameof(MaxFOV), 130f, new ConfigDescription("Maximum FOV to increase", new AcceptableValueRange<float>(66f, 130f))).Value;
	}
}
