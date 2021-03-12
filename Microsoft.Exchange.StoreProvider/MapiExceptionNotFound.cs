using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000EB RID: 235
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotFound : MapiPermanentException
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0001307A File Offset: 0x0001127A
		internal MapiExceptionNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001308E File Offset: 0x0001128E
		private MapiExceptionNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
