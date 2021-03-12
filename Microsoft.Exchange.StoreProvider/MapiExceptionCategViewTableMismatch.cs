using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000153 RID: 339
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCategViewTableMismatch : MapiPermanentException
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x00013CA4 File Offset: 0x00011EA4
		internal MapiExceptionCategViewTableMismatch(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCategViewTableMismatch", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00013CB8 File Offset: 0x00011EB8
		private MapiExceptionCategViewTableMismatch(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
