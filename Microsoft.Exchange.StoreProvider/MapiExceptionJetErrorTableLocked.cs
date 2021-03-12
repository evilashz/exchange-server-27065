using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000C4 RID: 196
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorTableLocked : MapiRetryableException
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x00012BD7 File Offset: 0x00010DD7
		internal MapiExceptionJetErrorTableLocked(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorTableLocked", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00012BEB File Offset: 0x00010DEB
		private MapiExceptionJetErrorTableLocked(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
