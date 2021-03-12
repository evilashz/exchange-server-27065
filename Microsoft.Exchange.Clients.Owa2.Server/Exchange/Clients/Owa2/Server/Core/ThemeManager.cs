using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000B7 RID: 183
	public class ThemeManager
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x0001628C File Offset: 0x0001448C
		public ThemeManager(string owaVersion)
		{
			this.owaVersion = owaVersion;
			this.storageIdToIdMap = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase);
			string path = Path.Combine(new string[]
			{
				FolderConfiguration.Instance.RootPath,
				ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(owaVersion),
				"resources",
				"styles",
				"0"
			});
			this.shouldSkipThemeFolder = Directory.Exists(path);
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000162FE File Offset: 0x000144FE
		public Theme[] Themes
		{
			get
			{
				if (this.themes == null)
				{
					this.LoadThemeFiles(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.owaVersion));
				}
				return this.themes;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001631F File Offset: 0x0001451F
		public Theme BaseTheme
		{
			get
			{
				if (this.baseTheme == null)
				{
					this.LoadThemeFiles(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.owaVersion));
				}
				return this.baseTheme;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00016340 File Offset: 0x00014540
		public bool ShouldSkipThemeFolder
		{
			get
			{
				return this.shouldSkipThemeFolder;
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00016348 File Offset: 0x00014548
		internal uint GetIdFromStorageId(string storageId)
		{
			if (storageId == null)
			{
				throw new ArgumentNullException("storageId");
			}
			uint maxValue = uint.MaxValue;
			if (this.storageIdToIdMap.ContainsKey(storageId))
			{
				this.storageIdToIdMap.TryGetValue(storageId, out maxValue);
			}
			return maxValue;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00016384 File Offset: 0x00014584
		internal string GetThemeFolderName(UserAgent agent, HttpContext httpContext)
		{
			Theme theme = this.BaseTheme;
			if (agent != null && httpContext != null && !UserAgentUtilities.IsMonitoringRequest(agent.RawString) && agent.Layout == LayoutType.Mouse && !Globals.IsAnonymousCalendarApp)
			{
				UserContext userContext = UserContextManager.GetUserContext(httpContext);
				if (userContext != null)
				{
					theme = userContext.Theme;
				}
			}
			return theme.FolderName;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000163D4 File Offset: 0x000145D4
		public Theme GetDefaultTheme(string defaultThemeStorageId)
		{
			Theme result;
			if (string.IsNullOrEmpty(defaultThemeStorageId))
			{
				result = this.baseTheme;
			}
			else
			{
				uint idFromStorageId = this.GetIdFromStorageId(defaultThemeStorageId);
				if (idFromStorageId == 4294967295U)
				{
					result = this.baseTheme;
				}
				else
				{
					result = this.themes[(int)((UIntPtr)idFromStorageId)];
				}
			}
			return result;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00016414 File Offset: 0x00014614
		private void LoadThemeFiles(string resourcesDiskRelativeFolderPath)
		{
			ExTraceGlobals.ThemesCallTracer.TraceDebug(0L, "LoadThemeFiles");
			string text = Path.Combine(FolderConfiguration.Instance.RootPath, resourcesDiskRelativeFolderPath, ThemeManager.themesFolderSubPath);
			if (!Directory.Exists(text))
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_NoThemesFolder, string.Empty, new object[]
				{
					text
				});
				throw new OwaThemeManagerInitializationException("Themes folder not found ('" + text + "')");
			}
			string[] directories = Directory.GetDirectories(text);
			List<Theme> list = new List<Theme>();
			foreach (string text2 in directories)
			{
				Path.GetFileNameWithoutExtension(text2);
				ExTraceGlobals.ThemesTracer.TraceDebug<string>(0L, "Inspecting theme folder '{0}'", text2);
				Theme theme = Theme.Create(text2);
				if (theme == null)
				{
					ExTraceGlobals.ThemesTracer.TraceWarning<string>(0L, "Theme folder '{0}' is invalid", text2);
				}
				else
				{
					list.Add(theme);
					if (theme.IsBase)
					{
						this.baseTheme = theme;
					}
					if (this.storageIdToIdMap.ContainsKey(theme.StorageId))
					{
						throw new OwaThemeManagerInitializationException(string.Format("Duplicated theme found (folder name={0})", theme.FolderName));
					}
					this.storageIdToIdMap.Add(theme.StorageId, uint.MaxValue);
					ExTraceGlobals.ThemesTracer.TraceDebug<string>(0L, "Successfully added theme. Name={0}", theme.DisplayName);
				}
			}
			list.Sort();
			this.themes = list.ToArray();
			for (int j = 0; j < this.themes.Length; j++)
			{
				this.storageIdToIdMap[this.themes[j].StorageId] = (uint)j;
			}
			if (this.baseTheme == null)
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_NoBaseTheme, string.Empty, new object[]
				{
					ThemeManager.BaseThemeFolderName
				});
				throw new OwaThemeManagerInitializationException(string.Format("Couldn't find a base theme (folder name={0})", ThemeManager.BaseThemeFolderName));
			}
		}

		// Token: 0x040003F4 RID: 1012
		public static readonly string BaseThemeFolderName = "base";

		// Token: 0x040003F5 RID: 1013
		private static readonly string themesFolderSubPath = "resources\\themes";

		// Token: 0x040003F6 RID: 1014
		private readonly bool shouldSkipThemeFolder;

		// Token: 0x040003F7 RID: 1015
		private readonly string owaVersion;

		// Token: 0x040003F8 RID: 1016
		private Theme baseTheme;

		// Token: 0x040003F9 RID: 1017
		private Theme[] themes;

		// Token: 0x040003FA RID: 1018
		private Dictionary<string, uint> storageIdToIdMap;
	}
}
