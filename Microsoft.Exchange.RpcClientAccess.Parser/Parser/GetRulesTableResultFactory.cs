using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000ED RID: 237
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetRulesTableResultFactory : StandardResultFactory
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x0000F3EF File Offset: 0x0000D5EF
		internal GetRulesTableResultFactory(ServerObjectHandle serverObjectHandle) : base(RopId.GetRulesTable)
		{
			this.ServerObjectHandle = serverObjectHandle;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000F400 File Offset: 0x0000D600
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x0000F408 File Offset: 0x0000D608
		public ServerObjectHandle ServerObjectHandle { get; private set; }

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000F411 File Offset: 0x0000D611
		public RopResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulGetRulesTableResult(serverObject);
		}
	}
}
