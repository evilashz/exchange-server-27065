using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000175 RID: 373
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorDatabaseInvalidPath : MapiPermanentException
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x000140A0 File Offset: 0x000122A0
		internal MapiExceptionJetErrorDatabaseInvalidPath(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorDatabaseInvalidPath", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000140B4 File Offset: 0x000122B4
		private MapiExceptionJetErrorDatabaseInvalidPath(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
