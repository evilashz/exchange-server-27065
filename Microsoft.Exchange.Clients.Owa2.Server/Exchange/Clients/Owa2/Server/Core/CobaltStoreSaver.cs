using System;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000041 RID: 65
	internal class CobaltStoreSaver : DisposeTrackableBase
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00006E24 File Offset: 0x00005024
		public void Initialize(string smtpAddress, string exchangeSessionId, TimeSpan interval, Func<bool> autoSaveCallback, Action<Exception> permanentFailureCallback)
		{
			this.smtpAddress = smtpAddress;
			this.exchangeSessionId = exchangeSessionId;
			this.autoSaveInterval = interval;
			this.autoSave = autoSaveCallback;
			this.permanentFailure = permanentFailureCallback;
			this.timer = new Timer(new TimerCallback(this.TimerCallback), null, this.autoSaveInterval, this.autoSaveInterval);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006E7C File Offset: 0x0000507C
		public void SaveAndThrowExceptions()
		{
			lock (this.syncObject)
			{
				if (this.timer != null)
				{
					Exception ex = null;
					try
					{
						try
						{
							if (!this.autoSave())
							{
								this.Stop();
							}
						}
						catch (SaveConflictException)
						{
							if (!this.autoSave())
							{
								this.Stop();
							}
						}
					}
					catch (StoragePermanentException ex2)
					{
						ex = ex2;
					}
					catch (InvalidStoreIdException ex3)
					{
						ex = ex3;
					}
					if (ex != null)
					{
						this.Stop();
						this.permanentFailure(ex);
						throw new StoragePermanentException(new LocalizedString(ex.Message), ex);
					}
				}
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006F48 File Offset: 0x00005148
		private void Stop()
		{
			if (!Monitor.IsEntered(this.syncObject))
			{
				throw new InvalidOperationException("CobaltStoreSaver.Stop must only be invoked while holding lock.");
			}
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006F7C File Offset: 0x0000517C
		public void SaveAndLogExceptions(RequestDetailsLogger logger)
		{
			Exception ex2;
			try
			{
				this.SaveAndThrowExceptions();
				return;
			}
			catch (StoragePermanentException ex)
			{
				ex2 = ex;
			}
			catch (StorageTransientException ex3)
			{
				ex2 = ex3;
			}
			logger.ActivityScope.SetProperty(ServiceCommonMetadata.GenericErrors, ex2.ToString());
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006FD0 File Offset: 0x000051D0
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				lock (this.syncObject)
				{
					this.Stop();
				}
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007014 File Offset: 0x00005214
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CobaltStoreSaver>(this);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000706A File Offset: 0x0000526A
		private void TimerCallback(object unused)
		{
			SimulatedWebRequestContext.ExecuteWithoutUserContext("WAC.AutoSave", delegate(RequestDetailsLogger logger)
			{
				WacUtilities.SetEventId(logger, "WAC.AutoSave");
				logger.ActivityScope.SetProperty(OwaServerLogger.LoggerData.PrimarySmtpAddress, this.smtpAddress);
				logger.ActivityScope.SetProperty(WacRequestHandlerMetadata.ExchangeSessionId, this.exchangeSessionId);
				this.SaveAndLogExceptions(logger);
			});
		}

		// Token: 0x040000A8 RID: 168
		private string smtpAddress;

		// Token: 0x040000A9 RID: 169
		private string exchangeSessionId;

		// Token: 0x040000AA RID: 170
		private Timer timer;

		// Token: 0x040000AB RID: 171
		private TimeSpan autoSaveInterval;

		// Token: 0x040000AC RID: 172
		private Func<bool> autoSave;

		// Token: 0x040000AD RID: 173
		private Action<Exception> permanentFailure;

		// Token: 0x040000AE RID: 174
		private object syncObject = new object();
	}
}
