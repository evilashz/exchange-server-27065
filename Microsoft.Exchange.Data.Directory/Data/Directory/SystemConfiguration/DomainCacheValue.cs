using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000638 RID: 1592
	[Serializable]
	internal class DomainCacheValue
	{
		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x06004B3D RID: 19261 RVA: 0x0011588A File Offset: 0x00113A8A
		// (set) Token: 0x06004B3E RID: 19262 RVA: 0x00115892 File Offset: 0x00113A92
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
			internal set
			{
				this.organizationId = value;
			}
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x06004B3F RID: 19263 RVA: 0x0011589B File Offset: 0x00113A9B
		// (set) Token: 0x06004B40 RID: 19264 RVA: 0x001158A3 File Offset: 0x00113AA3
		public LiveIdInstanceType? LiveIdInstanceType
		{
			get
			{
				return this.liveIdInstanceType;
			}
			internal set
			{
				this.liveIdInstanceType = value;
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x06004B41 RID: 19265 RVA: 0x001158AC File Offset: 0x00113AAC
		// (set) Token: 0x06004B42 RID: 19266 RVA: 0x001158B4 File Offset: 0x00113AB4
		public AuthenticationType? AuthenticationType { get; internal set; }

		// Token: 0x06004B43 RID: 19267 RVA: 0x001158BD File Offset: 0x00113ABD
		internal DomainCacheValue()
		{
		}

		// Token: 0x040033B4 RID: 13236
		private OrganizationId organizationId;

		// Token: 0x040033B5 RID: 13237
		private LiveIdInstanceType? liveIdInstanceType;
	}
}
