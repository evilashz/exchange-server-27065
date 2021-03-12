using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000995 RID: 2453
	[Flags]
	internal enum TemplateDistribution : uint
	{
		// Token: 0x04002D41 RID: 11585
		NonSilent = 1U,
		// Token: 0x04002D42 RID: 11586
		ObtainAll = 2U,
		// Token: 0x04002D43 RID: 11587
		Cancel = 4U
	}
}
