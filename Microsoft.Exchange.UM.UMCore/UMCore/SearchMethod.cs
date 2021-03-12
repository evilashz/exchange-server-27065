using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	internal enum SearchMethod
	{
		// Token: 0x04000856 RID: 2134
		None,
		// Token: 0x04000857 RID: 2135
		FirstNameLastName,
		// Token: 0x04000858 RID: 2136
		LastNameFirstName,
		// Token: 0x04000859 RID: 2137
		EmailAlias,
		// Token: 0x0400085A RID: 2138
		CompanyName,
		// Token: 0x0400085B RID: 2139
		PromptForAlias
	}
}
