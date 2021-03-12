using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000136 RID: 310
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionVirusMessageDeleted : MapiPermanentException
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x0001393E File Offset: 0x00011B3E
		internal MapiExceptionVirusMessageDeleted(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionVirusMessageDeleted", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00013952 File Offset: 0x00011B52
		private MapiExceptionVirusMessageDeleted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
