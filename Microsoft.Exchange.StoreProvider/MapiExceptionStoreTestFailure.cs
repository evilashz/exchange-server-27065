using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000179 RID: 377
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionStoreTestFailure : MapiRetryableException
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x00014118 File Offset: 0x00012318
		internal MapiExceptionStoreTestFailure(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionStoreTestFailure", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001412C File Offset: 0x0001232C
		private MapiExceptionStoreTestFailure(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
