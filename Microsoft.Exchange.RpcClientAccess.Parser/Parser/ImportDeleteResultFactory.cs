using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000134 RID: 308
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ImportDeleteResultFactory : StandardResultFactory
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x00010F15 File Offset: 0x0000F115
		internal ImportDeleteResultFactory() : base(RopId.ImportDelete)
		{
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00010F1F File Offset: 0x0000F11F
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.ImportDelete, ErrorCode.None);
		}
	}
}
