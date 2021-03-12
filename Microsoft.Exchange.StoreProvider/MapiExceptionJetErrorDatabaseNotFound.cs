using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000172 RID: 370
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorDatabaseNotFound : MapiPermanentException
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x00014046 File Offset: 0x00012246
		internal MapiExceptionJetErrorDatabaseNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorDatabaseNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001405A File Offset: 0x0001225A
		private MapiExceptionJetErrorDatabaseNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
