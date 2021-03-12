using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000FC RID: 252
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnknownEntryId : MapiPermanentException
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x00013278 File Offset: 0x00011478
		internal MapiExceptionUnknownEntryId(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnknownEntryId", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001328C File Offset: 0x0001148C
		private MapiExceptionUnknownEntryId(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
