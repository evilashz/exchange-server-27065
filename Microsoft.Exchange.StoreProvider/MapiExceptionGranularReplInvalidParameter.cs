using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200013A RID: 314
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionGranularReplInvalidParameter : MapiRetryableException
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x000139B6 File Offset: 0x00011BB6
		internal MapiExceptionGranularReplInvalidParameter(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionGranularReplInvalidParameter", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000139CA File Offset: 0x00011BCA
		private MapiExceptionGranularReplInvalidParameter(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
