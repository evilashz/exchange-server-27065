using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000159 RID: 345
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionAssertionFailedError : MapiPermanentException
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00013D58 File Offset: 0x00011F58
		internal MapiExceptionAssertionFailedError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionAssertionFailedError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00013D6C File Offset: 0x00011F6C
		private MapiExceptionAssertionFailedError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
