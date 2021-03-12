using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200014F RID: 335
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionEmbeddedMessagePropertyCopyFailed : MapiPermanentException
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x00013C2C File Offset: 0x00011E2C
		internal MapiExceptionEmbeddedMessagePropertyCopyFailed(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionEmbeddedMessagePropertyCopyFailed", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00013C40 File Offset: 0x00011E40
		private MapiExceptionEmbeddedMessagePropertyCopyFailed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
