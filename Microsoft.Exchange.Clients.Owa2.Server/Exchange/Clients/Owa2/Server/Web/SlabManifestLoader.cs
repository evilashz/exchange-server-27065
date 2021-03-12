using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200049E RID: 1182
	internal class SlabManifestLoader
	{
		// Token: 0x0600284E RID: 10318 RVA: 0x00094AD4 File Offset: 0x00092CD4
		public static SlabManifest Load(XmlDocument document, string fileName, Action<string, Exception> logError)
		{
			SlabManifest result;
			try
			{
				if (document.DocumentElement == null)
				{
					throw new SlabManifestFormatException(string.Format("XmlDocument for {0} does not have a root.", fileName));
				}
				XmlNodeList nodes = document.DocumentElement.SelectNodes(string.Format("//slab[@name!='{0}']", "boot"));
				Dictionary<string, SlabDefinition> slabDefinitions = SlabManifestLoader.LoadNonBootSlabDefinitions(nodes);
				XmlNode xmlNode = document.DocumentElement.SelectSingleNode(string.Format("//slab[@name='{0}']", "boot"));
				result = ((xmlNode == null) ? new SlabManifest(slabDefinitions) : new SlabManifest(SlabManifestLoader.LoadBootSlabDefinition(xmlNode), slabDefinitions));
			}
			catch (Exception arg)
			{
				logError(fileName, arg);
				throw;
			}
			return result;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x00094B74 File Offset: 0x00092D74
		public static SlabManifest Load(string folder, string slabManifestFileName, Action<string, Exception> logError, string manifestDiskRelativeFolderPath)
		{
			string text = Path.Combine(folder, manifestDiskRelativeFolderPath, slabManifestFileName);
			SlabManifest result;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.Load(text);
					result = SlabManifestLoader.Load(xmlDocument, text, delegate(string param0, Exception param1)
					{
					});
				}
				catch (XmlException ex)
				{
					throw new SlabManifestFormatException(ex.Message, ex);
				}
			}
			catch (SlabManifestException arg)
			{
				logError(text, arg);
				throw;
			}
			return result;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x00094BF8 File Offset: 0x00092DF8
		private static BootSlabDefinition LoadBootSlabDefinition(XmlNode node)
		{
			return SlabManifestLoader.LoadSlab<BootSlabDefinition>(node).Value;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00094C14 File Offset: 0x00092E14
		private static Dictionary<string, SlabDefinition> LoadNonBootSlabDefinitions(XmlNodeList nodes)
		{
			Dictionary<string, SlabDefinition> dictionary = new Dictionary<string, SlabDefinition>();
			for (int i = 0; i < nodes.Count; i++)
			{
				KeyValuePair<string, SlabDefinition> keyValuePair = SlabManifestLoader.LoadSlab<SlabDefinition>(nodes[i]);
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00094C5C File Offset: 0x00092E5C
		private static KeyValuePair<string, T> LoadSlab<T>(XmlNode node) where T : SlabDefinition, new()
		{
			if (node.LocalName != "slab")
			{
				throw new SlabManifestFormatException(string.Format("slab node expected. Found {0}", node.LocalName));
			}
			string attributeValue = SlabManifestLoader.GetAttributeValue(node, "name");
			if (string.IsNullOrEmpty(attributeValue))
			{
				throw new SlabManifestFormatException("slab attribute 'name' was expected");
			}
			if (node["types"] == null)
			{
				throw new SlabManifestFormatException(string.Format("slab node must contain a 'types' node", new object[0]));
			}
			if (node["templates"] == null)
			{
				throw new SlabManifestFormatException(string.Format("slab node must contain a 'templates' node", new object[0]));
			}
			if (node["bindings"] == null)
			{
				throw new SlabManifestFormatException(string.Format("slab node must contain a 'bindings' node", new object[0]));
			}
			string[] array = SlabManifestLoader.LoadSlabTypes(node["types"]);
			string[] array2 = SlabManifestLoader.LoadSlabTemplates(node["templates"]);
			IList<SlabBinding> list = SlabManifestLoader.LoadSlabBindings(attributeValue, node["bindings"]);
			T t = Activator.CreateInstance<T>();
			Array.ForEach<string>(array, new Action<string>(t.AddType));
			Array.ForEach<string>(array2, new Action<string>(t.AddTemplate));
			foreach (SlabBinding binding in list)
			{
				t.AddBinding(binding);
			}
			return new KeyValuePair<string, T>(attributeValue, t);
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00094DD8 File Offset: 0x00092FD8
		private static IList<SlabBinding> LoadSlabBindings(string slabName, XmlNode node)
		{
			if (node.LocalName != "bindings")
			{
				throw new SlabManifestFormatException("Expected xml node with local name 'bindings' but found " + node.LocalName);
			}
			List<SlabBinding> list = new List<SlabBinding>();
			bool flag = false;
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				SlabBinding slabBinding = SlabManifestLoader.LoadSlabSingleBinding(node.ChildNodes[i]);
				if (flag && slabBinding.IsDefault)
				{
					throw new SlabManifestFormatException("Multiple default binding found slab " + slabName);
				}
				flag |= slabBinding.IsDefault;
				list.Add(slabBinding);
			}
			return list;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x00094E6C File Offset: 0x0009306C
		private static SlabBinding LoadSlabSingleBinding(XmlNode node)
		{
			if (node.LocalName != "binding")
			{
				throw new SlabManifestFormatException("Expected xml node with local name 'binding' but found " + node.LocalName);
			}
			string scope = (node["scope"] != null) ? node["scope"].InnerText : null;
			SlabStyleFile[] styles = (node["packagedStyles"] != null && node["packagedStyles"].HasChildNodes) ? SlabManifestLoader.LoadSlabStyleFiles(node["packagedStyles"]) : SlabManifestLoader.LoadSlabStyleFiles(node["styles"]);
			string[] features = (node["features"] == null) ? new string[0] : SlabManifestLoader.LoadSlabBindingFeatures(node["features"]);
			string[] dependencies = (node["dependencies"] == null) ? new string[0] : SlabManifestLoader.LoadSlabBindingDependencies(node["dependencies"]);
			SlabConfiguration[] configurations = (node["configurations"] == null) ? new SlabConfiguration[0] : SlabManifestLoader.LoadSlabBindingConfigurations(node["configurations"]);
			SlabSourceFile[] sources = (node["sources"] == null) ? new SlabSourceFile[0] : SlabManifestLoader.LoadSlabBindingSourceFiles(node["sources"]);
			SlabSourceFile[] packagedSources = (node["packagedSources"] == null) ? new SlabSourceFile[0] : SlabManifestLoader.LoadSlabBindingSourceFiles(node["packagedSources"]);
			SlabStringFile[] strings = (node["strings"] == null) ? new SlabStringFile[0] : SlabManifestLoader.LoadSlabBindingStringFiles(node["strings"]);
			SlabStringFile[] packagedStrings = (node["packagedStrings"] == null) ? new SlabStringFile[0] : SlabManifestLoader.LoadSlabBindingStringFiles(node["packagedStrings"]);
			SlabFontFile[] fonts = (node["fonts"] == null) ? new SlabFontFile[0] : SlabManifestLoader.LoadSlabBindingFontFiles(node["fonts"]);
			SlabImageFile[] images = (node["images"] == null) ? new SlabImageFile[0] : SlabManifestLoader.LoadSlabBindingImageFiles(node["images"]);
			return new SlabBinding
			{
				Features = features,
				Configurations = configurations,
				Dependencies = dependencies,
				Styles = styles,
				PackagedSources = packagedSources,
				Sources = sources,
				PackagedStrings = packagedStrings,
				Strings = strings,
				Fonts = fonts,
				Images = images,
				Scope = scope
			};
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000950D0 File Offset: 0x000932D0
		private static string[] LoadSlabTypes(XmlNode node)
		{
			if (node.LocalName != "types")
			{
				throw new SlabManifestFormatException(string.Format("types node expected. Found {0}", node.LocalName));
			}
			string[] array = new string[node.ChildNodes.Count];
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				array[i] = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "name");
			}
			return array;
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x00095148 File Offset: 0x00093348
		private static string[] LoadSlabTemplates(XmlNode node)
		{
			if (node.LocalName != "templates")
			{
				throw new SlabManifestFormatException(string.Format("templates node expected. Found {0}", node.LocalName));
			}
			string[] array = new string[node.ChildNodes.Count];
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				array[i] = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "name");
			}
			return array;
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000951C0 File Offset: 0x000933C0
		private static SlabStyleFile[] LoadSlabStyleFiles(XmlNode node)
		{
			if (node.LocalName != "styles" && node.LocalName != "packagedStyles")
			{
				throw new SlabManifestFormatException(string.Format("styles node expected. Found {0}", node.LocalName));
			}
			SlabStyleFile[] array = new SlabStyleFile[node.ChildNodes.Count];
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				string attributeValue = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "layout");
				string attributeValue2 = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "type");
				array[i] = new SlabStyleFile
				{
					Name = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "name"),
					Layout = SlabManifestLoader.GetResourceLayout(attributeValue),
					Type = attributeValue2
				};
			}
			return array;
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x0009529C File Offset: 0x0009349C
		private static string[] LoadSlabBindingDependencies(XmlNode node)
		{
			if (node.LocalName != "dependencies")
			{
				throw new SlabManifestFormatException(string.Format("dependencies node expected. Found {0}", node.LocalName));
			}
			string[] array = new string[node.ChildNodes.Count];
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				array[i] = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "slab");
			}
			return array;
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x00095314 File Offset: 0x00093514
		private static string[] LoadSlabBindingFeatures(XmlNode node)
		{
			if (node.LocalName != "features")
			{
				throw new SlabManifestFormatException(string.Format("features node expected. Found {0}", node.LocalName));
			}
			string[] array = new string[node.ChildNodes.Count];
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				array[i] = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "name");
			}
			return array;
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x0009538C File Offset: 0x0009358C
		private static SlabConfiguration[] LoadSlabBindingConfigurations(XmlNode node)
		{
			if (node.LocalName != "configurations")
			{
				throw new SlabManifestFormatException(string.Format("configurations node expected. Found {0}", node.LocalName));
			}
			SlabConfiguration[] array = new SlabConfiguration[node.ChildNodes.Count];
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				string attributeValue = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "type");
				string attributeValue2 = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "layout");
				array[i] = new SlabConfiguration
				{
					Type = attributeValue,
					Layout = SlabManifestLoader.GetResourceLayout(attributeValue2)
				};
			}
			return array;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x00095438 File Offset: 0x00093638
		private static SlabSourceFile[] LoadSlabBindingSourceFiles(XmlNode node)
		{
			if (node.LocalName != "sources" && node.LocalName != "packagedSources" && node.LocalName != "strings")
			{
				throw new SlabManifestFormatException(string.Format("sources or packagedSources node expected. Found {0}", node.LocalName));
			}
			SlabSourceFile[] array = new SlabSourceFile[node.ChildNodes.Count];
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				string attributeValue = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "name");
				string attributeValue2 = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "layout");
				if (attributeValue != null)
				{
					array[i] = new SlabSourceFile
					{
						Name = attributeValue,
						Layout = SlabManifestLoader.GetResourceLayout(attributeValue2)
					};
				}
			}
			return array;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0009550C File Offset: 0x0009370C
		private static SlabStringFile[] LoadSlabBindingStringFiles(XmlNode node)
		{
			if (node.LocalName != "strings" && node.LocalName != "packagedStrings")
			{
				throw new SlabManifestFormatException(string.Format("strings node expected. Found {0}", node.LocalName));
			}
			SlabStringFile[] array = new SlabStringFile[node.ChildNodes.Count];
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				string attributeValue = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "name");
				string attributeValue2 = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "type");
				string attributeValue3 = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "layout");
				if (attributeValue != null)
				{
					array[i] = new SlabStringFile
					{
						Name = attributeValue,
						Type = attributeValue2,
						Layout = SlabManifestLoader.GetResourceLayout(attributeValue3)
					};
				}
			}
			return array;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000955F4 File Offset: 0x000937F4
		private static SlabFontFile[] LoadSlabBindingFontFiles(XmlNode node)
		{
			if (node.LocalName != "fonts")
			{
				throw new SlabManifestFormatException(string.Format("fonts node expected. Found {0}", node.LocalName));
			}
			SlabFontFile[] array = new SlabFontFile[node.ChildNodes.Count];
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				string attributeValue = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "name");
				string attributeValue2 = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "layout");
				if (attributeValue != null)
				{
					array[i] = new SlabFontFile
					{
						Name = attributeValue,
						Layout = SlabManifestLoader.GetResourceLayout(attributeValue2)
					};
				}
			}
			return array;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000956A4 File Offset: 0x000938A4
		private static SlabImageFile[] LoadSlabBindingImageFiles(XmlNode node)
		{
			if (node.LocalName != "images")
			{
				throw new SlabManifestFormatException(string.Format("images node expected. Found {0}", node.LocalName));
			}
			SlabImageFile[] array = new SlabImageFile[node.ChildNodes.Count];
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				string attributeValue = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "name");
				string attributeValue2 = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "layout");
				string attributeValue3 = SlabManifestLoader.GetAttributeValue(node.ChildNodes[i], "type");
				if (string.IsNullOrEmpty(attributeValue3))
				{
					throw new SlabManifestFormatException(string.Format("image node requires a type attribute", new object[0]));
				}
				if (attributeValue != null)
				{
					array[i] = new SlabImageFile
					{
						Name = attributeValue,
						Type = attributeValue3,
						Layout = SlabManifestLoader.GetResourceLayout(attributeValue2)
					};
				}
			}
			return array;
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0009579C File Offset: 0x0009399C
		private static string GetAttributeValue(XmlNode node, string attributeName)
		{
			if (node.Attributes == null)
			{
				return null;
			}
			XmlAttribute xmlAttribute = node.Attributes[attributeName];
			if (xmlAttribute != null)
			{
				return xmlAttribute.Value;
			}
			return null;
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000957CC File Offset: 0x000939CC
		private static ResourceLayout GetResourceLayout(string layout)
		{
			if (layout == null)
			{
				return ResourceLayout.Any;
			}
			if (layout.ToLower().Contains("narrow"))
			{
				return ResourceLayout.TNarrow;
			}
			if (layout.ToLower().Contains("wide"))
			{
				return ResourceLayout.TWide;
			}
			if (layout.ToLower().Contains("mouse"))
			{
				return ResourceLayout.Mouse;
			}
			return ResourceLayout.Any;
		}
	}
}
