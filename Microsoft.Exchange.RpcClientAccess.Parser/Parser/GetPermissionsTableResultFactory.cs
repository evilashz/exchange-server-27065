using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000E4 RID: 228
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetPermissionsTableResultFactory : StandardResultFactory
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x0000F247 File Offset: 0x0000D447
		internal GetPermissionsTableResultFactory(ServerObjectHandle serverObjectHandle) : base(RopId.GetPermissionsTable)
		{
			this.ServerObjectHandle = serverObjectHandle;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000F258 File Offset: 0x0000D458
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000F260 File Offset: 0x0000D460
		public ServerObjectHandle ServerObjectHandle { get; private set; }

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000F269 File Offset: 0x0000D469
		public RopResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulGetPermissionsTableResult(serverObject);
		}
	}
}
