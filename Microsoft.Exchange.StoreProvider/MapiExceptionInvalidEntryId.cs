using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000E9 RID: 233
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidEntryId : MapiPermanentException
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0001303E File Offset: 0x0001123E
		internal MapiExceptionInvalidEntryId(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidEntryId", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00013052 File Offset: 0x00011252
		private MapiExceptionInvalidEntryId(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
