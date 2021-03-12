using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200016B RID: 363
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorDiskFull : MapiPermanentException
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x00013F74 File Offset: 0x00012174
		internal MapiExceptionJetErrorDiskFull(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorDiskFull", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00013F88 File Offset: 0x00012188
		private MapiExceptionJetErrorDiskFull(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
