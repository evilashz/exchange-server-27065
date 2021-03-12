using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200019A RID: 410
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorInstanceNameInUse : MapiPermanentException
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x000144C2 File Offset: 0x000126C2
		internal MapiExceptionJetErrorInstanceNameInUse(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorInstanceNameInUse", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000144D6 File Offset: 0x000126D6
		private MapiExceptionJetErrorInstanceNameInUse(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
