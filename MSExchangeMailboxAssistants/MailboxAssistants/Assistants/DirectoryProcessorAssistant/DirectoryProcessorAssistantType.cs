using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant;
using Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator;
using Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x0200019B RID: 411
	internal sealed class DirectoryProcessorAssistantType : AdminRpcAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0005EA74 File Offset: 0x0005CC74
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.DirectoryProcessorAssistant;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0005EA78 File Offset: 0x0005CC78
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.DirectoryProcessorWorkCycle.Read();
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0005EA84 File Offset: 0x0005CC84
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.DirectoryProcessorWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0005EA90 File Offset: 0x0005CC90
		public LocalizedString Name
		{
			get
			{
				return Strings.directoryProcessorAssistantName;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0005EA97 File Offset: 0x0005CC97
		public string NonLocalizedName
		{
			get
			{
				return DirectoryProcessorAssistantType.AssistantName;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0005EA9E File Offset: 0x0005CC9E
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.DirectoryProcessorAssistant;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0005EAA2 File Offset: 0x0005CCA2
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return new PropertyTagPropertyDefinition[0];
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0005EAAA File Offset: 0x0005CCAA
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			return true;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0005EAAD File Offset: 0x0005CCAD
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForDirectoryProcessorAssistant;
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0005EAB4 File Offset: 0x0005CCB4
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0005EAB6 File Offset: 0x0005CCB6
		public override void OnWorkCycleStart(DatabaseInfo databaseInfo)
		{
			Utilities.DebugTrace(DirectoryProcessorAssistantType.Tracer, "Entering DirectoryProcessorAssistantType.OnWorkCycleStart", new object[0]);
			UMGrammarTenantCache.Instance.Update(databaseInfo.Guid);
			GroupMetricsTenantCache.Instance.Update(databaseInfo.Guid);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0005EAED File Offset: 0x0005CCED
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new DirectoryProcessorAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000A37 RID: 2615
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x04000A38 RID: 2616
		internal static readonly string AssistantName = "DirectoryProcessorAssistant";
	}
}
