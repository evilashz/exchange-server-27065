using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000C0 RID: 192
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorTableInUse : MapiRetryableException
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x00012B5F File Offset: 0x00010D5F
		internal MapiExceptionJetErrorTableInUse(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorTableInUse", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00012B73 File Offset: 0x00010D73
		private MapiExceptionJetErrorTableInUse(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
