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

using UnityEngine;
using System.Xml;
using MSCLoader;

namespace MopOcclusionCore
{
    class Occlusion
    {
        // This class reads through occlusiontable.xml file for objects that need to be injected with OcclusionObject script.

        public Occlusion()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Properties.Resources.occlusiontable);
            XmlNodeList root = doc.SelectNodes("World/Object");
            ReadChildNode(root, "");

            Camera.main.gameObject.AddComponent<OcclusionCamera>();

            ModConsole.Print("[MOP: Occlusion Core] Occlusion listing done.");
        }

        /// <summary>
        /// Reads the occlusiontable.xml file
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="path"></param>
        void ReadChildNode(XmlNodeList nodeList, string path)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode node = nodeList[i];
                string name = node.Attributes["name"].Value;
                string pathToSelf = path != "" ? path + "/" + name : name;
                bool exception = node.Attributes["exception"] != null;

                if (!exception)
                {
                    GameObject gm = GameObject.Find(pathToSelf);

                    if (gm == null)
                    {
                        ModConsole.Error("[MOP: Occlusion Core] Object not found: " + pathToSelf);
                        continue;
                    }

                    if (gm.GetComponent<OcclusionObject>() == null)
                    {
                        if (CustomExtensions.IsSatsumaRelatedModPresent() 
                            && (gm.name.StartsWith("SATSUMA") || gm.transform.root.gameObject.name.StartsWith("SATSUMA")))
                            continue;

                        gm.AddComponent<OcclusionObject>();
                    }
                }

                if (node.ChildNodes.Count > 0)
                {
                    ReadChildNode(node.SelectNodes("Object"), pathToSelf);
                }
            }
        }
    }
}
