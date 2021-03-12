using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000F7 RID: 247
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionPasswordExpired : MapiPermanentException
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x000131E2 File Offset: 0x000113E2
		internal MapiExceptionPasswordExpired(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionPasswordExpired", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000131F6 File Offset: 0x000113F6
		private MapiExceptionPasswordExpired(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
