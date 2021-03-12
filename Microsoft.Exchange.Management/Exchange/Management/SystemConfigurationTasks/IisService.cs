using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000ABE RID: 2750
	[Serializable]
	public class IisService
	{
		// Token: 0x06006119 RID: 24857 RVA: 0x00195470 File Offset: 0x00193670
		public IisService(string name)
		{
			this.serviceName = name;
		}

		// Token: 0x0600611A RID: 24858 RVA: 0x0019547F File Offset: 0x0019367F
		internal IisService(ADVirtualDirectory virtualDirectory) : this(virtualDirectory.Name)
		{
			this.internalUrl = virtualDirectory.InternalUrl;
			this.externalUrl = virtualDirectory.ExternalUrl;
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x001954A8 File Offset: 0x001936A8
		internal IisService(IisService item, IEnumerable<SmtpDomainWithSubdomains> certificateDomains) : this(item.ServiceName)
		{
			this.internalUrl = item.InternalUrl;
			this.externalUrl = item.ExternalUrl;
			if (this.InternalUrl != null || this.ExternalUrl != null)
			{
				foreach (SmtpDomainWithSubdomains smtpDomainWithSubdomains in certificateDomains)
				{
					if (this.InternalUrl != null && string.Equals(this.InternalUrl.Host, smtpDomainWithSubdomains.Address, StringComparison.OrdinalIgnoreCase))
					{
						this.internalUrlValid = UrlValidation.Valid;
					}
					if (this.ExternalUrl != null && string.Equals(this.ExternalUrl.Host, smtpDomainWithSubdomains.Address, StringComparison.OrdinalIgnoreCase))
					{
						this.externalUrlValid = UrlValidation.Valid;
					}
				}
			}
		}

		// Token: 0x17001D69 RID: 7529
		// (get) Token: 0x0600611C RID: 24860 RVA: 0x00195588 File Offset: 0x00193788
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x17001D6A RID: 7530
		// (get) Token: 0x0600611D RID: 24861 RVA: 0x00195590 File Offset: 0x00193790
		public Uri InternalUrl
		{
			get
			{
				return this.internalUrl;
			}
		}

		// Token: 0x17001D6B RID: 7531
		// (get) Token: 0x0600611E RID: 24862 RVA: 0x00195598 File Offset: 0x00193798
		public UrlValidation InternalUrlValid
		{
			get
			{
				return this.internalUrlValid;
			}
		}

		// Token: 0x17001D6C RID: 7532
		// (get) Token: 0x0600611F RID: 24863 RVA: 0x001955A0 File Offset: 0x001937A0
		public Uri ExternalUrl
		{
			get
			{
				return this.externalUrl;
			}
		}

		// Token: 0x17001D6D RID: 7533
		// (get) Token: 0x06006120 RID: 24864 RVA: 0x001955A8 File Offset: 0x001937A8
		public UrlValidation ExternalUrlValid
		{
			get
			{
				return this.externalUrlValid;
			}
		}

		// Token: 0x04003587 RID: 13703
		private readonly string serviceName;

		// Token: 0x04003588 RID: 13704
		private Uri internalUrl;

		// Token: 0x04003589 RID: 13705
		private UrlValidation internalUrlValid;

		// Token: 0x0400358A RID: 13706
		private Uri externalUrl;

		// Token: 0x0400358B RID: 13707
		private UrlValidation externalUrlValid;
	}
}
