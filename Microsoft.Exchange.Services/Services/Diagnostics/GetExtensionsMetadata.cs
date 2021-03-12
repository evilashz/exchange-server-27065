using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000040 RID: 64
	public enum GetExtensionsMetadata
	{
		// Token: 0x040002E2 RID: 738
		[DisplayName("EXT", "GE")]
		GetExtensionsTime,
		// Token: 0x040002E3 RID: 739
		[DisplayName("EXT", "GM")]
		GetMasterTableTime,
		// Token: 0x040002E4 RID: 740
		[DisplayName("EXT", "GP")]
		GetProvidedExtensionsTime,
		// Token: 0x040002E5 RID: 741
		[DisplayName("EXT", "AM")]
		AddMasterTableTime,
		// Token: 0x040002E6 RID: 742
		[DisplayName("EXT", "CU")]
		CheckUpdatesTime,
		// Token: 0x040002E7 RID: 743
		[DisplayName("EXT", "SU")]
		SaveMasterTableTime,
		// Token: 0x040002E8 RID: 744
		[DisplayName("EXT", "OrgHost")]
		OrgMailboxEwsUrlHost,
		// Token: 0x040002E9 RID: 745
		[DisplayName("EXT", "EWSReqId")]
		OrgMailboxEwsRequestId,
		// Token: 0x040002EA RID: 746
		[DisplayName("EXT", "GO")]
		GetOrgExtensionsTime,
		// Token: 0x040002EB RID: 747
		[DisplayName("EXT", "GET")]
		GetExtensionsTotalTime,
		// Token: 0x040002EC RID: 748
		[DisplayName("EXT", "CES")]
		CreateExchangeServiceTime,
		// Token: 0x040002ED RID: 749
		[DisplayName("EXT", "GCE")]
		GetClientExtensionTime,
		// Token: 0x040002EE RID: 750
		[DisplayName("EXT", "OAD")]
		OrgMailboxAdUserLookupTime,
		// Token: 0x040002EF RID: 751
		[DisplayName("EXT", "WSUrl")]
		WebServiceUrlLookupTime,
		// Token: 0x040002F0 RID: 752
		[DisplayName("EXT", "CET")]
		CreateExtensionsTime,
		// Token: 0x040002F1 RID: 753
		[DisplayName("EXT", "GMUT")]
		GetMarketplaceUrlTime
	}
}
