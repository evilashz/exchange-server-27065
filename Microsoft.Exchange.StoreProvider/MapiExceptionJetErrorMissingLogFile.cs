using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000196 RID: 406
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorMissingLogFile : MapiPermanentException
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x0001444A File Offset: 0x0001264A
		internal MapiExceptionJetErrorMissingLogFile(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorMissingLogFile", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001445E File Offset: 0x0001265E
		private MapiExceptionJetErrorMissingLogFile(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
