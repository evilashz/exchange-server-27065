using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001BF RID: 447
	public class ConnectToServerParams
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x0004A374 File Offset: 0x00048574
		public ConnectToServerParams(bool defaultServer, string name)
		{
			this.setAsDefaultServer = defaultServer;
			this.serverName = name;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0004A38A File Offset: 0x0004858A
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x0004A392 File Offset: 0x00048592
		public bool SetAsDefaultServer
		{
			get
			{
				return this.setAsDefaultServer;
			}
			set
			{
				this.setAsDefaultServer = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0004A39B File Offset: 0x0004859B
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x0004A3A3 File Offset: 0x000485A3
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

		// Token: 0x040006FC RID: 1788
		private bool setAsDefaultServer;

		// Token: 0x040006FD RID: 1789
		private string serverName;
	}
}
