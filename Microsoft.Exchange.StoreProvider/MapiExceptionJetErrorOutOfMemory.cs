using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000C7 RID: 199
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorOutOfMemory : MapiRetryableException
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x00012C31 File Offset: 0x00010E31
		internal MapiExceptionJetErrorOutOfMemory(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorOutOfMemory", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00012C45 File Offset: 0x00010E45
		private MapiExceptionJetErrorOutOfMemory(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
