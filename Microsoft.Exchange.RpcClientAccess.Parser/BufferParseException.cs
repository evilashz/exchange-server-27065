using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020000A1 RID: 161
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class BufferParseException : Exception
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x0000DFD1 File Offset: 0x0000C1D1
		internal BufferParseException(string message) : base(message)
		{
		}
	}
}
