using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000E7 RID: 231
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionStringTooLong : MapiPermanentException
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x00013002 File Offset: 0x00011202
		internal MapiExceptionStringTooLong(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionStringTooLong", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00013016 File Offset: 0x00011216
		private MapiExceptionStringTooLong(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
