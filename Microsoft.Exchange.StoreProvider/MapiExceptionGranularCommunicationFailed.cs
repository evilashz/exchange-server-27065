using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000139 RID: 313
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionGranularCommunicationFailed : MapiRetryableException
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x00013998 File Offset: 0x00011B98
		internal MapiExceptionGranularCommunicationFailed(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionGranularCommunicationFailed", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000139AC File Offset: 0x00011BAC
		private MapiExceptionGranularCommunicationFailed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
