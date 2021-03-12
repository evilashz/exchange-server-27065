using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000135 RID: 309
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionVirusDetected : MapiPermanentException
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x00013920 File Offset: 0x00011B20
		internal MapiExceptionVirusDetected(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionVirusDetected", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00013934 File Offset: 0x00011B34
		private MapiExceptionVirusDetected(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
