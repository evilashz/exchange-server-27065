using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C1D RID: 3101
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BindingItemSchema : ItemSchema
	{
		// Token: 0x17001DE0 RID: 7648
		// (get) Token: 0x06006E64 RID: 28260 RVA: 0x001DAA9A File Offset: 0x001D8C9A
		public new static BindingItemSchema Instance
		{
			get
			{
				if (BindingItemSchema.instance == null)
				{
					BindingItemSchema.instance = new BindingItemSchema();
				}
				return BindingItemSchema.instance;
			}
		}

		// Token: 0x0400409C RID: 16540
		private static BindingItemSchema instance;

		// Token: 0x0400409D RID: 16541
		public static readonly StorePropertyDefinition SharingStatus = InternalSchema.SharingStatus;

		// Token: 0x0400409E RID: 16542
		public static readonly StorePropertyDefinition SharingProviderGuid = InternalSchema.SharingProviderGuid;

		// Token: 0x0400409F RID: 16543
		public static readonly StorePropertyDefinition SharingProviderName = InternalSchema.SharingProviderName;

		// Token: 0x040040A0 RID: 16544
		public static readonly StorePropertyDefinition SharingProviderUrl = InternalSchema.SharingProviderUrl;

		// Token: 0x040040A1 RID: 16545
		public static readonly StorePropertyDefinition SharingRemotePath = InternalSchema.SharingRemotePath;

		// Token: 0x040040A2 RID: 16546
		public static readonly StorePropertyDefinition SharingRemoteName = InternalSchema.SharingRemoteName;

		// Token: 0x040040A3 RID: 16547
		public static readonly StorePropertyDefinition SharingLocalName = InternalSchema.SharingLocalName;

		// Token: 0x040040A4 RID: 16548
		public static readonly StorePropertyDefinition SharingLocalUid = InternalSchema.SharingLocalUid;

		// Token: 0x040040A5 RID: 16549
		public static readonly StorePropertyDefinition SharingLocalType = InternalSchema.SharingLocalType;

		// Token: 0x040040A6 RID: 16550
		public static readonly StorePropertyDefinition SharingFlavor = InternalSchema.SharingFlavor;

		// Token: 0x040040A7 RID: 16551
		public static readonly StorePropertyDefinition SharingInstanceGuid = InternalSchema.SharingInstanceGuid;

		// Token: 0x040040A8 RID: 16552
		public static readonly StorePropertyDefinition SharingRemoteType = InternalSchema.SharingRemoteType;

		// Token: 0x040040A9 RID: 16553
		public static readonly StorePropertyDefinition SharingLastSync = InternalSchema.SharingLastSync;

		// Token: 0x040040AA RID: 16554
		public static readonly StorePropertyDefinition SharingRemoteLastMod = InternalSchema.SharingRemoteLastMod;

		// Token: 0x040040AB RID: 16555
		public static readonly StorePropertyDefinition SharingConfigUrl = InternalSchema.SharingConfigUrl;

		// Token: 0x040040AC RID: 16556
		public static readonly StorePropertyDefinition SharingDetail = InternalSchema.SharingDetail;

		// Token: 0x040040AD RID: 16557
		public static readonly StorePropertyDefinition SharingTimeToLive = InternalSchema.SharingTimeToLive;

		// Token: 0x040040AE RID: 16558
		public static readonly StorePropertyDefinition SharingBindingEid = InternalSchema.SharingBindingEid;

		// Token: 0x040040AF RID: 16559
		public static readonly StorePropertyDefinition SharingIndexEid = InternalSchema.SharingIndexEid;

		// Token: 0x040040B0 RID: 16560
		public static readonly StorePropertyDefinition SharingRemoteComment = InternalSchema.SharingRemoteComment;

		// Token: 0x040040B1 RID: 16561
		public static readonly StorePropertyDefinition SharingLocalStoreUid = InternalSchema.SharingLocalStoreUid;

		// Token: 0x040040B2 RID: 16562
		public static readonly StorePropertyDefinition SharingRemoteByteSize = InternalSchema.SharingRemoteByteSize;

		// Token: 0x040040B3 RID: 16563
		public static readonly StorePropertyDefinition SharingRemoteCrc = InternalSchema.SharingRemoteCrc;

		// Token: 0x040040B4 RID: 16564
		public static readonly StorePropertyDefinition SharingLastAutoSync = InternalSchema.SharingLastAutoSync;

		// Token: 0x040040B5 RID: 16565
		public static readonly StorePropertyDefinition SharingSavedSession = InternalSchema.SharingSavedSession;

		// Token: 0x040040B6 RID: 16566
		public static readonly StorePropertyDefinition SharingInitiatorName = InternalSchema.SharingInitiatorName;

		// Token: 0x040040B7 RID: 16567
		public static readonly StorePropertyDefinition SharingInitiatorSmtp = InternalSchema.SharingInitiatorSmtp;

		// Token: 0x040040B8 RID: 16568
		public static readonly StorePropertyDefinition SharingRemoteFolderId = InternalSchema.SharingRemoteFolderId;

		// Token: 0x040040B9 RID: 16569
		public static readonly StorePropertyDefinition SharingRoamLog = InternalSchema.SharingRoamLog;

		// Token: 0x040040BA RID: 16570
		public static readonly StorePropertyDefinition SharingLocalFolderEwsId = InternalSchema.SharingLocalFolderEwsId;
	}
}
