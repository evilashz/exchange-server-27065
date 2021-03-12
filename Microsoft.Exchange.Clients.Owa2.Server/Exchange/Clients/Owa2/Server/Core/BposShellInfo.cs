using System;
using System.Runtime.Serialization;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000401 RID: 1025
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class BposShellInfo : BposNavBarInfo
	{
		// Token: 0x060021C8 RID: 8648 RVA: 0x0007A69E File Offset: 0x0007889E
		public BposShellInfo(string version, NavBarData data, string allowedProxyOrigins, string proxyScriptUrl) : base(version, data)
		{
			this.AllowedProxyOrigins = allowedProxyOrigins;
			this.ProxyScriptUrl = proxyScriptUrl;
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x0007A6B7 File Offset: 0x000788B7
		// (set) Token: 0x060021CA RID: 8650 RVA: 0x0007A6BF File Offset: 0x000788BF
		[DataMember]
		public string SuiteServiceProxyOriginAllowedList { get; set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x0007A6C8 File Offset: 0x000788C8
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x0007A6D0 File Offset: 0x000788D0
		[DataMember]
		public string SuiteServiceProxyScriptUrl { get; set; }

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x0007A6D9 File Offset: 0x000788D9
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x0007A6E1 File Offset: 0x000788E1
		[DataMember]
		public string AllowedProxyOrigins { get; set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x0007A6EA File Offset: 0x000788EA
		// (set) Token: 0x060021D0 RID: 8656 RVA: 0x0007A6F2 File Offset: 0x000788F2
		[DataMember]
		public string ProxyScriptUrl { get; set; }
	}
}
