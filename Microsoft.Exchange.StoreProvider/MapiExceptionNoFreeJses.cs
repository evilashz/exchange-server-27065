using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000D3 RID: 211
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoFreeJses : MapiRetryableException
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x00012DAA File Offset: 0x00010FAA
		internal MapiExceptionNoFreeJses(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoFreeJses", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00012DBE File Offset: 0x00010FBE
		private MapiExceptionNoFreeJses(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
