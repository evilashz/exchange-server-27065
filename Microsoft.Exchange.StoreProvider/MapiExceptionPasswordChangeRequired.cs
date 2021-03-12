using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000F6 RID: 246
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionPasswordChangeRequired : MapiPermanentException
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x000131C4 File Offset: 0x000113C4
		internal MapiExceptionPasswordChangeRequired(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionPasswordChangeRequired", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000131D8 File Offset: 0x000113D8
		private MapiExceptionPasswordChangeRequired(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
