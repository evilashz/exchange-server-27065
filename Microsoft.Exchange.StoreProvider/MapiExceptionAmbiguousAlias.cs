using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200015F RID: 351
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionAmbiguousAlias : MapiPermanentException
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x00013E0C File Offset: 0x0001200C
		internal MapiExceptionAmbiguousAlias(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionAmbiguousAlias", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00013E20 File Offset: 0x00012020
		private MapiExceptionAmbiguousAlias(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
