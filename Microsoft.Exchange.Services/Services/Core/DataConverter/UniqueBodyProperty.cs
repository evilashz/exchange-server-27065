using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000163 RID: 355
	internal sealed class UniqueBodyProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x0003043E File Offset: 0x0002E63E
		public UniqueBodyProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00030447 File Offset: 0x0002E647
		public static UniqueBodyProperty CreateCommand(CommandContext commandContext)
		{
			return new UniqueBodyProperty(commandContext);
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0003044F File Offset: 0x0002E64F
		public override bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00030454 File Offset: 0x0002E654
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			ResponseShape responseShape = commandSettings.ResponseShape;
			this.xsoItem = (storeObject as Item);
			this.itemResponseShape = (responseShape as ItemResponseShape);
			this.itemResponseShape.BlockExternalImages = Util.GetItemBlockStatus(this.xsoItem, this.itemResponseShape.BlockExternalImages, this.itemResponseShape.BlockExternalImagesIfSenderUntrusted);
			bool isIrmEnabled = IrmUtils.IsIrmEnabled(this.itemResponseShape.ClientSupportsIrm, idAndSession.Session);
			this.CreateUniqueBodyServiceObject(storeObject, idAndSession, serviceObject, isIrmEnabled);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000304F0 File Offset: 0x0002E6F0
		internal static void GetTruncatedString(string stringToTruncate, int maximumSize, BodyContentType bodyContent)
		{
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
			{
				bodyContent.IsTruncatedSpecified = true;
				bodyContent.IsTruncated = false;
			}
			if (maximumSize > 0 && stringToTruncate.Length > maximumSize)
			{
				int num = maximumSize;
				if (char.IsHighSurrogate(stringToTruncate[num - 1]))
				{
					num--;
				}
				bodyContent.Value = stringToTruncate.Substring(0, num);
				bodyContent.IsTruncated = true;
				return;
			}
			bodyContent.Value = stringToTruncate;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0003055C File Offset: 0x0002E75C
		private static ConversationId GetConversationId(StoreObject storeObject)
		{
			return PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ItemSchema.ConversationId) as ConversationId;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0003057C File Offset: 0x0002E77C
		private static ICoreConversation GetConversation(ConversationId conversationId, IdAndSession idAndSession, bool isIrmEnabled)
		{
			ICoreConversation coreConversation = null;
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession == null)
			{
				return coreConversation;
			}
			ICoreConversation result;
			try
			{
				FaultInjection.GenerateFault((FaultInjection.LIDs)3976604989U);
				coreConversation = Conversation.Load(mailboxSession, conversationId, isIrmEnabled, new PropertyDefinition[0]);
				result = coreConversation;
			}
			catch (StoragePermanentException ex)
			{
				if (!(ex.InnerException is MapiExceptionNoAccess))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)conversationId.GetHashCode(), "[UniqueBodyProperty::GetConversation] encountered exception - Class: {0}, Message: {1}.Inner exception was not MapiExceptionConversationNotFound but rather Class: {2}, Message {3}", new object[]
					{
						ex.GetType().FullName,
						ex.Message,
						(ex.InnerException == null) ? "<NULL>" : ex.InnerException.GetType().FullName,
						(ex.InnerException == null) ? "<NULL>" : ex.InnerException.Message
					});
					throw;
				}
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)conversationId.GetHashCode(), "[UniqueBodyProperty::GetConversation] encountered exception - Class: {0}, Message: {1}.Inner exception was MapiExceptionNoAccess - Class: {2}, Message {3}", new object[]
				{
					ex.GetType().FullName,
					ex.Message,
					(ex.InnerException == null) ? "<NULL>" : ex.InnerException.GetType().FullName,
					(ex.InnerException == null) ? "<NULL>" : ex.InnerException.Message
				});
				result = coreConversation;
			}
			return result;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000306E4 File Offset: 0x0002E8E4
		private static ItemPart GetItemPartForStoreObjectId(ICoreConversation conversation, StoreObject storeObject)
		{
			ItemPart itemPart = null;
			StoreObjectId objectId = storeObject.Id.ObjectId;
			ItemPart result;
			try
			{
				itemPart = conversation.GetItemPart(objectId);
				result = itemPart;
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)conversation.ConversationId.GetHashCode(), "[UniqueBodyProperty::GetItemPartForStoreObjectId] encountered exception - Class: {0}, Message: {1}.Inner exception was Class: {2}, Message {3}", new object[]
				{
					ex.GetType().FullName,
					ex.Message,
					(ex.InnerException == null) ? "<NULL>" : ex.InnerException.GetType().FullName,
					(ex.InnerException == null) ? "<NULL>" : ex.InnerException.Message
				});
				result = itemPart;
			}
			catch (TextConvertersException innerException)
			{
				throw new PropertyRequestFailedException(CoreResources.IDs.ErrorItemPropertyRequestFailed, ItemSchema.UniqueBody.PropertyPath, innerException);
			}
			catch (Exception ex2)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)conversation.ConversationId.GetHashCode(), "[UniqueBodyProperty::GetItemPartForStoreObjectId] encountered exception - Class: {0}, Message: {1}.Inner exception was Class: {2}, Message {3}", new object[]
				{
					ex2.GetType().FullName,
					ex2.Message,
					(ex2.InnerException == null) ? "<NULL>" : ex2.InnerException.GetType().FullName,
					(ex2.InnerException == null) ? "<NULL>" : ex2.InnerException.Message
				});
				throw;
			}
			return result;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00030868 File Offset: 0x0002EA68
		private static BodyFormat ComputeBodyFormat(BodyResponseType bodyType)
		{
			switch (bodyType)
			{
			case BodyResponseType.Text:
				return BodyFormat.TextPlain;
			}
			return BodyFormat.TextHtml;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000308AC File Offset: 0x0002EAAC
		private void CreateUniqueBodyServiceObject(StoreObject storeObject, IdAndSession idAndSession, ServiceObject serviceObject, bool isIrmEnabled)
		{
			BodyType bodyType = BodyProperty.GetBodyType(this.itemResponseShape.UniqueBodyType);
			BodyContentType value2 = null;
			BodyContentType bodyContentType = new BodyContentType
			{
				BodyType = bodyType
			};
			ConversationId conversationId = UniqueBodyProperty.GetConversationId(storeObject);
			if (conversationId == null)
			{
				return;
			}
			ICoreConversation coreConversation = EWSSettings.CurrentConversation;
			if (coreConversation == null)
			{
				coreConversation = UniqueBodyProperty.GetConversation(conversationId, idAndSession, isIrmEnabled);
			}
			if (coreConversation == null)
			{
				return;
			}
			if (this.CanGenerateCssScopedBody(coreConversation))
			{
				this.itemResponseShape.BodyType = BodyResponseType.HTML;
				BodyProperty bodyProperty = BodyProperty.CreateCommand(this.commandContext);
				bodyProperty.CharBuffer = new char[32768];
				bodyProperty.InlineAttachmentAction = delegate(string value)
				{
					IDictionary<string, bool> inlineImagesInNormalizedBody = EWSSettings.InlineImagesInNormalizedBody;
					inlineImagesInNormalizedBody[value] = true;
				};
				((IToServiceObjectCommand)bodyProperty).ToServiceObject();
				return;
			}
			coreConversation.OnBeforeItemLoad += this.ConversationOnBeforeItemLoad;
			if (coreConversation.ConversationTree != null && coreConversation.ConversationTree.Count != 0)
			{
				ItemPart itemPartForStoreObjectId = UniqueBodyProperty.GetItemPartForStoreObjectId(coreConversation, storeObject);
				value2 = UniqueBodyProperty.GetBodyContentFromItemPart(bodyType, this.itemResponseShape, itemPartForStoreObjectId);
				if (itemPartForStoreObjectId != null)
				{
					switch (bodyType)
					{
					case BodyType.Text:
						UniqueBodyProperty.ConvertToTextOutput(itemPartForStoreObjectId, this.itemResponseShape, bodyContentType);
						goto IL_124;
					}
					UniqueBodyProperty.ConvertFragmentInfoToHtmlOutput(itemPartForStoreObjectId, this.itemResponseShape, bodyContentType);
					IL_124:
					value2 = bodyContentType;
				}
			}
			serviceObject.PropertyBag[this.commandContext.PropertyInformation] = value2;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000309F6 File Offset: 0x0002EBF6
		private bool CanGenerateCssScopedBody(ICoreConversation conversation)
		{
			return !string.IsNullOrEmpty(this.itemResponseShape.CssScopeClassName) && this.itemResponseShape.UniqueBodyType != BodyResponseType.Text && conversation.ConversationTree != null && conversation.ConversationTree.Count == 1;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00030A30 File Offset: 0x0002EC30
		internal static BodyContentType GetBodyContentFromItemPart(BodyType bodyType, ItemResponseShape itemResponseShape, ItemPart itemPart)
		{
			BodyContentType result = null;
			if (itemPart != null)
			{
				BodyContentType bodyContentType = new BodyContentType
				{
					BodyType = bodyType
				};
				switch (bodyType)
				{
				case BodyType.Text:
					UniqueBodyProperty.ConvertToTextOutput(itemPart, itemResponseShape, bodyContentType);
					goto IL_38;
				}
				UniqueBodyProperty.ConvertFragmentInfoToHtmlOutput(itemPart, itemResponseShape, bodyContentType);
				IL_38:
				result = bodyContentType;
			}
			return result;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00030A78 File Offset: 0x0002EC78
		internal static void ConvertToTextOutput(ItemPart itemPart, ItemResponseShape itemResponseShape, BodyContentType bodyContent)
		{
			string uniqueContentFromItemPart = UniqueBodyProperty.GetUniqueContentFromItemPart(itemPart);
			HtmlToText htmlToText = new HtmlToText(TextExtractionMode.NormalConversion);
			StringBuilder stringBuilder = new StringBuilder(uniqueContentFromItemPart.Length);
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				using (StringReader stringReader = new StringReader(uniqueContentFromItemPart))
				{
					htmlToText.Convert(stringReader, stringWriter);
				}
				stringWriter.Flush();
			}
			UniqueBodyProperty.GetTruncatedString(stringBuilder.ToString(), itemResponseShape.MaximumBodySize, bodyContent);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00030B08 File Offset: 0x0002ED08
		private static void ConvertFragmentInfoToHtmlOutput(ItemPart itemPart, ItemResponseShape itemResponseShape, BodyContentType bodyContent)
		{
			if (itemResponseShape.SeparateQuotedTextFromBody && !itemPart.UniqueFragmentInfo.QuotedTextFragment.IsEmpty)
			{
				string uniqueContentWithoutQuotedTextFromItemPart = UniqueBodyProperty.GetUniqueContentWithoutQuotedTextFromItemPart(itemPart);
				UniqueBodyProperty.GetTruncatedString(uniqueContentWithoutQuotedTextFromItemPart, itemResponseShape.MaximumBodySize, bodyContent);
				bodyContent.Value = string.Format("<html><body>{0}</body></html>", bodyContent.Value);
				if (bodyContent.IsTruncated)
				{
					return;
				}
				string value = bodyContent.Value;
				bodyContent.Value = null;
				string quotedTextFromItemPart = UniqueBodyProperty.GetQuotedTextFromItemPart(itemPart);
				int maximumSize = Math.Max(0, itemResponseShape.MaximumBodySize - uniqueContentWithoutQuotedTextFromItemPart.Length);
				UniqueBodyProperty.GetTruncatedString(quotedTextFromItemPart, maximumSize, bodyContent);
				if (!bodyContent.IsTruncated)
				{
					bodyContent.QuotedText = string.Format("<html><body>{0}</body></html>", bodyContent.Value);
					bodyContent.Value = value;
					return;
				}
			}
			string uniqueContentFromItemPart = UniqueBodyProperty.GetUniqueContentFromItemPart(itemPart);
			UniqueBodyProperty.GetTruncatedString(uniqueContentFromItemPart, itemResponseShape.MaximumBodySize, bodyContent);
			bodyContent.Value = string.Format("<html><body>{0}</body></html>", bodyContent.Value);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00030BEC File Offset: 0x0002EDEC
		private static string GetUniqueContentFromItemPart(ItemPart itemPart)
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					using (HtmlWriter htmlWriter = new HtmlWriter(stringWriter))
					{
						itemPart.WriteUniquePart(htmlWriter);
						htmlWriter.Flush();
					}
					stringWriter.Flush();
				}
				FaultInjection.GenerateFault((FaultInjection.LIDs)3305516349U);
				result = stringBuilder.ToString();
			}
			catch (TextConvertersException innerException)
			{
				throw new PropertyRequestFailedException(CoreResources.IDs.ErrorItemPropertyRequestFailed, ItemSchema.UniqueBody.PropertyPath, innerException);
			}
			return result;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00030C94 File Offset: 0x0002EE94
		private static string GetUniqueContentWithoutQuotedTextFromItemPart(ItemPart itemPart)
		{
			return UniqueBodyProperty.GetUniqueBodyContentsAsHtmlString(new Action<HtmlWriter>(itemPart.WriteUniquePartWithoutQuotedText));
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00030CA7 File Offset: 0x0002EEA7
		private static string GetQuotedTextFromItemPart(ItemPart itemPart)
		{
			return UniqueBodyProperty.GetUniqueBodyContentsAsHtmlString(new Action<HtmlWriter>(itemPart.WriteUniquePartQuotedText));
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00030CBC File Offset: 0x0002EEBC
		private static string GetUniqueBodyContentsAsHtmlString(Action<HtmlWriter> contentProvider)
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					using (HtmlWriter htmlWriter = new HtmlWriter(stringWriter))
					{
						contentProvider(htmlWriter);
						htmlWriter.Flush();
					}
					stringWriter.Flush();
				}
				result = stringBuilder.ToString();
			}
			catch (TextConvertersException innerException)
			{
				throw new PropertyRequestFailedException(CoreResources.IDs.ErrorItemPropertyRequestFailed, ItemSchema.UniqueBody.PropertyPath, innerException);
			}
			return result;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00030D58 File Offset: 0x0002EF58
		private void ConversationOnBeforeItemLoad(object sender, LoadItemEventArgs eventArgs)
		{
			eventArgs.HtmlStreamOptionCallback = new HtmlStreamOptionCallback(this.GetSafeHtmlCallbacks);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00030D98 File Offset: 0x0002EF98
		private KeyValuePair<HtmlStreamingFlags, HtmlCallbackBase> GetSafeHtmlCallbacks(Item item)
		{
			HtmlBodyCallback htmlBodyCallback = new HtmlBodyCallback(item, null, false);
			htmlBodyCallback.AddBlankTargetToLinks = this.itemResponseShape.AddBlankTargetToLinks;
			htmlBodyCallback.InlineImageUrlTemplate = this.itemResponseShape.InlineImageUrlTemplate;
			htmlBodyCallback.InlineImageUrlOnLoadTemplate = this.itemResponseShape.InlineImageUrlOnLoadTemplate;
			htmlBodyCallback.InlineImageCustomDataTemplate = this.itemResponseShape.InlineImageCustomDataTemplate;
			htmlBodyCallback.IsBodyFragment = true;
			htmlBodyCallback.BlockExternalImages = this.itemResponseShape.BlockExternalImages;
			htmlBodyCallback.CalculateAttachmentInlineProps = this.itemResponseShape.CalculateAttachmentInlineProps;
			htmlBodyCallback.HasBlockedImagesAction = delegate(bool value)
			{
				EWSSettings.ItemHasBlockedImages = new bool?(value);
			};
			htmlBodyCallback.InlineAttachmentIdAction = delegate(string value)
			{
				IDictionary<string, bool> inlineImagesInUniqueBody = EWSSettings.InlineImagesInUniqueBody;
				inlineImagesInUniqueBody[value] = true;
			};
			HtmlBodyCallback value2 = htmlBodyCallback;
			return new KeyValuePair<HtmlStreamingFlags, HtmlCallbackBase>(HtmlStreamingFlags.FilterHtml, value2);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00030E6A File Offset: 0x0002F06A
		public void ToXml()
		{
			throw new InvalidOperationException("UniqueBodyProperty.ToXml should not be called.");
		}

		// Token: 0x040007A9 RID: 1961
		private const string HtmlBodyWrapperFormatString = "<html><body>{0}</body></html>";

		// Token: 0x040007AA RID: 1962
		private Item xsoItem;

		// Token: 0x040007AB RID: 1963
		private ItemResponseShape itemResponseShape;
	}
}
