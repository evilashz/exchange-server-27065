using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200009C RID: 156
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionLockViolation : MapiRetryableException
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00012727 File Offset: 0x00010927
		internal MapiExceptionLockViolation(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionLockViolation", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001273B File Offset: 0x0001093B
		private MapiExceptionLockViolation(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
