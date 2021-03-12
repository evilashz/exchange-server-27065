using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200013F RID: 319
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnexpectedMailboxState : MapiPermanentException
	{
		// Token: 0x0600055D RID: 1373 RVA: 0x00013A4C File Offset: 0x00011C4C
		internal MapiExceptionUnexpectedMailboxState(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnexpectedMailboxState", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00013A60 File Offset: 0x00011C60
		private MapiExceptionUnexpectedMailboxState(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
