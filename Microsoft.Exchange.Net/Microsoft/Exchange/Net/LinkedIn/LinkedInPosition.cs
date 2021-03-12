using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000754 RID: 1876
	[DataContract]
	public class LinkedInPosition : IExtensibleDataObject
	{
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x0004CC80 File Offset: 0x0004AE80
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x0004CC88 File Offset: 0x0004AE88
		[DataMember(Name = "title")]
		public string Title { get; set; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x0004CC91 File Offset: 0x0004AE91
		// (set) Token: 0x060024BF RID: 9407 RVA: 0x0004CC99 File Offset: 0x0004AE99
		[DataMember(Name = "company")]
		public LinkedInCompany Company { get; set; }

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x0004CCA2 File Offset: 0x0004AEA2
		// (set) Token: 0x060024C1 RID: 9409 RVA: 0x0004CCAA File Offset: 0x0004AEAA
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
