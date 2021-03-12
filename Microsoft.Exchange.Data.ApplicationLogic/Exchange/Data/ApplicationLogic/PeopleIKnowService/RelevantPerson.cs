using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService
{
	// Token: 0x0200018C RID: 396
	[DataContract]
	public sealed class RelevantPerson : IExtensibleDataObject
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0003D65D File Offset: 0x0003B85D
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0003D665 File Offset: 0x0003B865
		[DataMember(Name = "E")]
		public string EmailAddress { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0003D66E File Offset: 0x0003B86E
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0003D676 File Offset: 0x0003B876
		[DataMember(Name = "R")]
		public int RelevanceScore { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0003D67F File Offset: 0x0003B87F
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0003D687 File Offset: 0x0003B887
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
