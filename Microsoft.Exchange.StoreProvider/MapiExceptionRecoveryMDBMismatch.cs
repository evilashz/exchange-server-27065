using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200014B RID: 331
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRecoveryMDBMismatch : MapiPermanentException
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00013BB4 File Offset: 0x00011DB4
		internal MapiExceptionRecoveryMDBMismatch(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRecoveryMDBMismatch", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00013BC8 File Offset: 0x00011DC8
		private MapiExceptionRecoveryMDBMismatch(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
