using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreIntegrityCheck
{
	// Token: 0x020001D7 RID: 471
	internal sealed class StoreScheduledIntegrityCheckAssistantType : AdminRpcAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00068029 File Offset: 0x00066229
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.StoreScheduledIntegrityCheckAssistant;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0006802D File Offset: 0x0006622D
		public LocalizedString Name
		{
			get
			{
				return Strings.storeScheduledIntegrityCheckAssistantName;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00068034 File Offset: 0x00066234
		public string NonLocalizedName
		{
			get
			{
				return "StoreScheduledIntegrityCheckAssistant";
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0006803B File Offset: 0x0006623B
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.StoreScheduledIntegrityCheckAssistant;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0006803F File Offset: 0x0006623F
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.StoreScheduledIntegrityCheckWorkCycle.Read();
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0006804B File Offset: 0x0006624B
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.StoreScheduledIntegrityCheckWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00068057 File Offset: 0x00066257
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0006805A File Offset: 0x0006625A
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0006805D File Offset: 0x0006625D
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return Array<PropertyTagPropertyDefinition>.Empty;
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00068064 File Offset: 0x00066264
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			return true;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00068067 File Offset: 0x00066267
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new StoreScheduledIntegrityCheckAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0006807B File Offset: 0x0006627B
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x04000B14 RID: 2836
		internal const string AssistantName = "StoreScheduledIntegrityCheckAssistant";
	}
}
