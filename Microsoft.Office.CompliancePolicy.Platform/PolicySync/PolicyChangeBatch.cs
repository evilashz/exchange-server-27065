using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000FA RID: 250
	[KnownType(typeof(AssociationConfiguration))]
	[KnownType(typeof(TenantCookie))]
	[DataContract]
	[KnownType(typeof(TenantCookieCollection))]
	[KnownType(typeof(RuleConfiguration))]
	[KnownType(typeof(ScopeConfiguration))]
	[KnownType(typeof(BindingConfiguration))]
	[KnownType(typeof(PolicyConfiguration))]
	public sealed class PolicyChangeBatch
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00014983 File Offset: 0x00012B83
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0001498B File Offset: 0x00012B8B
		[DataMember]
		public IEnumerable<PolicyConfigurationBase> Changes { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00014994 File Offset: 0x00012B94
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0001499C File Offset: 0x00012B9C
		[DataMember]
		public TenantCookieCollection NewCookies { get; set; }
	}
}
