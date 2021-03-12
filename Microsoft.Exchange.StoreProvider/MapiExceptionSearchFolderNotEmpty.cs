using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200014C RID: 332
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionSearchFolderNotEmpty : MapiPermanentException
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x00013BD2 File Offset: 0x00011DD2
		internal MapiExceptionSearchFolderNotEmpty(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionSearchFolderNotEmpty", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00013BE6 File Offset: 0x00011DE6
		private MapiExceptionSearchFolderNotEmpty(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
