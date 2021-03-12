using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000B1 RID: 177
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionBackupInProgress : MapiRetryableException
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x0001299D File Offset: 0x00010B9D
		internal MapiExceptionBackupInProgress(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionBackupInProgress", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000129B1 File Offset: 0x00010BB1
		private MapiExceptionBackupInProgress(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
