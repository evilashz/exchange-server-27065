using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000192 RID: 402
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNonCanonicalACL : MapiPermanentException
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x000143D2 File Offset: 0x000125D2
		internal MapiExceptionNonCanonicalACL(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNonCanonicalACL", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000143E6 File Offset: 0x000125E6
		private MapiExceptionNonCanonicalACL(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
