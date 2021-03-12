using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000124 RID: 292
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMessageTooBig : MapiPermanentException
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00013728 File Offset: 0x00011928
		internal MapiExceptionMessageTooBig(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMessageTooBig", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001373C File Offset: 0x0001193C
		private MapiExceptionMessageTooBig(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
