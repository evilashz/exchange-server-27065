using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000BE RID: 190
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorOutOfSessions : MapiRetryableException
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x00012B23 File Offset: 0x00010D23
		internal MapiExceptionJetErrorOutOfSessions(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorOutOfSessions", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00012B37 File Offset: 0x00010D37
		private MapiExceptionJetErrorOutOfSessions(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
