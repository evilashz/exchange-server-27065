using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000122 RID: 290
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionQuotaExceeded : MapiPermanentException
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x000136EC File Offset: 0x000118EC
		internal MapiExceptionQuotaExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionQuotaExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00013700 File Offset: 0x00011900
		private MapiExceptionQuotaExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
