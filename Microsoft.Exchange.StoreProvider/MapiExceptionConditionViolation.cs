using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000D4 RID: 212
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionConditionViolation : MapiRetryableException
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x00012DC8 File Offset: 0x00010FC8
		internal MapiExceptionConditionViolation(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionConditionViolation", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00012DDC File Offset: 0x00010FDC
		private MapiExceptionConditionViolation(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
