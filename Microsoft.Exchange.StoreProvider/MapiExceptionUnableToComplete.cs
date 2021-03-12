using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000104 RID: 260
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnableToComplete : MapiPermanentException
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x00013368 File Offset: 0x00011568
		internal MapiExceptionUnableToComplete(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnableToComplete", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001337C File Offset: 0x0001157C
		private MapiExceptionUnableToComplete(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
