using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000141 RID: 321
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionDatabaseStateConflict : MapiPermanentException
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x00013A88 File Offset: 0x00011C88
		internal MapiExceptionDatabaseStateConflict(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionDatabaseStateConflict", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00013A9C File Offset: 0x00011C9C
		private MapiExceptionDatabaseStateConflict(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
