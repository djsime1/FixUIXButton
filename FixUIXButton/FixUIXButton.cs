using HarmonyLib;
using ResoniteModLoader;
using FrooxEngine.UIX;

namespace FixUIXButton
{
    public class FixUIXButton : ResoniteMod
    {
        public override string Name => "FixUIXButton";
        public override string Author => "art0007i & djsime1";
        public override string Version => "2.0.0";
        public override string Link => "https://github.com/djsime1/FixUIXButton/";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("me.art0007i.FixUIXButton");
            harmony.PatchAll();

        }
        [HarmonyPatch(typeof(RectTransform), "BuildInspectorUI")]
        class FixUIXButtonPatch
        {
            public static void Postfix(UIBuilder ui, RectTransform __instance)
            {
                var btn = ui.Button("Fix UIX");
                btn.LocalPressed += (button, data) => {
                    var canvas = AccessTools.Field(__instance.GetType(), "_registeredCanvas").GetValue(__instance) as Canvas;

                    AccessTools.Method(typeof(Canvas), "RegisterDirtyTransform").Invoke(canvas, new object[] { __instance });
                };
            }
        }
    }
}