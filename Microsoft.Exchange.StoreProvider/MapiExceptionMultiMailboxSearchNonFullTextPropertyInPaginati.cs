using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001AB RID: 427
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxSearchNonFullTextPropertyInPagination : MapiPermanentException
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x000146C9 File Offset: 0x000128C9
		internal MapiExceptionMultiMailboxSearchNonFullTextPropertyInPagination(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxSearchInvalidSortBy", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000146DD File Offset: 0x000128DD
		private MapiExceptionMultiMailboxSearchNonFullTextPropertyInPagination(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
