﻿// Modern Optimization Plugin - Occlusion Core
// Copyright(C) 2019-2020 Athlon

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.

using MSCLoader;
using UnityEngine;

namespace MopOcclusionCore
{
    public class MopOcclusionCore : Mod
    {
        public override string ID => "MopOcclusionCore"; //Your mod ID (unique)
        public override string Name => "MOP: Occlusion Core (Beta)"; //You mod name
        public override string Author => "Athlon"; //Your Username
        public override string Version => "0.2"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => false;
        public override bool SecondPass => true;

        public override void SecondPassOnLoad()
        {
            new Occlusion();
        }

        // Occlussion Sample Detail
        public static Settings occlusionSamplesLower = new Settings("occlusionSamplesLow", "Lowest (fastest)", false, MopOcclusionCoreSettings.UpdateAll);
        public static Settings occlusionSamplesLow = new Settings("occlusionSamplesLow", "Low", false, MopOcclusionCoreSettings.UpdateAll);
        public static Settings occlusionSamplesDetailed = new Settings("occlusionSamplesDetailed", "High (default)", true, MopOcclusionCoreSettings.UpdateAll);
        public static Settings occlusionSamplesVeryDetailed = new Settings("occlusionSamplesVeryDetailed", "Very High (slowest)", false, MopOcclusionCoreSettings.UpdateAll);
        // Delay and Range
        public static Settings minOcclusionDistance = new Settings("minOcclusionDistance", "Minimum Distance", 50, MopOcclusionCoreSettings.UpdateAll);
        public static Settings occlusionDistance = new Settings("occlusionDistance", "Maximum Distance", 400, MopOcclusionCoreSettings.UpdateAll);
        // Occlusion Method
        public static Settings occlusionChequered = new Settings("occlusionChequered", "Fast (Default)", true, MopOcclusionCoreSettings.UpdateAll);
        public static Settings occlusionDouble = new Settings("occlusionDouble", "Fancy", false, MopOcclusionCoreSettings.UpdateAll);

        readonly Color32 headerColor = new Color32(46, 149, 180, 255);

        public override void ModSettings()
        {
            // All settings should be created here. 
            // DO NOT put anything else here that settings.
            // Occlusion
            Settings.AddText(this, "Occlusion Culling disables rendering of objects not visible by camera.");
            Settings.AddText(this, "Note: Occlusion Culling is the experimental feature that may cause bigger or smaller graphical glitches!\n");
            Settings.AddSlider(this, minOcclusionDistance, 10, 500);
            Settings.AddText(this, "Minimum distance after which the object's visiblity in camera is checked." +
                " If you're closer to the object than this value, the object will not be hidden, even when you not look at it.");
            Settings.AddSlider(this, occlusionDistance, 200, 3000);
            Settings.AddText(this, "How far the rays will be sent which check visible objects. " +
                "Objects that are further than that value will not be checked.\nWARNING: large values" +
                " may cause lag.");
            Settings.AddHeader(this, "Sampling Level of Detail", headerColor);
            Settings.AddCheckBox(this, occlusionSamplesLower, "occlusionSampleDetail");
            Settings.AddCheckBox(this, occlusionSamplesLow, "occlusionSampleDetail");
            Settings.AddCheckBox(this, occlusionSamplesDetailed, "occlusionSampleDetail");
            Settings.AddCheckBox(this, occlusionSamplesVeryDetailed, "occlusionSampleDetail");
            Settings.AddHeader(this, "Occlusion Method", headerColor);
            Settings.AddCheckBox(this, occlusionChequered, "occlusionMethod");
            Settings.AddCheckBox(this, occlusionDouble, "occlusionMethod");
        }
    }
}
