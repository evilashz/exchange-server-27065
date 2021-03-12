using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000174 RID: 372
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorAttachedDatabaseMismatch : MapiPermanentException
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x00014082 File Offset: 0x00012282
		internal MapiExceptionJetErrorAttachedDatabaseMismatch(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorAttachedDatabaseMismatch", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00014096 File Offset: 0x00012296
		private MapiExceptionJetErrorAttachedDatabaseMismatch(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
