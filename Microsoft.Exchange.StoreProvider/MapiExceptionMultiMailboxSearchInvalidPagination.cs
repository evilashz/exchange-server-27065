using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001AA RID: 426
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxSearchInvalidPagination : MapiPermanentException
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x000146AB File Offset: 0x000128AB
		internal MapiExceptionMultiMailboxSearchInvalidPagination(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxSearchInvalidSortBy", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000146BF File Offset: 0x000128BF
		private MapiExceptionMultiMailboxSearchInvalidPagination(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
