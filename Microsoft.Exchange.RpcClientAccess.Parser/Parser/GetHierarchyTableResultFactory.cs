using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000DE RID: 222
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetHierarchyTableResultFactory : StandardResultFactory
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0000F1B3 File Offset: 0x0000D3B3
		internal GetHierarchyTableResultFactory(ServerObjectHandle serverObjectHandle) : base(RopId.GetHierarchyTable)
		{
			this.ServerObjectHandle = serverObjectHandle;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000F1C3 File Offset: 0x0000D3C3
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000F1CB File Offset: 0x0000D3CB
		public ServerObjectHandle ServerObjectHandle { get; private set; }

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		public RopResult CreateSuccessfulResult(IServerObject serverObject, int rowCount)
		{
			return new SuccessfulGetHierarchyTableResult(serverObject, rowCount);
		}
	}
}
