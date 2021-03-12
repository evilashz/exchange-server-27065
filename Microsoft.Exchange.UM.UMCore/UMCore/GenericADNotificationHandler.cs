using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000146 RID: 326
	internal abstract class GenericADNotificationHandler<T> : ADNotificationHandler where T : ADConfigurationObject, new()
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x0002729C File Offset: 0x0002549C
		internal GenericADNotificationHandler()
		{
			this.Register();
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x000272AA File Offset: 0x000254AA
		private IConfigurationSession ConfigSession
		{
			get
			{
				return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 58, "ConfigSession", "f:\\15.00.1497\\sources\\dev\\um\\src\\umcore\\GenericADNotificationHandler.cs");
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000272C8 File Offset: 0x000254C8
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					ADNotificationHandler.DebugTrace("{0}.Dispose() called", new object[]
					{
						base.GetType().Name
					});
					lock (this)
					{
						this.EnsureTimerDisposed();
						if (this.notifRequest != null)
						{
							ADNotificationHandler.DebugTrace("{0}.Dispose: UnregisterChangeNotification", new object[]
							{
								base.GetType().Name
							});
							try
							{
								ADNotificationAdapter.UnregisterChangeNotification(this.notifRequest);
							}
							catch (ADTransientException ex)
							{
								ADNotificationHandler.ErrorTrace("{0}.Dispose: {1}", new object[]
								{
									base.GetType().Name,
									ex
								});
							}
							this.notifRequest = null;
						}
					}
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000273B8 File Offset: 0x000255B8
		protected void Register()
		{
			this.EnsureTimerDisposed();
			try
			{
				ADNotificationHandler.DebugTrace("GenericADNotificationHandler.Register()", new object[0]);
				this.notifRequest = ADNotificationAdapter.RegisterChangeNotification<T>(this.ConfigSession.GetOrgContainerId(), new ADNotificationCallback(this.InternalConfigChanged), this);
				base.FireConfigChangedEvent(null);
			}
			catch (ADTransientException ex)
			{
				TimeSpan adnotificationsRetryTime = Constants.ADNotificationsRetryTime;
				this.LogRegistrationError(adnotificationsRetryTime, ex);
				ADNotificationHandler.ErrorTrace("GenericADNotificationHandler.Register: {0}", new object[]
				{
					ex
				});
				this.registrationTimer = new Timer(new TimerCallback(this.RegistrationTimerExpired), this, adnotificationsRetryTime, TimeSpan.Zero);
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0002745C File Offset: 0x0002565C
		private void InternalConfigChanged(ADNotificationEventArgs args)
		{
			lock (this)
			{
				Exception ex = null;
				try
				{
					ADNotificationHandler.DebugTrace("GenericADNotificationHandler.InternalConfigChanged: {0}, id={1}, type={2}", new object[]
					{
						args.ChangeType,
						args.Id,
						args.Type
					});
					if (this.notifRequest != null)
					{
						base.FireConfigChangedEvent(args);
					}
				}
				catch (ADTransientException ex2)
				{
					ex = ex2;
				}
				catch (ADOperationException ex3)
				{
					ex = ex3;
				}
				catch (DataValidationException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					ADNotificationHandler.ErrorTrace("GenericADNotificationHandler.InternalConfigChanged: {1}", new object[]
					{
						base.GetType().Name,
						ex
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ADNotificationProcessingError, null, new object[]
					{
						ex.Message
					});
				}
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00027564 File Offset: 0x00025764
		private void RegistrationTimerExpired(object context)
		{
			lock (this)
			{
				if (this.registrationTimer != null)
				{
					this.Register();
				}
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000275A8 File Offset: 0x000257A8
		private void EnsureTimerDisposed()
		{
			if (this.registrationTimer != null)
			{
				ADNotificationHandler.DebugTrace("{0}: disposing retry timer", new object[]
				{
					base.GetType().Name
				});
				this.registrationTimer.Dispose();
				this.registrationTimer = null;
			}
		}

		// Token: 0x040008C8 RID: 2248
		private Timer registrationTimer;

		// Token: 0x040008C9 RID: 2249
		private ADNotificationRequestCookie notifRequest;
	}
}
