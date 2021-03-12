using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000158 RID: 344
	internal sealed class BrokerHandlerReferenceCounter : DisposeTrackableBase
	{
		// Token: 0x06000C8D RID: 3213 RVA: 0x0002E93C File Offset: 0x0002CB3C
		public BrokerHandlerReferenceCounter(Func<BrokerHandler> createHandler)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.handler = createHandler();
				this.handler.Subscribe();
				disposeGuard.Success();
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0002E9A0 File Offset: 0x0002CBA0
		public int Count
		{
			get
			{
				return this.associatedChannels.Count;
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002E9AD File Offset: 0x0002CBAD
		public void Add(string channelId)
		{
			this.associatedChannels.Add(channelId ?? string.Empty);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002E9C5 File Offset: 0x0002CBC5
		public void Remove(string channelId)
		{
			if (string.IsNullOrEmpty(channelId))
			{
				return;
			}
			this.associatedChannels.Remove(channelId);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002E9DD File Offset: 0x0002CBDD
		public void KeepAlive(ExDateTime eventTime)
		{
			if (this.handler != null)
			{
				this.handler.KeepAlive(eventTime);
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002E9F3 File Offset: 0x0002CBF3
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.handler != null)
			{
				this.handler.Dispose();
				this.handler = null;
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002EA12 File Offset: 0x0002CC12
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BrokerHandlerReferenceCounter>(this);
		}

		// Token: 0x040007D2 RID: 2002
		private BrokerHandler handler;

		// Token: 0x040007D3 RID: 2003
		private readonly HashSet<string> associatedChannels = new HashSet<string>();
	}
}
