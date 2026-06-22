using HarmonyLib;
using Timberborn.Gathering;
using Timberborn.ModManagerScene;
using Timberborn.TemplateSystem;
using UnityEngine;
using System.Collections.Generic;

namespace Calloatti.ZapThatSap
{
    /// <summary>
    /// Entry point for the Timberborn Modding System.
    /// </summary>
    public class ModStarter : IModStarter
    {
        public void StartMod(IModEnvironment modEnvironment)
        {
            // Initializes Harmony and applies the visual override.
            var harmony = new Harmony("calloatti.zapthatsap");
            harmony.PatchAll();
        }
    }

    /// <summary>
    /// Patches GatherableModel to intercept the resin visibility toggle.
    /// </summary>
    [HarmonyPatch(typeof(GatherableModel), "UpdateMaterial")]
    public static class Patch_GatherableModel_UpdateMaterial
    {
        // Internal shader property ID for the sap/detail texture.
        private static readonly int EnableDetailId = Shader.PropertyToID("_EnableDetail");

        /// <summary>
        /// Prefix to override the shader float specifically for Pine trees.
        /// </summary>
        /// <param name="__instance">The GatherableModel instance.</param>
        /// <param name="____meshRenderers">Private list of MeshRenderers in the Mature model.</param>
        static bool Prefix(GatherableModel __instance, List<MeshRenderer> ____meshRenderers)
        {
            // Verify if the entity is the Pine tree defined in the blueprint.
            var template = __instance.GetComponent<TemplateSpec>();
            
            if (template != null && template.TemplateName == "Pine")
            {
                // Force the shader's 'EnableDetail' float to 0 (off).
                foreach (MeshRenderer meshRenderer in ____meshRenderers)
                {
                    if (meshRenderer != null && meshRenderer.material != null)
                    {
                        meshRenderer.material.SetFloat(EnableDetailId, 0f);
                    }
                }
                
                // Return false to skip original logic, preventing the sap from appearing.
                return false; 
            }

            // Allow normal behavior for other resources like Blueberries or Chestnuts.
            return true;
        }
    }
}