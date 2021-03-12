using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreMaintenance
{
	// Token: 0x020001CD RID: 461
	internal sealed class StoreDirectoryServiceMaintenanceAssistantType : AdminRpcAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00067BC0 File Offset: 0x00065DC0
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.StoreDSMaintenanceAssistant;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00067BC4 File Offset: 0x00065DC4
		public LocalizedString Name
		{
			get
			{
				return Strings.storeDSMaintenanceAssistantName;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00067BCB File Offset: 0x00065DCB
		public string NonLocalizedName
		{
			get
			{
				return "StoreDSMaintenanceAssistant";
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00067BD2 File Offset: 0x00065DD2
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.StoreDSMaintenanceAssistant;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00067BD6 File Offset: 0x00065DD6
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.StoreDsMaintenanceWorkCycle.Read();
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00067BE2 File Offset: 0x00065DE2
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.StoreDsMaintenanceWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00067BEE File Offset: 0x00065DEE
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00067BF1 File Offset: 0x00065DF1
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00067BF4 File Offset: 0x00065DF4
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return Array<PropertyTagPropertyDefinition>.Empty;
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00067BFB File Offset: 0x00065DFB
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			return true;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00067BFE File Offset: 0x00065DFE
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new StoreDirectoryServiceMaintenanceAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00067C12 File Offset: 0x00065E12
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x04000B06 RID: 2822
		internal const string AssistantName = "StoreDSMaintenanceAssistant";
	}
}
