using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000181 RID: 385
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionStreamSizeError : MapiPermanentException
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00014208 File Offset: 0x00012408
		internal MapiExceptionStreamSizeError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionStreamSizeError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001421C File Offset: 0x0001241C
		private MapiExceptionStreamSizeError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
