using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000191 RID: 401
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionWatermarkError : MapiPermanentException
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x000143B4 File Offset: 0x000125B4
		internal MapiExceptionWatermarkError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionWatermarkError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000143C8 File Offset: 0x000125C8
		private MapiExceptionWatermarkError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
