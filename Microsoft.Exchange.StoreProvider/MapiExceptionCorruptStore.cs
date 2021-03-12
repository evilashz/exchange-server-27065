using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000108 RID: 264
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCorruptStore : MapiPermanentException
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x000133E0 File Offset: 0x000115E0
		internal MapiExceptionCorruptStore(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCorruptStore", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000133F4 File Offset: 0x000115F4
		private MapiExceptionCorruptStore(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
