using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000003 RID: 3
	public class CachedDirectory : IDirectory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020D0 File Offset: 0x000002D0
		public CachedDirectory(IDirectory wrappedDirectory)
		{
			if (wrappedDirectory == null)
			{
				throw new ArgumentNullException("wrappedDirectory");
			}
			this.wrappedDirectory = wrappedDirectory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		internal static CachedDirectory DefaultCachedDirectory
		{
			get
			{
				if (CachedDirectory.defaultCachedDirectory == null)
				{
					lock (CachedDirectory.defaultObjectLock)
					{
						if (CachedDirectory.defaultCachedDirectory == null)
						{
							CachedDirectory.defaultCachedDirectory = new CachedDirectory(new Directory());
						}
					}
				}
				return CachedDirectory.defaultCachedDirectory;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002160 File Offset: 0x00000360
		public SecurityIdentifier GetExchangeServersUsgSid()
		{
			if (this.exchangeServersUsgSid == null)
			{
				this.exchangeServersUsgSid = this.wrappedDirectory.GetExchangeServersUsgSid();
			}
			return this.exchangeServersUsgSid;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002188 File Offset: 0x00000388
		public bool AllowsTokenSerializationBy(WindowsIdentity windowsIdentity)
		{
			if (windowsIdentity == null)
			{
				throw new ArgumentNullException("windowsIdentity");
			}
			SecurityIdentifier user = windowsIdentity.User;
			try
			{
				this.syncLock.EnterReadLock();
				if (this.verifiedCallers.Contains(user))
				{
					return true;
				}
			}
			finally
			{
				try
				{
					this.syncLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			bool flag = this.wrappedDirectory.AllowsTokenSerializationBy(windowsIdentity);
			if (flag)
			{
				try
				{
					this.syncLock.EnterWriteLock();
					this.verifiedCallers.Add(user);
				}
				finally
				{
					try
					{
						this.syncLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x04000001 RID: 1
		private static CachedDirectory defaultCachedDirectory = null;

		// Token: 0x04000002 RID: 2
		private static object defaultObjectLock = new object();

		// Token: 0x04000003 RID: 3
		private IDirectory wrappedDirectory;

		// Token: 0x04000004 RID: 4
		private SecurityIdentifier exchangeServersUsgSid;

		// Token: 0x04000005 RID: 5
		private HashSet<SecurityIdentifier> verifiedCallers = new HashSet<SecurityIdentifier>();

		// Token: 0x04000006 RID: 6
		private ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();
	}
}
