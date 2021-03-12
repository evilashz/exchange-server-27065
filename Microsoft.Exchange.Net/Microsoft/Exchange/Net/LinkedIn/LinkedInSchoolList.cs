using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x0200075A RID: 1882
	[DataContract]
	public class LinkedInSchoolList : IExtensibleDataObject
	{
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x0004D063 File Offset: 0x0004B263
		// (set) Token: 0x060024EB RID: 9451 RVA: 0x0004D06B File Offset: 0x0004B26B
		[DataMember(Name = "values")]
		public List<LinkedInSchool> Schools { get; set; }

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x0004D074 File Offset: 0x0004B274
		// (set) Token: 0x060024ED RID: 9453 RVA: 0x0004D07C File Offset: 0x0004B27C
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
