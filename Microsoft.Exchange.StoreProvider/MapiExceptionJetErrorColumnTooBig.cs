using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200016D RID: 365
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorColumnTooBig : MapiPermanentException
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x00013FB0 File Offset: 0x000121B0
		internal MapiExceptionJetErrorColumnTooBig(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorColumnTooBig", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00013FC4 File Offset: 0x000121C4
		private MapiExceptionJetErrorColumnTooBig(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
