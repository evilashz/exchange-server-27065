using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000A9 RID: 169
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionDeclineCopy : MapiRetryableException
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x000128AD File Offset: 0x00010AAD
		internal MapiExceptionDeclineCopy(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionDeclineCopy", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000128C1 File Offset: 0x00010AC1
		private MapiExceptionDeclineCopy(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
