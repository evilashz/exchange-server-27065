using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200019D RID: 413
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRuleFormat : MapiPermanentException
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x0001451C File Offset: 0x0001271C
		internal MapiExceptionRuleFormat(string message) : base("MapiExceptionRuleFormat", message)
		{
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001452A File Offset: 0x0001272A
		internal MapiExceptionRuleFormat(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRuleFormat", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001453E File Offset: 0x0001273E
		private MapiExceptionRuleFormat(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
