using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DE1 RID: 3553
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SubscribeResults
	{
		// Token: 0x06007A41 RID: 31297 RVA: 0x0021CEC9 File Offset: 0x0021B0C9
		public SubscribeResults(SharingDataType dataType, string initiatorSmtpAddress, string initiatorName, string remoteFolderName, StoreObjectId localFolderId, bool localFolderCreated, string localFolderName)
		{
			this.DataType = dataType;
			this.InitiatorSmtpAddress = initiatorSmtpAddress;
			this.InitiatorName = initiatorName;
			this.RemoteFolderName = remoteFolderName;
			this.LocalFolderId = localFolderId;
			this.LocalFolderCreated = localFolderCreated;
			this.LocalFolderName = localFolderName;
		}

		// Token: 0x170020B2 RID: 8370
		// (get) Token: 0x06007A42 RID: 31298 RVA: 0x0021CF06 File Offset: 0x0021B106
		// (set) Token: 0x06007A43 RID: 31299 RVA: 0x0021CF0E File Offset: 0x0021B10E
		public SharingDataType DataType { get; private set; }

		// Token: 0x170020B3 RID: 8371
		// (get) Token: 0x06007A44 RID: 31300 RVA: 0x0021CF17 File Offset: 0x0021B117
		// (set) Token: 0x06007A45 RID: 31301 RVA: 0x0021CF1F File Offset: 0x0021B11F
		public string InitiatorSmtpAddress { get; private set; }

		// Token: 0x170020B4 RID: 8372
		// (get) Token: 0x06007A46 RID: 31302 RVA: 0x0021CF28 File Offset: 0x0021B128
		// (set) Token: 0x06007A47 RID: 31303 RVA: 0x0021CF30 File Offset: 0x0021B130
		public string InitiatorName { get; private set; }

		// Token: 0x170020B5 RID: 8373
		// (get) Token: 0x06007A48 RID: 31304 RVA: 0x0021CF39 File Offset: 0x0021B139
		// (set) Token: 0x06007A49 RID: 31305 RVA: 0x0021CF41 File Offset: 0x0021B141
		public string RemoteFolderName { get; private set; }

		// Token: 0x170020B6 RID: 8374
		// (get) Token: 0x06007A4A RID: 31306 RVA: 0x0021CF4A File Offset: 0x0021B14A
		// (set) Token: 0x06007A4B RID: 31307 RVA: 0x0021CF52 File Offset: 0x0021B152
		public StoreObjectId LocalFolderId { get; private set; }

		// Token: 0x170020B7 RID: 8375
		// (get) Token: 0x06007A4C RID: 31308 RVA: 0x0021CF5B File Offset: 0x0021B15B
		// (set) Token: 0x06007A4D RID: 31309 RVA: 0x0021CF63 File Offset: 0x0021B163
		public string LocalFolderName { get; private set; }

		// Token: 0x170020B8 RID: 8376
		// (get) Token: 0x06007A4E RID: 31310 RVA: 0x0021CF6C File Offset: 0x0021B16C
		// (set) Token: 0x06007A4F RID: 31311 RVA: 0x0021CF74 File Offset: 0x0021B174
		public bool LocalFolderCreated { get; private set; }

		// Token: 0x06007A50 RID: 31312 RVA: 0x0021CF80 File Offset: 0x0021B180
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"DataType=",
				this.DataType,
				", InitiatorSmtpAddress=",
				this.InitiatorSmtpAddress,
				", InitiatorName=",
				this.InitiatorName,
				", RemoteFolderName=",
				this.RemoteFolderName
			});
		}
	}
}
