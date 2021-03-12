using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C50 RID: 3152
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationItemSchema : Schema
	{
		// Token: 0x17001E0B RID: 7691
		// (get) Token: 0x06006F58 RID: 28504 RVA: 0x001DF4B2 File Offset: 0x001DD6B2
		public new static ConversationItemSchema Instance
		{
			get
			{
				if (ConversationItemSchema.instance == null)
				{
					ConversationItemSchema.instance = new ConversationItemSchema();
				}
				return ConversationItemSchema.instance;
			}
		}

		// Token: 0x04004309 RID: 17161
		public static readonly PropertyDefinition ConversationId = new ConversationIdProperty(InternalSchema.MapiConversationId, "ConversationId");

		// Token: 0x0400430A RID: 17162
		public static readonly PropertyDefinition FamilyId = new ConversationIdProperty(InternalSchema.InternalFamilyId, "FamilyId");

		// Token: 0x0400430B RID: 17163
		public static readonly PropertyDefinition ConversationTopic = InternalSchema.ConversationTopic;

		// Token: 0x0400430C RID: 17164
		public static readonly ReadonlySmartProperty ConversationMVFrom = new ReadonlySmartProperty(InternalSchema.InternalConversationMVFrom);

		// Token: 0x0400430D RID: 17165
		public static readonly ReadonlySmartProperty ConversationGlobalMVFrom = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalMVFrom);

		// Token: 0x0400430E RID: 17166
		public static readonly ReadonlySmartProperty ConversationMVUnreadFrom = new ReadonlySmartProperty(InternalSchema.InternalConversationMVUnreadFrom);

		// Token: 0x0400430F RID: 17167
		public static readonly ReadonlySmartProperty ConversationGlobalMVUnreadFrom = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalMVUnreadFrom);

		// Token: 0x04004310 RID: 17168
		public static readonly ReadonlySmartProperty ConversationMVTo = new ReadonlySmartProperty(InternalSchema.InternalConversationMVTo);

		// Token: 0x04004311 RID: 17169
		public static readonly ReadonlySmartProperty ConversationGlobalMVTo = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalMVTo);

		// Token: 0x04004312 RID: 17170
		public static readonly ReadonlySmartProperty ConversationLastDeliveryTime = new ReadonlySmartProperty(InternalSchema.InternalConversationLastDeliveryTime);

		// Token: 0x04004313 RID: 17171
		public static readonly ReadonlySmartProperty ConversationGlobalLastDeliveryTime = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalLastDeliveryTime);

		// Token: 0x04004314 RID: 17172
		public static readonly ReadonlySmartProperty ConversationLastDeliveryOrRenewTime = new ReadonlySmartProperty(InternalSchema.InternalConversationLastDeliveryOrRenewTime);

		// Token: 0x04004315 RID: 17173
		public static readonly ReadonlySmartProperty ConversationGlobalLastDeliveryOrRenewTime = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalLastDeliveryOrRenewTime);

		// Token: 0x04004316 RID: 17174
		public static readonly ReadonlySmartProperty MailboxGuid = new ReadonlySmartProperty(InternalSchema.InternalConversationMailboxGuid);

		// Token: 0x04004317 RID: 17175
		public static readonly ReadonlySmartProperty ConversationCategories = new ReadonlySmartProperty(InternalSchema.InternalConversationCategories);

		// Token: 0x04004318 RID: 17176
		public static readonly ReadonlySmartProperty ConversationGlobalCategories = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalCategories);

		// Token: 0x04004319 RID: 17177
		public static readonly ReadonlySmartProperty ConversationFlagStatus = new ReadonlySmartProperty(InternalSchema.InternalConversationFlagStatus);

		// Token: 0x0400431A RID: 17178
		public static readonly ReadonlySmartProperty ConversationGlobalFlagStatus = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalFlagStatus);

		// Token: 0x0400431B RID: 17179
		public static readonly ReadonlySmartProperty ConversationFlagCompleteTime = new ReadonlySmartProperty(InternalSchema.InternalConversationFlagCompleteTime);

		// Token: 0x0400431C RID: 17180
		public static readonly ReadonlySmartProperty ConversationGlobalFlagCompleteTime = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalFlagCompleteTime);

		// Token: 0x0400431D RID: 17181
		public static readonly ReadonlySmartProperty ConversationHasAttach = new ReadonlySmartProperty(InternalSchema.InternalConversationHasAttach);

		// Token: 0x0400431E RID: 17182
		public static readonly ReadonlySmartProperty ConversationGlobalHasAttach = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalHasAttach);

		// Token: 0x0400431F RID: 17183
		public static readonly ReadonlySmartProperty ConversationHasIrm = new ReadonlySmartProperty(InternalSchema.InternalConversationHasIrm);

		// Token: 0x04004320 RID: 17184
		public static readonly ReadonlySmartProperty ConversationGlobalHasIrm = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalHasIrm);

		// Token: 0x04004321 RID: 17185
		public static readonly ReadonlySmartProperty ConversationMessageCount = new ReadonlySmartProperty(InternalSchema.InternalConversationMessageCount);

		// Token: 0x04004322 RID: 17186
		public static readonly ReadonlySmartProperty ConversationGlobalMessageCount = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalMessageCount);

		// Token: 0x04004323 RID: 17187
		public static readonly ReadonlySmartProperty ConversationUnreadMessageCount = new ReadonlySmartProperty(InternalSchema.InternalConversationUnreadMessageCount);

		// Token: 0x04004324 RID: 17188
		public static readonly ReadonlySmartProperty ConversationGlobalUnreadMessageCount = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalUnreadMessageCount);

		// Token: 0x04004325 RID: 17189
		public static readonly ReadonlySmartProperty ConversationMessageSize = new ReadonlySmartProperty(InternalSchema.InternalConversationMessageSize);

		// Token: 0x04004326 RID: 17190
		public static readonly ReadonlySmartProperty ConversationGlobalMessageSize = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalMessageSize);

		// Token: 0x04004327 RID: 17191
		public static readonly ReadonlySmartProperty ConversationMessageClasses = new ReadonlySmartProperty(InternalSchema.InternalConversationMessageClasses);

		// Token: 0x04004328 RID: 17192
		public static readonly ReadonlySmartProperty ConversationGlobalMessageClasses = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalMessageClasses);

		// Token: 0x04004329 RID: 17193
		public static readonly ReadonlySmartProperty ConversationReplyForwardState = new ReadonlySmartProperty(InternalSchema.InternalConversationReplyForwardState);

		// Token: 0x0400432A RID: 17194
		public static readonly ReadonlySmartProperty ConversationGlobalReplyForwardState = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalReplyForwardState);

		// Token: 0x0400432B RID: 17195
		public static readonly ReadonlySmartProperty ConversationImportance = new ReadonlySmartProperty(InternalSchema.InternalConversationImportance);

		// Token: 0x0400432C RID: 17196
		public static readonly ReadonlySmartProperty ConversationGlobalImportance = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalImportance);

		// Token: 0x0400432D RID: 17197
		public static readonly StoreObjectIdCollectionProperty ConversationItemIds = new StoreObjectIdCollectionProperty(InternalSchema.InternalConversationMVItemIds, PropertyFlags.ReadOnly, "Conversation Member ItemIds");

		// Token: 0x0400432E RID: 17198
		public static readonly StoreObjectIdCollectionProperty ConversationGlobalItemIds = new StoreObjectIdCollectionProperty(InternalSchema.InternalConversationGlobalMVItemIds, PropertyFlags.ReadOnly, "Conversation Member Global ItemIds");

		// Token: 0x0400432F RID: 17199
		public static readonly ReadonlySmartProperty ConversationLastMemberDocumentId = new ReadonlySmartProperty(InternalSchema.InternalConversationLastMemberDocumentId);

		// Token: 0x04004330 RID: 17200
		public static readonly ReadonlySmartProperty ConversationGlobalLastMemberDocumentId = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalLastMemberDocumentId);

		// Token: 0x04004331 RID: 17201
		public static readonly ReadonlySmartProperty ConversationPreview = new ReadonlySmartProperty(InternalSchema.InternalConversationPreview);

		// Token: 0x04004332 RID: 17202
		public static readonly ReadonlySmartProperty ConversationGlobalPreview = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalPreview);

		// Token: 0x04004333 RID: 17203
		public static readonly ReadonlySmartProperty ConversationHasClutter = new ReadonlySmartProperty(InternalSchema.InternalConversationHasClutter);

		// Token: 0x04004334 RID: 17204
		public static readonly ReadonlySmartProperty ConversationGlobalHasClutter = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalHasClutter);

		// Token: 0x04004335 RID: 17205
		public static readonly ReadonlySmartProperty ConversationInitialMemberDocumentId = new ReadonlySmartProperty(InternalSchema.InternalConversationInitialMemberDocumentId);

		// Token: 0x04004336 RID: 17206
		public static readonly ReadonlySmartProperty ConversationMemberDocumentIds = new ReadonlySmartProperty(InternalSchema.InternalConversationMemberDocumentIds);

		// Token: 0x04004337 RID: 17207
		public static readonly ReadonlySmartProperty ConversationGlobalRichContent = new ReadonlySmartProperty(InternalSchema.InternalConversationGlobalRichContent);

		// Token: 0x04004338 RID: 17208
		public static readonly ReadonlySmartProperty ConversationWorkingSetSourcePartition = new ReadonlySmartProperty(InternalSchema.InternalConversationWorkingSetSourcePartition);

		// Token: 0x04004339 RID: 17209
		private static ConversationItemSchema instance = null;
	}
}
