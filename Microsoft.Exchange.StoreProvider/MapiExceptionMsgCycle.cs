using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000130 RID: 304
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMsgCycle : MapiPermanentException
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x00013890 File Offset: 0x00011A90
		internal MapiExceptionMsgCycle(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMsgCycle", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000138A4 File Offset: 0x00011AA4
		private MapiExceptionMsgCycle(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
