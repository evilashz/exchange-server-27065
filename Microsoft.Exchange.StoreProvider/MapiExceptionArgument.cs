using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000091 RID: 145
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionArgument : ExArgumentException
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00012476 File Offset: 0x00010676
		internal MapiExceptionArgument(string argumentName, string message) : base("MapiExceptionArgument: Param \"" + argumentName + "\" - " + message)
		{
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001248F File Offset: 0x0001068F
		private MapiExceptionArgument(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
