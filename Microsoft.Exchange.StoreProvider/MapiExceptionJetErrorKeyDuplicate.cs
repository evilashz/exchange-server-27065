using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000169 RID: 361
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorKeyDuplicate : MapiPermanentException
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x00013F38 File Offset: 0x00012138
		internal MapiExceptionJetErrorKeyDuplicate(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorKeyDuplicate", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00013F4C File Offset: 0x0001214C
		private MapiExceptionJetErrorKeyDuplicate(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
