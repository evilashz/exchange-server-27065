using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001B7 RID: 439
	[KnownType(typeof(CombinedScenarioRecoResult))]
	[KnownType(typeof(RowNotificationPayload))]
	[KnownType(typeof(HierarchyNotificationPayload))]
	[KnownType(typeof(JsonFaultResponse))]
	[KnownType(typeof(CreateAttachmentNotificationPayload))]
	[KnownType(typeof(DayTimeDurationRecoResult))]
	[KnownType(typeof(CalendarItemNotificationPayload))]
	[KnownType(typeof(InstantMessagePayload))]
	[KnownType(typeof(NewMailNotificationPayload))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(FilteredViewHierarchyNotificationPayload))]
	[KnownType(typeof(SearchNotificationPayload))]
	[KnownType(typeof(InstantSearchNotificationPayload))]
	[KnownType(typeof(PeopleIKnowRowNotificationPayload))]
	[KnownType(typeof(AttachmentOperationCorrelationIdNotificationPayload))]
	public class SubscriptionData
	{
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0003CA41 File Offset: 0x0003AC41
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x0003CA49 File Offset: 0x0003AC49
		[DataMember]
		public string SubscriptionId { get; set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0003CA52 File Offset: 0x0003AC52
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0003CA5A File Offset: 0x0003AC5A
		[DataMember]
		public SubscriptionParameters Parameters { get; set; }
	}
}
