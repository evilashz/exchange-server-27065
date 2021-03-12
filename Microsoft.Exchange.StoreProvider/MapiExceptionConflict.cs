using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000FB RID: 251
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionConflict : MapiRetryableException
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0001325A File Offset: 0x0001145A
		internal MapiExceptionConflict(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionConflict", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001326E File Offset: 0x0001146E
		private MapiExceptionConflict(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
