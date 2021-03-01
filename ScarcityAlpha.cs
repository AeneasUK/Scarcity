using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using kremnev8;
using UnityEngine;
using xiaoye97;


[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace ScarcityAlpha
{
    [BepInPlugin("org.Aeneas.plugin.scarcityalpha", "Scarcity Alpha", "0.1.0")]
    public class ScarcityAlpha : BaseUnityPlugin
    {
        public static ManualLogSource logger;

        void Awake()
        {
            logger = Logger;

            Registry.Init("scarcitybundle", "assets", true, false);

            Registry.registerString("copperWireName", "Copper Wire");
            Registry.registerString("copperWireDesc", "By extruding copper we can make a component which allows current to be carried");

            Registry.registerString("oxygenName", "Oxygen");
            Registry.registerString("oxygenDesc", "One of the most plenty and perhaps one of the most important elements in the cluster. We can use this to support life but also as an oxidiser");




            ItemProto oxygen = Registry.registerItem(10002, "oxygenName", "oxygenDesc", "assets/oxygen", 1710);
            ItemProto wire = Registry.registerItem(10001, "copperWireName", "copperWireDesc", "assets/copper_wire", 1711);

            Registry.registerRecipe(10001, ERecipeType.Assemble, 60, new[] { 1104 }, new[] { 2 }, new[] { wire.ID }, new[] { 1 }, "copperWireDesc", 1);
            Registry.registerRecipe(10002, ERecipeType.Refine, 120, new[] { 1000 }, new[] { 2 }, new[] { oxygen.ID, 1120 }, new[] { 1, 2 }, "oxygenDesc", 1102);



            LDBTool.PostAddDataAction += EditRecipes;

            logger.LogInfo("Scarcity ALPHA has initialized!");
        }
        void EditRecipes()
        {
            //Tier 1

            //Copper Wire
            var item = LDB.items.Select(10001);
            item.StackSize = 100;
            item.makes = new List<RecipeProto>() { LDB.recipes.Select(50), LDB.recipes.Select(6) };

            //Oxygen
            var recipe = LDB.recipes.Select(10002);
            recipe.Handcraft = false;
            recipe.Explicit = true;
            //Circuit Board
            recipe = LDB.recipes.Select(50);
            recipe.Items = new int[] { 10001, 1101 };
            recipe.ItemCounts = new int[] { 1, 1 };
            recipe.ResultCounts = new int[] { 1 };
            item = LDB.items.Select(1202);
            item.handcraftProductCount = 1;

            //Gear
            recipe = LDB.recipes.Select(5);
            recipe.ItemCounts = new int[] { 2 };
            recipe.ResultCounts = new int[] { 1 };
            //Magnet
            recipe = LDB.recipes.Select(2);
            recipe.ItemCounts = new int[] { 2 };
            recipe.ResultCounts = new int[] { 1 };
            //Electromagnet
            recipe = LDB.recipes.Select(6);
            recipe.Items = new int[] { 1102, 10001 };
            recipe.ItemCounts = new int[] { 1, 1 };
            recipe.ResultCounts = new int[] { 1 };
            item = LDB.items.Select(1202);
            item.handcraftProductCount = 1;

            //Glass
            recipe = LDB.recipes.Select(57);
            recipe.ItemCounts = new int[] { 3 };
            //Stone Bricks
            recipe = LDB.recipes.Select(4);
            recipe.ItemCounts = new int[] { 2 };
            //Prism
            recipe = LDB.recipes.Select(11);
            recipe.ItemCounts = new int[] { 3 };

            //Tier 2

            //Energetic Graphite
            recipe = LDB.recipes.Select(17);
            recipe.ItemCounts = new int[] { 3 };
            //Refined Oil
            recipe = LDB.recipes.Select(16);
            recipe.ItemCounts = new int[] { 1 };
            recipe.Results = new int[] { 1114 };
            recipe.ResultCounts = new int[] { 1 };
            recipe.TimeSpend = 180;
            //Steel
            recipe = LDB.recipes.Select(63);
            recipe.Items = new int[] { 1101, 10002 };
            recipe.Type = ERecipeType.Smelt;
            recipe.ItemCounts = new int[] { 3, 1 };
            recipe.ResultCounts = new int[] { 1 };
            //Refinery Building
            recipe = LDB.recipes.Select(15);
            recipe.Items = new int[] { 1101, 1108, 1301, 1401 };
            recipe.ItemCounts = new int[] { 10, 10, 6, 6 };
            //Plasma Exciter
            recipe = LDB.recipes.Select(12);
            recipe.Items = new int[] { 1111, 1202 };
            recipe.ItemCounts = new int[] { 2, 6 };
            recipe.ResultCounts = new int[] { 1 };
            //Graphene
            recipe = LDB.recipes.Select(31);
            recipe.Items = new int[] { 1109, 1116 };
            recipe.ItemCounts = new int[] { 3, 1 };
            recipe.ResultCounts = new int[] { 2 };
            //Diamond
            recipe = LDB.recipes.Select(60);
            recipe.Items = new int[] { 1123 };
            recipe.ItemCounts = new int[] { 2 };
            recipe.ResultCounts = new int[] { 1 };
            recipe.preTech = LDB.techs.Select(1);
            //Crystal Silicon
            recipe = LDB.recipes.Select(37);
            recipe.Items = new int[] { 1112, 1105 };
            recipe.ItemCounts = new int[] { 1, 1 };
            recipe.ResultCounts = new int[] { 1 };
            recipe.preTech = LDB.techs.Select(1);
            //HPS
            recipe = LDB.recipes.Select(59);
            item = LDB.items.Select(1105);
            item.Name = "Synthetic Silicon Wafers";
            recipe.Name = "Synthetic Silicon Wafers";
            recipe.Items = new int[] { 1003 };
            recipe.ItemCounts = new int[] { 1 };
            recipe.ResultCounts = new int[] { 1 };
            //X-Ray Cracking
            recipe = LDB.recipes.Select(58);
            recipe.Items = new int[] { 1114, 1120 };
            recipe.ItemCounts = new int[] { 1, 1 };
            recipe.Results = new int[] { 1120, 1109 };
            recipe.ResultCounts = new int[] { 3, 1 };
            //Red Jello
            recipe = LDB.recipes.Select(18);
            recipe.Items = new int[] { 1303, 1123 };
            recipe.ItemCounts = new int[] { 1, 1 };
            //Silicon Ore Recipe
            recipe = LDB.recipes.Select(34);
            recipe.Items = new int[] { };
            recipe.Type = ERecipeType.Chemical;
            recipe.TimeSpend = 120;
            recipe.Items = new int[] { 1115 };
            recipe.ItemCounts = new int[] { 1 };
            recipe.ResultCounts = new int[] { 3 };
            //Microcrystalline Component
            recipe = LDB.recipes.Select(53);
            recipe.ItemCounts = new int[] { 1, 1 };
            
            //Tier Three

            //Yellow Jello
            recipe = LDB.recipes.Select(27);
            recipe.Items = new int[] { 1113, 1118 };
            recipe.ItemCounts = new int[] { 1, 1 };
            //Titanium Crystals
            recipe = LDB.recipes.Select(26);
            item = LDB.items.Select(1118);
            item.Name = "High Strength Crystals";
            recipe.Name = "High Strength Crystals";
            recipe.Items = new int[] { 1117, 1103 };
            recipe.ItemCounts = new int[] { 1, 1 };



        }

    }

}
