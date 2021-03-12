using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000B7 RID: 183
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorLogWriteFail : MapiRetryableException
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x00012A51 File Offset: 0x00010C51
		internal MapiExceptionJetErrorLogWriteFail(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorLogWriteFail", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00012A65 File Offset: 0x00010C65
		private MapiExceptionJetErrorLogWriteFail(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
