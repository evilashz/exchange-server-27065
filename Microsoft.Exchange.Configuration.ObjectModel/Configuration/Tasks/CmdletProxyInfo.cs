using System;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000283 RID: 643
	internal class CmdletProxyInfo
	{
		// Token: 0x06001616 RID: 5654 RVA: 0x00052BB1 File Offset: 0x00050DB1
		public CmdletProxyInfo(string remoteServerFqdn, int remoteServerVersion, bool shouldAsyncProxy, LocalizedString confirmationMessage, CmdletProxyInfo.ChangeCmdletProxyParametersDelegate changeCmdletProxyParameters)
		{
			this.RemoteServerFqdn = remoteServerFqdn;
			this.RemoteServerVersion = remoteServerVersion;
			this.ShouldAsyncProxy = shouldAsyncProxy;
			this.ConfirmationMessage = confirmationMessage;
			this.ChangeCmdletProxyParameters = changeCmdletProxyParameters;
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x00052BDE File Offset: 0x00050DDE
		// (set) Token: 0x06001618 RID: 5656 RVA: 0x00052BE6 File Offset: 0x00050DE6
		public string RemoteServerFqdn { get; private set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x00052BEF File Offset: 0x00050DEF
		// (set) Token: 0x0600161A RID: 5658 RVA: 0x00052BF7 File Offset: 0x00050DF7
		public int RemoteServerVersion { get; private set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x00052C00 File Offset: 0x00050E00
		// (set) Token: 0x0600161C RID: 5660 RVA: 0x00052C08 File Offset: 0x00050E08
		public bool ShouldAsyncProxy { get; private set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x00052C11 File Offset: 0x00050E11
		// (set) Token: 0x0600161E RID: 5662 RVA: 0x00052C19 File Offset: 0x00050E19
		public LocalizedString ConfirmationMessage { get; private set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00052C22 File Offset: 0x00050E22
		// (set) Token: 0x06001620 RID: 5664 RVA: 0x00052C2A File Offset: 0x00050E2A
		public CmdletProxyInfo.ChangeCmdletProxyParametersDelegate ChangeCmdletProxyParameters { get; set; }

		// Token: 0x02000284 RID: 644
		// (Invoke) Token: 0x06001622 RID: 5666
		public delegate void ChangeCmdletProxyParametersDelegate(PropertyBag parameters);
	}
}
