using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000616 RID: 1558
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class PropertyExistenceType
	{
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060030D9 RID: 12505 RVA: 0x000B6B61 File Offset: 0x000B4D61
		// (set) Token: 0x060030DA RID: 12506 RVA: 0x000B6B69 File Offset: 0x000B4D69
		[DataMember(EmitDefaultValue = false)]
		public bool ExtractedAddresses { get; set; }

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060030DB RID: 12507 RVA: 0x000B6B72 File Offset: 0x000B4D72
		// (set) Token: 0x060030DC RID: 12508 RVA: 0x000B6B7A File Offset: 0x000B4D7A
		[DataMember(EmitDefaultValue = false)]
		public bool ExtractedContacts { get; set; }

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060030DD RID: 12509 RVA: 0x000B6B83 File Offset: 0x000B4D83
		// (set) Token: 0x060030DE RID: 12510 RVA: 0x000B6B8B File Offset: 0x000B4D8B
		[DataMember(EmitDefaultValue = false)]
		public bool ExtractedEmails { get; set; }

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060030DF RID: 12511 RVA: 0x000B6B94 File Offset: 0x000B4D94
		// (set) Token: 0x060030E0 RID: 12512 RVA: 0x000B6B9C File Offset: 0x000B4D9C
		[DataMember(EmitDefaultValue = false)]
		public bool ExtractedKeywords { get; set; }

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060030E1 RID: 12513 RVA: 0x000B6BA5 File Offset: 0x000B4DA5
		// (set) Token: 0x060030E2 RID: 12514 RVA: 0x000B6BAD File Offset: 0x000B4DAD
		[DataMember(EmitDefaultValue = false)]
		public bool ExtractedMeetings { get; set; }

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x000B6BB6 File Offset: 0x000B4DB6
		// (set) Token: 0x060030E4 RID: 12516 RVA: 0x000B6BBE File Offset: 0x000B4DBE
		[DataMember(EmitDefaultValue = false)]
		public bool ExtractedPhones { get; set; }

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060030E5 RID: 12517 RVA: 0x000B6BC7 File Offset: 0x000B4DC7
		// (set) Token: 0x060030E6 RID: 12518 RVA: 0x000B6BCF File Offset: 0x000B4DCF
		[DataMember(EmitDefaultValue = false)]
		public bool ExtractedTasks { get; set; }

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060030E7 RID: 12519 RVA: 0x000B6BD8 File Offset: 0x000B4DD8
		// (set) Token: 0x060030E8 RID: 12520 RVA: 0x000B6BE0 File Offset: 0x000B4DE0
		[DataMember(EmitDefaultValue = false)]
		public bool ExtractedUrls { get; set; }

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x000B6BE9 File Offset: 0x000B4DE9
		// (set) Token: 0x060030EA RID: 12522 RVA: 0x000B6BF1 File Offset: 0x000B4DF1
		[DataMember(EmitDefaultValue = false)]
		public bool ReplyToNames { get; set; }

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x000B6BFA File Offset: 0x000B4DFA
		// (set) Token: 0x060030EC RID: 12524 RVA: 0x000B6C02 File Offset: 0x000B4E02
		[DataMember(EmitDefaultValue = false)]
		public bool ReplyToBlob { get; set; }
	}
}
