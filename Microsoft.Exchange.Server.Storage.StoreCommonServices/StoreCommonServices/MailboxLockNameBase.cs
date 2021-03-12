using System;
using System.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000B3 RID: 179
	public abstract class MailboxLockNameBase : IMailboxLockName, ILockName, IEquatable<ILockName>, IComparable<ILockName>
	{
		// Token: 0x060006F0 RID: 1776 RVA: 0x00024085 File Offset: 0x00022285
		public MailboxLockNameBase(Guid databaseGuid, int mailboxPartitionNumber)
		{
			this.mailboxPartitionNumber = mailboxPartitionNumber;
			this.hashCode = (mailboxPartitionNumber.GetHashCode() ^ databaseGuid.GetHashCode());
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x000240AF File Offset: 0x000222AF
		public LockManager.LockLevel LockLevel
		{
			get
			{
				return LockManager.LockLevel.Mailbox;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x000240B2 File Offset: 0x000222B2
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x000240BA File Offset: 0x000222BA
		public virtual LockManager.NamedLockObject CachedLockObject
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

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x000240C3 File Offset: 0x000222C3
		public int MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006F5 RID: 1781
		public abstract Guid DatabaseGuid { get; }

		// Token: 0x060006F6 RID: 1782 RVA: 0x000240CB File Offset: 0x000222CB
		public static IMailboxLockName GetMailboxLockName(Guid databaseGuid, int mailboxPartitionNumber)
		{
			return new MailboxLockName(databaseGuid, mailboxPartitionNumber);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000240D4 File Offset: 0x000222D4
		public static bool IsMailboxLocked(Guid databaseGuid, int mailboxPartitionNumber, bool shared)
		{
			if (shared)
			{
				return LockManager.TestLock(MailboxLockNameBase.GetMailboxLockName(databaseGuid, mailboxPartitionNumber), LockManager.LockType.MailboxShared);
			}
			return LockManager.TestLock(MailboxLockNameBase.GetMailboxLockName(databaseGuid, mailboxPartitionNumber), LockManager.LockType.MailboxExclusive);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000240F6 File Offset: 0x000222F6
		[Conditional("DEBUG")]
		public static void AssertMailboxLocked(Guid databaseGuid, int mailboxPartitionNumber)
		{
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000240F8 File Offset: 0x000222F8
		[Conditional("DEBUG")]
		public static void AssertMailboxLocked(Guid databaseGuid, int mailboxPartitionNumber, bool shared)
		{
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000240FA File Offset: 0x000222FA
		[Conditional("DEBUG")]
		public static void AssertMailboxNotLocked(Guid databaseGuid, int mailboxPartitionNumber)
		{
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000240FC File Offset: 0x000222FC
		[Conditional("DEBUG")]
		public static void AssertNoMailboxLocked()
		{
		}

		// Token: 0x060006FC RID: 1788
		public abstract ILockName GetLockNameToCache();

		// Token: 0x060006FD RID: 1789
		public abstract string GetFriendlyNameForLogging();

		// Token: 0x060006FE RID: 1790 RVA: 0x000240FE File Offset: 0x000222FE
		public bool Equals(ILockName other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0002410C File Offset: 0x0002230C
		public virtual int CompareTo(ILockName other)
		{
			int num = ((int)this.LockLevel).CompareTo((int)other.LockLevel);
			if (num == 0)
			{
				MailboxLockNameBase mailboxLockNameBase = other as MailboxLockNameBase;
				num = this.DatabaseGuid.CompareTo(mailboxLockNameBase.DatabaseGuid);
				if (num == 0)
				{
					num = this.mailboxPartitionNumber.CompareTo(mailboxLockNameBase.MailboxPartitionNumber);
				}
			}
			return num;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00024167 File Offset: 0x00022367
		public override bool Equals(object other)
		{
			return this.Equals(other as ILockName);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00024175 File Offset: 0x00022375
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00024180 File Offset: 0x00022380
		public override string ToString()
		{
			return "DB " + this.DatabaseGuid.ToString() + "/MBX " + this.MailboxPartitionNumber.ToString();
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000241BE File Offset: 0x000223BE
		public bool IsMailboxLocked()
		{
			return LockManager.TestLock(this, LockManager.LockType.MailboxExclusive) || LockManager.TestLock(this, LockManager.LockType.MailboxShared);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000241D4 File Offset: 0x000223D4
		public bool IsMailboxLockedExclusively()
		{
			return LockManager.TestLock(this, LockManager.LockType.MailboxExclusive);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000241DE File Offset: 0x000223DE
		public bool IsMailboxSharedLockHeld()
		{
			return LockManager.TestLock(this, LockManager.LockType.MailboxShared);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000241E8 File Offset: 0x000223E8
		[Conditional("DEBUG")]
		public void AssertMailboxLocked()
		{
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000241EA File Offset: 0x000223EA
		[Conditional("DEBUG")]
		public void AssertMailboxExclusiveLockHeld()
		{
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000241EC File Offset: 0x000223EC
		[Conditional("DEBUG")]
		public void AssertMailboxLocked(bool shared)
		{
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000241F0 File Offset: 0x000223F0
		[Conditional("DEBUG")]
		public void AssertMailboxNotLocked()
		{
		}

		// Token: 0x0400043C RID: 1084
		private readonly int hashCode;

		// Token: 0x0400043D RID: 1085
		private readonly int mailboxPartitionNumber;

		// Token: 0x0400043E RID: 1086
		private LockManager.NamedLockObject cachedLockObject;
	}
}
