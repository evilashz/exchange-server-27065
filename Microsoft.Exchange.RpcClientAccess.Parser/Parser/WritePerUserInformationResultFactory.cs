using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000149 RID: 329
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WritePerUserInformationResultFactory : StandardResultFactory
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x0001117D File Offset: 0x0000F37D
		internal WritePerUserInformationResultFactory() : base(RopId.WritePerUserInformation)
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00011187 File Offset: 0x0000F387
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.WritePerUserInformation, ErrorCode.None);
		}
	}
}
