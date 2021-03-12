using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000160 RID: 352
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionADDuplicateEntry : MapiPermanentException
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x00013E2A File Offset: 0x0001202A
		internal MapiExceptionADDuplicateEntry(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionADDuplicateEntry", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00013E3E File Offset: 0x0001203E
		private MapiExceptionADDuplicateEntry(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
