using UnrealBuildTool;

public class ArcadeGameTarget : TargetRules
{
	public ArcadeGameTarget(TargetInfo Target) : base(Target)
	{
		DefaultBuildSettings = BuildSettingsVersion.V3;
		IncludeOrderVersion = EngineIncludeOrderVersion.Latest;
		Type = TargetType.Game;
		ExtraModuleNames.Add("ArcadeGame");
	}
}
