using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005E3 RID: 1507
	internal class TransportConfigContainerSchema : ADAMTransportConfigContainerSchema
	{
		// Token: 0x0600461B RID: 17947 RVA: 0x00104456 File Offset: 0x00102656
		internal static object DSNConversionModeGetter(IPropertyBag propertyBag)
		{
			if ((bool)propertyBag[ADAMTransportConfigContainerSchema.ConvertReportToMessage])
			{
				return DSNConversionOption.DoNotConvert;
			}
			if ((bool)propertyBag[ADAMTransportConfigContainerSchema.PreserveReportBodypart])
			{
				return DSNConversionOption.PreserveDSNBody;
			}
			return DSNConversionOption.UseExchangeDSNs;
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x00104490 File Offset: 0x00102690
		internal static void DSNConversionModeSetter(object value, IPropertyBag propertyBag)
		{
			switch ((DSNConversionOption)value)
			{
			case DSNConversionOption.UseExchangeDSNs:
				propertyBag[ADAMTransportConfigContainerSchema.PreserveReportBodypart] = false;
				propertyBag[ADAMTransportConfigContainerSchema.ConvertReportToMessage] = false;
				return;
			case DSNConversionOption.PreserveDSNBody:
				propertyBag[ADAMTransportConfigContainerSchema.PreserveReportBodypart] = true;
				propertyBag[ADAMTransportConfigContainerSchema.ConvertReportToMessage] = false;
				return;
			case DSNConversionOption.DoNotConvert:
				propertyBag[ADAMTransportConfigContainerSchema.PreserveReportBodypart] = false;
				propertyBag[ADAMTransportConfigContainerSchema.ConvertReportToMessage] = true;
				return;
			default:
				throw new ArgumentException("DSNConversionMode", "DSNConversionMode");
			}
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x00104530 File Offset: 0x00102730
		internal static object TransportRuleCollectionAddedRecipientsLimitGetter(IPropertyBag propertyBag)
		{
			return MultivaluedPropertyAccessors.GetIntValueFromMultivaluedProperty(TransportConfigContainerSchema.TransportRuleCollectionAddedRecipientsLimit.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig], (int)TransportConfigContainerSchema.TransportRuleCollectionAddedRecipientsLimit.DefaultValue);
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x00104565 File Offset: 0x00102765
		internal static void TransportRuleCollectionAddedRecipientsLimitSetter(object value, IPropertyBag propertyBag)
		{
			MultivaluedPropertyAccessors.UpdateMultivaluedProperty<int>((int)value, TransportConfigContainerSchema.TransportRuleCollectionAddedRecipientsLimit.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig]);
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x0010458C File Offset: 0x0010278C
		internal static object TransportRuleLimitGetter(IPropertyBag propertyBag)
		{
			return MultivaluedPropertyAccessors.GetIntValueFromMultivaluedProperty(TransportConfigContainerSchema.TransportRuleLimit.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig], (int)TransportConfigContainerSchema.TransportRuleLimit.DefaultValue);
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x001045C1 File Offset: 0x001027C1
		internal static void TransportRuleLimitSetter(object value, IPropertyBag propertyBag)
		{
			MultivaluedPropertyAccessors.UpdateMultivaluedProperty<int>((int)value, TransportConfigContainerSchema.TransportRuleLimit.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig]);
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x001045E8 File Offset: 0x001027E8
		internal static object TransportRuleCollectionRegexCharsLimitGetter(IPropertyBag propertyBag)
		{
			return MultivaluedPropertyAccessors.GetByteQuantifiedValueFromMultivaluedProperty(TransportConfigContainerSchema.TransportRuleCollectionRegexCharsLimit.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig], (ByteQuantifiedSize)TransportConfigContainerSchema.TransportRuleCollectionRegexCharsLimit.DefaultValue);
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x0010461D File Offset: 0x0010281D
		internal static void TransportRuleCollectionRegexCharsLimitSetter(object value, IPropertyBag propertyBag)
		{
			MultivaluedPropertyAccessors.UpdateMultivaluedProperty<ByteQuantifiedSize>((ByteQuantifiedSize)value, TransportConfigContainerSchema.TransportRuleCollectionRegexCharsLimit.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig]);
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x00104644 File Offset: 0x00102844
		internal static object TransportRuleSizeLimitGetter(IPropertyBag propertyBag)
		{
			return MultivaluedPropertyAccessors.GetByteQuantifiedValueFromMultivaluedProperty(TransportConfigContainerSchema.TransportRuleSizeLimit.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig], (ByteQuantifiedSize)TransportConfigContainerSchema.TransportRuleSizeLimit.DefaultValue);
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x00104679 File Offset: 0x00102879
		internal static void TransportRuleSizeLimitSetter(object value, IPropertyBag propertyBag)
		{
			MultivaluedPropertyAccessors.UpdateMultivaluedProperty<ByteQuantifiedSize>((ByteQuantifiedSize)value, TransportConfigContainerSchema.TransportRuleSizeLimit.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig]);
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x001046A0 File Offset: 0x001028A0
		internal static object TransportRuleAttachmentTextScanLimitGetter(IPropertyBag propertyBag)
		{
			return MultivaluedPropertyAccessors.GetByteQuantifiedValueFromMultivaluedProperty(TransportConfigContainerSchema.TransportRuleAttachmentTextScanLimit.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig], (ByteQuantifiedSize)TransportConfigContainerSchema.TransportRuleAttachmentTextScanLimit.DefaultValue);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x001046D5 File Offset: 0x001028D5
		internal static void TransportRuleAttachmentTextScanLimitSetter(object value, IPropertyBag propertyBag)
		{
			MultivaluedPropertyAccessors.UpdateMultivaluedProperty<ByteQuantifiedSize>((ByteQuantifiedSize)value, TransportConfigContainerSchema.TransportRuleAttachmentTextScanLimit.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig]);
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x001046FC File Offset: 0x001028FC
		internal static object TransportRuleRegexValidationTimeoutGetter(IPropertyBag propertyBag)
		{
			return MultivaluedPropertyAccessors.GetTimespanValueFromMultivaluedProperty(TransportConfigContainerSchema.TransportRuleRegexValidationTimeout.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig], (EnhancedTimeSpan)TransportConfigContainerSchema.TransportRuleRegexValidationTimeout.DefaultValue);
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x00104731 File Offset: 0x00102931
		internal static void TransportRuleRegexValidationTimeoutSetter(object value, IPropertyBag propertyBag)
		{
			MultivaluedPropertyAccessors.UpdateMultivaluedProperty<EnhancedTimeSpan>((EnhancedTimeSpan)value, TransportConfigContainerSchema.TransportRuleRegexValidationTimeout.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig]);
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x00104758 File Offset: 0x00102958
		internal static object TransportRuleMinProductVersionGetter(IPropertyBag propertyBag)
		{
			return MultivaluedPropertyAccessors.GetVersionValueFromMultivaluedProperty(TransportConfigContainerSchema.TransportRuleMinProductVersion.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig], (Version)TransportConfigContainerSchema.TransportRuleMinProductVersion.DefaultValue);
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x00104788 File Offset: 0x00102988
		internal static void TransportRuleMinProductVersionSetter(object value, IPropertyBag propertyBag)
		{
			MultivaluedPropertyAccessors.UpdateMultivaluedProperty<Version>((Version)value, TransportConfigContainerSchema.TransportRuleMinProductVersion.Name, (MultiValuedProperty<string>)propertyBag[TransportConfigContainerSchema.TransportRuleConfig]);
		}

		// Token: 0x0400300D RID: 12301
		internal static readonly EnhancedTimeSpan DefaultShadowHeartbeatFrequency = EnhancedTimeSpan.FromMinutes(2.0);

		// Token: 0x0400300E RID: 12302
		internal static readonly EnhancedTimeSpan DefaultShadowResubmitTimeSpan = EnhancedTimeSpan.FromHours(3.0);

		// Token: 0x0400300F RID: 12303
		public static readonly ADPropertyDefinition MaxDumpsterSizePerDatabase = new ADPropertyDefinition("MaxDumpsterSizePerDatabase", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchMaxDumpsterSizePerStorageGroup", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(18UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003010 RID: 12304
		public static readonly ADPropertyDefinition MaxDumpsterTime = new ADPropertyDefinition("MaxDumpsterTime", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchMaxDumpsterTime", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(7.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003011 RID: 12305
		public static readonly ADPropertyDefinition MaxReceiveSize = new ADPropertyDefinition("MaxReceiveSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "delivContLength", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2097151UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003012 RID: 12306
		public static readonly ADPropertyDefinition MaxRecipientEnvelopeLimit = new ADPropertyDefinition("MaxRecipientEnvelopeLimit", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), "msExchRecipLimit", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003013 RID: 12307
		public static readonly ADPropertyDefinition OrganizationFederatedMailbox = new ADPropertyDefinition("OrganizationFederatedMailbox", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress), "msExchOrgFederatedMailbox", ADPropertyDefinitionFlags.None, SmtpAddress.NullReversePath, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003014 RID: 12308
		public static readonly ADPropertyDefinition SupervisionTags = new ADPropertyDefinition("SupervisionTags", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchRelationTags", ADPropertyDefinitionFlags.MultiValued, null, new PropertyDefinitionConstraint[]
		{
			new CharacterConstraint(SupervisionListEntryConstraint.SupervisionTagInvalidChars, false),
			new StringLengthConstraint(1, 20)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003015 RID: 12309
		public static readonly ADPropertyDefinition ShadowHeartbeatFrequency = new ADPropertyDefinition("ShadowHeartbeatFrequency", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchProvisioningFlags", ADPropertyDefinitionFlags.PersistDefaultValue, TransportConfigContainerSchema.DefaultShadowHeartbeatFrequency, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.OneDay),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003016 RID: 12310
		public static readonly ADPropertyDefinition ShadowResubmitTimeSpan = new ADPropertyDefinition("ShadowResubmitTimeSpan", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchELCMailboxFlags", ADPropertyDefinitionFlags.PersistDefaultValue, TransportConfigContainerSchema.DefaultShadowResubmitTimeSpan, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.OneDay),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003017 RID: 12311
		public static readonly ADPropertyDefinition TransportRuleConfig = new ADPropertyDefinition("TransportRuleConfig", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchTransportRuleConfig", ADPropertyDefinitionFlags.MultiValued, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 256)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003018 RID: 12312
		public static readonly ADPropertyDefinition AnonymousSenderToRecipientRatePerHour = new ADPropertyDefinition("AnonymousSenderToRecipientRatePerHour", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchOWASettings", ADPropertyDefinitionFlags.PersistDefaultValue, 1800, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003019 RID: 12313
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x0400301A RID: 12314
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<TransportSettingsConfigXml>(TransportConfigContainerSchema.ConfigurationXMLRaw);

		// Token: 0x0400301B RID: 12315
		public static readonly ADPropertyDefinition QueueDiagnosticsAggregationInterval = XMLSerializableBase.ConfigXmlProperty<TransportSettingsConfigXml, long>("QueueDiagnosticsAggregationInterval", ExchangeObjectVersion.Exchange2007, TransportConfigContainerSchema.ConfigurationXML, TransportSettingsConfigXml.DefaultQueueAggregationIntervalTicks, (TransportSettingsConfigXml configXml) => configXml.QueueAggregationIntervalTicks, delegate(TransportSettingsConfigXml configXml, long value)
		{
			configXml.QueueAggregationIntervalTicks = value;
		}, null, null);

		// Token: 0x0400301C RID: 12316
		public static readonly ADPropertyDefinition DiagnosticsAggregationServicePort = XMLSerializableBase.ConfigXmlProperty<TransportSettingsConfigXml, int>("DiagnosticsAggregationServicePort", ExchangeObjectVersion.Exchange2007, TransportConfigContainerSchema.ConfigurationXML, TransportSettingsConfigXml.DefaultDiagnosticsAggregationServicePort, (TransportSettingsConfigXml configXml) => configXml.DiagnosticsAggregationServicePort, delegate(TransportSettingsConfigXml configXml, int value)
		{
			configXml.DiagnosticsAggregationServicePort = value;
		}, null, null);

		// Token: 0x0400301D RID: 12317
		public static readonly ADPropertyDefinition AgentGeneratedMessageLoopDetectionInSubmissionEnabled = XMLSerializableBase.ConfigXmlProperty<TransportSettingsConfigXml, bool>("AgentGeneratedMessageLoopDetectionInSubmissionEnabled", ExchangeObjectVersion.Exchange2007, TransportConfigContainerSchema.ConfigurationXML, TransportSettingsConfigXml.DefaultAgentGeneratedMessageLoopDetectionInSubmissionEnabled, (TransportSettingsConfigXml configXml) => configXml.AgentGeneratedMessageLoopDetectionInSubmissionEnabled, delegate(TransportSettingsConfigXml configXml, bool value)
		{
			configXml.AgentGeneratedMessageLoopDetectionInSubmissionEnabled = value;
		}, null, null);

		// Token: 0x0400301E RID: 12318
		public static readonly ADPropertyDefinition AgentGeneratedMessageLoopDetectionInSmtpEnabled = XMLSerializableBase.ConfigXmlProperty<TransportSettingsConfigXml, bool>("AgentGeneratedMessageLoopDetectionInSmtpEnabled", ExchangeObjectVersion.Exchange2007, TransportConfigContainerSchema.ConfigurationXML, TransportSettingsConfigXml.DefaultAgentGeneratedMessageLoopDetectionInSmtpEnabled, (TransportSettingsConfigXml configXml) => configXml.AgentGeneratedMessageLoopDetectionInSmtpEnabled, delegate(TransportSettingsConfigXml configXml, bool value)
		{
			configXml.AgentGeneratedMessageLoopDetectionInSmtpEnabled = value;
		}, null, null);

		// Token: 0x0400301F RID: 12319
		public static readonly ADPropertyDefinition MaxAllowedAgentGeneratedMessageDepth = XMLSerializableBase.ConfigXmlProperty<TransportSettingsConfigXml, uint>("MaxAllowedAgentGeneratedMessageDepth", ExchangeObjectVersion.Exchange2007, TransportConfigContainerSchema.ConfigurationXML, TransportSettingsConfigXml.DefaultMaxAllowedAgentGeneratedMessageDepth, (TransportSettingsConfigXml configXml) => configXml.MaxAllowedAgentGeneratedMessageDepth, delegate(TransportSettingsConfigXml configXml, uint value)
		{
			configXml.MaxAllowedAgentGeneratedMessageDepth = value;
		}, null, null);

		// Token: 0x04003020 RID: 12320
		public static readonly ADPropertyDefinition MaxAllowedAgentGeneratedMessageDepthPerAgent = XMLSerializableBase.ConfigXmlProperty<TransportSettingsConfigXml, uint>("MaxAllowedAgentGeneratedMessageDepthPerAgent", ExchangeObjectVersion.Exchange2007, TransportConfigContainerSchema.ConfigurationXML, TransportSettingsConfigXml.DefaultMaxAllowedAgentGeneratedMessageDepthPerAgent, (TransportSettingsConfigXml configXml) => configXml.MaxAllowedAgentGeneratedMessageDepthPerAgent, delegate(TransportSettingsConfigXml configXml, uint value)
		{
			configXml.MaxAllowedAgentGeneratedMessageDepthPerAgent = value;
		}, null, null);

		// Token: 0x04003021 RID: 12321
		public static readonly ADPropertyDefinition TransportRuleCollectionAddedRecipientsLimit = new ADPropertyDefinition("TransportRuleCollectionAddedRecipientsLimit", ExchangeObjectVersion.Exchange2007, typeof(int), null, ADPropertyDefinitionFlags.Calculated, 100, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, new GetterDelegate(TransportConfigContainerSchema.TransportRuleCollectionAddedRecipientsLimitGetter), new SetterDelegate(TransportConfigContainerSchema.TransportRuleCollectionAddedRecipientsLimitSetter), null, null);

		// Token: 0x04003022 RID: 12322
		public static readonly ADPropertyDefinition TransportRuleLimit = new ADPropertyDefinition("TransportRuleLimit", ExchangeObjectVersion.Exchange2007, typeof(int), null, ADPropertyDefinitionFlags.Calculated, 300, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, new GetterDelegate(TransportConfigContainerSchema.TransportRuleLimitGetter), new SetterDelegate(TransportConfigContainerSchema.TransportRuleLimitSetter), null, null);

		// Token: 0x04003023 RID: 12323
		public static readonly ADPropertyDefinition TransportRuleCollectionRegexCharsLimit = new ADPropertyDefinition("TransportRuleCollectionRegexCharsLimit", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), null, ADPropertyDefinitionFlags.Calculated, ByteQuantifiedSize.FromKB(20UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, new GetterDelegate(TransportConfigContainerSchema.TransportRuleCollectionRegexCharsLimitGetter), new SetterDelegate(TransportConfigContainerSchema.TransportRuleCollectionRegexCharsLimitSetter), null, null);

		// Token: 0x04003024 RID: 12324
		public static readonly ADPropertyDefinition TransportRuleSizeLimit = new ADPropertyDefinition("TransportRuleSizeLimit", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), null, ADPropertyDefinitionFlags.Calculated, ByteQuantifiedSize.FromKB(4UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, new GetterDelegate(TransportConfigContainerSchema.TransportRuleSizeLimitGetter), new SetterDelegate(TransportConfigContainerSchema.TransportRuleSizeLimitSetter), null, null);

		// Token: 0x04003025 RID: 12325
		public static readonly ADPropertyDefinition TransportRuleAttachmentTextScanLimit = new ADPropertyDefinition("TransportRuleAttachmentTextScanLimit", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), null, ADPropertyDefinitionFlags.Calculated, ByteQuantifiedSize.FromKB(150UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, new GetterDelegate(TransportConfigContainerSchema.TransportRuleAttachmentTextScanLimitGetter), new SetterDelegate(TransportConfigContainerSchema.TransportRuleAttachmentTextScanLimitSetter), null, null);

		// Token: 0x04003026 RID: 12326
		public static readonly ADPropertyDefinition TransportRuleRegexValidationTimeout = new ADPropertyDefinition("TransportRuleRegexValidationTimeout", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), null, ADPropertyDefinitionFlags.Calculated, EnhancedTimeSpan.FromMilliseconds(300.0), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, new GetterDelegate(TransportConfigContainerSchema.TransportRuleRegexValidationTimeoutGetter), new SetterDelegate(TransportConfigContainerSchema.TransportRuleRegexValidationTimeoutSetter), null, null);

		// Token: 0x04003027 RID: 12327
		public static readonly ADPropertyDefinition TransportRuleMinProductVersion = new ADPropertyDefinition("TransportRuleMinProductVersion", ExchangeObjectVersion.Exchange2007, typeof(Version), null, ADPropertyDefinitionFlags.Calculated, new Version("14.00.0000.000"), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, new GetterDelegate(TransportConfigContainerSchema.TransportRuleMinProductVersionGetter), new SetterDelegate(TransportConfigContainerSchema.TransportRuleMinProductVersionSetter), null, null);
	}
}
