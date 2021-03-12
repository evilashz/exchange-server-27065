using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200010C RID: 268
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNonStandard : MapiPermanentException
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00013458 File Offset: 0x00011658
		internal MapiExceptionNonStandard(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNonStandard", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001346C File Offset: 0x0001166C
		private MapiExceptionNonStandard(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
