using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.PeopleRelevance;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PeopleRelevance
{
	// Token: 0x0200021B RID: 539
	internal sealed class PeopleRelevanceAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x000765AA File Offset: 0x000747AA
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.PeopleRelevanceAssistant;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x000765AE File Offset: 0x000747AE
		public LocalizedString Name
		{
			get
			{
				return Strings.peopleRelevanceAssistantName;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x000765B5 File Offset: 0x000747B5
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.PeopleRelevanceAssistant;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x000765B9 File Offset: 0x000747B9
		public string NonLocalizedName
		{
			get
			{
				return "PeopleRelevanceAssistant";
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x000765C0 File Offset: 0x000747C0
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.PeopleRelevanceWorkCycle.Read();
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x000765CC File Offset: 0x000747CC
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.PeopleRelevanceWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x000765D8 File Offset: 0x000747D8
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x000765DB File Offset: 0x000747DB
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForPeopleRelevanceAssistant;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x000765E2 File Offset: 0x000747E2
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return new PropertyTagPropertyDefinition[0];
			}
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x000765EA File Offset: 0x000747EA
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			PeopleRelevanceAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "PeopleRelevanceAssistantType.IsMailboxInteresting");
			return mailboxInformation.LastLogonTime > DateTime.UtcNow - PeopleRelevanceAssistantType.AllowedInactivity;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00076621 File Offset: 0x00074821
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new PeopleRelevanceAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00076635 File Offset: 0x00074835
		public void OnWorkCycleCheckpoint()
		{
			PeopleRelevanceAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "PeopleRelevanceAssistantType.OnWorkCycleCheckpoint");
		}

		// Token: 0x04000C61 RID: 3169
		internal const string AssistantName = "PeopleRelevanceAssistant";

		// Token: 0x04000C62 RID: 3170
		internal const string PipelineDefinitionFileName = "PeopleRelevancePipelineDefinition.xml";

		// Token: 0x04000C63 RID: 3171
		internal const string TrainingPipelineName = "PeopleRelevance";

		// Token: 0x04000C64 RID: 3172
		internal static readonly Version InferencePipelineVersion = new Version();

		// Token: 0x04000C65 RID: 3173
		private static readonly TimeSpan AllowedInactivity = TimeSpan.FromDays(7.0);

		// Token: 0x04000C66 RID: 3174
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;
	}
}
