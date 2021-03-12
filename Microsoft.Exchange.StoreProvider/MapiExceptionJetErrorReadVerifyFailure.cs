using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000167 RID: 359
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorReadVerifyFailure : MapiPermanentException
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x00013EFC File Offset: 0x000120FC
		internal MapiExceptionJetErrorReadVerifyFailure(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorReadVerifyFailure", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00013F10 File Offset: 0x00012110
		private MapiExceptionJetErrorReadVerifyFailure(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
