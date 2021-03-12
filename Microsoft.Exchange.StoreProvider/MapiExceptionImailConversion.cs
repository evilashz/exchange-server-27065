using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200015E RID: 350
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionImailConversion : MapiPermanentException
	{
		// Token: 0x0600059B RID: 1435 RVA: 0x00013DEE File Offset: 0x00011FEE
		internal MapiExceptionImailConversion(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionImailConversion", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00013E02 File Offset: 0x00012002
		private MapiExceptionImailConversion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
