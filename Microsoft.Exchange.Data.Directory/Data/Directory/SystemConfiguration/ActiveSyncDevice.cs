using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000300 RID: 768
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ActiveSyncDevice : MobileDevice
	{
		// Token: 0x060023CE RID: 9166 RVA: 0x0009AA97 File Offset: 0x00098C97
		public ActiveSyncDevice()
		{
			base.ClientType = MobileClientType.EAS;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x0009AAA6 File Offset: 0x00098CA6
		public ActiveSyncDevice(MobileDevice mobileDevice)
		{
			if (mobileDevice.ClientType != MobileClientType.EAS)
			{
				throw new ArgumentException("mobileDevice's ClientType is not EAS.");
			}
			this.propertyBag = mobileDevice.propertyBag;
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x0009AACE File Offset: 0x00098CCE
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x0009AAD6 File Offset: 0x00098CD6
		public string DeviceActiveSyncVersion
		{
			get
			{
				return base.ClientVersion;
			}
			internal set
			{
				base.ClientVersion = value;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x0009AAE0 File Offset: 0x00098CE0
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new AndFilter(new QueryFilter[]
				{
					base.ImplicitFilter,
					new ComparisonFilter(ComparisonOperator.Equal, MobileDeviceSchema.ClientType, MobileClientType.EAS)
				});
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x0009AB17 File Offset: 0x00098D17
		// (set) Token: 0x060023D4 RID: 9172 RVA: 0x0009AB1F File Offset: 0x00098D1F
		private new string ClientVersion { get; set; }
	}
}
