using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000EA RID: 234
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionObjectDeleted : MapiPermanentException
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0001305C File Offset: 0x0001125C
		internal MapiExceptionObjectDeleted(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionObjectDeleted", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00013070 File Offset: 0x00011270
		private MapiExceptionObjectDeleted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
