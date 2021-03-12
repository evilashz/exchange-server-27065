using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000E0 RID: 224
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxSearchOperationFailed : MapiRetryableException
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x00012F30 File Offset: 0x00011130
		internal MapiExceptionMultiMailboxSearchOperationFailed(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxSearchOperationFailed", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00012F44 File Offset: 0x00011144
		private MapiExceptionMultiMailboxSearchOperationFailed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
