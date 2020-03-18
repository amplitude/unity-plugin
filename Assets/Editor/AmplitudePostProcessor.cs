using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public static class AmplitudePostProcessor {

  [PostProcessBuild]
  public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath) {
    if (buildTarget == BuildTarget.iOS) {
      var projPath = string.Concat(buildPath, "/Unity-iPhone.xcodeproj/project.pbxproj");
      var proj = new PBXProject();
      proj.ReadFromFile(projPath);

      var targetGuid = proj.TargetGuidByName(PBXProject.GetUnityTargetName());

      // Configure build settings
      proj.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-lsqlite3.0");
      proj.SetBuildProperty(targetGuid, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
      proj.WriteToFile(projPath);
    }
  }
}