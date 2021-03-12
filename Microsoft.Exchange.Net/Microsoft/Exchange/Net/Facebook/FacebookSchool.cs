using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000726 RID: 1830
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class FacebookSchool : IExtensibleDataObject
	{
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x00047843 File Offset: 0x00045A43
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x0004784B File Offset: 0x00045A4B
		[DataMember(Name = "id")]
		public string Id { get; set; }

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x00047854 File Offset: 0x00045A54
		// (set) Token: 0x060022D4 RID: 8916 RVA: 0x0004785C File Offset: 0x00045A5C
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x00047865 File Offset: 0x00045A65
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x0004786D File Offset: 0x00045A6D
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
