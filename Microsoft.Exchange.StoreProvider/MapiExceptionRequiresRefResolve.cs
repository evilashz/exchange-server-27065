using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000180 RID: 384
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRequiresRefResolve : MapiPermanentException
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x000141EA File Offset: 0x000123EA
		internal MapiExceptionRequiresRefResolve(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRequiresRefResolve", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000141FE File Offset: 0x000123FE
		private MapiExceptionRequiresRefResolve(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
