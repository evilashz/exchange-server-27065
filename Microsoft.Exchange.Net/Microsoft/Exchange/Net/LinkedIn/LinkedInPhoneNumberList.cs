using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000752 RID: 1874
	[DataContract]
	public class LinkedInPhoneNumberList : IExtensibleDataObject
	{
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x0004CC2C File Offset: 0x0004AE2C
		// (set) Token: 0x060024B3 RID: 9395 RVA: 0x0004CC34 File Offset: 0x0004AE34
		[DataMember(Name = "values")]
		public List<LinkedInPhoneNumber> Numbers { get; set; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x0004CC3D File Offset: 0x0004AE3D
		// (set) Token: 0x060024B5 RID: 9397 RVA: 0x0004CC45 File Offset: 0x0004AE45
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
