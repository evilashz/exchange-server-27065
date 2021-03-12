using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200014D RID: 333
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSearchFolderScopeViolation : MapiPermanentException
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x00013BF0 File Offset: 0x00011DF0
		internal MapiExceptionSearchFolderScopeViolation(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSearchFolderScopeViolation", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00013C04 File Offset: 0x00011E04
		private MapiExceptionSearchFolderScopeViolation(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
