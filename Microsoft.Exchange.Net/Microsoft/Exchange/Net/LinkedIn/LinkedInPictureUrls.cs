using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000753 RID: 1875
	[DataContract]
	public class LinkedInPictureUrls : IExtensibleDataObject
	{
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x0004CC56 File Offset: 0x0004AE56
		// (set) Token: 0x060024B8 RID: 9400 RVA: 0x0004CC5E File Offset: 0x0004AE5E
		[DataMember(Name = "values")]
		public List<string> Urls { get; set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x0004CC67 File Offset: 0x0004AE67
		// (set) Token: 0x060024BA RID: 9402 RVA: 0x0004CC6F File Offset: 0x0004AE6F
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
