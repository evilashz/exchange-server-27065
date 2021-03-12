using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200011A RID: 282
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNamedPropsQuotaExceeded : MapiPermanentException
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x000135FC File Offset: 0x000117FC
		internal MapiExceptionNamedPropsQuotaExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNamedPropsQuotaExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00013610 File Offset: 0x00011810
		private MapiExceptionNamedPropsQuotaExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
