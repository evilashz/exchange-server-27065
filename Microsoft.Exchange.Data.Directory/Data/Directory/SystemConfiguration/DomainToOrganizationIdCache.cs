using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000636 RID: 1590
	internal sealed class DomainToOrganizationIdCache
	{
		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x06004B32 RID: 19250 RVA: 0x001155B8 File Offset: 0x001137B8
		public static DomainToOrganizationIdCache Singleton
		{
			get
			{
				return DomainToOrganizationIdCache.singleton;
			}
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x001155C0 File Offset: 0x001137C0
		public OrganizationId Get(SmtpDomain smtpDomain)
		{
			if (smtpDomain == null)
			{
				throw new ArgumentNullException("smtpDomain");
			}
			SmtpDomainWithSubdomains smtpDomainWithSubdomain = new SmtpDomainWithSubdomains(smtpDomain, false);
			return this.Get(smtpDomainWithSubdomain);
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x001155EC File Offset: 0x001137EC
		public OrganizationId Get(SmtpDomainWithSubdomains smtpDomainWithSubdomain)
		{
			DomainProperties domainProperties = DomainPropertyCache.Singleton.Get(smtpDomainWithSubdomain);
			if (domainProperties == null)
			{
				return null;
			}
			return domainProperties.OrganizationId;
		}

		// Token: 0x040033AE RID: 13230
		private static DomainToOrganizationIdCache singleton = new DomainToOrganizationIdCache();
	}
}
