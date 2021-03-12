using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000307 RID: 775
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	internal class ActiveSyncDevices : ADContainer
	{
		// Token: 0x060023F3 RID: 9203 RVA: 0x0009AEA7 File Offset: 0x000990A7
		public ActiveSyncDevices()
		{
			base.Name = "ExchangeActiveSyncDevices";
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x0009AEBA File Offset: 0x000990BA
		// (set) Token: 0x060023F5 RID: 9205 RVA: 0x0009AECC File Offset: 0x000990CC
		public DateTime? DeletionPeriod
		{
			get
			{
				return (DateTime?)this[ActiveSyncDevicesSchema.DeletionPeriod];
			}
			internal set
			{
				this[ActiveSyncDevicesSchema.DeletionPeriod] = value;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x0009AEDF File Offset: 0x000990DF
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x0009AEF1 File Offset: 0x000990F1
		public int ObjectsDeletedThisPeriod
		{
			get
			{
				return (int)this[ActiveSyncDevicesSchema.ObjectsDeletedThisPeriod];
			}
			internal set
			{
				this[ActiveSyncDevicesSchema.ObjectsDeletedThisPeriod] = value;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x0009AF04 File Offset: 0x00099104
		internal override ADObjectSchema Schema
		{
			get
			{
				return ActiveSyncDevices.schema;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x0009AF0B File Offset: 0x0009910B
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchActiveSyncDevices";
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x0009AF12 File Offset: 0x00099112
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04001644 RID: 5700
		internal const string ContainerName = "ExchangeActiveSyncDevices";

		// Token: 0x04001645 RID: 5701
		internal const string MostDerivedClassName = "msExchActiveSyncDevices";

		// Token: 0x04001646 RID: 5702
		private static ActiveSyncDevicesSchema schema = ObjectSchema.GetInstance<ActiveSyncDevicesSchema>();
	}
}
