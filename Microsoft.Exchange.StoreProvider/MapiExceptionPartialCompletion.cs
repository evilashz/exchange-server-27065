using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000143 RID: 323
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionPartialCompletion : MapiPermanentException
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x00013AC4 File Offset: 0x00011CC4
		internal MapiExceptionPartialCompletion(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionPartialCompletion", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00013AD8 File Offset: 0x00011CD8
		private MapiExceptionPartialCompletion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
