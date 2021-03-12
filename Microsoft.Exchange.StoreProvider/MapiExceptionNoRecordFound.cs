using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200011F RID: 287
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoRecordFound : MapiPermanentException
	{
		// Token: 0x0600051D RID: 1309 RVA: 0x00013692 File Offset: 0x00011892
		internal MapiExceptionNoRecordFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoRecordFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000136A6 File Offset: 0x000118A6
		private MapiExceptionNoRecordFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
