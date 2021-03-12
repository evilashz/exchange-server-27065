using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001A1 RID: 417
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionTooManyMountedDatabases : MapiPermanentException
	{
		// Token: 0x06000622 RID: 1570 RVA: 0x000145A2 File Offset: 0x000127A2
		internal MapiExceptionTooManyMountedDatabases(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionTooManyMountedDatabases", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000145B6 File Offset: 0x000127B6
		private MapiExceptionTooManyMountedDatabases(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
