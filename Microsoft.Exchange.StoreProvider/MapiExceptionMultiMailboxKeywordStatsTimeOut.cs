using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000DF RID: 223
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxKeywordStatsTimeOut : MapiRetryableException
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00012F12 File Offset: 0x00011112
		internal MapiExceptionMultiMailboxKeywordStatsTimeOut(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxKeywordStatsTimeOut", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00012F26 File Offset: 0x00011126
		private MapiExceptionMultiMailboxKeywordStatsTimeOut(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
