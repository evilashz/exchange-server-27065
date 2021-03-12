using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000110 RID: 272
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionHasMessages : MapiPermanentException
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x000134D0 File Offset: 0x000116D0
		internal MapiExceptionHasMessages(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionHasMessages", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000134E4 File Offset: 0x000116E4
		private MapiExceptionHasMessages(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
