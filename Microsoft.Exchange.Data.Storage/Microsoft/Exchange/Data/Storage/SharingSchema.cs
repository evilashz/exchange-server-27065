using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CC8 RID: 3272
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharingSchema : Schema
	{
		// Token: 0x17001E5F RID: 7775
		// (get) Token: 0x06007191 RID: 29073 RVA: 0x001F796D File Offset: 0x001F5B6D
		public new static SharingSchema Instance
		{
			get
			{
				if (SharingSchema.instance == null)
				{
					SharingSchema.instance = new SharingSchema();
				}
				return SharingSchema.instance;
			}
		}

		// Token: 0x04004EE5 RID: 20197
		public static readonly StorePropertyDefinition ExternalSharingSharerIdentity = InternalSchema.ExternalSharingSharerIdentity;

		// Token: 0x04004EE6 RID: 20198
		public static readonly StorePropertyDefinition ExternalSharingSharerName = InternalSchema.ExternalSharingSharerName;

		// Token: 0x04004EE7 RID: 20199
		public static readonly GuidNamePropertyDefinition ExternalSharingRemoteFolderId = InternalSchema.ExternalSharingRemoteFolderId;

		// Token: 0x04004EE8 RID: 20200
		public static readonly GuidNamePropertyDefinition ExternalSharingRemoteFolderName = InternalSchema.ExternalSharingRemoteFolderName;

		// Token: 0x04004EE9 RID: 20201
		public static readonly GuidNamePropertyDefinition ExternalSharingLevelOfDetails = InternalSchema.ExternalSharingLevelOfDetails;

		// Token: 0x04004EEA RID: 20202
		public static readonly GuidNamePropertyDefinition ExternalSharingIsPrimary = InternalSchema.ExternalSharingIsPrimary;

		// Token: 0x04004EEB RID: 20203
		public static readonly GuidNamePropertyDefinition ExternalSharingSharerIdentityFederationUri = InternalSchema.ExternalSharingSharerIdentityFederationUri;

		// Token: 0x04004EEC RID: 20204
		public static readonly GuidNamePropertyDefinition ExternalSharingUrl = InternalSchema.ExternalSharingUrl;

		// Token: 0x04004EED RID: 20205
		public static readonly GuidNamePropertyDefinition ExternalSharingLocalFolderId = InternalSchema.ExternalSharingLocalFolderId;

		// Token: 0x04004EEE RID: 20206
		public static readonly GuidNamePropertyDefinition ExternalSharingDataType = InternalSchema.ExternalSharingDataType;

		// Token: 0x04004EEF RID: 20207
		public static readonly GuidNamePropertyDefinition ExternalSharingSharingKey = InternalSchema.ExternalSharingSharingKey;

		// Token: 0x04004EF0 RID: 20208
		public static readonly GuidNamePropertyDefinition ExternalSharingSubscriberIdentity = InternalSchema.ExternalSharingSubscriberIdentity;

		// Token: 0x04004EF1 RID: 20209
		public static readonly GuidNamePropertyDefinition ExternalSharingMasterId = InternalSchema.ExternalSharingMasterId;

		// Token: 0x04004EF2 RID: 20210
		public static readonly GuidNamePropertyDefinition ExternalSharingSyncState = InternalSchema.ExternalSharingSyncState;

		// Token: 0x04004EF3 RID: 20211
		private static SharingSchema instance = null;
	}
}
