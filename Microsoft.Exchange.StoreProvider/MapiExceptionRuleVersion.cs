using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200019C RID: 412
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRuleVersion : MapiPermanentException
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x000144FE File Offset: 0x000126FE
		internal MapiExceptionRuleVersion(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRuleVersion", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00014512 File Offset: 0x00012712
		private MapiExceptionRuleVersion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
