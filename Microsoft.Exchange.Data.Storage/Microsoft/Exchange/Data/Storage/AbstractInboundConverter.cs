using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200058E RID: 1422
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class AbstractInboundConverter
	{
		// Token: 0x06003A64 RID: 14948 RVA: 0x000F03A8 File Offset: 0x000EE5A8
		protected AbstractInboundConverter(InboundMessageWriter writer, AbstractInboundConverter.IPromotionRule defaultPromotionRule)
		{
			this.messageWriter = writer;
			this.defaultPromotionRule = defaultPromotionRule;
		}

		// Token: 0x06003A65 RID: 14949
		protected abstract bool IsLargeValue();

		// Token: 0x06003A66 RID: 14950
		protected abstract object ReadValue();

		// Token: 0x06003A67 RID: 14951
		protected abstract Stream OpenValueReadStream(out int skipTrailingNulls);

		// Token: 0x06003A68 RID: 14952
		protected abstract void PromoteAttachDataObject();

		// Token: 0x06003A69 RID: 14953
		protected abstract void PromoteBodyProperty(StorePropertyDefinition property);

		// Token: 0x06003A6A RID: 14954
		protected abstract bool CanPromoteMimeOnlyProperties();

		// Token: 0x06003A6B RID: 14955 RVA: 0x000F03C9 File Offset: 0x000EE5C9
		protected virtual void PromoteInternetCpidProperty()
		{
			this.PromoteProperty(InternalSchema.InternetCpid, false);
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x000F03D7 File Offset: 0x000EE5D7
		protected virtual void PromoteMessageClass()
		{
			this.PromoteProperty(InternalSchema.ItemClass, false);
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000F03E8 File Offset: 0x000EE5E8
		protected AbstractInboundConverter.IPromotionRule GetPropertyPromotionRule(NativeStorePropertyDefinition property)
		{
			AbstractInboundConverter.IPromotionRule result = null;
			if (this.rules.TryGetValue(property, out result))
			{
				return result;
			}
			return this.defaultPromotionRule;
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000F0410 File Offset: 0x000EE610
		protected NativeStorePropertyDefinition CreatePropertyDefinition(TnefPropertyTag propertyTag, TnefNameId? namedProperty)
		{
			PropType tnefType = (PropType)propertyTag.TnefType;
			if (tnefType == PropType.Error || tnefType == PropType.Null || tnefType == PropType.ObjectArray || tnefType == PropType.Unspecified)
			{
				return null;
			}
			if (tnefType == PropType.Object && propertyTag != TnefPropertyTag.AttachDataObj)
			{
				return null;
			}
			if (namedProperty == null)
			{
				return PropertyTagPropertyDefinition.InternalCreateCustom(string.Empty, (PropTag)propertyTag, PropertyFlags.None, NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType);
			}
			Guid propertySetGuid = namedProperty.Value.PropertySetGuid;
			if (namedProperty.Value.Kind == TnefNameIdKind.Id)
			{
				return GuidIdPropertyDefinition.InternalCreateCustom(string.Empty, tnefType, propertySetGuid, namedProperty.Value.Id, PropertyFlags.None, NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType, new PropertyDefinitionConstraint[0]);
			}
			string name = namedProperty.Value.Name;
			if (!GuidNamePropertyDefinition.IsValidName(propertySetGuid, name))
			{
				return null;
			}
			return GuidNamePropertyDefinition.InternalCreateCustom(string.Empty, tnefType, propertySetGuid, name, PropertyFlags.None, NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType, new PropertyDefinitionConstraint[0]);
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000F04E8 File Offset: 0x000EE6E8
		protected void PromoteProperty(StorePropertyDefinition property, bool allowLargeProperties)
		{
			if (allowLargeProperties && this.IsLargeValue())
			{
				this.PromoteLargeProperty(property);
				return;
			}
			object value = this.ReadValue();
			this.SetProperty(property, value);
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000F0517 File Offset: 0x000EE717
		protected void SetProperty(StorePropertyDefinition property, object value)
		{
			this.messageWriter.SetProperty(property, value);
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000F0528 File Offset: 0x000EE728
		protected void PromoteAddressProperty(StorePropertyDefinition property)
		{
			object value = this.ReadValue();
			this.messageWriter.SetAddressProperty(property, value);
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000F054C File Offset: 0x000EE74C
		protected void PromoteLargeProperty(StorePropertyDefinition property)
		{
			NativeStorePropertyDefinition nativeStorePropertyDefinition = property as NativeStorePropertyDefinition;
			if (nativeStorePropertyDefinition == null)
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundTnefTracer, "AbstractInboundConverter::ReadSmallPropertyValue: non-native property");
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent);
			}
			if (nativeStorePropertyDefinition.MapiPropertyType != PropType.String && nativeStorePropertyDefinition.MapiPropertyType != PropType.Binary)
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundTnefTracer, "AbstractInboundConverter::ReadSmallPropertyValue: non-streamable property");
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent);
			}
			int trailingNulls = 0;
			using (Stream stream = this.OpenValueReadStream(out trailingNulls))
			{
				using (Stream stream2 = this.OpenPropertyStream(property))
				{
					Util.StreamHandler.CopyStreamData(stream, stream2, null, trailingNulls);
				}
			}
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000F0600 File Offset: 0x000EE800
		protected void PromoteTimeZoneProperty(StorePropertyDefinition property)
		{
			string text = this.ReadValue() as string;
			if (!string.IsNullOrEmpty(text) && text.EndsWith("\r\n"))
			{
				text = text.Substring(0, text.Length - 2);
			}
			this.SetProperty(property, text);
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000F0646 File Offset: 0x000EE846
		private Stream OpenPropertyStream(StorePropertyDefinition property)
		{
			return this.MessageWriter.OpenPropertyStream(property);
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x000F0654 File Offset: 0x000EE854
		protected InboundConversionOptions ConversionOptions
		{
			get
			{
				return this.MessageWriter.ConversionOptions;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x000F0661 File Offset: 0x000EE861
		protected bool IsSenderTrusted
		{
			get
			{
				return this.ConversionOptions.IsSenderTrusted;
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06003A77 RID: 14967 RVA: 0x000F066E File Offset: 0x000EE86E
		protected ConversionComponentType CurrentComponentType
		{
			get
			{
				return this.MessageWriter.ComponentType;
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x000F067B File Offset: 0x000EE87B
		protected InboundMessageWriter MessageWriter
		{
			get
			{
				return this.messageWriter;
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06003A79 RID: 14969 RVA: 0x000F0683 File Offset: 0x000EE883
		protected ICoreItem CoreItem
		{
			get
			{
				return this.MessageWriter.CoreItem;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06003A7A RID: 14970 RVA: 0x000F0690 File Offset: 0x000EE890
		protected bool IsTopLevelMessage
		{
			get
			{
				return this.MessageWriter.IsTopLevelMessage;
			}
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000F06A0 File Offset: 0x000EE8A0
		private static void AddPromotionRule(Dictionary<NativeStorePropertyDefinition, AbstractInboundConverter.IPromotionRule> rulesTable, AbstractInboundConverter.IPromotionRule rule, params NativeStorePropertyDefinition[] properties)
		{
			foreach (NativeStorePropertyDefinition key in properties)
			{
				rulesTable[key] = rule;
			}
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000F06CC File Offset: 0x000EE8CC
		private static void AddAddressRule(Dictionary<NativeStorePropertyDefinition, AbstractInboundConverter.IPromotionRule> rulesTable)
		{
			HashSet<NativeStorePropertyDefinition> allCacheProperties = ConversionAddressCache.AllCacheProperties;
			AbstractInboundConverter.AddressPromotionRule value = new AbstractInboundConverter.AddressPromotionRule();
			foreach (NativeStorePropertyDefinition key in allCacheProperties)
			{
				AbstractInboundConverter.IPromotionRule promotionRule = null;
				if (!rulesTable.TryGetValue(key, out promotionRule))
				{
					rulesTable[key] = value;
				}
			}
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000F080C File Offset: 0x000EEA0C
		private static Dictionary<NativeStorePropertyDefinition, AbstractInboundConverter.IPromotionRule> CreateRulesTable()
		{
			Dictionary<NativeStorePropertyDefinition, AbstractInboundConverter.IPromotionRule> dictionary = new Dictionary<NativeStorePropertyDefinition, AbstractInboundConverter.IPromotionRule>();
			AbstractInboundConverter.AddPromotionRule(dictionary, null, new NativeStorePropertyDefinition[]
			{
				InternalSchema.AttachNum
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, null, new NativeStorePropertyDefinition[]
			{
				InternalSchema.NativeBlockStatus
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, null, new NativeStorePropertyDefinition[]
			{
				InternalSchema.NativeBodyInfo
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, null, new NativeStorePropertyDefinition[]
			{
				InternalSchema.XMsExchOrganizationAVStampMailbox
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, null, new NativeStorePropertyDefinition[]
			{
				InternalSchema.QuarantineOriginalSender
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, null, new NativeStorePropertyDefinition[]
			{
				InternalSchema.LocallyDelivered,
				InternalSchema.EntryId
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.CustomRule(delegate(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				if (converter.CanPromoteMimeOnlyProperties())
				{
					converter.PromoteProperty(property, false);
				}
			}), new NativeStorePropertyDefinition[]
			{
				InternalSchema.SpamConfidenceLevel,
				InternalSchema.ContentFilterPcl,
				InternalSchema.SenderIdStatus,
				InternalSchema.PurportedSenderDomain,
				InternalSchema.IsClassified,
				InternalSchema.Classification,
				InternalSchema.ClassificationDescription,
				InternalSchema.ClassificationGuid,
				InternalSchema.ClassificationKeep,
				InternalSchema.XMsExchOrganizationAuthAs,
				InternalSchema.XMsExchOrganizationAuthDomain,
				InternalSchema.XMsExchOrganizationAuthMechanism,
				InternalSchema.XMsExchOrganizationAuthSource,
				InternalSchema.ApprovalAllowedDecisionMakers,
				InternalSchema.ApprovalRequestor
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.ComponentSpecificPromotionRule(ConversionComponentType.Message), new NativeStorePropertyDefinition[]
			{
				InternalSchema.UrlCompName
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.ComponentSpecificPromotionRule(ConversionComponentType.Recipient), new NativeStorePropertyDefinition[]
			{
				InternalSchema.ObjectType
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.ComponentSpecificPromotionRule(ConversionComponentType.FileAttachment), new NativeStorePropertyDefinition[]
			{
				InternalSchema.IsContactPhoto,
				InternalSchema.AttachCalendarHidden
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, null, new NativeStorePropertyDefinition[]
			{
				InternalSchema.SentMailSvrEId,
				InternalSchema.StoreEntryId,
				InternalSchema.StoreRecordKey,
				InternalSchema.ParentEntryId,
				InternalSchema.SourceKey,
				InternalSchema.CreatorEntryId,
				InternalSchema.LastModifierEntryId,
				InternalSchema.MdbProvider,
				InternalSchema.MappingSignature,
				InternalSchema.UrlCompNamePostfix,
				InternalSchema.MID,
				InternalSchema.Associated,
				InternalSchema.Size,
				InternalSchema.SentMailSvrEId,
				InternalSchema.SentMailEntryId,
				InternalSchema.PredictedActionsInternal,
				InternalSchema.GroupingActionsDeprecated,
				InternalSchema.PredictedActionsSummaryDeprecated,
				InternalSchema.AttachSize
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.SmartPropertyPromotionRule(InternalSchema.AutoResponseSuppress, ConversionComponentType.Message, true), new NativeStorePropertyDefinition[]
			{
				InternalSchema.AutoResponseSuppressInternal
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.SmartPropertyPromotionRule(InternalSchema.IsDeliveryReceiptRequested, ConversionComponentType.Message, true), new NativeStorePropertyDefinition[]
			{
				InternalSchema.IsDeliveryReceiptRequestedInternal
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.SmartPropertyPromotionRule(InternalSchema.IsNonDeliveryReceiptRequested, ConversionComponentType.Message, true), new NativeStorePropertyDefinition[]
			{
				InternalSchema.IsNonDeliveryReceiptRequestedInternal
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.SmartPropertyPromotionRule(InternalSchema.IsReadReceiptRequested, ConversionComponentType.Message, true), new NativeStorePropertyDefinition[]
			{
				InternalSchema.IsReadReceiptRequestedInternal
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.SmartPropertyPromotionRule(InternalSchema.IsNotReadReceiptRequested, ConversionComponentType.Message, true), new NativeStorePropertyDefinition[]
			{
				InternalSchema.IsNotReadReceiptRequestedInternal
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.CustomRule(delegate(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				if (converter.IsLargeValue())
				{
					StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundTnefTracer, "AbstractInboundConverter.ParseTnefProperty - subject value too big, ignoring...");
					return;
				}
				string text = (string)converter.ReadValue();
				if (text != null)
				{
					converter.messageWriter.SetSubjectProperty(property, text);
				}
			}), new NativeStorePropertyDefinition[]
			{
				InternalSchema.MapiSubject,
				InternalSchema.SubjectPrefixInternal,
				InternalSchema.NormalizedSubjectInternal
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.CustomRule(delegate(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				if (converter.CurrentComponentType == ConversionComponentType.Message && !converter.ConversionOptions.ClearCategories)
				{
					converter.PromoteProperty(property, false);
				}
			}), new NativeStorePropertyDefinition[]
			{
				InternalSchema.Categories
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.CustomRule(delegate(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				converter.PromoteInternetCpidProperty();
			}), new NativeStorePropertyDefinition[]
			{
				InternalSchema.MapiInternetCpid
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.CustomRule(delegate(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				if (converter.CurrentComponentType == ConversionComponentType.FileAttachment)
				{
					converter.PromoteProperty(property, true);
					return;
				}
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, ServerStrings.InvalidTnef, null);
			}), new NativeStorePropertyDefinition[]
			{
				InternalSchema.AttachDataBin
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.CustomRule(delegate(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				if (converter.CurrentComponentType == ConversionComponentType.FileAttachment)
				{
					converter.PromoteAttachDataObject();
					return;
				}
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, ServerStrings.InvalidTnef, null);
			}), new NativeStorePropertyDefinition[]
			{
				InternalSchema.AttachDataObj
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.CustomRule(delegate(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				converter.PromoteMessageClass();
			}), new NativeStorePropertyDefinition[]
			{
				InternalSchema.ItemClass
			});
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.CustomRule(delegate(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				converter.PromoteBodyProperty(property);
			}), new NativeStorePropertyDefinition[]
			{
				InternalSchema.TextBody,
				InternalSchema.HtmlBody,
				InternalSchema.RtfBody,
				InternalSchema.RtfInSync
			});
			AbstractInboundConverter.AddAddressRule(dictionary);
			AbstractInboundConverter.AddPromotionRule(dictionary, new AbstractInboundConverter.CustomRule(delegate(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				converter.PromoteTimeZoneProperty(property);
			}), new NativeStorePropertyDefinition[]
			{
				InternalSchema.TimeZone
			});
			return dictionary;
		}

		// Token: 0x04001F5A RID: 8026
		private InboundMessageWriter messageWriter;

		// Token: 0x04001F5B RID: 8027
		private readonly AbstractInboundConverter.IPromotionRule defaultPromotionRule;

		// Token: 0x04001F5C RID: 8028
		private readonly Dictionary<NativeStorePropertyDefinition, AbstractInboundConverter.IPromotionRule> rules = AbstractInboundConverter.staticRules;

		// Token: 0x04001F5D RID: 8029
		private static readonly Dictionary<NativeStorePropertyDefinition, AbstractInboundConverter.IPromotionRule> staticRules = AbstractInboundConverter.CreateRulesTable();

		// Token: 0x0200058F RID: 1423
		internal interface IPromotionRule
		{
			// Token: 0x06003A88 RID: 14984
			void PromoteProperty(AbstractInboundConverter converter, NativeStorePropertyDefinition definition);
		}

		// Token: 0x02000590 RID: 1424
		protected class WriteablePropertyPromotionRule : AbstractInboundConverter.IPromotionRule
		{
			// Token: 0x06003A89 RID: 14985 RVA: 0x000F0D79 File Offset: 0x000EEF79
			public void PromoteProperty(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				if ((property.PropertyFlags & PropertyFlags.ReadOnly) != PropertyFlags.ReadOnly)
				{
					converter.PromoteProperty(property, true);
				}
			}
		}

		// Token: 0x02000591 RID: 1425
		protected class TransmittablePropertyPromotionRule : AbstractInboundConverter.IPromotionRule
		{
			// Token: 0x06003A8B RID: 14987 RVA: 0x000F0D96 File Offset: 0x000EEF96
			public void PromoteProperty(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				if (!converter.IsTopLevelMessage)
				{
					converter.PromoteProperty(property, true);
					return;
				}
				if ((property.PropertyFlags & PropertyFlags.Transmittable) == PropertyFlags.Transmittable)
				{
					converter.PromoteProperty(property, true);
				}
			}
		}

		// Token: 0x02000592 RID: 1426
		private class AddressPromotionRule : AbstractInboundConverter.IPromotionRule
		{
			// Token: 0x06003A8E RID: 14990 RVA: 0x000F0DD4 File Offset: 0x000EEFD4
			public void PromoteProperty(AbstractInboundConverter converter, NativeStorePropertyDefinition definition)
			{
				if (converter.CurrentComponentType == ConversionComponentType.Message)
				{
					converter.PromoteAddressProperty(definition);
				}
			}
		}

		// Token: 0x02000593 RID: 1427
		private class ComponentSpecificPromotionRule : AbstractInboundConverter.IPromotionRule
		{
			// Token: 0x06003A8F RID: 14991 RVA: 0x000F0DE5 File Offset: 0x000EEFE5
			public ComponentSpecificPromotionRule(ConversionComponentType targetComponentType)
			{
				this.targetComponentType = targetComponentType;
			}

			// Token: 0x06003A90 RID: 14992 RVA: 0x000F0DF4 File Offset: 0x000EEFF4
			public void PromoteProperty(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				if (converter.CurrentComponentType == this.targetComponentType)
				{
					converter.PromoteProperty(property, true);
				}
			}

			// Token: 0x04001F67 RID: 8039
			private readonly ConversionComponentType targetComponentType;
		}

		// Token: 0x02000594 RID: 1428
		private class SmartPropertyPromotionRule : AbstractInboundConverter.IPromotionRule
		{
			// Token: 0x06003A91 RID: 14993 RVA: 0x000F0E0C File Offset: 0x000EF00C
			public SmartPropertyPromotionRule(StorePropertyDefinition substituteProperty, ConversionComponentType componentType, bool promoteForOtherComponentTypes)
			{
				this.substituteProperty = substituteProperty;
				this.expectedComponentType = componentType;
				this.promoteForOtherComponentTypes = promoteForOtherComponentTypes;
			}

			// Token: 0x06003A92 RID: 14994 RVA: 0x000F0E2C File Offset: 0x000EF02C
			public void PromoteProperty(AbstractInboundConverter converter, NativeStorePropertyDefinition property)
			{
				if (converter.CurrentComponentType == this.expectedComponentType)
				{
					converter.PromoteProperty(this.substituteProperty, false);
					return;
				}
				StorageGlobals.ContextTraceError<NativeStorePropertyDefinition, ConversionComponentType>(ExTraceGlobals.CcInboundTnefTracer, "SmartPropertyPromotionRule.PromoteProperty {0}, wrong component type {1}", property, converter.CurrentComponentType);
				if (this.promoteForOtherComponentTypes)
				{
					converter.PromoteProperty(property, false);
				}
			}

			// Token: 0x04001F68 RID: 8040
			private readonly StorePropertyDefinition substituteProperty;

			// Token: 0x04001F69 RID: 8041
			private readonly ConversionComponentType expectedComponentType;

			// Token: 0x04001F6A RID: 8042
			private readonly bool promoteForOtherComponentTypes;
		}

		// Token: 0x02000595 RID: 1429
		private class CustomRule : AbstractInboundConverter.IPromotionRule
		{
			// Token: 0x06003A93 RID: 14995 RVA: 0x000F0E7B File Offset: 0x000EF07B
			public CustomRule(AbstractInboundConverter.CustomRule.CustomPromotionDelegate promotionDelegate)
			{
				this.promotionDelegate = promotionDelegate;
			}

			// Token: 0x06003A94 RID: 14996 RVA: 0x000F0E8A File Offset: 0x000EF08A
			public void PromoteProperty(AbstractInboundConverter converter, NativeStorePropertyDefinition definition)
			{
				this.promotionDelegate(converter, definition);
			}

			// Token: 0x04001F6B RID: 8043
			private AbstractInboundConverter.CustomRule.CustomPromotionDelegate promotionDelegate;

			// Token: 0x02000596 RID: 1430
			// (Invoke) Token: 0x06003A96 RID: 14998
			public delegate void CustomPromotionDelegate(AbstractInboundConverter converter, NativeStorePropertyDefinition definition);
		}
	}
}
