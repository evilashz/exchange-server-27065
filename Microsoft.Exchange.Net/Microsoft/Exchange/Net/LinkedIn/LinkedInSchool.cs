using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000759 RID: 1881
	[DataContract]
	public class LinkedInSchool : IExtensibleDataObject
	{
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x0004D039 File Offset: 0x0004B239
		// (set) Token: 0x060024E6 RID: 9446 RVA: 0x0004D041 File Offset: 0x0004B241
		[DataMember(Name = "schoolName")]
		public string SchoolName { get; set; }

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x0004D04A File Offset: 0x0004B24A
		// (set) Token: 0x060024E8 RID: 9448 RVA: 0x0004D052 File Offset: 0x0004B252
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
