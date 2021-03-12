using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001A9 RID: 425
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxSearchNonFullTextSortBy : MapiPermanentException
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x0001468D File Offset: 0x0001288D
		internal MapiExceptionMultiMailboxSearchNonFullTextSortBy(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxSearchNonFullTextSortBy", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000146A1 File Offset: 0x000128A1
		private MapiExceptionMultiMailboxSearchNonFullTextSortBy(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
