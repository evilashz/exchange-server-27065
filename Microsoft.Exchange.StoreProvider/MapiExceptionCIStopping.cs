using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000B6 RID: 182
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCIStopping : MapiRetryableException
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x00012A33 File Offset: 0x00010C33
		internal MapiExceptionCIStopping(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCIStopping", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00012A47 File Offset: 0x00010C47
		private MapiExceptionCIStopping(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
