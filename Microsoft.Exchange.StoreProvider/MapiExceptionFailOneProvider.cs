using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000F3 RID: 243
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionFailOneProvider : MapiPermanentException
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0001316A File Offset: 0x0001136A
		internal MapiExceptionFailOneProvider(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionFailOneProvider", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001317E File Offset: 0x0001137E
		private MapiExceptionFailOneProvider(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
