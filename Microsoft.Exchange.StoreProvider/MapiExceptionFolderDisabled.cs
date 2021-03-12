using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000127 RID: 295
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionFolderDisabled : MapiPermanentException
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x00013782 File Offset: 0x00011982
		internal MapiExceptionFolderDisabled(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionFolderDisabled", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00013796 File Offset: 0x00011996
		private MapiExceptionFolderDisabled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
