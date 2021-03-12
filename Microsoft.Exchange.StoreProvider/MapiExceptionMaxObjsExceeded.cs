using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000142 RID: 322
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMaxObjsExceeded : MapiPermanentException
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x00013AA6 File Offset: 0x00011CA6
		internal MapiExceptionMaxObjsExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMaxObjsExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00013ABA File Offset: 0x00011CBA
		private MapiExceptionMaxObjsExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
