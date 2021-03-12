using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000CC RID: 204
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionADError : MapiRetryableException
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x00012CC7 File Offset: 0x00010EC7
		internal MapiExceptionADError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionADError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00012CDB File Offset: 0x00010EDB
		private MapiExceptionADError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
