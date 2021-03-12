using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000116 RID: 278
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidRecipients : MapiPermanentException
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x00013584 File Offset: 0x00011784
		internal MapiExceptionInvalidRecipients(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidRecipients", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013598 File Offset: 0x00011798
		private MapiExceptionInvalidRecipients(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
