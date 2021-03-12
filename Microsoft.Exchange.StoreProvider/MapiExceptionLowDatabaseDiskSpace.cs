using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000D5 RID: 213
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionLowDatabaseDiskSpace : MapiRetryableException
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x00012DE6 File Offset: 0x00010FE6
		internal MapiExceptionLowDatabaseDiskSpace(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionLowDatabaseDiskSpace", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00012DFA File Offset: 0x00010FFA
		private MapiExceptionLowDatabaseDiskSpace(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
