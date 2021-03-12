using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001B2 RID: 434
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUserInformationLockTimeout : MapiRetryableException
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x00014796 File Offset: 0x00012996
		internal MapiExceptionUserInformationLockTimeout(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUserInformationLockTimeout", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000147AA File Offset: 0x000129AA
		private MapiExceptionUserInformationLockTimeout(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
