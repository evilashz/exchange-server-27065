using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001AE RID: 430
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionISIntegMdbTaskExceeded : MapiRetryableException
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x00014723 File Offset: 0x00012923
		internal MapiExceptionISIntegMdbTaskExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionISIntegMdbTaskExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00014737 File Offset: 0x00012937
		private MapiExceptionISIntegMdbTaskExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
