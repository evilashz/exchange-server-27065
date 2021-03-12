using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200011B RID: 283
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoMessages : MapiPermanentException
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x0001361A File Offset: 0x0001181A
		internal MapiExceptionNoMessages(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoMessages", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001362E File Offset: 0x0001182E
		private MapiExceptionNoMessages(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
