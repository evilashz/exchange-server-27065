using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000121 RID: 289
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMdbOffline : MapiPermanentException
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x000136CE File Offset: 0x000118CE
		internal MapiExceptionMdbOffline(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMdbOffline", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000136E2 File Offset: 0x000118E2
		private MapiExceptionMdbOffline(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
