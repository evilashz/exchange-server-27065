using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200017A RID: 378
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnknownUser : MapiPermanentException
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x00014136 File Offset: 0x00012336
		internal MapiExceptionUnknownUser(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnknownUser", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001414A File Offset: 0x0001234A
		private MapiExceptionUnknownUser(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
