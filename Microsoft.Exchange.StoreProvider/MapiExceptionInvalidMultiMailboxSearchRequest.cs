using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001A5 RID: 421
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidMultiMailboxSearchRequest : MapiPermanentException
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x00014615 File Offset: 0x00012815
		internal MapiExceptionInvalidMultiMailboxSearchRequest(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidMultiMailboxSearchRequest", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00014629 File Offset: 0x00012829
		private MapiExceptionInvalidMultiMailboxSearchRequest(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
