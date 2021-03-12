using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000093 RID: 147
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionArgumentOutOfRange : ExArgumentException
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x000124BB File Offset: 0x000106BB
		internal MapiExceptionArgumentOutOfRange(string argumentName, string message) : base("MapiExceptionArgumentOutOfRange: Param \"" + argumentName + "\" - " + message)
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000124D4 File Offset: 0x000106D4
		private MapiExceptionArgumentOutOfRange(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
