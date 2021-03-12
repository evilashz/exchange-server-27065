using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200015D RID: 349
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnsupportedProp : MapiPermanentException
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x00013DD0 File Offset: 0x00011FD0
		internal MapiExceptionUnsupportedProp(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnsupportedProp", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00013DE4 File Offset: 0x00011FE4
		private MapiExceptionUnsupportedProp(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
