using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000150 RID: 336
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCannotDeriveMsgViewFromBase : MapiPermanentException
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x00013C4A File Offset: 0x00011E4A
		internal MapiExceptionCannotDeriveMsgViewFromBase(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCannotDeriveMsgViewFromBase", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00013C5E File Offset: 0x00011E5E
		private MapiExceptionCannotDeriveMsgViewFromBase(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
