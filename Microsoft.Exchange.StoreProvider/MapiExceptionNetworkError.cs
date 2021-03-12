using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000A6 RID: 166
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNetworkError : MapiRetryableException
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x00012853 File Offset: 0x00010A53
		internal MapiExceptionNetworkError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNetworkError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00012867 File Offset: 0x00010A67
		private MapiExceptionNetworkError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
