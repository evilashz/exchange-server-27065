using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200002E RID: 46
	internal class QuarantineHandler : ITransportComponent
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public QuarantineHandler(TimeSpan quarantineCrashCountWindow, TimeSpan quarantineSpan, int quarantineCrashCountThreshold, ICrashRepository crashRepository, IEventNotificationItem eventNotificationItem)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("quarantineCrashCountThreshold", quarantineCrashCountThreshold);
			ArgumentValidator.ThrowIfNull("crashRepository", crashRepository);
			ArgumentValidator.ThrowIfNull("eventNotificationItem", eventNotificationItem);
			this.quarantineCrashCountWindow = quarantineCrashCountWindow;
			this.quarantineSpan = quarantineSpan;
			this.quarantineCrashCountThreshold = quarantineCrashCountThreshold;
			this.crashRepository = crashRepository;
			this.eventNotificationItem = eventNotificationItem;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000BC21 File Offset: 0x00009E21
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000BC29 File Offset: 0x00009E29
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000BC32 File Offset: 0x00009E32
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000BC3A File Offset: 0x00009E3A
		public Dictionary<Guid, QuarantineInfoContext> ResourceQuarantineData
		{
			get
			{
				return this.resourceQuarantineData;
			}
			protected set
			{
				this.resourceQuarantineData = value;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000BC44 File Offset: 0x00009E44
		public void Load()
		{
			try
			{
				foreach (Guid guid in this.crashRepository.GetAllResourceIDs())
				{
					QuarantineInfoContext value;
					if (this.crashRepository.GetQuarantineInfoContext(guid, this.quarantineSpan, out value))
					{
						this.resourceQuarantineData.Add(guid, value);
					}
				}
			}
			catch (CrashRepositoryAccessException ex)
			{
				throw new TransportComponentLoadFailedException(ex.ErrorDescription, ex);
			}
			this.loaded = true;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000BCDC File Offset: 0x00009EDC
		public void Unload()
		{
			this.resourceProtector.EnterWriteLock();
			try
			{
				if (this.loaded)
				{
					this.resourceQuarantineData.Clear();
					this.loaded = false;
				}
			}
			finally
			{
				this.resourceProtector.ExitWriteLock();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000BD2C File Offset: 0x00009F2C
		public string OnUnhandledException(Exception e)
		{
			if (e != null)
			{
				return "Unhandled Exception encountered: " + e.ToString();
			}
			return "Unhandled Exception encountered";
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000BD48 File Offset: 0x00009F48
		public void CheckAndQuarantine(Guid resourceGuid, SortedSet<DateTime> crashTimes)
		{
			if (!this.loaded)
			{
				return;
			}
			ArgumentValidator.ThrowIfEmpty("resourceGuid", resourceGuid);
			ArgumentValidator.ThrowIfNull("dateTimes", crashTimes);
			if (this.resourceQuarantineData.ContainsKey(resourceGuid))
			{
				return;
			}
			if (crashTimes.Count == 0)
			{
				return;
			}
			int num = 0;
			foreach (DateTime dateTime in crashTimes)
			{
				if (!StoreDriverUtils.CheckIfDateTimeExceedsThreshold(dateTime, DateTime.UtcNow, this.quarantineCrashCountWindow))
				{
					num++;
				}
			}
			if (num >= this.quarantineCrashCountThreshold)
			{
				QuarantineInfoContext quarantineInfoContext = new QuarantineInfoContext(DateTime.UtcNow);
				if (this.crashRepository.PersistQuarantineInfo(resourceGuid, quarantineInfoContext, false))
				{
					this.AddQuarantineDataToDictionary(resourceGuid, quarantineInfoContext);
					this.LogResourceQuarantineInfoOnCrimsonChannel(resourceGuid);
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000BE14 File Offset: 0x0000A014
		public bool IsResourceQuarantined(Guid resourceId, out QuarantineInfoContext quarantineInfoContext, out TimeSpan quarantineRemainingTime)
		{
			quarantineInfoContext = null;
			quarantineRemainingTime = this.quarantineSpan;
			this.resourceProtector.EnterReadLock();
			try
			{
				if (!this.loaded)
				{
					return false;
				}
				ArgumentValidator.ThrowIfEmpty("resourceId", resourceId);
				if (this.resourceQuarantineData.ContainsKey(resourceId))
				{
					quarantineInfoContext = this.resourceQuarantineData[resourceId];
					DateTime utcNow = DateTime.UtcNow;
					if (!StoreDriverUtils.CheckIfDateTimeExceedsThreshold(this.resourceQuarantineData[resourceId].QuarantineStartTime, utcNow, this.quarantineSpan))
					{
						quarantineRemainingTime = this.quarantineSpan - utcNow.Subtract(this.resourceQuarantineData[resourceId].QuarantineStartTime);
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

		// Token: 0x060001EB RID: 491 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		protected void AddQuarantineDataToDictionary(Guid resourceGuid, QuarantineInfoContext quarantineInfoContext)
		{
			if (this.resourceQuarantineData.ContainsKey(resourceGuid))
			{
				this.resourceQuarantineData[resourceGuid] = quarantineInfoContext;
				return;
			}
			this.resourceQuarantineData.Add(resourceGuid, quarantineInfoContext);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000BF0F File Offset: 0x0000A10F
		protected void LogResourceQuarantineInfoOnCrimsonChannel(Guid resourceGuid)
		{
			this.eventNotificationItem.Publish(ExchangeComponent.MailboxTransport.Name, "MailboxTransportUserQuarantine", string.Empty, "Resource has been quarantined due to several messages causing crashes. The resource Guid is: " + resourceGuid, ResultSeverityLevel.Warning, false);
		}

		// Token: 0x040000D5 RID: 213
		private readonly TimeSpan quarantineCrashCountWindow;

		// Token: 0x040000D6 RID: 214
		private readonly TimeSpan quarantineSpan;

		// Token: 0x040000D7 RID: 215
		private readonly int quarantineCrashCountThreshold;

		// Token: 0x040000D8 RID: 216
		private readonly ICrashRepository crashRepository;

		// Token: 0x040000D9 RID: 217
		private readonly ReaderWriterLockSlim resourceProtector = new ReaderWriterLockSlim();

		// Token: 0x040000DA RID: 218
		private readonly IEventNotificationItem eventNotificationItem;

		// Token: 0x040000DB RID: 219
		private Dictionary<Guid, QuarantineInfoContext> resourceQuarantineData = new Dictionary<Guid, QuarantineInfoContext>();

		// Token: 0x040000DC RID: 220
		private bool loaded;
	}
}
