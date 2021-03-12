using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000131 RID: 305
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMaxAttachmentExceeded : MapiPermanentException
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x000138AE File Offset: 0x00011AAE
		internal MapiExceptionMaxAttachmentExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMaxAttachmentExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000138C2 File Offset: 0x00011AC2
		private MapiExceptionMaxAttachmentExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
