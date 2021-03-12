using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000157 RID: 343
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionVersionStoreBusy : MapiRetryableException
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x00013D1C File Offset: 0x00011F1C
		internal MapiExceptionVersionStoreBusy(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionVersionStoreBusy", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00013D30 File Offset: 0x00011F30
		private MapiExceptionVersionStoreBusy(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
