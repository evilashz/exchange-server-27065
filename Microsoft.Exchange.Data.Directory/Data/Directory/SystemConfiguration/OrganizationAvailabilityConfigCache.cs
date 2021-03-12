using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000641 RID: 1601
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OrganizationAvailabilityConfigCache : OrganizationBaseCache
	{
		// Token: 0x06004B68 RID: 19304 RVA: 0x00116110 File Offset: 0x00114310
		public OrganizationAvailabilityConfigCache(OrganizationId organizationId, IConfigurationSession session, IRecipientSession recipientSession) : base(organizationId, session)
		{
			this.recipientSession = recipientSession;
		}

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x06004B69 RID: 19305 RVA: 0x00116121 File Offset: 0x00114321
		public AvailabilityConfig AvailabilityConfig
		{
			get
			{
				this.PopulateCacheIfNeeded();
				return this.config;
			}
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x0011612F File Offset: 0x0011432F
		public ADRecipient GetPerUserAccountObject()
		{
			this.PopulateAccessInformationIfNeeded();
			return this.perUserAccountObject;
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x0011613D File Offset: 0x0011433D
		public ADRecipient GetOrgWideAccountObject()
		{
			this.PopulateAccessInformationIfNeeded();
			return this.orgWideAccountObject;
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x0011614C File Offset: 0x0011434C
		private void PopulateAccessInformationIfNeeded()
		{
			if (!this.accountInformationCached)
			{
				if (this.AvailabilityConfig != null)
				{
					if (this.config.PerUserAccount != null)
					{
						this.perUserAccountObject = this.recipientSession.Read(this.config.PerUserAccount);
					}
					else
					{
						this.perUserAccountObject = null;
						OrganizationBaseCache.Tracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "Null PerUserAccount for get availability configuration for: {0}", base.OrganizationId);
					}
					if (this.config.OrgWideAccount != null)
					{
						this.orgWideAccountObject = this.recipientSession.Read(this.config.OrgWideAccount);
					}
					else
					{
						this.orgWideAccountObject = null;
						OrganizationBaseCache.Tracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "Null OrgWideAccount for get availability configuration for: {0}", base.OrganizationId);
					}
				}
				this.accountInformationCached = true;
			}
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x00116214 File Offset: 0x00114414
		private void PopulateCacheIfNeeded()
		{
			if (!this.cached)
			{
				OrganizationBaseCache.Tracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "Cache miss, get the Availability Configuration for: {0}", base.OrganizationId);
				this.config = base.Session.GetAvailabilityConfig();
				OrganizationBaseCache.Tracer.TraceDebug<string, OrganizationId>((long)this.GetHashCode(), "{0} to get availability configuration for: {1}", (this.config == null) ? "Unable" : "Able", base.OrganizationId);
				this.cached = true;
			}
		}

		// Token: 0x040033CD RID: 13261
		private AvailabilityConfig config;

		// Token: 0x040033CE RID: 13262
		private ADRecipient perUserAccountObject;

		// Token: 0x040033CF RID: 13263
		private ADRecipient orgWideAccountObject;

		// Token: 0x040033D0 RID: 13264
		private bool cached;

		// Token: 0x040033D1 RID: 13265
		private bool accountInformationCached;

		// Token: 0x040033D2 RID: 13266
		private IRecipientSession recipientSession;
	}
}
