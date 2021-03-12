using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200002C RID: 44
	internal class SubmissionPoisonHandler : ITransportComponent
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000B564 File Offset: 0x00009764
		public SubmissionPoisonHandler(TimeSpan poisonEntryExpiryWindow, int maxPoisonEntries, QuarantineHandler quarantineHandler, ICrashRepository crashRepository, IStoreDriverTracer storeDriverTracer)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("maxPoisonEntries", maxPoisonEntries);
			ArgumentValidator.ThrowIfNull("quarantineHandler", quarantineHandler);
			ArgumentValidator.ThrowIfNull("crashRepository", crashRepository);
			ArgumentValidator.ThrowIfNull("storeDriverTracer", storeDriverTracer);
			this.poisonEntryExpiryWindow = poisonEntryExpiryWindow;
			this.maxPoisonEntries = maxPoisonEntries;
			this.quarantineHandler = quarantineHandler;
			this.crashRepository = crashRepository;
			this.storeDriverTracer = storeDriverTracer;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000B5E0 File Offset: 0x000097E0
		public virtual int PoisonThreshold
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.PoisonThreshold;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000B5F6 File Offset: 0x000097F6
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000B5FE File Offset: 0x000097FE
		public bool Loaded
		{
			get
			{
				return this.loaded;
			}
			protected set
			{
				this.loaded = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000B607 File Offset: 0x00009807
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000B60F File Offset: 0x0000980F
		public Dictionary<Guid, Dictionary<long, ResourceEventCounterCrashInfo>> SubmissionPoisonDataStore
		{
			get
			{
				return this.submissionPoisonDataStore;
			}
			protected set
			{
				this.submissionPoisonDataStore = value;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B618 File Offset: 0x00009818
		public void Load()
		{
			try
			{
				foreach (Guid guid in this.crashRepository.GetAllResourceIDs())
				{
					Dictionary<long, ResourceEventCounterCrashInfo> value;
					SortedSet<DateTime> sortedSet;
					if (!this.crashRepository.GetResourceCrashInfoData(guid, this.poisonEntryExpiryWindow, out value, out sortedSet))
					{
						this.crashRepository.PurgeResourceData(guid);
					}
					else
					{
						this.submissionPoisonDataStore.Add(guid, value);
						if (sortedSet != null && sortedSet.Count > 0)
						{
							this.quarantineHandler.CheckAndQuarantine(guid, sortedSet);
						}
					}
				}
			}
			catch (CrashRepositoryAccessException ex)
			{
				throw new TransportComponentLoadFailedException(ex.ErrorDescription, ex);
			}
			this.loaded = true;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000B6DC File Offset: 0x000098DC
		public void Unload()
		{
			this.resourceProtector.EnterWriteLock();
			try
			{
				if (this.loaded)
				{
					this.submissionPoisonDataStore.Clear();
					this.loaded = false;
				}
			}
			finally
			{
				this.resourceProtector.ExitWriteLock();
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000B72C File Offset: 0x0000992C
		public string OnUnhandledException(Exception e)
		{
			if (e != null)
			{
				return "Unhandled Exception encountered: " + e.ToString();
			}
			return "Unhandled Exception encountered";
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000B748 File Offset: 0x00009948
		public bool VerifyPoisonMessage(SubmissionPoisonContext submissionPoisonContext)
		{
			int num = 0;
			this.resourceProtector.EnterReadLock();
			try
			{
				if (!this.loaded)
				{
					return false;
				}
				ArgumentValidator.ThrowIfNull("submissionPoisonContext", submissionPoisonContext);
				ArgumentValidator.ThrowIfEmpty("submissionPoisonContext.ResourceGuid", submissionPoisonContext.ResourceGuid);
				if (this.submissionPoisonDataStore.ContainsKey(submissionPoisonContext.ResourceGuid) && this.submissionPoisonDataStore[submissionPoisonContext.ResourceGuid].ContainsKey(submissionPoisonContext.MapiEventCounter))
				{
					foreach (DateTime dateTime in this.submissionPoisonDataStore[submissionPoisonContext.ResourceGuid][submissionPoisonContext.MapiEventCounter].CrashTimes)
					{
						if (!StoreDriverUtils.CheckIfDateTimeExceedsThreshold(dateTime, DateTime.UtcNow, this.poisonEntryExpiryWindow))
						{
							num++;
						}
					}
					if (num >= this.PoisonThreshold)
					{
						return true;
					}
				}
			}
			finally
			{
				this.resourceProtector.ExitReadLock();
			}
			return false;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000B85C File Offset: 0x00009A5C
		public bool VerifyPoisonNdrSent(SubmissionPoisonContext submissionPoisonContext)
		{
			this.resourceProtector.EnterReadLock();
			try
			{
				if (!this.loaded)
				{
					return false;
				}
				ArgumentValidator.ThrowIfNull("submissionPoisonContext", submissionPoisonContext);
				ArgumentValidator.ThrowIfEmpty("submissionPoisonContext.ResourceGuid", submissionPoisonContext.ResourceGuid);
				if (this.submissionPoisonDataStore.ContainsKey(submissionPoisonContext.ResourceGuid) && this.submissionPoisonDataStore[submissionPoisonContext.ResourceGuid].ContainsKey(submissionPoisonContext.MapiEventCounter))
				{
					return this.submissionPoisonDataStore[submissionPoisonContext.ResourceGuid][submissionPoisonContext.MapiEventCounter].IsPoisonNdrSent;
				}
			}
			finally
			{
				this.resourceProtector.ExitReadLock();
			}
			return false;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000B914 File Offset: 0x00009B14
		public void SavePoisonContext(SubmissionPoisonContext submissionPoisonContext)
		{
			this.resourceProtector.EnterReadLock();
			try
			{
				if (!this.loaded)
				{
					return;
				}
				if (submissionPoisonContext == null || submissionPoisonContext.ResourceGuid == Guid.Empty)
				{
					this.storeDriverTracer.GeneralTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Poison context information cannot be store on the crashing thread. Exiting...");
					return;
				}
			}
			finally
			{
				this.resourceProtector.ExitReadLock();
			}
			this.resourceProtector.EnterWriteLock();
			try
			{
				ResourceEventCounterCrashInfo resourceEventCounterCrashInfo;
				if (this.submissionPoisonDataStore.ContainsKey(submissionPoisonContext.ResourceGuid) && this.submissionPoisonDataStore[submissionPoisonContext.ResourceGuid].ContainsKey(submissionPoisonContext.MapiEventCounter))
				{
					resourceEventCounterCrashInfo = this.submissionPoisonDataStore[submissionPoisonContext.ResourceGuid][submissionPoisonContext.MapiEventCounter];
				}
				else
				{
					resourceEventCounterCrashInfo = new ResourceEventCounterCrashInfo(new SortedSet<DateTime>(), false);
				}
				resourceEventCounterCrashInfo.CrashTimes.Add(DateTime.UtcNow);
				try
				{
					this.crashRepository.PersistCrashInfo(submissionPoisonContext.ResourceGuid, submissionPoisonContext.MapiEventCounter, resourceEventCounterCrashInfo, this.maxPoisonEntries);
				}
				catch (CrashRepositoryAccessException)
				{
				}
			}
			finally
			{
				this.resourceProtector.ExitWriteLock();
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000BA54 File Offset: 0x00009C54
		public void UpdatePoisonNdrSentToTrue(SubmissionPoisonContext submissionPoisonContext)
		{
			this.resourceProtector.EnterReadLock();
			try
			{
				if (!this.loaded)
				{
					return;
				}
				ArgumentValidator.ThrowIfNull("submissionPoisonContext", submissionPoisonContext);
				ArgumentValidator.ThrowIfEmpty("submissionPoisonContext.ResourceGuid", submissionPoisonContext.ResourceGuid);
			}
			finally
			{
				this.resourceProtector.ExitReadLock();
			}
			this.resourceProtector.EnterWriteLock();
			try
			{
				if (this.submissionPoisonDataStore.ContainsKey(submissionPoisonContext.ResourceGuid) && this.submissionPoisonDataStore[submissionPoisonContext.ResourceGuid].ContainsKey(submissionPoisonContext.MapiEventCounter))
				{
					ResourceEventCounterCrashInfo resourceEventCounterCrashInfo = this.submissionPoisonDataStore[submissionPoisonContext.ResourceGuid][submissionPoisonContext.MapiEventCounter];
					resourceEventCounterCrashInfo.IsPoisonNdrSent = true;
					this.crashRepository.PersistCrashInfo(submissionPoisonContext.ResourceGuid, submissionPoisonContext.MapiEventCounter, resourceEventCounterCrashInfo, this.maxPoisonEntries);
				}
			}
			finally
			{
				this.resourceProtector.ExitWriteLock();
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000BB4C File Offset: 0x00009D4C
		public SmtpResponse GetPoisonHandledSmtpResponse(long mapiEventCounterID)
		{
			return new SmtpResponse("554", "5.6.0", new string[]
			{
				string.Format(SubmissionPoisonHandler.poisonNdrGenerationFailureSmtpResponseText, mapiEventCounterID)
			});
		}

		// Token: 0x040000CA RID: 202
		private static string poisonNdrGenerationFailureSmtpResponseText = "An unexpected error was encountered during submission of this mail. Error reference number = {0}.";

		// Token: 0x040000CB RID: 203
		private readonly int maxPoisonEntries;

		// Token: 0x040000CC RID: 204
		private readonly TimeSpan poisonEntryExpiryWindow;

		// Token: 0x040000CD RID: 205
		private readonly QuarantineHandler quarantineHandler;

		// Token: 0x040000CE RID: 206
		private readonly ICrashRepository crashRepository;

		// Token: 0x040000CF RID: 207
		private readonly IStoreDriverTracer storeDriverTracer;

		// Token: 0x040000D0 RID: 208
		private readonly ReaderWriterLockSlim resourceProtector = new ReaderWriterLockSlim();

		// Token: 0x040000D1 RID: 209
		private Dictionary<Guid, Dictionary<long, ResourceEventCounterCrashInfo>> submissionPoisonDataStore = new Dictionary<Guid, Dictionary<long, ResourceEventCounterCrashInfo>>();

		// Token: 0x040000D2 RID: 210
		private bool loaded;
	}
}
