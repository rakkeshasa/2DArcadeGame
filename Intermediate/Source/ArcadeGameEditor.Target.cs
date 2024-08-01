using UnrealBuildTool;

public class ArcadeGameEditorTarget : TargetRules
{
	public ArcadeGameEditorTarget(TargetInfo Target) : base(Target)
	{
		DefaultBuildSettings = BuildSettingsVersion.V3;
		IncludeOrderVersion = EngineIncludeOrderVersion.Latest;
		Type = TargetType.Editor;
		ExtraModuleNames.Add("ArcadeGame");
	}
}
