using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000115 RID: 277
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RegisterNotificationResultFactory : StandardResultFactory
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x000106E7 File Offset: 0x0000E8E7
		internal RegisterNotificationResultFactory(ServerObjectHandle serverObjectHandle) : base(RopId.RegisterNotification)
		{
			this.ServerObjectHandle = serverObjectHandle;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000106F8 File Offset: 0x0000E8F8
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x00010700 File Offset: 0x0000E900
		public ServerObjectHandle ServerObjectHandle { get; private set; }

		// Token: 0x06000597 RID: 1431 RVA: 0x00010709 File Offset: 0x0000E909
		public RopResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulRegisterNotificationResult(serverObject);
		}
	}
}
