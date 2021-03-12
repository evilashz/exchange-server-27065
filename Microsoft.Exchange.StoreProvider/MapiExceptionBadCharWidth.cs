using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000E6 RID: 230
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionBadCharWidth : MapiPermanentException
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x00012FE4 File Offset: 0x000111E4
		internal MapiExceptionBadCharWidth(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionBadCharWidth", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00012FF8 File Offset: 0x000111F8
		private MapiExceptionBadCharWidth(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
