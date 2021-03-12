using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000113 RID: 275
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionDataLoss : MapiPermanentException
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x0001352A File Offset: 0x0001172A
		internal MapiExceptionDataLoss(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionDataLoss", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001353E File Offset: 0x0001173E
		private MapiExceptionDataLoss(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
