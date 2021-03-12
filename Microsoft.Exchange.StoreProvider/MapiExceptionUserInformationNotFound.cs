using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001B3 RID: 435
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUserInformationNotFound : MapiPermanentException
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x000147B4 File Offset: 0x000129B4
		internal MapiExceptionUserInformationNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUserInformationNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000147C8 File Offset: 0x000129C8
		private MapiExceptionUserInformationNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
