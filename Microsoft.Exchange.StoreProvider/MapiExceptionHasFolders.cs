using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200010F RID: 271
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionHasFolders : MapiPermanentException
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x000134B2 File Offset: 0x000116B2
		internal MapiExceptionHasFolders(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionHasFolders", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000134C6 File Offset: 0x000116C6
		private MapiExceptionHasFolders(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
