using System;
using System.Net.Sockets;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000037 RID: 55
	internal class SocketStreamAsyncArgs : SocketAsyncEventArgs, IPoolableObject
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000785B File Offset: 0x00005A5B
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00007863 File Offset: 0x00005A63
		public bool Preallocated { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000786C File Offset: 0x00005A6C
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00007874 File Offset: 0x00005A74
		public SimpleBuffer InternalBuffer { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000787D File Offset: 0x00005A7D
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00007885 File Offset: 0x00005A85
		public EventHandler<SocketAsyncEventArgs> CompletionRtn { get; set; }

		// Token: 0x060001DA RID: 474 RVA: 0x0000788E File Offset: 0x00005A8E
		public SocketStreamAsyncArgs(bool preallocated = false)
		{
			this.Preallocated = preallocated;
		}
	}
}
