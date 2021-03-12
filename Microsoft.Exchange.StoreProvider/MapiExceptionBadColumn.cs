using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000EE RID: 238
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionBadColumn : MapiPermanentException
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x000130D4 File Offset: 0x000112D4
		internal MapiExceptionBadColumn(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionBadColumn", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000130E8 File Offset: 0x000112E8
		private MapiExceptionBadColumn(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
