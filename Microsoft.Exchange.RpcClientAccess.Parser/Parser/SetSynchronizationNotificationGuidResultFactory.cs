using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200012F RID: 303
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetSynchronizationNotificationGuidResultFactory : StandardResultFactory
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x00010EAF File Offset: 0x0000F0AF
		internal SetSynchronizationNotificationGuidResultFactory() : base(RopId.SetSynchronizationNotificationGuid)
		{
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00010EBC File Offset: 0x0000F0BC
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SetSynchronizationNotificationGuid, ErrorCode.None);
		}
	}
}
