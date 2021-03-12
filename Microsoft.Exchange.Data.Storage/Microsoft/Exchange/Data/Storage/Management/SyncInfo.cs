using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A85 RID: 2693
	[Serializable]
	public sealed class SyncInfo : ConfigurableObject
	{
		// Token: 0x06006289 RID: 25225 RVA: 0x001A025C File Offset: 0x0019E45C
		public SyncInfo(string displayName, string url) : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			if (string.IsNullOrEmpty(displayName))
			{
				throw new ArgumentNullException("displayName");
			}
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = new SyncInfoId();
			this.propertyBag.ResetChangeTracking();
			this.DisplayName = displayName;
			this.Url = url;
		}

		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x0600628A RID: 25226 RVA: 0x001A02C5 File Offset: 0x0019E4C5
		// (set) Token: 0x0600628B RID: 25227 RVA: 0x001A02D7 File Offset: 0x0019E4D7
		public string DisplayName
		{
			get
			{
				return (string)this[SyncInfoSchema.DisplayName];
			}
			private set
			{
				this[SyncInfoSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x0600628C RID: 25228 RVA: 0x001A02E5 File Offset: 0x0019E4E5
		// (set) Token: 0x0600628D RID: 25229 RVA: 0x001A02F7 File Offset: 0x0019E4F7
		public string Url
		{
			get
			{
				return (string)this[SyncInfoSchema.Url];
			}
			internal set
			{
				this[SyncInfoSchema.Url] = value;
			}
		}

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x0600628E RID: 25230 RVA: 0x001A0305 File Offset: 0x0019E505
		// (set) Token: 0x0600628F RID: 25231 RVA: 0x001A0317 File Offset: 0x0019E517
		public string LastSyncFailure
		{
			get
			{
				return (string)this[SyncInfoSchema.LastSyncFailure];
			}
			internal set
			{
				this[SyncInfoSchema.LastSyncFailure] = value;
			}
		}

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x06006290 RID: 25232 RVA: 0x001A0325 File Offset: 0x0019E525
		// (set) Token: 0x06006291 RID: 25233 RVA: 0x001A0337 File Offset: 0x0019E537
		public ExDateTime? FirstAttemptedSyncTime
		{
			get
			{
				return (ExDateTime?)this[SyncInfoSchema.FirstAttemptedSyncTime];
			}
			internal set
			{
				this[SyncInfoSchema.FirstAttemptedSyncTime] = value;
			}
		}

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x06006292 RID: 25234 RVA: 0x001A034A File Offset: 0x0019E54A
		// (set) Token: 0x06006293 RID: 25235 RVA: 0x001A035C File Offset: 0x0019E55C
		public ExDateTime? LastAttemptedSyncTime
		{
			get
			{
				return (ExDateTime?)this[SyncInfoSchema.LastAttemptedSyncTime];
			}
			internal set
			{
				this[SyncInfoSchema.LastAttemptedSyncTime] = value;
			}
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x06006294 RID: 25236 RVA: 0x001A036F File Offset: 0x0019E56F
		// (set) Token: 0x06006295 RID: 25237 RVA: 0x001A0381 File Offset: 0x0019E581
		public ExDateTime? LastSuccessfulSyncTime
		{
			get
			{
				return (ExDateTime?)this[SyncInfoSchema.LastSuccessfulSyncTime];
			}
			internal set
			{
				this[SyncInfoSchema.LastSuccessfulSyncTime] = value;
			}
		}

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x06006296 RID: 25238 RVA: 0x001A0394 File Offset: 0x0019E594
		// (set) Token: 0x06006297 RID: 25239 RVA: 0x001A03A6 File Offset: 0x0019E5A6
		public ExDateTime? LastFailedSyncTime
		{
			get
			{
				return (ExDateTime?)this[SyncInfoSchema.LastFailedSyncTime];
			}
			internal set
			{
				this[SyncInfoSchema.LastFailedSyncTime] = value;
			}
		}

		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x06006298 RID: 25240 RVA: 0x001A03B9 File Offset: 0x0019E5B9
		// (set) Token: 0x06006299 RID: 25241 RVA: 0x001A03CB File Offset: 0x0019E5CB
		public ExDateTime? LastFailedSyncEmailTime
		{
			get
			{
				return (ExDateTime?)this[SyncInfoSchema.LastFailedSyncEmailTime];
			}
			internal set
			{
				this[SyncInfoSchema.LastFailedSyncEmailTime] = value;
			}
		}

		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x0600629A RID: 25242 RVA: 0x001A03DE File Offset: 0x0019E5DE
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return SyncInfo.schema;
			}
		}

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x0600629B RID: 25243 RVA: 0x001A03E5 File Offset: 0x0019E5E5
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x040037CB RID: 14283
		private static readonly SyncInfoSchema schema = ObjectSchema.GetInstance<SyncInfoSchema>();
	}
}
