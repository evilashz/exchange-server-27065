using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000AD RID: 173
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IResultFactory
	{
		// Token: 0x06000419 RID: 1049
		RopResult CreateStandardFailedResult(ErrorCode errorCode);

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600041A RID: 1050
		long SuccessfulResultMinimalSize { get; }
	}
}
