using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004D0 RID: 1232
	[Serializable]
	internal sealed class IisVersionValidCondition : Condition
	{
		// Token: 0x06002AC0 RID: 10944 RVA: 0x000AB558 File Offset: 0x000A9758
		public IisVersionValidCondition(string serverName)
		{
			this.ServerName = serverName;
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x000AB567 File Offset: 0x000A9767
		// (set) Token: 0x06002AC2 RID: 10946 RVA: 0x000AB56F File Offset: 0x000A976F
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

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000AB578 File Offset: 0x000A9778
		public override bool Verify()
		{
			TaskLogger.LogEnter();
			bool result = IisUtility.IsSupportedIisVersion(this.ServerName);
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x04001FE7 RID: 8167
		private string serverName;
	}
}
