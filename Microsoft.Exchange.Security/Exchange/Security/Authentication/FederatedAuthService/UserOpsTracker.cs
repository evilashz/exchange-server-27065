using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200008C RID: 140
	internal class UserOpsTracker
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x000280B2 File Offset: 0x000262B2
		public UserOpsTracker()
		{
			this.userOperations = new Dictionary<string, UserOp>(100);
			this.readerWriterLock = new FastReaderWriterLock();
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000280D4 File Offset: 0x000262D4
		public UserOp GetOperation(string user)
		{
			UserOp userOp = null;
			bool flag = false;
			this.readerWriterLock.AcquireReaderLock(-1);
			try
			{
				this.userOperations.TryGetValue(user, out userOp);
				if (userOp == null)
				{
					this.readerWriterLock.ReleaseReaderLock();
					this.readerWriterLock.AcquireWriterLock(-1);
					flag = true;
					this.userOperations.TryGetValue(user, out userOp);
					if (userOp == null)
					{
						userOp = new UserOp();
						userOp.User = user;
						this.userOperations.Add(user, userOp);
					}
				}
			}
			finally
			{
				Interlocked.Increment(ref userOp.refCount);
				if (flag)
				{
					this.readerWriterLock.ReleaseWriterLock();
				}
				else
				{
					this.readerWriterLock.ReleaseReaderLock();
				}
			}
			return userOp;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00028184 File Offset: 0x00026384
		public void ReleaseOperation(UserOp clientOp)
		{
			if (Interlocked.Decrement(ref clientOp.refCount) == 0)
			{
				this.readerWriterLock.AcquireWriterLock(-1);
				try
				{
					if (clientOp.refCount == 0)
					{
						this.userOperations.Remove(clientOp.User);
						if (clientOp.HrdEvent != null)
						{
							clientOp.HrdEvent.Close();
						}
						if (clientOp.StsEvent != null)
						{
							clientOp.StsEvent.Close();
						}
					}
				}
				finally
				{
					this.readerWriterLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x04000539 RID: 1337
		private FastReaderWriterLock readerWriterLock;

		// Token: 0x0400053A RID: 1338
		private Dictionary<string, UserOp> userOperations;
	}
}
