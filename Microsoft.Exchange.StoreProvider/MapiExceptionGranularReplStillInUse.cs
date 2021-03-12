using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200013B RID: 315
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionGranularReplStillInUse : MapiRetryableException
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x000139D4 File Offset: 0x00011BD4
		internal MapiExceptionGranularReplStillInUse(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionGranularReplStillInUse", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000139E8 File Offset: 0x00011BE8
		private MapiExceptionGranularReplStillInUse(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
