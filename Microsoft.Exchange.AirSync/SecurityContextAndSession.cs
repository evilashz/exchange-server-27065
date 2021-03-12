using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000248 RID: 584
	internal class SecurityContextAndSession : IDisposable
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x0007C880 File Offset: 0x0007AA80
		public SecurityContextAndSession(ClientSecurityContextWrapper wrapper, MailboxSession session)
		{
			if (wrapper == null)
			{
				throw new ArgumentNullException("wrapper");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			this.SecurityContextWrapper = wrapper;
			this.MailboxSession = session;
			this.SecurityContextWrapper.AddRef();
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0007C8D4 File Offset: 0x0007AAD4
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x0007C8DC File Offset: 0x0007AADC
		public ClientSecurityContextWrapper SecurityContextWrapper { get; private set; }

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0007C8E5 File Offset: 0x0007AAE5
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x0007C8ED File Offset: 0x0007AAED
		public MailboxSession MailboxSession { get; private set; }

		// Token: 0x0600154E RID: 5454 RVA: 0x0007C8F6 File Offset: 0x0007AAF6
		public void Dispose()
		{
			this.InternalDispose(true);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0007C900 File Offset: 0x0007AB00
		private void InternalDispose(bool fromDispose)
		{
			if (fromDispose && !this.disposed)
			{
				lock (this.lockObject)
				{
					if (!this.disposed)
					{
						try
						{
							this.MailboxSession.Dispose();
							this.MailboxSession = null;
						}
						finally
						{
							this.SecurityContextWrapper.Dispose();
							this.SecurityContextWrapper = null;
							this.disposed = true;
						}
					}
				}
			}
		}

		// Token: 0x04000C97 RID: 3223
		private bool disposed;

		// Token: 0x04000C98 RID: 3224
		private object lockObject = new object();
	}
}
