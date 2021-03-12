using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NullLogEmitter : ILogEmitter
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000264C File Offset: 0x0000084C
		public void Emit(string formatString, params object[] args)
		{
		}
	}
}
