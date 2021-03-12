using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000AB RID: 171
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionTableEmpty : MapiRetryableException
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x000128E9 File Offset: 0x00010AE9
		internal MapiExceptionTableEmpty(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionTableEmpty", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000128FD File Offset: 0x00010AFD
		private MapiExceptionTableEmpty(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
