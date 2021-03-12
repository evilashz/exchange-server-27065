using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.UMReporting
{
	// Token: 0x020001C6 RID: 454
	internal sealed class UMReportingAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00066593 File Offset: 0x00064793
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.UMReportingWorkCycle.Read();
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0006659F File Offset: 0x0006479F
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.UMReportingWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x000665AB File Offset: 0x000647AB
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.UMReportingAssistant;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x000665AE File Offset: 0x000647AE
		public LocalizedString Name
		{
			get
			{
				return Strings.umReportingAssistantName;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x000665B5 File Offset: 0x000647B5
		public string NonLocalizedName
		{
			get
			{
				return "UMReportingAssistant";
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x000665BC File Offset: 0x000647BC
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.UMReportingAssistant;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x000665C0 File Offset: 0x000647C0
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return new PropertyTagPropertyDefinition[]
				{
					MailboxSchema.HasUMReportData
				};
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000665E0 File Offset: 0x000647E0
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			if (mailboxInformation.IsPublicFolderMailbox())
			{
				return false;
			}
			object mailboxProperty = mailboxInformation.GetMailboxProperty(MailboxSchema.HasUMReportData);
			if (mailboxProperty == null || !(bool)mailboxProperty)
			{
				CallIdTracer.TraceDebug(UMReportingAssistantType.Tracer, this.GetHashCode(), "Mailbox with Display name {0} has UM extended property {1}.", new object[]
				{
					mailboxInformation.DisplayName,
					(mailboxProperty == null) ? "not set" : "false"
				});
				return false;
			}
			CallIdTracer.TraceDebug(UMReportingAssistantType.Tracer, this.GetHashCode(), "Mailbox with Display name {0} has UM extended property set to true.", new object[]
			{
				mailboxInformation.DisplayName
			});
			return true;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00066679 File Offset: 0x00064879
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForUMReportingAssistant;
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00066680 File Offset: 0x00064880
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00066682 File Offset: 0x00064882
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new UMReportingAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000AE5 RID: 2789
		internal const string AssistantName = "UMReportingAssistant";

		// Token: 0x04000AE6 RID: 2790
		private static readonly Trace Tracer = ExTraceGlobals.UMReportsTracer;
	}
}
