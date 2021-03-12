using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000102 RID: 258
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionTooBig : MapiPermanentException
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x0001332C File Offset: 0x0001152C
		internal MapiExceptionTooBig(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionTooBig", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00013340 File Offset: 0x00011540
		private MapiExceptionTooBig(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
