using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000168 RID: 360
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorPageNotInitialized : MapiPermanentException
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x00013F1A File Offset: 0x0001211A
		internal MapiExceptionJetErrorPageNotInitialized(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorPageNotInitialized", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00013F2E File Offset: 0x0001212E
		private MapiExceptionJetErrorPageNotInitialized(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
