using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200014E RID: 334
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionRecursiveSearchChainTooDeep : MapiPermanentException
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x00013C0E File Offset: 0x00011E0E
		internal MapiExceptionRecursiveSearchChainTooDeep(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionRecursiveSearchChainTooDeep", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00013C22 File Offset: 0x00011E22
		private MapiExceptionRecursiveSearchChainTooDeep(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
