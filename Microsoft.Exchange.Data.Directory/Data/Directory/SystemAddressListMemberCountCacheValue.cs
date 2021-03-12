using System;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000656 RID: 1622
	internal class SystemAddressListMemberCountCacheValue
	{
		// Token: 0x06004C04 RID: 19460 RVA: 0x00118AE4 File Offset: 0x00116CE4
		internal SystemAddressListMemberCountCacheValue(AddressBookBase systemAddressList)
		{
			this.cachedCountLock = new ReaderWriterLockSlim();
			if (systemAddressList == null)
			{
				this.systemAddressListExists = false;
				this.memberCount = 0;
				this.lifetime = TimeSpan.MaxValue;
				return;
			}
			this.systemAddressListGuid = systemAddressList.Guid;
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x00118B50 File Offset: 0x00116D50
		internal int InitializeMemberCount(IConfigurationSession session, ExDateTime now, int quota)
		{
			try
			{
				if (!this.cachedCountLock.TryEnterReadLock(SystemAddressListMemberCountCacheValue.readerLockTimeout))
				{
					throw new TransientException(DirectoryStrings.ErrorTimeoutReadingSystemAddressListMemberCount);
				}
				if (!this.systemAddressListExists)
				{
					return 0;
				}
			}
			finally
			{
				try
				{
					this.cachedCountLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			SystemAddressListMemberCountCacheValue.UpdateInfo o = new SystemAddressListMemberCountCacheValue.UpdateInfo(session, now, quota);
			Interlocked.Increment(ref this.asyncUpdatesInProgress);
			this.UpdateMemberCount(o);
			this.ThrowAnySavedExceptionsFromAsyncThreads();
			return this.memberCount;
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x00118BE0 File Offset: 0x00116DE0
		internal int GetMemberCount(IConfigurationSession session, ExDateTime now, int quota)
		{
			int result = 0;
			this.ThrowAnySavedExceptionsFromAsyncThreads();
			try
			{
				if (!this.cachedCountLock.TryEnterReadLock(SystemAddressListMemberCountCacheValue.readerLockTimeout))
				{
					throw new TransientException(DirectoryStrings.ErrorTimeoutReadingSystemAddressListMemberCount);
				}
				if (this.systemAddressListExists && this.lastQueriedTime + this.lifetime < now)
				{
					SystemAddressListMemberCountCacheValue.UpdateInfo updateInfo = new SystemAddressListMemberCountCacheValue.UpdateInfo(session, now, quota);
					this.StartAsyncUpdate(updateInfo);
				}
				result = this.memberCount;
			}
			finally
			{
				try
				{
					this.cachedCountLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x00118C7C File Offset: 0x00116E7C
		internal int GetMemberCountImmediate(IConfigurationSession session)
		{
			int num = 0;
			try
			{
				if (!this.cachedCountLock.TryEnterReadLock(SystemAddressListMemberCountCacheValue.readerLockTimeout))
				{
					throw new TransientException(DirectoryStrings.ErrorTimeoutReadingSystemAddressListMemberCount);
				}
				if (!this.systemAddressListExists)
				{
					throw new ADNoSuchObjectException(DirectoryStrings.SystemAddressListDoesNotExist);
				}
				num = AddressBookBase.GetAddressListSize(session, this.systemAddressListGuid);
				if (num == -1)
				{
					throw new ADNoSuchObjectException(DirectoryStrings.SystemAddressListDoesNotExist);
				}
			}
			finally
			{
				try
				{
					this.cachedCountLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return num;
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x00118D08 File Offset: 0x00116F08
		private void StartAsyncUpdate(SystemAddressListMemberCountCacheValue.UpdateInfo updateInfo)
		{
			if (this.asyncUpdatesInProgress == 0)
			{
				Interlocked.Increment(ref this.asyncUpdatesInProgress);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.UpdateMemberCount), updateInfo);
			}
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x00118D34 File Offset: 0x00116F34
		private void UpdateMemberCount(object o)
		{
			try
			{
				SystemAddressListMemberCountCacheValue.UpdateInfo updateInfo = (SystemAddressListMemberCountCacheValue.UpdateInfo)o;
				Exception ex = null;
				int num = 0;
				try
				{
					num = AddressBookBase.GetAddressListSize(updateInfo.Session, this.systemAddressListGuid);
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				try
				{
					this.cachedCountLock.EnterWriteLock();
					if (ex != null)
					{
						this.asyncException = ex;
					}
					else if (num == -1)
					{
						this.systemAddressListExists = false;
						this.memberCount = 0;
						this.lastQueriedTime = updateInfo.Now;
						this.lifetime = TimeSpan.MaxValue;
					}
					else
					{
						this.memberCount = num;
						this.lastQueriedTime = updateInfo.Now;
						this.lifetime = this.CalculateValidLifetime(updateInfo.Quota);
					}
				}
				finally
				{
					try
					{
						this.cachedCountLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.asyncUpdatesInProgress);
			}
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x00118E24 File Offset: 0x00117024
		private void ThrowAnySavedExceptionsFromAsyncThreads()
		{
			try
			{
				if (!this.cachedCountLock.TryEnterUpgradeableReadLock(SystemAddressListMemberCountCacheValue.readerLockTimeout))
				{
					throw new TransientException(DirectoryStrings.ErrorTimeoutReadingSystemAddressListMemberCount);
				}
				if (this.asyncException != null)
				{
					try
					{
						if (!this.cachedCountLock.TryEnterWriteLock(SystemAddressListMemberCountCacheValue.writerLockTimeout))
						{
							throw new TransientException(DirectoryStrings.ErrorTimeoutWritingSystemAddressListMemberCount);
						}
						Exception ex = this.asyncException;
						this.asyncException = null;
						throw ex;
					}
					finally
					{
						try
						{
							this.cachedCountLock.ExitWriteLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
			}
			finally
			{
				try
				{
					this.cachedCountLock.ExitUpgradeableReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x00118EDC File Offset: 0x001170DC
		private TimeSpan CalculateValidLifetime(int quota)
		{
			double num = (T)ThrottlingPolicyDefaults.ExchangeMaxCmdlets / (T)ThrottlingPolicyDefaults.PowerShellMaxCmdletsTimePeriod;
			double num2 = (double)(quota - this.memberCount);
			if (num2 < 0.0)
			{
				num2 = 0.0;
			}
			int num3 = Convert.ToInt32(num2 / num);
			if (num3 < 30)
			{
				return TimeSpan.FromSeconds(0.0);
			}
			if (num3 > 300)
			{
				return TimeSpan.FromSeconds(300.0);
			}
			return TimeSpan.FromSeconds((double)num3);
		}

		// Token: 0x04003425 RID: 13349
		private static readonly TimeSpan readerLockTimeout = TimeSpan.FromSeconds(120.0);

		// Token: 0x04003426 RID: 13350
		private static readonly TimeSpan writerLockTimeout = TimeSpan.FromSeconds(300.0);

		// Token: 0x04003427 RID: 13351
		private Guid systemAddressListGuid;

		// Token: 0x04003428 RID: 13352
		private int memberCount = int.MinValue;

		// Token: 0x04003429 RID: 13353
		private ExDateTime lastQueriedTime = (ExDateTime)DateTime.MinValue;

		// Token: 0x0400342A RID: 13354
		private TimeSpan lifetime;

		// Token: 0x0400342B RID: 13355
		private bool systemAddressListExists = true;

		// Token: 0x0400342C RID: 13356
		private ReaderWriterLockSlim cachedCountLock;

		// Token: 0x0400342D RID: 13357
		private int asyncUpdatesInProgress;

		// Token: 0x0400342E RID: 13358
		private Exception asyncException;

		// Token: 0x02000657 RID: 1623
		private class UpdateInfo
		{
			// Token: 0x06004C0D RID: 19469 RVA: 0x00118F86 File Offset: 0x00117186
			internal UpdateInfo(IConfigurationSession session, ExDateTime now, int quota)
			{
				this.Session = session;
				this.Now = now;
				this.Quota = quota;
			}

			// Token: 0x0400342F RID: 13359
			internal IConfigurationSession Session;

			// Token: 0x04003430 RID: 13360
			internal ExDateTime Now;

			// Token: 0x04003431 RID: 13361
			internal int Quota;
		}
	}
}
