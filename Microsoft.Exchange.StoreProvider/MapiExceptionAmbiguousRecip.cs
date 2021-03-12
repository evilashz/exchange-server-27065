using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000119 RID: 281
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionAmbiguousRecip : MapiPermanentException
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x000135DE File Offset: 0x000117DE
		internal MapiExceptionAmbiguousRecip(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionAmbiguousRecip", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000135F2 File Offset: 0x000117F2
		private MapiExceptionAmbiguousRecip(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
