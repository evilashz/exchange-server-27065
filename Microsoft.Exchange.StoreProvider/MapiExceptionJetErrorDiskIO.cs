using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000C5 RID: 197
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorDiskIO : MapiRetryableException
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x00012BF5 File Offset: 0x00010DF5
		internal MapiExceptionJetErrorDiskIO(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorDiskIO", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00012C09 File Offset: 0x00010E09
		private MapiExceptionJetErrorDiskIO(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
