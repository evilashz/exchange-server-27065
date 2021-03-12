using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020001A4 RID: 420
	internal abstract class ADConfigurationLoader<ADConfigType, StateType> : DisposeTrackableBase where ADConfigType : ADConfigurationObject, new()
	{
		// Token: 0x06000FFC RID: 4092
		protected abstract void LogFailure(ADConfigurationLoader<ADConfigType, StateType>.FailureLocation failureLocation, Exception exception);

		// Token: 0x06000FFD RID: 4093
		protected abstract void PreAdOperation(ref StateType state);

		// Token: 0x06000FFE RID: 4094
		protected abstract void AdOperation(ref StateType state);

		// Token: 0x06000FFF RID: 4095
		protected abstract void PostAdOperation(StateType state, bool wasSuccessful);

		// Token: 0x06001000 RID: 4096
		protected abstract void OnServerChangeCallback(ADNotificationEventArgs args);

		// Token: 0x06001001 RID: 4097 RVA: 0x000417CA File Offset: 0x0003F9CA
		protected ADConfigurationLoader() : this(TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0))
		{
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000417F0 File Offset: 0x0003F9F0
		protected ADConfigurationLoader(TimeSpan failureRetryInterval, TimeSpan periodicReadInterval)
		{
			this.failureRetryInterval = failureRetryInterval;
			this.periodicReadInterval = periodicReadInterval;
			this.periodicTimer = new GuardedTimer(new TimerCallback(this.ReadConfiguration));
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0004185C File Offset: 0x0003FA5C
		internal void ReadConfiguration()
		{
			StateType state = default(StateType);
			this.PreAdOperation(ref state);
			base.CheckDisposed();
			this.TryRegisterForADNotifications();
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				this.AdOperation(ref state);
			}, 2);
			if (adoperationResult.Succeeded)
			{
				if (this.adNotificationCookie != null)
				{
					this.UpdateTimer(this.periodicReadInterval);
				}
			}
			else
			{
				this.LogFailure(ADConfigurationLoader<ADConfigType, StateType>.FailureLocation.ADConfigurationLoading, adoperationResult.Exception);
				if (this.adNotificationCookie != null)
				{
					this.UpdateTimer(this.failureRetryInterval);
				}
			}
			this.PostAdOperation(state, adoperationResult.Succeeded);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x000418FE File Offset: 0x0003FAFE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ADConfigurationLoader<ADConfigType, StateType>>(this);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00041914 File Offset: 0x0003FB14
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.periodicTimer != null)
				{
					this.periodicTimer.Dispose(true);
					lock (this.periodicTimerLock)
					{
						this.periodicTimer = null;
					}
				}
				lock (this.notificationLock)
				{
					if (this.adNotificationCookie != null)
					{
						ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
						{
							ADNotificationAdapter.UnregisterChangeNotification(this.adNotificationCookie);
						}, 2);
						if (!adoperationResult.Succeeded)
						{
							this.LogFailure(ADConfigurationLoader<ADConfigType, StateType>.FailureLocation.ADNotificationRegistration, adoperationResult.Exception);
						}
						this.adNotificationCookie = null;
					}
					this.hasUnregisteredNotification = true;
				}
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x000419E4 File Offset: 0x0003FBE4
		private void ReadConfiguration(object obj)
		{
			this.ReadConfiguration();
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00041A08 File Offset: 0x0003FC08
		private void TryRegisterForADNotifications()
		{
			if (this.adNotificationCookie == null)
			{
				lock (this.notificationLock)
				{
					if (this.adNotificationCookie == null && !this.hasUnregisteredNotification)
					{
						ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
						{
							this.adNotificationCookie = ADNotificationAdapter.RegisterChangeNotification<ADConfigType>(null, new ADNotificationCallback(this.ServerChangeCallback));
						}, 2);
						if (!adoperationResult.Succeeded)
						{
							this.LogFailure(ADConfigurationLoader<ADConfigType, StateType>.FailureLocation.ADNotificationRegistration, adoperationResult.Exception);
							this.UpdateTimer(this.failureRetryInterval);
						}
					}
				}
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00041A98 File Offset: 0x0003FC98
		private void ServerChangeCallback(ADNotificationEventArgs args)
		{
			lock (this.notificationLock)
			{
				if (!this.hasUnregisteredNotification)
				{
					this.OnServerChangeCallback(args);
				}
			}
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00041AE4 File Offset: 0x0003FCE4
		private void UpdateTimer(TimeSpan interval)
		{
			lock (this.periodicTimerLock)
			{
				if (this.periodicTimer != null)
				{
					this.periodicTimer.Continue(interval, interval);
				}
			}
		}

		// Token: 0x04000887 RID: 2183
		private const int DefaultFailureRetryMinutes = 1;

		// Token: 0x04000888 RID: 2184
		private const int DefaultPeriodicReadMinutes = 15;

		// Token: 0x04000889 RID: 2185
		private ADNotificationRequestCookie adNotificationCookie;

		// Token: 0x0400088A RID: 2186
		private object notificationLock = new object();

		// Token: 0x0400088B RID: 2187
		private GuardedTimer periodicTimer;

		// Token: 0x0400088C RID: 2188
		private object periodicTimerLock = new object();

		// Token: 0x0400088D RID: 2189
		private TimeSpan failureRetryInterval;

		// Token: 0x0400088E RID: 2190
		private TimeSpan periodicReadInterval;

		// Token: 0x0400088F RID: 2191
		private bool hasUnregisteredNotification;

		// Token: 0x020001A5 RID: 421
		protected enum FailureLocation
		{
			// Token: 0x04000891 RID: 2193
			ADNotificationRegistration,
			// Token: 0x04000892 RID: 2194
			ADConfigurationLoading
		}
	}
}
