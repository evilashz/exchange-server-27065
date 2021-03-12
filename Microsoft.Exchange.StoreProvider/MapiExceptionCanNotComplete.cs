using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000B3 RID: 179
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCanNotComplete : MapiRetryableException
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x000129D9 File Offset: 0x00010BD9
		internal MapiExceptionCanNotComplete(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCanNotComplete", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000129ED File Offset: 0x00010BED
		private MapiExceptionCanNotComplete(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
