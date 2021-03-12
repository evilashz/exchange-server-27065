using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConsoleLog : Log
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000242B File Offset: 0x0000062B
		public ConsoleLog() : base(new ConsoleLogEmitter(), LogLevel.LogAll)
		{
		}
	}
}
