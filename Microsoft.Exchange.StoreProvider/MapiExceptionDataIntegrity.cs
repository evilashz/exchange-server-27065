using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200018B RID: 395
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionDataIntegrity : MapiPermanentException
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00014312 File Offset: 0x00012512
		internal MapiExceptionDataIntegrity(string message) : base("MapiExceptionDataIntegrity", message)
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00014320 File Offset: 0x00012520
		private MapiExceptionDataIntegrity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
