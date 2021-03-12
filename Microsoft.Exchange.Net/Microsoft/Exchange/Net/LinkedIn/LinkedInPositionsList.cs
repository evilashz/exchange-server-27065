using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000755 RID: 1877
	[DataContract]
	public class LinkedInPositionsList : IExtensibleDataObject
	{
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x0004CCBB File Offset: 0x0004AEBB
		// (set) Token: 0x060024C4 RID: 9412 RVA: 0x0004CCC3 File Offset: 0x0004AEC3
		[DataMember(Name = "values")]
		public List<LinkedInPosition> Positions { get; set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x0004CCCC File Offset: 0x0004AECC
		// (set) Token: 0x060024C6 RID: 9414 RVA: 0x0004CCD4 File Offset: 0x0004AED4
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
