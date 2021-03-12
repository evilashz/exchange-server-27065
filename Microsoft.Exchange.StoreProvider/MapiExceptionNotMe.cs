using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000107 RID: 263
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotMe : MapiPermanentException
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x000133C2 File Offset: 0x000115C2
		internal MapiExceptionNotMe(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNotMe", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000133D6 File Offset: 0x000115D6
		private MapiExceptionNotMe(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
