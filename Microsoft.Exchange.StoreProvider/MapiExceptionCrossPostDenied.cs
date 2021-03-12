using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000164 RID: 356
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCrossPostDenied : MapiPermanentException
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x00013EA2 File Offset: 0x000120A2
		internal MapiExceptionCrossPostDenied(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCrossPostDenied", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00013EB6 File Offset: 0x000120B6
		private MapiExceptionCrossPostDenied(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
