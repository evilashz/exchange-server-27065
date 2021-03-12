using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200026B RID: 619
	public sealed class ThemeManager
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x0007E064 File Offset: 0x0007C264
		private static bool CheckFileExtensionsForCDN(string resourceName)
		{
			for (int i = 0; i < ThemeManager.cdnFileExtensions.Length; i++)
			{
				if (resourceName.EndsWith(ThemeManager.cdnFileExtensions[i], StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0007E096 File Offset: 0x0007C296
		internal static void Initialize()
		{
			ThemeManager.themes = null;
			ThemeManager.baseTheme = null;
			ThemeManager.storageIdToIdMap = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase);
			ThemeManager.themesFolderPath = string.Format("{0}/{1}/", Globals.ApplicationVersion, ThemeManager.themesFolderName);
			ThemeManager.LoadThemeFiles();
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0007E0D4 File Offset: 0x0007C2D4
		private static void LoadThemeFiles()
		{
			ExTraceGlobals.ThemesCallTracer.TraceDebug(0L, "LoadThemeFiles");
			string text = Path.Combine(HttpRuntime.AppDomainAppPath, Globals.ApplicationVersion);
			text = Path.Combine(text, ThemeManager.themesFolderName);
			if (!Directory.Exists(text))
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_NoThemesFolder, string.Empty, new object[]
				{
					text
				});
				throw new OwaThemeManagerInitializationException("Themes folder not found ('" + text + "')");
			}
			string[] directories = Directory.GetDirectories(text);
			SortedDictionary<int, SortedList<string, Theme>> sortedDictionary = new SortedDictionary<int, SortedList<string, Theme>>();
			int i = 0;
			while (i < directories.Length)
			{
				string text2 = directories[i];
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text2);
				if (string.IsNullOrEmpty(fileNameWithoutExtension))
				{
					goto IL_151;
				}
				if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.UseThemeStorageFolder.Enabled || !string.Equals(fileNameWithoutExtension, ThemeManager.DataCenterThemeStorageId, StringComparison.OrdinalIgnoreCase))
				{
					if (string.Equals(fileNameWithoutExtension, ThemeManager.ResourcesFolderName, StringComparison.OrdinalIgnoreCase))
					{
						ThemeManager.resourcesFolderPath = string.Format("{0}{1}/", ThemeManager.themesFolderPath, ThemeManager.ResourcesFolderName);
						ExTraceGlobals.ThemesTracer.TraceDebug<string>(0L, "Located Theme resources folder: '{0}'", ThemeManager.resourcesFolderPath);
					}
					else
					{
						if (!string.Equals(fileNameWithoutExtension, ThemeManager.BasicFilesFolderName, StringComparison.OrdinalIgnoreCase))
						{
							goto IL_151;
						}
						ThemeManager.basicFolderPath = string.Format("{0}{1}/", ThemeManager.themesFolderPath, ThemeManager.BasicFilesFolderName);
						ExTraceGlobals.ThemesTracer.TraceDebug<string>(0L, "Located Basic folder: '{0}'", ThemeManager.basicFolderPath);
					}
				}
				IL_232:
				i++;
				continue;
				IL_151:
				ExTraceGlobals.ThemesTracer.TraceDebug<string>(0L, "Inspecting theme folder '{0}'", text2);
				Theme theme = Theme.Create(text2);
				if (theme == null)
				{
					goto IL_232;
				}
				if (!sortedDictionary.ContainsKey(theme.SortOrder))
				{
					sortedDictionary.Add(theme.SortOrder, new SortedList<string, Theme>());
				}
				sortedDictionary[theme.SortOrder].Add(theme.StorageId, theme);
				if (theme.IsBase)
				{
					ThemeManager.baseTheme = theme;
					ThemeManager.baseThemeFolderPath = string.Format("{0}{1}/", ThemeManager.themesFolderPath, ThemeManager.baseTheme.FolderName);
				}
				if (ThemeManager.storageIdToIdMap.ContainsKey(theme.StorageId))
				{
					throw new OwaThemeManagerInitializationException(string.Format("Duplicated theme found (folder name={0})", theme.FolderName));
				}
				ThemeManager.storageIdToIdMap.Add(theme.StorageId, uint.MaxValue);
				ExTraceGlobals.ThemesTracer.TraceDebug<string>(0L, "Succesfully added theme. Name={0}", theme.DisplayName);
				goto IL_232;
			}
			List<Theme> list = new List<Theme>();
			foreach (KeyValuePair<int, SortedList<string, Theme>> keyValuePair in sortedDictionary)
			{
				foreach (KeyValuePair<string, Theme> keyValuePair2 in keyValuePair.Value)
				{
					list.Add(keyValuePair2.Value);
				}
			}
			ThemeManager.themes = list.ToArray();
			for (int j = 0; j < ThemeManager.themes.Length; j++)
			{
				ThemeManager.storageIdToIdMap[ThemeManager.themes[j].StorageId] = (uint)j;
			}
			if (ThemeManager.baseTheme == null)
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_NoBaseTheme, string.Empty, new object[]
				{
					ThemeManager.BaseThemeFolderName
				});
				throw new OwaThemeManagerInitializationException(string.Format("Couldn't find a base theme (folder name={0})", ThemeManager.BaseThemeFolderName));
			}
			if (string.IsNullOrEmpty(ThemeManager.resourcesFolderPath))
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_NoThemeResources, string.Empty, new object[]
				{
					ThemeManager.BaseThemeFolderName
				});
				throw new OwaThemeManagerInitializationException(string.Format("Couldn't find theme resources (folder name={0})", ThemeManager.ResourcesFolderName));
			}
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0007E46C File Offset: 0x0007C66C
		public static Theme GetDefaultTheme(string defaultThemeStorageId)
		{
			Theme result;
			if (string.IsNullOrEmpty(defaultThemeStorageId))
			{
				result = ThemeManager.baseTheme;
			}
			else
			{
				uint idFromStorageId = ThemeManager.GetIdFromStorageId(defaultThemeStorageId);
				if (idFromStorageId == 4294967295U)
				{
					result = ThemeManager.baseTheme;
				}
				else
				{
					result = ThemeManager.themes[(int)((UIntPtr)idFromStorageId)];
				}
			}
			return result;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0007E4A8 File Offset: 0x0007C6A8
		private static bool RenderThemeFilePath(TextWriter writer, uint themeId, int themeFileIndex)
		{
			return ThemeManager.RenderThemeFilePath(writer, themeId, themeFileIndex, false, true);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0007E4B4 File Offset: 0x0007C6B4
		private static bool RenderThemeFilePath(TextWriter writer, uint themeId, int themeFileIndex, bool isBasicExperience, bool useCDN)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (themeId == 4294967295U)
			{
				return false;
			}
			Theme theme = ThemeManager.themes[(int)((UIntPtr)themeId)];
			if (useCDN && !string.IsNullOrEmpty(Globals.ContentDeliveryNetworkEndpoint) && ThemeManager.CheckFileExtensionsForCDN(ThemeFileList.GetNameFromId(themeFileIndex)))
			{
				writer.Write(Globals.ContentDeliveryNetworkEndpoint);
			}
			writer.Write(ThemeManager.themesFolderPath);
			bool flag = ThemeFileList.IsResourceFile(themeFileIndex);
			if (flag)
			{
				writer.Write(ThemeManager.ResourcesFolderName);
			}
			else if (isBasicExperience)
			{
				writer.Write(ThemeManager.BasicFilesFolderName);
			}
			else
			{
				writer.Write(theme.FolderName);
			}
			writer.Write("/");
			return !flag;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0007E554 File Offset: 0x0007C754
		public static void RenderThemePreviewUrl(TextWriter writer, string themeStorageId)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			bool flag = false;
			uint idFromStorageId = ThemeManager.GetIdFromStorageId(themeStorageId);
			if (idFromStorageId != 4294967295U)
			{
				Theme theme = ThemeManager.themes[(int)((UIntPtr)idFromStorageId)];
				flag = theme.IsFileInTheme(23);
				if (flag)
				{
					ThemeManager.RenderThemeFilePath(writer, ThemeManager.GetIdFromStorageId(themeStorageId), 23);
					writer.Write(ThemeFileList.GetNameFromId(23));
				}
			}
			if (!flag)
			{
				ThemeManager.RenderThemeFilePath(writer, ThemeManager.baseTheme.Id, 22);
				writer.Write(ThemeFileList.GetNameFromId(22));
			}
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0007E5D0 File Offset: 0x0007C7D0
		public static void RenderCssFontThemeFileUrl(TextWriter writer, bool isBasicExperience)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (!string.IsNullOrEmpty(Globals.ContentDeliveryNetworkEndpoint))
			{
				writer.Write(Globals.ContentDeliveryNetworkEndpoint);
			}
			if (isBasicExperience)
			{
				ThemeManager.RenderBasicPath(writer);
			}
			else
			{
				ThemeManager.RenderResourcesPath(writer);
			}
			string cssFontFileNameFromCulture = Culture.GetCssFontFileNameFromCulture(isBasicExperience);
			writer.Write(cssFontFileNameFromCulture);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0007E621 File Offset: 0x0007C821
		public static void RenderThemeFileUrl(TextWriter writer, uint themeId, int themeFileIndex)
		{
			ThemeManager.RenderThemeFileUrl(writer, themeId, themeFileIndex, false);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0007E62C File Offset: 0x0007C82C
		public static void RenderThemeFileUrl(TextWriter writer, uint themeId, int themeFileIndex, bool isBasicExperience)
		{
			ThemeManager.RenderThemeFileUrl(writer, themeId, themeFileIndex, isBasicExperience, true);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0007E638 File Offset: 0x0007C838
		public static void RenderThemeFileUrl(TextWriter writer, uint themeId, ThemeFileId themeFileId)
		{
			ThemeManager.RenderThemeFileUrl(writer, themeId, themeFileId, false);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0007E643 File Offset: 0x0007C843
		public static void RenderThemeFileUrl(TextWriter writer, uint themeId, ThemeFileId themeFileId, bool isBasicExperience)
		{
			ThemeManager.RenderThemeFileUrl(writer, themeId, (int)themeFileId, isBasicExperience, true);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0007E64F File Offset: 0x0007C84F
		public static void RenderThemeFileUrl(TextWriter writer, uint themeId, int themeFileIndex, bool isBasicExperience, bool useCDN)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			ThemeManager.RenderThemeFilePath(writer, themeId, themeFileIndex, isBasicExperience, useCDN);
			writer.Write(ThemeFileList.GetNameFromId(themeFileIndex));
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0007E677 File Offset: 0x0007C877
		public static string GetThemeFileUrl(uint themeId, ThemeFileId themeFileId)
		{
			return ThemeManager.GetThemeFileUrl(themeId, themeFileId, false);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0007E684 File Offset: 0x0007C884
		public static string GetThemeFileUrl(uint themeId, ThemeFileId themeFileId, bool isBasicExperience)
		{
			StringBuilder stringBuilder = new StringBuilder(40);
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				ThemeManager.RenderThemeFileUrl(stringWriter, themeId, (int)themeFileId, isBasicExperience);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0007E6CC File Offset: 0x0007C8CC
		public static void RenderBaseThemeFileUrl(TextWriter writer, ThemeFileId themeFileId)
		{
			ThemeManager.RenderBaseThemeFileUrl(writer, themeFileId, true);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0007E6D6 File Offset: 0x0007C8D6
		public static void RenderBaseThemeFileUrl(TextWriter writer, ThemeFileId themeFileId, bool useCDN)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (ThemeManager.baseTheme != null)
			{
				ThemeManager.RenderThemeFileUrl(writer, ThemeManager.baseTheme.Id, (int)themeFileId, false, useCDN);
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0007E700 File Offset: 0x0007C900
		public static string GetBaseThemeFileUrl(ThemeFileId themeFileId)
		{
			StringBuilder stringBuilder = new StringBuilder(40);
			if (ThemeManager.baseTheme != null)
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					ThemeManager.RenderThemeFileUrl(stringWriter, ThemeManager.baseTheme.Id, themeFileId);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0007E758 File Offset: 0x0007C958
		public static Theme[] Themes
		{
			get
			{
				return ThemeManager.themes;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x0007E75F File Offset: 0x0007C95F
		public static Theme BaseTheme
		{
			get
			{
				return ThemeManager.baseTheme;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0007E766 File Offset: 0x0007C966
		internal static string ThemesFolderPath
		{
			get
			{
				return ThemeManager.themesFolderPath;
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0007E770 File Offset: 0x0007C970
		internal static uint GetIdFromStorageId(string storageId)
		{
			if (storageId == null)
			{
				throw new ArgumentNullException("storageId");
			}
			uint maxValue = uint.MaxValue;
			if (ThemeManager.storageIdToIdMap.ContainsKey(storageId))
			{
				ThemeManager.storageIdToIdMap.TryGetValue(storageId, out maxValue);
			}
			return maxValue;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0007E7A9 File Offset: 0x0007C9A9
		internal static string GetStorageIdFromId(uint id)
		{
			return ThemeManager.themes[(int)((UIntPtr)id)].StorageId;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0007E7B8 File Offset: 0x0007C9B8
		public static bool IsThemeLoaded(string storageId)
		{
			return ThemeManager.storageIdToIdMap.ContainsKey(storageId);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0007E7C5 File Offset: 0x0007C9C5
		internal static void RenderBaseThemePath(TextWriter writer)
		{
			writer.Write(ThemeManager.baseThemeFolderPath);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0007E7D2 File Offset: 0x0007C9D2
		internal static void RenderBasicPath(TextWriter writer)
		{
			writer.Write(ThemeManager.basicFolderPath);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0007E7DF File Offset: 0x0007C9DF
		internal static void RenderResourcesPath(TextWriter writer)
		{
			writer.Write(ThemeManager.resourcesFolderPath);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0007E7EC File Offset: 0x0007C9EC
		internal static Strings.IDs GetLocalizedThemeName(string displayName)
		{
			if (string.CompareOrdinal("$$_BASE_$$", displayName) == 0)
			{
				return 1735777226;
			}
			if (string.CompareOrdinal("$$_arctc_$$", displayName) == 0)
			{
				return 407770443;
			}
			if (string.CompareOrdinal("$$_autmn_$$", displayName) == 0)
			{
				return -1997668257;
			}
			if (string.CompareOrdinal("$$_blib_$$", displayName) == 0)
			{
				return 504638635;
			}
			if (string.CompareOrdinal("$$_blue-b_$$", displayName) == 0)
			{
				return 1976813521;
			}
			if (string.CompareOrdinal("$$_blue-o_$$", displayName) == 0)
			{
				return 416276951;
			}
			if (string.CompareOrdinal("$$_bot_$$", displayName) == 0)
			{
				return 1424015370;
			}
			if (string.CompareOrdinal("$$_cats_$$", displayName) == 0)
			{
				return 168260423;
			}
			if (string.CompareOrdinal("$$_cpck_$$", displayName) == 0)
			{
				return 1832516831;
			}
			if (string.CompareOrdinal("$$_dmsk_$$", displayName) == 0)
			{
				return -2144114660;
			}
			if (string.CompareOrdinal("$$_goth_$$", displayName) == 0)
			{
				return -1878892268;
			}
			if (string.CompareOrdinal("$$_grey-b_$$", displayName) == 0)
			{
				return -39256516;
			}
			if (string.CompareOrdinal("$$_grey-o_$$", displayName) == 0)
			{
				return -1667267878;
			}
			if (string.CompareOrdinal("$$_grey-plm_$$", displayName) == 0)
			{
				return -1773095810;
			}
			if (string.CompareOrdinal("$$_grn-g_$$", displayName) == 0)
			{
				return 1961948849;
			}
			if (string.CompareOrdinal("$$_grn-o_$$", displayName) == 0)
			{
				return 1718259320;
			}
			if (string.CompareOrdinal("$$_mix_$$", displayName) == 0)
			{
				return -1838478750;
			}
			if (string.CompareOrdinal("$$_paint_$$", displayName) == 0)
			{
				return -1736106959;
			}
			if (string.CompareOrdinal("$$_pnk-b_$$", displayName) == 0)
			{
				return -614937081;
			}
			if (string.CompareOrdinal("$$_pnk-g_$$", displayName) == 0)
			{
				return -403037122;
			}
			if (string.CompareOrdinal("$$_pnk-plm_$$", displayName) == 0)
			{
				return 1683552217;
			}
			if (string.CompareOrdinal("$$_pnk-pnk_$$", displayName) == 0)
			{
				return -1946008623;
			}
			if (string.CompareOrdinal("$$_space_$$", displayName) == 0)
			{
				return 1491076554;
			}
			if (string.CompareOrdinal("$$_super_$$", displayName) == 0)
			{
				return -2091543582;
			}
			if (string.CompareOrdinal("$$_violet_$$", displayName) == 0)
			{
				return -1146410324;
			}
			if (string.CompareOrdinal("$$_wntrlnd_$$", displayName) == 0)
			{
				return -1748408431;
			}
			if (string.CompareOrdinal("$$_wrld_$$", displayName) == 0)
			{
				return -1050805589;
			}
			return -1018465893;
		}

		// Token: 0x04001093 RID: 4243
		public static readonly string BaseThemeFolderName = "base";

		// Token: 0x04001094 RID: 4244
		public static readonly string BasicFilesFolderName = "basic";

		// Token: 0x04001095 RID: 4245
		public static readonly string ResourcesFolderName = "resources";

		// Token: 0x04001096 RID: 4246
		public static readonly string DataCenterThemeStorageId = "datacenter";

		// Token: 0x04001097 RID: 4247
		private static readonly string themesFolderName = "themes";

		// Token: 0x04001098 RID: 4248
		private static readonly string[] cdnFileExtensions = new string[]
		{
			".css",
			".png",
			".gif",
			".wav",
			".mp3",
			".ico"
		};

		// Token: 0x04001099 RID: 4249
		private static string themesFolderPath;

		// Token: 0x0400109A RID: 4250
		private static string baseThemeFolderPath;

		// Token: 0x0400109B RID: 4251
		private static string basicFolderPath;

		// Token: 0x0400109C RID: 4252
		private static string resourcesFolderPath;

		// Token: 0x0400109D RID: 4253
		private static Theme baseTheme;

		// Token: 0x0400109E RID: 4254
		private static Theme[] themes;

		// Token: 0x0400109F RID: 4255
		private static Dictionary<string, uint> storageIdToIdMap;
	}
}
