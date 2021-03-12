using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200017D RID: 381
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSyncNoParent : MapiPermanentException
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x00014190 File Offset: 0x00012390
		internal MapiExceptionSyncNoParent(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSyncNoParent", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000141A4 File Offset: 0x000123A4
		private MapiExceptionSyncNoParent(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
