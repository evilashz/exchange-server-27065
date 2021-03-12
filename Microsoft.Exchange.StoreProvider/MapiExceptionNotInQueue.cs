using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000109 RID: 265
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotInQueue : MapiPermanentException
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x000133FE File Offset: 0x000115FE
		internal MapiExceptionNotInQueue(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNotInQueue", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00013412 File Offset: 0x00011612
		private MapiExceptionNotInQueue(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
