using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000138 RID: 312
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionGranularReplInitFailed : MapiRetryableException
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x0001397A File Offset: 0x00011B7A
		internal MapiExceptionGranularReplInitFailed(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionGranularReplInitFailed", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001398E File Offset: 0x00011B8E
		private MapiExceptionGranularReplInitFailed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
