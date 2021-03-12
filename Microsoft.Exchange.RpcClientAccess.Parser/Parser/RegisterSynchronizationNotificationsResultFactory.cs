using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000116 RID: 278
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RegisterSynchronizationNotificationsResultFactory : StandardResultFactory
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x00010711 File Offset: 0x0000E911
		internal RegisterSynchronizationNotificationsResultFactory() : base(RopId.RegisterSynchronizationNotifications)
		{
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001071E File Offset: 0x0000E91E
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.RegisterSynchronizationNotifications, ErrorCode.None);
		}
	}
}
