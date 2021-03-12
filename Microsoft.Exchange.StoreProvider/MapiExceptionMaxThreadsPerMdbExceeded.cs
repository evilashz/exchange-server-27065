using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000DA RID: 218
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMaxThreadsPerMdbExceeded : MapiExceptionRpcServerTooBusy
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x00012E7C File Offset: 0x0001107C
		internal MapiExceptionMaxThreadsPerMdbExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMaxThreadsPerMdbExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00012E90 File Offset: 0x00011090
		private MapiExceptionMaxThreadsPerMdbExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
