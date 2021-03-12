using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000B9 RID: 185
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorOutOfCursors : MapiRetryableException
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x00012A8D File Offset: 0x00010C8D
		internal MapiExceptionJetErrorOutOfCursors(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorOutOfCursors", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00012AA1 File Offset: 0x00010CA1
		private MapiExceptionJetErrorOutOfCursors(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
