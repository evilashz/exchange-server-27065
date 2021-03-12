using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000DC RID: 220
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetContentsTableResultFactory : StandardResultFactory
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0000F177 File Offset: 0x0000D377
		internal GetContentsTableResultFactory(ServerObjectHandle serverObjectHandle) : base(RopId.GetContentsTable)
		{
			this.ServerObjectHandle = serverObjectHandle;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000F187 File Offset: 0x0000D387
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0000F18F File Offset: 0x0000D38F
		public ServerObjectHandle ServerObjectHandle { get; private set; }

		// Token: 0x060004BD RID: 1213 RVA: 0x0000F198 File Offset: 0x0000D398
		public RopResult CreateSuccessfulResult(IServerObject table, int rowCount)
		{
			return new SuccessfulGetContentsTableResult(table, rowCount);
		}
	}
}
