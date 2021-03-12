using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000149 RID: 329
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotPrivateMDB : MapiPermanentException
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x00013B78 File Offset: 0x00011D78
		internal MapiExceptionNotPrivateMDB(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNotPrivateMDB", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00013B8C File Offset: 0x00011D8C
		private MapiExceptionNotPrivateMDB(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
