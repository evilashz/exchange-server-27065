using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000309 RID: 777
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ActiveSyncDeviceClass : ADConfigurationObject
	{
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x0009AF8A File Offset: 0x0009918A
		internal static ActiveSyncDeviceClassSchema StaticSchema
		{
			get
			{
				return ActiveSyncDeviceClass.schema;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x0009AF91 File Offset: 0x00099191
		// (set) Token: 0x06002401 RID: 9217 RVA: 0x0009AFA3 File Offset: 0x000991A3
		public string DeviceType
		{
			get
			{
				return (string)this[ActiveSyncDeviceClassSchema.DeviceType];
			}
			set
			{
				this[ActiveSyncDeviceClassSchema.DeviceType] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.DeviceType, value);
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x0009AFC1 File Offset: 0x000991C1
		// (set) Token: 0x06002403 RID: 9219 RVA: 0x0009AFD3 File Offset: 0x000991D3
		public string DeviceModel
		{
			get
			{
				return (string)this[ActiveSyncDeviceClassSchema.DeviceModel];
			}
			set
			{
				this[ActiveSyncDeviceClassSchema.DeviceModel] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.DeviceModel, value);
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0009AFF4 File Offset: 0x000991F4
		// (set) Token: 0x06002405 RID: 9221 RVA: 0x0009B036 File Offset: 0x00099236
		public DateTime? LastUpdateTime
		{
			get
			{
				DateTime? result = this[ActiveSyncDeviceClassSchema.LastUpdateTime] as DateTime?;
				if (result != null)
				{
					return new DateTime?(result.Value.ToUniversalTime());
				}
				return result;
			}
			set
			{
				this[ActiveSyncDeviceClassSchema.LastUpdateTime] = value;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x0009B049 File Offset: 0x00099249
		// (set) Token: 0x06002407 RID: 9223 RVA: 0x0009B051 File Offset: 0x00099251
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x0009B05A File Offset: 0x0009925A
		internal override ADObjectSchema Schema
		{
			get
			{
				return ActiveSyncDeviceClass.schema;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x0009B061 File Offset: 0x00099261
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchActiveSyncDevice";
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x0009B068 File Offset: 0x00099268
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x0009B06F File Offset: 0x0009926F
		internal override ADObjectId ParentPath
		{
			get
			{
				return ActiveSyncDeviceClass.parentPath;
			}
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0009B076 File Offset: 0x00099276
		internal static string GetCommonName(string deviceType, string deviceModel)
		{
			return deviceType + '§' + deviceModel;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0009B089 File Offset: 0x00099289
		internal string GetCommonName()
		{
			return ActiveSyncDeviceClass.GetCommonName(this.DeviceType, this.DeviceModel);
		}

		// Token: 0x0400164A RID: 5706
		public const char SeparatorChar = '§';

		// Token: 0x0400164B RID: 5707
		private const string MostDerivedClass = "msExchActiveSyncDevice";

		// Token: 0x0400164C RID: 5708
		private const string RelativeParentPath = "CN=ExchangeDeviceClasses,CN=Mobile Mailbox Settings";

		// Token: 0x0400164D RID: 5709
		private static ADObjectId parentPath = new ADObjectId("CN=ExchangeDeviceClasses,CN=Mobile Mailbox Settings");

		// Token: 0x0400164E RID: 5710
		private static ActiveSyncDeviceClassSchema schema = ObjectSchema.GetInstance<ActiveSyncDeviceClassSchema>();
	}
}
