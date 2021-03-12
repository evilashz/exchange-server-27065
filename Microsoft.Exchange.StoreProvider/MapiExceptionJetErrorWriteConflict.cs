using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000BF RID: 191
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorWriteConflict : MapiRetryableException
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x00012B41 File Offset: 0x00010D41
		internal MapiExceptionJetErrorWriteConflict(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorWriteConflict", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00012B55 File Offset: 0x00010D55
		private MapiExceptionJetErrorWriteConflict(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
