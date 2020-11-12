#if UNITY_IOS
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

#if UNITY_2019_3_OR_NEWER
      var targetGuid = proj.GetUnityFrameworkTargetGuid();
#else
      var targetGuid = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
#endif
                
      // Configure build settings
      proj.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-lsqlite3.0");
      proj.SetBuildProperty(targetGuid, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
      proj.WriteToFile(projPath);
    }
  }
}
#endif