using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000ED RID: 237
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionTooComplex : MapiPermanentException
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x000130B6 File Offset: 0x000112B6
		internal MapiExceptionTooComplex(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionTooComplex", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000130CA File Offset: 0x000112CA
		private MapiExceptionTooComplex(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
