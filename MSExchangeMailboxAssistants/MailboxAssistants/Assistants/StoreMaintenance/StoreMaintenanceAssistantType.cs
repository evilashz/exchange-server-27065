using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreMaintenance
{
	// Token: 0x020001D5 RID: 469
	internal sealed class StoreMaintenanceAssistantType : AdminRpcAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00067FB7 File Offset: 0x000661B7
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.StoreMaintenanceAssistant;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00067FBA File Offset: 0x000661BA
		public LocalizedString Name
		{
			get
			{
				return Strings.storeMaintenanceAssistantName;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00067FC1 File Offset: 0x000661C1
		public string NonLocalizedName
		{
			get
			{
				return "StoreMaintenanceAssistant";
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00067FC8 File Offset: 0x000661C8
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.StoreMaintenanceAssistant;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00067FCC File Offset: 0x000661CC
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.StoreMaintenanceWorkCycle.Read();
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00067FD8 File Offset: 0x000661D8
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.StoreMaintenanceWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00067FE4 File Offset: 0x000661E4
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00067FE7 File Offset: 0x000661E7
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00067FEA File Offset: 0x000661EA
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return Array<PropertyTagPropertyDefinition>.Empty;
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00067FF1 File Offset: 0x000661F1
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			return true;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00067FF4 File Offset: 0x000661F4
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new StoreMaintenanceAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00068008 File Offset: 0x00066208
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x04000B13 RID: 2835
		internal const string AssistantName = "StoreMaintenanceAssistant";
	}
}
