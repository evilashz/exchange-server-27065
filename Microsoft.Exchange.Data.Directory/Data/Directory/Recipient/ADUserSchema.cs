using System;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000274 RID: 628
	internal class ADUserSchema : ADMailboxRecipientSchema
	{
		// Token: 0x06001DB9 RID: 7609 RVA: 0x00084044 File Offset: 0x00082244
		internal static SetterDelegate EnumFlagSetterDelegate(ProviderPropertyDefinition propertyDefinition, int mask)
		{
			return delegate(object value, IPropertyBag bag)
			{
				int num = (int)bag[propertyDefinition];
				int num2 = ((bool)value) ? (num | mask) : (num & ~mask);
				if (propertyDefinition.Type != null && propertyDefinition.Type.IsEnum)
				{
					bag[propertyDefinition] = Enum.ToObject(propertyDefinition.Type, num2);
					return;
				}
				bag[propertyDefinition] = num2;
			};
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00084074 File Offset: 0x00082274
		internal static object MobileSyncEnabledGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[ADUserSchema.MobileFeaturesEnabled];
			if (obj == null)
			{
				return true;
			}
			MobileFeaturesEnabled mobileFeaturesEnabled = (MobileFeaturesEnabled)obj;
			return (mobileFeaturesEnabled & Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.AirSyncDisabled) == Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.None;
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000840AC File Offset: 0x000822AC
		internal static void MobileSyncEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)value;
			object obj = propertyBag[ADUserSchema.MobileFeaturesEnabled];
			if (obj == null)
			{
				obj = Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.None;
			}
			if (flag)
			{
				propertyBag[ADUserSchema.MobileFeaturesEnabled] = ((MobileFeaturesEnabled)obj & ~Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.AirSyncDisabled);
				return;
			}
			propertyBag[ADUserSchema.MobileFeaturesEnabled] = ((MobileFeaturesEnabled)obj | Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.AirSyncDisabled);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0008410C File Offset: 0x0008230C
		internal static object OWAforDevicesEnabledGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[ADUserSchema.MobileFeaturesEnabled];
			if (obj == null)
			{
				return true;
			}
			MobileFeaturesEnabled mobileFeaturesEnabled = (MobileFeaturesEnabled)obj;
			return (mobileFeaturesEnabled & Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.MowaDisabled) == Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.None;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00084144 File Offset: 0x00082344
		internal static void OWAforDevicesEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)value;
			object obj = propertyBag[ADUserSchema.MobileFeaturesEnabled];
			if (obj == null)
			{
				obj = Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.None;
			}
			if (flag)
			{
				propertyBag[ADUserSchema.MobileFeaturesEnabled] = ((MobileFeaturesEnabled)obj & ~Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.MowaDisabled);
				return;
			}
			propertyBag[ADUserSchema.MobileFeaturesEnabled] = ((MobileFeaturesEnabled)obj | Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.MowaDisabled);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x000841A4 File Offset: 0x000823A4
		internal static object MobileHasDevicePartnershipGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[ADUserSchema.MobileMailboxFlags];
			if (obj == null)
			{
				return false;
			}
			MobileMailboxFlags mobileMailboxFlags = (MobileMailboxFlags)obj;
			return (mobileMailboxFlags & Microsoft.Exchange.Data.Directory.Recipient.MobileMailboxFlags.HasDevicePartnership) != Microsoft.Exchange.Data.Directory.Recipient.MobileMailboxFlags.None;
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x000841DC File Offset: 0x000823DC
		internal static void MobileHasDevicePartnershipSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)value;
			object obj = propertyBag[ADUserSchema.MobileMailboxFlags];
			if (obj == null)
			{
				obj = Microsoft.Exchange.Data.Directory.Recipient.MobileMailboxFlags.None;
			}
			if (flag)
			{
				propertyBag[ADUserSchema.MobileMailboxFlags] = ((MobileMailboxFlags)obj | Microsoft.Exchange.Data.Directory.Recipient.MobileMailboxFlags.HasDevicePartnership);
				return;
			}
			propertyBag[ADUserSchema.MobileMailboxFlags] = ((MobileMailboxFlags)obj & ~Microsoft.Exchange.Data.Directory.Recipient.MobileMailboxFlags.HasDevicePartnership);
		}

		// Token: 0x0400104B RID: 4171
		public static readonly ADPropertyDefinition Birthdate = new ADPropertyDefinition("Birthdate", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), null, ADPropertyDefinitionFlags.NonADProperty, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.Birthdate);

		// Token: 0x0400104C RID: 4172
		public static readonly ADPropertyDefinition Country = new ADPropertyDefinition("Country", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.Country);

		// Token: 0x0400104D RID: 4173
		public static readonly ADPropertyDefinition Gender = new ADPropertyDefinition("Gender", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.Gender);

		// Token: 0x0400104E RID: 4174
		public static readonly ADPropertyDefinition MemberName = new ADPropertyDefinition("MemberName", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.MemberName);

		// Token: 0x0400104F RID: 4175
		public static readonly ADPropertyDefinition Occupation = new ADPropertyDefinition("Occupation", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.Occupation);

		// Token: 0x04001050 RID: 4176
		public static readonly ADPropertyDefinition Region = new ADPropertyDefinition("Region", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.Region);

		// Token: 0x04001051 RID: 4177
		public static readonly ADPropertyDefinition Timezone = new ADPropertyDefinition("Timezone", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.Timezone);

		// Token: 0x04001052 RID: 4178
		public static readonly ADPropertyDefinition BirthdayPrecision = new ADPropertyDefinition("BirthdayPrecision", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.BirthdayPrecision);

		// Token: 0x04001053 RID: 4179
		public static readonly ADPropertyDefinition NameVersion = new ADPropertyDefinition("NameVersion", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.NameVersion);

		// Token: 0x04001054 RID: 4180
		public static readonly ADPropertyDefinition OptInUser = new ADPropertyDefinition("OptInUser", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.NonADProperty, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.OptInUser);

		// Token: 0x04001055 RID: 4181
		public static readonly ADPropertyDefinition IsMigratedConsumerMailbox = new ADPropertyDefinition("IsMigratedConsumerMailbox", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.NonADProperty, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.IsMigratedConsumerMailbox);

		// Token: 0x04001056 RID: 4182
		public static readonly ADPropertyDefinition MigrationDryRun = new ADPropertyDefinition("MigrationDryRun", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.NonADProperty, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.MigrationDryRun);

		// Token: 0x04001057 RID: 4183
		public static readonly ADPropertyDefinition IsPremiumConsumerMailbox = new ADPropertyDefinition("IsPremiumConsumerMailbox", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.NonADProperty, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.IsPremiumConsumerMailbox);

		// Token: 0x04001058 RID: 4184
		public static readonly ADPropertyDefinition AlternateSupportEmailAddresses = new ADPropertyDefinition("AlternateSupportEmailAddresses", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.AlternateSupportEmailAddresses);

		// Token: 0x04001059 RID: 4185
		public static readonly ADPropertyDefinition ExchangeUserAccountControl = new ADPropertyDefinition("ExchangeUserAccountControl", ExchangeObjectVersion.Exchange2003, typeof(UserAccountControlFlags), "msExchUserAccountControl", ADPropertyDefinitionFlags.PersistDefaultValue, UserAccountControlFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400105A RID: 4186
		public static readonly ADPropertyDefinition LocaleID = new ADPropertyDefinition("LocaleID", ExchangeObjectVersion.Exchange2003, typeof(int), "localeID", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.LocaleID);

		// Token: 0x0400105B RID: 4187
		public static readonly ADPropertyDefinition MobileFeaturesEnabled = new ADPropertyDefinition("MobileFeaturesEnabled", ExchangeObjectVersion.Exchange2003, typeof(MobileFeaturesEnabled), "msExchOmaAdminWirelessEnable", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.Recipient.MobileFeaturesEnabled.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.MobileFeaturesEnabled);

		// Token: 0x0400105C RID: 4188
		public static readonly ADPropertyDefinition MobileMailboxFlags = new ADPropertyDefinition("MobileMailboxFlags", ExchangeObjectVersion.Exchange2007, typeof(MobileMailboxFlags), "msExchMobileMailboxFlags", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.Recipient.MobileMailboxFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400105D RID: 4189
		public static readonly ADPropertyDefinition MobileAdminExtendedSettings = new ADPropertyDefinition("MobileAdminExtendedSettings", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchOmaAdminExtendedSettings", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400105E RID: 4190
		public static readonly ADPropertyDefinition ActiveSyncAllowedDeviceIDs = new ADPropertyDefinition("ActiveSyncAllowedDeviceIDs", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMobileAllowedDeviceIds", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.ActiveSyncAllowedDeviceIDs);

		// Token: 0x0400105F RID: 4191
		public static readonly ADPropertyDefinition ActiveSyncBlockedDeviceIDs = new ADPropertyDefinition("ActiveSyncBlockedDeviceIDs", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchMobileBlockedDeviceIds", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.ActiveSyncBlockedDeviceIDs);

		// Token: 0x04001060 RID: 4192
		public static readonly ADPropertyDefinition ActiveSyncMailboxPolicy = new ADPropertyDefinition("ActiveSyncMailboxPolicy", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchMobileMailboxPolicyLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001061 RID: 4193
		public static readonly ADPropertyDefinition ActiveSyncMailboxPolicyIsDefaulted = new ADPropertyDefinition("ActiveSyncMailboxPolicyIsDefaulted", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001062 RID: 4194
		public static readonly ADPropertyDefinition CatchAllRecipientBL = new ADPropertyDefinition("CatchAllRecipientBL", ExchangeObjectVersion.Exchange2012, typeof(ADObjectId), "msExchCatchAllRecipientBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001063 RID: 4195
		public static readonly ADPropertyDefinition ActiveSyncDebugLogging = new ADPropertyDefinition("ActiveSyncDebugLogging", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchMobileDebugLogging", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.ActiveSyncDebugLogging);

		// Token: 0x04001064 RID: 4196
		public static readonly ADPropertyDefinition PasswordLastSetRaw = new ADPropertyDefinition("PasswordLastSetRaw", ExchangeObjectVersion.Exchange2003, typeof(long?), "pwdLastSet", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001065 RID: 4197
		public static readonly ADPropertyDefinition PrimaryGroupId = new ADPropertyDefinition("PrimaryGroupId", ExchangeObjectVersion.Exchange2003, typeof(int?), "primaryGroupId", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001066 RID: 4198
		public static readonly ADPropertyDefinition UnicodePassword = new ADPropertyDefinition("UnicodePassword", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "unicodePwd", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001067 RID: 4199
		public static readonly ADPropertyDefinition QueryBaseDN = new ADPropertyDefinition("QueryBaseDN", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchQueryBaseDN", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001068 RID: 4200
		public static readonly ADPropertyDefinition UMEnabledFlags = new ADPropertyDefinition("UMEnabledFlags", ExchangeObjectVersion.Exchange2007, typeof(UMEnabledFlags), "msExchUMEnabledFlags", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.Default, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001069 RID: 4201
		public static readonly ADPropertyDefinition UMEnabledFlags2 = new ADPropertyDefinition("UMEnabledFlags2", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchUMEnabledFlags2", ADPropertyDefinitionFlags.PersistDefaultValue, -1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400106A RID: 4202
		public static readonly ADPropertyDefinition AdminDisplayName = ADConfigurationObjectSchema.AdminDisplayName;

		// Token: 0x0400106B RID: 4203
		public static readonly ADPropertyDefinition AdminDisplayVersion = new ADPropertyDefinition("AdminDisplayVersion", ExchangeObjectVersion.Exchange2003, typeof(ServerVersion), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400106C RID: 4204
		public static readonly ADPropertyDefinition IntendedMailboxPlan = new ADPropertyDefinition("IntendedMailboxPlan", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchIntendedMailboxPlanLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400106D RID: 4205
		public static readonly ADPropertyDefinition IntendedMailboxPlanName = new ADPropertyDefinition("IntendedMailboxPlanName", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400106E RID: 4206
		public static readonly ADPropertyDefinition UMMailboxPolicy = new ADPropertyDefinition("UMMailboxPolicy", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchUMTemplateLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400106F RID: 4207
		public static readonly ADPropertyDefinition OperatorNumber = new ADPropertyDefinition("OperatorNumber", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMOperatorNumber", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 20)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001070 RID: 4208
		public static readonly ADPropertyDefinition PhoneProviderId = new ADPropertyDefinition("PhoneProviderId", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchUMPhoneProvider", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.PhoneProviderId);

		// Token: 0x04001071 RID: 4209
		public static readonly ADPropertyDefinition RMSComputerAccounts = new ADPropertyDefinition("RMSComputerAccounts", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, "msExchRMSComputerAccountsLink", null, "msExchRMSComputerAccountsLinkSL", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001072 RID: 4210
		public static readonly ADPropertyDefinition UMPinChecksum = new ADPropertyDefinition("UMPinChecksum", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchUMPinChecksum", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(160)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001073 RID: 4211
		public static readonly ADPropertyDefinition UMServerWritableFlags = new ADPropertyDefinition("UMServerWritableFlags", ExchangeObjectVersion.Exchange2007, typeof(UMServerWritableFlagsBits), "msExchUMServerWritableFlags", ADPropertyDefinitionFlags.None, UMServerWritableFlagsBits.MissedCallNotificationEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001074 RID: 4212
		public static readonly ADPropertyDefinition CallAnsweringAudioCodecLegacy = new ADPropertyDefinition("CallAnsweringAudioCodecLegacy", ExchangeObjectVersion.Exchange2007, typeof(AudioCodecEnum?), "msExchUMAudioCodec", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new NullableEnumValueDefinedConstraint(typeof(AudioCodecEnum))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001075 RID: 4213
		public static readonly ADPropertyDefinition CallAnsweringAudioCodec2 = new ADPropertyDefinition("CallAnsweringAudioCodec2", ExchangeObjectVersion.Exchange2010, typeof(AudioCodecEnum?), "msExchUMAudioCodec2", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001076 RID: 4214
		public static readonly ADPropertyDefinition AccessTelephoneNumbers = new ADPropertyDefinition("AccessTelephoneNumbers", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001077 RID: 4215
		public static readonly ADPropertyDefinition CallAnsweringRulesExtensions = new ADPropertyDefinition("CallAnsweringRulesExtensions", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001078 RID: 4216
		public static readonly ADPropertyDefinition UserAccountControl = new ADPropertyDefinition("UserAccountControl", ExchangeObjectVersion.Exchange2003, typeof(UserAccountControlFlags), "userAccountControl", ADPropertyDefinitionFlags.DoNotProvisionalClone, UserAccountControlFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001079 RID: 4217
		public static readonly ADPropertyDefinition UserPrincipalNameRaw = new ADPropertyDefinition("UserPrincipalNameRaw", ExchangeObjectVersion.Exchange2003, typeof(string), "userPrincipalName", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new MandatoryStringLengthConstraint(1, 1024),
			new RegexConstraint("^.*@[^@]+$", DataStrings.UserPrincipalNamePatternDescription)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400107A RID: 4218
		public static readonly ADPropertyDefinition UserPrincipalName = new ADPropertyDefinition("UserPrincipalName", ExchangeObjectVersion.Exchange2003, typeof(string), "userPrincipalName", ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADUserSchema.UserPrincipalNameRaw
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ADUser.UserPrincipalNameGetter), new SetterDelegate(ADUser.UserPrincipalNameSetter), null, null);

		// Token: 0x0400107B RID: 4219
		public static readonly ADPropertyDefinition AltSecurityIdentities = IADSecurityPrincipalSchema.AltSecurityIdentities;

		// Token: 0x0400107C RID: 4220
		public static readonly ADPropertyDefinition NetID = IADSecurityPrincipalSchema.NetID;

		// Token: 0x0400107D RID: 4221
		public static readonly ADPropertyDefinition OriginalNetID = IADSecurityPrincipalSchema.OriginalNetID;

		// Token: 0x0400107E RID: 4222
		public static readonly ADPropertyDefinition NetIDSuffix = IADSecurityPrincipalSchema.NetIDSuffix;

		// Token: 0x0400107F RID: 4223
		public static readonly ADPropertyDefinition ConsumerNetID = IADSecurityPrincipalSchema.ConsumerNetID;

		// Token: 0x04001080 RID: 4224
		public static readonly ADPropertyDefinition CertificateSubject = IADSecurityPrincipalSchema.CertificateSubject;

		// Token: 0x04001081 RID: 4225
		public static readonly ADPropertyDefinition PreviousDatabase = IADMailStorageSchema.PreviousDatabase;

		// Token: 0x04001082 RID: 4226
		public static readonly ADPropertyDefinition ElcExpirationSuspensionEndDate = IADMailStorageSchema.ElcExpirationSuspensionEndDate;

		// Token: 0x04001083 RID: 4227
		public static readonly ADPropertyDefinition ElcExpirationSuspensionStartDate = IADMailStorageSchema.ElcExpirationSuspensionStartDate;

		// Token: 0x04001084 RID: 4228
		public static readonly ADPropertyDefinition ElcMailboxFlags = IADMailStorageSchema.ElcMailboxFlags;

		// Token: 0x04001085 RID: 4229
		public static readonly ADPropertyDefinition RetentionComment = IADMailStorageSchema.RetentionComment;

		// Token: 0x04001086 RID: 4230
		public static readonly ADPropertyDefinition RetentionUrl = IADMailStorageSchema.RetentionUrl;

		// Token: 0x04001087 RID: 4231
		public static readonly ADPropertyDefinition ElcPolicyTemplate = IADMailStorageSchema.ElcPolicyTemplate;

		// Token: 0x04001088 RID: 4232
		public static readonly ADPropertyDefinition ManagedFolderMailboxPolicy = IADMailStorageSchema.ManagedFolderMailboxPolicy;

		// Token: 0x04001089 RID: 4233
		public static readonly ADPropertyDefinition RetentionPolicy = IADMailStorageSchema.RetentionPolicy;

		// Token: 0x0400108A RID: 4234
		public static readonly ADPropertyDefinition ShouldUseDefaultRetentionPolicy = IADMailStorageSchema.ShouldUseDefaultRetentionPolicy;

		// Token: 0x0400108B RID: 4235
		public static readonly ADPropertyDefinition SharingPolicy = IADMailStorageSchema.SharingPolicy;

		// Token: 0x0400108C RID: 4236
		public static readonly ADPropertyDefinition RemoteAccountPolicy = IADMailStorageSchema.RemoteAccountPolicy;

		// Token: 0x0400108D RID: 4237
		public static readonly ADPropertyDefinition UseDatabaseRetentionDefaults = IADMailStorageSchema.UseDatabaseRetentionDefaults;

		// Token: 0x0400108E RID: 4238
		public static readonly ADPropertyDefinition RetainDeletedItemsUntilBackup = IADMailStorageSchema.RetainDeletedItemsUntilBackup;

		// Token: 0x0400108F RID: 4239
		public static readonly ADPropertyDefinition MailboxContainerGuid = IADMailStorageSchema.MailboxContainerGuid;

		// Token: 0x04001090 RID: 4240
		public static readonly ADPropertyDefinition AggregatedMailboxGuids = IADMailStorageSchema.AggregatedMailboxGuids;

		// Token: 0x04001091 RID: 4241
		public static readonly ADPropertyDefinition UnifiedMailbox = IADMailStorageSchema.UnifiedMailbox;

		// Token: 0x04001092 RID: 4242
		public static readonly ADPropertyDefinition PreviousExchangeGuid = IADMailStorageSchema.PreviousExchangeGuid;

		// Token: 0x04001093 RID: 4243
		public static readonly ADPropertyDefinition RecoverableItemsQuota = IADMailStorageSchema.RecoverableItemsQuota;

		// Token: 0x04001094 RID: 4244
		public static readonly ADPropertyDefinition RecoverableItemsWarningQuota = IADMailStorageSchema.RecoverableItemsWarningQuota;

		// Token: 0x04001095 RID: 4245
		public static readonly ADPropertyDefinition CalendarLoggingQuota = IADMailStorageSchema.CalendarLoggingQuota;

		// Token: 0x04001096 RID: 4246
		public static readonly ADPropertyDefinition ApprovalApplications = IADMailStorageSchema.ApprovalApplications;

		// Token: 0x04001097 RID: 4247
		public static readonly ADPropertyDefinition SharingPartnerIdentities = IADMailStorageSchema.SharingPartnerIdentities;

		// Token: 0x04001098 RID: 4248
		public static readonly ADPropertyDefinition SharingAnonymousIdentities = IADMailStorageSchema.SharingAnonymousIdentities;

		// Token: 0x04001099 RID: 4249
		public static readonly ADPropertyDefinition DatabaseName = IADMailStorageSchema.DatabaseName;

		// Token: 0x0400109A RID: 4250
		public static readonly ADPropertyDefinition LitigationHoldEnabled = IADMailStorageSchema.LitigationHoldEnabled;

		// Token: 0x0400109B RID: 4251
		public static readonly ADPropertyDefinition SingleItemRecoveryEnabled = IADMailStorageSchema.SingleItemRecoveryEnabled;

		// Token: 0x0400109C RID: 4252
		public static readonly ADPropertyDefinition ElcExpirationSuspensionEnabled = IADMailStorageSchema.ElcExpirationSuspensionEnabled;

		// Token: 0x0400109D RID: 4253
		public static readonly ADPropertyDefinition CalendarRepairDisabled = new ADPropertyDefinition("CalendarRepairDisabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchCalendarRepairDisabled", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.CalendarRepairDisabled);

		// Token: 0x0400109E RID: 4254
		public static readonly ADPropertyDefinition StorageGroupName = IADMailStorageSchema.StorageGroupName;

		// Token: 0x0400109F RID: 4255
		public static readonly ADPropertyDefinition SecurityProtocol = new ADPropertyDefinition("SecurityProtocol", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "securityProtocol", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040010A0 RID: 4256
		public static readonly ADPropertyDefinition OwaMailboxPolicy = new ADPropertyDefinition("OwaMailboxPolicy", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchOWAPolicy", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040010A1 RID: 4257
		public static readonly ADPropertyDefinition MaxSafeSenders = new ADPropertyDefinition("MaxSafeSenders", ExchangeObjectVersion.Exchange2003, typeof(int?), "msExchMaxSafeSenders", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.MaxSafeSenders);

		// Token: 0x040010A2 RID: 4258
		public static readonly ADPropertyDefinition MaxBlockedSenders = new ADPropertyDefinition("MaxBlockedSenders", ExchangeObjectVersion.Exchange2003, typeof(int?), "msExchMaxBlockedSenders", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.MaxBlockedSenders);

		// Token: 0x040010A3 RID: 4259
		public static readonly ADPropertyDefinition RTCSIPPrimaryUserAddress = new ADPropertyDefinition("RTCSIPPrimaryUserAddress", ExchangeObjectVersion.Exchange2003, typeof(string), "msRTCSIP-PrimaryUserAddress", ADPropertyDefinitionFlags.ReadOnly, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040010A4 RID: 4260
		public static readonly ADPropertyDefinition RemoteRecipientType = IADMailStorageSchema.RemoteRecipientType;

		// Token: 0x040010A5 RID: 4261
		public static readonly ADPropertyDefinition ArchiveDatabaseRaw = IADMailStorageSchema.ArchiveDatabaseRaw;

		// Token: 0x040010A6 RID: 4262
		public static readonly ADPropertyDefinition ArchiveDatabase = IADMailStorageSchema.ArchiveDatabase;

		// Token: 0x040010A7 RID: 4263
		public static readonly ADPropertyDefinition ArchiveGuid = IADMailStorageSchema.ArchiveGuid;

		// Token: 0x040010A8 RID: 4264
		public static readonly ADPropertyDefinition ArchiveName = IADMailStorageSchema.ArchiveName;

		// Token: 0x040010A9 RID: 4265
		public static readonly ADPropertyDefinition ArchiveQuota = IADMailStorageSchema.ArchiveQuota;

		// Token: 0x040010AA RID: 4266
		public static readonly ADPropertyDefinition ArchiveWarningQuota = IADMailStorageSchema.ArchiveWarningQuota;

		// Token: 0x040010AB RID: 4267
		public static readonly ADPropertyDefinition ArchiveDomain = IADMailStorageSchema.ArchiveDomain;

		// Token: 0x040010AC RID: 4268
		public static readonly ADPropertyDefinition ArchiveStatus = IADMailStorageSchema.ArchiveStatus;

		// Token: 0x040010AD RID: 4269
		public static readonly ADPropertyDefinition ArchiveState = IADMailStorageSchema.ArchiveState;

		// Token: 0x040010AE RID: 4270
		public static readonly ADPropertyDefinition DisabledArchiveGuid = IADMailStorageSchema.DisabledArchiveGuid;

		// Token: 0x040010AF RID: 4271
		public static readonly ADPropertyDefinition DisabledArchiveDatabase = IADMailStorageSchema.DisabledArchiveDatabase;

		// Token: 0x040010B0 RID: 4272
		public static readonly ADPropertyDefinition IsAuxMailbox = IADMailStorageSchema.IsAuxMailbox;

		// Token: 0x040010B1 RID: 4273
		public static readonly ADPropertyDefinition AuxMailboxParentObjectId = IADMailStorageSchema.AuxMailboxParentObjectId;

		// Token: 0x040010B2 RID: 4274
		public static readonly ADPropertyDefinition AuxMailboxParentObjectIdBL = IADMailStorageSchema.AuxMailboxParentObjectIdBL;

		// Token: 0x040010B3 RID: 4275
		public static readonly ADPropertyDefinition MailboxRelationType = IADMailStorageSchema.MailboxRelationType;

		// Token: 0x040010B4 RID: 4276
		public static readonly ADPropertyDefinition MailboxMoveTargetMDB = IADMailStorageSchema.MailboxMoveTargetMDB;

		// Token: 0x040010B5 RID: 4277
		public static readonly ADPropertyDefinition MailboxMoveSourceMDB = IADMailStorageSchema.MailboxMoveSourceMDB;

		// Token: 0x040010B6 RID: 4278
		public static readonly ADPropertyDefinition MailboxMoveTargetArchiveMDB = IADMailStorageSchema.MailboxMoveTargetArchiveMDB;

		// Token: 0x040010B7 RID: 4279
		public static readonly ADPropertyDefinition MailboxMoveSourceArchiveMDB = IADMailStorageSchema.MailboxMoveSourceArchiveMDB;

		// Token: 0x040010B8 RID: 4280
		public static readonly ADPropertyDefinition MailboxMoveFlags = IADMailStorageSchema.MailboxMoveFlags;

		// Token: 0x040010B9 RID: 4281
		public static readonly ADPropertyDefinition MailboxMoveRemoteHostName = IADMailStorageSchema.MailboxMoveRemoteHostName;

		// Token: 0x040010BA RID: 4282
		public static readonly ADPropertyDefinition MailboxMoveBatchName = IADMailStorageSchema.MailboxMoveBatchName;

		// Token: 0x040010BB RID: 4283
		public static readonly ADPropertyDefinition MailboxMoveStatus = IADMailStorageSchema.MailboxMoveStatus;

		// Token: 0x040010BC RID: 4284
		public static readonly ADPropertyDefinition MailboxRelease = SharedPropertyDefinitions.MailboxRelease;

		// Token: 0x040010BD RID: 4285
		public static readonly ADPropertyDefinition ArchiveRelease = IADMailStorageSchema.ArchiveRelease;

		// Token: 0x040010BE RID: 4286
		public static readonly ADPropertyDefinition CalendarVersionStoreDisabled = IADMailStorageSchema.CalendarVersionStoreDisabled;

		// Token: 0x040010BF RID: 4287
		public static readonly ADPropertyDefinition SIPResourceIdentifier = new ADPropertyDefinition("SIPResourceIdentifier", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040010C0 RID: 4288
		public static readonly ADPropertyDefinition PhoneNumber = new ADPropertyDefinition("PhoneNumber", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040010C1 RID: 4289
		public static readonly ADPropertyDefinition SourceAnchor = new ADPropertyDefinition("SourceAnchor", ExchangeObjectVersion.Exchange2010, typeof(string), "ms-MSCustomerObjectGUIDString", ADPropertyDefinitionFlags.ReadOnly, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040010C2 RID: 4290
		public static readonly ADPropertyDefinition TeamMailboxClosedTime = IADMailStorageSchema.TeamMailboxClosedTime;

		// Token: 0x040010C3 RID: 4291
		public static readonly ADPropertyDefinition SharePointSiteInfo = IADMailStorageSchema.SharePointSiteInfo;

		// Token: 0x040010C4 RID: 4292
		public static readonly ADPropertyDefinition SharePointLinkedBy = IADMailStorageSchema.SharePointLinkedBy;

		// Token: 0x040010C5 RID: 4293
		public static readonly ADPropertyDefinition Owners = IADMailStorageSchema.Owners;

		// Token: 0x040010C6 RID: 4294
		public static readonly ADPropertyDefinition SiteMailboxMessageDedupEnabled = IADMailStorageSchema.SiteMailboxMessageDedupEnabled;

		// Token: 0x040010C7 RID: 4295
		public static readonly ADPropertyDefinition TeamMailboxShowInMyClient = new ADPropertyDefinition("TeamMailboxShowInMyClient", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040010C8 RID: 4296
		public static readonly ADPropertyDefinition TeamMailboxUserMembership = new ADPropertyDefinition("TeamMailboxUserMembership", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040010C9 RID: 4297
		public static readonly ADPropertyDefinition TeamMailboxShowInClientList = IADMailStorageSchema.TeamMailboxShowInClientList;

		// Token: 0x040010CA RID: 4298
		public static readonly ADPropertyDefinition LitigationHoldDate = IADMailStorageSchema.LitigationHoldDate;

		// Token: 0x040010CB RID: 4299
		public static readonly ADPropertyDefinition LitigationHoldOwner = IADMailStorageSchema.LitigationHoldOwner;

		// Token: 0x040010CC RID: 4300
		public static readonly ADPropertyDefinition SatchmoClusterIp = IADMailStorageSchema.SatchmoClusterIp;

		// Token: 0x040010CD RID: 4301
		public static readonly ADPropertyDefinition SatchmoDGroup = IADMailStorageSchema.SatchmoDGroup;

		// Token: 0x040010CE RID: 4302
		public static readonly ADPropertyDefinition PrimaryMailboxSource = IADMailStorageSchema.PrimaryMailboxSource;

		// Token: 0x040010CF RID: 4303
		public static readonly ADPropertyDefinition FblEnabled = IADMailStorageSchema.FblEnabled;

		// Token: 0x040010D0 RID: 4304
		public static readonly ADPropertyDefinition AccountDisabled = new ADPropertyDefinition("AccountDisabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.ExchangeUserAccountControl
		}, null, ADObject.FlagGetterDelegate(ADUserSchema.ExchangeUserAccountControl, 2), ADUserSchema.EnumFlagSetterDelegate(ADUserSchema.ExchangeUserAccountControl, 2), null, null);

		// Token: 0x040010D1 RID: 4305
		public static readonly ADPropertyDefinition StsRefreshTokensValidFrom = new ADPropertyDefinition("StsRefreshTokensValidFrom", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), "msExchStsRefreshTokensValidFrom", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040010D2 RID: 4306
		public static readonly ADPropertyDefinition PasswordLastSet = new ADPropertyDefinition("PasswordLastSet", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.PasswordLastSetRaw
		}, null, new GetterDelegate(ADUser.PasswordLastSetGetter), null, null, null);

		// Token: 0x040010D3 RID: 4307
		public static readonly ADPropertyDefinition PersistedCapabilities = SharedPropertyDefinitions.PersistedCapabilities;

		// Token: 0x040010D4 RID: 4308
		public static readonly ADPropertyDefinition TeamMailboxMembers = new ADPropertyDefinition("TeamMailboxMembers", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.Owners,
			ADMailboxRecipientSchema.DelegateListLink
		}, null, new GetterDelegate(TeamMailbox.MembersGetter), null, null, null);

		// Token: 0x040010D5 RID: 4309
		public static readonly ADPropertyDefinition SiteMailboxWebCollectionUrl = new ADPropertyDefinition("SiteMailboxWebCollectionUrl", ExchangeObjectVersion.Exchange2010, typeof(Uri), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.SharePointSiteInfo
		}, null, new GetterDelegate(TeamMailbox.WebCollectionUrlGetter), null, null, null);

		// Token: 0x040010D6 RID: 4310
		public static readonly ADPropertyDefinition SiteMailboxWebId = new ADPropertyDefinition("SiteMailboxWebId", ExchangeObjectVersion.Exchange2010, typeof(Guid), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.SharePointSiteInfo
		}, null, new GetterDelegate(TeamMailbox.WebIdGetter), null, null, null);

		// Token: 0x040010D7 RID: 4311
		public static readonly ADPropertyDefinition CallAnsweringAudioCodec = new ADPropertyDefinition("CallAnsweringAudioCodec", ExchangeObjectVersion.Exchange2010, typeof(AudioCodecEnum?), null, ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(AudioCodecEnum))
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.CallAnsweringAudioCodec2,
			ADUserSchema.CallAnsweringAudioCodecLegacy
		}, null, UMDialPlanSchema.AudioCodecGetterDelegate(ADUserSchema.CallAnsweringAudioCodec2, ADUserSchema.CallAnsweringAudioCodecLegacy, null), UMDialPlanSchema.AudioCodecSetterDelegate(ADUserSchema.CallAnsweringAudioCodec2, ADUserSchema.CallAnsweringAudioCodecLegacy), null, null);

		// Token: 0x040010D8 RID: 4312
		public static readonly ADPropertyDefinition ActiveSyncEnabled = new ADPropertyDefinition("ActiveSyncEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.MobileFeaturesEnabled
		}, new CustomFilterBuilderDelegate(ADUser.ActiveSyncEnabledFilterBuilder), new GetterDelegate(ADUserSchema.MobileSyncEnabledGetter), new SetterDelegate(ADUserSchema.MobileSyncEnabledSetter), null, MbxRecipientSchema.ActiveSyncEnabled);

		// Token: 0x040010D9 RID: 4313
		public static readonly ADPropertyDefinition HasActiveSyncDevicePartnership = new ADPropertyDefinition("HasActiveSyncDevicePartnership", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.MobileMailboxFlags
		}, new CustomFilterBuilderDelegate(ADUser.HasActiveSyncDevicePartnershipFilterBuilder), new GetterDelegate(ADUserSchema.MobileHasDevicePartnershipGetter), new SetterDelegate(ADUserSchema.MobileHasDevicePartnershipSetter), null, MbxRecipientSchema.HasActiveSyncDevicePartnership);

		// Token: 0x040010DA RID: 4314
		public static readonly ADPropertyDefinition OWAforDevicesEnabled = new ADPropertyDefinition("OWAforDevicesEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.MobileFeaturesEnabled
		}, new CustomFilterBuilderDelegate(ADUser.OWAforDevicesEnabledFilterBuilder), new GetterDelegate(ADUserSchema.OWAforDevicesEnabledGetter), new SetterDelegate(ADUserSchema.OWAforDevicesEnabledSetter), null, MbxRecipientSchema.OWAforDevicesEnabled);

		// Token: 0x040010DB RID: 4315
		public static readonly ADPropertyDefinition ResetPasswordOnNextLogon = new ADPropertyDefinition("ResetPasswordOnNextLogon", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
		{
			ADUserSchema.PasswordLastSetRaw
		}, null, new GetterDelegate(ADUser.ResetPasswordOnNextLogonGetter), new SetterDelegate(ADUser.ResetPasswordOnNextLogonSetter), null, MbxRecipientSchema.ResetPasswordOnNextLogon);

		// Token: 0x040010DC RID: 4316
		public static readonly ADPropertyDefinition IsPilotMailboxPlan = ADObject.BitfieldProperty("IsPilotMailboxPlan", 9, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x040010DD RID: 4317
		public static readonly ADPropertyDefinition UMEnabled = new ADPropertyDefinition("UMEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags
		}, new CustomFilterBuilderDelegate(ADRecipient.UMEnabledFilterBuilder), (IPropertyBag propertyBag) => ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.UMEnabled) == Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.UMEnabled, delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] | Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.UMEnabled);
				return;
			}
			propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & ~Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.UMEnabled);
		}, null, null);

		// Token: 0x040010DE RID: 4318
		public static readonly ADPropertyDefinition DowngradeHighPriorityMessagesEnabled = new ADPropertyDefinition("DowngradeHighPriorityMessagesEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.SecurityProtocol
		}, null, new GetterDelegate(ADUser.DowngradeHighPriorityMessagesEnabledGetter), new SetterDelegate(ADUser.DowngradeHighPriorityMessagesEnabledSetter), null, MbxRecipientSchema.DowngradeHighPriorityMessagesEnabled);

		// Token: 0x040010DF RID: 4319
		public static readonly ADPropertyDefinition TUIAccessToCalendarEnabled = new ADPropertyDefinition("TUIAccessToCalendarEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags
		}, null, (IPropertyBag propertyBag) => ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.TUIAccessToCalendarEnabled) == Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.TUIAccessToCalendarEnabled, delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] | Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.TUIAccessToCalendarEnabled);
				return;
			}
			propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & ~Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.TUIAccessToCalendarEnabled);
		}, null, null);

		// Token: 0x040010E0 RID: 4320
		public static readonly ADPropertyDefinition FaxEnabled = new ADPropertyDefinition("FaxEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags
		}, null, (IPropertyBag propertyBag) => ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.FaxEnabled) == Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.FaxEnabled, delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] | Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.FaxEnabled);
				return;
			}
			propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & ~Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.FaxEnabled);
		}, null, null);

		// Token: 0x040010E1 RID: 4321
		public static readonly ADPropertyDefinition TUIAccessToEmailEnabled = new ADPropertyDefinition("TUIAccessToEmailEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags
		}, null, (IPropertyBag propertyBag) => ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.TUIAccessToEmailEnabled) == Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.TUIAccessToEmailEnabled, delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] | Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.TUIAccessToEmailEnabled);
				return;
			}
			propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & ~Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.TUIAccessToEmailEnabled);
		}, null, null);

		// Token: 0x040010E2 RID: 4322
		public static readonly ADPropertyDefinition SubscriberAccessEnabled = new ADPropertyDefinition("SubscriberAccessEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags
		}, null, (IPropertyBag propertyBag) => ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.SubscriberAccessEnabled) == Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.SubscriberAccessEnabled, delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] | Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.SubscriberAccessEnabled);
				return;
			}
			propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & ~Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.SubscriberAccessEnabled);
		}, null, MbxRecipientSchema.SubscriberAccessEnabled);

		// Token: 0x040010E3 RID: 4323
		public static readonly ADPropertyDefinition MissedCallNotificationEnabled = new ADPropertyDefinition("MissedCallNotificationEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMServerWritableFlags
		}, null, (IPropertyBag propertyBag) => ((UMServerWritableFlagsBits)propertyBag[ADUserSchema.UMServerWritableFlags] & UMServerWritableFlagsBits.MissedCallNotificationEnabled) == UMServerWritableFlagsBits.MissedCallNotificationEnabled, delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ADUserSchema.UMServerWritableFlags] = ((UMServerWritableFlagsBits)propertyBag[ADUserSchema.UMServerWritableFlags] | UMServerWritableFlagsBits.MissedCallNotificationEnabled);
				return;
			}
			propertyBag[ADUserSchema.UMServerWritableFlags] = ((UMServerWritableFlagsBits)propertyBag[ADUserSchema.UMServerWritableFlags] & ~UMServerWritableFlagsBits.MissedCallNotificationEnabled);
		}, null, null);

		// Token: 0x040010E4 RID: 4324
		public static readonly ADPropertyDefinition UMSMSNotificationOption = new ADPropertyDefinition("UMSMSNotificationOption", ExchangeObjectVersion.Exchange2010, typeof(UMSMSNotificationOptions), null, ADPropertyDefinitionFlags.Calculated, UMSMSNotificationOptions.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMServerWritableFlags
		}, null, delegate(IPropertyBag propertyBag)
		{
			bool flag = ((UMServerWritableFlagsBits)propertyBag[ADUserSchema.UMServerWritableFlags] & UMServerWritableFlagsBits.SMSVoiceMailNotificationEnabled) == UMServerWritableFlagsBits.SMSVoiceMailNotificationEnabled;
			bool flag2 = ((UMServerWritableFlagsBits)propertyBag[ADUserSchema.UMServerWritableFlags] & UMServerWritableFlagsBits.SMSMissedCallNotificationEnabled) == UMServerWritableFlagsBits.SMSMissedCallNotificationEnabled;
			if (flag && flag2)
			{
				return UMSMSNotificationOptions.VoiceMailAndMissedCalls;
			}
			if (flag)
			{
				return UMSMSNotificationOptions.VoiceMail;
			}
			return UMSMSNotificationOptions.None;
		}, delegate(object value, IPropertyBag propertyBag)
		{
			UMServerWritableFlagsBits umserverWritableFlagsBits = (UMServerWritableFlagsBits)propertyBag[ADUserSchema.UMServerWritableFlags];
			UMServerWritableFlagsBits umserverWritableFlagsBits2 = UMServerWritableFlagsBits.SMSVoiceMailNotificationEnabled;
			UMServerWritableFlagsBits umserverWritableFlagsBits3 = UMServerWritableFlagsBits.SMSMissedCallNotificationEnabled;
			umserverWritableFlagsBits &= ~umserverWritableFlagsBits2;
			umserverWritableFlagsBits &= ~umserverWritableFlagsBits3;
			UMSMSNotificationOptions umsmsnotificationOptions = (UMSMSNotificationOptions)value;
			if (umsmsnotificationOptions == UMSMSNotificationOptions.VoiceMail)
			{
				umserverWritableFlagsBits |= umserverWritableFlagsBits2;
			}
			else if (umsmsnotificationOptions == UMSMSNotificationOptions.VoiceMailAndMissedCalls)
			{
				umserverWritableFlagsBits |= umserverWritableFlagsBits2;
				umserverWritableFlagsBits |= umserverWritableFlagsBits3;
			}
			propertyBag[ADUserSchema.UMServerWritableFlags] = umserverWritableFlagsBits;
		}, null, null);

		// Token: 0x040010E5 RID: 4325
		public static readonly ADPropertyDefinition PinlessAccessToVoiceMailEnabled = new ADPropertyDefinition("PinlessAccessToVoiceMailEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMServerWritableFlags
		}, null, (IPropertyBag propertyBag) => ((UMServerWritableFlagsBits)propertyBag[ADUserSchema.UMServerWritableFlags] & UMServerWritableFlagsBits.PinlessAccessToVoiceMailEnabled) == UMServerWritableFlagsBits.PinlessAccessToVoiceMailEnabled, delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ADUserSchema.UMServerWritableFlags] = ((UMServerWritableFlagsBits)propertyBag[ADUserSchema.UMServerWritableFlags] | UMServerWritableFlagsBits.PinlessAccessToVoiceMailEnabled);
				return;
			}
			propertyBag[ADUserSchema.UMServerWritableFlags] = ((UMServerWritableFlagsBits)propertyBag[ADUserSchema.UMServerWritableFlags] & ~UMServerWritableFlagsBits.PinlessAccessToVoiceMailEnabled);
		}, null, null);

		// Token: 0x040010E6 RID: 4326
		public static readonly ADPropertyDefinition AnonymousCallersCanLeaveMessages = new ADPropertyDefinition("AnonymousCallersCanLeaveMessages", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags
		}, null, (IPropertyBag propertyBag) => ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.AnonymousCallersCanLeaveMessages) == Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.AnonymousCallersCanLeaveMessages, delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] | Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.AnonymousCallersCanLeaveMessages);
				return;
			}
			propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & ~Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.AnonymousCallersCanLeaveMessages);
		}, null, null);

		// Token: 0x040010E7 RID: 4327
		public static readonly ADPropertyDefinition ASREnabled = new ADPropertyDefinition("ASREnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags
		}, null, (IPropertyBag propertyBag) => ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.ASREnabled) == Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.ASREnabled, delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] | Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.ASREnabled);
				return;
			}
			propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & ~Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.ASREnabled);
		}, null, null);

		// Token: 0x040010E8 RID: 4328
		public static readonly ADPropertyDefinition VoiceMailAnalysisEnabled = new ADPropertyDefinition("VoiceMailAnalysisEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags
		}, null, (IPropertyBag propertyBag) => ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.VoiceMailAnalysisEnabled) == Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.VoiceMailAnalysisEnabled, delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] | Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.VoiceMailAnalysisEnabled);
				return;
			}
			propertyBag[ADUserSchema.UMEnabledFlags] = ((UMEnabledFlags)propertyBag[ADUserSchema.UMEnabledFlags] & ~Microsoft.Exchange.Data.Directory.Recipient.UMEnabledFlags.VoiceMailAnalysisEnabled);
		}, null, null);

		// Token: 0x040010E9 RID: 4329
		public static readonly ADPropertyDefinition PlayOnPhoneEnabled = new ADPropertyDefinition("PlayOnPhoneEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags2
		}, null, ADObject.FlagGetterDelegate(ADUserSchema.UMEnabledFlags2, 1), ADObject.FlagSetterDelegate(ADUserSchema.UMEnabledFlags2, 1), null, null);

		// Token: 0x040010EA RID: 4330
		public static readonly ADPropertyDefinition CallAnsweringRulesEnabled = new ADPropertyDefinition("CallAnsweringRulesEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags2
		}, null, ADObject.FlagGetterDelegate(ADUserSchema.UMEnabledFlags2, 4), ADObject.FlagSetterDelegate(ADUserSchema.UMEnabledFlags2, 4), null, null);

		// Token: 0x040010EB RID: 4331
		public static readonly ADPropertyDefinition Company = ADOrgPersonSchema.Company;

		// Token: 0x040010EC RID: 4332
		public static readonly ADPropertyDefinition Co = ADOrgPersonSchema.Co;

		// Token: 0x040010ED RID: 4333
		internal static readonly ADPropertyDefinition C = ADOrgPersonSchema.C;

		// Token: 0x040010EE RID: 4334
		internal static readonly ADPropertyDefinition CountryCode = ADOrgPersonSchema.CountryCode;

		// Token: 0x040010EF RID: 4335
		public static readonly ADPropertyDefinition Department = ADOrgPersonSchema.Department;

		// Token: 0x040010F0 RID: 4336
		public static readonly ADPropertyDefinition DirectReports = ADOrgPersonSchema.DirectReports;

		// Token: 0x040010F1 RID: 4337
		public static readonly ADPropertyDefinition Fax = ADOrgPersonSchema.Fax;

		// Token: 0x040010F2 RID: 4338
		public static readonly ADPropertyDefinition FirstName = ADOrgPersonSchema.FirstName;

		// Token: 0x040010F3 RID: 4339
		public static readonly ADPropertyDefinition HomePhone = ADOrgPersonSchema.HomePhone;

		// Token: 0x040010F4 RID: 4340
		public static readonly ADPropertyDefinition Initials = ADOrgPersonSchema.Initials;

		// Token: 0x040010F5 RID: 4341
		public static readonly ADPropertyDefinition LanguagesRaw = ADOrgPersonSchema.LanguagesRaw;

		// Token: 0x040010F6 RID: 4342
		public static readonly ADPropertyDefinition LastName = ADOrgPersonSchema.LastName;

		// Token: 0x040010F7 RID: 4343
		public static readonly ADPropertyDefinition City = ADOrgPersonSchema.City;

		// Token: 0x040010F8 RID: 4344
		public static readonly ADPropertyDefinition Manager = ADOrgPersonSchema.Manager;

		// Token: 0x040010F9 RID: 4345
		public static readonly ADPropertyDefinition MobilePhone = ADOrgPersonSchema.MobilePhone;

		// Token: 0x040010FA RID: 4346
		public static readonly ADPropertyDefinition Office = ADOrgPersonSchema.Office;

		// Token: 0x040010FB RID: 4347
		public static readonly ADPropertyDefinition OtherFax = ADOrgPersonSchema.OtherFax;

		// Token: 0x040010FC RID: 4348
		public static readonly ADPropertyDefinition OtherHomePhone = ADOrgPersonSchema.OtherHomePhone;

		// Token: 0x040010FD RID: 4349
		public static readonly ADPropertyDefinition OtherTelephone = ADOrgPersonSchema.OtherTelephone;

		// Token: 0x040010FE RID: 4350
		public static readonly ADPropertyDefinition OtherMobile = ADOrgPersonSchema.OtherMobile;

		// Token: 0x040010FF RID: 4351
		public static readonly ADPropertyDefinition Pager = ADOrgPersonSchema.Pager;

		// Token: 0x04001100 RID: 4352
		public static readonly ADPropertyDefinition Phone = ADOrgPersonSchema.Phone;

		// Token: 0x04001101 RID: 4353
		public static readonly ADPropertyDefinition PostalCode = ADOrgPersonSchema.PostalCode;

		// Token: 0x04001102 RID: 4354
		public static readonly ADPropertyDefinition PostOfficeBox = ADOrgPersonSchema.PostOfficeBox;

		// Token: 0x04001103 RID: 4355
		public static readonly ADPropertyDefinition StateOrProvince = ADOrgPersonSchema.StateOrProvince;

		// Token: 0x04001104 RID: 4356
		public static readonly ADPropertyDefinition StreetAddress = ADOrgPersonSchema.StreetAddress;

		// Token: 0x04001105 RID: 4357
		public static readonly ADPropertyDefinition TelephoneAssistant = ADOrgPersonSchema.TelephoneAssistant;

		// Token: 0x04001106 RID: 4358
		public static readonly ADPropertyDefinition Title = ADOrgPersonSchema.Title;

		// Token: 0x04001107 RID: 4359
		public static readonly ADPropertyDefinition ViewDepth = ADOrgPersonSchema.ViewDepth;

		// Token: 0x04001108 RID: 4360
		public static readonly ADPropertyDefinition RtcSipLine = ADOrgPersonSchema.RtcSipLine;

		// Token: 0x04001109 RID: 4361
		public static readonly ADPropertyDefinition UMCallingLineIds = ADOrgPersonSchema.UMCallingLineIds;

		// Token: 0x0400110A RID: 4362
		public static readonly ADPropertyDefinition VoiceMailSettings = ADOrgPersonSchema.VoiceMailSettings;

		// Token: 0x0400110B RID: 4363
		public static readonly ADPropertyDefinition CountryOrRegion = ADOrgPersonSchema.CountryOrRegion;

		// Token: 0x0400110C RID: 4364
		public static readonly ADPropertyDefinition Languages = ADOrgPersonSchema.Languages;

		// Token: 0x0400110D RID: 4365
		public static readonly ADPropertyDefinition SanitizedPhoneNumbers = ADOrgPersonSchema.SanitizedPhoneNumbers;
	}
}
