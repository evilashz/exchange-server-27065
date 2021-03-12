using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200012B RID: 299
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetReceiveFolderResultFactory : StandardResultFactory
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x00010E5F File Offset: 0x0000F05F
		internal SetReceiveFolderResultFactory() : base(RopId.SetReceiveFolder)
		{
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00010E69 File Offset: 0x0000F069
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SetReceiveFolder, ErrorCode.None);
		}
	}
}
