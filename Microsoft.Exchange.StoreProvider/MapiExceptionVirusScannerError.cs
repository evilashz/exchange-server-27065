using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000137 RID: 311
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionVirusScannerError : MapiPermanentException
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x0001395C File Offset: 0x00011B5C
		internal MapiExceptionVirusScannerError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionVirusScannerError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00013970 File Offset: 0x00011B70
		private MapiExceptionVirusScannerError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
