using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001B1 RID: 433
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUserInformationAlreadyExists : MapiPermanentException
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x00014778 File Offset: 0x00012978
		internal MapiExceptionUserInformationAlreadyExists(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUserInformationAlreadyExists", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001478C File Offset: 0x0001298C
		private MapiExceptionUserInformationAlreadyExists(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
