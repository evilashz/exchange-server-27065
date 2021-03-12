using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000AE RID: 174
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCollision : MapiRetryableException
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x00012943 File Offset: 0x00010B43
		internal MapiExceptionCollision(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCollision", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00012957 File Offset: 0x00010B57
		private MapiExceptionCollision(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
