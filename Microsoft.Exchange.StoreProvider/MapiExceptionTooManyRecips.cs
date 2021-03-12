using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000114 RID: 276
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionTooManyRecips : MapiPermanentException
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x00013548 File Offset: 0x00011748
		internal MapiExceptionTooManyRecips(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionTooManyRecips", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001355C File Offset: 0x0001175C
		private MapiExceptionTooManyRecips(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
