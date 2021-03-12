using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000106 RID: 262
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidBookmark : MapiPermanentException
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x000133A4 File Offset: 0x000115A4
		internal MapiExceptionInvalidBookmark(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidBookmark", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000133B8 File Offset: 0x000115B8
		private MapiExceptionInvalidBookmark(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
