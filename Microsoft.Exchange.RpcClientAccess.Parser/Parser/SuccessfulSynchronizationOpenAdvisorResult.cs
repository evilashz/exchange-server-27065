using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000370 RID: 880
	internal sealed class SuccessfulSynchronizationOpenAdvisorResult : RopResult
	{
		// Token: 0x0600157F RID: 5503 RVA: 0x00037A34 File Offset: 0x00035C34
		internal SuccessfulSynchronizationOpenAdvisorResult(IServerObject serverObject) : base(RopId.SynchronizationOpenAdvisor, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00037A51 File Offset: 0x00035C51
		internal SuccessfulSynchronizationOpenAdvisorResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00037A5A File Offset: 0x00035C5A
		internal static SuccessfulSynchronizationOpenAdvisorResult Parse(Reader reader)
		{
			return new SuccessfulSynchronizationOpenAdvisorResult(reader);
		}
	}
}
