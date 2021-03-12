using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001D4 RID: 468
	internal interface IRequestIndexEntry : IConfigurable
	{
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600131F RID: 4895
		// (set) Token: 0x06001320 RID: 4896
		Guid RequestGuid { get; set; }

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001321 RID: 4897
		// (set) Token: 0x06001322 RID: 4898
		string Name { get; set; }

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001323 RID: 4899
		// (set) Token: 0x06001324 RID: 4900
		RequestStatus Status { get; set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001325 RID: 4901
		// (set) Token: 0x06001326 RID: 4902
		RequestFlags Flags { get; set; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001327 RID: 4903
		// (set) Token: 0x06001328 RID: 4904
		string RemoteHostName { get; set; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001329 RID: 4905
		// (set) Token: 0x0600132A RID: 4906
		string BatchName { get; set; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600132B RID: 4907
		// (set) Token: 0x0600132C RID: 4908
		ADObjectId SourceMDB { get; set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600132D RID: 4909
		// (set) Token: 0x0600132E RID: 4910
		ADObjectId TargetMDB { get; set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600132F RID: 4911
		// (set) Token: 0x06001330 RID: 4912
		ADObjectId StorageMDB { get; set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001331 RID: 4913
		// (set) Token: 0x06001332 RID: 4914
		string FilePath { get; set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001333 RID: 4915
		// (set) Token: 0x06001334 RID: 4916
		MRSRequestType Type { get; set; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001335 RID: 4917
		// (set) Token: 0x06001336 RID: 4918
		ADObjectId TargetUserId { get; set; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001337 RID: 4919
		// (set) Token: 0x06001338 RID: 4920
		ADObjectId SourceUserId { get; set; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001339 RID: 4921
		OrganizationId OrganizationId { get; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600133A RID: 4922
		DateTime? WhenChanged { get; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600133B RID: 4923
		DateTime? WhenCreated { get; }

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600133C RID: 4924
		DateTime? WhenChangedUTC { get; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600133D RID: 4925
		DateTime? WhenCreatedUTC { get; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x0600133E RID: 4926
		RequestIndexId RequestIndexId { get; }

		// Token: 0x0600133F RID: 4927
		RequestJobObjectId GetRequestJobId();

		// Token: 0x06001340 RID: 4928
		RequestIndexEntryObjectId GetRequestIndexEntryId(RequestBase owner);
	}
}
