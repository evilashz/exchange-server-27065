using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200015C RID: 348
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionDatabaseError : MapiPermanentException
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x00013DB2 File Offset: 0x00011FB2
		internal MapiExceptionDatabaseError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionDatabaseError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00013DC6 File Offset: 0x00011FC6
		private MapiExceptionDatabaseError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
