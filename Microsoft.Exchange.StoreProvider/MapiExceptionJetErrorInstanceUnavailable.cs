using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000BD RID: 189
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorInstanceUnavailable : MapiRetryableException
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00012B05 File Offset: 0x00010D05
		internal MapiExceptionJetErrorInstanceUnavailable(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorInstanceUnavailable", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00012B19 File Offset: 0x00010D19
		private MapiExceptionJetErrorInstanceUnavailable(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
