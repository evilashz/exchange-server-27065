using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000112 RID: 274
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRecursionLimit : MapiPermanentException
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0001350C File Offset: 0x0001170C
		internal MapiExceptionRecursionLimit(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRecursionLimit", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00013520 File Offset: 0x00011720
		private MapiExceptionRecursionLimit(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
