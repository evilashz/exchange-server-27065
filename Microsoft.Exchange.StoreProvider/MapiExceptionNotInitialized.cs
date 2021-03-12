using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200010B RID: 267
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotInitialized : MapiPermanentException
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x0001343A File Offset: 0x0001163A
		internal MapiExceptionNotInitialized(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNotInitialized", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001344E File Offset: 0x0001164E
		private MapiExceptionNotInitialized(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
