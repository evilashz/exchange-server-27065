using System;
using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ProbeTimeBasedAssistant
{
	// Token: 0x02000240 RID: 576
	internal sealed class ProbeTimeBasedAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0007A93F File Offset: 0x00078B3F
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.ProbeTimeBasedAssistant;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x0007A943 File Offset: 0x00078B43
		public LocalizedString Name
		{
			get
			{
				return Strings.probeTimeBasedAssistant;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0007A94A File Offset: 0x00078B4A
		public string NonLocalizedName
		{
			get
			{
				return "ProbeTimeBasedAssistant";
			}
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0007A951 File Offset: 0x00078B51
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			this.TraceDebugExecuting(ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace) ? MethodBase.GetCurrentMethod().Name : string.Empty);
			return new ProbeTimeBasedAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0007A989 File Offset: 0x00078B89
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.ProbeTimeBasedAssistant;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0007A98D File Offset: 0x00078B8D
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0007A990 File Offset: 0x00078B90
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0007A993 File Offset: 0x00078B93
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return Array<PropertyTagPropertyDefinition>.Empty;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0007A99A File Offset: 0x00078B9A
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.ProbeTimeBasedAssistantWorkCycle.Read();
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0007A9A6 File Offset: 0x00078BA6
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.ProbeTimeBasedAssistantWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0007A9B2 File Offset: 0x00078BB2
		public void OnWorkCycleCheckpoint()
		{
			ProbeTimeBasedAssistantType.numberOfInterestingMailboxes.Clear();
			this.TraceDebugExecuting(ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace) ? MethodBase.GetCurrentMethod().Name : string.Empty);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0007A9E4 File Offset: 0x00078BE4
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			this.TraceDebugExecuting(ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace) ? MethodBase.GetCurrentMethod().Name : string.Empty);
			bool result = false;
			Guid databaseGuid = mailboxInformation.MailboxData.DatabaseGuid;
			if (!ProbeTimeBasedAssistantType.numberOfInterestingMailboxes.ContainsKey(databaseGuid) && !ProbeTimeBasedAssistantType.numberOfInterestingMailboxes.TryAdd(databaseGuid, 0))
			{
				return false;
			}
			if (ProbeTimeBasedAssistantType.numberOfInterestingMailboxes[databaseGuid] < ProbeTimeBasedAssistantType.maxNumberOfInterestingMailboxes)
			{
				result = true;
				ConcurrentDictionary<Guid, int> concurrentDictionary;
				Guid key;
				(concurrentDictionary = ProbeTimeBasedAssistantType.numberOfInterestingMailboxes)[key = databaseGuid] = concurrentDictionary[key] + 1;
			}
			return result;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0007AA6D File Offset: 0x00078C6D
		public override TimeBasedDatabaseDriver CreateDriver(ThrottleGovernor governor, DatabaseInfo databaseInfo, ITimeBasedAssistantType timeBasedAssistantType, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters)
		{
			this.TraceDebugExecuting(ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace) ? MethodBase.GetCurrentMethod().Name : string.Empty);
			return base.CreateDriver(governor, databaseInfo, timeBasedAssistantType, poisonControl, databaseCounters);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0007AAA0 File Offset: 0x00078CA0
		private void TraceDebugExecuting(string method)
		{
			if (ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ProbeTimeBasedAssistantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} is executing {1}", "ProbeTimeBasedAssistant", method);
			}
		}

		// Token: 0x04000CC1 RID: 3265
		internal const string AssistantName = "ProbeTimeBasedAssistant";

		// Token: 0x04000CC2 RID: 3266
		private static ConcurrentDictionary<Guid, int> numberOfInterestingMailboxes = new ConcurrentDictionary<Guid, int>();

		// Token: 0x04000CC3 RID: 3267
		private static int maxNumberOfInterestingMailboxes = 2;
	}
}
