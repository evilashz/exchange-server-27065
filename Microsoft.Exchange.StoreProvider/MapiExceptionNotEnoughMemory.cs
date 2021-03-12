using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200009B RID: 155
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotEnoughMemory : MapiRetryableException
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x00012709 File Offset: 0x00010909
		internal MapiExceptionNotEnoughMemory(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNotEnoughMemory", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001271D File Offset: 0x0001091D
		private MapiExceptionNotEnoughMemory(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
