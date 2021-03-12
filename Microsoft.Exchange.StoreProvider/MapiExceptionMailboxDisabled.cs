using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000148 RID: 328
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionMailboxDisabled : MapiPermanentException
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x00013B5A File Offset: 0x00011D5A
		internal MapiExceptionMailboxDisabled(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionMailboxDisabled", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00013B6E File Offset: 0x00011D6E
		private MapiExceptionMailboxDisabled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
