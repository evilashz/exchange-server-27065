using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200004B RID: 75
	internal interface INonOrgHierarchy
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003BE RID: 958
		// (set) Token: 0x060003BF RID: 959
		OrganizationId OrgHierarchyToIgnore { get; set; }
	}
}
