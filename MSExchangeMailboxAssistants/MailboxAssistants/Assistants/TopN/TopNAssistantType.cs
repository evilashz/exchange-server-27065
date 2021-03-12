using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Assistants.TopN;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.TopN
{
	// Token: 0x02000188 RID: 392
	internal sealed class TopNAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0005C40B File Offset: 0x0005A60B
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.TopNWordsAssistant;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0005C40E File Offset: 0x0005A60E
		public LocalizedString Name
		{
			get
			{
				return Strings.topNName;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0005C415 File Offset: 0x0005A615
		public string NonLocalizedName
		{
			get
			{
				return "TopNWordsAssistant";
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0005C41C File Offset: 0x0005A61C
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.TopNAssistant;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0005C41F File Offset: 0x0005A61F
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.TopNWorkCycle.Read();
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0005C42B File Offset: 0x0005A62B
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.TopNWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0005C437 File Offset: 0x0005A637
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0005C43A File Offset: 0x0005A63A
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForTopNWordsAssistant;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0005C444 File Offset: 0x0005A644
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return new PropertyTagPropertyDefinition[]
				{
					MailboxSchema.IsTopNEnabled
				};
			}
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0005C464 File Offset: 0x0005A664
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			if (mailboxInformation.IsPublicFolderMailbox())
			{
				return false;
			}
			object mailboxProperty = mailboxInformation.GetMailboxProperty(MailboxSchema.IsTopNEnabled);
			if (mailboxProperty == null || !(bool)mailboxProperty)
			{
				TopNAssistantType.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Mailbox with Display name {0} has IsTopNEnabled extended property {1}.", mailboxInformation.DisplayName, (mailboxProperty == null) ? "not set" : "false");
				return false;
			}
			TopNAssistantType.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Mailbox with Display name {0} has IsTopNEnabled extended property set to true.", mailboxInformation.DisplayName);
			return true;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0005C4DC File Offset: 0x0005A6DC
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new TopNAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0005C4F0 File Offset: 0x0005A6F0
		public void OnWorkCycleCheckpoint()
		{
			TopNPerf.NumberOfMailboxesProcessed.RawValue = 0L;
		}

		// Token: 0x040009E4 RID: 2532
		internal const string AssistantName = "TopNWordsAssistant";

		// Token: 0x040009E5 RID: 2533
		private static readonly Trace Tracer = ExTraceGlobals.TopNAssistantTracer;
	}
}
