using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreMaintenance
{
	// Token: 0x020001D9 RID: 473
	internal sealed class StoreUrgentMaintenanceAssistantType : AdminRpcAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00068099 File Offset: 0x00066299
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.StoreUrgentMaintenanceAssistant;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0006809D File Offset: 0x0006629D
		public LocalizedString Name
		{
			get
			{
				return Strings.storeUrgentMaintenanceAssistantName;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x000680A4 File Offset: 0x000662A4
		public string NonLocalizedName
		{
			get
			{
				return "StoreUrgentMaintenanceAssistant";
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000680AB File Offset: 0x000662AB
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.StoreUrgentMaintenanceAssistant;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x000680AF File Offset: 0x000662AF
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.StoreUrgentMaintenanceWorkCycle.Read();
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x000680BB File Offset: 0x000662BB
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.StoreUrgentMaintenanceWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x000680C7 File Offset: 0x000662C7
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x000680CA File Offset: 0x000662CA
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x000680CD File Offset: 0x000662CD
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return Array<PropertyTagPropertyDefinition>.Empty;
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000680D4 File Offset: 0x000662D4
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			return true;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000680D7 File Offset: 0x000662D7
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new StoreUrgentMaintenanceAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000680EB File Offset: 0x000662EB
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x04000B15 RID: 2837
		internal const string AssistantName = "StoreUrgentMaintenanceAssistant";
	}
}
