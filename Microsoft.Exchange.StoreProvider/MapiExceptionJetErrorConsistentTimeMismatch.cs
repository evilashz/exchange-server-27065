using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000176 RID: 374
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorConsistentTimeMismatch : MapiPermanentException
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x000140BE File Offset: 0x000122BE
		internal MapiExceptionJetErrorConsistentTimeMismatch(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorConsistentTimeMismatch", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000140D2 File Offset: 0x000122D2
		private MapiExceptionJetErrorConsistentTimeMismatch(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
