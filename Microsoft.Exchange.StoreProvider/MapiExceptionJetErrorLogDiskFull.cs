using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000197 RID: 407
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorLogDiskFull : MapiPermanentException
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x00014468 File Offset: 0x00012668
		internal MapiExceptionJetErrorLogDiskFull(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorLogDiskFull", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001447C File Offset: 0x0001267C
		private MapiExceptionJetErrorLogDiskFull(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
