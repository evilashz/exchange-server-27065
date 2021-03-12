using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000133 RID: 307
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionWrongServer : MapiPermanentException
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x000138EA File Offset: 0x00011AEA
		internal MapiExceptionWrongServer(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionWrongServer", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000138FE File Offset: 0x00011AFE
		private MapiExceptionWrongServer(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
