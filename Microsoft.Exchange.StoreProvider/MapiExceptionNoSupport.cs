using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000E5 RID: 229
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoSupport : MapiPermanentException
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x00012FC6 File Offset: 0x000111C6
		internal MapiExceptionNoSupport(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoSupport", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00012FDA File Offset: 0x000111DA
		private MapiExceptionNoSupport(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
