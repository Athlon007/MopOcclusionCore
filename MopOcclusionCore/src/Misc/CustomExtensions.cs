using MSCLoader;

namespace MopOcclusionCore
{
    static class CustomExtensions
    {
        /// <summary>
        /// Checks if string contains any of the values provided in lookFor.
        /// </summary>
        /// <param name="lookIn">String in which we should look for</param>
        /// <param name="lookFor">Values that we want to look for in lookIn</param>
        /// <returns></returns>
        public static bool ContainsAny(this string lookIn, params string[] lookFor)
        {
            for (int i = 0; i < lookFor.Length; i++)
            {
                // Value found? Return true.
                if (lookIn.Contains(lookFor[i]))
                {
                    return true;
                }
            }

            // Nothing has been found? Return false.
            return false;
        }

        public static bool EqualsAny(this string lookIn, params string[] lookFor)
        {
            for (int i = 0; i < lookFor.Length; i++)
            {
                // Value found? Return true.
                if (lookIn == lookFor[i])
                {
                    return true;
                }
            }

            // Nothing has been found? Return false.
            return false;
        }

        public static bool IsSatsumaRelatedModPresent()
        {
            var mods = ModLoader.LoadedMods;
            foreach (var mod in mods)
                if (mod.ID.EqualsAny("SuperchargerForSatsuma", "SatsumaTurboCharger", "DonnerTech_ECU_Mod"))
                    return true;

            return false;
        }
    }
}
