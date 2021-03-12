using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000AF RID: 175
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMailboxInTransit : MapiRetryableException
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x00012961 File Offset: 0x00010B61
		public MapiExceptionMailboxInTransit(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMailboxInTransit", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00012975 File Offset: 0x00010B75
		private MapiExceptionMailboxInTransit(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
