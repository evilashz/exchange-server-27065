using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002DF RID: 735
	[Serializable]
	public class GlobalLocatorServiceDomain : ConfigurableObject
	{
		// Token: 0x0600228A RID: 8842 RVA: 0x0009702A File Offset: 0x0009522A
		internal GlobalLocatorServiceDomain() : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2012);
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x00097053 File Offset: 0x00095253
		// (set) Token: 0x0600228C RID: 8844 RVA: 0x00097065 File Offset: 0x00095265
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return (Guid)this[GlobalLocatorServiceDomainSchema.ExternalDirectoryOrganizationId];
			}
			set
			{
				this[GlobalLocatorServiceDomainSchema.ExternalDirectoryOrganizationId] = value;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x00097078 File Offset: 0x00095278
		// (set) Token: 0x0600228E RID: 8846 RVA: 0x0009708A File Offset: 0x0009528A
		public SmtpDomain DomainName
		{
			get
			{
				return (SmtpDomain)this[GlobalLocatorServiceDomainSchema.DomainName];
			}
			set
			{
				this[GlobalLocatorServiceDomainSchema.DomainName] = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x00097098 File Offset: 0x00095298
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x000970AA File Offset: 0x000952AA
		public GlsDomainFlags? DomainFlags
		{
			get
			{
				return (GlsDomainFlags?)this[GlobalLocatorServiceDomainSchema.DomainFlags];
			}
			set
			{
				this[GlobalLocatorServiceDomainSchema.DomainFlags] = value;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x000970BD File Offset: 0x000952BD
		// (set) Token: 0x06002292 RID: 8850 RVA: 0x000970CF File Offset: 0x000952CF
		public bool DomainInUse
		{
			get
			{
				return (bool)this[GlobalLocatorServiceDomainSchema.DomainInUse];
			}
			set
			{
				this[GlobalLocatorServiceDomainSchema.DomainInUse] = value;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x000970E2 File Offset: 0x000952E2
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<GlobalLocatorServiceDomainSchema>();
			}
		}
	}
}
