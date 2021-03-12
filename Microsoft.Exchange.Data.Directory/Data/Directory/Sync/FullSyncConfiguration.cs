using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007B7 RID: 1975
	internal abstract class FullSyncConfiguration : SyncConfiguration
	{
		// Token: 0x170022FD RID: 8957
		// (get) Token: 0x06006239 RID: 25145 RVA: 0x001508D4 File Offset: 0x0014EAD4
		// (set) Token: 0x0600623A RID: 25146 RVA: 0x001508DB File Offset: 0x0014EADB
		private protected static int ObjectsPerPageLimit { protected get; private set; }

		// Token: 0x170022FE RID: 8958
		// (get) Token: 0x0600623B RID: 25147 RVA: 0x001508E3 File Offset: 0x0014EAE3
		// (set) Token: 0x0600623C RID: 25148 RVA: 0x001508EA File Offset: 0x0014EAEA
		private protected static int LinksPerPageLimit { protected get; private set; }

		// Token: 0x170022FF RID: 8959
		// (get) Token: 0x0600623D RID: 25149 RVA: 0x001508F2 File Offset: 0x0014EAF2
		// (set) Token: 0x0600623E RID: 25150 RVA: 0x001508F9 File Offset: 0x0014EAF9
		public static int InitialLinkReadSize { get; private set; }

		// Token: 0x17002300 RID: 8960
		// (get) Token: 0x0600623F RID: 25151 RVA: 0x00150901 File Offset: 0x0014EB01
		// (set) Token: 0x06006240 RID: 25152 RVA: 0x00150909 File Offset: 0x0014EB09
		protected int ReturnedLinkCount { get; set; }

		// Token: 0x17002301 RID: 8961
		// (get) Token: 0x06006241 RID: 25153 RVA: 0x00150912 File Offset: 0x0014EB12
		// (set) Token: 0x06006242 RID: 25154 RVA: 0x0015091A File Offset: 0x0014EB1A
		protected int ReturnedObjectCount { get; set; }

		// Token: 0x17002302 RID: 8962
		// (get) Token: 0x06006243 RID: 25155 RVA: 0x00150923 File Offset: 0x0014EB23
		// (set) Token: 0x06006244 RID: 25156 RVA: 0x0015092B File Offset: 0x0014EB2B
		public IFullSyncPageToken FullSyncPageToken { get; private set; }

		// Token: 0x06006245 RID: 25157 RVA: 0x00150934 File Offset: 0x0014EB34
		static FullSyncConfiguration()
		{
			FullSyncConfiguration.InitializeConfigurableSettings();
			FullSyncConfiguration.InitialLinkMetadataRangedProperty = RangedPropertyHelper.CreateRangedProperty(ADRecipientSchema.LinkMetadata, new IntRange(0, FullSyncConfiguration.InitialLinkReadSize - 1));
			List<PropertyDefinition> list = new List<PropertyDefinition>(SyncSchema.Instance.AllBackSyncBaseProperties.Cast<PropertyDefinition>());
			list.AddRange(SyncObject.BackSyncProperties.Cast<PropertyDefinition>());
			list.AddRange(SyncSchema.Instance.AllBackSyncShadowBaseProperties.Cast<PropertyDefinition>());
			FullSyncConfiguration.backSyncBaseProperties = list.ToArray();
			list.Add(FullSyncConfiguration.InitialLinkMetadataRangedProperty);
			list.Add(ADRecipientSchema.UsnChanged);
			FullSyncConfiguration.backSyncBasePropertiesPlusLinks = list.ToArray();
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x001509C8 File Offset: 0x0014EBC8
		internal static void InitializeConfigurableSettings()
		{
			FullSyncConfiguration.ObjectsPerPageLimit = SyncConfiguration.GetConfigurationValue<int>("ObjectsPerPageLimit", 200);
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "FullSyncConfiguration.InitializeConfigurableSettings FullSyncConfiguration.ObjectsPerPageLimit = {0}", FullSyncConfiguration.ObjectsPerPageLimit);
			FullSyncConfiguration.LinksPerPageLimit = SyncConfiguration.GetConfigurationValue<int>("LinksPerPageLimit", 2000);
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "FullSyncConfiguration.InitializeConfigurableSettings FullSyncConfiguration.LinksPerPageLimit = {0}", FullSyncConfiguration.LinksPerPageLimit);
			FullSyncConfiguration.InitialLinkReadSize = SyncConfiguration.GetConfigurationValue<int>("InitialLinkReadSize", 100);
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "FullSyncConfiguration.InitializeConfigurableSettings FullSyncConfiguration.InitialLinkReadSize = {0}", FullSyncConfiguration.InitialLinkReadSize);
		}

		// Token: 0x06006247 RID: 25159 RVA: 0x00150A5C File Offset: 0x0014EC5C
		public virtual DirectoryObjectError[] GetReportedErrors()
		{
			return null;
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x00150A5F File Offset: 0x0014EC5F
		public FullSyncConfiguration(IFullSyncPageToken pageToken, Guid invocationId, OutputResultDelegate writeResult, ISyncEventLogger eventLogger, IExcludedObjectReporter excludedObjectReporter, IFullSyncPageToken originalToken) : base(invocationId, writeResult, eventLogger, excludedObjectReporter)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New FullSyncConfiguration");
			this.FullSyncPageToken = pageToken;
			this.originalToken = originalToken;
		}

		// Token: 0x17002303 RID: 8963
		// (get) Token: 0x06006249 RID: 25161 RVA: 0x00150A91 File Offset: 0x0014EC91
		public override bool MoreData
		{
			get
			{
				return this.FullSyncPageToken.MoreData;
			}
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x00150A9E File Offset: 0x0014EC9E
		public override byte[] GetResultCookie()
		{
			return this.FullSyncPageToken.ToByteArray();
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x00150AAC File Offset: 0x0014ECAC
		public override Exception HandleException(Exception e)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "FullSyncConfiguration HandleException");
			DateTime utcNow = DateTime.UtcNow;
			if (e is ADExternalException || (base.IsSubsequentFailedAttempt() && base.IsFailoverTimeoutExceeded(utcNow)))
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "FullSyncConfiguration throw BackSyncDataSourceUnavailableException for exception {0}", e.ToString());
				return new BackSyncDataSourceUnavailableException(e);
			}
			if (base.IsTransientException(e))
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Handle transient exception");
				this.originalToken.PrepareForFailover();
				base.UpdateSyncCookieErrorObjectsAndFailureCounts(this.originalToken);
				this.ReturnErrorPageToken(utcNow, this.originalToken);
				return new BackSyncDataSourceTransientException(e);
			}
			return e;
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x00150B5C File Offset: 0x0014ED5C
		protected static bool NotAllLinksRetrieved(MultiValuedProperty<LinkMetadata> linkMetadata)
		{
			bool flag = linkMetadata.ValueRange != null && !linkMetadata.ValueRange.Equals(RangedPropertyHelper.AllValuesRange);
			ExTraceGlobals.BackSyncTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "NotAllLinksRetrieved = {0}", flag);
			return flag;
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x00150BA5 File Offset: 0x0014EDA5
		protected static PropertyDefinition[] GetPropertyDefinitions(bool includeLinks)
		{
			if (!includeLinks)
			{
				return FullSyncConfiguration.backSyncBaseProperties;
			}
			return FullSyncConfiguration.backSyncBasePropertiesPlusLinks;
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x00150BB5 File Offset: 0x0014EDB5
		protected override DateTime GetLastReadFailureStartTime()
		{
			return this.FullSyncPageToken.LastReadFailureStartTime;
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x00150BC2 File Offset: 0x0014EDC2
		protected override DateTime GetSyncSequenceStartTime()
		{
			return this.FullSyncPageToken.SequenceStartTimestamp;
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x00150BCF File Offset: 0x0014EDCF
		protected override bool IsDCFailoverResilienceEnabled()
		{
			return SyncConfiguration.EnableDCFailoverResilienceForTenantFullSync();
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x00150BDC File Offset: 0x0014EDDC
		protected void ReturnErrorPageToken(DateTime now, IFullSyncPageToken pageToken)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "FullSyncConfiguration ReturnErrorPageToken");
			DateTime dateTime = base.IsSubsequentFailedAttempt() ? this.GetLastReadFailureStartTime() : now;
			ExTraceGlobals.BackSyncTracer.TraceDebug<DateTime>((long)SyncConfiguration.TraceId, "FullSyncConfiguration lastReadFailureStartTime {0}", dateTime);
			this.originalToken.Timestamp = now;
			this.originalToken.LastReadFailureStartTime = dateTime;
			byte[] array = pageToken.ToByteArray();
			base.WriteResult(array, SyncObject.CreateGetDirectoryObjectsResponse(new List<SyncObject>(), true, array, this.GetReportedErrors(), null));
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x00150C64 File Offset: 0x0014EE64
		protected virtual bool ShouldReadMoreData()
		{
			return this.ReturnedObjectCount < FullSyncConfiguration.ObjectsPerPageLimit && this.ReturnedLinkCount < FullSyncConfiguration.LinksPerPageLimit;
		}

		// Token: 0x040041CF RID: 16847
		private const string ObjectsPerPageLimitValueName = "ObjectsPerPageLimit";

		// Token: 0x040041D0 RID: 16848
		private const string LinksPerPageLimitValueName = "LinksPerPageLimit";

		// Token: 0x040041D1 RID: 16849
		private const string InitialLinkReadSizeValueName = "InitialLinkReadSize";

		// Token: 0x040041D2 RID: 16850
		internal const int DefaultInitialLinkReadSize = 100;

		// Token: 0x040041D3 RID: 16851
		private const int DefaultObjectsPerPageLimit = 200;

		// Token: 0x040041D4 RID: 16852
		private const int DefaultLinksPerPageLimit = 2000;

		// Token: 0x040041D5 RID: 16853
		private static readonly PropertyDefinition[] backSyncBaseProperties;

		// Token: 0x040041D6 RID: 16854
		private static readonly PropertyDefinition[] backSyncBasePropertiesPlusLinks;

		// Token: 0x040041D7 RID: 16855
		protected static readonly ADPropertyDefinition InitialLinkMetadataRangedProperty;

		// Token: 0x040041D8 RID: 16856
		private IFullSyncPageToken originalToken;
	}
}
