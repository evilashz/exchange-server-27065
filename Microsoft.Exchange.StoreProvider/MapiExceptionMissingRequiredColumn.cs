using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000FD RID: 253
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMissingRequiredColumn : MapiPermanentException
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x00013296 File Offset: 0x00011496
		internal MapiExceptionMissingRequiredColumn(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMissingRequiredColumn", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000132AA File Offset: 0x000114AA
		private MapiExceptionMissingRequiredColumn(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
