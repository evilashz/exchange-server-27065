using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000098 RID: 152
	[MessageContract]
	public class AutodiscoverResponseMessage
	{
		// Token: 0x060003DC RID: 988 RVA: 0x000179AA File Offset: 0x00015BAA
		public AutodiscoverResponseMessage()
		{
			this.ServerVersionInfo = ServerVersionInfo.CurrentVersion.Member;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003DD RID: 989 RVA: 0x000179C2 File Offset: 0x00015BC2
		// (set) Token: 0x060003DE RID: 990 RVA: 0x000179CA File Offset: 0x00015BCA
		[MessageHeader]
		public ServerVersionInfo ServerVersionInfo { get; set; }
	}
}
