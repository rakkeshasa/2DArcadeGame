using UnrealBuildTool;

public class ArcadeGameClientTarget : TargetRules
{
	public ArcadeGameClientTarget(TargetInfo Target) : base(Target)
	{
		DefaultBuildSettings = BuildSettingsVersion.V3;
		IncludeOrderVersion = EngineIncludeOrderVersion.Latest;
		Type = TargetType.Client;
		ExtraModuleNames.Add("ArcadeGame");
	}
}
