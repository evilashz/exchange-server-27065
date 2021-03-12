using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000B4 RID: 180
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSubsystemStopping : MapiRetryableException
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x000129F7 File Offset: 0x00010BF7
		internal MapiExceptionSubsystemStopping(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSubsystemStopping", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00012A0B File Offset: 0x00010C0B
		private MapiExceptionSubsystemStopping(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
