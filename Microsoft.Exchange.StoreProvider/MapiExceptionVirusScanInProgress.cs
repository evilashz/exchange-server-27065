using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000B0 RID: 176
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionVirusScanInProgress : MapiRetryableException
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0001297F File Offset: 0x00010B7F
		internal MapiExceptionVirusScanInProgress(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionVirusScanInProgress", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00012993 File Offset: 0x00010B93
		private MapiExceptionVirusScanInProgress(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
