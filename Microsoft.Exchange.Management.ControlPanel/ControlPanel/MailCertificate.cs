using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000DD RID: 221
	[DataContract]
	public class MailCertificate
	{
		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0005AABA File Offset: 0x00058CBA
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x0005AAC2 File Offset: 0x00058CC2
		[DataMember]
		public string FriendlyName { get; set; }

		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0005AACB File Offset: 0x00058CCB
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x0005AAD3 File Offset: 0x00058CD3
		[DataMember]
		public string Thumbprint { get; set; }

		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0005AADC File Offset: 0x00058CDC
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x0005AAE4 File Offset: 0x00058CE4
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x0005AAED File Offset: 0x00058CED
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x0005AAF5 File Offset: 0x00058CF5
		[DataMember]
		public string Issuer { get; set; }
	}
}
