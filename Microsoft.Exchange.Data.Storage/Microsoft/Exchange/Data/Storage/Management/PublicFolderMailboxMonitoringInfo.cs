using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A94 RID: 2708
	[Serializable]
	public abstract class PublicFolderMailboxMonitoringInfo : ConfigurableObject
	{
		// Token: 0x0600631E RID: 25374 RVA: 0x001A2698 File Offset: 0x001A0898
		public PublicFolderMailboxMonitoringInfo() : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = new PublicFolderMailboxDiagnosticsInfoId();
			this.DisplayName = "Public Folder Diagnostics Information";
			this.propertyBag.ResetChangeTracking();
		}

		// Token: 0x17001B7A RID: 7034
		// (get) Token: 0x0600631F RID: 25375 RVA: 0x001A26EB File Offset: 0x001A08EB
		// (set) Token: 0x06006320 RID: 25376 RVA: 0x001A26FD File Offset: 0x001A08FD
		public string DisplayName
		{
			get
			{
				return (string)this[PublicFolderMailboxMonitoringInfoSchema.DisplayName];
			}
			private set
			{
				this[PublicFolderMailboxMonitoringInfoSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001B7B RID: 7035
		// (get) Token: 0x06006321 RID: 25377 RVA: 0x001A270B File Offset: 0x001A090B
		// (set) Token: 0x06006322 RID: 25378 RVA: 0x001A271D File Offset: 0x001A091D
		public string LastSyncFailure
		{
			get
			{
				return (string)this[PublicFolderMailboxMonitoringInfoSchema.LastSyncFailure];
			}
			internal set
			{
				this[PublicFolderMailboxMonitoringInfoSchema.LastSyncFailure] = value;
			}
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x06006323 RID: 25379 RVA: 0x001A272B File Offset: 0x001A092B
		// (set) Token: 0x06006324 RID: 25380 RVA: 0x001A273D File Offset: 0x001A093D
		public ExDateTime? LastAttemptedSyncTime
		{
			get
			{
				return (ExDateTime?)this[PublicFolderMailboxMonitoringInfoSchema.LastAttemptedSyncTime];
			}
			internal set
			{
				this[PublicFolderMailboxMonitoringInfoSchema.LastAttemptedSyncTime] = value;
			}
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x06006325 RID: 25381 RVA: 0x001A2750 File Offset: 0x001A0950
		// (set) Token: 0x06006326 RID: 25382 RVA: 0x001A2762 File Offset: 0x001A0962
		public ExDateTime? LastSuccessfulSyncTime
		{
			get
			{
				return (ExDateTime?)this[PublicFolderMailboxMonitoringInfoSchema.LastSuccessfulSyncTime];
			}
			internal set
			{
				this[PublicFolderMailboxMonitoringInfoSchema.LastSuccessfulSyncTime] = value;
			}
		}

		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x06006327 RID: 25383 RVA: 0x001A2775 File Offset: 0x001A0975
		// (set) Token: 0x06006328 RID: 25384 RVA: 0x001A2787 File Offset: 0x001A0987
		public ExDateTime? LastFailedSyncTime
		{
			get
			{
				return (ExDateTime?)this[PublicFolderMailboxMonitoringInfoSchema.LastFailedSyncTime];
			}
			internal set
			{
				this[PublicFolderMailboxMonitoringInfoSchema.LastFailedSyncTime] = value;
			}
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x06006329 RID: 25385 RVA: 0x001A279A File Offset: 0x001A099A
		// (set) Token: 0x0600632A RID: 25386 RVA: 0x001A27AC File Offset: 0x001A09AC
		public int? NumberofAttemptsAfterLastSuccess
		{
			get
			{
				return (int?)this[PublicFolderMailboxMonitoringInfoSchema.NumberofAttemptsAfterLastSuccess];
			}
			internal set
			{
				this[PublicFolderMailboxMonitoringInfoSchema.NumberofAttemptsAfterLastSuccess] = value;
			}
		}

		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x0600632B RID: 25387 RVA: 0x001A27BF File Offset: 0x001A09BF
		// (set) Token: 0x0600632C RID: 25388 RVA: 0x001A27D1 File Offset: 0x001A09D1
		public ExDateTime? FirstFailedSyncTimeAfterLastSuccess
		{
			get
			{
				return (ExDateTime?)this[PublicFolderMailboxMonitoringInfoSchema.FirstFailedSyncTimeAfterLastSuccess];
			}
			internal set
			{
				this[PublicFolderMailboxMonitoringInfoSchema.FirstFailedSyncTimeAfterLastSuccess] = value;
			}
		}

		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x0600632D RID: 25389 RVA: 0x001A27E4 File Offset: 0x001A09E4
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return PublicFolderMailboxMonitoringInfo.schema;
			}
		}

		// Token: 0x17001B82 RID: 7042
		// (get) Token: 0x0600632E RID: 25390 RVA: 0x001A27EB File Offset: 0x001A09EB
		// (set) Token: 0x0600632F RID: 25391 RVA: 0x001A27FD File Offset: 0x001A09FD
		public string LastSyncCycleLog
		{
			get
			{
				return (string)this[PublicFolderMailboxMonitoringInfoSchema.LastSyncCycleLog];
			}
			internal set
			{
				this[PublicFolderMailboxMonitoringInfoSchema.LastSyncCycleLog] = value;
			}
		}

		// Token: 0x17001B83 RID: 7043
		// (get) Token: 0x06006330 RID: 25392 RVA: 0x001A280B File Offset: 0x001A0A0B
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04003806 RID: 14342
		internal const string DiagnosticsInfoDisplayName = "Public Folder Diagnostics Information";

		// Token: 0x04003807 RID: 14343
		private static readonly PublicFolderMailboxMonitoringInfoSchema schema = ObjectSchema.GetInstance<PublicFolderMailboxMonitoringInfoSchema>();
	}
}
