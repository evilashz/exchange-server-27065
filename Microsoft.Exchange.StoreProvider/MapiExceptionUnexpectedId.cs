using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000103 RID: 259
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnexpectedId : MapiPermanentException
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x0001334A File Offset: 0x0001154A
		internal MapiExceptionUnexpectedId(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnexpectedId", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001335E File Offset: 0x0001155E
		private MapiExceptionUnexpectedId(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
