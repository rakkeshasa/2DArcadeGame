using UnrealBuildTool;

public class ArcadeGameServerTarget : TargetRules
{
	public ArcadeGameServerTarget(TargetInfo Target) : base(Target)
	{
		DefaultBuildSettings = BuildSettingsVersion.V3;
		IncludeOrderVersion = EngineIncludeOrderVersion.Latest;
		Type = TargetType.Server;
		ExtraModuleNames.Add("ArcadeGame");
	}
}
