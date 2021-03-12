using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConsoleLogEmitter : ILogEmitter
	{
		// Token: 0x0600003C RID: 60 RVA: 0x0000243C File Offset: 0x0000063C
		public void Emit(string formatString, params object[] args)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			this.WriteInternal(formatString, args);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002463 File Offset: 0x00000663
		private void WriteInternal(string formatString, params object[] args)
		{
			if (args == null || args.Length == 0)
			{
				Console.WriteLine(formatString);
				return;
			}
			Console.WriteLine(formatString, args);
		}
	}
}
