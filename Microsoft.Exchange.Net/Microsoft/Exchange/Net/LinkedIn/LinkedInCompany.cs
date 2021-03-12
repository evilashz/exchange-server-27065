using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000748 RID: 1864
	[DataContract]
	public class LinkedInCompany : IExtensibleDataObject
	{
		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002453 RID: 9299 RVA: 0x0004C262 File Offset: 0x0004A462
		// (set) Token: 0x06002454 RID: 9300 RVA: 0x0004C26A File Offset: 0x0004A46A
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x0004C273 File Offset: 0x0004A473
		// (set) Token: 0x06002456 RID: 9302 RVA: 0x0004C27B File Offset: 0x0004A47B
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
