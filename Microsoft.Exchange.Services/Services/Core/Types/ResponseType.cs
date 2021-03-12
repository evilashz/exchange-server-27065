using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000548 RID: 1352
	[XmlType(IncludeInSchema = false)]
	[Serializable]
	public enum ResponseType
	{
		// Token: 0x040017E8 RID: 6120
		CopyFolderResponseMessage,
		// Token: 0x040017E9 RID: 6121
		CreateUnifiedMailboxResponseMessage,
		// Token: 0x040017EA RID: 6122
		CopyItemResponseMessage,
		// Token: 0x040017EB RID: 6123
		CreateFolderResponseMessage,
		// Token: 0x040017EC RID: 6124
		CreateItemResponseMessage,
		// Token: 0x040017ED RID: 6125
		CreateManagedFolderResponseMessage,
		// Token: 0x040017EE RID: 6126
		CreateFolderPathResponseMessage,
		// Token: 0x040017EF RID: 6127
		DeleteFolderResponseMessage,
		// Token: 0x040017F0 RID: 6128
		DeleteItemResponseMessage,
		// Token: 0x040017F1 RID: 6129
		ExpandDLResponseMessage,
		// Token: 0x040017F2 RID: 6130
		FindFolderResponseMessage,
		// Token: 0x040017F3 RID: 6131
		FindItemResponseMessage,
		// Token: 0x040017F4 RID: 6132
		FindConversationResponseMessage,
		// Token: 0x040017F5 RID: 6133
		GetEventsResponseMessage,
		// Token: 0x040017F6 RID: 6134
		GetStreamingEventsResponseMessage,
		// Token: 0x040017F7 RID: 6135
		GetFolderResponseMessage,
		// Token: 0x040017F8 RID: 6136
		GetMailTipsResponseMessage,
		// Token: 0x040017F9 RID: 6137
		GetComplianceConfigurationResponseMessage,
		// Token: 0x040017FA RID: 6138
		PlayOnPhoneResponseMessage,
		// Token: 0x040017FB RID: 6139
		GetPhoneCallInformationResponseMessage,
		// Token: 0x040017FC RID: 6140
		DisconnectPhoneCallResponseMessage,
		// Token: 0x040017FD RID: 6141
		GetServiceConfigurationResponseMessage,
		// Token: 0x040017FE RID: 6142
		GetItemResponseMessage,
		// Token: 0x040017FF RID: 6143
		GetServerTimeZonesResponseMessage,
		// Token: 0x04001800 RID: 6144
		MoveFolderResponseMessage,
		// Token: 0x04001801 RID: 6145
		MoveItemResponseMessage,
		// Token: 0x04001802 RID: 6146
		ResolveNamesResponseMessage,
		// Token: 0x04001803 RID: 6147
		SendItemResponseMessage,
		// Token: 0x04001804 RID: 6148
		SubscribeResponseMessage,
		// Token: 0x04001805 RID: 6149
		UnsubscribeResponseMessage,
		// Token: 0x04001806 RID: 6150
		UpdateFolderResponseMessage,
		// Token: 0x04001807 RID: 6151
		UpdateItemResponseMessage,
		// Token: 0x04001808 RID: 6152
		UpdateItemInRecoverableItemsResponseMessage,
		// Token: 0x04001809 RID: 6153
		CreateAttachmentResponseMessage,
		// Token: 0x0400180A RID: 6154
		DeleteAttachmentResponseMessage,
		// Token: 0x0400180B RID: 6155
		GetAttachmentResponseMessage,
		// Token: 0x0400180C RID: 6156
		GetClientAccessTokenResponseMessage,
		// Token: 0x0400180D RID: 6157
		SendNotificationResponseMessage,
		// Token: 0x0400180E RID: 6158
		SyncFolderItemsResponseMessage,
		// Token: 0x0400180F RID: 6159
		SyncFolderHierarchyResponseMessage,
		// Token: 0x04001810 RID: 6160
		ConvertIdResponseMessage,
		// Token: 0x04001811 RID: 6161
		GetDelegateResponseMessage,
		// Token: 0x04001812 RID: 6162
		AddDelegateResponseMessage,
		// Token: 0x04001813 RID: 6163
		RemoveDelegateResponseMessage,
		// Token: 0x04001814 RID: 6164
		UpdateDelegateResponseMessage,
		// Token: 0x04001815 RID: 6165
		CreateUserConfigurationResponseMessage,
		// Token: 0x04001816 RID: 6166
		DeleteUserConfigurationResponseMessage,
		// Token: 0x04001817 RID: 6167
		GetUserConfigurationResponseMessage,
		// Token: 0x04001818 RID: 6168
		UpdateUserConfigurationResponseMessage,
		// Token: 0x04001819 RID: 6169
		GetUserAvailabilityResponseMessage,
		// Token: 0x0400181A RID: 6170
		GetUserOofSettingsResponseMessage,
		// Token: 0x0400181B RID: 6171
		SetUserOofSettingsResponseMessage,
		// Token: 0x0400181C RID: 6172
		GetSharingMetadataResponseMessage,
		// Token: 0x0400181D RID: 6173
		RefreshSharingFolderResponseMessage,
		// Token: 0x0400181E RID: 6174
		GetSharingFolderResponseMessage,
		// Token: 0x0400181F RID: 6175
		GetRemindersResponseMessage,
		// Token: 0x04001820 RID: 6176
		PerformReminderActionResponseMessage,
		// Token: 0x04001821 RID: 6177
		GetRoomListsResponseMessage,
		// Token: 0x04001822 RID: 6178
		GetRoomsResponseMessage,
		// Token: 0x04001823 RID: 6179
		FindMessageTrackingReportResponseMessage,
		// Token: 0x04001824 RID: 6180
		GetMessageTrackingReportResponseMessage,
		// Token: 0x04001825 RID: 6181
		ApplyConversationActionResponseMessage,
		// Token: 0x04001826 RID: 6182
		EmptyFolderResponseMessage,
		// Token: 0x04001827 RID: 6183
		UploadItemsResponseMessage,
		// Token: 0x04001828 RID: 6184
		ExportItemsResponseMessage,
		// Token: 0x04001829 RID: 6185
		MarkAllItemsAsReadResponseMessage,
		// Token: 0x0400182A RID: 6186
		ExecuteDiagnosticMethodResponseMessage,
		// Token: 0x0400182B RID: 6187
		FindMailboxStatisticsByKeywordsResponseMessage,
		// Token: 0x0400182C RID: 6188
		GetSearchableMailboxesResponseMessage,
		// Token: 0x0400182D RID: 6189
		SearchMailboxesResponseMessage,
		// Token: 0x0400182E RID: 6190
		GetDiscoverySearchConfigurationResponseMessage,
		// Token: 0x0400182F RID: 6191
		GetHoldOnMailboxesResponseMessage,
		// Token: 0x04001830 RID: 6192
		SetHoldOnMailboxesResponseMessage,
		// Token: 0x04001831 RID: 6193
		GetNonIndexableItemStatisticsResponseMessage,
		// Token: 0x04001832 RID: 6194
		GetNonIndexableItemDetailsResponseMessage,
		// Token: 0x04001833 RID: 6195
		GetInboxRulesResponseMessage,
		// Token: 0x04001834 RID: 6196
		UpdateInboxRulesResponseMessage,
		// Token: 0x04001835 RID: 6197
		IsUMEnabledResponseMessage,
		// Token: 0x04001836 RID: 6198
		GetUMPropertiesResponseMessage,
		// Token: 0x04001837 RID: 6199
		SetOofStatusResponseMessage,
		// Token: 0x04001838 RID: 6200
		SetPlayOnPhoneDialStringResponseMessage,
		// Token: 0x04001839 RID: 6201
		SetTelephoneAccessFolderEmailResponseMessage,
		// Token: 0x0400183A RID: 6202
		SetMissedCallNotificationEnabledResponseMessage,
		// Token: 0x0400183B RID: 6203
		ResetPINResponseMessage,
		// Token: 0x0400183C RID: 6204
		GetCallInfoResponseMessage,
		// Token: 0x0400183D RID: 6205
		DisconnectResponseMessage,
		// Token: 0x0400183E RID: 6206
		PlayOnPhoneGreetingResponseMessage,
		// Token: 0x0400183F RID: 6207
		FindPeopleResponseMessage,
		// Token: 0x04001840 RID: 6208
		GetPasswordExpirationDateResponseMessage,
		// Token: 0x04001841 RID: 6209
		GetPersonaResponseMessage,
		// Token: 0x04001842 RID: 6210
		SyncConversationResponseMessage,
		// Token: 0x04001843 RID: 6211
		GetAppManifestsResponseMessage,
		// Token: 0x04001844 RID: 6212
		AddImContactToGroupResponseMessage,
		// Token: 0x04001845 RID: 6213
		AddNewImContactToGroupResponseMessage,
		// Token: 0x04001846 RID: 6214
		RemoveImContactFromGroupResponseMessage,
		// Token: 0x04001847 RID: 6215
		GetConversationItemsResponseMessage,
		// Token: 0x04001848 RID: 6216
		GetThreadedConversationItemsResponseMessage,
		// Token: 0x04001849 RID: 6217
		GetConversationItemsDiagnosticsResponseMessage,
		// Token: 0x0400184A RID: 6218
		GetUserRetentionPolicyTagsResponseMessage,
		// Token: 0x0400184B RID: 6219
		AddDistributionGroupToImListResponseMessage,
		// Token: 0x0400184C RID: 6220
		AddImGroupResponseMessage,
		// Token: 0x0400184D RID: 6221
		GetImItemListResponseMessage,
		// Token: 0x0400184E RID: 6222
		ProvisionResponseMessage,
		// Token: 0x0400184F RID: 6223
		SyncPeopleResponseMessage,
		// Token: 0x04001850 RID: 6224
		GetClientExtensionTokenResponseMessage,
		// Token: 0x04001851 RID: 6225
		InstallAppResponseMessage,
		// Token: 0x04001852 RID: 6226
		UninstallAppResponseMessage,
		// Token: 0x04001853 RID: 6227
		DisableAppResponseMessage,
		// Token: 0x04001854 RID: 6228
		GetAppMarketplaceUrlResponseMessage,
		// Token: 0x04001855 RID: 6229
		AddAggregatedAccountResponseMessage,
		// Token: 0x04001856 RID: 6230
		GetAggregatedAccountResponseMessage,
		// Token: 0x04001857 RID: 6231
		RemoveAggregatedAccountResponseMessage,
		// Token: 0x04001858 RID: 6232
		SetAggregatedAccountResponseMessage,
		// Token: 0x04001859 RID: 6233
		IsOffice365DomainResponseMessage,
		// Token: 0x0400185A RID: 6234
		RemoveContactFromImListResponseMessage,
		// Token: 0x0400185B RID: 6235
		RemoveImGroupResponseMessage,
		// Token: 0x0400185C RID: 6236
		SetImGroupResponseMessage,
		// Token: 0x0400185D RID: 6237
		GetUserPhotoResponseMessage,
		// Token: 0x0400185E RID: 6238
		GetPeopleICommunicateWithResponseMessage,
		// Token: 0x0400185F RID: 6239
		DeleteUMPromptsResponseMessage,
		// Token: 0x04001860 RID: 6240
		GetUMPromptNamesResponseMessage,
		// Token: 0x04001861 RID: 6241
		GetUMPromptResponseMessage,
		// Token: 0x04001862 RID: 6242
		CreateUMPromptResponseMessage,
		// Token: 0x04001863 RID: 6243
		GetImItemsResponseMessage,
		// Token: 0x04001864 RID: 6244
		RemoveDistributionGroupFromImListResponseMessage,
		// Token: 0x04001865 RID: 6245
		SetTeamMailboxResponseMessage,
		// Token: 0x04001866 RID: 6246
		UnpinTeamMailboxResponseMessage,
		// Token: 0x04001867 RID: 6247
		GetClientExtensionResponseMessage,
		// Token: 0x04001868 RID: 6248
		SetClientExtensionResponseMessage,
		// Token: 0x04001869 RID: 6249
		SetImListMigrationCompletedResponseMessage,
		// Token: 0x0400186A RID: 6250
		StartFindInGALSpeechRecognitionResponseMessage,
		// Token: 0x0400186B RID: 6251
		CompleteFindInGALSpeechRecognitionResponseMessage,
		// Token: 0x0400186C RID: 6252
		CreateUMCallDataRecordResponseMessage,
		// Token: 0x0400186D RID: 6253
		GetUMCallDataRecordsResponseMessage,
		// Token: 0x0400186E RID: 6254
		GetUMCallSummaryResponseMessage,
		// Token: 0x0400186F RID: 6255
		GetTimeZoneOffsetsResponseMessage,
		// Token: 0x04001870 RID: 6256
		DeprovisionResponseMessage,
		// Token: 0x04001871 RID: 6257
		ArchiveItemResponseMessage,
		// Token: 0x04001872 RID: 6258
		GetUserPhotoDataResponseMessage,
		// Token: 0x04001873 RID: 6259
		InitUMMailboxResponseMessage,
		// Token: 0x04001874 RID: 6260
		ResetUMMailboxResponseMessage,
		// Token: 0x04001875 RID: 6261
		ValidateUMPinResponseMessage,
		// Token: 0x04001876 RID: 6262
		SaveUMPinResponseMessage,
		// Token: 0x04001877 RID: 6263
		GetUMPinResponseMessage,
		// Token: 0x04001878 RID: 6264
		SubscribeToPushNotificationResponseMessage,
		// Token: 0x04001879 RID: 6265
		UnsubscribeToPushNotificationResponseMessage,
		// Token: 0x0400187A RID: 6266
		GetClientIntentResponseMessage,
		// Token: 0x0400187B RID: 6267
		GetUMSubscriberCallAnsweringDataResponseMessage,
		// Token: 0x0400187C RID: 6268
		MarkAsJunkResponseMessage,
		// Token: 0x0400187D RID: 6269
		AddNewTelUriContactToGroupResponseMessage,
		// Token: 0x0400187E RID: 6270
		GetModernConversationItemsResponseMessage,
		// Token: 0x0400187F RID: 6271
		PostModernGroupItemResponseMessage,
		// Token: 0x04001880 RID: 6272
		UpdateAndPostModernGroupItemResponseMessage,
		// Token: 0x04001881 RID: 6273
		GetModernConversationAttachmentsResponseMessage,
		// Token: 0x04001882 RID: 6274
		UpdateMailboxAssociationResponseMessage,
		// Token: 0x04001883 RID: 6275
		GetEncryptionConfigurationResponseMessage,
		// Token: 0x04001884 RID: 6276
		SetEncryptionConfigurationResponseMessage,
		// Token: 0x04001885 RID: 6277
		GetCalendarEventResponseMessage,
		// Token: 0x04001886 RID: 6278
		CreateCalendarEventResponseMessage,
		// Token: 0x04001887 RID: 6279
		UpdateCalendarEventResponseMessage,
		// Token: 0x04001888 RID: 6280
		CancelCalendarEventResponseMessage,
		// Token: 0x04001889 RID: 6281
		RespondToCalendarEventResponseMessage,
		// Token: 0x0400188A RID: 6282
		SubscribeToConversationChangesResponseMessage,
		// Token: 0x0400188B RID: 6283
		SubscribeToMessageChangesResponseMessage,
		// Token: 0x0400188C RID: 6284
		SubscribeToHierarchyChangesResponseMessage,
		// Token: 0x0400188D RID: 6285
		LogPushNotificationDataResponseMessage,
		// Token: 0x0400188E RID: 6286
		RefreshGALContactsFolderResponseMessage,
		// Token: 0x0400188F RID: 6287
		DeleteCalendarEventResponseMessage,
		// Token: 0x04001890 RID: 6288
		ForwardCalendarEventResponseMessage,
		// Token: 0x04001891 RID: 6289
		LikeItemResponseMessage,
		// Token: 0x04001892 RID: 6290
		SetModernGroupMembershipResponseMessage,
		// Token: 0x04001893 RID: 6291
		ExpandCalendarEventResponseMessage,
		// Token: 0x04001894 RID: 6292
		UpdateGroupMailboxResponseMessage,
		// Token: 0x04001895 RID: 6293
		GetCalendarViewResponseMessage,
		// Token: 0x04001896 RID: 6294
		RequestDeviceRegistrationChallengeResponseMessage,
		// Token: 0x04001897 RID: 6295
		CreateResponseFromModernGroupResponseMessage,
		// Token: 0x04001898 RID: 6296
		GetLikersResponseMessage,
		// Token: 0x04001899 RID: 6297
		GetBirthdayCalendarViewResponseMessage,
		// Token: 0x0400189A RID: 6298
		PerformInstantSearchResponseMessage,
		// Token: 0x0400189B RID: 6299
		EndInstantSearchSessionResponseMessage,
		// Token: 0x0400189C RID: 6300
		GetUserUnifiedGroupsResponseMessage,
		// Token: 0x0400189D RID: 6301
		SyncAutoCompleteRecipientsResponseMessage,
		// Token: 0x0400189E RID: 6302
		MaskAutoCompleteRecipientResponseMessage,
		// Token: 0x0400189F RID: 6303
		GetClutterStateResponseMessage,
		// Token: 0x040018A0 RID: 6304
		SetClutterStateResponseMessage
	}
}
