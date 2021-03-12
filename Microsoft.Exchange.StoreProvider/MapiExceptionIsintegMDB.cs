using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200014A RID: 330
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionIsintegMDB : MapiRetryableException
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x00013B96 File Offset: 0x00011D96
		internal MapiExceptionIsintegMDB(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionIsintegMDB", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00013BAA File Offset: 0x00011DAA
		private MapiExceptionIsintegMDB(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
