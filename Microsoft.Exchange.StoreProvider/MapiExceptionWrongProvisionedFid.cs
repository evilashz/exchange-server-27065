using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001A4 RID: 420
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionWrongProvisionedFid : MapiPermanentException
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x000145F7 File Offset: 0x000127F7
		internal MapiExceptionWrongProvisionedFid(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionWrongProvisionedFid", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001460B File Offset: 0x0001280B
		private MapiExceptionWrongProvisionedFid(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
