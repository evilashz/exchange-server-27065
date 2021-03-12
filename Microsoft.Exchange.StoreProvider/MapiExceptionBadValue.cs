using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000FE RID: 254
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionBadValue : MapiPermanentException
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x000132B4 File Offset: 0x000114B4
		internal MapiExceptionBadValue(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionBadValue", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000132C8 File Offset: 0x000114C8
		private MapiExceptionBadValue(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
