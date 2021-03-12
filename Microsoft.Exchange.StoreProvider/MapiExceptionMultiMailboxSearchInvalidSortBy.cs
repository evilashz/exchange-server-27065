using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001A8 RID: 424
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxSearchInvalidSortBy : MapiPermanentException
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x0001466F File Offset: 0x0001286F
		internal MapiExceptionMultiMailboxSearchInvalidSortBy(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxSearchInvalidSortBy", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00014683 File Offset: 0x00012883
		private MapiExceptionMultiMailboxSearchInvalidSortBy(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
