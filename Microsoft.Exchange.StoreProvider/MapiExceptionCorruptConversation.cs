using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000154 RID: 340
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCorruptConversation : MapiPermanentException
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x00013CC2 File Offset: 0x00011EC2
		internal MapiExceptionCorruptConversation(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCorruptConversation", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00013CD6 File Offset: 0x00011ED6
		private MapiExceptionCorruptConversation(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
