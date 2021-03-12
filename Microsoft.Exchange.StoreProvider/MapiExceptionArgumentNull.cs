using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000092 RID: 146
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionArgumentNull : ExArgumentException
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x00012499 File Offset: 0x00010699
		internal MapiExceptionArgumentNull(string argumentName) : base("MapiExceptionArgumentNull: Param \"" + argumentName + "\" is null.")
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000124B1 File Offset: 0x000106B1
		private MapiExceptionArgumentNull(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
