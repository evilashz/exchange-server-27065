using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000129 RID: 297
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSendAsDenied : MapiPermanentException
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x000137BE File Offset: 0x000119BE
		internal MapiExceptionSendAsDenied(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSendAsDenied", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000137D2 File Offset: 0x000119D2
		private MapiExceptionSendAsDenied(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
