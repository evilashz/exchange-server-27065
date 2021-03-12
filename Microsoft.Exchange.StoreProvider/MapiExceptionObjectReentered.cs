using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000186 RID: 390
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionObjectReentered : BaseException
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x00014290 File Offset: 0x00012490
		internal MapiExceptionObjectReentered(string message) : base("MapiExceptionObjectReentered: " + message)
		{
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000142A3 File Offset: 0x000124A3
		private MapiExceptionObjectReentered(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
