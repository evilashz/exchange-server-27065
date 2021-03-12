using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020000A6 RID: 166
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CorruptRecipientException : RopExecutionException
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000E0DD File Offset: 0x0000C2DD
		public CorruptRecipientException(string message, ErrorCode error) : base(message, error)
		{
		}
	}
}
