using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000132 RID: 306
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMaxSubmissionExceeded : MapiPermanentException
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x000138CC File Offset: 0x00011ACC
		internal MapiExceptionMaxSubmissionExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMaxSubmissionExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000138E0 File Offset: 0x00011AE0
		private MapiExceptionMaxSubmissionExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
