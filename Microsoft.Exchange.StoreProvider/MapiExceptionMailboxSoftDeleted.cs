using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000140 RID: 320
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMailboxSoftDeleted : MapiPermanentException
	{
		// Token: 0x0600055F RID: 1375 RVA: 0x00013A6A File Offset: 0x00011C6A
		internal MapiExceptionMailboxSoftDeleted(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMailboxSoftDeleted", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00013A7E File Offset: 0x00011C7E
		private MapiExceptionMailboxSoftDeleted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
