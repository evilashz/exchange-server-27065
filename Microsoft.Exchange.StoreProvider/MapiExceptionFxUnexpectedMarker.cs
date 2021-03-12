using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000162 RID: 354
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionFxUnexpectedMarker : MapiPermanentException
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x00013E66 File Offset: 0x00012066
		internal MapiExceptionFxUnexpectedMarker(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionFxUnexpectedMarker", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00013E7A File Offset: 0x0001207A
		private MapiExceptionFxUnexpectedMarker(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
