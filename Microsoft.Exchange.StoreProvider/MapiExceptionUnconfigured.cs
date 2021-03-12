using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000F2 RID: 242
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnconfigured : MapiPermanentException
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x0001314C File Offset: 0x0001134C
		internal MapiExceptionUnconfigured(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnconfigured", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00013160 File Offset: 0x00011360
		private MapiExceptionUnconfigured(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
