using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000457 RID: 1111
	public enum ScopeRestrictionType
	{
		// Token: 0x04002248 RID: 8776
		NotApplicable = 1,
		// Token: 0x04002249 RID: 8777
		DomainScope_Obsolete,
		// Token: 0x0400224A RID: 8778
		RecipientScope,
		// Token: 0x0400224B RID: 8779
		ServerScope,
		// Token: 0x0400224C RID: 8780
		PartnerDelegatedTenantScope,
		// Token: 0x0400224D RID: 8781
		DatabaseScope
	}
}
