using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000284 RID: 644
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", IncludeInSchema = false)]
	[Serializable]
	public enum ItemsChoiceType4
	{
		// Token: 0x04001004 RID: 4100
		ApplyConversationActionResponseMessage,
		// Token: 0x04001005 RID: 4101
		ArchiveItemResponseMessage,
		// Token: 0x04001006 RID: 4102
		ConvertIdResponseMessage,
		// Token: 0x04001007 RID: 4103
		CopyFolderResponseMessage,
		// Token: 0x04001008 RID: 4104
		CopyItemResponseMessage,
		// Token: 0x04001009 RID: 4105
		CreateAttachmentResponseMessage,
		// Token: 0x0400100A RID: 4106
		CreateFolderPathResponseMessage,
		// Token: 0x0400100B RID: 4107
		CreateFolderResponseMessage,
		// Token: 0x0400100C RID: 4108
		CreateItemResponseMessage,
		// Token: 0x0400100D RID: 4109
		CreateManagedFolderResponseMessage,
		// Token: 0x0400100E RID: 4110
		CreateUserConfigurationResponseMessage,
		// Token: 0x0400100F RID: 4111
		DeleteAttachmentResponseMessage,
		// Token: 0x04001010 RID: 4112
		DeleteFolderResponseMessage,
		// Token: 0x04001011 RID: 4113
		DeleteItemResponseMessage,
		// Token: 0x04001012 RID: 4114
		DeleteUserConfigurationResponseMessage,
		// Token: 0x04001013 RID: 4115
		EmptyFolderResponseMessage,
		// Token: 0x04001014 RID: 4116
		ExpandDLResponseMessage,
		// Token: 0x04001015 RID: 4117
		ExportItemsResponseMessage,
		// Token: 0x04001016 RID: 4118
		FindFolderResponseMessage,
		// Token: 0x04001017 RID: 4119
		FindItemResponseMessage,
		// Token: 0x04001018 RID: 4120
		FindMailboxStatisticsByKeywordsResponseMessage,
		// Token: 0x04001019 RID: 4121
		FindPeopleResponseMessage,
		// Token: 0x0400101A RID: 4122
		GetAppManifestsResponseMessage,
		// Token: 0x0400101B RID: 4123
		GetAttachmentResponseMessage,
		// Token: 0x0400101C RID: 4124
		GetClientAccessTokenResponseMessage,
		// Token: 0x0400101D RID: 4125
		GetClientExtensionResponseMessage,
		// Token: 0x0400101E RID: 4126
		GetConversationItemsResponseMessage,
		// Token: 0x0400101F RID: 4127
		GetDiscoverySearchConfigurationResponseMessage,
		// Token: 0x04001020 RID: 4128
		GetEncryptionConfigurationResponseMessage,
		// Token: 0x04001021 RID: 4129
		GetEventsResponseMessage,
		// Token: 0x04001022 RID: 4130
		GetFolderResponseMessage,
		// Token: 0x04001023 RID: 4131
		GetHoldOnMailboxesResponseMessage,
		// Token: 0x04001024 RID: 4132
		GetItemResponseMessage,
		// Token: 0x04001025 RID: 4133
		GetNonIndexableItemDetailsResponseMessage,
		// Token: 0x04001026 RID: 4134
		GetNonIndexableItemStatisticsResponseMessage,
		// Token: 0x04001027 RID: 4135
		GetPasswordExpirationDateResponse,
		// Token: 0x04001028 RID: 4136
		GetPersonaResponseMessage,
		// Token: 0x04001029 RID: 4137
		GetRemindersResponse,
		// Token: 0x0400102A RID: 4138
		GetRoomListsResponse,
		// Token: 0x0400102B RID: 4139
		GetRoomsResponse,
		// Token: 0x0400102C RID: 4140
		GetSearchableMailboxesResponseMessage,
		// Token: 0x0400102D RID: 4141
		GetServerTimeZonesResponseMessage,
		// Token: 0x0400102E RID: 4142
		GetSharingFolderResponseMessage,
		// Token: 0x0400102F RID: 4143
		GetSharingMetadataResponseMessage,
		// Token: 0x04001030 RID: 4144
		GetStreamingEventsResponseMessage,
		// Token: 0x04001031 RID: 4145
		GetUserConfigurationResponseMessage,
		// Token: 0x04001032 RID: 4146
		GetUserPhotoResponseMessage,
		// Token: 0x04001033 RID: 4147
		GetUserRetentionPolicyTagsResponseMessage,
		// Token: 0x04001034 RID: 4148
		MarkAllItemsAsReadResponseMessage,
		// Token: 0x04001035 RID: 4149
		MarkAsJunkResponseMessage,
		// Token: 0x04001036 RID: 4150
		MoveFolderResponseMessage,
		// Token: 0x04001037 RID: 4151
		MoveItemResponseMessage,
		// Token: 0x04001038 RID: 4152
		PerformReminderActionResponse,
		// Token: 0x04001039 RID: 4153
		PostModernGroupItemResponseMessage,
		// Token: 0x0400103A RID: 4154
		RefreshSharingFolderResponseMessage,
		// Token: 0x0400103B RID: 4155
		ResolveNamesResponseMessage,
		// Token: 0x0400103C RID: 4156
		SearchMailboxesResponseMessage,
		// Token: 0x0400103D RID: 4157
		SendItemResponseMessage,
		// Token: 0x0400103E RID: 4158
		SendNotificationResponseMessage,
		// Token: 0x0400103F RID: 4159
		SetClientExtensionResponseMessage,
		// Token: 0x04001040 RID: 4160
		SetEncryptionConfigurationResponseMessage,
		// Token: 0x04001041 RID: 4161
		SetHoldOnMailboxesResponseMessage,
		// Token: 0x04001042 RID: 4162
		SubscribeResponseMessage,
		// Token: 0x04001043 RID: 4163
		SyncFolderHierarchyResponseMessage,
		// Token: 0x04001044 RID: 4164
		SyncFolderItemsResponseMessage,
		// Token: 0x04001045 RID: 4165
		UnsubscribeResponseMessage,
		// Token: 0x04001046 RID: 4166
		UpdateFolderResponseMessage,
		// Token: 0x04001047 RID: 4167
		UpdateGroupMailboxResponseMessage,
		// Token: 0x04001048 RID: 4168
		UpdateItemInRecoverableItemsResponseMessage,
		// Token: 0x04001049 RID: 4169
		UpdateItemResponseMessage,
		// Token: 0x0400104A RID: 4170
		UpdateMailboxAssociationResponseMessage,
		// Token: 0x0400104B RID: 4171
		UpdateUserConfigurationResponseMessage,
		// Token: 0x0400104C RID: 4172
		UploadItemsResponseMessage
	}
}
