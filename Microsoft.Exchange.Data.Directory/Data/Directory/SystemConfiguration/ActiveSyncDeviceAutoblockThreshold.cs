using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000303 RID: 771
	[Serializable]
	public class ActiveSyncDeviceAutoblockThreshold : ADConfigurationObject
	{
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x0009ACC4 File Offset: 0x00098EC4
		// (set) Token: 0x060023D9 RID: 9177 RVA: 0x0009ACCC File Offset: 0x00098ECC
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x0009ACD5 File Offset: 0x00098ED5
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x0009ACE7 File Offset: 0x00098EE7
		public AutoblockThresholdType BehaviorType
		{
			get
			{
				return (AutoblockThresholdType)this[ActiveSyncDeviceAutoblockThresholdSchema.BehaviorType];
			}
			internal set
			{
				this[ActiveSyncDeviceAutoblockThresholdSchema.BehaviorType] = value;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x0009ACFA File Offset: 0x00098EFA
		// (set) Token: 0x060023DD RID: 9181 RVA: 0x0009AD0C File Offset: 0x00098F0C
		[Parameter(Mandatory = false)]
		public int BehaviorTypeIncidenceLimit
		{
			get
			{
				return (int)this[ActiveSyncDeviceAutoblockThresholdSchema.BehaviorTypeIncidenceLimit];
			}
			set
			{
				this[ActiveSyncDeviceAutoblockThresholdSchema.BehaviorTypeIncidenceLimit] = value;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x0009AD1F File Offset: 0x00098F1F
		// (set) Token: 0x060023DF RID: 9183 RVA: 0x0009AD31 File Offset: 0x00098F31
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan BehaviorTypeIncidenceDuration
		{
			get
			{
				return (EnhancedTimeSpan)this[ActiveSyncDeviceAutoblockThresholdSchema.BehaviorTypeIncidenceDuration];
			}
			set
			{
				this[ActiveSyncDeviceAutoblockThresholdSchema.BehaviorTypeIncidenceDuration] = value;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x0009AD44 File Offset: 0x00098F44
		// (set) Token: 0x060023E1 RID: 9185 RVA: 0x0009AD56 File Offset: 0x00098F56
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan DeviceBlockDuration
		{
			get
			{
				return (EnhancedTimeSpan)this[ActiveSyncDeviceAutoblockThresholdSchema.DeviceBlockDuration];
			}
			set
			{
				this[ActiveSyncDeviceAutoblockThresholdSchema.DeviceBlockDuration] = value;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x0009AD69 File Offset: 0x00098F69
		// (set) Token: 0x060023E3 RID: 9187 RVA: 0x0009AD7B File Offset: 0x00098F7B
		[Parameter(Mandatory = false)]
		public string AdminEmailInsert
		{
			get
			{
				return (string)this[ActiveSyncDeviceAutoblockThresholdSchema.AdminEmailInsert];
			}
			set
			{
				this[ActiveSyncDeviceAutoblockThresholdSchema.AdminEmailInsert] = value;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x0009AD89 File Offset: 0x00098F89
		internal override ADObjectSchema Schema
		{
			get
			{
				return ActiveSyncDeviceAutoblockThreshold.schema;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x0009AD90 File Offset: 0x00098F90
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchActiveSyncDeviceAutoblockThreshold";
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x0009AD97 File Offset: 0x00098F97
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x0009AD9E File Offset: 0x00098F9E
		internal override ADObjectId ParentPath
		{
			get
			{
				return ActiveSyncDeviceAutoblockThreshold.ContainerPath;
			}
		}

		// Token: 0x0400163C RID: 5692
		internal const string MostDerivedClass = "msExchActiveSyncDeviceAutoblockThreshold";

		// Token: 0x0400163D RID: 5693
		public static ADObjectId ContainerPath = new ADObjectId("CN=Mobile Mailbox Policies");

		// Token: 0x0400163E RID: 5694
		private static ActiveSyncDeviceAutoblockThresholdSchema schema = ObjectSchema.GetInstance<ActiveSyncDeviceAutoblockThresholdSchema>();
	}
}
