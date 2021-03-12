using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000193 RID: 403
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorInvalidBufferSize : MapiPermanentException
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x000143F0 File Offset: 0x000125F0
		internal MapiExceptionJetErrorInvalidBufferSize(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorInvalidBufferSize", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00014404 File Offset: 0x00012604
		private MapiExceptionJetErrorInvalidBufferSize(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
