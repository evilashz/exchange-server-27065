using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000003 RID: 3
	internal abstract class AgentGeneratedMessageLoopCheckerConfig
	{
		// Token: 0x06000007 RID: 7
		internal abstract bool GetIsEnabledInSubmission();

		// Token: 0x06000008 RID: 8
		internal abstract bool GetIsEnabledInSmtp();

		// Token: 0x06000009 RID: 9
		internal abstract uint GetMaxAllowedMessageDepth();

		// Token: 0x0600000A RID: 10
		internal abstract uint GetMaxAllowedMessageDepthPerAgent();
	}
}
