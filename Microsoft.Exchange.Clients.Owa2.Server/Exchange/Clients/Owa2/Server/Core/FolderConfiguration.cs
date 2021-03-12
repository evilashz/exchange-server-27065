using System;
using System.Web;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000D5 RID: 213
	public class FolderConfiguration
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x0001B0B0 File Offset: 0x000192B0
		private FolderConfiguration()
		{
			this.scriptsPath = new StringAppSettingsEntry("ScriptsPath", "scripts", null).Value;
			this.resourcesPath = new StringAppSettingsEntry("ResourcesPath", "resources", null).Value;
			this.rootPath = null;
			string text = new StringAppSettingsEntry("VDirForResources", null, null).Value;
			if (!string.IsNullOrEmpty(text))
			{
				if (!text.StartsWith("/"))
				{
					text = "/" + text;
				}
				try
				{
					this.rootPath = HttpContext.Current.Server.MapPath(text);
				}
				catch (Exception)
				{
				}
			}
			if (string.IsNullOrEmpty(this.rootPath))
			{
				this.rootPath = HttpRuntime.AppDomainAppPath;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0001B178 File Offset: 0x00019378
		public static FolderConfiguration Instance
		{
			get
			{
				if (FolderConfiguration.instance == null)
				{
					lock (FolderConfiguration.syncRoot)
					{
						if (FolderConfiguration.instance == null)
						{
							FolderConfiguration.instance = new FolderConfiguration();
						}
					}
				}
				return FolderConfiguration.instance;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001B1D8 File Offset: 0x000193D8
		internal string ScriptsPath
		{
			get
			{
				return this.scriptsPath;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001B1E0 File Offset: 0x000193E0
		internal string ResourcesPath
		{
			get
			{
				return this.resourcesPath;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001B1E8 File Offset: 0x000193E8
		internal string RootPath
		{
			get
			{
				return this.rootPath;
			}
		}

		// Token: 0x040004E1 RID: 1249
		private readonly string scriptsPath;

		// Token: 0x040004E2 RID: 1250
		private readonly string resourcesPath;

		// Token: 0x040004E3 RID: 1251
		private readonly string rootPath;

		// Token: 0x040004E4 RID: 1252
		private static volatile FolderConfiguration instance = null;

		// Token: 0x040004E5 RID: 1253
		private static object syncRoot = new object();
	}
}
