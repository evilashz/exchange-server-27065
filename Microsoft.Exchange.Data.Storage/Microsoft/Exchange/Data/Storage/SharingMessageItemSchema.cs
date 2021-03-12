using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CC7 RID: 3271
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharingMessageItemSchema : MessageItemSchema
	{
		// Token: 0x17001E5E RID: 7774
		// (get) Token: 0x0600718E RID: 29070 RVA: 0x001F77DA File Offset: 0x001F59DA
		public new static SharingMessageItemSchema Instance
		{
			get
			{
				if (SharingMessageItemSchema.instance == null)
				{
					SharingMessageItemSchema.instance = new SharingMessageItemSchema();
				}
				return SharingMessageItemSchema.instance;
			}
		}

		// Token: 0x04004EC1 RID: 20161
		[Autoload]
		internal new static readonly StorePropertyDefinition SharingProviderGuid = InternalSchema.ProviderGuidBinary;

		// Token: 0x04004EC2 RID: 20162
		[Autoload]
		internal new static readonly StorePropertyDefinition SharingProviderName = InternalSchema.SharingProviderName;

		// Token: 0x04004EC3 RID: 20163
		[Autoload]
		internal new static readonly StorePropertyDefinition SharingProviderUrl = InternalSchema.SharingProviderUrl;

		// Token: 0x04004EC4 RID: 20164
		[Autoload]
		public static readonly StorePropertyDefinition SharingInitiatorName = InternalSchema.SharingInitiatorName;

		// Token: 0x04004EC5 RID: 20165
		[Autoload]
		internal static readonly StorePropertyDefinition SharingInitiatorSmtp = InternalSchema.SharingInitiatorSmtp;

		// Token: 0x04004EC6 RID: 20166
		[Autoload]
		internal static readonly StorePropertyDefinition SharingInitiatorEntryId = InternalSchema.SharingInitiatorEntryId;

		// Token: 0x04004EC7 RID: 20167
		[Autoload]
		internal new static readonly StorePropertyDefinition SharingRemoteType = InternalSchema.SharingRemoteType;

		// Token: 0x04004EC8 RID: 20168
		[Autoload]
		public new static readonly StorePropertyDefinition SharingRemoteName = InternalSchema.SharingRemoteName;

		// Token: 0x04004EC9 RID: 20169
		[Autoload]
		internal static readonly StorePropertyDefinition SharingRemoteUid = InternalSchema.SharingRemoteUid;

		// Token: 0x04004ECA RID: 20170
		[Autoload]
		internal static readonly StorePropertyDefinition SharingRemoteStoreUid = InternalSchema.SharingRemoteStoreUid;

		// Token: 0x04004ECB RID: 20171
		[Autoload]
		internal new static readonly StorePropertyDefinition SharingLocalType = InternalSchema.SharingLocalType;

		// Token: 0x04004ECC RID: 20172
		[Autoload]
		public new static readonly StorePropertyDefinition SharingLocalName = InternalSchema.SharingLocalName;

		// Token: 0x04004ECD RID: 20173
		[Autoload]
		internal new static readonly StorePropertyDefinition SharingLocalUid = InternalSchema.SharingLocalUid;

		// Token: 0x04004ECE RID: 20174
		[Autoload]
		internal new static readonly StorePropertyDefinition SharingLocalStoreUid = InternalSchema.SharingLocalStoreUid;

		// Token: 0x04004ECF RID: 20175
		[Autoload]
		internal new static readonly StorePropertyDefinition SharingCapabilities = InternalSchema.SharingCapabilities;

		// Token: 0x04004ED0 RID: 20176
		[Autoload]
		internal new static readonly StorePropertyDefinition SharingFlavor = InternalSchema.SharingFlavor;

		// Token: 0x04004ED1 RID: 20177
		[Autoload]
		internal new static readonly StorePropertyDefinition SharingDetail = InternalSchema.SharingDetail;

		// Token: 0x04004ED2 RID: 20178
		[Autoload]
		internal static readonly StorePropertyDefinition SharingPermissions = InternalSchema.SharingPermissions;

		// Token: 0x04004ED3 RID: 20179
		[Autoload]
		internal static readonly StorePropertyDefinition SharingResponseType = InternalSchema.SharingResponseType;

		// Token: 0x04004ED4 RID: 20180
		[Autoload]
		internal static readonly StorePropertyDefinition SharingResponseTime = InternalSchema.SharingResponseTime;

		// Token: 0x04004ED5 RID: 20181
		[Autoload]
		internal static readonly StorePropertyDefinition SharingOriginalMessageEntryId = InternalSchema.SharingOriginalMessageEntryId;

		// Token: 0x04004ED6 RID: 20182
		[Autoload]
		internal static readonly StorePropertyDefinition SharingLastSubscribeTime = InternalSchema.SharingLastSubscribeTime;

		// Token: 0x04004ED7 RID: 20183
		[Autoload]
		public new static readonly StorePropertyDefinition SharingRemotePath = InternalSchema.SharingRemotePath;

		// Token: 0x04004ED8 RID: 20184
		[Autoload]
		public static readonly StorePropertyDefinition SharingBrowseUrl = InternalSchema.SharingBrowseUrl;

		// Token: 0x04004ED9 RID: 20185
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingBrowseUrl = InternalSchema.XSharingBrowseUrl;

		// Token: 0x04004EDA RID: 20186
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingCapabilities = InternalSchema.XSharingCapabilities;

		// Token: 0x04004EDB RID: 20187
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingFlavor = InternalSchema.XSharingFlavor;

		// Token: 0x04004EDC RID: 20188
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingInstanceGuid = InternalSchema.XSharingInstanceGuid;

		// Token: 0x04004EDD RID: 20189
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingLocalType = InternalSchema.XSharingLocalType;

		// Token: 0x04004EDE RID: 20190
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingProviderGuid = InternalSchema.XSharingProviderGuid;

		// Token: 0x04004EDF RID: 20191
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingProviderName = InternalSchema.XSharingProviderName;

		// Token: 0x04004EE0 RID: 20192
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingProviderUrl = InternalSchema.XSharingProviderUrl;

		// Token: 0x04004EE1 RID: 20193
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingRemoteName = InternalSchema.XSharingRemoteName;

		// Token: 0x04004EE2 RID: 20194
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingRemotePath = InternalSchema.XSharingRemotePath;

		// Token: 0x04004EE3 RID: 20195
		[Autoload]
		internal static readonly StorePropertyDefinition XSharingRemoteType = InternalSchema.XSharingRemoteType;

		// Token: 0x04004EE4 RID: 20196
		private static SharingMessageItemSchema instance = null;
	}
}
