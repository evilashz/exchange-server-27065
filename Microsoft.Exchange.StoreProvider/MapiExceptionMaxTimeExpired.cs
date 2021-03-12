using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200012B RID: 299
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMaxTimeExpired : MapiPermanentException
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x000137FA File Offset: 0x000119FA
		internal MapiExceptionMaxTimeExpired(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMaxTimeExpired", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001380E File Offset: 0x00011A0E
		private MapiExceptionMaxTimeExpired(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
