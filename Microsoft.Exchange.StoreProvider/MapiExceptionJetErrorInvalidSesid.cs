using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200016E RID: 366
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorInvalidSesid : MapiPermanentException
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x00013FCE File Offset: 0x000121CE
		internal MapiExceptionJetErrorInvalidSesid(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorInvalidSesid", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00013FE2 File Offset: 0x000121E2
		private MapiExceptionJetErrorInvalidSesid(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
