using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000152 RID: 338
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMsgHeaderViewTableMismatch : MapiPermanentException
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x00013C86 File Offset: 0x00011E86
		internal MapiExceptionMsgHeaderViewTableMismatch(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMsgHeaderViewTableMismatch", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00013C9A File Offset: 0x00011E9A
		private MapiExceptionMsgHeaderViewTableMismatch(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
