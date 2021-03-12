using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000171 RID: 369
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorFileNotFound : MapiPermanentException
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x00014028 File Offset: 0x00012228
		internal MapiExceptionJetErrorFileNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorFileNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001403C File Offset: 0x0001223C
		private MapiExceptionJetErrorFileNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
