using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000D8 RID: 216
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMountInProgress : MapiRetryableException
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x00012E40 File Offset: 0x00011040
		internal MapiExceptionMountInProgress(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMountInProgress", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00012E54 File Offset: 0x00011054
		private MapiExceptionMountInProgress(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
