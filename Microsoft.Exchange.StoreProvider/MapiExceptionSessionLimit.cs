using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000A3 RID: 163
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSessionLimit : MapiRetryableException
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x000127F9 File Offset: 0x000109F9
		internal MapiExceptionSessionLimit(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSessionLimit", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001280D File Offset: 0x00010A0D
		private MapiExceptionSessionLimit(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
