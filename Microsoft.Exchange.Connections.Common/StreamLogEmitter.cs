using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StreamLogEmitter : ILogEmitter
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002969 File Offset: 0x00000B69
		internal StreamLogEmitter(StreamWriter writer)
		{
			this.Writer = writer;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002978 File Offset: 0x00000B78
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002980 File Offset: 0x00000B80
		private protected StreamWriter Writer { protected get; private set; }

		// Token: 0x0600007F RID: 127 RVA: 0x00002989 File Offset: 0x00000B89
		public void Emit(string formatString, params object[] args)
		{
			this.Writer.WriteLine(formatString, args);
		}
	}
}
