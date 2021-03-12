using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000DB RID: 219
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMaxThreadsPerSCTExceeded : MapiExceptionRpcServerTooBusy
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x00012E9A File Offset: 0x0001109A
		internal MapiExceptionMaxThreadsPerSCTExceeded(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMaxThreadsPerSCTExceeded", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00012EAE File Offset: 0x000110AE
		private MapiExceptionMaxThreadsPerSCTExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
