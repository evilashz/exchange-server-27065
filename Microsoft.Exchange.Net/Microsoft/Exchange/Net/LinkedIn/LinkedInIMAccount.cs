using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x0200074B RID: 1867
	[DataContract]
	public class LinkedInIMAccount : IExtensibleDataObject
	{
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x0004C4A8 File Offset: 0x0004A6A8
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x0004C4B0 File Offset: 0x0004A6B0
		[DataMember(Name = "im-account-type")]
		public string IMAccountType { get; set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x0004C4B9 File Offset: 0x0004A6B9
		// (set) Token: 0x06002475 RID: 9333 RVA: 0x0004C4C1 File Offset: 0x0004A6C1
		[DataMember(Name = "im-account-name")]
		public string IMAccountName { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x0004C4CA File Offset: 0x0004A6CA
		// (set) Token: 0x06002477 RID: 9335 RVA: 0x0004C4D2 File Offset: 0x0004A6D2
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
