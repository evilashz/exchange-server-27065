using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005E7 RID: 1511
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ItemInformationType
	{
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x000B232A File Offset: 0x000B052A
		// (set) Token: 0x06002D7E RID: 11646 RVA: 0x000B2332 File Offset: 0x000B0532
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlIgnore]
		public string Start { get; set; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002D7F RID: 11647 RVA: 0x000B233B File Offset: 0x000B053B
		// (set) Token: 0x06002D80 RID: 11648 RVA: 0x000B2343 File Offset: 0x000B0543
		[DataMember(EmitDefaultValue = false, Order = 2)]
		[DateTimeString]
		[XmlIgnore]
		public string End { get; set; }

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000B234C File Offset: 0x000B054C
		// (set) Token: 0x06002D82 RID: 11650 RVA: 0x000B2354 File Offset: 0x000B0554
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public EnhancedLocationType Location { get; set; }

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x000B235D File Offset: 0x000B055D
		// (set) Token: 0x06002D84 RID: 11652 RVA: 0x000B2365 File Offset: 0x000B0565
		[DataMember(EmitDefaultValue = false, Order = 4)]
		[XmlIgnore]
		public SingleRecipientType Organizer { get; set; }

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x000B236E File Offset: 0x000B056E
		// (set) Token: 0x06002D86 RID: 11654 RVA: 0x000B2376 File Offset: 0x000B0576
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public bool IsResponseRequested { get; set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x000B237F File Offset: 0x000B057F
		// (set) Token: 0x06002D88 RID: 11656 RVA: 0x000B2387 File Offset: 0x000B0587
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string Subject { get; set; }

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x000B2390 File Offset: 0x000B0590
		// (set) Token: 0x06002D8A RID: 11658 RVA: 0x000B2398 File Offset: 0x000B0598
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public ItemId ConversationId { get; set; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000B23A1 File Offset: 0x000B05A1
		// (set) Token: 0x06002D8C RID: 11660 RVA: 0x000B23A9 File Offset: 0x000B05A9
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public RecurrenceType Recurrence { get; set; }
	}
}
