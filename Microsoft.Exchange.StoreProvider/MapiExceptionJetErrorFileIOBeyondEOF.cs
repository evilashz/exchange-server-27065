using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000C8 RID: 200
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorFileIOBeyondEOF : MapiRetryableException
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x00012C4F File Offset: 0x00010E4F
		internal MapiExceptionJetErrorFileIOBeyondEOF(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorFileIOBeyondEOF", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00012C63 File Offset: 0x00010E63
		private MapiExceptionJetErrorFileIOBeyondEOF(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
