using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004CE RID: 1230
	[Serializable]
	internal sealed class WebSiteExistsCondition : Condition
	{
		// Token: 0x06002AAC RID: 10924 RVA: 0x000AB396 File Offset: 0x000A9596
		public WebSiteExistsCondition(string webSitePath)
		{
			this.WebSitePath = webSitePath;
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06002AAD RID: 10925 RVA: 0x000AB3A5 File Offset: 0x000A95A5
		// (set) Token: 0x06002AAE RID: 10926 RVA: 0x000AB3AD File Offset: 0x000A95AD
		public string WebSitePath
		{
			get
			{
				return this.webSitePath;
			}
			set
			{
				this.webSitePath = value;
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000AB3B6 File Offset: 0x000A95B6
		public WebSiteExistsCondition(string serverName, string webSiteName)
		{
			this.ServerName = serverName;
			this.WebSiteName = webSiteName;
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x000AB3CC File Offset: 0x000A95CC
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x000AB3D4 File Offset: 0x000A95D4
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

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x000AB3DD File Offset: 0x000A95DD
		// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x000AB3E5 File Offset: 0x000A95E5
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

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000AB3F0 File Offset: 0x000A95F0
		public override bool Verify()
		{
			TaskLogger.LogEnter();
			bool flag = false;
			if (this.webSitePath != null)
			{
				flag = IisUtility.Exists(this.WebSitePath, "IIsWebServer");
			}
			else
			{
				try
				{
					IisUtility.FindWebSiteRoot(this.WebSiteName, this.ServerName);
					flag = true;
				}
				catch (WebObjectNotFoundException)
				{
					flag = false;
				}
			}
			TaskLogger.Trace("WebSiteExistsCondition is returning '{0}'", new object[]
			{
				flag
			});
			TaskLogger.LogExit();
			return flag;
		}

		// Token: 0x04001FE0 RID: 8160
		private string webSitePath;

		// Token: 0x04001FE1 RID: 8161
		private string serverName;

		// Token: 0x04001FE2 RID: 8162
		private string webSiteName;
	}
}
