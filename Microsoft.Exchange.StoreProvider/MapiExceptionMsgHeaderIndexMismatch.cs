using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000151 RID: 337
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMsgHeaderIndexMismatch : MapiPermanentException
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x00013C68 File Offset: 0x00011E68
		internal MapiExceptionMsgHeaderIndexMismatch(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMsgHeaderIndexMismatch", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00013C7C File Offset: 0x00011E7C
		private MapiExceptionMsgHeaderIndexMismatch(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
