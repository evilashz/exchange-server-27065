using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200016A RID: 362
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetDatabaseCorruption : MapiPermanentException
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x00013F56 File Offset: 0x00012156
		internal MapiExceptionJetDatabaseCorruption(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetDatabaseCorruption", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00013F6A File Offset: 0x0001216A
		private MapiExceptionJetDatabaseCorruption(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
