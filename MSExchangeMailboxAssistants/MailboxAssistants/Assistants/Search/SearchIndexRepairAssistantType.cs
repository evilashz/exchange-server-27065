using System;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Search;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Search
{
	// Token: 0x02000194 RID: 404
	internal sealed class SearchIndexRepairAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0005D756 File Offset: 0x0005B956
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.SearchIndexRepairAssistant;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0005D75A File Offset: 0x0005B95A
		public LocalizedString Name
		{
			get
			{
				return Strings.searchIndexRepairTimeBasedAssistant;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0005D761 File Offset: 0x0005B961
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.SearchIndexRepairTimebasedAssistant;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0005D765 File Offset: 0x0005B965
		public string NonLocalizedName
		{
			get
			{
				return "SearchIndexRepairAssistant";
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0005D76C File Offset: 0x0005B96C
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.SearchIndexRepairTimeBasedAssistantWorkCycle.Read();
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0005D778 File Offset: 0x0005B978
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0005D784 File Offset: 0x0005B984
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForSearchIndexRepairAssistant;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0005D78B File Offset: 0x0005B98B
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0005D78E File Offset: 0x0005B98E
		private static SearchConfig Config
		{
			get
			{
				if (SearchIndexRepairAssistantType.config == null)
				{
					Interlocked.CompareExchange<SearchConfig>(ref SearchIndexRepairAssistantType.config, new SearchConfig(), null);
				}
				return SearchIndexRepairAssistantType.config;
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0005D7AD File Offset: 0x0005B9AD
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			return mailboxInformation.Active;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0005D7B5 File Offset: 0x0005B9B5
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			SearchIndexRepairAssistantType.tracer.TraceFunction((long)this.GetHashCode(), "SearchIndexRepairAssistant:CreateInstance");
			return new SearchIndexRepairAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0005D7DF File Offset: 0x0005B9DF
		public void OnWorkCycleCheckpoint()
		{
			SearchIndexRepairAssistantType.tracer.TraceFunction((long)this.GetHashCode(), "SearchIndexRepairAssistant:OnWorkCycleCheckpoint");
		}

		// Token: 0x04000A22 RID: 2594
		internal const string AssistantName = "SearchIndexRepairAssistant";

		// Token: 0x04000A23 RID: 2595
		private static SearchConfig config;

		// Token: 0x04000A24 RID: 2596
		private static readonly Trace tracer = ExTraceGlobals.GeneralTracer;
	}
}
