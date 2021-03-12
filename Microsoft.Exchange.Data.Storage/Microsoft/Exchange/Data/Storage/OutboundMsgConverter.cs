using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.MsgStorage.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200060C RID: 1548
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OutboundMsgConverter
	{
		// Token: 0x06003FA7 RID: 16295 RVA: 0x001088B3 File Offset: 0x00106AB3
		internal OutboundMsgConverter(OutboundConversionOptions options)
		{
			this.options = options;
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x001088C2 File Offset: 0x00106AC2
		private MsgStorageWriter PropertyWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x001088CC File Offset: 0x00106ACC
		internal void ConvertItemToMsgStorage(Item item, Stream outStream)
		{
			MsgStorageWriter msgStorageWriter = null;
			try
			{
				msgStorageWriter = new MsgStorageWriter(outStream);
				ConversionLimitsTracker conversionLimitsTracker = new ConversionLimitsTracker(this.options.Limits);
				conversionLimitsTracker.CountMessageBody();
				this.InternalConvertItemToMsgStorage(item, msgStorageWriter, conversionLimitsTracker);
			}
			finally
			{
				if (msgStorageWriter != null)
				{
					msgStorageWriter.Dispose();
				}
			}
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x00108920 File Offset: 0x00106B20
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
				InternalSchema.StoreSupportMask,
				InternalSchema.AttachNum,
				InternalSchema.ObjectType,
				InternalSchema.EntryId,
				InternalSchema.RecordKey,
				InternalSchema.StoreEntryId,
				InternalSchema.StoreRecordKey,
				InternalSchema.ParentEntryId,
				InternalSchema.SourceKey,
				InternalSchema.CreatorEntryId,
				InternalSchema.LastModifierEntryId,
				InternalSchema.MdbProvider,
				InternalSchema.MappingSignature,
				InternalSchema.UrlCompName,
				InternalSchema.UrlCompNamePostfix,
				InternalSchema.MID,
				InternalSchema.Associated,
				InternalSchema.Size,
				InternalSchema.SentMailSvrEId,
				InternalSchema.SentMailEntryId,
				InternalSchema.AttachSize
			};
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x00108A8C File Offset: 0x00106C8C
		private static HashSet<NativeStorePropertyDefinition> CreateRecipientExcludedPropertiesSet()
		{
			return new HashSet<NativeStorePropertyDefinition>
			{
				InternalSchema.BusinessPhoneNumber,
				InternalSchema.OfficeLocation,
				InternalSchema.MobilePhone,
				InternalSchema.RowId
			};
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x00108ACC File Offset: 0x00106CCC
		private void InternalConvertItemToMsgStorage(Item item, MsgStorageWriter writer, ConversionLimitsTracker limitsTracker)
		{
			this.item = item;
			this.writer = writer;
			this.limitsTracker = limitsTracker;
			this.addressCache = new OutboundAddressCache(this.options, limitsTracker);
			ExTimeZone exTimeZone = this.item.PropertyBag.ExTimeZone;
			this.item.PropertyBag.ExTimeZone = ExTimeZone.UtcTimeZone;
			try
			{
				this.addressCache.CopyDataFromItem(this.item);
				this.WriteMessageProperties();
				this.WriteRecipientTable();
				this.WriteAttachments();
				this.writer.Flush();
			}
			finally
			{
				this.item.PropertyBag.ExTimeZone = exTimeZone;
			}
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x00108C58 File Offset: 0x00106E58
		private void WriteMessageProperties()
		{
			this.WriteProperty(InternalSchema.StoreSupportMask, 265849);
			this.WritePropertyIfMissing(InternalSchema.MapiHasAttachment, this.item.PropertyBag, delegate
			{
				foreach (AttachmentHandle attachmentHandle in this.item.AttachmentCollection)
				{
					using (Attachment attachment = this.item.AttachmentCollection.Open(attachmentHandle, null))
					{
						if (!attachment.IsInline || attachmentHandle.AttachMethod == 7)
						{
							return true;
						}
					}
				}
				return false;
			});
			this.WritePropertyIfMissing(InternalSchema.DisplayToInternal, this.item.PropertyBag, () => this.item.PropertyBag.TryGetProperty(InternalSchema.DisplayTo));
			this.WritePropertyIfMissing(InternalSchema.DisplayCcInternal, this.item.PropertyBag, () => this.item.PropertyBag.TryGetProperty(InternalSchema.DisplayCc));
			this.WritePropertyIfMissing(InternalSchema.DisplayBccInternal, this.item.PropertyBag, () => this.item.PropertyBag.TryGetProperty(InternalSchema.DisplayBcc));
			this.WriteMessageBody();
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in this.addressCache.Properties.AllNativeProperties)
			{
				object obj = this.addressCache.Properties.TryGetProperty(nativeStorePropertyDefinition);
				if (obj != null && !(obj is PropertyError))
				{
					this.WriteProperty(nativeStorePropertyDefinition, obj);
				}
			}
			foreach (NativeStorePropertyDefinition property in this.item.PropertyBag.AllNativeProperties)
			{
				if (!OutboundMsgConverter.excludedPropertySet.Contains(property) && !ConversionAddressCache.IsAnyCacheProperty(property))
				{
					this.WriteProperty(property, this.item.PropertyBag);
				}
			}
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x00108DDC File Offset: 0x00106FDC
		private void WriteProperty(NativeStorePropertyDefinition property, PersistablePropertyBag propertyBag)
		{
			object obj = propertyBag.TryGetProperty(property);
			PropertyError propertyError = obj as PropertyError;
			if (propertyError == null)
			{
				this.WriteProperty(property, obj);
				return;
			}
			if (PropertyError.IsPropertyValueTooBig(propertyError))
			{
				this.StreamProperty(property, propertyBag);
			}
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x00108E14 File Offset: 0x00107014
		private void WritePropertyIfMissing(NativeStorePropertyDefinition property, PersistablePropertyBag propertyBag, OutboundMsgConverter.ComputeValueDelegate computeValue)
		{
			PropertyError propertyError = ((IDirectPropertyBag)propertyBag).GetValue(property) as PropertyError;
			if (propertyError != null && !PropertyError.IsPropertyValueTooBig(propertyError))
			{
				object obj = computeValue();
				if (!(obj is PropertyError))
				{
					this.WriteProperty(property, obj);
				}
			}
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x00108E50 File Offset: 0x00107050
		private void WriteProperty(NativeStorePropertyDefinition property, object value)
		{
			value = ExTimeZoneHelperForMigrationOnly.ToLegacyUtcIfDateTime(value);
			switch (property.SpecifiedWith)
			{
			case PropertyTypeSpecifier.PropertyTag:
			{
				PropertyTagPropertyDefinition propertyTagPropertyDefinition = (PropertyTagPropertyDefinition)property;
				TnefPropertyTag propertyTag = (int)propertyTagPropertyDefinition.PropertyTag;
				this.PropertyWriter.WriteProperty(propertyTag, value);
				return;
			}
			case PropertyTypeSpecifier.GuidString:
			{
				GuidNamePropertyDefinition guidNamePropertyDefinition = (GuidNamePropertyDefinition)property;
				TnefPropertyType propertyType = (TnefPropertyType)guidNamePropertyDefinition.MapiPropertyType;
				this.PropertyWriter.WriteProperty(guidNamePropertyDefinition.Guid, guidNamePropertyDefinition.PropertyName, propertyType, value);
				return;
			}
			case PropertyTypeSpecifier.GuidId:
			{
				GuidIdPropertyDefinition guidIdPropertyDefinition = (GuidIdPropertyDefinition)property;
				TnefPropertyType propertyType2 = (TnefPropertyType)guidIdPropertyDefinition.MapiPropertyType;
				this.PropertyWriter.WriteProperty(guidIdPropertyDefinition.Guid, guidIdPropertyDefinition.Id, propertyType2, value);
				return;
			}
			default:
				throw new InvalidOperationException(string.Format("Invalid native property specifier: {0}", property.SpecifiedWith));
			}
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x00108F18 File Offset: 0x00107118
		private void StreamProperty(NativeStorePropertyDefinition property, PersistablePropertyBag propertyBag)
		{
			Stream stream = null;
			try
			{
				stream = propertyBag.OpenPropertyStream(property, PropertyOpenMode.ReadOnly);
				this.StreamProperty(property, stream);
			}
			catch (ObjectNotFoundException)
			{
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x00108F68 File Offset: 0x00107168
		private void StreamProperty(NativeStorePropertyDefinition property, Stream propertyStream)
		{
			Stream stream = null;
			try
			{
				switch (property.SpecifiedWith)
				{
				case PropertyTypeSpecifier.PropertyTag:
				{
					PropertyTagPropertyDefinition propertyTagPropertyDefinition = (PropertyTagPropertyDefinition)property;
					TnefPropertyTag propertyTag = (int)propertyTagPropertyDefinition.PropertyTag;
					stream = this.PropertyWriter.OpenPropertyStream(propertyTag);
					break;
				}
				case PropertyTypeSpecifier.GuidString:
				{
					GuidNamePropertyDefinition guidNamePropertyDefinition = (GuidNamePropertyDefinition)property;
					TnefPropertyType propertyType = (TnefPropertyType)guidNamePropertyDefinition.MapiPropertyType;
					stream = this.PropertyWriter.OpenPropertyStream(guidNamePropertyDefinition.Guid, guidNamePropertyDefinition.PropertyName, propertyType);
					break;
				}
				case PropertyTypeSpecifier.GuidId:
				{
					GuidIdPropertyDefinition guidIdPropertyDefinition = (GuidIdPropertyDefinition)property;
					TnefPropertyType propertyType2 = (TnefPropertyType)guidIdPropertyDefinition.MapiPropertyType;
					stream = this.PropertyWriter.OpenPropertyStream(guidIdPropertyDefinition.Guid, guidIdPropertyDefinition.Id, propertyType2);
					break;
				}
				default:
					throw new InvalidOperationException(string.Format("Invalid native property specifier: {0}", property.SpecifiedWith));
				}
				Util.StreamHandler.CopyStreamData(propertyStream, stream);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x00109054 File Offset: 0x00107254
		private void WriteMessageBody()
		{
			if (this.options.FilterBodyHandler != null && !this.options.FilterBodyHandler(this.item))
			{
				return;
			}
			this.limitsTracker.CountMessageBody();
			BodyFormat rawFormat = this.item.Body.RawFormat;
			if (rawFormat == BodyFormat.TextHtml)
			{
				this.WriteProperty(InternalSchema.HtmlBody, this.item.PropertyBag);
				return;
			}
			this.WriteProperty(InternalSchema.RtfSyncBodyCrc, this.item.PropertyBag);
			this.WriteProperty(InternalSchema.RtfSyncBodyCount, this.item.PropertyBag);
			this.WriteProperty(InternalSchema.RtfSyncBodyTag, this.item.PropertyBag);
			this.WriteProperty(InternalSchema.RtfInSync, this.item.PropertyBag);
			this.WriteProperty(InternalSchema.RtfSyncPrefixCount, this.item.PropertyBag);
			this.WriteProperty(InternalSchema.RtfSyncTrailingCount, this.item.PropertyBag);
			if (!this.item.Body.IsBodyDefined)
			{
				return;
			}
			if (rawFormat == BodyFormat.TextPlain)
			{
				this.WriteProperty(InternalSchema.TextBody, this.item.PropertyBag);
			}
			BodyReadConfiguration configuration = new BodyReadConfiguration(BodyFormat.ApplicationRtf);
			using (Stream stream = this.item.Body.OpenReadStream(configuration))
			{
				using (Stream stream2 = this.PropertyWriter.OpenPropertyStream(TnefPropertyTag.RtfCompressed))
				{
					Util.StreamHandler.CopyStreamData(stream, stream2);
				}
			}
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x001091D8 File Offset: 0x001073D8
		private void WriteAttachment(Attachment attachment, int attachNumber)
		{
			this.limitsTracker.CountMessageAttachment();
			this.PropertyWriter.StartAttachment();
			this.WriteProperty(InternalSchema.AttachNum, attachNumber);
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in attachment.AllNativeProperties)
			{
				if (!nativeStorePropertyDefinition.Equals(InternalSchema.AttachDataObj) && !nativeStorePropertyDefinition.Equals(InternalSchema.AttachDataBin) && !OutboundMsgConverter.excludedPropertySet.Contains(nativeStorePropertyDefinition))
				{
					this.WriteProperty(nativeStorePropertyDefinition, attachment.PropertyBag);
				}
			}
			switch (attachment.AttachmentType)
			{
			case AttachmentType.Stream:
				this.WriteProperty(InternalSchema.AttachDataBin, attachment.PropertyBag);
				return;
			case AttachmentType.EmbeddedMessage:
				this.WriteAttachedItem((ItemAttachment)attachment);
				return;
			case AttachmentType.Ole:
				this.StreamProperty(InternalSchema.AttachDataObj, attachment.PropertyBag);
				break;
			case AttachmentType.Reference:
				break;
			default:
				return;
			}
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x001092CC File Offset: 0x001074CC
		private void WriteAttachedItem(ItemAttachment itemAttachment)
		{
			using (Item item = itemAttachment.GetItem(InternalSchema.ContentConversionProperties))
			{
				this.limitsTracker.StartEmbeddedMessage();
				using (MsgStorageWriter embeddedMessageWriter = this.PropertyWriter.GetEmbeddedMessageWriter())
				{
					OutboundMsgConverter outboundMsgConverter = new OutboundMsgConverter(this.options);
					outboundMsgConverter.InternalConvertItemToMsgStorage(item, embeddedMessageWriter, this.limitsTracker);
				}
				this.limitsTracker.EndEmbeddedMessage();
			}
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x00109358 File Offset: 0x00107558
		private void WriteAttachments()
		{
			if (this.item.AttachmentCollection != null)
			{
				int num = 0;
				this.item.CoreItem.OpenAttachmentCollection();
				foreach (AttachmentHandle handle in this.item.CoreItem.AttachmentCollection)
				{
					using (Attachment attachment = this.item.AttachmentCollection.Open(handle, InternalSchema.ContentConversionProperties))
					{
						if (this.options.FilterAttachmentHandler == null || this.options.FilterAttachmentHandler(this.item, attachment))
						{
							using (StorageGlobals.SetTraceContext(attachment))
							{
								StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Starting attachment (OutboundMsgStorage.WriteAttachments)");
								this.WriteAttachment(attachment, num++);
								StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing attachment (OutboundMsgStorage.WriteAttachments)");
							}
						}
					}
				}
			}
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x00109474 File Offset: 0x00107674
		private void WriteRecipient(ConversionRecipientEntry recipient, int recipientIndex)
		{
			if (recipient.Participant == null)
			{
				return;
			}
			this.PropertyWriter.StartRecipient();
			this.writer.WriteProperty(TnefPropertyTag.Rowid, recipientIndex);
			RecipientItemType recipientItemType = recipient.RecipientItemType;
			this.writer.WriteProperty(TnefPropertyTag.RecipientType, (int)MapiUtil.RecipientItemTypeToMapiRecipientType(recipientItemType, false));
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in recipient.AllNativeProperties)
			{
				if (nativeStorePropertyDefinition != null && !OutboundMsgConverter.recipientExcludedPropertySet.Contains(nativeStorePropertyDefinition))
				{
					this.WriteRecipientProperty(recipient, nativeStorePropertyDefinition);
				}
			}
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x00109528 File Offset: 0x00107728
		private void WriteRecipientProperty(ConversionRecipientEntry recipient, NativeStorePropertyDefinition property)
		{
			object obj = recipient.TryGetProperty(property);
			if (!PropertyError.IsPropertyError(obj))
			{
				this.WriteProperty(property, obj);
			}
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x00109550 File Offset: 0x00107750
		private void WriteRecipientTable()
		{
			List<ConversionRecipientEntry> recipients = this.addressCache.Recipients;
			if (recipients != null)
			{
				int num = 0;
				foreach (ConversionRecipientEntry recipient in recipients)
				{
					this.WriteRecipient(recipient, num++);
				}
			}
		}

		// Token: 0x04002310 RID: 8976
		private static HashSet<NativeStorePropertyDefinition> excludedPropertySet = OutboundMsgConverter.CreateExcludedPropertiesSet();

		// Token: 0x04002311 RID: 8977
		private static HashSet<NativeStorePropertyDefinition> recipientExcludedPropertySet = OutboundMsgConverter.CreateRecipientExcludedPropertiesSet();

		// Token: 0x04002312 RID: 8978
		private Item item;

		// Token: 0x04002313 RID: 8979
		private MsgStorageWriter writer;

		// Token: 0x04002314 RID: 8980
		private OutboundConversionOptions options;

		// Token: 0x04002315 RID: 8981
		private ConversionLimitsTracker limitsTracker;

		// Token: 0x04002316 RID: 8982
		private OutboundAddressCache addressCache;

		// Token: 0x0200060D RID: 1549
		private enum StoreSupportMaskValues
		{
			// Token: 0x04002318 RID: 8984
			None,
			// Token: 0x04002319 RID: 8985
			StoreEntryIdUnique,
			// Token: 0x0400231A RID: 8986
			StoreReadOnly,
			// Token: 0x0400231B RID: 8987
			StoreSearchOk = 4,
			// Token: 0x0400231C RID: 8988
			StoreModifyOk = 8,
			// Token: 0x0400231D RID: 8989
			StoreCreateOk = 16,
			// Token: 0x0400231E RID: 8990
			StoreAttachOk = 32,
			// Token: 0x0400231F RID: 8991
			StoreOleOk = 64,
			// Token: 0x04002320 RID: 8992
			StoreSubmitOk = 128,
			// Token: 0x04002321 RID: 8993
			StoreNotifyOk = 256,
			// Token: 0x04002322 RID: 8994
			StoreMVPropsOk = 512,
			// Token: 0x04002323 RID: 8995
			StoreCategorizeOk = 1024,
			// Token: 0x04002324 RID: 8996
			StoreRtfOk = 2048,
			// Token: 0x04002325 RID: 8997
			StoreRestrictionOk = 4096,
			// Token: 0x04002326 RID: 8998
			StoreSortOk = 8192,
			// Token: 0x04002327 RID: 8999
			StorePublicFolders = 16384,
			// Token: 0x04002328 RID: 9000
			StoreUncompressedRtf = 32768,
			// Token: 0x04002329 RID: 9001
			StoreHtmlOk = 65536,
			// Token: 0x0400232A RID: 9002
			StoreAnsiOk = 131072,
			// Token: 0x0400232B RID: 9003
			StoreUnicodeOk = 262144,
			// Token: 0x0400232C RID: 9004
			StoreLocalStore = 524288,
			// Token: 0x0400232D RID: 9005
			Default = 265849
		}

		// Token: 0x0200060E RID: 1550
		// (Invoke) Token: 0x06003FC0 RID: 16320
		private delegate object ComputeValueDelegate();
	}
}
