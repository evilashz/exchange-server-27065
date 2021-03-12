using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000125 RID: 293
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionFormNotValid : MapiPermanentException
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x00013746 File Offset: 0x00011946
		internal MapiExceptionFormNotValid(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionFormNotValid", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001375A File Offset: 0x0001195A
		private MapiExceptionFormNotValid(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
