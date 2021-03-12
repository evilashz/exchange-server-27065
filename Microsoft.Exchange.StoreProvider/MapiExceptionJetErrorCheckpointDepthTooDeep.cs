using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000B8 RID: 184
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorCheckpointDepthTooDeep : MapiRetryableException
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x00012A6F File Offset: 0x00010C6F
		internal MapiExceptionJetErrorCheckpointDepthTooDeep(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorCheckpointDepthTooDeep", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00012A83 File Offset: 0x00010C83
		private MapiExceptionJetErrorCheckpointDepthTooDeep(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
