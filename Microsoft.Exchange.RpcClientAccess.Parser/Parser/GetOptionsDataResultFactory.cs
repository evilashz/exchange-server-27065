using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000E2 RID: 226
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetOptionsDataResultFactory : StandardResultFactory
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x0000F213 File Offset: 0x0000D413
		internal GetOptionsDataResultFactory() : base(RopId.GetOptionsData)
		{
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000F21D File Offset: 0x0000D41D
		public RopResult CreateSuccessfulResult(byte[] optionsInfo)
		{
			return new SuccessfulGetOptionsDataResult(optionsInfo, Array<byte>.Empty, null);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000F22B File Offset: 0x0000D42B
		public RopResult CreateSuccessfulResult(byte[] optionsInfo, byte[] helpFileData, string helpFileName)
		{
			return new SuccessfulGetOptionsDataResult(optionsInfo, helpFileData, helpFileName);
		}
	}
}
