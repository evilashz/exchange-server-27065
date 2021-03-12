using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000115 RID: 277
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionTooManyProps : MapiPermanentException
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x00013566 File Offset: 0x00011766
		internal MapiExceptionTooManyProps(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionTooManyRecips", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001357A File Offset: 0x0001177A
		private MapiExceptionTooManyProps(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
