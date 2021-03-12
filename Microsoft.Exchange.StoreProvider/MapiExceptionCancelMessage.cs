using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200011C RID: 284
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCancelMessage : MapiPermanentException
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x00013638 File Offset: 0x00011838
		internal MapiExceptionCancelMessage(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCancelMessage", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001364C File Offset: 0x0001184C
		private MapiExceptionCancelMessage(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
