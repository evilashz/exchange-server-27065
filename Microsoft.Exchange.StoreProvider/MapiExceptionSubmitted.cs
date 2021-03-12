using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200010E RID: 270
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSubmitted : MapiPermanentException
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x00013494 File Offset: 0x00011694
		internal MapiExceptionSubmitted(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSubmitted", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000134A8 File Offset: 0x000116A8
		private MapiExceptionSubmitted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
