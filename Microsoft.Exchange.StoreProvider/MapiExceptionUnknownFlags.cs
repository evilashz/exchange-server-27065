using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000E8 RID: 232
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnknownFlags : MapiPermanentException
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x00013020 File Offset: 0x00011220
		internal MapiExceptionUnknownFlags(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnknownFlags", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00013034 File Offset: 0x00011234
		private MapiExceptionUnknownFlags(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
