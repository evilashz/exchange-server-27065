using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.DirSync;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007B8 RID: 1976
	internal class IncrementalSyncConfiguration : SyncConfiguration
	{
		// Token: 0x17002304 RID: 8964
		// (get) Token: 0x06006253 RID: 25171 RVA: 0x00150C82 File Offset: 0x0014EE82
		// (set) Token: 0x06006254 RID: 25172 RVA: 0x00150C8A File Offset: 0x0014EE8A
		internal IMissingPropertyResolver MissingPropertyResolver { get; set; }

		// Token: 0x06006256 RID: 25174 RVA: 0x00150CAC File Offset: 0x0014EEAC
		public IncrementalSyncConfiguration(BackSyncCookie syncCookie, Guid invocationId, OutputResultDelegate writeResult, ISyncEventLogger eventLogger, IExcludedObjectReporter excludedObjectReporter) : base(invocationId, writeResult, eventLogger, excludedObjectReporter)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New IncrementalSyncConfiguration");
			this.syncCookie = syncCookie;
		}

		// Token: 0x06006257 RID: 25175 RVA: 0x00150CD8 File Offset: 0x0014EED8
		protected virtual DateTime GetLastWhenChanged()
		{
			ADRawEntry adrawEntry = (this.MissingPropertyResolver != null) ? this.MissingPropertyResolver.LastProcessedEntry : null;
			if (adrawEntry != null && adrawEntry[ADObjectSchema.WhenChangedUTC] != null)
			{
				return (DateTime)adrawEntry[ADObjectSchema.WhenChangedUTC];
			}
			return this.syncCookie.LastWhenChanged;
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x00150D28 File Offset: 0x0014EF28
		protected virtual void CheckForFullSyncFallback()
		{
			if (this.NewCookie.LastWhenChanged != DateTime.MinValue)
			{
				TimeSpan timeSpan = this.syncCookie.LastWhenChanged - this.NewCookie.LastWhenChanged;
				if (timeSpan >= IncrementalSyncConfiguration.FullSyncDetectionThreshold)
				{
					ExTraceGlobals.BackSyncTracer.TraceWarning<DateTime, DateTime, TimeSpan>((long)SyncConfiguration.TraceId, "IncrementalSyncConfiguration.CheckForFullSyncFallback detected full sync: Previous cookie LastWhenChanged: {0}, current cookie LastWhenChanged: {1}, difference: {2}", this.syncCookie.LastWhenChanged, this.NewCookie.LastWhenChanged, timeSpan);
					if (base.EventLogger != null)
					{
						base.EventLogger.LogFullSyncFallbackDetectedEvent(this.syncCookie, this.NewCookie);
					}
				}
			}
		}

		// Token: 0x17002305 RID: 8965
		// (get) Token: 0x06006259 RID: 25177 RVA: 0x00150DC0 File Offset: 0x0014EFC0
		public override bool MoreData
		{
			get
			{
				return this.NewCookie != null && this.NewCookie.MoreDirSyncData;
			}
		}

		// Token: 0x17002306 RID: 8966
		// (get) Token: 0x0600625A RID: 25178 RVA: 0x00150DD7 File Offset: 0x0014EFD7
		// (set) Token: 0x0600625B RID: 25179 RVA: 0x00150DDF File Offset: 0x0014EFDF
		public BackSyncCookie NewCookie { get; private set; }

		// Token: 0x0600625C RID: 25180 RVA: 0x00150DE8 File Offset: 0x0014EFE8
		public override Exception HandleException(Exception e)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "IncrementalSyncConfiguration.HandleException {0}", e.ToString());
			if (base.IsTransientException(e))
			{
				this.PrepareCookieForFailure();
				this.ReturnErrorCookie();
				return new BackSyncDataSourceTransientException(e);
			}
			return e;
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x00150E22 File Offset: 0x0014F022
		public override byte[] GetResultCookie()
		{
			this.NewCookie.LastWhenChanged = this.GetLastWhenChanged();
			this.CheckForFullSyncFallback();
			if (this.NewCookie == null)
			{
				return null;
			}
			return this.NewCookie.ToByteArray();
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x00150E50 File Offset: 0x0014F050
		public override IEnumerable<ADRawEntry> GetDataPage()
		{
			this.NewCookie = this.syncCookie;
			Guid invocationId;
			bool flag;
			byte[] array;
			ADRawEntry[] dirSyncData = this.GetDirSyncData(out invocationId, out flag, out array);
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "IncrementalSyncConfiguration.GetDataPage result count = {0}", (dirSyncData != null) ? dirSyncData.Length : 0);
			byte[] lastDirSyncCookieWithReplicationVectors = (this.syncCookie != null) ? this.syncCookie.LastDirSyncCookieWithReplicationVectors : null;
			if (!flag)
			{
				lastDirSyncCookieWithReplicationVectors = array;
			}
			this.NewCookie = new BackSyncCookie(DateTime.UtcNow, DateTime.MinValue, DateTime.MinValue, invocationId, flag, array, null, lastDirSyncCookieWithReplicationVectors, this.NewCookie.ServiceInstanceId, this.NewCookie.SequenceId, this.NewCookie.SequenceStartTimestamp);
			return dirSyncData;
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x00150EF4 File Offset: 0x0014F0F4
		public override void CheckIfConnectionAllowed()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "CheckIfConnectionAllowed entering");
			if (!string.IsNullOrEmpty(base.RecipientSession.DomainController))
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "this.RecipientSession.DomainController {0}", base.RecipientSession.DomainController);
				ADServer adserver = base.RootOrgConfigurationSession.FindDCByFqdn(base.RecipientSession.DomainController);
				PartitionId partitionId = base.RootOrgConfigurationSession.SessionSettings.PartitionId;
				if (adserver == null || !ConnectionPoolManager.IsServerInPreferredSite(partitionId.ForestFQDN, adserver))
				{
					ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "DC site {0} not in preferred site list.", (adserver != null) ? adserver.Site.DistinguishedName : "<null>");
					throw new BackSyncDataSourceNotInPreferredSiteException((adserver != null) ? adserver.DistinguishedName : "<null>");
				}
			}
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x00150FC4 File Offset: 0x0014F1C4
		protected virtual void PrepareCookieForFailure()
		{
			DateTime utcNow = DateTime.UtcNow;
			DateTime lastReadFailureStartTime = base.IsSubsequentFailedAttempt() ? this.GetLastReadFailureStartTime() : utcNow;
			Guid guid = this.syncCookie.InvocationId;
			DateTime sequenceStartTimestamp = this.syncCookie.SequenceStartTimestamp;
			Guid sequenceId = this.syncCookie.SequenceId;
			if (base.IsSubsequentFailedAttempt() && base.IsFailoverTimeoutExceeded(utcNow))
			{
				guid = Guid.Empty;
				sequenceId = Guid.NewGuid();
				sequenceStartTimestamp = DateTime.UtcNow;
			}
			bool moreData = this.syncCookie.MoreDirSyncData;
			byte[] rawCookie = this.syncCookie.DirSyncCookie;
			if (guid == Guid.Empty && this.syncCookie.LastDirSyncCookieWithReplicationVectors != null)
			{
				moreData = true;
				rawCookie = this.syncCookie.LastDirSyncCookieWithReplicationVectors;
			}
			this.NewCookie = new BackSyncCookie(utcNow, lastReadFailureStartTime, this.GetLastWhenChanged(), guid, moreData, rawCookie, this.syncCookie.ErrorObjectsAndFailureCounts, this.syncCookie.LastDirSyncCookieWithReplicationVectors, this.syncCookie.ServiceInstanceId, sequenceId, sequenceStartTimestamp);
			base.UpdateSyncCookieErrorObjectsAndFailureCounts(this.NewCookie);
			this.CheckForFullSyncFallback();
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x001510C4 File Offset: 0x0014F2C4
		protected override DateTime GetLastReadFailureStartTime()
		{
			return this.syncCookie.LastReadFailureStartTime;
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x001510D1 File Offset: 0x0014F2D1
		protected override DateTime GetSyncSequenceStartTime()
		{
			return this.syncCookie.SequenceStartTimestamp;
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x001510DE File Offset: 0x0014F2DE
		protected override bool IsDCFailoverResilienceEnabled()
		{
			return SyncConfiguration.EnableDCFailoverResilienceForIncrementalSync();
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x001510EC File Offset: 0x0014F2EC
		protected virtual QueryFilter GetDirSyncQueryFilter()
		{
			return new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADUser.MostDerivedClass),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADGroup.MostDerivedClass),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADContact.MostDerivedClass),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADOrganizationalUnit.MostDerivedClass)
			});
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x00151154 File Offset: 0x0014F354
		protected virtual ADRawEntry[] GetDirSyncData(out Guid invocationId, out bool moreData, out byte[] dirSyncCookie)
		{
			QueryFilter dirSyncQueryFilter = this.GetDirSyncQueryFilter();
			IEnumerable<PropertyDefinition> allBackSyncProperties = SyncSchema.Instance.AllBackSyncProperties;
			IEnumerable<PropertyDefinition> allShadowProperties = SyncSchema.Instance.AllShadowProperties;
			IEnumerable<PropertyDefinition> enumerable = allBackSyncProperties.Concat(allShadowProperties);
			if (ExTraceGlobals.BackSyncTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				int num = 0;
				foreach (PropertyDefinition propertyDefinition in enumerable)
				{
					ExTraceGlobals.BackSyncTracer.TraceDebug<int, string>((long)SyncConfiguration.TraceId, "incrementalBacksyncProperties[{0}] {1}", num++, propertyDefinition.Name);
				}
			}
			ADSessionSettings adsessionSettings = ADSessionSettings.SessionSettingsFactory.Default.FromAccountPartitionWideScopeSet(base.RecipientSession.SessionSettings.PartitionId);
			adsessionSettings.IncludeSoftDeletedObjects = true;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.RecipientSession.DomainController, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 339, "GetDirSyncData", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\BackSync\\Configuration\\IncrementalSyncConfiguration.cs");
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = false;
			ADDirSyncReader<BackSyncRecipient> addirSyncReader = new ADDirSyncReader<BackSyncRecipient>(tenantOrRootOrgRecipientSession, dirSyncQueryFilter, enumerable)
			{
				Cookie = this.syncCookie.DirSyncCookie
			};
			ADRawEntry[] nextResultPage = addirSyncReader.GetNextResultPage();
			invocationId = SyncConfiguration.FindInvocationIdByFqdn(addirSyncReader.PreferredServerName, base.RecipientSession.SessionSettings.PartitionId);
			moreData = (addirSyncReader.MorePagesAvailable != null && addirSyncReader.MorePagesAvailable.Value);
			dirSyncCookie = addirSyncReader.Cookie;
			return nextResultPage;
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x001512C8 File Offset: 0x0014F4C8
		protected virtual void ReturnErrorCookie()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "ReturnErrorCookie entering");
			base.WriteResult(this.NewCookie.ToByteArray(), SyncObject.CreateGetChangesResponse(new List<SyncObject>(), this.NewCookie.MoreDirSyncData, this.NewCookie.ToByteArray(), this.NewCookie.ServiceInstanceId));
		}

		// Token: 0x040041DF RID: 16863
		private const string FullSyncDetectionThresholdValueName = "FullSyncDetectionThreshold";

		// Token: 0x040041E0 RID: 16864
		private const int DefaultFullSyncDetectionThreshold = 30;

		// Token: 0x040041E1 RID: 16865
		private readonly BackSyncCookie syncCookie;

		// Token: 0x040041E2 RID: 16866
		private static readonly TimeSpan FullSyncDetectionThreshold = TimeSpan.FromDays((double)SyncConfiguration.GetConfigurationValue<int>("FullSyncDetectionThreshold", 30));
	}
}
