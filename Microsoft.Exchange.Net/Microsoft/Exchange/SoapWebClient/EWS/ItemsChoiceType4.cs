using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000365 RID: 869
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", IncludeInSchema = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ItemsChoiceType4
	{
		// Token: 0x04001456 RID: 5206
		ApplyConversationActionResponseMessage,
		// Token: 0x04001457 RID: 5207
		ArchiveItemResponseMessage,
		// Token: 0x04001458 RID: 5208
		ConvertIdResponseMessage,
		// Token: 0x04001459 RID: 5209
		CopyFolderResponseMessage,
		// Token: 0x0400145A RID: 5210
		CopyItemResponseMessage,
		// Token: 0x0400145B RID: 5211
		CreateAttachmentResponseMessage,
		// Token: 0x0400145C RID: 5212
		CreateFolderPathResponseMessage,
		// Token: 0x0400145D RID: 5213
		CreateFolderResponseMessage,
		// Token: 0x0400145E RID: 5214
		CreateItemResponseMessage,
		// Token: 0x0400145F RID: 5215
		CreateManagedFolderResponseMessage,
		// Token: 0x04001460 RID: 5216
		CreateUserConfigurationResponseMessage,
		// Token: 0x04001461 RID: 5217
		DeleteAttachmentResponseMessage,
		// Token: 0x04001462 RID: 5218
		DeleteFolderResponseMessage,
		// Token: 0x04001463 RID: 5219
		DeleteItemResponseMessage,
		// Token: 0x04001464 RID: 5220
		DeleteUserConfigurationResponseMessage,
		// Token: 0x04001465 RID: 5221
		EmptyFolderResponseMessage,
		// Token: 0x04001466 RID: 5222
		ExpandDLResponseMessage,
		// Token: 0x04001467 RID: 5223
		ExportItemsResponseMessage,
		// Token: 0x04001468 RID: 5224
		FindFolderResponseMessage,
		// Token: 0x04001469 RID: 5225
		FindItemResponseMessage,
		// Token: 0x0400146A RID: 5226
		FindMailboxStatisticsByKeywordsResponseMessage,
		// Token: 0x0400146B RID: 5227
		FindPeopleResponseMessage,
		// Token: 0x0400146C RID: 5228
		GetAppManifestsResponseMessage,
		// Token: 0x0400146D RID: 5229
		GetAttachmentResponseMessage,
		// Token: 0x0400146E RID: 5230
		GetClientAccessTokenResponseMessage,
		// Token: 0x0400146F RID: 5231
		GetClientExtensionResponseMessage,
		// Token: 0x04001470 RID: 5232
		GetConversationItemsResponseMessage,
		// Token: 0x04001471 RID: 5233
		GetDiscoverySearchConfigurationResponseMessage,
		// Token: 0x04001472 RID: 5234
		GetEncryptionConfigurationResponseMessage,
		// Token: 0x04001473 RID: 5235
		GetEventsResponseMessage,
		// Token: 0x04001474 RID: 5236
		GetFolderResponseMessage,
		// Token: 0x04001475 RID: 5237
		GetHoldOnMailboxesResponseMessage,
		// Token: 0x04001476 RID: 5238
		GetItemResponseMessage,
		// Token: 0x04001477 RID: 5239
		GetNonIndexableItemDetailsResponseMessage,
		// Token: 0x04001478 RID: 5240
		GetNonIndexableItemStatisticsResponseMessage,
		// Token: 0x04001479 RID: 5241
		GetPasswordExpirationDateResponse,
		// Token: 0x0400147A RID: 5242
		GetPersonaResponseMessage,
		// Token: 0x0400147B RID: 5243
		GetRemindersResponse,
		// Token: 0x0400147C RID: 5244
		GetRoomListsResponse,
		// Token: 0x0400147D RID: 5245
		GetRoomsResponse,
		// Token: 0x0400147E RID: 5246
		GetSearchableMailboxesResponseMessage,
		// Token: 0x0400147F RID: 5247
		GetServerTimeZonesResponseMessage,
		// Token: 0x04001480 RID: 5248
		GetSharingFolderResponseMessage,
		// Token: 0x04001481 RID: 5249
		GetSharingMetadataResponseMessage,
		// Token: 0x04001482 RID: 5250
		GetStreamingEventsResponseMessage,
		// Token: 0x04001483 RID: 5251
		GetUserConfigurationResponseMessage,
		// Token: 0x04001484 RID: 5252
		GetUserPhotoResponseMessage,
		// Token: 0x04001485 RID: 5253
		GetUserRetentionPolicyTagsResponseMessage,
		// Token: 0x04001486 RID: 5254
		MarkAllItemsAsReadResponseMessage,
		// Token: 0x04001487 RID: 5255
		MarkAsJunkResponseMessage,
		// Token: 0x04001488 RID: 5256
		MoveFolderResponseMessage,
		// Token: 0x04001489 RID: 5257
		MoveItemResponseMessage,
		// Token: 0x0400148A RID: 5258
		PerformReminderActionResponse,
		// Token: 0x0400148B RID: 5259
		PostModernGroupItemResponseMessage,
		// Token: 0x0400148C RID: 5260
		RefreshSharingFolderResponseMessage,
		// Token: 0x0400148D RID: 5261
		ResolveNamesResponseMessage,
		// Token: 0x0400148E RID: 5262
		SearchMailboxesResponseMessage,
		// Token: 0x0400148F RID: 5263
		SendItemResponseMessage,
		// Token: 0x04001490 RID: 5264
		SendNotificationResponseMessage,
		// Token: 0x04001491 RID: 5265
		SetClientExtensionResponseMessage,
		// Token: 0x04001492 RID: 5266
		SetEncryptionConfigurationResponseMessage,
		// Token: 0x04001493 RID: 5267
		SetHoldOnMailboxesResponseMessage,
		// Token: 0x04001494 RID: 5268
		SubscribeResponseMessage,
		// Token: 0x04001495 RID: 5269
		SyncFolderHierarchyResponseMessage,
		// Token: 0x04001496 RID: 5270
		SyncFolderItemsResponseMessage,
		// Token: 0x04001497 RID: 5271
		UnsubscribeResponseMessage,
		// Token: 0x04001498 RID: 5272
		UpdateFolderResponseMessage,
		// Token: 0x04001499 RID: 5273
		UpdateGroupMailboxResponseMessage,
		// Token: 0x0400149A RID: 5274
		UpdateItemInRecoverableItemsResponseMessage,
		// Token: 0x0400149B RID: 5275
		UpdateItemResponseMessage,
		// Token: 0x0400149C RID: 5276
		UpdateMailboxAssociationResponseMessage,
		// Token: 0x0400149D RID: 5277
		UpdateUserConfigurationResponseMessage,
		// Token: 0x0400149E RID: 5278
		UploadItemsResponseMessage
	}
}
