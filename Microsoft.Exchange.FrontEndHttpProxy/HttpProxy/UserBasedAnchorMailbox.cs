using System;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;
using Microsoft.Exchange.HttpProxy.Routing.RoutingEntries;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000008 RID: 8
	internal abstract class UserBasedAnchorMailbox : DatabaseBasedAnchorMailbox
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000031E0 File Offset: 0x000013E0
		protected UserBasedAnchorMailbox(AnchorSource anchorSource, object sourceObject, IRequestContext requestContext) : base(anchorSource, sourceObject, requestContext)
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000031EB File Offset: 0x000013EB
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000031F3 File Offset: 0x000013F3
		public Func<ADRawEntry, ADObjectId> MissingDatabaseHandler { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000031FC File Offset: 0x000013FC
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003204 File Offset: 0x00001404
		public string CacheKeyPostfix { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000320D File Offset: 0x0000140D
		protected virtual ADPropertyDefinition[] PropertySet
		{
			get
			{
				return UserBasedAnchorMailbox.ADRawEntryPropertySet;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003214 File Offset: 0x00001414
		protected virtual ADPropertyDefinition DatabaseProperty
		{
			get
			{
				return ADMailboxRecipientSchema.Database;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000321C File Offset: 0x0000141C
		public ADRawEntry GetADRawEntry()
		{
			if (!this.activeDirectoryRawEntryLoaded)
			{
				this.loadedADRawEntry = this.LoadADRawEntry();
				if (this.loadedADRawEntry == null)
				{
					base.RequestContext.Logger.AppendString(HttpProxyMetadata.RoutingHint, "-NoUser");
				}
				ExTraceGlobals.VerboseTracer.TraceDebug<ADRawEntry, UserBasedAnchorMailbox>((long)this.GetHashCode(), "[UserBasedAnchorMailbox::GetADRawEntry]: LoadADRawEntry() resturns {0} for anchor mailbox {1}.", this.loadedADRawEntry, this);
				this.activeDirectoryRawEntryLoaded = true;
			}
			return this.loadedADRawEntry;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000328A File Offset: 0x0000148A
		public string GetDomainName()
		{
			return base.GetCacheEntry().DomainName;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003297 File Offset: 0x00001497
		public override string GetOrganizationNameForLogging()
		{
			if (this.activeDirectoryRawEntryLoaded && this.GetADRawEntry() != null)
			{
				return ((OrganizationId)this.GetADRawEntry()[ADObjectSchema.OrganizationId]).GetFriendlyName();
			}
			return base.GetOrganizationNameForLogging();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000032CC File Offset: 0x000014CC
		public override BackEndCookieEntryBase BuildCookieEntryForTarget(BackEndServer routingTarget, bool proxyToDownLevel, bool useResourceForest)
		{
			if (routingTarget == null)
			{
				throw new ArgumentNullException("routingTarget");
			}
			if (!proxyToDownLevel && !base.UseServerCookie)
			{
				ADObjectId database = this.GetDatabase();
				if (database != null)
				{
					if (useResourceForest)
					{
						return new BackEndDatabaseResourceForestCookieEntry(database.ObjectGuid, this.GetDomainName(), database.PartitionFQDN);
					}
					return new BackEndDatabaseCookieEntry(database.ObjectGuid, this.GetDomainName());
				}
			}
			return base.BuildCookieEntryForTarget(routingTarget, proxyToDownLevel, useResourceForest);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003334 File Offset: 0x00001534
		public override IRoutingEntry GetRoutingEntry()
		{
			IRoutingKey routingKey = this.GetRoutingKey();
			DatabaseGuidRoutingDestination databaseGuidRoutingDestination = this.GetRoutingDestination() as DatabaseGuidRoutingDestination;
			if (routingKey != null && databaseGuidRoutingDestination != null)
			{
				return new SuccessfulMailboxRoutingEntry(routingKey, databaseGuidRoutingDestination, 0L);
			}
			return base.GetRoutingEntry();
		}

		// Token: 0x06000047 RID: 71
		protected abstract ADRawEntry LoadADRawEntry();

		// Token: 0x06000048 RID: 72 RVA: 0x00003370 File Offset: 0x00001570
		protected override AnchorMailboxCacheEntry RefreshCacheEntry()
		{
			ADRawEntry adrawEntry = this.GetADRawEntry();
			if (adrawEntry == null)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<UserBasedAnchorMailbox>((long)this.GetHashCode(), "[UserBasedAnchorMailbox::RefreshCacheEntry]: Anchor mailbox {0} has no AD object. Will use random server.", this);
				return new AnchorMailboxCacheEntry();
			}
			string domainNameFromADRawEntry = UserBasedAnchorMailbox.GetDomainNameFromADRawEntry(adrawEntry);
			ExTraceGlobals.VerboseTracer.TraceDebug<UserBasedAnchorMailbox, string>((long)this.GetHashCode(), "[UserBasedAnchorMailbox::RefreshCacheEntry]: The domain name of anchor mailbox {0} is {1}.", this, domainNameFromADRawEntry);
			ADObjectId adobjectId = (ADObjectId)adrawEntry[this.DatabaseProperty];
			if (adobjectId == null && this.MissingDatabaseHandler != null)
			{
				adobjectId = this.MissingDatabaseHandler(adrawEntry);
			}
			if (adobjectId == null)
			{
				base.RequestContext.Logger.AppendString(HttpProxyMetadata.RoutingHint, "-NoDatabase");
				OrganizationId organizationId = (OrganizationId)adrawEntry[ADObjectSchema.OrganizationId];
				ADUser defaultOrganizationMailbox = HttpProxyBackEndHelper.GetDefaultOrganizationMailbox(organizationId, (string)adrawEntry[ADObjectSchema.DistinguishedName]);
				if (defaultOrganizationMailbox == null || defaultOrganizationMailbox.Database == null)
				{
					if ((Utilities.IsPartnerHostedOnly || VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled) && OrganizationId.ForestWideOrgId.Equals(organizationId))
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "[UserBasedAnchorMailbox::RefreshCacheEntry]: Cannot find organization mailbox for datacenter FirstOrg user {0}. Will use random server.", adrawEntry.Id);
						return new AnchorMailboxCacheEntry
						{
							DomainName = domainNameFromADRawEntry
						};
					}
					string text = string.Format("Unable to find organization mailbox for organization {0}", organizationId);
					ExTraceGlobals.VerboseTracer.TraceError<string>((long)this.GetHashCode(), "[UserBasedAnchorMailbox::RefreshCacheEntry]: {0}", text);
					throw new HttpProxyException(HttpStatusCode.InternalServerError, HttpProxySubErrorCode.OrganizationMailboxNotFound, text);
				}
				else
				{
					adobjectId = defaultOrganizationMailbox.Database;
					ExTraceGlobals.VerboseTracer.TraceDebug<ADObjectId, ObjectId, ADObjectId>((long)this.GetHashCode(), "[UserBasedAnchorMailbox::RefreshCacheEntry]: Anchor mailbox user {0} has no mailbox. Will use organization mailbox {1} with database {2}", adrawEntry.Id, defaultOrganizationMailbox.Identity, adobjectId);
				}
			}
			return new AnchorMailboxCacheEntry
			{
				Database = adobjectId,
				DomainName = domainNameFromADRawEntry
			};
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003520 File Offset: 0x00001720
		protected override AnchorMailboxCacheEntry LoadCacheEntryFromIncomingCookie()
		{
			BackEndDatabaseCookieEntry backEndDatabaseCookieEntry = base.IncomingCookieEntry as BackEndDatabaseCookieEntry;
			if (backEndDatabaseCookieEntry != null)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<UserBasedAnchorMailbox, BackEndDatabaseCookieEntry>((long)this.GetHashCode(), "[UserBasedAnchorMailbox::LoadCacheEntryFromIncomingCookie]: Anchor mailbox {0} using cookie entry {1} as cache entry.", this, backEndDatabaseCookieEntry);
				BackEndDatabaseResourceForestCookieEntry backEndDatabaseResourceForestCookieEntry = base.IncomingCookieEntry as BackEndDatabaseResourceForestCookieEntry;
				return new AnchorMailboxCacheEntry
				{
					Database = new ADObjectId(backEndDatabaseCookieEntry.Database, (backEndDatabaseResourceForestCookieEntry == null) ? null : backEndDatabaseResourceForestCookieEntry.ResourceForest),
					DomainName = backEndDatabaseCookieEntry.Domain
				};
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<UserBasedAnchorMailbox>((long)this.GetHashCode(), "[UserBasedAnchorMailbox::LoadCacheEntryFromCookie]: Anchor mailbox {0} had no BackEndDatabaseCookie.", this);
			return null;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000035A9 File Offset: 0x000017A9
		protected override string ToCacheKey()
		{
			if (!string.IsNullOrEmpty(this.CacheKeyPostfix))
			{
				return base.ToCacheKey() + this.CacheKeyPostfix;
			}
			return base.ToCacheKey();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000035D0 File Offset: 0x000017D0
		protected virtual IRoutingKey GetRoutingKey()
		{
			return null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000035D4 File Offset: 0x000017D4
		private static string GetDomainNameFromADRawEntry(ADRawEntry activeDirectoryRawEntry)
		{
			OrganizationId organizationId = (OrganizationId)activeDirectoryRawEntry[ADObjectSchema.OrganizationId];
			if (organizationId == null || organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				return null;
			}
			SmtpAddress smtpAddress = (SmtpAddress)activeDirectoryRawEntry[ADRecipientSchema.PrimarySmtpAddress];
			if (!string.IsNullOrEmpty(smtpAddress.Domain))
			{
				return smtpAddress.Domain;
			}
			SmtpAddress smtpAddress2 = (SmtpAddress)activeDirectoryRawEntry[ADRecipientSchema.WindowsLiveID];
			if (!string.IsNullOrEmpty(smtpAddress2.Domain))
			{
				return smtpAddress2.Domain;
			}
			return organizationId.ConfigurationUnit.Parent.Name;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003668 File Offset: 0x00001868
		private IRoutingDestination GetRoutingDestination()
		{
			string domainName = this.GetDomainName();
			if (!string.IsNullOrEmpty(domainName))
			{
				ADObjectId database = this.GetDatabase();
				return new DatabaseGuidRoutingDestination(database.ObjectGuid, domainName, database.PartitionFQDN);
			}
			return null;
		}

		// Token: 0x04000021 RID: 33
		public static readonly ADPropertyDefinition[] ADRawEntryPropertySet = new ADPropertyDefinition[]
		{
			ADObjectSchema.ExchangeVersion,
			ADObjectSchema.OrganizationId,
			ADMailboxRecipientSchema.Database,
			ADMailboxRecipientSchema.Sid,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.ExternalEmailAddress
		};

		// Token: 0x04000022 RID: 34
		private ADRawEntry loadedADRawEntry;

		// Token: 0x04000023 RID: 35
		private bool activeDirectoryRawEntryLoaded;
	}
}
