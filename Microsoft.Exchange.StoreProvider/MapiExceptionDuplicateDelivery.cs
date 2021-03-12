using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001A0 RID: 416
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionDuplicateDelivery : MapiPermanentException
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x00014584 File Offset: 0x00012784
		internal MapiExceptionDuplicateDelivery(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionDuplicateDelivery", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00014598 File Offset: 0x00012798
		private MapiExceptionDuplicateDelivery(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
