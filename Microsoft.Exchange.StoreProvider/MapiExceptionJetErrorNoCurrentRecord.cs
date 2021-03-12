using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000C6 RID: 198
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorNoCurrentRecord : MapiRetryableException
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x00012C13 File Offset: 0x00010E13
		internal MapiExceptionJetErrorNoCurrentRecord(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorNoCurrentRecord", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00012C27 File Offset: 0x00010E27
		private MapiExceptionJetErrorNoCurrentRecord(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
