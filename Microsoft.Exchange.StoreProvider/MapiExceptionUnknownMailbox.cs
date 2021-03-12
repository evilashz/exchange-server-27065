using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200018F RID: 399
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnknownMailbox : MapiPermanentException
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x00014378 File Offset: 0x00012578
		internal MapiExceptionUnknownMailbox(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnknownMailbox", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001438C File Offset: 0x0001258C
		private MapiExceptionUnknownMailbox(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
