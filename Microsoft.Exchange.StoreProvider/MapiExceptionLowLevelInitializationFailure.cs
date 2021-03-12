using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000096 RID: 150
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionLowLevelInitializationFailure : BaseException
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x00012518 File Offset: 0x00010718
		internal MapiExceptionLowLevelInitializationFailure(string message) : base("MapiExceptionLowLevelInitializationFailure: " + message)
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001252B File Offset: 0x0001072B
		private MapiExceptionLowLevelInitializationFailure(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
