using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000F1 RID: 241
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCorruptData : MapiPermanentException
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x0001312E File Offset: 0x0001132E
		internal MapiExceptionCorruptData(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCorruptData", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00013142 File Offset: 0x00011342
		private MapiExceptionCorruptData(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
