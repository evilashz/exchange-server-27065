using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000E4 RID: 228
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoAccess : MapiPermanentException
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x00012FA8 File Offset: 0x000111A8
		internal MapiExceptionNoAccess(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoAccess", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00012FBC File Offset: 0x000111BC
		private MapiExceptionNoAccess(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
