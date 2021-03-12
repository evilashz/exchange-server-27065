using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001A7 RID: 423
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxSearchNonFullTextSearch : MapiPermanentException
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x00014651 File Offset: 0x00012851
		internal MapiExceptionMultiMailboxSearchNonFullTextSearch(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxSearchNonFullTextSearch", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00014665 File Offset: 0x00012865
		private MapiExceptionMultiMailboxSearchNonFullTextSearch(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
