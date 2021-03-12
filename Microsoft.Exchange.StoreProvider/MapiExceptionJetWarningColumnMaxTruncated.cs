using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000165 RID: 357
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetWarningColumnMaxTruncated : MapiPermanentException
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x00013EC0 File Offset: 0x000120C0
		internal MapiExceptionJetWarningColumnMaxTruncated(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetWarningColumnMaxTruncated", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00013ED4 File Offset: 0x000120D4
		private MapiExceptionJetWarningColumnMaxTruncated(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
