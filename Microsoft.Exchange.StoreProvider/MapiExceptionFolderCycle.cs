using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000111 RID: 273
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionFolderCycle : MapiPermanentException
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x000134EE File Offset: 0x000116EE
		internal MapiExceptionFolderCycle(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionFolderCycle", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00013502 File Offset: 0x00011702
		private MapiExceptionFolderCycle(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
