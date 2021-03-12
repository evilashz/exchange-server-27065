using System;
using System.Collections;
using System.DirectoryServices;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004AA RID: 1194
	internal sealed class Gzip
	{
		// Token: 0x06002A20 RID: 10784 RVA: 0x000A7D10 File Offset: 0x000A5F10
		internal static void SetIisGzipMimeTypes()
		{
			using (ServerManager serverManager = new ServerManager())
			{
				Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
				ConfigurationSection section = applicationHostConfiguration.GetSection("system.webServer/httpCompression");
				ConfigurationElement childElement = section.GetChildElement("staticTypes");
				ConfigurationElementCollection collection = childElement.GetCollection();
				int num = -1;
				int num2 = -1;
				for (int i = 0; i < collection.Count; i++)
				{
					if (collection[i].GetAttributeValue("mimeType").ToString() == "*/*")
					{
						num = i;
					}
					else if (collection[i].GetAttributeValue("mimeType").ToString() == "image/*")
					{
						num2 = i;
					}
				}
				if (num == -1)
				{
					ConfigurationElement configurationElement = collection.CreateElement();
					configurationElement.SetAttributeValue("mimeType", "*/*");
					configurationElement.SetAttributeValue("enabled", "true");
					collection.Add(configurationElement);
				}
				else
				{
					ConfigurationElement configurationElement2 = collection[num];
					configurationElement2.SetAttributeValue("enabled", "true");
				}
				if (num2 == -1)
				{
					ConfigurationElement configurationElement3 = collection.CreateElement();
					configurationElement3.SetAttributeValue("mimeType", "image/*");
					configurationElement3.SetAttributeValue("enabled", "false");
					collection.Add(configurationElement3);
				}
				else
				{
					ConfigurationElement configurationElement4 = collection[num2];
					configurationElement4.SetAttributeValue("enabled", "false");
				}
				ConfigurationElement childElement2 = section.GetChildElement("dynamicTypes");
				ConfigurationElementCollection collection2 = childElement2.GetCollection();
				num = -1;
				num2 = -1;
				for (int j = 0; j < collection2.Count; j++)
				{
					if (collection2[j].GetAttributeValue("mimeType").ToString() == "*/*")
					{
						num = j;
					}
					else if (collection2[j].GetAttributeValue("mimeType").ToString() == "image/*")
					{
						num2 = j;
					}
				}
				if (num == -1)
				{
					ConfigurationElement configurationElement5 = collection2.CreateElement();
					configurationElement5.SetAttributeValue("mimeType", "*/*");
					configurationElement5.SetAttributeValue("enabled", "true");
					collection2.Add(configurationElement5);
				}
				else
				{
					ConfigurationElement configurationElement6 = collection2[num];
					configurationElement6.SetAttributeValue("enabled", "true");
				}
				if (num2 == -1)
				{
					ConfigurationElement configurationElement7 = collection2.CreateElement();
					configurationElement7.SetAttributeValue("mimeType", "image/*");
					configurationElement7.SetAttributeValue("enabled", "false");
					collection2.Add(configurationElement7);
				}
				else
				{
					ConfigurationElement configurationElement8 = collection2[num2];
					configurationElement8.SetAttributeValue("enabled", "false");
				}
				serverManager.CommitChanges();
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000A7FCC File Offset: 0x000A61CC
		internal static void SetIisGzipLevel(string site, GzipLevel gzipLevel)
		{
			if (gzipLevel == GzipLevel.Off)
			{
				return;
			}
			string str = site + "/W3SVC";
			bool flag = gzipLevel == GzipLevel.High;
			bool flag2 = true;
			bool flag3 = true;
			string[] array = new string[]
			{
				"htm",
				"html",
				"txt",
				"htc",
				"css",
				"js",
				"xsl",
				"docx",
				"doc",
				"xlsx",
				"xls",
				"pptx",
				"ppt",
				"mdb"
			};
			string[] array2 = new string[]
			{
				"owa",
				"aspx",
				"eas"
			};
			string[] array3 = new string[]
			{
				"ashx"
			};
			int num = 10;
			int num2 = 3;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(str + "/Filters/Compression/gzip"))
			{
				if (gzipLevel == GzipLevel.High)
				{
					IisUtility.SetProperty(directoryEntry, "HCDoDynamicCompression", flag, true);
				}
				IisUtility.SetProperty(directoryEntry, "HCDoStaticCompression", flag2, true);
				IisUtility.SetProperty(directoryEntry, "HCDoOnDemandCompression", flag3, true);
				for (int i = 0; i < array.Length; i++)
				{
					IisUtility.SetProperty(directoryEntry, "HCFileExtensions", array[i], i == 0);
				}
				IList list = null;
				if (directoryEntry.Properties.Contains("HCScriptFileExtensions"))
				{
					list = directoryEntry.Properties["HCScriptFileExtensions"];
				}
				for (int j = 0; j < array2.Length; j++)
				{
					bool flag7 = false;
					if (list != null)
					{
						for (int k = 0; k < list.Count; k++)
						{
							if (list[k] is string && string.Equals(array2[j], (string)list[k], StringComparison.OrdinalIgnoreCase))
							{
								flag7 = true;
								break;
							}
						}
					}
					if (!flag7)
					{
						IisUtility.SetProperty(directoryEntry, "HCScriptFileExtensions", array2[j], false);
					}
				}
				if (list != null)
				{
					for (int l = 0; l < array3.Length; l++)
					{
						for (int m = 0; m < list.Count; m++)
						{
							if (list[m] is string && string.Equals(array3[l], (string)list[m], StringComparison.OrdinalIgnoreCase))
							{
								list.Remove((string)list[m]);
								m = -1;
							}
						}
					}
				}
				IisUtility.SetProperty(directoryEntry, "HCOnDemandCompLevel", num, true);
				if (gzipLevel == GzipLevel.High)
				{
					IisUtility.SetProperty(directoryEntry, "HCDynamicCompressionLevel", num2, true);
				}
				directoryEntry.CommitChanges();
				IisUtility.CommitMetabaseChanges(IisUtility.ServerFromWebSite(site));
			}
			using (DirectoryEntry directoryEntry2 = IisUtility.CreateIISDirectoryEntry(str + "/Filters/Compression/Parameters"))
			{
				IisUtility.SetProperty(directoryEntry2, "HCSendCacheHeaders", flag4, true);
				IisUtility.SetProperty(directoryEntry2, "HCNoCompressionForHTTP10", flag5, true);
				IisUtility.SetProperty(directoryEntry2, "HCNoCompressionForProxies", flag6, true);
				directoryEntry2.CommitChanges();
				IisUtility.CommitMetabaseChanges(IisUtility.ServerFromWebSite(site));
			}
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000A8348 File Offset: 0x000A6548
		internal static void SetVirtualDirectoryGzipLevel(string adsiVirtualDirectoryPath, GzipLevel gzipLevel)
		{
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(adsiVirtualDirectoryPath))
			{
				bool flag = gzipLevel == GzipLevel.High;
				bool flag2 = gzipLevel == GzipLevel.Low || gzipLevel == GzipLevel.High;
				IisUtility.SetProperty(directoryEntry, "DoDynamicCompression", flag, true);
				IisUtility.SetProperty(directoryEntry, "DoStaticCompression", flag2, true);
				directoryEntry.CommitChanges();
				IisUtility.CommitMetabaseChanges(IisUtility.ServerFromWebSite(IisUtility.WebSiteFromMetabasePath(directoryEntry.Path)));
			}
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000A83CC File Offset: 0x000A65CC
		internal static GzipLevel GetGzipLevel(string adsiVirtualDirectoryPath)
		{
			GzipLevel result;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(adsiVirtualDirectoryPath))
			{
				if (!directoryEntry.Properties.Contains("DoDynamicCompression") || !directoryEntry.Properties.Contains("DoStaticCompression"))
				{
					result = GzipLevel.Off;
				}
				else
				{
					bool flag = (bool)directoryEntry.Properties["DoDynamicCompression"].Value;
					bool flag2 = (bool)directoryEntry.Properties["DoStaticCompression"].Value;
					if (!flag && !flag2)
					{
						result = GzipLevel.Off;
					}
					else if (!flag && flag2)
					{
						result = GzipLevel.Low;
					}
					else if (flag && flag2)
					{
						result = GzipLevel.High;
					}
					else
					{
						result = GzipLevel.Error;
					}
				}
			}
			return result;
		}

		// Token: 0x04001EFE RID: 7934
		private const string w3svcRoot = "/W3SVC";

		// Token: 0x04001EFF RID: 7935
		private const string gzipPath = "/Filters/Compression/gzip";

		// Token: 0x020004AB RID: 1195
		internal struct MetabasePropertyNames
		{
			// Token: 0x04001F00 RID: 7936
			public const string DoDynamicCompression = "DoDynamicCompression";

			// Token: 0x04001F01 RID: 7937
			public const string DoStaticCompression = "DoStaticCompression";

			// Token: 0x04001F02 RID: 7938
			public const string HCDoDynamicCompression = "HCDoDynamicCompression";

			// Token: 0x04001F03 RID: 7939
			public const string HCDoStaticCompression = "HCDoStaticCompression";

			// Token: 0x04001F04 RID: 7940
			public const string HCDoOnDemandCompression = "HCDoOnDemandCompression";

			// Token: 0x04001F05 RID: 7941
			public const string HCFileExtensions = "HCFileExtensions";

			// Token: 0x04001F06 RID: 7942
			public const string HCScriptFileExtensions = "HCScriptFileExtensions";

			// Token: 0x04001F07 RID: 7943
			public const string HCOnDemandCompLevel = "HCOnDemandCompLevel";

			// Token: 0x04001F08 RID: 7944
			public const string HCDynamicCompressionLevel = "HCDynamicCompressionLevel";

			// Token: 0x04001F09 RID: 7945
			public const string HCSendCacheHeaders = "HCSendCacheHeaders";

			// Token: 0x04001F0A RID: 7946
			public const string HCNoCompressionForHTTP10 = "HCNoCompressionForHTTP10";

			// Token: 0x04001F0B RID: 7947
			public const string HCNoCompressionForProxies = "HCNoCompressionForProxies";
		}
	}
}
