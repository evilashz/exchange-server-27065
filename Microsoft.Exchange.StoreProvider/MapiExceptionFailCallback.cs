using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200015A RID: 346
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionFailCallback : MapiPermanentException
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x00013D76 File Offset: 0x00011F76
		internal MapiExceptionFailCallback(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionFailCallback", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00013D8A File Offset: 0x00011F8A
		private MapiExceptionFailCallback(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
