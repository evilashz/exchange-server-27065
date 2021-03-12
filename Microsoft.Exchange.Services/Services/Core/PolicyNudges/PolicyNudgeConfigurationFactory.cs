using System;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003CC RID: 972
	internal static class PolicyNudgeConfigurationFactory
	{
		// Token: 0x06001B4A RID: 6986 RVA: 0x0009BA5C File Offset: 0x00099C5C
		internal static PolicyNudgeConfiguration Create()
		{
			return new PolicyNudgeConfiguration15();
		}
	}
}
