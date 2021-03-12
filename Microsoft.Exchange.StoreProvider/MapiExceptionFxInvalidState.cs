using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000161 RID: 353
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionFxInvalidState : MapiPermanentException
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x00013E48 File Offset: 0x00012048
		internal MapiExceptionFxInvalidState(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionFxInvalidState", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00013E5C File Offset: 0x0001205C
		private MapiExceptionFxInvalidState(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
