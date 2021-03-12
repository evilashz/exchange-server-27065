using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000F9 RID: 249
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidAccessTime : MapiPermanentException
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x0001321E File Offset: 0x0001141E
		internal MapiExceptionInvalidAccessTime(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidAccessTime", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00013232 File Offset: 0x00011432
		private MapiExceptionInvalidAccessTime(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
