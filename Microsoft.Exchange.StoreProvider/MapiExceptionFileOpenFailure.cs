using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000183 RID: 387
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionFileOpenFailure : MapiPermanentException
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x00014244 File Offset: 0x00012444
		internal MapiExceptionFileOpenFailure(string message, int hr) : base("MapiExceptionFileOpenFailure", message, hr, 0, null, null)
		{
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00014256 File Offset: 0x00012456
		private MapiExceptionFileOpenFailure(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
