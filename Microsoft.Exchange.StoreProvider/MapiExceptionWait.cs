using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000AC RID: 172
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionWait : MapiRetryableException
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x00012907 File Offset: 0x00010B07
		internal MapiExceptionWait(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionWait", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001291B File Offset: 0x00010B1B
		private MapiExceptionWait(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
