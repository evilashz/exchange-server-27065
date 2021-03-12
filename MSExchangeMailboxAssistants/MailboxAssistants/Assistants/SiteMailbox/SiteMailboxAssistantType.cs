using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SiteMailbox
{
	// Token: 0x0200022F RID: 559
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SiteMailboxAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x000787BB File Offset: 0x000769BB
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.SiteMailboxAssistant;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x000787BF File Offset: 0x000769BF
		public LocalizedString Name
		{
			get
			{
				return Strings.SiteMailboxAssistantName;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x000787C6 File Offset: 0x000769C6
		public string NonLocalizedName
		{
			get
			{
				return "Site Mailbox Assistant";
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x000787CD File Offset: 0x000769CD
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.SiteMailboxAssistant;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x000787D1 File Offset: 0x000769D1
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForSiteMailboxAssistant;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x000787D8 File Offset: 0x000769D8
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return SiteMailboxAssistantType.mailboxExtendedProperties;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x000787DF File Offset: 0x000769DF
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.SiteMailboxWorkCycle.Read();
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x000787EB File Offset: 0x000769EB
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.SiteMailboxWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x000787F7 File Offset: 0x000769F7
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x000787FA File Offset: 0x000769FA
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000787FC File Offset: 0x000769FC
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			StoreMailboxTypeDetail storeMailboxTypeDetail = (StoreMailboxTypeDetail)mailboxInformation.GetMailboxProperty(MailboxSchema.MailboxTypeDetail);
			return storeMailboxTypeDetail == StoreMailboxTypeDetail.TeamMailbox;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0007881E File Offset: 0x00076A1E
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new SiteMailboxAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000CA3 RID: 3235
		internal const string AssistantName = "Site Mailbox Assistant";

		// Token: 0x04000CA4 RID: 3236
		private static readonly PropertyTagPropertyDefinition[] mailboxExtendedProperties = new PropertyTagPropertyDefinition[]
		{
			MailboxSchema.MailboxTypeDetail
		};
	}
}
