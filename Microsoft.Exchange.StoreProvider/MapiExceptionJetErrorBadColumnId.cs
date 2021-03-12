using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000173 RID: 371
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorBadColumnId : MapiPermanentException
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x00014064 File Offset: 0x00012264
		internal MapiExceptionJetErrorBadColumnId(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorBadColumnId", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00014078 File Offset: 0x00012278
		private MapiExceptionJetErrorBadColumnId(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
