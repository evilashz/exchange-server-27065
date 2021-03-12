using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001A6 RID: 422
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidMultiMailboxKeywordStatsRequest : MapiPermanentException
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x00014633 File Offset: 0x00012833
		internal MapiExceptionInvalidMultiMailboxKeywordStatsRequest(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidMultiMailboxKeywordStatsRequest", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00014647 File Offset: 0x00012847
		private MapiExceptionInvalidMultiMailboxKeywordStatsRequest(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
