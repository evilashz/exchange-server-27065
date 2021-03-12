using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000A1 RID: 161
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotEnoughResources : MapiRetryableException
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x000127BD File Offset: 0x000109BD
		internal MapiExceptionNotEnoughResources(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNotEnoughResources", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000127D1 File Offset: 0x000109D1
		private MapiExceptionNotEnoughResources(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
