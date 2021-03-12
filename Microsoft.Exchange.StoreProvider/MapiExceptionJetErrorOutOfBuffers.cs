using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000BA RID: 186
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorOutOfBuffers : MapiRetryableException
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x00012AAB File Offset: 0x00010CAB
		internal MapiExceptionJetErrorOutOfBuffers(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorOutOfBuffers", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00012ABF File Offset: 0x00010CBF
		private MapiExceptionJetErrorOutOfBuffers(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
