using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001DE RID: 478
	internal sealed class OABGeneratorAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00069E01 File Offset: 0x00068001
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.OABGeneratorWorkCycle.Read();
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00069E0D File Offset: 0x0006800D
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.OABGeneratorWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00069E19 File Offset: 0x00068019
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.OABGeneratorAssistant;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00069E1D File Offset: 0x0006801D
		public LocalizedString Name
		{
			get
			{
				return Strings.oabGeneratorAssistantName;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00069E24 File Offset: 0x00068024
		public string NonLocalizedName
		{
			get
			{
				return "OABGeneratorAssistant";
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00069E2B File Offset: 0x0006802B
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.OABGeneratorAssistant;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00069E30 File Offset: 0x00068030
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return new PropertyTagPropertyDefinition[]
				{
					this.ControlDataPropertyDefinition
				};
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00069E50 File Offset: 0x00068050
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			OABGeneratorAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "OABGeneratorAssistantType.IsMailboxInteresting");
			OABGeneratorAssistantType.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "OABGeneratorAssistantType.IsMailboxInteresting: Processing mailbox {0} ({1})", mailboxInformation.DisplayName, mailboxInformation.MailboxGuid);
			bool result = false;
			object mailboxProperty = mailboxInformation.GetMailboxProperty(this.ControlDataPropertyDefinition);
			if (mailboxProperty == null || mailboxProperty is PropertyError)
			{
				OABGeneratorAssistantType.Tracer.TraceDebug((long)this.GetHashCode(), "OABGeneratorAssistantType.IsMailboxInteresting: Control data property is null or property error; mailbox is not interesting");
			}
			else
			{
				ControlData controlData = ControlData.CreateFromByteArray(mailboxProperty as byte[]);
				OABGeneratorAssistantType.Tracer.TraceDebug<DateTime, TimeSpan>((long)this.GetHashCode(), "OABGeneratorAssistantType.IsMailboxInteresting: ControlData.LastProcessedDate = {0}; WorkCycle = {1}", controlData.LastProcessedDate, this.WorkCycle);
				if (DateTime.UtcNow - controlData.LastProcessedDate > TimeSpan.FromTicks(this.WorkCycle.Ticks / 2L))
				{
					OABGeneratorAssistantType.Tracer.TraceDebug((long)this.GetHashCode(), "OABGeneratorAssistantType.IsMailboxInteresting: mailbox has not been processed within half-workcycle; it is interesting");
					result = true;
				}
				else
				{
					OABGeneratorAssistantType.Tracer.TraceDebug((long)this.GetHashCode(), "OABGeneratorAssistantType.IsMailboxInteresting: mailbox has been processed within half-workcycle; it is not interesting");
					result = false;
				}
			}
			return result;
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x00069F58 File Offset: 0x00068158
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForOABGeneratorAssistant;
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00069F5F File Offset: 0x0006815F
		public void OnWorkCycleCheckpoint()
		{
			OABGeneratorAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "OABGeneratorAssistantType.OnWorkCycleCheckpoint");
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00069F77 File Offset: 0x00068177
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			OABGeneratorAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "OABGeneratorAssistantType.CreateInstance");
			return new OABGeneratorAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00069FA1 File Offset: 0x000681A1
		public override TimeBasedDatabaseDriver CreateDriver(ThrottleGovernor governor, DatabaseInfo databaseInfo, ITimeBasedAssistantType timeBasedAssistantType, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters)
		{
			return new OABTimeBasedStoreDatabaseDriver(governor, databaseInfo, timeBasedAssistantType, poisonControl, databaseCounters);
		}

		// Token: 0x04000B2F RID: 2863
		internal const string AssistantName = "OABGeneratorAssistant";

		// Token: 0x04000B30 RID: 2864
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;
	}
}
