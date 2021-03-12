using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200017F RID: 383
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSyncClientChangeNewer : MapiPermanentException
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x000141CC File Offset: 0x000123CC
		internal MapiExceptionSyncClientChangeNewer(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSyncClientChangeNewer", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000141E0 File Offset: 0x000123E0
		private MapiExceptionSyncClientChangeNewer(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
