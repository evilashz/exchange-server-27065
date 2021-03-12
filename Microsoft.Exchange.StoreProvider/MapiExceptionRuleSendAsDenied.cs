using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200019E RID: 414
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRuleSendAsDenied : MapiPermanentException
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x00014548 File Offset: 0x00012748
		internal MapiExceptionRuleSendAsDenied(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRuleSendAsDenied", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001455C File Offset: 0x0001275C
		private MapiExceptionRuleSendAsDenied(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
