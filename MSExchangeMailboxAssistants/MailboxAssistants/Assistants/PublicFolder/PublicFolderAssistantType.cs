using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.PublicFolder;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000173 RID: 371
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00058778 File Offset: 0x00056978
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.PublicFolderAssistant;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x0005877C File Offset: 0x0005697C
		public LocalizedString Name
		{
			get
			{
				return Strings.PublicFolderAssistantName;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00058783 File Offset: 0x00056983
		public string NonLocalizedName
		{
			get
			{
				return "PublicFolderAssistant";
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0005878A File Offset: 0x0005698A
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.PublicFolderAssistant;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0005878E File Offset: 0x0005698E
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForPublicFolderAssistant;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00058795 File Offset: 0x00056995
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return PublicFolderAssistantType.mailboxExtendedProperties;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0005879C File Offset: 0x0005699C
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.PublicFolderWorkCycle.Read();
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x000587A8 File Offset: 0x000569A8
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.PublicFolderWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x000587B4 File Offset: 0x000569B4
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.PublicFolder;
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000587B8 File Offset: 0x000569B8
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000587BC File Offset: 0x000569BC
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			if (!mailboxInformation.IsPublicFolderMailbox())
			{
				PublicFolderAssistantType.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "[{0}]: mailbox isn't a public folder mailbox. No need to process this mailbox.", mailboxInformation.MailboxGuid);
				return false;
			}
			PublicFolderAssistantType.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "[{0}]: Public folder mailbox needs to process. Adding it to the list of mailboxes to process.", mailboxInformation.MailboxGuid);
			return true;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0005880C File Offset: 0x00056A0C
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new PublicFolderAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x0400095C RID: 2396
		private static readonly PropertyTagPropertyDefinition[] mailboxExtendedProperties = new PropertyTagPropertyDefinition[]
		{
			MailboxSchema.IsMarkedMailbox,
			MailboxSchema.MailboxLastProcessedTimestamp
		};

		// Token: 0x0400095D RID: 2397
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;
	}
}
