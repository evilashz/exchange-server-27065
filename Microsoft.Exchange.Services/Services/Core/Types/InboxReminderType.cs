using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005E3 RID: 1507
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "InboxReminderType")]
	[Serializable]
	public class InboxReminderType
	{
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x000B226E File Offset: 0x000B046E
		// (set) Token: 0x06002D60 RID: 11616 RVA: 0x000B2276 File Offset: 0x000B0476
		[DataMember(Order = 1)]
		public Guid Id { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x000B227F File Offset: 0x000B047F
		// (set) Token: 0x06002D62 RID: 11618 RVA: 0x000B2287 File Offset: 0x000B0487
		[DataMember(Order = 2)]
		public int ReminderOffset { get; set; }

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x000B2290 File Offset: 0x000B0490
		// (set) Token: 0x06002D64 RID: 11620 RVA: 0x000B2298 File Offset: 0x000B0498
		[DataMember(Order = 3)]
		public string Message { get; set; }

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x000B22A1 File Offset: 0x000B04A1
		// (set) Token: 0x06002D66 RID: 11622 RVA: 0x000B22A9 File Offset: 0x000B04A9
		[DataMember(Order = 4)]
		public bool IsOrganizerReminder { get; set; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x000B22B2 File Offset: 0x000B04B2
		// (set) Token: 0x06002D68 RID: 11624 RVA: 0x000B22BA File Offset: 0x000B04BA
		[DataMember(Order = 5)]
		public EmailReminderChangeType OccurrenceChange { get; set; }
	}
}
