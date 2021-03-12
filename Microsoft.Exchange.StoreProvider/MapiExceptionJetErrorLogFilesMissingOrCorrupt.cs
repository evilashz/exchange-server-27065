using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000178 RID: 376
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorLogFilesMissingOrCorrupt : MapiPermanentException
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x000140FA File Offset: 0x000122FA
		internal MapiExceptionJetErrorLogFilesMissingOrCorrupt(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorRequiredLogFilesMissing", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001410E File Offset: 0x0001230E
		private MapiExceptionJetErrorLogFilesMissingOrCorrupt(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
