using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200017B RID: 379
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSyncIgnore : MapiPermanentException
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x00014154 File Offset: 0x00012354
		internal MapiExceptionSyncIgnore(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSyncIgnore", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00014168 File Offset: 0x00012368
		private MapiExceptionSyncIgnore(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
