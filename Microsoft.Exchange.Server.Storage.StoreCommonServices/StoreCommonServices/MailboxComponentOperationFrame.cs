using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000A8 RID: 168
	public struct MailboxComponentOperationFrame : IDisposable
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x00023C93 File Offset: 0x00021E93
		public static MailboxComponentOperationFrame CreateWrite(Context context, LockableMailboxComponent lockableMailboxComponent)
		{
			return new MailboxComponentOperationFrame(context, lockableMailboxComponent, true);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00023C9D File Offset: 0x00021E9D
		public static MailboxComponentOperationFrame CreateRead(Context context, LockableMailboxComponent lockableMailboxComponent)
		{
			return new MailboxComponentOperationFrame(context, lockableMailboxComponent, false);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00023CA7 File Offset: 0x00021EA7
		private MailboxComponentOperationFrame(Context context, LockableMailboxComponent lockableMailboxComponent, bool writeOperation)
		{
			this.context = context;
			this.lockableMailboxComponent = lockableMailboxComponent;
			this.writeOperation = writeOperation;
			this.success = false;
			if (writeOperation)
			{
				context.StartMailboxComponentWriteOperation(lockableMailboxComponent);
				this.locked = true;
				return;
			}
			context.StartMailboxComponentReadOperation(lockableMailboxComponent);
			this.locked = true;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00023CE5 File Offset: 0x00021EE5
		public void Success()
		{
			this.success = true;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00023CEE File Offset: 0x00021EEE
		public void Dispose()
		{
			if (this.locked)
			{
				if (this.writeOperation)
				{
					this.context.EndMailboxComponentWriteOperation(this.lockableMailboxComponent, this.success);
					return;
				}
				this.context.EndMailboxComponentReadOperation(this.lockableMailboxComponent);
			}
		}

		// Token: 0x0400042F RID: 1071
		private readonly LockableMailboxComponent lockableMailboxComponent;

		// Token: 0x04000430 RID: 1072
		private readonly Context context;

		// Token: 0x04000431 RID: 1073
		private readonly bool writeOperation;

		// Token: 0x04000432 RID: 1074
		private readonly bool locked;

		// Token: 0x04000433 RID: 1075
		private bool success;
	}
}
