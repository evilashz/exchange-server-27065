using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000170 RID: 368
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorLVCorrupted : MapiPermanentException
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x0001400A File Offset: 0x0001220A
		internal MapiExceptionJetErrorLVCorrupted(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorLVCorrupted", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001401E File Offset: 0x0001221E
		private MapiExceptionJetErrorLVCorrupted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
