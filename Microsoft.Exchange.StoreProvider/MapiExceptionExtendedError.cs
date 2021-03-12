using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000EF RID: 239
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionExtendedError : MapiPermanentException
	{
		// Token: 0x060004BD RID: 1213 RVA: 0x000130F2 File Offset: 0x000112F2
		internal MapiExceptionExtendedError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base(" MapiExceptionExtendedError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00013106 File Offset: 0x00011306
		private MapiExceptionExtendedError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
