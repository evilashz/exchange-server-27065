using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004CF RID: 1231
	[Serializable]
	internal sealed class VirtualDirectoryExistsCondition : Condition
	{
		// Token: 0x06002AB5 RID: 10933 RVA: 0x000AB46C File Offset: 0x000A966C
		public VirtualDirectoryExistsCondition(string virtualDirectoryPath)
		{
			this.VirtualDirectoryPath = virtualDirectoryPath;
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x000AB47B File Offset: 0x000A967B
		// (set) Token: 0x06002AB7 RID: 10935 RVA: 0x000AB483 File Offset: 0x000A9683
		public string VirtualDirectoryPath
		{
			get
			{
				return this.virtualDirectoryPath;
			}
			set
			{
				this.virtualDirectoryPath = value;
			}
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000AB48C File Offset: 0x000A968C
		public VirtualDirectoryExistsCondition(string serverName, string webSiteName, string virtualDirectoryName)
		{
			this.ServerName = serverName;
			this.WebSiteName = webSiteName;
			this.VirtualDirectoryName = virtualDirectoryName;
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x000AB4A9 File Offset: 0x000A96A9
		// (set) Token: 0x06002ABA RID: 10938 RVA: 0x000AB4B1 File Offset: 0x000A96B1
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
			set
			{
				this.serverName = value;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x000AB4BA File Offset: 0x000A96BA
		// (set) Token: 0x06002ABC RID: 10940 RVA: 0x000AB4C2 File Offset: 0x000A96C2
		public string WebSiteName
		{
			get
			{
				return this.webSiteName;
			}
			set
			{
				this.webSiteName = value;
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x000AB4CB File Offset: 0x000A96CB
		// (set) Token: 0x06002ABE RID: 10942 RVA: 0x000AB4D3 File Offset: 0x000A96D3
		public string VirtualDirectoryName
		{
			get
			{
				return this.virtualDirectoryName;
			}
			set
			{
				this.virtualDirectoryName = value;
			}
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000AB4DC File Offset: 0x000A96DC
		public override bool Verify()
		{
			TaskLogger.LogEnter();
			bool result = false;
			if (this.virtualDirectoryPath != null)
			{
				result = IisUtility.Exists(this.VirtualDirectoryPath, "IIsWebVirtualDir");
			}
			else
			{
				try
				{
					string str = IisUtility.FindWebSiteRoot(this.WebSiteName, this.ServerName);
					result = IisUtility.Exists(str + "/" + this.VirtualDirectoryName, "IIsWebVirtualDir");
				}
				catch (WebObjectNotFoundException)
				{
					result = false;
				}
			}
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x04001FE3 RID: 8163
		private string virtualDirectoryPath;

		// Token: 0x04001FE4 RID: 8164
		private string serverName;

		// Token: 0x04001FE5 RID: 8165
		private string webSiteName;

		// Token: 0x04001FE6 RID: 8166
		private string virtualDirectoryName;
	}
}
