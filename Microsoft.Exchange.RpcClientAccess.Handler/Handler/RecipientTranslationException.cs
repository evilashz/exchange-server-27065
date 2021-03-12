using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RecipientTranslationException : RopExecutionException
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00015628 File Offset: 0x00013828
		internal RecipientTranslationException(string message) : base(message, (ErrorCode)2147942487U)
		{
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00015636 File Offset: 0x00013836
		internal RecipientTranslationException(string message, Exception innerException) : base(message, (ErrorCode)2147942487U, innerException)
		{
		}

		// Token: 0x040000E1 RID: 225
		private const ErrorCode DefaultErrorCode = ErrorCode.InvalidParam;
	}
}
