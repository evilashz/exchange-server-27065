using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000634 RID: 1588
	[Serializable]
	internal class DomainProperties
	{
		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x06004B26 RID: 19238 RVA: 0x001152F2 File Offset: 0x001134F2
		// (set) Token: 0x06004B27 RID: 19239 RVA: 0x001152FA File Offset: 0x001134FA
		public SmtpDomain SmtpDomain
		{
			get
			{
				return this.domain;
			}
			internal set
			{
				this.domain = value;
			}
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x06004B28 RID: 19240 RVA: 0x00115303 File Offset: 0x00113503
		// (set) Token: 0x06004B29 RID: 19241 RVA: 0x0011530B File Offset: 0x0011350B
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

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x06004B2A RID: 19242 RVA: 0x00115314 File Offset: 0x00113514
		// (set) Token: 0x06004B2B RID: 19243 RVA: 0x0011531C File Offset: 0x0011351C
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

		// Token: 0x06004B2C RID: 19244 RVA: 0x00115325 File Offset: 0x00113525
		internal DomainProperties(SmtpDomain domain)
		{
			this.domain = domain;
		}

		// Token: 0x040033A5 RID: 13221
		private SmtpDomain domain;

		// Token: 0x040033A6 RID: 13222
		private OrganizationId organizationId;

		// Token: 0x040033A7 RID: 13223
		private LiveIdInstanceType? liveIdInstanceType;
	}
}
