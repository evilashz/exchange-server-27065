using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000D6 RID: 214
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionLowDatabaseLogDiskSpace : MapiRetryableException
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x00012E04 File Offset: 0x00011004
		internal MapiExceptionLowDatabaseLogDiskSpace(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionLowDatabaseLogDiskSpace", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00012E18 File Offset: 0x00011018
		private MapiExceptionLowDatabaseLogDiskSpace(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
