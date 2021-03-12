using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200017C RID: 380
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSyncConflict : MapiPermanentException
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x00014172 File Offset: 0x00012372
		internal MapiExceptionSyncConflict(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSyncConflict", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00014186 File Offset: 0x00012386
		private MapiExceptionSyncConflict(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
