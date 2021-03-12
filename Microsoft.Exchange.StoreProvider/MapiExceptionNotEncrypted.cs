using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000147 RID: 327
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotEncrypted : MapiPermanentException
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00013B3C File Offset: 0x00011D3C
		internal MapiExceptionNotEncrypted(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNotEncrypted", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00013B50 File Offset: 0x00011D50
		private MapiExceptionNotEncrypted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
