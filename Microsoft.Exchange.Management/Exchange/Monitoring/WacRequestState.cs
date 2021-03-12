using System;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005F4 RID: 1524
	public class WacRequestState
	{
		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06003641 RID: 13889 RVA: 0x000DFFFB File Offset: 0x000DE1FB
		// (set) Token: 0x06003642 RID: 13890 RVA: 0x000E0003 File Offset: 0x000DE203
		public HttpWebRequest Request { get; set; }

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06003643 RID: 13891 RVA: 0x000E000C File Offset: 0x000DE20C
		// (set) Token: 0x06003644 RID: 13892 RVA: 0x000E0014 File Offset: 0x000DE214
		public HttpWebResponse Response { get; set; }

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06003645 RID: 13893 RVA: 0x000E001D File Offset: 0x000DE21D
		// (set) Token: 0x06003646 RID: 13894 RVA: 0x000E0025 File Offset: 0x000DE225
		public string WacIFrameUrl { get; set; }

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06003647 RID: 13895 RVA: 0x000E002E File Offset: 0x000DE22E
		// (set) Token: 0x06003648 RID: 13896 RVA: 0x000E0036 File Offset: 0x000DE236
		public string WopiUrl { get; set; }

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x000E003F File Offset: 0x000DE23F
		// (set) Token: 0x0600364A RID: 13898 RVA: 0x000E0047 File Offset: 0x000DE247
		public bool Error { get; set; }

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000E0050 File Offset: 0x000DE250
		// (set) Token: 0x0600364C RID: 13900 RVA: 0x000E0058 File Offset: 0x000DE258
		public StringBuilder DiagnosticsDetails { get; set; }
	}
}
