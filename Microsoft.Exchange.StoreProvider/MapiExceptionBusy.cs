using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200009F RID: 159
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionBusy : MapiRetryableException
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x00012781 File Offset: 0x00010981
		internal MapiExceptionBusy(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionBusy", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00012795 File Offset: 0x00010995
		private MapiExceptionBusy(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
