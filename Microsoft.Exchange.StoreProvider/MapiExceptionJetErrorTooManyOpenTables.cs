using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000C1 RID: 193
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorTooManyOpenTables : MapiRetryableException
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00012B7D File Offset: 0x00010D7D
		internal MapiExceptionJetErrorTooManyOpenTables(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorTooManyOpenTables", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00012B91 File Offset: 0x00010D91
		private MapiExceptionJetErrorTooManyOpenTables(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
