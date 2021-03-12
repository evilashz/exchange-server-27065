using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000167 RID: 359
	internal sealed class NormalizedBodyProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x0003177C File Offset: 0x0002F97C
		public NormalizedBodyProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00031785 File Offset: 0x0002F985
		public static NormalizedBodyProperty CreateCommand(CommandContext commandContext)
		{
			return new NormalizedBodyProperty(commandContext);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000317AC File Offset: 0x0002F9AC
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			ResponseShape responseShape = commandSettings.ResponseShape;
			this.xsoItem = (storeObject as Item);
			this.itemResponseShape = (responseShape as ItemResponseShape);
			this.idAndSession = idAndSession;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			if (this.itemResponseShape.CanCreateNormalizedBodyServiceObject)
			{
				serviceObject.PropertyBag[propertyInformation] = this.CreateNormalizedBodyServiceObject();
				return;
			}
			this.itemResponseShape.BodyType = BodyResponseType.HTML;
			BodyProperty bodyProperty = BodyProperty.CreateCommand(this.commandContext);
			bodyProperty.CharBuffer = new char[32768];
			bodyProperty.InlineAttachmentAction = delegate(string value)
			{
				IDictionary<string, bool> inlineImagesInNormalizedBody = EWSSettings.InlineImagesInNormalizedBody;
				inlineImagesInNormalizedBody[value] = true;
			};
			((IToServiceObjectCommand)bodyProperty).ToServiceObject();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00031880 File Offset: 0x0002FA80
		private BodyContentType CreateNormalizedBodyServiceObject()
		{
			BodyType bodyType = BodyProperty.GetBodyType(this.itemResponseShape.NormalizedBodyType);
			BodyContentType bodyContentType = new BodyContentType
			{
				BodyType = bodyType,
				Value = string.Empty
			};
			switch (bodyType)
			{
			case BodyType.Text:
				this.FillTextBody(bodyContentType);
				return bodyContentType;
			}
			this.FillNormalizedHtmlBody(bodyContentType);
			return bodyContentType;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000318DC File Offset: 0x0002FADC
		private void FillTextBody(BodyContentType bodyContent)
		{
			Body effectiveBody = Util.GetEffectiveBody(this.xsoItem);
			using (TextReader textReader = effectiveBody.OpenTextReader(BodyFormat.TextPlain))
			{
				UniqueBodyProperty.GetTruncatedString(textReader.ReadToEnd(), this.itemResponseShape.MaximumBodySize, bodyContent);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0003195C File Offset: 0x0002FB5C
		private void FillNormalizedHtmlBody(BodyContentType bodyContent)
		{
			try
			{
				bool itemBlockStatus = Util.GetItemBlockStatus(this.xsoItem, this.itemResponseShape.BlockExternalImages, this.itemResponseShape.BlockExternalImagesIfSenderUntrusted);
				HtmlBodyCallback htmlBodyCallback = new HtmlBodyCallback(this.xsoItem, this.idAndSession, false);
				htmlBodyCallback.AddBlankTargetToLinks = this.itemResponseShape.AddBlankTargetToLinks;
				htmlBodyCallback.InlineImageUrlTemplate = this.itemResponseShape.InlineImageUrlTemplate;
				htmlBodyCallback.InlineImageUrlOnLoadTemplate = this.itemResponseShape.InlineImageUrlOnLoadTemplate;
				htmlBodyCallback.InlineImageCustomDataTemplate = this.itemResponseShape.InlineImageCustomDataTemplate;
				htmlBodyCallback.IsBodyFragment = false;
				htmlBodyCallback.BlockExternalImages = itemBlockStatus;
				htmlBodyCallback.CalculateAttachmentInlineProps = this.itemResponseShape.CalculateAttachmentInlineProps;
				htmlBodyCallback.HasBlockedImagesAction = delegate(bool value)
				{
					EWSSettings.ItemHasBlockedImages = new bool?(value);
				};
				htmlBodyCallback.InlineAttachmentIdAction = delegate(string value)
				{
					IDictionary<string, bool> inlineImagesInNormalizedBody = EWSSettings.InlineImagesInNormalizedBody;
					inlineImagesInNormalizedBody[value] = true;
				};
				HtmlBodyCallback callback = htmlBodyCallback;
				long num = 0L;
				Body effectiveBody = Util.GetEffectiveBody(this.xsoItem);
				ConversationBodyScanner conversationBodyScanner = effectiveBody.GetConversationBodyScanner(callback, -1L, 0L, true, this.itemResponseShape.FilterHtmlContent, out num);
				StringBuilder stringBuilder = new StringBuilder((int)num);
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					using (HtmlWriter htmlWriter = new HtmlWriter(stringWriter))
					{
						conversationBodyScanner.WriteAll(htmlWriter);
						htmlWriter.Flush();
					}
					stringWriter.Flush();
				}
				UniqueBodyProperty.GetTruncatedString(stringBuilder.ToString(), this.itemResponseShape.MaximumBodySize, bodyContent);
				FaultInjection.GenerateFault((FaultInjection.LIDs)2231774525U);
			}
			catch (TextConvertersException innerException)
			{
				throw new PropertyRequestFailedException(CoreResources.IDs.ErrorItemPropertyRequestFailed, this.commandContext.PropertyInformation.PropertyPath, innerException);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00031B60 File Offset: 0x0002FD60
		public void ToXml()
		{
			throw new InvalidOperationException("NormalizedBodyProperty.ToXml should not be called.");
		}

		// Token: 0x040007B2 RID: 1970
		private Item xsoItem;

		// Token: 0x040007B3 RID: 1971
		private ItemResponseShape itemResponseShape;

		// Token: 0x040007B4 RID: 1972
		private IdAndSession idAndSession;
	}
}
