using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000101 RID: 257
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnexpectedType : MapiPermanentException
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x0001330E File Offset: 0x0001150E
		internal MapiExceptionUnexpectedType(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnexpectedType", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00013322 File Offset: 0x00011522
		private MapiExceptionUnexpectedType(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
