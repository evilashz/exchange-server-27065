using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001AC RID: 428
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxSearchMailboxNotFound : MapiPermanentException
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x000146E7 File Offset: 0x000128E7
		internal MapiExceptionMultiMailboxSearchMailboxNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxSearchMailboxNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000146FB File Offset: 0x000128FB
		private MapiExceptionMultiMailboxSearchMailboxNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
