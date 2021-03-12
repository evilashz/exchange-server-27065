using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200019F RID: 415
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionEventsDeleted : MapiPermanentException
	{
		// Token: 0x0600061E RID: 1566 RVA: 0x00014566 File Offset: 0x00012766
		internal MapiExceptionEventsDeleted(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionEventsDeleted", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001457A File Offset: 0x0001277A
		private MapiExceptionEventsDeleted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
