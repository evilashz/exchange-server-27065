using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000AA RID: 170
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionTimeout : MapiRetryableException
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x000128CB File Offset: 0x00010ACB
		internal MapiExceptionTimeout(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionTimeout", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000128DF File Offset: 0x00010ADF
		private MapiExceptionTimeout(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
