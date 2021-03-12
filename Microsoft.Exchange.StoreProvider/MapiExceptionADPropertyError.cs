using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000CE RID: 206
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionADPropertyError : MapiRetryableException
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00012D03 File Offset: 0x00010F03
		internal MapiExceptionADPropertyError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionADPropertyError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00012D17 File Offset: 0x00010F17
		private MapiExceptionADPropertyError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
