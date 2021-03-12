using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001A3 RID: 419
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionPermanentImportFailure : MapiPermanentException
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x000145DE File Offset: 0x000127DE
		internal MapiExceptionPermanentImportFailure(string message, Exception innerException) : base("MapiExceptionPermanentImportFailure", message, innerException)
		{
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000145ED File Offset: 0x000127ED
		private MapiExceptionPermanentImportFailure(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
