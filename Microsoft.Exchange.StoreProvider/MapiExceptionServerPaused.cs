using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000B2 RID: 178
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionServerPaused : MapiRetryableException
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x000129BB File Offset: 0x00010BBB
		internal MapiExceptionServerPaused(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionServerPaused", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000129CF File Offset: 0x00010BCF
		private MapiExceptionServerPaused(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
