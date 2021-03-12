using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DDC RID: 3548
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SharingSubscriptionData : ISharingSubscriptionData<SharingSubscriptionKey>, ISharingSubscriptionData
	{
		// Token: 0x06007A17 RID: 31255 RVA: 0x0021C50C File Offset: 0x0021A70C
		internal SharingSubscriptionData(string dataType, string sharerIdentity, string sharerName, string remoteFolderId, string remoteFolderName, bool isPrimary, Uri sharerIdentityFederationUri, Uri sharingUrl, StoreObjectId localFolderId, string sharingKey, string subscriberIdentity) : this(null, dataType, sharerIdentity, sharerName, remoteFolderId, remoteFolderName, isPrimary, sharerIdentityFederationUri, sharingUrl, localFolderId, sharingKey, subscriberIdentity)
		{
		}

		// Token: 0x06007A18 RID: 31256 RVA: 0x0021C534 File Offset: 0x0021A734
		internal SharingSubscriptionData(VersionedId id, string dataType, string sharerIdentity, string sharerName, string remoteFolderId, string remoteFolderName, bool isPrimary, Uri sharerIdentityFederationUri, Uri sharingUrl, StoreObjectId localFolderId, string sharingKey, string subscriberIdentity)
		{
			this.Id = id;
			this.DataType = SharingDataType.FromExternalName(dataType);
			this.SharerIdentity = sharerIdentity;
			this.SharerName = sharerName;
			this.RemoteFolderId = remoteFolderId;
			this.RemoteFolderName = remoteFolderName;
			this.IsPrimary = isPrimary;
			this.SharerIdentityFederationUri = sharerIdentityFederationUri;
			this.SharingUrl = sharingUrl;
			this.LocalFolderId = localFolderId;
			this.SharingKey = sharingKey;
			this.SubscriberIdentity = subscriberIdentity;
			this.Key = new SharingSubscriptionKey(this.SharerIdentity, this.RemoteFolderId);
		}

		// Token: 0x170020A3 RID: 8355
		// (get) Token: 0x06007A19 RID: 31257 RVA: 0x0021C5C0 File Offset: 0x0021A7C0
		// (set) Token: 0x06007A1A RID: 31258 RVA: 0x0021C5C8 File Offset: 0x0021A7C8
		public VersionedId Id { get; private set; }

		// Token: 0x170020A4 RID: 8356
		// (get) Token: 0x06007A1B RID: 31259 RVA: 0x0021C5D1 File Offset: 0x0021A7D1
		// (set) Token: 0x06007A1C RID: 31260 RVA: 0x0021C5D9 File Offset: 0x0021A7D9
		public SharingDataType DataType { get; private set; }

		// Token: 0x170020A5 RID: 8357
		// (get) Token: 0x06007A1D RID: 31261 RVA: 0x0021C5E2 File Offset: 0x0021A7E2
		// (set) Token: 0x06007A1E RID: 31262 RVA: 0x0021C5EA File Offset: 0x0021A7EA
		public string SharerIdentity { get; private set; }

		// Token: 0x170020A6 RID: 8358
		// (get) Token: 0x06007A1F RID: 31263 RVA: 0x0021C5F3 File Offset: 0x0021A7F3
		// (set) Token: 0x06007A20 RID: 31264 RVA: 0x0021C5FB File Offset: 0x0021A7FB
		public string SharerName { get; private set; }

		// Token: 0x170020A7 RID: 8359
		// (get) Token: 0x06007A21 RID: 31265 RVA: 0x0021C604 File Offset: 0x0021A804
		// (set) Token: 0x06007A22 RID: 31266 RVA: 0x0021C60C File Offset: 0x0021A80C
		public string RemoteFolderId { get; private set; }

		// Token: 0x170020A8 RID: 8360
		// (get) Token: 0x06007A23 RID: 31267 RVA: 0x0021C615 File Offset: 0x0021A815
		// (set) Token: 0x06007A24 RID: 31268 RVA: 0x0021C61D File Offset: 0x0021A81D
		public string RemoteFolderName { get; private set; }

		// Token: 0x170020A9 RID: 8361
		// (get) Token: 0x06007A25 RID: 31269 RVA: 0x0021C626 File Offset: 0x0021A826
		// (set) Token: 0x06007A26 RID: 31270 RVA: 0x0021C62E File Offset: 0x0021A82E
		public bool IsPrimary { get; private set; }

		// Token: 0x170020AA RID: 8362
		// (get) Token: 0x06007A27 RID: 31271 RVA: 0x0021C637 File Offset: 0x0021A837
		// (set) Token: 0x06007A28 RID: 31272 RVA: 0x0021C63F File Offset: 0x0021A83F
		public string SharingKey { get; private set; }

		// Token: 0x170020AB RID: 8363
		// (get) Token: 0x06007A29 RID: 31273 RVA: 0x0021C648 File Offset: 0x0021A848
		// (set) Token: 0x06007A2A RID: 31274 RVA: 0x0021C650 File Offset: 0x0021A850
		public string SubscriberIdentity { get; private set; }

		// Token: 0x170020AC RID: 8364
		// (get) Token: 0x06007A2B RID: 31275 RVA: 0x0021C659 File Offset: 0x0021A859
		// (set) Token: 0x06007A2C RID: 31276 RVA: 0x0021C661 File Offset: 0x0021A861
		public Uri SharerIdentityFederationUri { get; set; }

		// Token: 0x170020AD RID: 8365
		// (get) Token: 0x06007A2D RID: 31277 RVA: 0x0021C66A File Offset: 0x0021A86A
		// (set) Token: 0x06007A2E RID: 31278 RVA: 0x0021C672 File Offset: 0x0021A872
		public Uri SharingUrl { get; set; }

		// Token: 0x170020AE RID: 8366
		// (get) Token: 0x06007A2F RID: 31279 RVA: 0x0021C67B File Offset: 0x0021A87B
		// (set) Token: 0x06007A30 RID: 31280 RVA: 0x0021C683 File Offset: 0x0021A883
		public StoreObjectId LocalFolderId { get; set; }

		// Token: 0x170020AF RID: 8367
		// (get) Token: 0x06007A31 RID: 31281 RVA: 0x0021C68C File Offset: 0x0021A88C
		// (set) Token: 0x06007A32 RID: 31282 RVA: 0x0021C694 File Offset: 0x0021A894
		public SharingSubscriptionKey Key { get; private set; }

		// Token: 0x06007A33 RID: 31283 RVA: 0x0021C6A0 File Offset: 0x0021A8A0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"DataType=",
				this.DataType,
				", SharerIdentity=",
				this.SharerIdentity,
				", SharerName=",
				this.SharerName,
				", RemoteFolderId=",
				this.RemoteFolderId,
				", RemoteFolderName=",
				this.RemoteFolderName,
				", IsPrimary=",
				this.IsPrimary.ToString(),
				", SharerIdentityFederationUri=",
				this.SharerIdentityFederationUri,
				", SharingUrl=",
				this.SharingUrl,
				", LocalFolderId=",
				this.LocalFolderId,
				", SharingKey=",
				this.SharingKey,
				", SubscriberIdentity=",
				this.SubscriberIdentity
			});
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x0021C78C File Offset: 0x0021A98C
		internal void CopyFrom(SharingSubscriptionData other)
		{
			if (this.Id == null || other.Id != null || !this.Key.Equals(other.Key))
			{
				throw new InvalidOperationException();
			}
			this.DataType = other.DataType;
			this.SharerName = other.SharerName;
			this.RemoteFolderName = other.RemoteFolderName;
			this.IsPrimary = other.IsPrimary;
			this.SharerIdentityFederationUri = other.SharerIdentityFederationUri;
			this.SharingUrl = other.SharingUrl;
			this.SharingKey = other.SharingKey;
			this.SubscriberIdentity = other.SubscriberIdentity;
		}
	}
}
