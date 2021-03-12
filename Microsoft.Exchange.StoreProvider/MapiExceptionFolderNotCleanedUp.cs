using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200012E RID: 302
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionFolderNotCleanedUp : MapiPermanentException
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x00013854 File Offset: 0x00011A54
		internal MapiExceptionFolderNotCleanedUp(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionFolderNotCleanedUp", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00013868 File Offset: 0x00011A68
		private MapiExceptionFolderNotCleanedUp(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
