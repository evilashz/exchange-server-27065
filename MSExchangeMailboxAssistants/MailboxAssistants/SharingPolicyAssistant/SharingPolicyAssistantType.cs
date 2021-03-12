using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SharingPolicy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxAssistants.Assistants;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.SharingPolicyAssistant
{
	// Token: 0x0200015E RID: 350
	internal sealed class SharingPolicyAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x00055812 File Offset: 0x00053A12
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.SharingPolicyAssistant;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00055815 File Offset: 0x00053A15
		public LocalizedString Name
		{
			get
			{
				return Strings.descSharingPolicyAssistantName;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0005581C File Offset: 0x00053A1C
		public string NonLocalizedName
		{
			get
			{
				return "SharingPolicyAssistant";
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00055823 File Offset: 0x00053A23
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.SharingPolicyAssistant;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00055826 File Offset: 0x00053A26
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.SharingPolicyWorkCycle.Read();
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00055832 File Offset: 0x00053A32
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.SharingPolicyWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0005583E File Offset: 0x00053A3E
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00055841 File Offset: 0x00053A41
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForSharingPolicyAssistant;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x00055848 File Offset: 0x00053A48
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return SharingPolicyAssistantType.mailboxExtendedProperties;
			}
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00055850 File Offset: 0x00053A50
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			if (mailboxInformation.IsPublicFolderMailbox() || mailboxInformation.IsGroupMailbox())
			{
				return false;
			}
			if (!CalendarUpgrade.IsMailboxActive(new ExDateTime?(new ExDateTime(ExTimeZone.UtcTimeZone, mailboxInformation.LastLogonTime.ToUniversalTime()))))
			{
				SharingPolicyAssistantType.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "{0}: mailbox is inactive hence skipping it.", mailboxInformation.MailboxGuid);
				return false;
			}
			SharingPolicyAssistantType.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "{0}: mailbox is not GroupMailbox/PublicFolderMailbox and is active too. Adding to the list of mailboxes to process.", mailboxInformation.MailboxGuid);
			return true;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000558CE File Offset: 0x00053ACE
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new SharingPolicyAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000558E2 File Offset: 0x00053AE2
		public void OnWorkCycleCheckpoint()
		{
			SharingPolicyCache.PurgeCache();
		}

		// Token: 0x0400092C RID: 2348
		internal const string AssistantName = "SharingPolicyAssistant";

		// Token: 0x0400092D RID: 2349
		private static readonly PropertyTagPropertyDefinition[] mailboxExtendedProperties = new PropertyTagPropertyDefinition[]
		{
			MailboxSchema.LastSharingPolicyAppliedId,
			MailboxSchema.LastSharingPolicyAppliedHash
		};

		// Token: 0x0400092E RID: 2350
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;
	}
}
