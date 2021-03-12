using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DB2 RID: 3506
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublishingSubscriptionData : ISharingSubscriptionData<PublishingSubscriptionKey>, ISharingSubscriptionData
	{
		// Token: 0x0600787F RID: 30847 RVA: 0x00214173 File Offset: 0x00212373
		internal PublishingSubscriptionData(string dataType, Uri publishingUrl, string remoteFolderName, StoreObjectId localFolderId) : this(null, dataType, publishingUrl, remoteFolderName, localFolderId)
		{
		}

		// Token: 0x06007880 RID: 30848 RVA: 0x00214184 File Offset: 0x00212384
		internal PublishingSubscriptionData(VersionedId id, string dataType, Uri publishingUrl, string remoteFolderName, StoreObjectId localFolderId)
		{
			this.Id = id;
			this.DataType = SharingDataType.FromPublishName(dataType);
			this.PublishingUrl = publishingUrl;
			this.RemoteFolderName = remoteFolderName;
			this.LocalFolderId = localFolderId;
			this.Key = new PublishingSubscriptionKey(this.PublishingUrl);
		}

		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x06007881 RID: 30849 RVA: 0x002141D2 File Offset: 0x002123D2
		// (set) Token: 0x06007882 RID: 30850 RVA: 0x002141DA File Offset: 0x002123DA
		public VersionedId Id { get; private set; }

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x06007883 RID: 30851 RVA: 0x002141E3 File Offset: 0x002123E3
		// (set) Token: 0x06007884 RID: 30852 RVA: 0x002141EB File Offset: 0x002123EB
		public SharingDataType DataType { get; private set; }

		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x06007885 RID: 30853 RVA: 0x002141F4 File Offset: 0x002123F4
		// (set) Token: 0x06007886 RID: 30854 RVA: 0x002141FC File Offset: 0x002123FC
		public Uri PublishingUrl { get; private set; }

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x06007887 RID: 30855 RVA: 0x00214205 File Offset: 0x00212405
		// (set) Token: 0x06007888 RID: 30856 RVA: 0x0021420D File Offset: 0x0021240D
		public string RemoteFolderName { get; private set; }

		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x06007889 RID: 30857 RVA: 0x00214216 File Offset: 0x00212416
		// (set) Token: 0x0600788A RID: 30858 RVA: 0x0021421E File Offset: 0x0021241E
		public StoreObjectId LocalFolderId { get; set; }

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x0600788B RID: 30859 RVA: 0x00214227 File Offset: 0x00212427
		// (set) Token: 0x0600788C RID: 30860 RVA: 0x0021422F File Offset: 0x0021242F
		public PublishingSubscriptionKey Key { get; private set; }

		// Token: 0x17002041 RID: 8257
		// (get) Token: 0x0600788D RID: 30861 RVA: 0x00214238 File Offset: 0x00212438
		public bool UrlNeedsExpansion
		{
			get
			{
				return this.PublishingUrl != null && this.PublishingUrl.Scheme == "holidays";
			}
		}

		// Token: 0x0600788E RID: 30862 RVA: 0x00214260 File Offset: 0x00212460
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"DataType=",
				this.DataType,
				", PublishingUrl=",
				this.PublishingUrl.OriginalString,
				", RemoteFolderName=",
				this.RemoteFolderName,
				", LocalFolderId=",
				this.LocalFolderId
			});
		}
	}
}
