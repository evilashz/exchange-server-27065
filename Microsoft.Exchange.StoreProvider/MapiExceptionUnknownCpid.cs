using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000F4 RID: 244
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnknownCpid : MapiPermanentException
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x00013188 File Offset: 0x00011388
		internal MapiExceptionUnknownCpid(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnknownCpid", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001319C File Offset: 0x0001139C
		private MapiExceptionUnknownCpid(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
