using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001AD RID: 429
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMultiMailboxSearchInvalidRestriction : MapiPermanentException
	{
		// Token: 0x0600063A RID: 1594 RVA: 0x00014705 File Offset: 0x00012905
		internal MapiExceptionMultiMailboxSearchInvalidRestriction(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMultiMailboxSearchInvalidRestriction", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00014719 File Offset: 0x00012919
		private MapiExceptionMultiMailboxSearchInvalidRestriction(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
