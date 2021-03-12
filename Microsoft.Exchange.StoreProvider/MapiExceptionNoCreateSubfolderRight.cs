using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200012C RID: 300
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoCreateSubfolderRight : MapiPermanentException
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x00013818 File Offset: 0x00011A18
		internal MapiExceptionNoCreateSubfolderRight(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoCreateSubfolderRight", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001382C File Offset: 0x00011A2C
		private MapiExceptionNoCreateSubfolderRight(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
