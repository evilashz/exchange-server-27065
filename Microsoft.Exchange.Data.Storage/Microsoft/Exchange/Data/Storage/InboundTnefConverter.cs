using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005F2 RID: 1522
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class InboundTnefConverter : AbstractInboundConverter
	{
		// Token: 0x06003E79 RID: 15993 RVA: 0x001033E5 File Offset: 0x001015E5
		internal InboundTnefConverter(InboundMessageWriter writer) : base(writer, new AbstractInboundConverter.TransmittablePropertyPromotionRule())
		{
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x001033F4 File Offset: 0x001015F4
		internal void ConvertToItem(Stream tnefStream, int primaryCodepage, bool isSummaryTnef)
		{
			base.SetProperty(InternalSchema.SendRichInfo, true);
			TnefReader tnefReader = new TnefReader(tnefStream, primaryCodepage, TnefComplianceMode.Loose);
			this.ConvertToItem(tnefReader, isSummaryTnef);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x00103424 File Offset: 0x00101624
		private void ConvertToItem(TnefReader reader, bool isSummaryTnef)
		{
			if (this.IsReplicationMessage)
			{
				PersistablePropertyBag persistablePropertyBag = CoreObject.GetPersistablePropertyBag(base.MessageWriter.CoreItem);
				if (persistablePropertyBag != null)
				{
					persistablePropertyBag.Context.IsValidationDisabled = true;
				}
			}
			this.reader = reader;
			this.isSummaryTnef = isSummaryTnef;
			this.isRecipientTablePromoted = false;
			while (this.reader.ReadNextAttribute())
			{
				this.CheckTnefComplianceStatus();
				this.ReadAttribute();
			}
			this.ProcessEndTnef();
			PropertyBagSaveFlags propertyBagSaveFlags = PropertyBagSaveFlags.IgnoreMapiComputedErrors | base.ConversionOptions.GetSaveFlags(base.IsTopLevelMessage);
			CoreObject.GetPersistablePropertyBag(base.CoreItem).SaveFlags |= propertyBagSaveFlags;
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x001034BC File Offset: 0x001016BC
		private void ReadAttribute()
		{
			TnefPropertyTag @null = TnefPropertyTag.Null;
			if (base.CurrentComponentType == ConversionComponentType.FileAttachment)
			{
				TnefAttributeTag attributeTag = this.reader.AttributeTag;
				if (attributeTag <= TnefAttributeTag.AttachModifyDate)
				{
					if (attributeTag != TnefAttributeTag.AttachTitle)
					{
						switch (attributeTag)
						{
						case TnefAttributeTag.AttachCreateDate:
						case TnefAttributeTag.AttachModifyDate:
							break;
						default:
							goto IL_71;
						}
					}
				}
				else
				{
					switch (attributeTag)
					{
					case TnefAttributeTag.AttachData:
					case TnefAttributeTag.AttachMetaFile:
						break;
					case (TnefAttributeTag)426000:
						goto IL_71;
					default:
						if (attributeTag != TnefAttributeTag.AttachTransportFilename && attributeTag != TnefAttributeTag.Attachment)
						{
							goto IL_71;
						}
						break;
					}
				}
				this.ParseAttachmentProperties();
				goto IL_77;
				IL_71:
				this.EndAttachment();
			}
			IL_77:
			if (base.CurrentComponentType == ConversionComponentType.Message)
			{
				TnefAttributeTag attributeTag2 = this.reader.AttributeTag;
				if (attributeTag2 <= TnefAttributeTag.RequestResponse)
				{
					if (attributeTag2 <= TnefAttributeTag.Body)
					{
						if (attributeTag2 <= TnefAttributeTag.Subject)
						{
							if (attributeTag2 != TnefAttributeTag.From && attributeTag2 != TnefAttributeTag.Subject)
							{
								goto IL_211;
							}
						}
						else
						{
							switch (attributeTag2)
							{
							case TnefAttributeTag.MessageId:
							case TnefAttributeTag.ParentId:
							case TnefAttributeTag.ConversationId:
								break;
							default:
								if (attributeTag2 != TnefAttributeTag.Body)
								{
									goto IL_211;
								}
								break;
							}
						}
					}
					else if (attributeTag2 <= TnefAttributeTag.DateReceived)
					{
						switch (attributeTag2)
						{
						case TnefAttributeTag.DateStart:
						case TnefAttributeTag.DateEnd:
							break;
						default:
							switch (attributeTag2)
							{
							case TnefAttributeTag.DateSent:
							case TnefAttributeTag.DateReceived:
								break;
							default:
								goto IL_211;
							}
							break;
						}
					}
					else if (attributeTag2 != TnefAttributeTag.DateModified && attributeTag2 != TnefAttributeTag.RequestResponse)
					{
						goto IL_211;
					}
				}
				else if (attributeTag2 <= TnefAttributeTag.MessageStatus)
				{
					if (attributeTag2 <= TnefAttributeTag.AidOwner)
					{
						if (attributeTag2 != TnefAttributeTag.Priority && attributeTag2 != TnefAttributeTag.AidOwner)
						{
							goto IL_211;
						}
					}
					else
					{
						switch (attributeTag2)
						{
						case TnefAttributeTag.Owner:
						case TnefAttributeTag.SentFor:
						case TnefAttributeTag.Delegate:
							break;
						default:
							if (attributeTag2 != TnefAttributeTag.MessageStatus)
							{
								goto IL_211;
							}
							this.ParseMapiProperties(true);
							return;
						}
					}
				}
				else if (attributeTag2 <= TnefAttributeTag.OriginalMessageClass)
				{
					switch (attributeTag2)
					{
					case TnefAttributeTag.AttachRenderData:
						this.StartNewAttachment();
						this.ParseAttachRenderData();
						return;
					case TnefAttributeTag.MapiProperties:
						break;
					case TnefAttributeTag.RecipientTable:
						this.ParseRecipientTable();
						return;
					case TnefAttributeTag.Attachment:
					case (TnefAttributeTag)430086:
						goto IL_211;
					case TnefAttributeTag.OemCodepage:
						base.SetProperty(InternalSchema.Codepage, this.reader.MessageCodepage);
						return;
					default:
						if (attributeTag2 != TnefAttributeTag.OriginalMessageClass)
						{
							goto IL_211;
						}
						break;
					}
				}
				else if (attributeTag2 != TnefAttributeTag.MessageClass)
				{
					if (attributeTag2 != TnefAttributeTag.TnefVersion)
					{
						goto IL_211;
					}
					return;
				}
				this.ParseMapiProperties(false);
				return;
				IL_211:
				StorageGlobals.ContextTraceDebug<uint>(ExTraceGlobals.CcInboundTnefTracer, "Unknown TNEF attribute encountered: 0x{0:X}. Ignoring.", (uint)this.reader.AttributeTag);
			}
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x001036F4 File Offset: 0x001018F4
		private void ParseMapiProperties(bool forceTransmittable)
		{
			TnefPropertyReader propertyReader = this.reader.PropertyReader;
			while (propertyReader.ReadNextProperty())
			{
				this.CheckTnefComplianceStatus();
				this.ParseTnefProperty(propertyReader, forceTransmittable);
			}
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x00103728 File Offset: 0x00101928
		private void ParseTnefProperty(TnefPropertyReader propertyReader, bool forceTransmittable)
		{
			this.CheckTnefComplianceStatus();
			TnefNameId? namedProperty = propertyReader.IsNamedProperty ? new TnefNameId?(propertyReader.PropertyNameId) : null;
			NativeStorePropertyDefinition nativeStorePropertyDefinition = base.CreatePropertyDefinition(propertyReader.PropertyTag, namedProperty);
			if (nativeStorePropertyDefinition == null)
			{
				return;
			}
			AbstractInboundConverter.IPromotionRule propertyPromotionRule = base.GetPropertyPromotionRule(nativeStorePropertyDefinition);
			if (propertyPromotionRule != null)
			{
				propertyPromotionRule.PromoteProperty(this, nativeStorePropertyDefinition);
			}
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x00103784 File Offset: 0x00101984
		protected override bool IsLargeValue()
		{
			return !this.PropertyReader.PropertyTag.IsMultiValued && this.PropertyReader.IsLargeValue;
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x001037BC File Offset: 0x001019BC
		protected override object ReadValue()
		{
			if (this.IsLargeValue())
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundTnefTracer, "InboundTnefConverter::ReadValue: large property value");
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent);
			}
			if (this.PropertyReader.IsMultiValuedProperty)
			{
				Type elementType = InternalSchema.ClrTypeFromPropTagType(ConvertUtils.GetPropertyBaseType(this.PropertyReader.PropertyTag));
				List<object> list = new List<object>();
				while (this.PropertyReader.ReadNextValue())
				{
					if (this.PropertyReader.IsLargeValue)
					{
						throw new ConversionFailedException(ConversionFailureReason.CorruptContent, ServerStrings.LargeMultivaluedPropertiesNotSupportedInTNEF, null);
					}
					object obj = this.ReadSingleValue();
					if (obj == null)
					{
						throw new ConversionFailedException(ConversionFailureReason.CorruptContent, ServerStrings.InvalidTnef, null);
					}
					list.Add(obj);
				}
				Array array = Array.CreateInstance(elementType, list.Count);
				for (int i = 0; i < array.Length; i++)
				{
					array.SetValue(list[i], i);
				}
				return array;
			}
			return this.ReadSingleValue();
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x001038A8 File Offset: 0x00101AA8
		private object ReadSingleValue()
		{
			TnefPropertyType tnefType = this.PropertyReader.PropertyTag.TnefType;
			if (tnefType == TnefPropertyType.AppTime || tnefType == (TnefPropertyType)4103)
			{
				double num = 0.0;
				DateTime dateTime = this.PropertyReader.ReadValueAsDateTime();
				try
				{
					num = ConvertUtils.GetOADate(dateTime);
				}
				catch (OverflowException arg)
				{
					StorageGlobals.ContextTraceError<DateTime, OverflowException>(ExTraceGlobals.CcInboundTnefTracer, "InboundTnefConverter::ReadPropertyReaderValue: OverflowException processing date {0}, {1}.", dateTime, arg);
				}
				return num;
			}
			return ExTimeZoneHelperForMigrationOnly.ToExDateTimeIfObjectIsDateTime(this.PropertyReader.ReadValue());
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x0010393C File Offset: 0x00101B3C
		protected override Stream OpenValueReadStream(out int skipTrailingNulls)
		{
			TnefPropertyType tnefType = this.PropertyReader.PropertyTag.TnefType;
			TnefPropertyType tnefPropertyType = tnefType;
			switch (tnefPropertyType)
			{
			case TnefPropertyType.String8:
			{
				skipTrailingNulls = 1;
				TextToText textToText = new TextToText(TextToTextConversionMode.ConvertCodePageOnly);
				textToText.InputEncoding = this.GetString8Encoding();
				textToText.OutputEncoding = ConvertUtils.UnicodeEncoding;
				return new ConverterStream(this.PropertyReader.GetRawValueReadStream(), textToText, ConverterStreamAccess.Read);
			}
			case TnefPropertyType.Unicode:
				skipTrailingNulls = 2;
				return this.PropertyReader.GetRawValueReadStream();
			default:
				if (tnefPropertyType == TnefPropertyType.Binary)
				{
					skipTrailingNulls = 0;
					return this.PropertyReader.GetRawValueReadStream();
				}
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundTnefTracer, "InboundTnefConverter::StreamLargeProperty: only supports binary and string properties.");
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent);
			}
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x001039F4 File Offset: 0x00101BF4
		private void ParseRecipientTable()
		{
			string valueOrDefault = base.CoreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass);
			if (base.ConversionOptions.IsSenderTrusted || !base.MessageWriter.IsTopLevelWriter || ObjectClass.IsNonSendableWithRecipients(valueOrDefault) || ObjectClass.IsDsn(valueOrDefault))
			{
				TnefPropertyReader propertyReader = this.reader.PropertyReader;
				while (propertyReader.ReadNextRow())
				{
					this.NewRecipient();
					while (propertyReader.ReadNextProperty())
					{
						this.ParseTnefProperty(propertyReader, false);
					}
					this.EndRecipient();
					this.isRecipientTablePromoted = true;
				}
			}
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x00103A7E File Offset: 0x00101C7E
		private void NewRecipient()
		{
			base.MessageWriter.StartNewRecipient();
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x00103A8B File Offset: 0x00101C8B
		private void EndRecipient()
		{
			base.MessageWriter.EndRecipient();
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x00103A98 File Offset: 0x00101C98
		private void StartNewAttachment()
		{
			base.MessageWriter.StartNewAttachment();
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x00103AA5 File Offset: 0x00101CA5
		private void EndAttachment()
		{
			base.MessageWriter.EndAttachment();
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x00103AB4 File Offset: 0x00101CB4
		private void ParseAttachRenderData()
		{
			TnefPropertyReader propertyReader = this.reader.PropertyReader;
			while (propertyReader.ReadNextProperty())
			{
				if (propertyReader.PropertyTag != TnefPropertyTag.AttachMethod)
				{
					this.ParseTnefProperty(propertyReader, false);
				}
			}
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x00103AF8 File Offset: 0x00101CF8
		private void ParseAttachmentProperties()
		{
			TnefPropertyReader propertyReader = this.reader.PropertyReader;
			while (propertyReader.ReadNextProperty())
			{
				this.ParseTnefProperty(propertyReader, false);
			}
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x00103B24 File Offset: 0x00101D24
		protected override void PromoteAttachDataObject()
		{
			if (this.PropertyReader.IsEmbeddedMessage)
			{
				using (TnefReader embeddedMessageReader = this.PropertyReader.GetEmbeddedMessageReader())
				{
					using (InboundMessageWriter inboundMessageWriter = base.MessageWriter.OpenAttachedMessageWriter())
					{
						if (this.IsReplicationMessage)
						{
							inboundMessageWriter.ForceParticipantResolution = this.ResolveParticipantsOnAttachments;
						}
						else
						{
							inboundMessageWriter.ForceParticipantResolution = base.ConversionOptions.ResolveRecipientsInAttachedMessages;
						}
						new InboundTnefConverter(inboundMessageWriter)
						{
							IsReplicationMessage = this.IsReplicationMessage
						}.ConvertToItem(embeddedMessageReader, this.isSummaryTnef);
						inboundMessageWriter.Commit();
					}
					return;
				}
			}
			if (this.PropertyReader.ObjectIid == MimeConstants.IID_IStorage)
			{
				using (Stream rawValueReadStream = this.PropertyReader.GetRawValueReadStream())
				{
					using (Stream stream = base.MessageWriter.OpenOleAttachmentDataStream())
					{
						Util.StreamHandler.CopyStreamData(rawValueReadStream, stream, null, 0, 131072);
					}
				}
			}
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x00103C68 File Offset: 0x00101E68
		protected override void PromoteInternetCpidProperty()
		{
			int num = this.PropertyReader.ReadValueAsInt32();
			this.internetCpid = num;
			base.SetProperty(InternalSchema.InternetCpid, num);
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x00103C9C File Offset: 0x00101E9C
		protected override bool CanPromoteMimeOnlyProperties()
		{
			return !base.IsTopLevelMessage;
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x00103CA8 File Offset: 0x00101EA8
		protected override void PromoteMessageClass()
		{
			string text = (string)this.ReadValue();
			base.SetProperty(InternalSchema.ItemClass, text);
			if (ObjectClass.IsOfClass(text, "IPM.Replication"))
			{
				base.ConversionOptions.ClearCategories = false;
				if (base.IsTopLevelMessage && this.isSummaryTnef && base.ConversionOptions.IsSenderTrusted)
				{
					base.MessageWriter.SuppressLimitChecks();
				}
			}
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x00103D10 File Offset: 0x00101F10
		protected override void PromoteBodyProperty(StorePropertyDefinition property)
		{
			if (base.CurrentComponentType != ConversionComponentType.Message)
			{
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent);
			}
			if (property == InternalSchema.RtfInSync)
			{
				base.PromoteProperty(property, false);
				return;
			}
			if (this.promotedBodyProperty == null)
			{
				this.promotedBodyProperty = property;
				this.StreamBody(property, this.PropertyReader);
				return;
			}
			if (property == InternalSchema.TextBody)
			{
				if (this.isSummaryTnef)
				{
					base.MessageWriter.DeleteMessageProperty(InternalSchema.RtfBody);
				}
				this.promotedBodyProperty = InternalSchema.TextBody;
				this.StreamBody(property, this.PropertyReader);
			}
			if (property == InternalSchema.HtmlBody)
			{
				this.StreamBody(property, this.PropertyReader);
			}
			if (property == InternalSchema.RtfBody)
			{
				if (this.promotedBodyProperty == InternalSchema.TextBody && this.isSummaryTnef)
				{
					return;
				}
				this.StreamBody(property, this.PropertyReader);
			}
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x00103DD4 File Offset: 0x00101FD4
		private void StreamBody(StorePropertyDefinition property, TnefPropertyReader reader)
		{
			Charset charset = null;
			if (this.internetCpid != 0)
			{
				ConvertUtils.TryGetValidCharset(this.internetCpid, out charset);
			}
			string text = (charset != null) ? charset.Name : null;
			BodyWriteConfiguration bodyWriteConfiguration = null;
			if (property == InternalSchema.TextBody)
			{
				bodyWriteConfiguration = new BodyWriteConfiguration(BodyFormat.TextPlain, ConvertUtils.UnicodeCharset.Name);
				bodyWriteConfiguration.SetTargetFormat(BodyFormat.TextPlain, text);
			}
			else if (property == InternalSchema.HtmlBody)
			{
				bodyWriteConfiguration = new BodyWriteConfiguration(BodyFormat.TextHtml, text);
				bodyWriteConfiguration.SetTargetFormat(BodyFormat.TextHtml, text);
			}
			else if (property == InternalSchema.RtfBody)
			{
				bodyWriteConfiguration = new BodyWriteConfiguration(BodyFormat.ApplicationRtf);
				bodyWriteConfiguration.SetTargetFormat(BodyFormat.ApplicationRtf, text);
			}
			int trailingNulls;
			using (Stream stream = this.OpenValueReadStream(out trailingNulls))
			{
				base.CoreItem.CharsetDetector.DetectionOptions = base.ConversionOptions.DetectionOptions;
				using (Stream stream2 = base.CoreItem.Body.InternalOpenWriteStream(bodyWriteConfiguration, null))
				{
					Util.StreamHandler.CopyStreamData(stream, stream2, null, trailingNulls, 65536);
				}
			}
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x00103EE8 File Offset: 0x001020E8
		private void ProcessEndTnef()
		{
			ConversionComponentType currentComponentType = base.CurrentComponentType;
			if (currentComponentType == ConversionComponentType.FileAttachment)
			{
				this.EndAttachment();
			}
			this.CheckTnefComplianceStatus();
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x00103F0C File Offset: 0x0010210C
		internal void Undo()
		{
			if (base.MessageWriter != null)
			{
				base.MessageWriter.UndoTnef();
			}
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x00103F24 File Offset: 0x00102124
		private void CheckTnefComplianceStatus()
		{
			TnefComplianceStatus complianceStatus = this.reader.ComplianceStatus;
			if ((complianceStatus & ~(TnefComplianceStatus.InvalidAttributeChecksum | TnefComplianceStatus.InvalidMessageCodepage | TnefComplianceStatus.InvalidDate)) != TnefComplianceStatus.Compliant)
			{
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, ServerStrings.ConversionCorruptTnef((int)complianceStatus), null);
			}
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x00103F54 File Offset: 0x00102154
		private Encoding GetString8Encoding()
		{
			if (this.string8Encoding == null)
			{
				Charset defaultWindowsCharset;
				if (!ConvertUtils.TryGetValidCharset(this.reader.MessageCodepage, out defaultWindowsCharset))
				{
					defaultWindowsCharset = Charset.DefaultWindowsCharset;
				}
				this.string8Encoding = defaultWindowsCharset.GetEncoding();
			}
			return this.string8Encoding;
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06003E94 RID: 16020 RVA: 0x00103F95 File Offset: 0x00102195
		internal bool IsRecipientTablePromoted
		{
			get
			{
				return this.isRecipientTablePromoted;
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x00103F9D File Offset: 0x0010219D
		internal StorePropertyDefinition PromotedBodyProperty
		{
			get
			{
				return this.promotedBodyProperty;
			}
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06003E96 RID: 16022 RVA: 0x00103FA5 File Offset: 0x001021A5
		internal TnefPropertyReader PropertyReader
		{
			get
			{
				return this.reader.PropertyReader;
			}
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06003E97 RID: 16023 RVA: 0x00103FB2 File Offset: 0x001021B2
		// (set) Token: 0x06003E98 RID: 16024 RVA: 0x00103FBA File Offset: 0x001021BA
		internal bool ResolveParticipantsOnAttachments
		{
			get
			{
				return this.resolveParticipantsOnAttachments;
			}
			set
			{
				this.resolveParticipantsOnAttachments = value;
			}
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06003E99 RID: 16025 RVA: 0x00103FC3 File Offset: 0x001021C3
		// (set) Token: 0x06003E9A RID: 16026 RVA: 0x00103FCB File Offset: 0x001021CB
		internal bool IsReplicationMessage
		{
			get
			{
				return this.isReplicationMessage;
			}
			set
			{
				this.isReplicationMessage = value;
			}
		}

		// Token: 0x04002170 RID: 8560
		private TnefReader reader;

		// Token: 0x04002171 RID: 8561
		private Encoding string8Encoding;

		// Token: 0x04002172 RID: 8562
		private int internetCpid;

		// Token: 0x04002173 RID: 8563
		private bool isSummaryTnef;

		// Token: 0x04002174 RID: 8564
		private bool isReplicationMessage;

		// Token: 0x04002175 RID: 8565
		private StorePropertyDefinition promotedBodyProperty;

		// Token: 0x04002176 RID: 8566
		private bool isRecipientTablePromoted;

		// Token: 0x04002177 RID: 8567
		private bool resolveParticipantsOnAttachments;
	}
}
