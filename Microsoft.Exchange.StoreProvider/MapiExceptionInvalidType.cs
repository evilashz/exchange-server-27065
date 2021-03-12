using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000FF RID: 255
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidType : MapiPermanentException
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x000132D2 File Offset: 0x000114D2
		internal MapiExceptionInvalidType(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidType", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000132E6 File Offset: 0x000114E6
		private MapiExceptionInvalidType(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
