using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000158 RID: 344
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSearchEvaluationInProgress : MapiRetryableException
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x00013D3A File Offset: 0x00011F3A
		internal MapiExceptionSearchEvaluationInProgress(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSearchEvaluationInProgress", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00013D4E File Offset: 0x00011F4E
		private MapiExceptionSearchEvaluationInProgress(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
