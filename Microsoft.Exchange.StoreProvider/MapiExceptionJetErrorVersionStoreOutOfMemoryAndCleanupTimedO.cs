using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000BB RID: 187
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorVersionStoreOutOfMemoryAndCleanupTimedOut : MapiRetryableException
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x00012AC9 File Offset: 0x00010CC9
		internal MapiExceptionJetErrorVersionStoreOutOfMemoryAndCleanupTimedOut(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorVersionStoreOutOfMemoryAndCleanupTimedOut", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00012ADD File Offset: 0x00010CDD
		private MapiExceptionJetErrorVersionStoreOutOfMemoryAndCleanupTimedOut(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
