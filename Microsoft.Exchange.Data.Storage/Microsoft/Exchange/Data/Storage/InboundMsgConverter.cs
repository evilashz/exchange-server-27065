using System;
using System.IO;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.MsgStorage.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005F1 RID: 1521
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class InboundMsgConverter : AbstractInboundConverter
	{
		// Token: 0x06003E66 RID: 15974 RVA: 0x00102DBC File Offset: 0x00100FBC
		internal InboundMsgConverter(InboundMessageWriter writer) : base(writer, new AbstractInboundConverter.WriteablePropertyPromotionRule())
		{
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x00102DCA File Offset: 0x00100FCA
		private void ConvertMessage()
		{
			this.isTextBodyFound = false;
			this.isHtmlBodyFound = false;
			this.isRtfBodyFound = false;
			this.isRtfInSync = false;
			this.PromotePropertyList();
			this.SetBody();
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x00102DF4 File Offset: 0x00100FF4
		protected override void PromoteBodyProperty(StorePropertyDefinition property)
		{
			if (property == InternalSchema.TextBody)
			{
				this.isTextBodyFound = true;
				return;
			}
			if (property == InternalSchema.HtmlBody)
			{
				this.isHtmlBodyFound = true;
				return;
			}
			if (property == InternalSchema.RtfBody)
			{
				this.isRtfBodyFound = true;
				return;
			}
			if (property == InternalSchema.RtfInSync)
			{
				this.isRtfInSync = this.InternalReadBool();
				base.SetProperty(InternalSchema.RtfInSync, this.isRtfInSync);
			}
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x00102E5C File Offset: 0x0010105C
		private void SetBody()
		{
			Stream stream = null;
			try
			{
				Charset charset = null;
				Charset.TryGetCharset(this.internetCpid, out charset);
				string targetCharsetName = (charset != null) ? charset.Name : null;
				BodyWriteConfiguration bodyWriteConfiguration;
				if (this.isRtfBodyFound && (!this.isHtmlBodyFound || this.isRtfInSync))
				{
					stream = this.InternalGetValueReadStream(TnefPropertyTag.RtfCompressed);
					bodyWriteConfiguration = new BodyWriteConfiguration(BodyFormat.ApplicationRtf);
					bodyWriteConfiguration.SetTargetFormat(BodyFormat.ApplicationRtf, targetCharsetName, BodyCharsetFlags.PreserveUnicode);
				}
				else if (this.isHtmlBodyFound)
				{
					if (charset == null)
					{
						charset = Charset.DefaultWebCharset;
					}
					stream = this.InternalGetValueReadStream(TnefPropertyTag.BodyHtmlB);
					bodyWriteConfiguration = new BodyWriteConfiguration(BodyFormat.TextHtml, charset.Name);
					bodyWriteConfiguration.SetTargetFormat(BodyFormat.TextHtml, charset.Name, BodyCharsetFlags.PreserveUnicode);
				}
				else
				{
					if (this.isTextBodyFound)
					{
						stream = this.InternalGetValueReadStream(TnefPropertyTag.BodyW);
					}
					bodyWriteConfiguration = new BodyWriteConfiguration(BodyFormat.TextPlain, ConvertUtils.UnicodeCharset.Name);
					if (charset != null)
					{
						bodyWriteConfiguration.SetTargetFormat(BodyFormat.TextPlain, charset.Name);
					}
					bodyWriteConfiguration.SetTargetFormat(BodyFormat.TextPlain, targetCharsetName, BodyCharsetFlags.PreserveUnicode);
				}
				base.CoreItem.CharsetDetector.DetectionOptions = base.MessageWriter.ConversionOptions.DetectionOptions;
				using (Stream stream2 = base.CoreItem.Body.OpenWriteStream(bodyWriteConfiguration))
				{
					if (stream != null)
					{
						Util.StreamHandler.CopyStreamData(stream, stream2);
					}
				}
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x00102FD4 File Offset: 0x001011D4
		protected override bool CanPromoteMimeOnlyProperties()
		{
			return true;
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00102FD8 File Offset: 0x001011D8
		public void ConvertToItem(Stream msgStorageStream)
		{
			MsgStorageReader msgStorageReader = null;
			try
			{
				msgStorageReader = new MsgStorageReader(msgStorageStream);
				this.InternalConvertToItem(msgStorageReader);
			}
			finally
			{
				if (msgStorageReader != null)
				{
					msgStorageReader.Dispose();
				}
			}
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x00103014 File Offset: 0x00101214
		private void InternalConvertToItem(MsgStorageReader reader)
		{
			CoreObject.GetPersistablePropertyBag(base.CoreItem).SaveFlags |= (PropertyBagSaveFlags.IgnoreMapiComputedErrors | PropertyBagSaveFlags.IgnoreUnresolvedHeaders);
			this.reader = reader;
			this.ConvertMessage();
			for (int num = 0; num != this.reader.RecipientCount; num++)
			{
				this.ConvertRecipient(num);
			}
			for (int num2 = 0; num2 != this.reader.AttachmentCount; num2++)
			{
				this.ConvertAttachment(num2);
			}
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x00103080 File Offset: 0x00101280
		private void ConvertRecipient(int recipientIndex)
		{
			try
			{
				this.reader.OpenRecipient(recipientIndex);
				base.MessageWriter.StartNewRecipient();
				this.PromotePropertyList();
				base.MessageWriter.EndRecipient();
			}
			catch (MsgStorageNotFoundException)
			{
				StorageGlobals.ContextTraceError<int>(ExTraceGlobals.CcInboundTnefTracer, "InboundMsgConverter::RecipientNotFound (index = {0})", recipientIndex);
			}
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x001030DC File Offset: 0x001012DC
		private void ConvertAttachment(int attachmentIndex)
		{
			try
			{
				this.reader.OpenAttachment(attachmentIndex);
				base.MessageWriter.StartNewAttachment(this.reader.PropertyReader.AttachMethod);
				this.PromotePropertyList();
				base.MessageWriter.EndAttachment();
			}
			catch (MsgStorageNotFoundException)
			{
				StorageGlobals.ContextTraceError<int>(ExTraceGlobals.CcInboundTnefTracer, "InboundMsgConverter::AttachmentNotFound (index = {0})", attachmentIndex);
			}
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x0010314C File Offset: 0x0010134C
		protected override void PromoteAttachDataObject()
		{
			if (this.PropertyReader.AttachMethod == 5)
			{
				using (MsgStorageReader embeddedMessageReader = this.PropertyReader.GetEmbeddedMessageReader())
				{
					using (InboundMessageWriter inboundMessageWriter = base.MessageWriter.OpenAttachedMessageWriter())
					{
						InboundMsgConverter inboundMsgConverter = new InboundMsgConverter(inboundMessageWriter);
						inboundMsgConverter.InternalConvertToItem(embeddedMessageReader);
						inboundMessageWriter.Commit();
					}
					return;
				}
			}
			if (this.PropertyReader.AttachMethod == 6)
			{
				using (Stream valueReadStream = this.PropertyReader.GetValueReadStream())
				{
					using (Stream stream = base.MessageWriter.OpenOleAttachmentDataStream())
					{
						Util.StreamHandler.CopyStreamData(valueReadStream, stream);
					}
				}
			}
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x0010323C File Offset: 0x0010143C
		protected override void PromoteInternetCpidProperty()
		{
			this.internetCpid = (int)this.PropertyReader.ReadValue();
			base.SetProperty(InternalSchema.InternetCpid, this.internetCpid);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x00103278 File Offset: 0x00101478
		private void PromotePropertyList()
		{
			while (this.PropertyReader.ReadNextProperty())
			{
				NativeStorePropertyDefinition nativeStorePropertyDefinition = this.CreatePropertyDefinition();
				if (nativeStorePropertyDefinition != null)
				{
					AbstractInboundConverter.IPromotionRule propertyPromotionRule = base.GetPropertyPromotionRule(nativeStorePropertyDefinition);
					if (propertyPromotionRule != null)
					{
						propertyPromotionRule.PromoteProperty(this, nativeStorePropertyDefinition);
					}
				}
			}
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x001032B4 File Offset: 0x001014B4
		protected override bool IsLargeValue()
		{
			return this.PropertyReader.IsLargeValue;
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x001032D0 File Offset: 0x001014D0
		protected override Stream OpenValueReadStream(out int skipTrailingNulls)
		{
			TnefPropertyType propertyType = this.PropertyReader.PropertyType;
			skipTrailingNulls = ((propertyType == TnefPropertyType.Unicode) ? 2 : 0);
			return this.PropertyReader.GetValueReadStream();
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x00103308 File Offset: 0x00101508
		private NativeStorePropertyDefinition CreatePropertyDefinition()
		{
			TnefNameId? namedProperty = this.PropertyReader.IsNamedProperty ? new TnefNameId?(this.PropertyReader.PropertyNameId) : null;
			return base.CreatePropertyDefinition(this.PropertyReader.PropertyTag, namedProperty);
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x0010335C File Offset: 0x0010155C
		protected override object ReadValue()
		{
			if (this.IsLargeValue())
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundTnefTracer, "InboundMsgConverter::ReadValue: large property value");
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent);
			}
			return ExTimeZoneHelperForMigrationOnly.ToExDateTimeIfObjectIsDateTime(this.PropertyReader.ReadValue());
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x0010339C File Offset: 0x0010159C
		private bool InternalReadBool()
		{
			return (bool)this.PropertyReader.ReadValue();
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x001033BC File Offset: 0x001015BC
		private Stream InternalGetValueReadStream(TnefPropertyTag propertyTag)
		{
			return this.PropertyReader.GetValueReadStream(propertyTag);
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x001033D8 File Offset: 0x001015D8
		private MsgStoragePropertyReader PropertyReader
		{
			get
			{
				return this.reader.PropertyReader;
			}
		}

		// Token: 0x0400216A RID: 8554
		private bool isRtfInSync;

		// Token: 0x0400216B RID: 8555
		private bool isTextBodyFound;

		// Token: 0x0400216C RID: 8556
		private bool isRtfBodyFound;

		// Token: 0x0400216D RID: 8557
		private bool isHtmlBodyFound;

		// Token: 0x0400216E RID: 8558
		private int internetCpid;

		// Token: 0x0400216F RID: 8559
		private MsgStorageReader reader;
	}
}
