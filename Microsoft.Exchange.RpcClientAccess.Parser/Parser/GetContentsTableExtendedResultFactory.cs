using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000DB RID: 219
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetContentsTableExtendedResultFactory : StandardResultFactory
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000F149 File Offset: 0x0000D349
		internal GetContentsTableExtendedResultFactory(ServerObjectHandle serverObjectHandle) : base(RopId.GetContentsTableExtended)
		{
			this.ServerObjectHandle = serverObjectHandle;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000F15D File Offset: 0x0000D35D
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000F165 File Offset: 0x0000D365
		public ServerObjectHandle ServerObjectHandle { get; private set; }

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000F16E File Offset: 0x0000D36E
		public RopResult CreateSuccessfulResult(IServerObject table, int rowCount)
		{
			return new SuccessfulGetContentsTableExtendedResult(table, rowCount);
		}
	}
}
