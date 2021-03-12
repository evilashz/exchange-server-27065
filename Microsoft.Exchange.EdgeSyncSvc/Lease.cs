using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.MessageSecurity.EdgeSync;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000007 RID: 7
	internal class Lease
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003740 File Offset: 0x00001940
		public Lease(string leaseHolderPath, int leaseHolderVersion, EdgeSyncLogSession logSession, int leaseLockTryCount, TestShutdownDelegate testShutdown)
		{
			this.leaseHolderPath = leaseHolderPath;
			this.leaseHolderVersion = leaseHolderVersion;
			this.logSession = logSession;
			this.leaseLockTryCount = leaseLockTryCount;
			this.testShutdown = testShutdown;
			this.currentLeaseToken = LeaseToken.Empty;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003778 File Offset: 0x00001978
		public string LeaseHeldBy
		{
			get
			{
				return this.currentLeaseToken.Path;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003788 File Offset: 0x00001988
		public bool TryAddLock(TargetConnection connection, SynchronizationProvider provider, LeaseAcquisitionMode mode)
		{
			bool result;
			lock (this)
			{
				bool flag2 = this.InternalTryLock(connection, provider, mode);
				if (flag2)
				{
					this.currentLocks++;
					ExTraceGlobals.ConnectionTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Lock to {0} on {1}", this.currentLocks, connection.Host);
				}
				else
				{
					ExTraceGlobals.ConnectionTracer.TraceDebug<int, string>((long)this.GetHashCode(), "failed to lock at {0} on {1}", this.currentLocks, connection.Host);
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003824 File Offset: 0x00001A24
		public bool TryRefreshLock(TargetConnection connection, SynchronizationProvider provider)
		{
			bool result;
			lock (this)
			{
				result = this.InternalTryLock(connection, provider, LeaseAcquisitionMode.RespectOptions);
			}
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003864 File Offset: 0x00001A64
		public void ReleaseLock(TargetConnection connection, SynchronizationProvider provider)
		{
			lock (this)
			{
				if (this.currentLocks <= 0)
				{
					throw new InvalidOperationException("releasing a lock with count zero");
				}
				if (--this.currentLocks == 0)
				{
					try
					{
						if (this.IsLocallyOwnedLease())
						{
							this.SetLease(connection, provider, LeaseTokenType.Option);
							ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Release lock on {0}", connection.Host);
						}
						goto IL_87;
					}
					catch (ExDirectoryException)
					{
						goto IL_87;
					}
				}
				ExTraceGlobals.ConnectionTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Silent release lock at {0} on {1}", this.currentLocks, connection.Host);
				IL_87:;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003920 File Offset: 0x00001B20
		private bool InternalTryLock(TargetConnection connection, SynchronizationProvider provider, LeaseAcquisitionMode mode)
		{
			if (this.IsLocallyOwnedLeaseLock())
			{
				return true;
			}
			ExDirectoryException ex = null;
			int i = 0;
			while (i < this.leaseLockTryCount)
			{
				try
				{
					this.currentLeaseToken = connection.GetLease();
					if (this.IsLocallyOwnedLeaseLock())
					{
						return true;
					}
					if (!this.IsLocallyOwnedLease() && !this.CanTakeoverNonLocalLease(connection, mode))
					{
						ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Lease: CanTakeoverLease false on {0}", connection.Host);
						this.logSession.LogLeaseHeld(this.currentLeaseToken.StringForm);
						return false;
					}
					LeaseToken leaseToken = this.currentLeaseToken;
					this.SetLease(connection, provider, LeaseTokenType.Lock);
					if (string.Equals(this.currentLeaseToken.Path, leaseToken.Path, StringComparison.OrdinalIgnoreCase))
					{
						this.logSession.LogLeaseRefresh(leaseToken.StringForm, this.currentLeaseToken.StringForm);
					}
					else
					{
						this.logSession.LogLeaseTakeover(leaseToken.StringForm, this.currentLeaseToken.StringForm);
					}
					return true;
				}
				catch (ExDirectoryException ex2)
				{
					ex = ex2;
				}
				if (!this.testShutdown())
				{
					Thread.Sleep(100);
					i++;
					continue;
				}
				ExTraceGlobals.ConnectionTracer.TraceDebug((long)this.GetHashCode(), "Quit getting lease because the service is shutting down");
				return false;
			}
			ExTraceGlobals.ConnectionTracer.TraceError<string, ExDirectoryException>((long)this.GetHashCode(), "Failed to lock {0} because of {1}", connection.Host, ex);
			this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, ex, "Failed to lock " + connection.Host);
			return false;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003AA8 File Offset: 0x00001CA8
		private void SetLease(TargetConnection connection, SynchronizationProvider provider, LeaseTokenType type)
		{
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime;
			switch (type)
			{
			case LeaseTokenType.Lock:
				dateTime = utcNow + EdgeSyncSvc.EdgeSync.Config.ServiceConfig.LockDuration;
				break;
			case LeaseTokenType.Option:
				dateTime = utcNow + EdgeSyncSvc.EdgeSync.Config.ServiceConfig.OptionDuration;
				break;
			default:
				throw new InvalidOperationException("lease token type");
			}
			EnhancedTimeSpan enhancedTimeSpan;
			if (provider.RecipientSyncInterval < provider.ConfigurationSyncInterval)
			{
				enhancedTimeSpan = provider.ConfigurationSyncInterval;
			}
			else
			{
				enhancedTimeSpan = provider.RecipientSyncInterval;
			}
			LeaseToken lease = new LeaseToken(this.leaseHolderPath, dateTime, type, utcNow, dateTime + enhancedTimeSpan + TimeSpan.FromMinutes(5.0), this.leaseHolderVersion);
			connection.SetLease(lease);
			this.currentLeaseToken = lease;
			if (type == LeaseTokenType.Lock)
			{
				ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Update Lease as Lock till {0}", lease.Expiry.ToString());
				return;
			}
			ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Update Lease as Option till {0}", dateTime.ToString());
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003BD8 File Offset: 0x00001DD8
		private bool CanTakeoverNonLocalLease(TargetConnection connection, LeaseAcquisitionMode acquisionMode)
		{
			bool force = acquisionMode == LeaseAcquisitionMode.OverrideOptions && this.currentLeaseToken.Type == LeaseTokenType.Option;
			return connection.CanTakeOverLease(force, this.currentLeaseToken, DateTime.UtcNow);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003C14 File Offset: 0x00001E14
		private bool IsLocallyOwnedLeaseLock()
		{
			return this.IsLocallyOwnedLease() && this.currentLeaseToken.Type == LeaseTokenType.Lock && this.currentLeaseToken.Expiry - DateTime.UtcNow > EdgeSyncSvc.EdgeSync.Config.ServiceConfig.LockRenewalDuration;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003C66 File Offset: 0x00001E66
		private bool IsLocallyOwnedLease()
		{
			return !this.currentLeaseToken.NotHeld && DistinguishedName.Equals(this.currentLeaseToken.Path, this.leaseHolderPath);
		}

		// Token: 0x04000021 RID: 33
		private int leaseLockTryCount;

		// Token: 0x04000022 RID: 34
		private LeaseToken currentLeaseToken;

		// Token: 0x04000023 RID: 35
		private int currentLocks;

		// Token: 0x04000024 RID: 36
		private string leaseHolderPath;

		// Token: 0x04000025 RID: 37
		private int leaseHolderVersion;

		// Token: 0x04000026 RID: 38
		private EdgeSyncLogSession logSession;

		// Token: 0x04000027 RID: 39
		private TestShutdownDelegate testShutdown;
	}
}
