using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200015B RID: 347
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionPathNotFound : MapiPermanentException
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x00013D94 File Offset: 0x00011F94
		internal MapiExceptionPathNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionPathNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00013DA8 File Offset: 0x00011FA8
		private MapiExceptionPathNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
