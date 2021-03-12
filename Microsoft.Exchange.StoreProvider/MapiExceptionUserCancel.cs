using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000A4 RID: 164
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUserCancel : MapiRetryableException
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x00012817 File Offset: 0x00010A17
		internal MapiExceptionUserCancel(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUserCancel", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001282B File Offset: 0x00010A2B
		private MapiExceptionUserCancel(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
