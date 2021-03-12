using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000123 RID: 291
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionShutoffQuotaExceeded : MapiPermanentException
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0001370A File Offset: 0x0001190A
		internal MapiExceptionShutoffQuotaExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionShutoffQuotaExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001371E File Offset: 0x0001191E
		private MapiExceptionShutoffQuotaExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
