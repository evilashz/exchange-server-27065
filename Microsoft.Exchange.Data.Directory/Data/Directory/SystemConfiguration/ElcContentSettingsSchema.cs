using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000418 RID: 1048
	internal sealed class ElcContentSettingsSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001FC9 RID: 8137
		internal static readonly ADPropertyDefinition MessageClassString = new ADPropertyDefinition("MessageClassString", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchELCMessageClass", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001FCA RID: 8138
		internal static readonly ADPropertyDefinition MessageClassArray = new ADPropertyDefinition("MessageClassArray", ExchangeObjectVersion.Exchange2007, typeof(string), "ExtensionName", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001FCB RID: 8139
		public static readonly ADPropertyDefinition MessageClass = new ADPropertyDefinition("MessageClass", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1023)
		}, new ProviderPropertyDefinition[]
		{
			ElcContentSettingsSchema.MessageClassString,
			ElcContentSettingsSchema.MessageClassArray
		}, null, new GetterDelegate(ElcContentSettings.ELCMessageClassGetter), new SetterDelegate(ElcContentSettings.ELCMessageClassSetter), null, null);

		// Token: 0x04001FCC RID: 8140
		public static readonly ADPropertyDefinition ElcFlags = new ADPropertyDefinition("ELCFlags", ExchangeObjectVersion.Exchange2007, typeof(ElcContentSettingFlags), "msExchELCFlags", ADPropertyDefinitionFlags.None, ElcContentSettingFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001FCD RID: 8141
		public static readonly ADPropertyDefinition RetentionAction = new ADPropertyDefinition("RetentionAction", ExchangeObjectVersion.Exchange2007, typeof(RetentionActionType), "msExchELCExpiryAction", ADPropertyDefinitionFlags.None, RetentionActionType.MoveToDeletedItems, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(RetentionActionType))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001FCE RID: 8142
		public static readonly ADPropertyDefinition AgeLimitForRetention = new ADPropertyDefinition("AgeLimitForRetention", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), "msExchELCExpiryAgeLimit", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromSeconds(86400.0), EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new NullableEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001FCF RID: 8143
		public static readonly ADPropertyDefinition MoveToDestinationFolder = new ADPropertyDefinition("MoveToDestinationFolder", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchELCExpiryDestinationLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001FD0 RID: 8144
		public static readonly ADPropertyDefinition AddressForJournaling = new ADPropertyDefinition("AddressForJournaling", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchELCAutoCopyAddressLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001FD1 RID: 8145
		public static readonly ADPropertyDefinition LabelForJournaling = new ADPropertyDefinition("LabelForJournaling", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchELCLabel", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 255)
		}, null, null);

		// Token: 0x04001FD2 RID: 8146
		public static readonly ADPropertyDefinition RetentionEnabled = new ADPropertyDefinition("RetentionEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ElcContentSettingsSchema.ElcFlags
		}, null, (IPropertyBag propertyBag) => ElcContentSettings.GetValueFromFlags(propertyBag, ElcContentSettingFlags.RetentionEnabled), delegate(object value, IPropertyBag propertyBag)
		{
			ElcContentSettings.SetFlags(propertyBag, ElcContentSettingFlags.RetentionEnabled, (bool)value);
		}, null, null);

		// Token: 0x04001FD3 RID: 8147
		public static readonly ADPropertyDefinition TriggerForRetention = new ADPropertyDefinition("TriggerForRetention", ExchangeObjectVersion.Exchange2007, typeof(RetentionDateType), null, ADPropertyDefinitionFlags.Calculated, RetentionDateType.WhenDelivered, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ElcContentSettingsSchema.ElcFlags
		}, null, delegate(IPropertyBag propertyBag)
		{
			bool valueFromFlags = ElcContentSettings.GetValueFromFlags(propertyBag, ElcContentSettingFlags.MoveDateBasedRetention);
			if (valueFromFlags)
			{
				return RetentionDateType.WhenMoved;
			}
			return RetentionDateType.WhenDelivered;
		}, delegate(object value, IPropertyBag propertyBag)
		{
			RetentionDateType retentionDateType = (RetentionDateType)value;
			if (retentionDateType == RetentionDateType.WhenMoved)
			{
				ElcContentSettings.SetFlags(propertyBag, ElcContentSettingFlags.MoveDateBasedRetention, true);
				return;
			}
			ElcContentSettings.SetFlags(propertyBag, ElcContentSettingFlags.MoveDateBasedRetention, false);
		}, null, null);

		// Token: 0x04001FD4 RID: 8148
		public static readonly ADPropertyDefinition JournalingEnabled = new ADPropertyDefinition("JournalingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ElcContentSettingsSchema.ElcFlags
		}, null, (IPropertyBag propertyBag) => ElcContentSettings.GetValueFromFlags(propertyBag, ElcContentSettingFlags.JournalingEnabled), delegate(object value, IPropertyBag propertyBag)
		{
			ElcContentSettings.SetFlags(propertyBag, ElcContentSettingFlags.JournalingEnabled, (bool)value);
		}, null, null);

		// Token: 0x04001FD5 RID: 8149
		public static readonly ADPropertyDefinition MessageClassDisplayName = new ADPropertyDefinition("MessageClassDisplayName", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ElcContentSettingsSchema.MessageClassString,
			ElcContentSettingsSchema.MessageClassArray
		}, null, new GetterDelegate(ElcContentSettings.MessageClassDisplayNameGetter), null, null, null);

		// Token: 0x04001FD6 RID: 8150
		public static readonly ADPropertyDefinition ManagedFolder = new ADPropertyDefinition("ManagedFolder", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(ElcContentSettings.ELCFolderGetter), null, null, null);

		// Token: 0x04001FD7 RID: 8151
		public static readonly ADPropertyDefinition ManagedFolderName = new ADPropertyDefinition("ManagedFolderName", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(ElcContentSettings.ELCFolderNameGetter), null, null, null);

		// Token: 0x04001FD8 RID: 8152
		public static readonly ADPropertyDefinition MessageFormatForJournaling = new ADPropertyDefinition("MessageFormatForJournaling", ExchangeObjectVersion.Exchange2007, typeof(JournalingFormat), null, ADPropertyDefinitionFlags.Calculated, JournalingFormat.UseTnef, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ElcContentSettingsSchema.ElcFlags
		}, null, delegate(IPropertyBag propertyBag)
		{
			bool valueFromFlags = ElcContentSettings.GetValueFromFlags(propertyBag, ElcContentSettingFlags.JournalAsMSG);
			if (valueFromFlags)
			{
				return JournalingFormat.UseMsg;
			}
			return JournalingFormat.UseTnef;
		}, delegate(object value, IPropertyBag propertyBag)
		{
			if ((JournalingFormat)value == JournalingFormat.UseMsg)
			{
				ElcContentSettings.SetFlags(propertyBag, ElcContentSettingFlags.JournalAsMSG, true);
				return;
			}
			ElcContentSettings.SetFlags(propertyBag, ElcContentSettingFlags.JournalAsMSG, false);
		}, null, null);
	}
}
