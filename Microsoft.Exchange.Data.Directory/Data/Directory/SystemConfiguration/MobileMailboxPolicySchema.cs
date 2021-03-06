using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000318 RID: 792
	internal class MobileMailboxPolicySchema : MailboxPolicySchema
	{
		// Token: 0x06002460 RID: 9312 RVA: 0x0009B940 File Offset: 0x00099B40
		private static GetterDelegate GetMobileFlagsGetterDelegate(MobileFlagsDefs flag)
		{
			return (IPropertyBag propertyBag) => ((MobileFlagsDefs)propertyBag[MobileMailboxPolicySchema.MobileFlags] & flag) == flag;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x0009B9D8 File Offset: 0x00099BD8
		private static SetterDelegate GetMobileFlagsSetterDelegate(MobileFlagsDefs flag)
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				if ((bool)value)
				{
					propertyBag[MobileMailboxPolicySchema.MobileFlags] = ((MobileFlagsDefs)propertyBag[MobileMailboxPolicySchema.MobileFlags] | flag);
					return;
				}
				propertyBag[MobileMailboxPolicySchema.MobileFlags] = ((MobileFlagsDefs)propertyBag[MobileMailboxPolicySchema.MobileFlags] & ~flag);
			};
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x0009BA2C File Offset: 0x00099C2C
		private static GetterDelegate GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs flag)
		{
			return (IPropertyBag propertyBag) => ((MobileAdditionalFlagsDefs)propertyBag[MobileMailboxPolicySchema.MobileAdditionalFlags] & flag) == flag;
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0009BAC4 File Offset: 0x00099CC4
		private static SetterDelegate GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs flag)
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				if ((bool)value)
				{
					propertyBag[MobileMailboxPolicySchema.MobileAdditionalFlags] = ((MobileAdditionalFlagsDefs)propertyBag[MobileMailboxPolicySchema.MobileAdditionalFlags] | flag);
					return;
				}
				propertyBag[MobileMailboxPolicySchema.MobileAdditionalFlags] = ((MobileAdditionalFlagsDefs)propertyBag[MobileMailboxPolicySchema.MobileAdditionalFlags] & ~flag);
			};
		}

		// Token: 0x04001682 RID: 5762
		public static readonly ADPropertyDefinition AssociatedUsers = new ADPropertyDefinition("AssociatedUsers", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchMobilePolicyBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001683 RID: 5763
		public static readonly ADPropertyDefinition DevicePolicyRefreshInterval = new ADPropertyDefinition("DevicePolicyRefreshInterval", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<EnhancedTimeSpan>), "msExchMobileDevicePolicyRefreshInterval", ADPropertyDefinitionFlags.None, Unlimited<EnhancedTimeSpan>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new UnlimitedEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001684 RID: 5764
		public static readonly ADPropertyDefinition MaxAttachmentSize = new ADPropertyDefinition("MaxAttachmentSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchMobileInitialMaxAttachmentSize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2097151UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001685 RID: 5765
		public static readonly ADPropertyDefinition MaxPasswordFailedAttempts = new ADPropertyDefinition("MaxPasswordFailedAttempts", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), "msExchMobileMaxDevicePasswordFailedAttempts", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(4, 16)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(4, 16)
		}, null, null);

		// Token: 0x04001686 RID: 5766
		public static readonly ADPropertyDefinition MaxInactivityTimeLock = new ADPropertyDefinition("MaxInactivityTimeLock", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<EnhancedTimeSpan>), "msExchMobileMaxInactivityTimeDeviceLock", ADPropertyDefinitionFlags.None, Unlimited<EnhancedTimeSpan>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneMinute, EnhancedTimeSpan.OneHour),
			new UnlimitedEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneMinute, EnhancedTimeSpan.OneHour),
			new UnlimitedEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, null, null);

		// Token: 0x04001687 RID: 5767
		public static readonly ADPropertyDefinition PasswordExpiration = new ADPropertyDefinition("PasswordExpiration", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<EnhancedTimeSpan>), "msExchMobileDevicePasswordExpiration", ADPropertyDefinitionFlags.None, Unlimited<EnhancedTimeSpan>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneDay, EnhancedTimeSpan.FromDays(730.0)),
			new UnlimitedEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneDay, EnhancedTimeSpan.FromDays(730.0)),
			new UnlimitedEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, null, null);

		// Token: 0x04001688 RID: 5768
		public static readonly ADPropertyDefinition PasswordHistory = new ADPropertyDefinition("PasswordHistory", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMobileDeviceNumberOfPreviousPasswordsDisallowed", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 50)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 50)
		}, null, null);

		// Token: 0x04001689 RID: 5769
		public static readonly ADPropertyDefinition MinPasswordLength = new ADPropertyDefinition("MinPasswordLength", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchMobileMinDevicePasswordLength", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(1, 16)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(1, 16)
		}, null, null);

		// Token: 0x0400168A RID: 5770
		public static readonly ADPropertyDefinition MobileFlags = new ADPropertyDefinition("MobileFlags", ExchangeObjectVersion.Exchange2007, typeof(MobileFlagsDefs), "msExchMobileFlags", ADPropertyDefinitionFlags.None, MobileFlagsDefs.AttachmentsEnabled | MobileFlagsDefs.AllowSimplePassword | MobileFlagsDefs.WSSAccessEnabled | MobileFlagsDefs.UNCAccessEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400168B RID: 5771
		public static readonly ADPropertyDefinition MobileAdditionalFlags = new ADPropertyDefinition("MobileAdditionalFlags", ExchangeObjectVersion.Exchange2007, typeof(MobileAdditionalFlagsDefs), "msExchMobileAdditionalFlags", ADPropertyDefinitionFlags.None, MobileAdditionalFlagsDefs.AllowStorageCard | MobileAdditionalFlagsDefs.AllowCamera | MobileAdditionalFlagsDefs.AllowUnsignedApplications | MobileAdditionalFlagsDefs.AllowUnsignedInstallationPackages | MobileAdditionalFlagsDefs.AllowWiFi | MobileAdditionalFlagsDefs.AllowTextMessaging | MobileAdditionalFlagsDefs.AllowPOPIMAPEmail | MobileAdditionalFlagsDefs.AllowIrDA | MobileAdditionalFlagsDefs.AllowDesktopSync | MobileAdditionalFlagsDefs.AllowHTMLEmail | MobileAdditionalFlagsDefs.AllowSMIMESoftCerts | MobileAdditionalFlagsDefs.AllowBrowser | MobileAdditionalFlagsDefs.AllowConsumerEmail | MobileAdditionalFlagsDefs.AllowRemoteDesktop | MobileAdditionalFlagsDefs.AllowInternetSharing | MobileAdditionalFlagsDefs.AllowMobileOTAUpdate | MobileAdditionalFlagsDefs.IrmEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400168C RID: 5772
		public static readonly ADPropertyDefinition AllowBluetooth = new ADPropertyDefinition("AllowBluetooth", ExchangeObjectVersion.Exchange2007, typeof(BluetoothType), "msExchMobileAllowBluetooth", ADPropertyDefinitionFlags.None, BluetoothType.Allow, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400168D RID: 5773
		public static readonly ADPropertyDefinition MaxCalendarAgeFilter = new ADPropertyDefinition("MaxCalendarAgeFilter", ExchangeObjectVersion.Exchange2007, typeof(CalendarAgeFilterType), "msExchMobileMaxCalendarAgeFilter", ADPropertyDefinitionFlags.None, CalendarAgeFilterType.All, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400168E RID: 5774
		public static readonly ADPropertyDefinition MaxEmailAgeFilter = new ADPropertyDefinition("MaxEmailAgeFilter", ExchangeObjectVersion.Exchange2007, typeof(EmailAgeFilterType), "msExchMobileMaxEmailAgeFilter", ADPropertyDefinitionFlags.None, EmailAgeFilterType.All, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400168F RID: 5775
		public static readonly ADPropertyDefinition RequireSignedSMIMEAlgorithm = new ADPropertyDefinition("RequireSignedSMIMEAlgorithm", ExchangeObjectVersion.Exchange2007, typeof(SignedSMIMEAlgorithmType), "msExchMobileRequireSignedSMIMEAlgorithm", ADPropertyDefinitionFlags.None, SignedSMIMEAlgorithmType.SHA1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001690 RID: 5776
		public static readonly ADPropertyDefinition RequireEncryptionSMIMEAlgorithm = new ADPropertyDefinition("RequireEncryptionSMIMEAlgorithm", ExchangeObjectVersion.Exchange2007, typeof(EncryptionSMIMEAlgorithmType), "msExchMobileRequireEncryptionSMIMEAlgorithm", ADPropertyDefinitionFlags.None, EncryptionSMIMEAlgorithmType.TripleDES, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001691 RID: 5777
		public static readonly ADPropertyDefinition AllowSMIMEEncryptionAlgorithmNegotiation = new ADPropertyDefinition("AllowSMIMEEncryptionAlgorithmNegotiation", ExchangeObjectVersion.Exchange2007, typeof(SMIMEEncryptionAlgorithmNegotiationType), "msExchMobileAllowSMIMEEncryptionAlgorithmNegotiation", ADPropertyDefinitionFlags.None, SMIMEEncryptionAlgorithmNegotiationType.AllowAnyAlgorithmNegotiation, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001692 RID: 5778
		public static readonly ADPropertyDefinition MinPasswordComplexCharacters = new ADPropertyDefinition("MinPasswordComplexCharacters", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMobileMinDevicePasswordComplexCharacters", ADPropertyDefinitionFlags.PersistDefaultValue, 1, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 4)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 4)
		}, null, null);

		// Token: 0x04001693 RID: 5779
		public static readonly ADPropertyDefinition ADMaxEmailBodyTruncationSize = new ADPropertyDefinition("ADMaxEmailBodyTruncationSize", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMobileMaxEmailBodyTruncationSize", ADPropertyDefinitionFlags.PersistDefaultValue, -1, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(-1, int.MaxValue)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(-1, int.MaxValue)
		}, null, null);

		// Token: 0x04001694 RID: 5780
		public static readonly ADPropertyDefinition ADMaxEmailHTMLBodyTruncationSize = new ADPropertyDefinition("ADMaxEmailHTMLBodyTruncationSize", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMobileMaxEmailHTMLBodyTruncationSize", ADPropertyDefinitionFlags.PersistDefaultValue, -1, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(-1, int.MaxValue)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(-1, int.MaxValue)
		}, null, null);

		// Token: 0x04001695 RID: 5781
		public static readonly ADPropertyDefinition UnapprovedInROMApplicationList = new ADPropertyDefinition("UnapprovedInROMApplicationList", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMobileUnapprovedInROMApplicationList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint()
		}, null, null);

		// Token: 0x04001696 RID: 5782
		public static readonly ADPropertyDefinition ADApprovedApplicationList = new ADPropertyDefinition("ADApprovedApplicationList", ExchangeObjectVersion.Exchange2007, typeof(ApprovedApplication), "msExchMobileApprovedApplicationList", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001697 RID: 5783
		public static readonly ADPropertyDefinition MobileOTAUpdateMode = new ADPropertyDefinition("MobileOTAUpdateMode", ExchangeObjectVersion.Exchange2007, typeof(MobileOTAUpdateModeType), "msExchMobileOTAUpdateMode", ADPropertyDefinitionFlags.None, MobileOTAUpdateModeType.MinorVersionUpdates, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001698 RID: 5784
		public static readonly ADPropertyDefinition AllowNonProvisionableDevices = new ADPropertyDefinition("AllowNonProvisionableDevices", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.AllowNonProvisionableDevices), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.AllowNonProvisionableDevices), null, null);

		// Token: 0x04001699 RID: 5785
		public static readonly ADPropertyDefinition AlphanumericPasswordRequired = new ADPropertyDefinition("AlphanumericPasswordRequired", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.AlphanumericPasswordRequired), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.AlphanumericPasswordRequired), null, null);

		// Token: 0x0400169A RID: 5786
		public static readonly ADPropertyDefinition AttachmentsEnabled = new ADPropertyDefinition("AttachmentsEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.AttachmentsEnabled), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.AttachmentsEnabled), null, null);

		// Token: 0x0400169B RID: 5787
		public static readonly ADPropertyDefinition RequireStorageCardEncryption = new ADPropertyDefinition("RequireStorageCardEncryption", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.RequireStorageCardEncryption), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.RequireStorageCardEncryption), null, null);

		// Token: 0x0400169C RID: 5788
		public static readonly ADPropertyDefinition PasswordEnabled = new ADPropertyDefinition("PasswordEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.PasswordEnabled), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.PasswordEnabled), null, null);

		// Token: 0x0400169D RID: 5789
		public static readonly ADPropertyDefinition PasswordRecoveryEnabled = new ADPropertyDefinition("PasswordRecoveryEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.PasswordRecoveryEnabled), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.PasswordRecoveryEnabled), null, null);

		// Token: 0x0400169E RID: 5790
		public static readonly ADPropertyDefinition AllowSimplePassword = new ADPropertyDefinition("AllowSimplePassword", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.AllowSimplePassword), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.AllowSimplePassword), null, null);

		// Token: 0x0400169F RID: 5791
		public static readonly ADPropertyDefinition WSSAccessEnabled = new ADPropertyDefinition("WSSAccessEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.WSSAccessEnabled), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.WSSAccessEnabled), null, null);

		// Token: 0x040016A0 RID: 5792
		public static readonly ADPropertyDefinition UNCAccessEnabled = new ADPropertyDefinition("UNCAccessEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.UNCAccessEnabled), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.UNCAccessEnabled), null, null);

		// Token: 0x040016A1 RID: 5793
		public static readonly ADPropertyDefinition IsDefault = new ADPropertyDefinition("IsDefault", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.IsDefault), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.IsDefault), null, null);

		// Token: 0x040016A2 RID: 5794
		public static readonly ADPropertyDefinition DenyApplePushNotifications = new ADPropertyDefinition("DenyApplePushNotifications", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.DenyApplePushNotifications), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.DenyApplePushNotifications), null, null);

		// Token: 0x040016A3 RID: 5795
		public static readonly ADPropertyDefinition DenyMicrosoftPushNotifications = new ADPropertyDefinition("DenyMicrosoftPushNotifications", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.DenyMicrosoftPushNotifications), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.DenyMicrosoftPushNotifications), null, null);

		// Token: 0x040016A4 RID: 5796
		public static readonly ADPropertyDefinition DenyGooglePushNotifications = new ADPropertyDefinition("DenyGooglePushNotifications", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileFlags
		}, null, MobileMailboxPolicySchema.GetMobileFlagsGetterDelegate(MobileFlagsDefs.DenyGooglePushNotifications), MobileMailboxPolicySchema.GetMobileFlagsSetterDelegate(MobileFlagsDefs.DenyGooglePushNotifications), null, null);

		// Token: 0x040016A5 RID: 5797
		public static readonly ADPropertyDefinition AllowStorageCard = new ADPropertyDefinition("AllowStorageCard", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowStorageCard), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowStorageCard), null, null);

		// Token: 0x040016A6 RID: 5798
		public static readonly ADPropertyDefinition AllowCamera = new ADPropertyDefinition("AllowCamera", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowCamera), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowCamera), null, null);

		// Token: 0x040016A7 RID: 5799
		public static readonly ADPropertyDefinition RequireDeviceEncryption = new ADPropertyDefinition("RequireDeviceEncryption", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.RequireDeviceEncryption), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.RequireDeviceEncryption), null, null);

		// Token: 0x040016A8 RID: 5800
		public static readonly ADPropertyDefinition AllowUnsignedApplications = new ADPropertyDefinition("AllowUnsignedApplications", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowUnsignedApplications), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowUnsignedApplications), null, null);

		// Token: 0x040016A9 RID: 5801
		public static readonly ADPropertyDefinition AllowUnsignedInstallationPackages = new ADPropertyDefinition("AllowUnsignedInstallationPackages", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowUnsignedInstallationPackages), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowUnsignedInstallationPackages), null, null);

		// Token: 0x040016AA RID: 5802
		public static readonly ADPropertyDefinition AllowWiFi = new ADPropertyDefinition("AllowWiFi", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowWiFi), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowWiFi), null, null);

		// Token: 0x040016AB RID: 5803
		public static readonly ADPropertyDefinition AllowTextMessaging = new ADPropertyDefinition("AllowTextMessaging", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowTextMessaging), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowTextMessaging), null, null);

		// Token: 0x040016AC RID: 5804
		public static readonly ADPropertyDefinition AllowPOPIMAPEmail = new ADPropertyDefinition("AllowPOPIMAPEmail", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowPOPIMAPEmail), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowPOPIMAPEmail), null, null);

		// Token: 0x040016AD RID: 5805
		public static readonly ADPropertyDefinition AllowIrDA = new ADPropertyDefinition("AllowIrDA", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowIrDA), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowIrDA), null, null);

		// Token: 0x040016AE RID: 5806
		public static readonly ADPropertyDefinition RequireManualSyncWhenRoaming = new ADPropertyDefinition("RequireManualSyncWhenRoaming", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.RequireManualSyncWhenRoaming), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.RequireManualSyncWhenRoaming), null, null);

		// Token: 0x040016AF RID: 5807
		public static readonly ADPropertyDefinition AllowDesktopSync = new ADPropertyDefinition("AllowDesktopSync", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowDesktopSync), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowDesktopSync), null, null);

		// Token: 0x040016B0 RID: 5808
		public static readonly ADPropertyDefinition AllowHTMLEmail = new ADPropertyDefinition("AllowHTMLEmail", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowHTMLEmail), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowHTMLEmail), null, null);

		// Token: 0x040016B1 RID: 5809
		public static readonly ADPropertyDefinition RequireSignedSMIMEMessages = new ADPropertyDefinition("RequireSignedSMIMEMessages", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.RequireSignedSMIMEMessages), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.RequireSignedSMIMEMessages), null, null);

		// Token: 0x040016B2 RID: 5810
		public static readonly ADPropertyDefinition RequireEncryptedSMIMEMessages = new ADPropertyDefinition("RequireEncryptedSMIMEMessages", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.RequireEncryptedSMIMEMessages), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.RequireEncryptedSMIMEMessages), null, null);

		// Token: 0x040016B3 RID: 5811
		public static readonly ADPropertyDefinition AllowSMIMESoftCerts = new ADPropertyDefinition("AllowSMIMESoftCerts", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowSMIMESoftCerts), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowSMIMESoftCerts), null, null);

		// Token: 0x040016B4 RID: 5812
		public static readonly ADPropertyDefinition AllowBrowser = new ADPropertyDefinition("AllowBrowser", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowBrowser), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowBrowser), null, null);

		// Token: 0x040016B5 RID: 5813
		public static readonly ADPropertyDefinition AllowConsumerEmail = new ADPropertyDefinition("AllowConsumerEmail", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowConsumerEmail), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowConsumerEmail), null, null);

		// Token: 0x040016B6 RID: 5814
		public static readonly ADPropertyDefinition AllowRemoteDesktop = new ADPropertyDefinition("AllowRemoteDesktop", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowRemoteDesktop), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowRemoteDesktop), null, null);

		// Token: 0x040016B7 RID: 5815
		public static readonly ADPropertyDefinition AllowInternetSharing = new ADPropertyDefinition("AllowInternetSharing", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowInternetSharing), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowInternetSharing), null, null);

		// Token: 0x040016B8 RID: 5816
		public static readonly ADPropertyDefinition MaxEmailBodyTruncationSize = new ADPropertyDefinition("MaxEmailBodyTruncationSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), null, ADPropertyDefinitionFlags.Calculated, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.ADMaxEmailBodyTruncationSize
		}, null, delegate(IPropertyBag propertyBag)
		{
			Unlimited<int> unlimitedValue;
			if ((int)propertyBag[MobileMailboxPolicySchema.ADMaxEmailBodyTruncationSize] == -1)
			{
				unlimitedValue = Unlimited<int>.UnlimitedValue;
			}
			else
			{
				unlimitedValue = new Unlimited<int>((int)propertyBag[MobileMailboxPolicySchema.ADMaxEmailBodyTruncationSize]);
			}
			return unlimitedValue;
		}, delegate(object value, IPropertyBag propertyBag)
		{
			if (((Unlimited<int>)value).IsUnlimited)
			{
				propertyBag[MobileMailboxPolicySchema.ADMaxEmailBodyTruncationSize] = -1;
				return;
			}
			propertyBag[MobileMailboxPolicySchema.ADMaxEmailBodyTruncationSize] = ((Unlimited<int>)value).Value;
		}, null, null);

		// Token: 0x040016B9 RID: 5817
		public static readonly ADPropertyDefinition MaxEmailHTMLBodyTruncationSize = new ADPropertyDefinition("MaxEmailHTMLBodyTruncationSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), null, ADPropertyDefinitionFlags.Calculated, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.ADMaxEmailHTMLBodyTruncationSize
		}, null, delegate(IPropertyBag propertyBag)
		{
			Unlimited<int> unlimitedValue;
			if ((int)propertyBag[MobileMailboxPolicySchema.ADMaxEmailHTMLBodyTruncationSize] == -1)
			{
				unlimitedValue = Unlimited<int>.UnlimitedValue;
			}
			else
			{
				unlimitedValue = new Unlimited<int>((int)propertyBag[MobileMailboxPolicySchema.ADMaxEmailHTMLBodyTruncationSize]);
			}
			return unlimitedValue;
		}, delegate(object value, IPropertyBag propertyBag)
		{
			if (((Unlimited<int>)value).IsUnlimited)
			{
				propertyBag[MobileMailboxPolicySchema.ADMaxEmailHTMLBodyTruncationSize] = -1;
				return;
			}
			propertyBag[MobileMailboxPolicySchema.ADMaxEmailHTMLBodyTruncationSize] = ((Unlimited<int>)value).Value;
		}, null, null);

		// Token: 0x040016BA RID: 5818
		public static readonly ADPropertyDefinition AllowExternalDeviceManagement = new ADPropertyDefinition("ExternallyDeviceManaged", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowExternalDeviceManagement), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowExternalDeviceManagement), null, null);

		// Token: 0x040016BB RID: 5819
		public static readonly ADPropertyDefinition AllowMobileOTAUpdate = new ADPropertyDefinition("AllowMobileOTAUpdate", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.AllowMobileOTAUpdate), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.AllowMobileOTAUpdate), null, null);

		// Token: 0x040016BC RID: 5820
		public static readonly ADPropertyDefinition IrmEnabled = new ADPropertyDefinition("IrmEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileMailboxPolicySchema.MobileAdditionalFlags
		}, null, MobileMailboxPolicySchema.GetMobileAdditionalFlagsGetterDelegate(MobileAdditionalFlagsDefs.IrmEnabled), MobileMailboxPolicySchema.GetMobileAdditionalFlagsSetterDelegate(MobileAdditionalFlagsDefs.IrmEnabled), null, null);
	}
}
