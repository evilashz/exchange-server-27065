using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000023 RID: 35
	public abstract class LockableMailboxComponent : ILockName, IEquatable<ILockName>, IComparable<ILockName>
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000109F5 File Offset: 0x0000EBF5
		public LockManager.LockLevel LockLevel
		{
			get
			{
				return LockManager.LockLevelFromLockType(this.ReaderLockType);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00010A02 File Offset: 0x0000EC02
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00010A0A File Offset: 0x0000EC0A
		public LockManager.NamedLockObject CachedLockObject
		{
			get
			{
				return this.cachedLockObject;
			}
			set
			{
				this.cachedLockObject = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000160 RID: 352
		public abstract int MailboxPartitionNumber { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000161 RID: 353
		public abstract Guid DatabaseGuid { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00010A13 File Offset: 0x0000EC13
		public virtual LockManager.LockType ReaderLockType
		{
			get
			{
				return LockManager.LockType.MailboxComponentsShared;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00010A17 File Offset: 0x0000EC17
		public virtual LockManager.LockType WriterLockType
		{
			get
			{
				return LockManager.LockType.MailboxComponentsExclusive;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00010A1B File Offset: 0x0000EC1B
		public virtual bool Committable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000165 RID: 357
		public abstract MailboxComponentId MailboxComponentId { get; }

		// Token: 0x06000166 RID: 358 RVA: 0x00010A1E File Offset: 0x0000EC1E
		[Conditional("DEBUG")]
		public void AssertSharedLockHeld()
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00010A20 File Offset: 0x0000EC20
		[Conditional("DEBUG")]
		public void AssertExclusiveLockHeld()
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00010A22 File Offset: 0x0000EC22
		public virtual ILockName GetLockNameToCache()
		{
			return new MailboxComponentLockName(this.ReaderLockType, this.MailboxComponentId, this.MailboxPartitionNumber, this.DatabaseGuid);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00010A41 File Offset: 0x0000EC41
		public virtual bool TestSharedLock()
		{
			return LockManager.TestLock(this, this.ReaderLockType);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00010A4F File Offset: 0x0000EC4F
		public virtual bool TestExclusiveLock()
		{
			return LockManager.TestLock(this, this.WriterLockType);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00010A5D File Offset: 0x0000EC5D
		public virtual void LockShared(ILockStatistics lockStats)
		{
			LockManager.GetLock(this, this.ReaderLockType, lockStats);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00010A6C File Offset: 0x0000EC6C
		public virtual void ReleaseShared()
		{
			LockManager.ReleaseLock(this, this.ReaderLockType);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00010A7A File Offset: 0x0000EC7A
		public virtual void LockExclusive(ILockStatistics lockStats)
		{
			LockManager.GetLock(this, this.WriterLockType, lockStats);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00010A89 File Offset: 0x0000EC89
		public virtual void ReleaseExclusive()
		{
			LockManager.ReleaseLock(this, this.WriterLockType);
		}

		// Token: 0x0600016F RID: 367
		public abstract bool IsValidTableOperation(Context context, Connection.OperationType operationType, Table table, IList<object> partitionValues);

		// Token: 0x06000170 RID: 368 RVA: 0x00010A97 File Offset: 0x0000EC97
		public override bool Equals(object other)
		{
			return this.Equals(other as ILockName);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00010AA5 File Offset: 0x0000ECA5
		public virtual bool Equals(ILockName other)
		{
			return other != null && this.CompareTo(other) == 0;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00010AB8 File Offset: 0x0000ECB8
		public virtual int CompareTo(ILockName other)
		{
			int num = ((int)this.LockLevel).CompareTo((int)other.LockLevel);
			if (num == 0)
			{
				LockableMailboxComponent lockableMailboxComponent = other as LockableMailboxComponent;
				num = this.DatabaseGuid.CompareTo(lockableMailboxComponent.DatabaseGuid);
				if (num == 0)
				{
					num = this.MailboxPartitionNumber.CompareTo(lockableMailboxComponent.MailboxPartitionNumber);
					if (num == 0)
					{
						num = ((int)this.MailboxComponentId).CompareTo((int)lockableMailboxComponent.MailboxComponentId);
					}
				}
			}
			return num;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00010B2C File Offset: 0x0000ED2C
		public override int GetHashCode()
		{
			return (int)(this.LockLevel ^ (LockManager.LockLevel)this.MailboxComponentId ^ (LockManager.LockLevel)this.MailboxPartitionNumber.GetHashCode());
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00010B55 File Offset: 0x0000ED55
		public override string ToString()
		{
			return "MailboxComponent " + this.MailboxComponentId.ToString() + "/" + this.LockLevel.ToString();
		}

		// Token: 0x040001ED RID: 493
		private LockManager.NamedLockObject cachedLockObject;
	}
}
