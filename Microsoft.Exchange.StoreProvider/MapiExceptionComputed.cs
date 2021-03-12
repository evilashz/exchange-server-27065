using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000F0 RID: 240
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionComputed : MapiPermanentException
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00013110 File Offset: 0x00011310
		internal MapiExceptionComputed(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionComputed", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00013124 File Offset: 0x00011324
		private MapiExceptionComputed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
