using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200013E RID: 318
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCannotPreserveMailboxSignature : MapiPermanentException
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00013A2E File Offset: 0x00011C2E
		internal MapiExceptionCannotPreserveMailboxSignature(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCannotPreserveMailboxSignature", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00013A42 File Offset: 0x00011C42
		private MapiExceptionCannotPreserveMailboxSignature(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
