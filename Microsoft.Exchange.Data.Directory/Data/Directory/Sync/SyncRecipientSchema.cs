using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000822 RID: 2082
	internal abstract class SyncRecipientSchema : SyncObjectSchema
	{
		// Token: 0x040043E9 RID: 17385
		public static SyncPropertyDefinition UseShadow = new SyncPropertyDefinition("UseShadow", null, typeof(bool), typeof(bool), SyncPropertyDefinitionFlags.TaskPopulated, SyncPropertyDefinition.InitialSyncPropertySetVersion, false);

		// Token: 0x040043EA RID: 17386
		public static SyncPropertyDefinition AcceptMessagesOnlyFrom = new SyncPropertyDefinition(ADRecipientSchema.AcceptMessagesOnlyFrom, "AuthOrig", typeof(SyncLink), typeof(AuthOrig), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043EB RID: 17387
		public static SyncPropertyDefinition AcceptMessagesOnlyFromDLMembers = new SyncPropertyDefinition(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers, "DLMemSubmitPerms", typeof(SyncLink), typeof(DLMemSubmitPerms), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043EC RID: 17388
		public static SyncPropertyDefinition Alias = new SyncPropertyDefinition(ADRecipientSchema.Alias, "MailNickname", typeof(string), typeof(DirectoryPropertyStringSingleMailNickname), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043ED RID: 17389
		public static SyncPropertyDefinition BypassModerationFrom = new SyncPropertyDefinition(ADRecipientSchema.BypassModerationFrom, "MSExchBypassModerationLink", typeof(SyncLink), typeof(MSExchBypassModerationLink), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043EE RID: 17390
		public static SyncPropertyDefinition BypassModerationFromDLMembers = new SyncPropertyDefinition(ADRecipientSchema.BypassModerationFromDLMembers, "MSExchBypassModerationFromDLMembersLink", typeof(SyncLink), typeof(MSExchBypassModerationFromDLMembersLink), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043EF RID: 17391
		public static SyncPropertyDefinition CloudLegacyExchangeDN = new SyncPropertyDefinition(ADRecipientSchema.LegacyExchangeDN, "CloudLegacyExchangeDN", typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043F0 RID: 17392
		public static SyncPropertyDefinition CloudMsExchRecipientDisplayType = new SyncPropertyDefinition(ADRecipientSchema.RecipientDisplayType, "CloudMSExchRecipientDisplayType", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043F1 RID: 17393
		public static SyncPropertyDefinition Cn = new SyncPropertyDefinition(ADObjectSchema.Name, "ShadowCommonName", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043F2 RID: 17394
		public static SyncPropertyDefinition CustomAttribute1 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute1, "ExtensionAttribute1", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043F3 RID: 17395
		public static SyncPropertyDefinition CustomAttribute2 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute2, "ExtensionAttribute2", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043F4 RID: 17396
		public static SyncPropertyDefinition CustomAttribute3 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute3, "ExtensionAttribute3", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043F5 RID: 17397
		public static SyncPropertyDefinition CustomAttribute4 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute4, "ExtensionAttribute4", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043F6 RID: 17398
		public static SyncPropertyDefinition CustomAttribute5 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute5, "ExtensionAttribute5", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043F7 RID: 17399
		public static SyncPropertyDefinition CustomAttribute6 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute6, "ExtensionAttribute6", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043F8 RID: 17400
		public static SyncPropertyDefinition CustomAttribute7 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute7, "ExtensionAttribute7", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043F9 RID: 17401
		public static SyncPropertyDefinition CustomAttribute8 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute8, "ExtensionAttribute8", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043FA RID: 17402
		public static SyncPropertyDefinition CustomAttribute9 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute9, "ExtensionAttribute9", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043FB RID: 17403
		public static SyncPropertyDefinition CustomAttribute10 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute10, "ExtensionAttribute10", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043FC RID: 17404
		public static SyncPropertyDefinition CustomAttribute11 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute11, "ExtensionAttribute11", typeof(string), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043FD RID: 17405
		public static SyncPropertyDefinition CustomAttribute12 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute12, "ExtensionAttribute12", typeof(string), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043FE RID: 17406
		public static SyncPropertyDefinition CustomAttribute13 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute13, "ExtensionAttribute13", typeof(string), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043FF RID: 17407
		public static SyncPropertyDefinition CustomAttribute14 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute14, "ExtensionAttribute14", typeof(string), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004400 RID: 17408
		public static SyncPropertyDefinition CustomAttribute15 = new SyncPropertyDefinition(ADRecipientSchema.CustomAttribute15, "ExtensionAttribute15", typeof(string), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004401 RID: 17409
		public static SyncPropertyDefinition ExtensionCustomAttribute1 = new SyncPropertyDefinition(ADRecipientSchema.ExtensionCustomAttribute1, "MSExchExtensionCustomAttribute1", typeof(string), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.IgnoredSyncPropertySetVersion);

		// Token: 0x04004402 RID: 17410
		public static SyncPropertyDefinition ExtensionCustomAttribute2 = new SyncPropertyDefinition(ADRecipientSchema.ExtensionCustomAttribute2, "MSExchExtensionCustomAttribute2", typeof(string), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.IgnoredSyncPropertySetVersion);

		// Token: 0x04004403 RID: 17411
		public static SyncPropertyDefinition ExtensionCustomAttribute3 = new SyncPropertyDefinition(ADRecipientSchema.ExtensionCustomAttribute3, "MSExchExtensionCustomAttribute3", typeof(string), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.IgnoredSyncPropertySetVersion);

		// Token: 0x04004404 RID: 17412
		public static SyncPropertyDefinition ExtensionCustomAttribute4 = new SyncPropertyDefinition(ADRecipientSchema.ExtensionCustomAttribute4, "MSExchExtensionCustomAttribute4", typeof(string), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.IgnoredSyncPropertySetVersion);

		// Token: 0x04004405 RID: 17413
		public static SyncPropertyDefinition ExtensionCustomAttribute5 = new SyncPropertyDefinition(ADRecipientSchema.ExtensionCustomAttribute5, "MSExchExtensionCustomAttribute5", typeof(string), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.IgnoredSyncPropertySetVersion);

		// Token: 0x04004406 RID: 17414
		public static SyncPropertyDefinition DisplayName = new SyncPropertyDefinition(ADRecipientSchema.DisplayName, "DisplayName", typeof(DirectoryPropertyStringSingleLength1To256), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004407 RID: 17415
		public static SyncPropertyDefinition EmailAddresses = new SyncPropertyDefinition(ADRecipientSchema.EmailAddresses, "ProxyAddresses", typeof(DirectoryPropertyProxyAddresses), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004408 RID: 17416
		public static SyncPropertyDefinition SipAddresses = new SyncPropertyDefinition("SipAddresses", "SipProxyAddress", typeof(ProxyAddress), typeof(DirectoryPropertyStringLength1To1123), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x04004409 RID: 17417
		public static SyncPropertyDefinition ExternalEmailAddress = new SyncPropertyDefinition("ExternalEmailAddress", "TargetAddress", typeof(ProxyAddress), typeof(DirectoryPropertyStringSingleLength1To1123), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x0400440A RID: 17418
		public static SyncPropertyDefinition GrantSendOnBehalfTo = new SyncPropertyDefinition("GrantSendOnBehalfTo", "PublicDelegates", typeof(SyncLink), typeof(object), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x0400440B RID: 17419
		public static SyncPropertyDefinition HiddenFromAddressListsEnabled = new SyncPropertyDefinition(ADRecipientSchema.HiddenFromAddressListsValue, "MSExchHideFromAddressLists", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400440C RID: 17420
		public static SyncPropertyDefinition IsDirSynced = new SyncPropertyDefinition(ADRecipientSchema.IsDirSynced, "DirSyncEnabled", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400440D RID: 17421
		public static SyncPropertyDefinition DirSyncAuthorityMetadata = new SyncPropertyDefinition(ADRecipientSchema.DirSyncAuthorityMetadata, "SingleAuthorityMetadata", typeof(AttributeSet), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.NotInMsoDirectory | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400440E RID: 17422
		public static SyncPropertyDefinition LitigationHoldDate = new SyncPropertyDefinition(IADMailStorageSchema.LitigationHoldDate, "MSExchLitigationHoldDate", typeof(DirectoryPropertyDateTimeSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400440F RID: 17423
		public static SyncPropertyDefinition LitigationHoldOwner = new SyncPropertyDefinition(IADMailStorageSchema.LitigationHoldOwner, "MSExchLitigationHoldOwner", typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004410 RID: 17424
		public static SyncPropertyDefinition MailTipTranslations = new SyncPropertyDefinition(ADRecipientSchema.MailTipTranslations, "MSExchSenderHintTranslations", typeof(DirectoryPropertyStringLength2To500), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004411 RID: 17425
		public static SyncPropertyDefinition ModeratedBy = new SyncPropertyDefinition(ADRecipientSchema.ModeratedBy, "MSExchModeratedByLink", typeof(SyncLink), typeof(MSExchModeratedByLink), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004412 RID: 17426
		public static SyncPropertyDefinition ModerationEnabled = new SyncPropertyDefinition(ADRecipientSchema.ModerationEnabled, "MSExchEnableModeration", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004413 RID: 17427
		public static SyncPropertyDefinition ModerationFlags = new SyncPropertyDefinition(ADRecipientSchema.ModerationFlags, "MSExchModerationFlags", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004414 RID: 17428
		public static SyncPropertyDefinition PhoneticDisplayName = new SyncPropertyDefinition(ADRecipientSchema.PhoneticDisplayName, "MSDSPhoneticDisplayName", typeof(DirectoryPropertyStringSingleLength1To256), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004415 RID: 17429
		public static SyncPropertyDefinition RecipientDisplayType = new SyncPropertyDefinition("RecipientDisplayType", "MSExchRecipientDisplayType", typeof(int?), typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x04004416 RID: 17430
		public static SyncPropertyDefinition RecipientTypeDetailsValue = new SyncPropertyDefinition(ADRecipientSchema.RecipientTypeDetailsValue, "MSExchRecipientTypeDetails", typeof(DirectoryPropertyInt64Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004417 RID: 17431
		public static SyncPropertyDefinition RejectMessagesFrom = new SyncPropertyDefinition(ADRecipientSchema.RejectMessagesFrom, "UnauthOrig", typeof(SyncLink), typeof(UnauthOrig), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004418 RID: 17432
		public static SyncPropertyDefinition RejectMessagesFromDLMembers = new SyncPropertyDefinition(ADRecipientSchema.RejectMessagesFromDLMembers, "DLMemRejectPerms", typeof(SyncLink), typeof(DLMemRejectPerms), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004419 RID: 17433
		public static SyncPropertyDefinition RequireAllSendersAreAuthenticated = new SyncPropertyDefinition(ADRecipientSchema.RequireAllSendersAreAuthenticated, "MSExchRequireAuthToSendTo", typeof(bool), typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400441A RID: 17434
		public static SyncPropertyDefinition RetentionComment = new SyncPropertyDefinition(IADMailStorageSchema.RetentionComment, "MSExchRetentionComment", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400441B RID: 17435
		public static SyncPropertyDefinition RetentionUrl = new SyncPropertyDefinition(IADMailStorageSchema.RetentionUrl, "MSExchRetentionUrl", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400441C RID: 17436
		public static SyncPropertyDefinition SeniorityIndex = new SyncPropertyDefinition(ADRecipientSchema.HABSeniorityIndex, "MSDSHABSeniorityIndex", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400441D RID: 17437
		public static SyncPropertyDefinition ValidationError = new SyncPropertyDefinition("ValidationError", "ValidationError", typeof(ValidationErrorValue), typeof(ValidationErrorValue), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x0400441E RID: 17438
		public static SyncPropertyDefinition BlockedSendersHash = new SyncPropertyDefinition("BlockedSendersHash", "MSExchBlockedSendersHash", typeof(byte[]), typeof(DirectoryPropertyBinarySingleLength1To4000), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x0400441F RID: 17439
		public static SyncPropertyDefinition SafeRecipientsHash = new SyncPropertyDefinition("SafeRecipientsHash", "MSExchSafeRecipientsHash", typeof(byte[]), typeof(DirectoryPropertyBinarySingleLength1To12000), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x04004420 RID: 17440
		public static SyncPropertyDefinition SafeSendersHash = new SyncPropertyDefinition("SafeSendersHash", "MSExchSafeSendersHash", typeof(byte[]), typeof(DirectoryPropertyBinarySingleLength1To32000), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x04004421 RID: 17441
		public static SyncPropertyDefinition OnPremisesObjectId = new SyncPropertyDefinition(ADRecipientSchema.OnPremisesObjectId, "SourceAnchor", typeof(string), typeof(DirectoryPropertyStringSingleLength1To256), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004422 RID: 17442
		public static SyncPropertyDefinition UserCertificate = new SyncPropertyDefinition(ADRecipientSchema.Certificate, "UserCertificate", typeof(byte[]), typeof(DirectoryPropertyBinaryLength1To32768), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion12);

		// Token: 0x04004423 RID: 17443
		public static SyncPropertyDefinition UserSMimeCertificate = new SyncPropertyDefinition(ADRecipientSchema.SMimeCertificate, "UserSMIMECertificate", typeof(byte[]), typeof(DirectoryPropertyBinaryLength1To32768), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion12);

		// Token: 0x04004424 RID: 17444
		public static SyncPropertyDefinition BypassNestedModerationEnabled = new SyncPropertyDefinition(ADRecipientSchema.BypassNestedModerationEnabled.Name, null, ADRecipientSchema.BypassNestedModerationEnabled.Type, typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.Calculated | SyncPropertyDefinitionFlags.ReadOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, false, new ProviderPropertyDefinition[]
		{
			SyncRecipientSchema.ModerationFlags
		}, ADObject.FlagGetterDelegate(1, SyncRecipientSchema.ModerationFlags), null);

		// Token: 0x04004425 RID: 17445
		public static SyncPropertyDefinition SendModerationNotifications = new SyncPropertyDefinition(ADRecipientSchema.SendModerationNotifications.Name, null, ADRecipientSchema.SendModerationNotifications.Type, typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.Calculated | SyncPropertyDefinitionFlags.ReadOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, TransportModerationNotificationFlags.Always, new ProviderPropertyDefinition[]
		{
			SyncRecipientSchema.ModerationFlags
		}, new GetterDelegate(SyncRecipient.SendModerationNotificationsGetter), null);

		// Token: 0x04004426 RID: 17446
		public static SyncPropertyDefinition SmtpAndX500Addresses = new SyncPropertyDefinition("SmtpAndX500Addresses", "ProxyAddresses", typeof(ProxyAddress), typeof(DirectoryPropertyProxyAddresses), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.MultiValued | SyncPropertyDefinitionFlags.Calculated | SyncPropertyDefinitionFlags.ReadOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, null, new ProviderPropertyDefinition[]
		{
			SyncRecipientSchema.EmailAddresses
		}, delegate(IPropertyBag propertyBag)
		{
			ProxyAddressCollection emailAddressesByPrefix = SyncRecipient.GetEmailAddressesByPrefix(propertyBag, ProxyAddressPrefix.Smtp, SyncRecipientSchema.SmtpAndX500Addresses);
			ProxyAddressCollection emailAddressesByPrefix2 = SyncRecipient.GetEmailAddressesByPrefix(propertyBag, ProxyAddressPrefix.X500, SyncRecipientSchema.SmtpAndX500Addresses);
			List<ProxyAddress> list = new List<ProxyAddress>();
			list.AddRange(emailAddressesByPrefix);
			list.AddRange(emailAddressesByPrefix2);
			return new ProxyAddressCollection(false, SyncRecipientSchema.SmtpAndX500Addresses, list);
		}, null);
	}
}
