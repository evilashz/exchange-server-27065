using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200010A RID: 266
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoSuppress : MapiPermanentException
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x0001341C File Offset: 0x0001161C
		internal MapiExceptionNoSuppress(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoSuppress", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00013430 File Offset: 0x00011630
		private MapiExceptionNoSuppress(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
