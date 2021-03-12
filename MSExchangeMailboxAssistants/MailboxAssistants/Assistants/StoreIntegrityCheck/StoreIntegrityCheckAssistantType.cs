using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreIntegrityCheck
{
	// Token: 0x020001D4 RID: 468
	internal sealed class StoreIntegrityCheckAssistantType : AdminRpcAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00067F5B File Offset: 0x0006615B
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.StoreOnlineIntegrityCheckAssistant;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00067F5F File Offset: 0x0006615F
		public LocalizedString Name
		{
			get
			{
				return Strings.storeIntegrityCheckAssistantName;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00067F66 File Offset: 0x00066166
		public string NonLocalizedName
		{
			get
			{
				return "StoreIntegrityCheckAssistant";
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00067F6D File Offset: 0x0006616D
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.StoreOnlineIntegrityCheckAssistant;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00067F71 File Offset: 0x00066171
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.StoreIntegrityCheckWorkCycle.Read();
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00067F7D File Offset: 0x0006617D
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.StoreIntegrityCheckWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00067F89 File Offset: 0x00066189
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00067F8C File Offset: 0x0006618C
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00067F8F File Offset: 0x0006618F
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return Array<PropertyTagPropertyDefinition>.Empty;
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00067F96 File Offset: 0x00066196
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			return true;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00067F99 File Offset: 0x00066199
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new StoreIntegrityCheckAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00067FAD File Offset: 0x000661AD
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x04000B12 RID: 2834
		internal const string AssistantName = "StoreIntegrityCheckAssistant";
	}
}
