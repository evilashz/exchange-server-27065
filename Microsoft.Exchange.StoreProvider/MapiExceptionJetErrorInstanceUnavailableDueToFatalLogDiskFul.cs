using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000199 RID: 409
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorInstanceUnavailableDueToFatalLogDiskFull : MapiPermanentException
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x000144A4 File Offset: 0x000126A4
		internal MapiExceptionJetErrorInstanceUnavailableDueToFatalLogDiskFull(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorInstanceUnavailableDueToFatalLogDiskFull", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000144B8 File Offset: 0x000126B8
		private MapiExceptionJetErrorInstanceUnavailableDueToFatalLogDiskFull(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
