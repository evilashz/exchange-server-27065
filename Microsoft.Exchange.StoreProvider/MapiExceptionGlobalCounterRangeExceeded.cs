using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000C9 RID: 201
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionGlobalCounterRangeExceeded : MapiRetryableException
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00012C6D File Offset: 0x00010E6D
		internal MapiExceptionGlobalCounterRangeExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionGlobalCounterRangeExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00012C81 File Offset: 0x00010E81
		private MapiExceptionGlobalCounterRangeExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
