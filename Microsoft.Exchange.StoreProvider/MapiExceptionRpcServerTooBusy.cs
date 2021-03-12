using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000CF RID: 207
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MapiExceptionRpcServerTooBusy : MapiRetryableException
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00012D21 File Offset: 0x00010F21
		internal MapiExceptionRpcServerTooBusy(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRpcServerTooBusy", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00012D35 File Offset: 0x00010F35
		protected MapiExceptionRpcServerTooBusy(string className, string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base(className, message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00012D46 File Offset: 0x00010F46
		protected MapiExceptionRpcServerTooBusy(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
