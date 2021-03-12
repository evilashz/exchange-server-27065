using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000FA RID: 250
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionAccountDisabled : MapiPermanentException
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0001323C File Offset: 0x0001143C
		internal MapiExceptionAccountDisabled(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionAccountDisabled", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00013250 File Offset: 0x00011450
		private MapiExceptionAccountDisabled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
