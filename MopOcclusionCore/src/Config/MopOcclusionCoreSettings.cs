// Modern Optimization Plugin - Occlusion Core
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

namespace MopOcclusionCore
{
    internal enum OcclusionMethods { Chequered, Double }

    class MopOcclusionCoreSettings
    {
        // This is the master switch of MOP. If deactivated, all functions will freeze.
        public static bool IsModActive { get; set; }

        //
        // OCCLUSION CULLING
        //
        static int occlusionSamples = 120;
        public static int OcclusionSamples { get => occlusionSamples; }

        static int viewDistance = 400;
        public static int ViewDistance { get => viewDistance; }

        static int occlusionSampleDelay = 1;
        public static int OcclusionSampleDelay { get => occlusionSampleDelay; }

        static int minOcclusionDistance = 50;
        public static int MinOcclusionDistance { get => minOcclusionDistance; }

        static OcclusionMethods occlusionMethod = OcclusionMethods.Chequered;
        public static OcclusionMethods OcclusionMethod { get => occlusionMethod; }

        // OcclusionHideDelay is calculated automatically
        public static int OcclusionHideDelay = -1;

        // Checked after the warning about maximum distance and minimum distance of occlusion culling is showed,
        // so it won't be displayed again.
        static bool occlusionDistanceWarningShowed;

        public static void UpdateAll()
        {
            // Occlusion Culling
            if (viewDistance < minOcclusionDistance)
            {
                MopOcclusionCore.occlusionDistance.Value = 400;
                MopOcclusionCore.minOcclusionDistance.Value = 50;

                if (!occlusionDistanceWarningShowed)
                {
                    MSCLoader.ModUI.ShowMessage("Occlusion Distance cannot be lower than Minimum Occlusion Distance." +
                        "\n\nIf you don't change it, both values will be reset to default!", "Error");

                    occlusionDistanceWarningShowed = true;
                }
            }

            occlusionSamples = GetOcclusionSamples();
            viewDistance = int.Parse(MopOcclusionCore.occlusionDistance.GetValue().ToString());
            minOcclusionDistance = int.Parse(MopOcclusionCore.minOcclusionDistance.GetValue().ToString());
            occlusionMethod = GetOcclusionMethod();
        }

        /// <summary>
        /// Returns the occlusion method used
        /// </summary>
        static OcclusionMethods GetOcclusionMethod()
        {
            return (bool)MopOcclusionCore.occlusionDouble.GetValue() ? OcclusionMethods.Double : OcclusionMethods.Chequered;
        }

        /// <summary>
        /// Returns the the occlusion sampling value
        /// </summary>
        static int GetOcclusionSamples()
        {
            if ((bool)MopOcclusionCore.occlusionSamplesLower.GetValue())
                return 30;

            if ((bool)MopOcclusionCore.occlusionSamplesLow.GetValue())
                return 60;

            if ((bool)MopOcclusionCore.occlusionSamplesVeryDetailed.GetValue())
                return 240;

            return 120;
        }
    }
}
