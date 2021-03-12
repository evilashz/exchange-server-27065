using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200017E RID: 382
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSyncIncest : MapiPermanentException
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x000141AE File Offset: 0x000123AE
		internal MapiExceptionSyncIncest(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSyncIncest", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000141C2 File Offset: 0x000123C2
		private MapiExceptionSyncIncest(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
