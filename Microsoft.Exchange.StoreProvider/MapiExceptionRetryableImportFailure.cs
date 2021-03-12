using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001B0 RID: 432
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRetryableImportFailure : MapiRetryableException
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x0001475F File Offset: 0x0001295F
		internal MapiExceptionRetryableImportFailure(string message, Exception innerException) : base("MapiExceptionRetryableImportFailure", message, innerException)
		{
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001476E File Offset: 0x0001296E
		private MapiExceptionRetryableImportFailure(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
