using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Dar
{
	// Token: 0x02000266 RID: 614
	internal sealed class TaskStoreTimeBasedAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0008060A File Offset: 0x0007E80A
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0008060D File Offset: 0x0007E80D
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new TaskStoreTimeBasedAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x00080621 File Offset: 0x0007E821
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.DarTaskStoreAssistant;
			}
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00080625 File Offset: 0x0007E825
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			return mailboxInformation.DisplayName == "Microsoft Exchange";
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x00080637 File Offset: 0x0007E837
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return new PropertyTagPropertyDefinition[0];
			}
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0008063F File Offset: 0x0007E83F
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x00080641 File Offset: 0x0007E841
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.DarTaskStoreTimeBasedAssistantWorkCycle.Read();
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0008064D File Offset: 0x0007E84D
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x00080659 File Offset: 0x0007E859
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.DarTaskStoreTimeBasedAssistant;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0008065D File Offset: 0x0007E85D
		public LocalizedString Name
		{
			get
			{
				return Strings.DarTaskStoreTimeBasedAssistant;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x00080664 File Offset: 0x0007E864
		public string NonLocalizedName
		{
			get
			{
				return "DarTaskStoreTimeBasedAssistant";
			}
		}

		// Token: 0x04000D45 RID: 3397
		internal const string AssistantName = "DarTaskStoreTimeBasedAssistant";

		// Token: 0x04000D46 RID: 3398
		private const string ArbitrationMailboxDisplayName = "Microsoft Exchange";
	}
}
