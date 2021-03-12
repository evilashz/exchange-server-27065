using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200030E RID: 782
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ActiveSyncDeviceAccessRule : ADConfigurationObject
	{
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x0009B211 File Offset: 0x00099411
		// (set) Token: 0x0600241C RID: 9244 RVA: 0x0009B223 File Offset: 0x00099423
		public string QueryString
		{
			get
			{
				return (string)this[ActiveSyncDeviceAccessRuleSchema.QueryString];
			}
			set
			{
				this[ActiveSyncDeviceAccessRuleSchema.QueryString] = value;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x0009B231 File Offset: 0x00099431
		// (set) Token: 0x0600241E RID: 9246 RVA: 0x0009B243 File Offset: 0x00099443
		public DeviceAccessCharacteristic Characteristic
		{
			get
			{
				return (DeviceAccessCharacteristic)this[ActiveSyncDeviceAccessRuleSchema.Characteristic];
			}
			set
			{
				this[ActiveSyncDeviceAccessRuleSchema.Characteristic] = value;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x0009B256 File Offset: 0x00099456
		// (set) Token: 0x06002420 RID: 9248 RVA: 0x0009B268 File Offset: 0x00099468
		[Parameter(Mandatory = false)]
		public DeviceAccessLevel AccessLevel
		{
			get
			{
				return (DeviceAccessLevel)this[ActiveSyncDeviceAccessRuleSchema.AccessLevel];
			}
			set
			{
				this[ActiveSyncDeviceAccessRuleSchema.AccessLevel] = value;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x0009B27B File Offset: 0x0009947B
		// (set) Token: 0x06002422 RID: 9250 RVA: 0x0009B283 File Offset: 0x00099483
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

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x0009B28C File Offset: 0x0009948C
		internal override ADObjectSchema Schema
		{
			get
			{
				return ActiveSyncDeviceAccessRule.schema;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x0009B293 File Offset: 0x00099493
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchDeviceAccessRule";
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x0009B29A File Offset: 0x0009949A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x0009B2A1 File Offset: 0x000994A1
		internal override ADObjectId ParentPath
		{
			get
			{
				return ActiveSyncDeviceAccessRule.parentPath;
			}
		}

		// Token: 0x0400165D RID: 5725
		private const string MostDerivedClass = "msExchDeviceAccessRule";

		// Token: 0x0400165E RID: 5726
		private static ADObjectId parentPath = new ADObjectId("CN=Mobile Mailbox Settings");

		// Token: 0x0400165F RID: 5727
		private static ActiveSyncDeviceAccessRuleSchema schema = ObjectSchema.GetInstance<ActiveSyncDeviceAccessRuleSchema>();
	}
}
