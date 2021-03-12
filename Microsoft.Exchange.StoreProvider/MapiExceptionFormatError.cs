using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000120 RID: 288
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionFormatError : MapiPermanentException
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x000136B0 File Offset: 0x000118B0
		internal MapiExceptionFormatError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionFormatError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000136C4 File Offset: 0x000118C4
		private MapiExceptionFormatError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
