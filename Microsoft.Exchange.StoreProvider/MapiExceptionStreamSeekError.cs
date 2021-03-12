using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000182 RID: 386
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionStreamSeekError : MapiPermanentException
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x00014226 File Offset: 0x00012426
		internal MapiExceptionStreamSeekError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionStreamSeekError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001423A File Offset: 0x0001243A
		private MapiExceptionStreamSeekError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
