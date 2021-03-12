using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005F6 RID: 1526
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ItemToTnefConverter : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003EBC RID: 16060 RVA: 0x001049E0 File Offset: 0x00102BE0
		internal ItemToTnefConverter(Item itemIn, OutboundAddressCache addressCache, Stream mimeOut, OutboundConversionOptions options, ConversionLimitsTracker limits, TnefType tnefType, string tnefCorrelationKey, bool parsingEmbeddedItem) : this(itemIn, addressCache, options, limits, tnefType, parsingEmbeddedItem)
		{
			this.tnefCorrelationKey = tnefCorrelationKey;
			Charset itemWindowsCharset = ConvertUtils.GetItemWindowsCharset(this.item, options);
			this.tnefWriter = new ItemToTnefConverter.TnefContentWriter(mimeOut, itemWindowsCharset);
			this.propertyChecker = new TnefPropertyChecker(tnefType, parsingEmbeddedItem, options);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x00104A3D File Offset: 0x00102C3D
		internal ItemToTnefConverter(Item itemIn, OutboundAddressCache addressCache, ItemToTnefConverter.TnefContentWriter writer, OutboundConversionOptions options, ConversionLimitsTracker limits, TnefType tnefType, bool parsingEmbeddedItem) : this(itemIn, addressCache, options, limits, tnefType, parsingEmbeddedItem)
		{
			this.tnefWriter = writer;
			this.tnefCorrelationKey = null;
			this.propertyChecker = new TnefPropertyChecker(tnefType, parsingEmbeddedItem, options);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x00104A7C File Offset: 0x00102C7C
		private ItemToTnefConverter(Item itemIn, OutboundAddressCache addressCache, OutboundConversionOptions options, ConversionLimitsTracker limits, TnefType tnefType, bool parsingEmbeddedItem)
		{
			if (options.FilterAttachmentHandler != null)
			{
				throw new NotSupportedException("FilterAttachmentHandler is not supported in ItemToTnefConverter");
			}
			if (options.FilterBodyHandler != null)
			{
				throw new NotSupportedException("FilterBodyHandler is not supported in ItemToTnefConverter");
			}
			this.item = itemIn;
			this.addressCache = addressCache;
			this.options = options;
			this.limitsTracker = limits;
			this.isEmbeddedItem = parsingEmbeddedItem;
			this.tnefType = tnefType;
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x00104AE2 File Offset: 0x00102CE2
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ItemToTnefConverter>(this);
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x00104AEA File Offset: 0x00102CEA
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x00104AFF File Offset: 0x00102CFF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x00104B10 File Offset: 0x00102D10
		internal ConversionResult Convert()
		{
			ExTimeZone exTimeZone = this.item.PropertyBag.ExTimeZone;
			this.item.PropertyBag.ExTimeZone = ExTimeZone.UtcTimeZone;
			this.conversionResult = new ConversionResult();
			try
			{
				this.WriteMessageAttributes();
				this.WriteRecipientTable();
				this.WriteMapiProperties();
				this.WriteAttachments();
				this.tnefWriter.Flush();
			}
			finally
			{
				this.item.PropertyBag.ExTimeZone = exTimeZone;
			}
			return this.conversionResult;
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x00104B9C File Offset: 0x00102D9C
		private static bool IsCompleteParticipant(Participant participant)
		{
			return participant != null && participant.ValidationStatus == ParticipantValidationStatus.NoError && participant.RoutingType != null && participant.EmailAddress != null;
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x00104BC4 File Offset: 0x00102DC4
		private static HashSet<NativeStorePropertyDefinition> CreateExcludedPropertiesSet()
		{
			return new HashSet<NativeStorePropertyDefinition>
			{
				InternalSchema.HtmlBody,
				InternalSchema.TextBody,
				InternalSchema.RtfBody,
				InternalSchema.RtfSyncBodyCrc,
				InternalSchema.RtfSyncBodyCount,
				InternalSchema.RtfSyncBodyTag,
				InternalSchema.RtfSyncPrefixCount,
				InternalSchema.RtfSyncTrailingCount,
				InternalSchema.RtfInSync,
				InternalSchema.TnefCorrelationKey,
				InternalSchema.PredictedActionsInternal,
				InternalSchema.GroupingActionsDeprecated,
				InternalSchema.PredictedActionsSummaryDeprecated,
				InternalSchema.NeedGroupExpansion
			};
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x00104C74 File Offset: 0x00102E74
		private static HashSet<NativeStorePropertyDefinition> CreateBodyPropertiesSet()
		{
			return new HashSet<NativeStorePropertyDefinition>
			{
				InternalSchema.HtmlBody,
				InternalSchema.RtfBody
			};
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x00104CA0 File Offset: 0x00102EA0
		private void WriteMessageAttribute(TnefAttributeTag attribute, TnefPropertyTag tnefProperty, StorePropertyDefinition property)
		{
			object obj = this.item.TryGetProperty(property);
			PropertyError propertyError = obj as PropertyError;
			if (propertyError == null)
			{
				if (attribute == TnefAttributeTag.OriginalMessageClass || attribute == TnefAttributeTag.MessageClass)
				{
					string value = obj as string;
					if (string.IsNullOrEmpty(value))
					{
						return;
					}
				}
				this.tnefWriter.StartAttribute(attribute, TnefAttributeLevel.Message);
				this.tnefWriter.WriteProperty(tnefProperty, obj);
				return;
			}
			if (PropertyError.IsPropertyValueTooBig(propertyError))
			{
				try
				{
					using (Stream stream = this.item.OpenPropertyStream(property, PropertyOpenMode.ReadOnly))
					{
						using (Stream stream2 = this.tnefWriter.OpenAttributeStream(attribute, TnefAttributeLevel.Message))
						{
							Util.StreamHandler.CopyStreamData(stream, stream2);
						}
					}
				}
				catch (ObjectNotFoundException)
				{
				}
			}
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x00104D7C File Offset: 0x00102F7C
		private void WriteMessageAttributes()
		{
			this.WriteMessageAttribute(TnefAttributeTag.MessageId, TnefPropertyTag.SearchKey, InternalSchema.SearchKey);
			this.WriteMessageAttribute(TnefAttributeTag.Priority, TnefPropertyTag.Importance, InternalSchema.Importance);
			this.WriteMessageAttribute(TnefAttributeTag.DateSent, TnefPropertyTag.ClientSubmitTime, InternalSchema.SentTime);
			this.WriteMessageAttribute(TnefAttributeTag.DateModified, TnefPropertyTag.LastModificationTime, InternalSchema.LastModifiedTime);
			if (this.isEmbeddedItem)
			{
				this.WriteMessageAttribute(TnefAttributeTag.DateReceived, TnefPropertyTag.MessageDeliveryTime, InternalSchema.ReceivedTime);
				this.WriteMessageAttribute(TnefAttributeTag.MessageStatus, TnefPropertyTag.MessageFlags, InternalSchema.Flags);
			}
			if (this.tnefType == TnefType.LegacyTnef)
			{
				this.WriteMessageAttribute(TnefAttributeTag.MessageClass, TnefPropertyTag.MessageClassW, InternalSchema.ItemClass);
				this.WriteMessageAttribute(TnefAttributeTag.OriginalMessageClass, TnefPropertyTag.OrigMessageClassW, InternalSchema.OrigMessageClass);
				this.WriteMessageAttribute(TnefAttributeTag.Subject, TnefPropertyTag.SubjectA, InternalSchema.Subject);
				this.WriteMessageAttribute(TnefAttributeTag.ConversationId, TnefPropertyTag.ConversationKey, InternalSchema.ConversationKey);
				this.WriteMessageAttribute(TnefAttributeTag.DateStart, TnefPropertyTag.StartDate, InternalSchema.StartTime);
				this.WriteMessageAttribute(TnefAttributeTag.DateEnd, TnefPropertyTag.EndDate, InternalSchema.EndTime);
				this.WriteMessageAttribute(TnefAttributeTag.AidOwner, TnefPropertyTag.OwnerApptId, InternalSchema.OwnerAppointmentID);
				this.WriteMessageAttribute(TnefAttributeTag.RequestResponse, TnefPropertyTag.ResponseRequested, InternalSchema.IsResponseRequested);
				if (this.isEmbeddedItem)
				{
					this.WriteMessageAttribute(TnefAttributeTag.ParentId, TnefPropertyTag.ParentEntryId, InternalSchema.ParentEntryId);
					this.WriteTnefParticipant(TnefAttributeTag.From, ConversionItemParticipants.ParticipantIndex.Sender);
					this.WriteTnefParticipant(TnefAttributeTag.SentFor, ConversionItemParticipants.ParticipantIndex.From);
					this.WriteAttOwnerTnef();
				}
			}
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x00104EFE File Offset: 0x001030FE
		private void WriteAttOwnerTnef()
		{
			if (this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.ReceivedRepresenting] == null)
			{
				this.WriteTnefParticipant(TnefAttributeTag.Owner, ConversionItemParticipants.ParticipantIndex.From);
				return;
			}
			this.WriteTnefParticipant(TnefAttributeTag.Owner, ConversionItemParticipants.ParticipantIndex.ReceivedRepresenting);
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x00104F34 File Offset: 0x00103134
		private void WriteAttachmentAttributes(Attachment attachment)
		{
			this.tnefWriter.StartAttribute(TnefAttributeTag.AttachRenderData, TnefAttributeLevel.Attachment);
			object obj = attachment.TryGetProperty(InternalSchema.AttachMethod);
			if (obj is PropertyError)
			{
				StorageGlobals.ContextTraceError<string>(ExTraceGlobals.CcOutboundTnefTracer, "ItemToTnefConverter::WriteAttachmentAttributes: unable to get attach method, {0}.", ((PropertyError)obj).PropertyErrorDescription);
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent);
			}
			this.tnefWriter.WriteProperty(TnefPropertyTag.AttachMethod, (int)obj);
			obj = attachment.TryGetProperty(InternalSchema.RenderingPosition);
			if (obj is PropertyError)
			{
				obj = -1;
			}
			this.tnefWriter.WriteProperty(TnefPropertyTag.RenderingPosition, (int)obj);
			obj = attachment.TryGetProperty(InternalSchema.AttachEncoding);
			if (!(obj is PropertyError))
			{
				this.tnefWriter.WriteProperty(TnefPropertyTag.AttachEncoding, (byte[])obj);
			}
			this.tnefWriter.StartAttribute(TnefAttributeTag.AttachTitle, TnefAttributeLevel.Attachment);
			this.tnefWriter.WriteProperty(TnefPropertyTag.AttachFilenameA, attachment.DisplayName ?? string.Empty);
			this.tnefWriter.StartAttribute(TnefAttributeTag.AttachCreateDate, TnefAttributeLevel.Attachment);
			this.tnefWriter.WriteProperty(TnefPropertyTag.CreationTime, attachment.CreationTime);
			this.tnefWriter.StartAttribute(TnefAttributeTag.AttachModifyDate, TnefAttributeLevel.Attachment);
			this.tnefWriter.WriteProperty(TnefPropertyTag.LastModificationTime, attachment.LastModifiedTime);
			object obj2 = attachment.TryGetProperty(InternalSchema.AttachRendering);
			if (!(obj2 is PropertyError))
			{
				this.tnefWriter.StartAttribute(TnefAttributeTag.AttachMetaFile, TnefAttributeLevel.Attachment);
				this.tnefWriter.WriteProperty(TnefPropertyTag.AttachRendering, obj2);
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x001050C0 File Offset: 0x001032C0
		private bool WriteMessageProperty(NativeStorePropertyDefinition property)
		{
			long num;
			return this.WriteMessageProperty(property, out num);
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x001050D8 File Offset: 0x001032D8
		private bool WriteMessageProperty(NativeStorePropertyDefinition property, out long totalBytesRead)
		{
			totalBytesRead = 0L;
			object obj = this.item.PropertyBag.TryGetProperty(property);
			PropertyError propertyError = obj as PropertyError;
			if (propertyError == null)
			{
				this.tnefWriter.WriteProperty(property, obj);
				if (ItemToTnefConverter.bodyProperties.Contains(property))
				{
					byte[] array = obj as byte[];
					if (array != null)
					{
						totalBytesRead = (long)array.Length;
					}
				}
				return true;
			}
			return PropertyError.IsPropertyValueTooBig(propertyError) && this.WritePropertyStreamData(this.item.PropertyBag, property, out totalBytesRead);
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x00105150 File Offset: 0x00103350
		private bool WritePropertyStreamData(PersistablePropertyBag propertyBag, NativeStorePropertyDefinition property)
		{
			long num;
			return this.WritePropertyStreamData(propertyBag, property, out num);
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x00105168 File Offset: 0x00103368
		private bool WritePropertyStreamData(PersistablePropertyBag propertyBag, NativeStorePropertyDefinition property, out long totalBytesRead)
		{
			totalBytesRead = 0L;
			try
			{
				using (Stream stream = propertyBag.OpenPropertyStream(property, PropertyOpenMode.ReadOnly))
				{
					using (Stream stream2 = this.tnefWriter.StartStreamProperty(property))
					{
						totalBytesRead = Util.StreamHandler.CopyStreamData(stream, stream2);
					}
				}
			}
			catch (ObjectNotFoundException)
			{
			}
			return false;
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x00105284 File Offset: 0x00103484
		private void WriteMessageBody()
		{
			this.limitsTracker.CountMessageBody();
			BodyFormat rawFormat = this.item.Body.RawFormat;
			long bodySize = 0L;
			if (rawFormat == BodyFormat.TextHtml)
			{
				this.WriteMessageProperty(InternalSchema.HtmlBody, out bodySize);
				this.conversionResult.BodySize = bodySize;
				return;
			}
			this.WriteMessageProperty(InternalSchema.RtfSyncBodyCrc);
			this.WriteMessageProperty(InternalSchema.RtfSyncBodyCount);
			this.WriteMessageProperty(InternalSchema.RtfSyncBodyTag);
			this.WriteMessageProperty(InternalSchema.RtfInSync);
			this.WriteMessageProperty(InternalSchema.RtfSyncPrefixCount);
			this.WriteMessageProperty(InternalSchema.RtfSyncTrailingCount);
			if (rawFormat == BodyFormat.ApplicationRtf)
			{
				this.WriteMessageProperty(InternalSchema.RtfBody, out bodySize);
				this.conversionResult.BodySize = bodySize;
				return;
			}
			if (rawFormat == BodyFormat.TextPlain)
			{
				if (!this.item.Body.IsBodyDefined)
				{
					return;
				}
				using (Stream textStream = this.item.OpenPropertyStream(InternalSchema.TextBody, PropertyOpenMode.ReadOnly))
				{
					if (this.tnefType == TnefType.SummaryTnef)
					{
						using (Stream stream = this.tnefWriter.StartStreamProperty(InternalSchema.TextBody))
						{
							this.conversionResult.BodySize = Util.StreamHandler.CopyStreamData(textStream, stream);
						}
						textStream.Position = 0L;
					}
					using (Stream tnefStream = this.tnefWriter.StartStreamProperty(InternalSchema.RtfBody))
					{
						int inCodePage = this.item.Body.RawCharset.CodePage;
						ConvertUtils.CallCts(ExTraceGlobals.CcOutboundTnefTracer, "ItemToTnefConverter::WriteMessageBody", ServerStrings.ConversionBodyConversionFailed, delegate
						{
							TextToRtf textToRtf = new TextToRtf();
							textToRtf.InputEncoding = Charset.GetEncoding(inCodePage);
							using (Stream stream2 = new ConverterStream(textStream, textToRtf, ConverterStreamAccess.Read))
							{
								using (ConverterStream converterStream = new ConverterStream(stream2, new RtfToRtfCompressed(), ConverterStreamAccess.Read))
								{
									Util.StreamHandler.CopyStreamData(converterStream, tnefStream);
								}
							}
						});
					}
				}
			}
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x00105484 File Offset: 0x00103684
		private void WriteTnefParticipant(TnefAttributeTag legacyTnefAttribute, ConversionItemParticipants.ParticipantIndex index)
		{
			Participant participant = this.addressCache.Participants[index];
			if (ItemToTnefConverter.IsCompleteParticipant(participant))
			{
				this.tnefWriter.StartAttribute(legacyTnefAttribute, TnefAttributeLevel.Message);
				EmbeddedParticipantProperty embeddedParticipantProperty = ConversionItemParticipants.GetEmbeddedParticipantProperty(index);
				this.tnefWriter.WriteProperty(embeddedParticipantProperty.EmailAddressPropertyDefinition, participant.EmailAddress);
				this.tnefWriter.WriteProperty(embeddedParticipantProperty.RoutingTypePropertyDefinition, participant.RoutingType);
				this.tnefWriter.WriteProperty(embeddedParticipantProperty.DisplayNamePropertyDefinition, string.IsNullOrEmpty(participant.DisplayName) ? participant.EmailAddress : participant.DisplayName);
			}
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x0010551C File Offset: 0x0010371C
		private void WriteTnefCorrelationKeyProperty()
		{
			if (this.tnefCorrelationKey != null)
			{
				byte[] array = new byte[this.tnefCorrelationKey.Length + 1];
				CTSGlobals.AsciiEncoding.GetBytes(this.tnefCorrelationKey, 0, this.tnefCorrelationKey.Length, array, 0);
				this.tnefWriter.WriteProperty(TnefPropertyTag.TnefCorrelationKey, array);
			}
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x00105574 File Offset: 0x00103774
		private void WriteMapiProperties()
		{
			this.tnefWriter.StartAttribute(TnefAttributeTag.MapiProperties, TnefAttributeLevel.Message);
			this.WriteTnefCorrelationKeyProperty();
			this.WriteMessageBody();
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in this.addressCache.Properties.AllNativeProperties)
			{
				object obj = this.addressCache.Properties.TryGetProperty(nativeStorePropertyDefinition);
				if (obj != null && !(obj is PropertyError))
				{
					this.tnefWriter.WriteProperty(nativeStorePropertyDefinition, obj);
				}
			}
			foreach (NativeStorePropertyDefinition property in this.item.AllNativeProperties)
			{
				if (!ItemToTnefConverter.excludedMapiProperties.Contains(property) && !ConversionAddressCache.IsAnyCacheProperty(property) && this.propertyChecker.IsItemPropertyWritable(property))
				{
					this.WriteMessageProperty(property);
				}
			}
			if (!this.isEmbeddedItem && ObjectClass.IsMdn(this.item.ClassName) && this.item.Session is MailboxSession)
			{
				this.AppendTimeZoneInfo();
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x001056A8 File Offset: 0x001038A8
		private void AppendTimeZoneInfo()
		{
			MailboxSession mailboxSession = this.item.Session as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			ExTimeZone timeZone;
			byte[] timeZoneBlob;
			if (TimeZoneSettings.TryFindOwaTimeZone(mailboxSession, out timeZone))
			{
				timeZoneBlob = O12TimeZoneFormatter.GetTimeZoneBlob(timeZone);
				this.tnefWriter.WriteProperty(InternalSchema.TimeZoneDefinitionStart, timeZoneBlob);
				return;
			}
			if (TimeZoneSettings.TryFindOutlookTimeZone(mailboxSession, out timeZoneBlob))
			{
				this.tnefWriter.WriteProperty(InternalSchema.TimeZoneDefinitionStart, timeZoneBlob);
			}
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x00105708 File Offset: 0x00103908
		private long WriteAttachDataObj(Attachment attachment)
		{
			OleAttachment oleAttachment = attachment as OleAttachment;
			long result = 0L;
			if (oleAttachment != null)
			{
				byte[] array = MimeConstants.IID_IStorage.ToByteArray();
				using (Stream stream = this.tnefWriter.StartStreamProperty(InternalSchema.AttachDataObj))
				{
					this.tnefWriter.StreamPropertyData(array, 0, array.Length);
					using (Stream contentStream = oleAttachment.GetContentStream(PropertyOpenMode.ReadOnly))
					{
						result = Util.StreamHandler.CopyStreamData(contentStream, stream);
					}
				}
			}
			return result;
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x0010579C File Offset: 0x0010399C
		private long WriteAttachmentProperties(Attachment attachment)
		{
			long result = 0L;
			this.tnefWriter.StartAttribute(TnefAttributeTag.Attachment, TnefAttributeLevel.Attachment);
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in attachment.AllNativeProperties)
			{
				if (nativeStorePropertyDefinition.Equals(InternalSchema.AttachDataObj))
				{
					result = this.WriteAttachDataObj(attachment);
				}
				else if (!nativeStorePropertyDefinition.Equals(InternalSchema.AttachDataBin) && this.propertyChecker.IsAttachmentPropertyWritable(nativeStorePropertyDefinition))
				{
					object obj = attachment.TryGetProperty(nativeStorePropertyDefinition);
					PropertyError propertyError = obj as PropertyError;
					if (propertyError != null && PropertyError.IsPropertyValueTooBig(propertyError))
					{
						this.WritePropertyStreamData(attachment.PropertyBag, nativeStorePropertyDefinition);
					}
					else if (propertyError == null)
					{
						obj = ExTimeZoneHelperForMigrationOnly.ToLegacyUtcIfDateTime(obj);
						this.tnefWriter.WriteProperty(nativeStorePropertyDefinition, obj);
					}
				}
			}
			ItemAttachment itemAttachment = attachment as ItemAttachment;
			if (itemAttachment != null)
			{
				result = this.WriteAttachedItem(itemAttachment);
			}
			return result;
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x00105888 File Offset: 0x00103A88
		private long WriteAttachedItem(ItemAttachment attachment)
		{
			Item item = ConvertUtils.OpenAttachedItem(attachment);
			long result = 0L;
			if (item != null)
			{
				try
				{
					this.limitsTracker.StartEmbeddedMessage();
					Charset itemWindowsCharset = ConvertUtils.GetItemWindowsCharset(item, this.options);
					using (ItemToTnefConverter.TnefContentWriter embeddedMessageWriter = this.tnefWriter.GetEmbeddedMessageWriter(itemWindowsCharset))
					{
						OutboundAddressCache outboundAddressCache = new OutboundAddressCache(this.options, this.limitsTracker);
						outboundAddressCache.CopyDataFromItem(item);
						if (this.tnefType == TnefType.LegacyTnef && this.options.ResolveRecipientsInAttachedMessages)
						{
							outboundAddressCache.Resolve();
						}
						using (ItemToTnefConverter itemToTnefConverter = new ItemToTnefConverter(item, outboundAddressCache, embeddedMessageWriter, this.options, this.limitsTracker, this.tnefType, true))
						{
							ConversionResult conversionResult = itemToTnefConverter.Convert();
							result = conversionResult.BodySize + conversionResult.AccumulatedAttachmentSize;
						}
					}
					this.limitsTracker.EndEmbeddedMessage();
				}
				finally
				{
					item.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x00105990 File Offset: 0x00103B90
		private long WriteAttachment(Item item, Attachment attachment)
		{
			this.limitsTracker.CountMessageAttachment();
			this.WriteAttachmentAttributes(attachment);
			long result = 0L;
			StreamAttachment streamAttachment = attachment as StreamAttachment;
			if (streamAttachment != null)
			{
				using (Stream stream = this.tnefWriter.OpenAttributeStream(TnefAttributeTag.AttachData, TnefAttributeLevel.Attachment))
				{
					object obj = attachment.TryGetProperty(InternalSchema.AttachDataBin);
					PropertyError propertyError = obj as PropertyError;
					if (propertyError != null)
					{
						using (Stream stream2 = streamAttachment.TryGetRawContentStream(PropertyOpenMode.ReadOnly))
						{
							if (stream2 != null)
							{
								result = Util.StreamHandler.CopyStreamData(stream2, stream);
							}
							goto IL_88;
						}
					}
					byte[] array = (byte[])obj;
					result = (long)array.Length;
					stream.Write(array, 0, array.Length);
					IL_88:;
				}
			}
			this.WriteAttachmentProperties(attachment);
			return result;
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x00105A58 File Offset: 0x00103C58
		private void WriteAttachments()
		{
			if (this.item.AttachmentCollection != null)
			{
				int num = 0;
				long num2 = 0L;
				this.item.CoreItem.OpenAttachmentCollection();
				foreach (AttachmentHandle handle in this.item.CoreItem.AttachmentCollection)
				{
					num++;
					using (Attachment attachment = this.item.AttachmentCollection.Open(handle, InternalSchema.ContentConversionProperties))
					{
						using (StorageGlobals.SetTraceContext(attachment))
						{
							StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Starting attachment (ItemToTnefConverter.WriteAttachments)");
							num2 += this.WriteAttachment(this.item, attachment);
							StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing attachment (ItemToTnefConverter.WriteAttachments)");
						}
					}
				}
				this.conversionResult.AttachmentCount = num;
				this.conversionResult.AccumulatedAttachmentSize = num2;
			}
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x00105B70 File Offset: 0x00103D70
		private void WriteRecipient(ConversionRecipientEntry recipient)
		{
			if (recipient.Participant == null)
			{
				return;
			}
			RecipientItemType recipientItemType = recipient.RecipientItemType;
			if (!ConvertUtils.IsRecipientTransmittable(recipientItemType) && this.tnefType != TnefType.SummaryTnef)
			{
				return;
			}
			this.tnefWriter.StartRow();
			this.tnefWriter.WriteProperty(TnefPropertyTag.RecipientType, (int)MapiUtil.RecipientItemTypeToMapiRecipientType(recipientItemType, false));
			Participant participant = recipient.Participant;
			this.tnefWriter.WriteProperty(TnefPropertyTag.DisplayNameW, participant.DisplayName ?? string.Empty);
			this.tnefWriter.WriteProperty(TnefPropertyTag.EmailAddressW, participant.EmailAddress ?? string.Empty);
			this.tnefWriter.WriteProperty(TnefPropertyTag.AddrtypeW, participant.RoutingType ?? string.Empty);
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in recipient.AllNativeProperties)
			{
				if (nativeStorePropertyDefinition != null && nativeStorePropertyDefinition != InternalSchema.RecipientType && nativeStorePropertyDefinition != InternalSchema.DisplayName && nativeStorePropertyDefinition != InternalSchema.EmailAddress && nativeStorePropertyDefinition != InternalSchema.AddrType && this.propertyChecker.IsRecipientPropertyWritable(nativeStorePropertyDefinition))
				{
					this.WriteRecipientProperty(recipient, nativeStorePropertyDefinition);
				}
			}
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x00105CA4 File Offset: 0x00103EA4
		private bool WriteRecipientProperty(ConversionRecipientEntry recipient, NativeStorePropertyDefinition property)
		{
			object obj = recipient.TryGetProperty(property);
			if (!PropertyError.IsPropertyError(obj))
			{
				obj = ExTimeZoneHelperForMigrationOnly.ToLegacyUtcIfDateTime(obj);
				this.tnefWriter.WriteProperty(property, obj);
				return true;
			}
			return false;
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x00105CD8 File Offset: 0x00103ED8
		private void WriteRecipientTable()
		{
			if (this.tnefType != TnefType.LegacyTnef || this.isEmbeddedItem)
			{
				this.tnefWriter.StartAttribute(TnefAttributeTag.RecipientTable, TnefAttributeLevel.Message);
				List<ConversionRecipientEntry> recipients = this.addressCache.Recipients;
				if (recipients != null)
				{
					this.conversionResult.RecipientCount = recipients.Count;
					foreach (ConversionRecipientEntry conversionRecipientEntry in recipients)
					{
						if (!(this.item is MessageItem) || this.options.DemoteBcc || conversionRecipientEntry.RecipientItemType != RecipientItemType.Bcc || ((conversionRecipientEntry.Participant.GetValueOrDefault<bool>(ParticipantSchema.IsRoom, false) || conversionRecipientEntry.Participant.GetValueOrDefault<bool>(ParticipantSchema.IsResource, false)) && ObjectClass.IsMeetingMessage(this.item.ClassName)))
						{
							this.WriteRecipient(conversionRecipientEntry);
						}
					}
				}
			}
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x00105DCC File Offset: 0x00103FCC
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.tnefWriter != null)
				{
					this.tnefWriter.Dispose();
					this.tnefWriter = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x04002184 RID: 8580
		private const int TnefVersionValue = 65536;

		// Token: 0x04002185 RID: 8581
		private const int Attach_OLE = 6;

		// Token: 0x04002186 RID: 8582
		private static readonly HashSet<NativeStorePropertyDefinition> excludedMapiProperties = ItemToTnefConverter.CreateExcludedPropertiesSet();

		// Token: 0x04002187 RID: 8583
		private static readonly HashSet<NativeStorePropertyDefinition> bodyProperties = ItemToTnefConverter.CreateBodyPropertiesSet();

		// Token: 0x04002188 RID: 8584
		private Item item;

		// Token: 0x04002189 RID: 8585
		private ItemToTnefConverter.TnefContentWriter tnefWriter;

		// Token: 0x0400218A RID: 8586
		private OutboundConversionOptions options;

		// Token: 0x0400218B RID: 8587
		private ConversionLimitsTracker limitsTracker;

		// Token: 0x0400218C RID: 8588
		private TnefType tnefType;

		// Token: 0x0400218D RID: 8589
		private bool isEmbeddedItem;

		// Token: 0x0400218E RID: 8590
		private string tnefCorrelationKey;

		// Token: 0x0400218F RID: 8591
		private OutboundAddressCache addressCache;

		// Token: 0x04002190 RID: 8592
		private TnefPropertyChecker propertyChecker;

		// Token: 0x04002191 RID: 8593
		private DisposeTracker disposeTracker;

		// Token: 0x04002192 RID: 8594
		private ConversionResult conversionResult;

		// Token: 0x020005F7 RID: 1527
		internal class TnefContentWriterPropertyStream : StreamBase
		{
			// Token: 0x06003EDD RID: 16093 RVA: 0x00105E14 File Offset: 0x00104014
			internal TnefContentWriterPropertyStream(ItemToTnefConverter.TnefContentWriter writer) : base(StreamBase.Capabilities.Writable)
			{
				this.writer = writer;
			}

			// Token: 0x06003EDE RID: 16094 RVA: 0x00105E24 File Offset: 0x00104024
			public override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<ItemToTnefConverter.TnefContentWriterPropertyStream>(this);
			}

			// Token: 0x06003EDF RID: 16095 RVA: 0x00105E2C File Offset: 0x0010402C
			public override void Write(byte[] buffer, int offset, int count)
			{
				base.CheckDisposed("Write");
				this.writer.StreamPropertyData(buffer, offset, count);
			}

			// Token: 0x04002193 RID: 8595
			private ItemToTnefConverter.TnefContentWriter writer;
		}

		// Token: 0x020005F8 RID: 1528
		internal class TnefContentWriterAttributeStream : StreamBase
		{
			// Token: 0x06003EE0 RID: 16096 RVA: 0x00105E47 File Offset: 0x00104047
			internal TnefContentWriterAttributeStream(ItemToTnefConverter.TnefContentWriter writer) : base(StreamBase.Capabilities.Writable)
			{
				this.writer = writer;
			}

			// Token: 0x06003EE1 RID: 16097 RVA: 0x00105E57 File Offset: 0x00104057
			public override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<ItemToTnefConverter.TnefContentWriterAttributeStream>(this);
			}

			// Token: 0x06003EE2 RID: 16098 RVA: 0x00105E5F File Offset: 0x0010405F
			public override void Write(byte[] buffer, int offset, int count)
			{
				base.CheckDisposed("Write");
				this.writer.StreamAttributeData(buffer, offset, count);
			}

			// Token: 0x04002194 RID: 8596
			private ItemToTnefConverter.TnefContentWriter writer;
		}

		// Token: 0x020005F9 RID: 1529
		internal class TnefContentWriter : IDisposeTrackable, IDisposable
		{
			// Token: 0x06003EE3 RID: 16099 RVA: 0x00105E7A File Offset: 0x0010407A
			internal TnefContentWriter(Stream outStream, Charset charset) : this(ItemToTnefConverter.TnefContentWriter.CreateTnefWriter(outStream, charset), charset)
			{
			}

			// Token: 0x06003EE4 RID: 16100 RVA: 0x00105E8C File Offset: 0x0010408C
			private TnefContentWriter(TnefWriter writer, Charset charset)
			{
				this.tnefWriter = writer;
				this.charset = charset;
				this.isNewRow = false;
				this.newTnefAttribute = TnefAttributeTag.Null;
				this.currentTnefAttribute = TnefAttributeTag.Null;
				this.newTnefAttributeLevel = TnefAttributeLevel.Message;
				this.streamProperty = null;
				this.tnefWriter.SetMessageCodePage(charset.CodePage);
				this.disposeTracker = this.GetDisposeTracker();
			}

			// Token: 0x06003EE5 RID: 16101 RVA: 0x00105EED File Offset: 0x001040ED
			public virtual DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<ItemToTnefConverter.TnefContentWriter>(this);
			}

			// Token: 0x06003EE6 RID: 16102 RVA: 0x00105EF5 File Offset: 0x001040F5
			public void SuppressDisposeTracker()
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Suppress();
				}
			}

			// Token: 0x06003EE7 RID: 16103 RVA: 0x00105F0A File Offset: 0x0010410A
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06003EE8 RID: 16104 RVA: 0x00105F19 File Offset: 0x00104119
			internal void StartAttribute(TnefAttributeTag tag, TnefAttributeLevel level)
			{
				this.newTnefAttribute = tag;
				this.newTnefAttributeLevel = level;
			}

			// Token: 0x06003EE9 RID: 16105 RVA: 0x00105F29 File Offset: 0x00104129
			internal Stream OpenAttributeStream(TnefAttributeTag tag, TnefAttributeLevel level)
			{
				this.StartAttribute(tag, level);
				return new ItemToTnefConverter.TnefContentWriterAttributeStream(this);
			}

			// Token: 0x06003EEA RID: 16106 RVA: 0x00105F39 File Offset: 0x00104139
			internal void StartRow()
			{
				this.isNewRow = true;
			}

			// Token: 0x06003EEB RID: 16107 RVA: 0x00105F42 File Offset: 0x00104142
			internal Stream StartStreamProperty(NativeStorePropertyDefinition property)
			{
				this.streamProperty = property;
				return new ItemToTnefConverter.TnefContentWriterPropertyStream(this);
			}

			// Token: 0x06003EEC RID: 16108 RVA: 0x00105F54 File Offset: 0x00104154
			internal void WriteProperty(NativeStorePropertyDefinition property, object propertyValue)
			{
				if (propertyValue == null)
				{
					return;
				}
				TnefPropertyTag propertyTag = this.StartProperty(property);
				if (this.IsMultivalued(property.MapiPropertyType))
				{
					using (IEnumerator enumerator = ((Array)propertyValue).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object propertyValue2 = enumerator.Current;
							this.WriteTnefWriterPropertyValue(propertyTag, propertyValue2);
						}
						return;
					}
				}
				this.WriteTnefWriterPropertyValue(propertyTag, propertyValue);
			}

			// Token: 0x06003EED RID: 16109 RVA: 0x00105FCC File Offset: 0x001041CC
			internal void WriteProperty(TnefPropertyTag property, object propertyValue)
			{
				if (propertyValue == null)
				{
					return;
				}
				this.StartProperty(property);
				if (this.IsMultivalued(property))
				{
					using (IEnumerator enumerator = ((Array)propertyValue).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object propertyValue2 = enumerator.Current;
							this.WriteTnefWriterPropertyValue(property, propertyValue2);
						}
						return;
					}
				}
				this.WriteTnefWriterPropertyValue(property, propertyValue);
			}

			// Token: 0x06003EEE RID: 16110 RVA: 0x00106040 File Offset: 0x00104240
			internal void StreamPropertyData(byte[] data, int offset, int length)
			{
				if (this.streamProperty != null)
				{
					this.StartProperty(this.streamProperty);
					this.streamProperty = null;
				}
				this.tnefWriter.WritePropertyRawValue(data, offset, length);
			}

			// Token: 0x06003EEF RID: 16111 RVA: 0x0010606C File Offset: 0x0010426C
			internal void StreamAttributeData(byte[] data, int offset, int length)
			{
				this.CheckStartTnefAttribute();
				this.tnefWriter.WriteAttributeRawValue(data, offset, length);
			}

			// Token: 0x06003EF0 RID: 16112 RVA: 0x00106082 File Offset: 0x00104282
			internal void Flush()
			{
				this.tnefWriter.Flush();
			}

			// Token: 0x06003EF1 RID: 16113 RVA: 0x00106090 File Offset: 0x00104290
			internal ItemToTnefConverter.TnefContentWriter GetEmbeddedMessageWriter(Charset charset)
			{
				this.tnefWriter.StartProperty(TnefPropertyTag.AttachDataObj);
				TnefWriter embeddedMessageWriter = this.tnefWriter.GetEmbeddedMessageWriter();
				ItemToTnefConverter.TnefContentWriter.InitializeWriter(embeddedMessageWriter, charset);
				return new ItemToTnefConverter.TnefContentWriter(embeddedMessageWriter, this.charset);
			}

			// Token: 0x06003EF2 RID: 16114 RVA: 0x001060CC File Offset: 0x001042CC
			private static TnefWriter CreateTnefWriter(Stream outStream, Charset charset)
			{
				Random random = new Random((int)ExDateTime.UtcNow.UtcTicks);
				TnefWriter tnefWriter = new TnefWriter(outStream, (short)random.Next(32767));
				ItemToTnefConverter.TnefContentWriter.InitializeWriter(tnefWriter, charset);
				return tnefWriter;
			}

			// Token: 0x06003EF3 RID: 16115 RVA: 0x00106108 File Offset: 0x00104308
			private static void InitializeWriter(TnefWriter writer, Charset charset)
			{
				writer.WriteTnefVersion();
				try
				{
					writer.WriteOemCodePage(charset.CodePage);
				}
				catch (ArgumentException innerException)
				{
					StorageGlobals.ContextTraceError<int>(ExTraceGlobals.CcOutboundTnefTracer, "TnefContentWriter::InitializeWriter: invalid codepage, {0}.", charset.CodePage);
					throw new ConversionFailedException(ConversionFailureReason.CorruptContent, innerException);
				}
			}

			// Token: 0x06003EF4 RID: 16116 RVA: 0x00106158 File Offset: 0x00104358
			private static TnefPropertyTag GetTnefPropertyTag(PropertyTagPropertyDefinition property)
			{
				return (int)property.PropertyTag;
			}

			// Token: 0x06003EF5 RID: 16117 RVA: 0x00106165 File Offset: 0x00104365
			private static TnefPropertyTag GetTnefPropertyTag(GuidNamePropertyDefinition property)
			{
				return (int)((PropType)(-2147483648) | property.MapiPropertyType);
			}

			// Token: 0x06003EF6 RID: 16118 RVA: 0x00106178 File Offset: 0x00104378
			private static TnefPropertyTag GetTnefPropertyTag(GuidIdPropertyDefinition property)
			{
				return (int)((PropType)(-2147483648) | property.MapiPropertyType);
			}

			// Token: 0x06003EF7 RID: 16119 RVA: 0x0010618B File Offset: 0x0010438B
			private void StartProperty(TnefPropertyTag tag)
			{
				this.CheckStartTnefAttribute();
				this.streamProperty = null;
				this.tnefWriter.StartProperty(tag);
			}

			// Token: 0x06003EF8 RID: 16120 RVA: 0x001061A8 File Offset: 0x001043A8
			private TnefPropertyTag StartProperty(NativeStorePropertyDefinition property)
			{
				this.CheckStartTnefAttribute();
				this.streamProperty = null;
				TnefPropertyTag tnefPropertyTag;
				switch (property.SpecifiedWith)
				{
				case PropertyTypeSpecifier.PropertyTag:
					tnefPropertyTag = ItemToTnefConverter.TnefContentWriter.GetTnefPropertyTag((PropertyTagPropertyDefinition)property);
					this.tnefWriter.StartProperty(tnefPropertyTag);
					break;
				case PropertyTypeSpecifier.GuidString:
				{
					GuidNamePropertyDefinition guidNamePropertyDefinition = (GuidNamePropertyDefinition)property;
					tnefPropertyTag = ItemToTnefConverter.TnefContentWriter.GetTnefPropertyTag(guidNamePropertyDefinition);
					this.tnefWriter.StartProperty(tnefPropertyTag, guidNamePropertyDefinition.Guid, guidNamePropertyDefinition.PropertyName);
					break;
				}
				case PropertyTypeSpecifier.GuidId:
				{
					GuidIdPropertyDefinition guidIdPropertyDefinition = (GuidIdPropertyDefinition)property;
					tnefPropertyTag = ItemToTnefConverter.TnefContentWriter.GetTnefPropertyTag(guidIdPropertyDefinition);
					this.tnefWriter.StartProperty(tnefPropertyTag, guidIdPropertyDefinition.Guid, guidIdPropertyDefinition.Id);
					break;
				}
				default:
					throw new InvalidOperationException(string.Format("Invalid native property specifier: {0}", property.SpecifiedWith));
				}
				return tnefPropertyTag;
			}

			// Token: 0x06003EF9 RID: 16121 RVA: 0x00106268 File Offset: 0x00104468
			private void WriteTnefWriterPropertyValue(TnefPropertyTag propertyTag, object propertyValue)
			{
				TnefPropertyType tnefType = propertyTag.TnefType;
				if (tnefType == TnefPropertyType.AppTime || tnefType == (TnefPropertyType)4103)
				{
					DateTime dateTime = (DateTime)Util.Date1601Utc.ToUtc();
					try
					{
						dateTime = ConvertUtils.GetDateTimeFromOADate((double)propertyValue);
					}
					catch (ArgumentException arg)
					{
						StorageGlobals.ContextTraceError<double, ArgumentException>(ExTraceGlobals.CcOutboundTnefTracer, "TnefContentWriter::WriteTnefWriterPropertyValue: ArgumentException processing date {0}, {1}.", (double)propertyValue, arg);
					}
					propertyValue = dateTime;
				}
				propertyValue = ExTimeZoneHelperForMigrationOnly.ToLegacyUtcIfDateTime(propertyValue);
				if (propertyValue is DateTime)
				{
					DateTime dateTime2 = (DateTime)propertyValue;
					if ((ExDateTime)dateTime2 < Util.Date1601Utc)
					{
						propertyValue = (DateTime)Util.Date1601Utc;
					}
				}
				this.tnefWriter.WritePropertyValue(propertyValue);
			}

			// Token: 0x06003EFA RID: 16122 RVA: 0x00106324 File Offset: 0x00104524
			private void CheckStartTnefAttribute()
			{
				if (this.newTnefAttribute != TnefAttributeTag.Null && this.newTnefAttribute != this.currentTnefAttribute)
				{
					this.tnefWriter.StartAttribute(this.newTnefAttribute, this.newTnefAttributeLevel);
					this.currentTnefAttribute = this.newTnefAttribute;
				}
				if (this.isNewRow)
				{
					this.tnefWriter.StartRow();
					this.isNewRow = false;
				}
			}

			// Token: 0x06003EFB RID: 16123 RVA: 0x00106384 File Offset: 0x00104584
			private bool IsMultivalued(PropType propType)
			{
				return ((long)propType & 4096L) != 0L;
			}

			// Token: 0x06003EFC RID: 16124 RVA: 0x00106396 File Offset: 0x00104596
			private bool IsMultivalued(TnefPropertyTag propTag)
			{
				return ((long)propTag & 4096L) != 0L;
			}

			// Token: 0x06003EFD RID: 16125 RVA: 0x001063AD File Offset: 0x001045AD
			private void Dispose(bool disposing)
			{
				if (disposing)
				{
					if (this.tnefWriter != null)
					{
						this.tnefWriter.Close();
						this.tnefWriter = null;
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
				}
			}

			// Token: 0x04002195 RID: 8597
			private readonly DisposeTracker disposeTracker;

			// Token: 0x04002196 RID: 8598
			private Charset charset;

			// Token: 0x04002197 RID: 8599
			private TnefWriter tnefWriter;

			// Token: 0x04002198 RID: 8600
			private TnefAttributeTag newTnefAttribute;

			// Token: 0x04002199 RID: 8601
			private TnefAttributeLevel newTnefAttributeLevel;

			// Token: 0x0400219A RID: 8602
			private TnefAttributeTag currentTnefAttribute;

			// Token: 0x0400219B RID: 8603
			private NativeStorePropertyDefinition streamProperty;

			// Token: 0x0400219C RID: 8604
			private bool isNewRow;
		}
	}
}
