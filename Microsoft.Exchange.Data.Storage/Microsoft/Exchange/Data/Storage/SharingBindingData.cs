using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DC1 RID: 3521
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SharingBindingData
	{
		// Token: 0x060078E7 RID: 30951 RVA: 0x00216078 File Offset: 0x00214278
		public static SharingBindingData CreateSharingBindingData(SharingDataType dataType, string initiatorName, string initiatorSmtpAddress, string remoteFolderName, string remoteFolderId, string localFolderName, StoreObjectId localFolderId, bool isDefaultFolderShared)
		{
			Util.ThrowOnNullArgument(dataType, "dataType");
			Util.ThrowOnNullOrEmptyArgument(initiatorName, "initiatorName");
			if (!SmtpAddress.IsValidSmtpAddress(initiatorSmtpAddress))
			{
				throw new ArgumentException("initiatorSmtpAddress");
			}
			Util.ThrowOnNullOrEmptyArgument(remoteFolderName, "remoteFolderName");
			Util.ThrowOnNullOrEmptyArgument(remoteFolderId, "remoteFolderId");
			Util.ThrowOnNullOrEmptyArgument(localFolderName, "localFolderName");
			Util.ThrowOnNullArgument(localFolderId, "localFolderId");
			return new SharingBindingData(null, dataType, initiatorName, initiatorSmtpAddress, remoteFolderName, remoteFolderId, localFolderName, localFolderId, isDefaultFolderShared, null);
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x002160F8 File Offset: 0x002142F8
		internal SharingBindingData(VersionedId itemId, SharingDataType dataType, string initiatorName, string initiatorSmtpAddress, string remoteFolderName, string remoteFolderId, string localFolderName, StoreObjectId localFolderId, bool isDefaultFolderShared, DateTime? lastSyncTimeUtc)
		{
			this.Id = itemId;
			this.DataType = dataType;
			this.InitiatorName = initiatorName;
			this.InitiatorSmtpAddress = initiatorSmtpAddress;
			this.RemoteFolderName = remoteFolderName;
			this.RemoteFolderId = remoteFolderId;
			this.LocalFolderName = localFolderName;
			this.LocalFolderId = localFolderId;
			this.IsDefaultFolderShared = isDefaultFolderShared;
			this.LastSyncTimeUtc = lastSyncTimeUtc;
		}

		// Token: 0x060078E9 RID: 30953 RVA: 0x00216158 File Offset: 0x00214358
		internal static bool EqualContent(SharingBindingData left, SharingBindingData right)
		{
			return left != null && right != null && left.InitiatorName == right.InitiatorName && left.InitiatorSmtpAddress == right.InitiatorSmtpAddress && left.IsDefaultFolderShared == right.IsDefaultFolderShared && object.Equals(left.LastSyncTimeUtc, right.LastSyncTimeUtc) && object.Equals(left.LocalFolderId, right.LocalFolderId) && left.LocalFolderName == right.LocalFolderName && left.RemoteFolderName == right.RemoteFolderName && left.RemoteFolderId == right.RemoteFolderId;
		}

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x060078EA RID: 30954 RVA: 0x00216211 File Offset: 0x00214411
		// (set) Token: 0x060078EB RID: 30955 RVA: 0x00216219 File Offset: 0x00214419
		public VersionedId Id { get; private set; }

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x060078EC RID: 30956 RVA: 0x00216222 File Offset: 0x00214422
		// (set) Token: 0x060078ED RID: 30957 RVA: 0x0021622A File Offset: 0x0021442A
		public SharingDataType DataType { get; private set; }

		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x060078EE RID: 30958 RVA: 0x00216233 File Offset: 0x00214433
		// (set) Token: 0x060078EF RID: 30959 RVA: 0x0021623B File Offset: 0x0021443B
		public string InitiatorName { get; private set; }

		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x060078F0 RID: 30960 RVA: 0x00216244 File Offset: 0x00214444
		// (set) Token: 0x060078F1 RID: 30961 RVA: 0x0021624C File Offset: 0x0021444C
		public string InitiatorSmtpAddress { get; private set; }

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x060078F2 RID: 30962 RVA: 0x00216255 File Offset: 0x00214455
		// (set) Token: 0x060078F3 RID: 30963 RVA: 0x0021625D File Offset: 0x0021445D
		public string RemoteFolderName { get; private set; }

		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x060078F4 RID: 30964 RVA: 0x00216266 File Offset: 0x00214466
		// (set) Token: 0x060078F5 RID: 30965 RVA: 0x0021626E File Offset: 0x0021446E
		public string RemoteFolderId { get; private set; }

		// Token: 0x17002058 RID: 8280
		// (get) Token: 0x060078F6 RID: 30966 RVA: 0x00216277 File Offset: 0x00214477
		// (set) Token: 0x060078F7 RID: 30967 RVA: 0x0021627F File Offset: 0x0021447F
		public string LocalFolderName { get; private set; }

		// Token: 0x17002059 RID: 8281
		// (get) Token: 0x060078F8 RID: 30968 RVA: 0x00216288 File Offset: 0x00214488
		// (set) Token: 0x060078F9 RID: 30969 RVA: 0x00216290 File Offset: 0x00214490
		public StoreObjectId LocalFolderId { get; set; }

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x060078FA RID: 30970 RVA: 0x00216299 File Offset: 0x00214499
		// (set) Token: 0x060078FB RID: 30971 RVA: 0x002162A1 File Offset: 0x002144A1
		public bool IsDefaultFolderShared { get; private set; }

		// Token: 0x1700205B RID: 8283
		// (get) Token: 0x060078FC RID: 30972 RVA: 0x002162AA File Offset: 0x002144AA
		// (set) Token: 0x060078FD RID: 30973 RVA: 0x002162B2 File Offset: 0x002144B2
		public DateTime? LastSyncTimeUtc { get; set; }

		// Token: 0x060078FE RID: 30974 RVA: 0x002162BC File Offset: 0x002144BC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"InitiatorName=",
				this.InitiatorName,
				", InitiatorSmtpAddress=",
				this.InitiatorSmtpAddress,
				", RemoteFolderName=",
				this.RemoteFolderName,
				", RemoteFolderId=",
				this.RemoteFolderId,
				", LocalFolderName=",
				this.LocalFolderName,
				", LocalFolderId=",
				this.LocalFolderId.ToBase64String(),
				", IsDefaultFolderShared=",
				this.IsDefaultFolderShared.ToString(),
				", LastSyncTimeUtc=",
				this.LastSyncTimeUtc.ToString()
			});
		}
	}
}
