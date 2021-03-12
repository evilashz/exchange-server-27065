using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200009E RID: 158
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionObjectChanged : MapiRetryableException
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x00012763 File Offset: 0x00010963
		internal MapiExceptionObjectChanged(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionObjectChanged", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00012777 File Offset: 0x00010977
		private MapiExceptionObjectChanged(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
