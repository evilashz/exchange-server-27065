using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200009D RID: 157
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidObject : MapiRetryableException
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x00012745 File Offset: 0x00010945
		internal MapiExceptionInvalidObject(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidObject", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00012759 File Offset: 0x00010959
		private MapiExceptionInvalidObject(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
