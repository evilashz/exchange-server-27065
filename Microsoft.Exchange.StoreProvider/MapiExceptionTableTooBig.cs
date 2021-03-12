using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000105 RID: 261
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionTableTooBig : MapiPermanentException
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00013386 File Offset: 0x00011586
		internal MapiExceptionTableTooBig(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionTableTooBig", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001339A File Offset: 0x0001159A
		private MapiExceptionTableTooBig(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
