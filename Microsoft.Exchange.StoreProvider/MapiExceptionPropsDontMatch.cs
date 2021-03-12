using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000163 RID: 355
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionPropsDontMatch : MapiPermanentException
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00013E84 File Offset: 0x00012084
		internal MapiExceptionPropsDontMatch(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionPropsDontMatch", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00013E98 File Offset: 0x00012098
		private MapiExceptionPropsDontMatch(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
