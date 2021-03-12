using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200044F RID: 1103
	public enum ConfigWriteScopeType
	{
		// Token: 0x04002203 RID: 8707
		None,
		// Token: 0x04002204 RID: 8708
		NotApplicable,
		// Token: 0x04002205 RID: 8709
		OrganizationConfig = 10,
		// Token: 0x04002206 RID: 8710
		CustomConfigScope,
		// Token: 0x04002207 RID: 8711
		PartnerDelegatedTenantScope,
		// Token: 0x04002208 RID: 8712
		ExclusiveConfigScope = 14
	}
}
