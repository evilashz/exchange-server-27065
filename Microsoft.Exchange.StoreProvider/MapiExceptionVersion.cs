using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000EC RID: 236
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionVersion : MapiPermanentException
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x00013098 File Offset: 0x00011298
		internal MapiExceptionVersion(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionVersion", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000130AC File Offset: 0x000112AC
		private MapiExceptionVersion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
