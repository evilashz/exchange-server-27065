using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000195 RID: 405
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorTableDuplicate : MapiPermanentException
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x0001442C File Offset: 0x0001262C
		internal MapiExceptionJetErrorTableDuplicate(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorTableDuplicate", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00014440 File Offset: 0x00012640
		private MapiExceptionJetErrorTableDuplicate(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
