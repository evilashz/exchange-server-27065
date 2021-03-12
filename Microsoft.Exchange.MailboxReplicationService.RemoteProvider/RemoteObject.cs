using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	internal abstract class RemoteObject : DisposeTrackableBase
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000021B0 File Offset: 0x000003B0
		protected RemoteObject(IMailboxReplicationProxyService mrsProxy, long handle)
		{
			this.mrsProxy = mrsProxy;
			this.handle = handle;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000021C6 File Offset: 0x000003C6
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000021CE File Offset: 0x000003CE
		public long Handle
		{
			get
			{
				return this.handle;
			}
			protected set
			{
				this.handle = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000021D7 File Offset: 0x000003D7
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000021DF File Offset: 0x000003DF
		public IMailboxReplicationProxyService MrsProxy
		{
			get
			{
				return this.mrsProxy;
			}
			protected set
			{
				this.mrsProxy = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021E8 File Offset: 0x000003E8
		public VersionInformation ServerVersion
		{
			get
			{
				return ((MailboxReplicationProxyClient)this.mrsProxy).ServerVersion;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021FA File Offset: 0x000003FA
		public MailboxReplicationProxyClient MrsProxyClient
		{
			get
			{
				return (MailboxReplicationProxyClient)this.mrsProxy;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000221C File Offset: 0x0000041C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.mrsProxy != null)
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					this.mrsProxy.CloseHandle(this.handle);
				}, null);
				this.mrsProxy = null;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002254 File Offset: 0x00000454
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RemoteObject>(this);
		}

		// Token: 0x04000001 RID: 1
		private IMailboxReplicationProxyService mrsProxy;

		// Token: 0x04000002 RID: 2
		private long handle;
	}
}
