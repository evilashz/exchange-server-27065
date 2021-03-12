using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000016 RID: 22
	public abstract class LogicalIndexComponentBase : LockableMailboxComponent
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000097 RID: 151
		public abstract int LogicalIndexNumber { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000519A File Offset: 0x0000339A
		public override MailboxComponentId MailboxComponentId
		{
			get
			{
				return MailboxComponentId.LogicalIndex;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000519D File Offset: 0x0000339D
		public override LockManager.LockType ReaderLockType
		{
			get
			{
				return LockManager.LockType.LogicalIndexShared;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000051A1 File Offset: 0x000033A1
		public override LockManager.LockType WriterLockType
		{
			get
			{
				return LockManager.LockType.LogicalIndexExclusive;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000051A8 File Offset: 0x000033A8
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.LogicalIndexNumber.GetHashCode();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000051CC File Offset: 0x000033CC
		public override int CompareTo(ILockName other)
		{
			int num = base.CompareTo(other);
			if (num == 0)
			{
				LogicalIndexComponentBase logicalIndexComponentBase = other as LogicalIndexComponentBase;
				num = this.LogicalIndexNumber.CompareTo(logicalIndexComponentBase.LogicalIndexNumber);
			}
			return num;
		}
	}
}
