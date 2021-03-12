using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200016F RID: 367
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorDefaultValueTooBig : MapiPermanentException
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x00013FEC File Offset: 0x000121EC
		internal MapiExceptionJetErrorDefaultValueTooBig(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorDefaultValueTooBig", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00014000 File Offset: 0x00012200
		private MapiExceptionJetErrorDefaultValueTooBig(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
