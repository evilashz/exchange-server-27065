using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000111 RID: 273
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ReadPerUserInformationResultFactory : StandardResultFactory
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x00010478 File Offset: 0x0000E678
		internal ReadPerUserInformationResultFactory() : base(RopId.ReadPerUserInformation)
		{
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00010482 File Offset: 0x0000E682
		public RopResult CreateSuccessfulResult(bool hasFinished, byte[] data)
		{
			return new SuccessfulReadPerUserInformationResult(hasFinished, data);
		}
	}
}
