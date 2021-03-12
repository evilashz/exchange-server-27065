using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Storage.ServerLocator;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x020002F9 RID: 761
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WcfProxyObjectsPerServerStack
	{
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x0008A801 File Offset: 0x00088A01
		internal int Count
		{
			get
			{
				return this.m_stack.Count;
			}
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x0008A810 File Offset: 0x00088A10
		internal void Push(ServerLocatorServiceClient client)
		{
			Interlocked.Exchange(ref this.m_lastAccessTicksUtc, DateTime.UtcNow.Ticks);
			this.m_stack.Push(client);
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0008A842 File Offset: 0x00088A42
		internal ServerLocatorServiceClient Pop()
		{
			return this.m_stack.Pop();
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0008A84F File Offset: 0x00088A4F
		public DateTime LastAccessTimeUtc
		{
			get
			{
				return new DateTime(Interlocked.Read(ref this.m_lastAccessTicksUtc));
			}
		}

		// Token: 0x0400140B RID: 5131
		private long m_lastAccessTicksUtc = long.MinValue;

		// Token: 0x0400140C RID: 5132
		private Stack<ServerLocatorServiceClient> m_stack = new Stack<ServerLocatorServiceClient>(10);
	}
}
