using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007F5 RID: 2037
	internal class TenantRelocationSyncConfiguration : FullSyncConfiguration
	{
		// Token: 0x170023B2 RID: 9138
		// (get) Token: 0x060064A9 RID: 25769 RVA: 0x0015D33F File Offset: 0x0015B53F
		// (set) Token: 0x060064AA RID: 25770 RVA: 0x0015D346 File Offset: 0x0015B546
		private protected new static int ObjectsPerPageLimit { protected get; private set; }

		// Token: 0x170023B3 RID: 9139
		// (get) Token: 0x060064AB RID: 25771 RVA: 0x0015D34E File Offset: 0x0015B54E
		// (set) Token: 0x060064AC RID: 25772 RVA: 0x0015D355 File Offset: 0x0015B555
		private protected new static int InitialLinkReadSize { protected get; private set; }

		// Token: 0x170023B4 RID: 9140
		// (get) Token: 0x060064AD RID: 25773 RVA: 0x0015D35D File Offset: 0x0015B55D
		// (set) Token: 0x060064AE RID: 25774 RVA: 0x0015D364 File Offset: 0x0015B564
		internal new static TimeSpan FailoverTimeout { get; private set; }

		// Token: 0x170023B5 RID: 9141
		// (get) Token: 0x060064AF RID: 25775 RVA: 0x0015D36C File Offset: 0x0015B56C
		// (set) Token: 0x060064B0 RID: 25776 RVA: 0x0015D373 File Offset: 0x0015B573
		internal static long DeltaSyncUsnRangeLimit { get; private set; }

		// Token: 0x170023B6 RID: 9142
		// (get) Token: 0x060064B1 RID: 25777 RVA: 0x0015D37B File Offset: 0x0015B57B
		public TenantRelocationSyncPageToken PageToken
		{
			get
			{
				return (TenantRelocationSyncPageToken)base.FullSyncPageToken;
			}
		}

		// Token: 0x060064B2 RID: 25778 RVA: 0x0015D388 File Offset: 0x0015B588
		static TenantRelocationSyncConfiguration()
		{
			TenantRelocationSyncConfiguration.InitializeConfigurableSettings();
			TenantRelocationSyncConfiguration.InitialLinkMetadataRangedProperty = RangedPropertyHelper.CreateRangedProperty(ADRecipientSchema.LinkMetadata, new IntRange(0, TenantRelocationSyncConfiguration.InitialLinkReadSize - 1));
			TenantRelocationSyncConfiguration.BaseProperties = new List<PropertyDefinition>(TenantRelocationSyncConfiguration.PropertyDefinitionsForDeletedObjects)
			{
				TenantRelocationSyncConfiguration.InitialLinkMetadataRangedProperty,
				TenantRelocationSyncSchema.AllAttributes,
				ADRecipientSchema.UsnChanged,
				OrganizationSchema.SupportedSharedConfigurations
			}.ToArray();
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x0015D484 File Offset: 0x0015B684
		public TenantRelocationSyncConfiguration(TenantRelocationSyncPageToken pageToken, Guid invocationId, bool isSmallTenant, OutputResultDelegate writeResult, ISyncEventLogger eventLogger) : base(pageToken, invocationId, writeResult, eventLogger, null, TenantRelocationSyncPageToken.Parse(pageToken.ToByteArray()))
		{
			this.isSmallTenant = isSmallTenant;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "New TenantRelocationSyncConfiguration: isSmallTenant:{0}", this.isSmallTenant);
		}

		// Token: 0x060064B4 RID: 25780 RVA: 0x0015E280 File Offset: 0x0015C480
		public override IEnumerable<ADRawEntry> GetDataPage()
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration GetDataPage ...");
			WatermarkMap watermarks = SyncConfiguration.GetReplicationCursors(base.RootOrgConfigurationSession);
			WatermarkMap configNcWatermarks = SyncConfiguration.GetReplicationCursors(base.RootOrgConfigurationSession, true, false);
			string dcName = base.RootOrgConfigurationSession.DomainController;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration dcName {0}", dcName);
			base.TenantConfigurationSession.DomainController = dcName;
			base.RecipientSession.DomainController = dcName;
			Guid watermarksInvocationId = base.RootOrgConfigurationSession.GetInvocationIdByFqdn(dcName);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration watermarksInvocationId {0}", watermarksInvocationId);
			long maxUsn = watermarks[watermarksInvocationId];
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration maxUsn {0}", maxUsn);
			this.PageToken.IsTenantConfigUnitInConfigNc = !ADSession.IsTenantConfigInDomainNC(base.RecipientSession.SessionSettings.GetAccountOrResourceForestFqdn());
			if (this.PageToken.InvocationId == Guid.Empty)
			{
				this.PageToken.SetInvocationId(watermarksInvocationId, dcName);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration set this.PageToken.InvocationId to {0}", this.PageToken.InvocationId);
			}
			this.moreDataForThisPage = true;
			while (this.ShouldReadMoreData())
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration this.PageToken.State = {0}", this.PageToken.State.ToString());
				switch (this.PageToken.State)
				{
				case TenantRelocationSyncState.PreSyncAllObjects:
					if (this.PageToken.PreSyncLdapPagingCookie == null)
					{
						this.PageToken.PendingConfigNcWatermarks = configNcWatermarks;
						this.PageToken.PendingWatermarks = watermarks;
						this.PageToken.PendingWatermarksInvocationId = watermarksInvocationId;
						ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration set this.PageToken.PendingWatermarksInvocationId to {0}", this.PageToken.PendingWatermarksInvocationId);
					}
					foreach (ADRawEntry entry in this.PreSyncAllObjects())
					{
						ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration PreSyncAllObjects yield {0}", entry.Id);
						yield return entry;
					}
					break;
				case TenantRelocationSyncState.EnumerateConfigUnitLiveObjects:
					foreach (ADRawEntry entry2 in this.ReadLiveObjects(maxUsn))
					{
						ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration EnumerateConfigUnitLiveObjects yield {0}", entry2.Id);
						yield return entry2;
					}
					this.PageToken.PendingWatermarksInvocationId = watermarksInvocationId;
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration set this.PageToken.PendingWatermarksInvocationId to {0}", this.PageToken.PendingWatermarksInvocationId);
					if (this.PageToken.IsTenantConfigUnitInConfigNc)
					{
						this.PageToken.PendingConfigNcWatermarks = configNcWatermarks;
					}
					else
					{
						this.PageToken.PendingWatermarks = watermarks;
					}
					break;
				case TenantRelocationSyncState.EnumerateConfigUnitLinksInPage:
					ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration EnumerateConfigUnitLinksInPage skips");
					this.PageToken.SwitchToEnumerateLiveObjectsState();
					break;
				case TenantRelocationSyncState.EnumerateOrganizationalUnitLiveObjects:
					foreach (ADRawEntry entry3 in this.ReadLiveObjects(maxUsn))
					{
						ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration EnumerateOrganizationalUnitLiveObjects yield {0}", entry3.Id);
						yield return entry3;
					}
					if (this.PageToken.IsTenantConfigUnitInConfigNc)
					{
						this.PageToken.PendingWatermarksInvocationId = watermarksInvocationId;
						ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration set this.PageToken.PendingWatermarksInvocationId to {0}", this.PageToken.PendingWatermarksInvocationId);
						this.PageToken.PendingWatermarks = watermarks;
					}
					break;
				case TenantRelocationSyncState.EnumerateOrganizationalUnitLinksInPage:
					ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration EnumerateConfigUnitLinksInPage skips");
					this.PageToken.SwitchToEnumerateLiveObjectsState();
					break;
				case TenantRelocationSyncState.EnumerateConfigUnitDeletedObjects:
					foreach (ADRawEntry entry4 in this.ReadDeletedObjects())
					{
						ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration EnumerateConfigUnitDeletedObjects yield {0}", entry4.Id);
						yield return entry4;
					}
					break;
				case TenantRelocationSyncState.EnumerateOrganizationalUnitDeletedObjects:
					if (!this.PageToken.IsTenantConfigUnitInConfigNc)
					{
						this.PageToken.SwitchToNextState();
					}
					else
					{
						foreach (ADRawEntry entry5 in this.ReadDeletedObjects())
						{
							ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration EnumerateOrganizationalUnitDeletedObjects yield {0}", entry5.Id);
							yield return entry5;
						}
					}
					break;
				case TenantRelocationSyncState.EnumerateSpecialObjects:
					foreach (ADRawEntry entry6 in this.ReadSpecialObjects())
					{
						ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration EnumerateSpecialObjects yield {0}", entry6.Id);
						yield return entry6;
					}
					if (!this.PageToken.IsTenantConfigUnitInConfigNc)
					{
						this.PageToken.PendingWatermarksInvocationId = watermarksInvocationId;
						ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration set this.PageToken.PendingWatermarksInvocationId to {0}", this.PageToken.PendingWatermarksInvocationId);
						this.PageToken.PendingConfigNcWatermarks = configNcWatermarks;
					}
					this.PageToken.SwitchToNextState();
					break;
				default:
					ExTraceGlobals.TenantRelocationTracer.TraceError<string>((long)SyncConfiguration.TraceId, "Invalid PageToken stat {0}", this.PageToken.State.ToString());
					throw new ArgumentException(this.PageToken.State.ToString());
				}
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration this.PageToken.OrganizationalUnitObjectUSN = {0}", this.PageToken.OrganizationalUnitObjectUSN);
			if (this.PageToken.OrganizationalUnitObjectUSN > maxUsn + 1L)
			{
				this.PageToken.OrganizationalUnitObjectUSN = maxUsn + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration set this.PageToken.OrganizationalUnitObjectUSN to {0}", this.PageToken.OrganizationalUnitObjectUSN);
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration this.PageToken.OrganizationalUnitTombstoneUSN = {0}", this.PageToken.OrganizationalUnitTombstoneUSN);
			if (this.PageToken.OrganizationalUnitTombstoneUSN > maxUsn + 1L)
			{
				this.PageToken.OrganizationalUnitTombstoneUSN = maxUsn + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration set this.PageToken.OrganizationalUnitTombstoneUSN to {0}", this.PageToken.OrganizationalUnitTombstoneUSN);
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration this.PageToken.ConfigUnitObjectUSN = {0}", this.PageToken.ConfigUnitObjectUSN);
			if (this.PageToken.ConfigUnitObjectUSN > maxUsn + 1L)
			{
				this.PageToken.ConfigUnitObjectUSN = maxUsn + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration this.PageToken.ConfigUnitObjectUSN = {0}", this.PageToken.ConfigUnitObjectUSN);
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration this.PageToken.ConfigUnitTombstoneUSN = {0}", this.PageToken.ConfigUnitTombstoneUSN);
			if (this.PageToken.ConfigUnitTombstoneUSN > maxUsn + 1L)
			{
				this.PageToken.ConfigUnitTombstoneUSN = maxUsn + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration this.PageToken.ConfigUnitTombstoneUSN = {0}", this.PageToken.ConfigUnitTombstoneUSN);
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration this.PageToken.SpecialObjectsUSN = {0}", this.PageToken.SpecialObjectsUSN);
			if (this.PageToken.SpecialObjectsUSN > maxUsn + 1L)
			{
				this.PageToken.SpecialObjectsUSN = maxUsn + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration this.PageToken.SpecialObjectsUSN = {0}", this.PageToken.SpecialObjectsUSN);
			}
			this.PageToken.LastReadFailureStartTime = DateTime.MinValue;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<DateTime>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration set this.PageToken.LastReadFailureStartTime to {0}", this.PageToken.LastReadFailureStartTime);
			this.PageToken.Timestamp = DateTime.UtcNow;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<DateTime>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration set this.PageToken.Timestamp to {0}", this.PageToken.Timestamp);
			yield break;
		}

		// Token: 0x060064B5 RID: 25781 RVA: 0x0015E2A0 File Offset: 0x0015C4A0
		protected override bool ShouldReadMoreData()
		{
			bool flag = base.ReturnedObjectCount < TenantRelocationSyncConfiguration.ObjectsPerPageLimit && this.PageToken.MoreData && this.moreDataForThisPage;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration ShouldReadMoreData = {0}", flag);
			return flag;
		}

		// Token: 0x060064B6 RID: 25782 RVA: 0x0015E2E8 File Offset: 0x0015C4E8
		private TenantRelocationSyncObject[] ReadAllTeantObjectsInPage(int sizeLimit)
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromAccountPartitionWideScopeSet(base.RecipientSession.SessionSettings.PartitionId);
			adsessionSettings.IncludeSoftDeletedObjectLinks = true;
			adsessionSettings.IncludeSoftDeletedObjects = true;
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 375, "ReadAllTeantObjectsInPage", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncConfiguration.cs");
			topologyConfigurationSession.DomainController = base.TenantConfigurationSession.DomainController;
			topologyConfigurationSession.LogSizeLimitExceededEvent = false;
			topologyConfigurationSession.UseGlobalCatalog = false;
			topologyConfigurationSession.UseConfigNC = false;
			TenantRelocationPagedReader tenantRelocationPagedReader = new TenantRelocationPagedReader(topologyConfigurationSession, base.TenantConfigurationSession.SessionSettings.CurrentOrganizationId.OrganizationalUnit, sizeLimit, TenantRelocationSyncConfiguration.BaseProperties, this.PageToken.PreSyncLdapPagingCookie);
			TenantRelocationSyncObject[] nextResultPage = tenantRelocationPagedReader.GetNextResultPage();
			this.SetPreSyncLdapPagingCookie(tenantRelocationPagedReader);
			return nextResultPage;
		}

		// Token: 0x060064B7 RID: 25783 RVA: 0x0015E398 File Offset: 0x0015C598
		private void SetPreSyncLdapPagingCookie(TenantRelocationPagedReader reader)
		{
			if (reader.Cookie == null || reader.Cookie.Length == 0)
			{
				this.PageToken.PreSyncLdapPagingCookie = null;
				return;
			}
			this.PageToken.PreSyncLdapPagingCookie = reader.Cookie;
		}

		// Token: 0x060064B8 RID: 25784 RVA: 0x0015E704 File Offset: 0x0015C904
		private IEnumerable<ADRawEntry> PreSyncAllObjects()
		{
			PropertyDefinition[] baseProperties = TenantRelocationSyncConfiguration.BaseProperties;
			int sizeLimit = Math.Max(1, TenantRelocationSyncConfiguration.ObjectsPerPageLimit - base.ReturnedObjectCount);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.PreSyncAllObjects sizeLimit = {0}", sizeLimit);
			TenantRelocationSyncObject[] results = this.ReadAllTeantObjectsInPage(sizeLimit);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.PreSyncAllObjects results count = {0}", (results != null) ? results.Length : 0);
			foreach (TenantRelocationSyncObject entry in results)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.PreSyncAllObjects entry {0}", entry.Id);
				MultiValuedProperty<LinkMetadata> linkMetadata = (MultiValuedProperty<LinkMetadata>)entry[TenantRelocationSyncConfiguration.InitialLinkMetadataRangedProperty];
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.PreSyncAllObjects linkMetadata count = {0}", (linkMetadata != null) ? linkMetadata.Count : 0);
				entry.propertyBag.SetField(ADRecipientSchema.LinkMetadata, linkMetadata);
				base.ReturnedObjectCount++;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.PreSyncAllObjects this.ReturnedObjectCount = {0}", base.ReturnedObjectCount);
				base.ReturnedLinkCount += linkMetadata.Count;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.PreSyncAllObjects this.ReturnedLinkCount = {0}", base.ReturnedLinkCount);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.PreSyncAllObjects yield {0}", entry.Id);
				yield return entry;
			}
			if (this.PageToken.PreSyncLdapPagingCookie == null)
			{
				this.PageToken.SwitchToNextState();
			}
			yield break;
		}

		// Token: 0x060064B9 RID: 25785 RVA: 0x0015ED84 File Offset: 0x0015CF84
		private IEnumerable<ADRawEntry> ReadLiveObjects(long maxUsn)
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration ReadLiveObjects ...");
			PropertyDefinition[] properties = TenantRelocationSyncConfiguration.BaseProperties;
			int sizeLimit = Math.Max(1, TenantRelocationSyncConfiguration.ObjectsPerPageLimit - base.ReturnedObjectCount);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects sizeLimit = {0}", sizeLimit);
			ADRawEntry[] results = null;
			long upperLimit;
			if (this.PageToken.State == TenantRelocationSyncState.EnumerateConfigUnitLiveObjects)
			{
				upperLimit = this.GetUsnUpperLimit(this.PageToken.ConfigUnitObjectUSN, maxUsn);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long, long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects USN range:({0},{1})", this.PageToken.ConfigUnitObjectUSN, upperLimit);
				results = base.TenantConfigurationSession.FindAllADRawEntriesByUsnRange(base.TenantConfigurationSession.SessionSettings.CurrentOrganizationId.ConfigurationUnit, this.PageToken.ConfigUnitObjectUSN, upperLimit, sizeLimit, this.isSmallTenant, properties);
			}
			else
			{
				upperLimit = this.GetUsnUpperLimit(this.PageToken.OrganizationalUnitObjectUSN, maxUsn);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long, long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects USN range:({0},{1})", this.PageToken.OrganizationalUnitObjectUSN, upperLimit);
				results = base.RecipientSession.FindAllADRawEntriesByUsnRange(base.TenantConfigurationSession.SessionSettings.CurrentOrganizationId.OrganizationalUnit, this.PageToken.OrganizationalUnitObjectUSN, upperLimit, sizeLimit, this.isSmallTenant, properties);
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects results count = {0}", (results != null) ? results.Length : 0);
			long maxUsnReturned = upperLimit;
			bool returnedAllResults = true;
			foreach (ADRawEntry entry in results)
			{
				if (!this.ShouldReadMoreData())
				{
					returnedAllResults = false;
					break;
				}
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects entry {0}", entry.Id);
				MultiValuedProperty<LinkMetadata> linkMetadata = (MultiValuedProperty<LinkMetadata>)entry[TenantRelocationSyncConfiguration.InitialLinkMetadataRangedProperty];
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects linkMetadata count = {0}", (linkMetadata != null) ? linkMetadata.Count : 0);
				entry.propertyBag.SetField(ADRecipientSchema.LinkMetadata, linkMetadata);
				long entryUsn = (long)entry[ADRecipientSchema.UsnChanged];
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects entryUsn = {0}", entryUsn);
				base.ReturnedObjectCount++;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects this.ReturnedObjectCount = {0}", base.ReturnedObjectCount);
				base.ReturnedLinkCount += linkMetadata.Count;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects this.ReturnedLinkCount = {0}", base.ReturnedLinkCount);
				maxUsnReturned = entryUsn;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects maxUsnReturned = {0}", maxUsnReturned);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects yield {0}", entry.Id);
				yield return entry;
			}
			if (this.PageToken.State == TenantRelocationSyncState.EnumerateConfigUnitLiveObjects)
			{
				this.PageToken.ConfigUnitObjectUSN = ((maxUsnReturned == long.MaxValue) ? long.MaxValue : (maxUsnReturned + 1L));
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects set this.PageToken.ConfigUnitObjectUSN to {0}", this.PageToken.ConfigUnitObjectUSN);
			}
			else
			{
				this.PageToken.OrganizationalUnitObjectUSN = ((maxUsnReturned == long.MaxValue) ? long.MaxValue : (maxUsnReturned + 1L));
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects set this.PageToken.OrganizationalUnitObjectUSN to {0}", this.PageToken.OrganizationalUnitObjectUSN);
			}
			if (results.Length < sizeLimit && returnedAllResults && upperLimit == 9223372036854775807L)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects switch to next state");
				this.PageToken.SwitchToNextState();
			}
			else
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadLiveObjects do nothing, page token is already up to date");
			}
			yield break;
		}

		// Token: 0x060064BA RID: 25786 RVA: 0x0015EDA8 File Offset: 0x0015CFA8
		private long GetUsnUpperLimit(long currentUsn, long maxUsn)
		{
			long result = long.MaxValue;
			if (!this.isSmallTenant && currentUsn < maxUsn - TenantRelocationSyncConfiguration.DeltaSyncUsnRangeLimit)
			{
				result = currentUsn + TenantRelocationSyncConfiguration.DeltaSyncUsnRangeLimit;
			}
			return result;
		}

		// Token: 0x060064BB RID: 25787 RVA: 0x0015F2E8 File Offset: 0x0015D4E8
		private IEnumerable<ADRawEntry> ReadDeletedObjects()
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadDeletedObjects ...");
			int sizeLimit = Math.Max(1, TenantRelocationSyncConfiguration.ObjectsPerPageLimit - base.ReturnedObjectCount);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadDeletedObjects sizeLimit = {0}", sizeLimit);
			ADRawEntry[] results = null;
			if (this.PageToken.State == TenantRelocationSyncState.EnumerateConfigUnitDeletedObjects)
			{
				ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.RecipientSession.SessionSettings.CurrentOrganizationId), 629, "ReadDeletedObjects", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncConfiguration.cs");
				tenantConfigurationSession.DomainController = base.RecipientSession.DomainController;
				tenantConfigurationSession.LogSizeLimitExceededEvent = false;
				tenantConfigurationSession.UseGlobalCatalog = false;
				tenantConfigurationSession.UseConfigNC = !ADSession.IsTenantConfigInDomainNC(base.RecipientSession.SessionSettings.GetAccountOrResourceForestFqdn());
				results = tenantConfigurationSession.FindDeletedTenantSyncObjectByUsnRange(base.RecipientSession.SessionSettings.CurrentOrganizationId.OrganizationalUnit, this.PageToken.ConfigUnitTombstoneUSN, sizeLimit, TenantRelocationSyncConfiguration.PropertyDefinitionsForDeletedObjects);
			}
			else
			{
				ITenantRecipientSession tenantRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.RecipientSession.SessionSettings.CurrentOrganizationId), 649, "ReadDeletedObjects", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncConfiguration.cs");
				tenantRecipientSession.DomainController = base.RecipientSession.DomainController;
				tenantRecipientSession.LogSizeLimitExceededEvent = false;
				tenantRecipientSession.UseGlobalCatalog = false;
				results = tenantRecipientSession.FindDeletedTenantSyncObjectByUsnRange(base.RecipientSession.SessionSettings.CurrentOrganizationId.OrganizationalUnit, this.PageToken.OrganizationalUnitTombstoneUSN, sizeLimit, TenantRelocationSyncConfiguration.PropertyDefinitionsForDeletedObjects);
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadDeletedObjects results count = {0}", (results != null) ? results.Length : 0);
			long maxUsnReturned = long.MaxValue;
			foreach (ADRawEntry entry in results)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadDeletedObjects entry {0}", entry.Id);
				base.ReturnedObjectCount++;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadDeletedObjects this.ReturnedObjectCount = {0}", base.ReturnedObjectCount);
				maxUsnReturned = (long)entry[ADRecipientSchema.UsnChanged];
				yield return entry;
			}
			if (this.PageToken.State == TenantRelocationSyncState.EnumerateConfigUnitDeletedObjects)
			{
				this.PageToken.ConfigUnitTombstoneUSN = ((maxUsnReturned == long.MaxValue) ? long.MaxValue : (maxUsnReturned + 1L));
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadDeletedObjects set this.PageToken.ConfigUnitTombstoneUSN to {0}", this.PageToken.ConfigUnitTombstoneUSN);
			}
			else
			{
				this.PageToken.OrganizationalUnitTombstoneUSN = ((maxUsnReturned == long.MaxValue) ? long.MaxValue : (maxUsnReturned + 1L));
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadDeletedObjects set this.PageToken.OrganizationalUnitTombstoneUSN to {0}", this.PageToken.OrganizationalUnitTombstoneUSN);
			}
			if (results.Length < sizeLimit)
			{
				this.PageToken.SwitchToNextState();
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadDeletedObjects no more deleted objects, we are done");
			}
			else
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadDeletedObjects do nothing, page token is already up to date");
			}
			yield break;
		}

		// Token: 0x060064BC RID: 25788 RVA: 0x0015F828 File Offset: 0x0015DA28
		private IEnumerable<ADRawEntry> ReadSpecialObjects()
		{
			ADObjectId exchangeServiceObject = base.RootOrgConfigurationSession.ConfigurationNamingContext.GetChildId("Services").GetChildId("Microsoft Exchange");
			int numberOfLinksToRead = 1500;
			int rangeStart = 0;
			int rangeEnd = rangeStart + numberOfLinksToRead - 1;
			ADPropertyDefinition[] propertiesToRetrieve = new ADPropertyDefinition[]
			{
				ADObjectSchema.Id
			};
			ADObjectId configUnitDn = new ADObjectId(this.PageToken.TenantConfigUnitObjectGuid);
			ADRawEntry dnResolve = base.TenantConfigurationSession.ReadADRawEntry(configUnitDn, propertiesToRetrieve);
			configUnitDn = dnResolve.Id;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadSpecialObjects tenant CU DN = {0}", configUnitDn);
			MultiValuedProperty<LinkMetadata> resultValues = new MultiValuedProperty<LinkMetadata>();
			ADRawEntry entry = null;
			long maxEntryUSN = 0L;
			bool done = false;
			while (!done)
			{
				ADPropertyDefinition adpropertyDefinition = RangedPropertyHelper.CreateRangedProperty(ADRecipientSchema.LinkMetadata, new IntRange(rangeStart, rangeEnd));
				List<PropertyDefinition> list = new List<PropertyDefinition>();
				list.Add(adpropertyDefinition);
				list.Add(ADRecipientSchema.UsnChanged);
				entry = base.RootOrgConfigurationSession.RetrieveTenantRelocationSyncObject(exchangeServiceObject, list);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadSpecialObjects object read: {0}", entry.Id.DistinguishedName);
				if ((long)entry[ADRecipientSchema.UsnChanged] < this.PageToken.SpecialObjectsUSN)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<object, long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadSpecialObjects USNChanged({0} < this.PageToken.SpecialObjectsUSN({1})", entry[ADRecipientSchema.UsnChanged], this.PageToken.SpecialObjectsUSN);
					break;
				}
				MultiValuedProperty<LinkMetadata> multiValuedProperty = (MultiValuedProperty<LinkMetadata>)entry[adpropertyDefinition];
				foreach (LinkMetadata linkMetadata in multiValuedProperty)
				{
					if (linkMetadata.LocalUpdateSequenceNumber >= this.PageToken.SpecialObjectsUSN)
					{
						ADObjectId adobjectId = new ADObjectId(linkMetadata.TargetDistinguishedName);
						if (adobjectId.IsDescendantOf(configUnitDn))
						{
							ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadSpecialObjects valid link found: {0}, USN={1}", linkMetadata.TargetDistinguishedName, linkMetadata.LocalUpdateSequenceNumber);
							if (linkMetadata.LocalUpdateSequenceNumber > maxEntryUSN)
							{
								maxEntryUSN = linkMetadata.LocalUpdateSequenceNumber;
							}
							resultValues.Add(linkMetadata);
						}
					}
				}
				if (multiValuedProperty.ValueRange != null && multiValuedProperty.ValueRange.UpperBound != 2147483647)
				{
					rangeStart += numberOfLinksToRead;
					rangeEnd = rangeStart + numberOfLinksToRead - 1;
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<int, int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadSpecialObjects retrieve next page: rangeStart={0}, rangeEnd={1}", rangeStart, rangeEnd);
				}
				else
				{
					done = true;
					ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadSpecialObjects page retrieval ends");
				}
			}
			this.PageToken.SpecialObjectsUSN = ((maxEntryUSN == 0L) ? long.MaxValue : (maxEntryUSN + 1L));
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadSpecialObjects SpecialObjectsUSN is set to {0}", this.PageToken.SpecialObjectsUSN);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.ReadSpecialObjects Number of values found = {0}", resultValues.Count);
			if (resultValues.Count > 0)
			{
				ADPropertyBag propertyBag = new ADPropertyBag();
				propertyBag.SetField(ADObjectSchema.Id, entry.Id);
				propertyBag.SetField(ADRecipientSchema.UsnChanged, entry[ADRecipientSchema.UsnChanged]);
				propertyBag.SetField(ADRecipientSchema.LinkMetadata, resultValues);
				propertyBag.SetField(SyncObjectSchema.Deleted, false);
				TenantRelocationSyncObject specialObject = new TenantRelocationSyncObject(propertyBag, new DirectoryAttribute[0]);
				yield return specialObject;
			}
			yield break;
		}

		// Token: 0x060064BD RID: 25789 RVA: 0x0015F848 File Offset: 0x0015DA48
		internal new static void InitializeConfigurableSettings()
		{
			TenantRelocationSyncConfiguration.ObjectsPerPageLimit = (int)TenantRelocationConfigImpl.GetConfig<uint>("DataSyncObjectsPerPageLimit");
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.InitializeConfigurableSettings Global Config: TenantRelocationSyncConfiguration.ObjectsPerPageLimit = {0}", TenantRelocationSyncConfiguration.ObjectsPerPageLimit);
			TenantRelocationSyncConfiguration.InitialLinkReadSize = (int)TenantRelocationConfigImpl.GetConfig<uint>("DataSyncInitialLinkReadSize");
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.InitializeConfigurableSettings Global Config: TenantRelocationSyncConfiguration.InitialLinkReadSize = {0}", TenantRelocationSyncConfiguration.InitialLinkReadSize);
			TenantRelocationSyncConfiguration.FailoverTimeout = TimeSpan.FromMinutes((double)TenantRelocationConfigImpl.GetConfig<uint>("DataSyncFailoverTimeoutInMinutes"));
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<TimeSpan>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.InitializeConfigurableSettings Global Config: TenantRelocationSyncConfiguration.FailoverTimeout = {0}", TenantRelocationSyncConfiguration.FailoverTimeout);
			TenantRelocationSyncConfiguration.DeltaSyncUsnRangeLimit = (long)((ulong)TenantRelocationConfigImpl.GetConfig<uint>("DeltaSyncUsnRangeLimit"));
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.InitializeConfigurableSettings Global Config: TenantRelocationSyncConfiguration.DeltaSyncUsnRangeLimit = {0}", TenantRelocationSyncConfiguration.DeltaSyncUsnRangeLimit);
		}

		// Token: 0x040042EC RID: 17132
		internal static readonly PropertyDefinition[] BaseProperties;

		// Token: 0x040042ED RID: 17133
		internal new static readonly ADPropertyDefinition InitialLinkMetadataRangedProperty;

		// Token: 0x040042EE RID: 17134
		private static readonly PropertyDefinition[] PropertyDefinitionsForDeletedObjects = new PropertyDefinition[]
		{
			ADObjectSchema.ObjectClass,
			ADRecipientSchema.DisplayName,
			ADObjectSchema.CorrelationIdRaw,
			ADObjectSchema.CorrelationId,
			ADObjectSchema.ExchangeObjectIdRaw,
			ADObjectSchema.ExchangeObjectId,
			ADRecipientSchema.ExternalDirectoryObjectId,
			ADRecipientSchema.UsnChanged,
			TenantRelocationSyncSchema.ObjectId,
			TenantRelocationSyncSchema.LastKnownParent,
			ADRecipientSchema.AttributeMetadata,
			IADSecurityPrincipalSchema.SamAccountName,
			IADSecurityPrincipalSchema.Sid,
			TenantRelocationSyncSchema.Deleted,
			ADRecipientSchema.ConfigurationXMLRaw
		};

		// Token: 0x040042EF RID: 17135
		private bool moreDataForThisPage;

		// Token: 0x040042F0 RID: 17136
		private readonly bool isSmallTenant;
	}
}
