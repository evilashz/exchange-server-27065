using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000C2 RID: 194
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorTooManyOpenTablesAndCleanupTimedOut : MapiRetryableException
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x00012B9B File Offset: 0x00010D9B
		internal MapiExceptionJetErrorTooManyOpenTablesAndCleanupTimedOut(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorTooManyOpenTablesAndCleanupTimedOut", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00012BAF File Offset: 0x00010DAF
		private MapiExceptionJetErrorTooManyOpenTablesAndCleanupTimedOut(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
