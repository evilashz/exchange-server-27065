using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000BC RID: 188
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorVersionStoreOutOfMemory : MapiRetryableException
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x00012AE7 File Offset: 0x00010CE7
		internal MapiExceptionJetErrorVersionStoreOutOfMemory(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorVersionStoreOutOfMemory", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00012AFB File Offset: 0x00010CFB
		private MapiExceptionJetErrorVersionStoreOutOfMemory(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
