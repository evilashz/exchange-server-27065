using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200016C RID: 364
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorRecordTooBig : MapiPermanentException
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x00013F92 File Offset: 0x00012192
		internal MapiExceptionJetErrorRecordTooBig(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorRecordTooBig", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00013FA6 File Offset: 0x000121A6
		private MapiExceptionJetErrorRecordTooBig(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
