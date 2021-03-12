using System;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x02000111 RID: 273
	public class AmPamInfo
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x00001E88 File Offset: 0x00001288
		public AmPamInfo(AmPamInfo pamInfo)
		{
			this.m_serverName = pamInfo.m_serverName;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00001E6C File Offset: 0x0000126C
		public AmPamInfo(string pamServerName)
		{
			this.m_serverName = pamServerName;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00001EA8 File Offset: 0x000012A8
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00001EBC File Offset: 0x000012BC
		public string ServerName
		{
			get
			{
				return this.m_serverName;
			}
			set
			{
				this.m_serverName = value;
			}
		}

		// Token: 0x0400094D RID: 2381
		private string m_serverName;
	}
}
