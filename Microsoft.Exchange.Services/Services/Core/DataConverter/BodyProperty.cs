using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.SafeHtml;
using Microsoft.Exchange.Services.Core.CssConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000113 RID: 275
	internal class BodyProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, ISetCommand, IAppendUpdateCommand, ISetUpdateCommand, IDeleteUpdateCommand, IUpdateCommand, IPropertyCommand, IRequireCharBuffer
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x00026709 File Offset: 0x00024909
		public BodyProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x17000143 RID: 323
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00026712 File Offset: 0x00024912
		public char[] CharBuffer
		{
			set
			{
				this.charBuffer = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0002671B File Offset: 0x0002491B
		public Action<string> InlineAttachmentAction
		{
			set
			{
				this.inlineAttachmentAction = value;
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00026724 File Offset: 0x00024924
		public static BodyProperty CreateCommand(CommandContext commandContext)
		{
			return new BodyProperty(commandContext);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002672C File Offset: 0x0002492C
		void IToServiceObjectCommand.ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			ResponseShape responseShape = commandSettings.ResponseShape;
			ItemResponseShape itemResponseShape = responseShape as ItemResponseShape;
			BodyResponseType bodyType = itemResponseShape.BodyType;
			Item item = (Item)storeObject;
			itemResponseShape.BlockExternalImages = Util.GetItemBlockStatus(item, itemResponseShape.BlockExternalImages, itemResponseShape.BlockExternalImagesIfSenderUntrusted);
			BodyType bodyType2 = BodyType.HTML;
			Body effectiveBody = Util.GetEffectiveBody(item);
			try
			{
				long size = effectiveBody.Size;
				base.ValidateDataSize(size);
				switch (this.ComputeBodyFormat(bodyType, item))
				{
				case BodyFormat.TextHtml:
				case BodyFormat.ApplicationRtf:
					bodyType2 = BodyType.HTML;
					goto IL_A1;
				}
				bodyType2 = BodyType.Text;
				IL_A1:;
			}
			catch (PropertyErrorException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[BodyProperty::ToXml] encountered exception - Class: {0}, Message: {1}", ex.GetType().FullName, ex.Message);
				throw new ObjectCorruptException(ex, true);
			}
			catch (StoragePermanentException ex2)
			{
				if (ex2.InnerException is MapiExceptionNoSupport)
				{
					throw new ObjectCorruptException(ex2, true);
				}
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)this.GetHashCode(), "[BodyProperty::ToXml] encountered exception - Class: {0}, Message: {1}.Inner exception was not MapiExceptionNoSupport but rather Class: {2}, Message {3}", new object[]
				{
					ex2.GetType().FullName,
					ex2.Message,
					(ex2.InnerException == null) ? "<NULL>" : ex2.InnerException.GetType().FullName,
					(ex2.InnerException == null) ? "<NULL>" : ex2.InnerException.Message
				});
				throw;
			}
			BodyContentType bodyContentType = new BodyContentType
			{
				BodyType = bodyType2,
				Value = string.Empty
			};
			using (TextWriter textWriter = new StringWriter())
			{
				if (bodyType2 == BodyType.HTML)
				{
					this.WriteHtmlContent(textWriter, item, itemResponseShape);
				}
				else
				{
					this.WriteTextContent(textWriter, item, itemResponseShape.ShouldUseNarrowGapForPTagHtmlToTextConversion);
				}
				UniqueBodyProperty.GetTruncatedString(textWriter.ToString(), itemResponseShape.MaximumBodySize, bodyContentType);
			}
			serviceObject[this.commandContext.PropertyInformation] = bodyContentType;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00026958 File Offset: 0x00024B58
		internal static void FixupEncodedBodyContent(BodyContentType bodyContent)
		{
			bodyContent.Value = bodyContent.Value.Replace("&#43;", "+");
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00026978 File Offset: 0x00024B78
		internal static BodyType GetBodyType(BodyResponseType bodyResponseType)
		{
			switch (bodyResponseType)
			{
			case BodyResponseType.Text:
				return BodyType.Text;
			}
			return BodyType.HTML;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000269A0 File Offset: 0x00024BA0
		public override void SetPhase2()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			this.ConvertHtmlToRtfIfNecessary(commandSettings.StoreObject as Item);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000269C8 File Offset: 0x00024BC8
		public override void PostUpdate()
		{
			UpdateCommandSettings commandSettings = base.GetCommandSettings<UpdateCommandSettings>();
			this.ConvertHtmlToRtfIfNecessary(commandSettings.StoreObject as Item);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000269F0 File Offset: 0x00024BF0
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Item item = (Item)updateCommandSettings.StoreObject;
			this.SetProperty(setPropertyUpdate.ServiceObject, item, false, updateCommandSettings.FeaturesManager);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00026A20 File Offset: 0x00024C20
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Item xsoItem = (Item)updateCommandSettings.StoreObject;
			Body body = IrmUtils.GetBody(xsoItem);
			using (TextWriter textWriter = body.OpenTextWriter(BodyFormat.TextPlain))
			{
				textWriter.Write(string.Empty);
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00026A70 File Offset: 0x00024C70
		public override void AppendUpdate(AppendPropertyUpdate appendPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Item item = (Item)updateCommandSettings.StoreObject;
			this.SetProperty(appendPropertyUpdate.ServiceObject, item, true, null);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00026A98 File Offset: 0x00024C98
		internal void ConvertHtmlToRtfIfNecessary(Item item)
		{
			if (this.IsHtmlBody(item) && this.IsCalendaringItem(item))
			{
				string value;
				using (TextReader textReader = item.Body.OpenTextReader(item.Body.Format))
				{
					value = textReader.ReadToEnd();
				}
				BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(item.Body.Format);
				bodyWriteConfiguration.SetTargetFormat(BodyFormat.ApplicationRtf, item.Body.Charset);
				try
				{
					using (TextWriter textWriter = item.Body.OpenTextWriter(bodyWriteConfiguration))
					{
						textWriter.Write(value);
					}
				}
				catch (InvalidCharsetException innerException)
				{
					throw new ObjectCorruptException(innerException, true);
				}
				catch (TextConvertersException innerException2)
				{
					throw new ObjectCorruptException(innerException2, true);
				}
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00026B78 File Offset: 0x00024D78
		internal bool IsHtmlBody(Item item)
		{
			return item != null && item.Body != null && item.Body.Format == BodyFormat.TextHtml;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00026B95 File Offset: 0x00024D95
		internal bool IsCalendaringItem(Item item)
		{
			return item != null && (ObjectClass.IsCalendarItem(item.ClassName) || ObjectClass.IsMeetingMessage(item.ClassName) || ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(item.ClassName));
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00026BC4 File Offset: 0x00024DC4
		internal void WriteTextContent(TextWriter writer, object item, bool shouldUseNarrowGapForPTagHtmlToTextConversion)
		{
			Item xsoItem = BodyProperty.GetXsoItem(item);
			Body effectiveBody = Util.GetEffectiveBody(xsoItem);
			BodyReadConfiguration bodyReadConfiguration = new BodyReadConfiguration(BodyFormat.TextPlain);
			if (shouldUseNarrowGapForPTagHtmlToTextConversion)
			{
				bodyReadConfiguration.ShouldUseNarrowGapForPTagHtmlToTextConversion = true;
			}
			using (TextReader textReader = effectiveBody.OpenTextReader(bodyReadConfiguration))
			{
				BodyProperty.CopyContent(textReader, writer, this.charBuffer);
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00026C34 File Offset: 0x00024E34
		internal void WriteHtmlContent(TextWriter writer, object item, ItemResponseShape itemResponseShape)
		{
			bool filterHtmlContent = itemResponseShape.FilterHtmlContent;
			bool convertHtmlCodePageToUTF = itemResponseShape.ConvertHtmlCodePageToUTF8;
			Item xsoItem = BodyProperty.GetXsoItem(item);
			BodyReadConfiguration bodyReadConfiguration;
			if (convertHtmlCodePageToUTF)
			{
				bodyReadConfiguration = new BodyReadConfiguration(BodyFormat.TextHtml, "utf-8");
			}
			else
			{
				bodyReadConfiguration = new BodyReadConfiguration(BodyFormat.TextHtml);
			}
			if (filterHtmlContent)
			{
				bodyReadConfiguration.HtmlFlags |= HtmlStreamingFlags.FilterHtml;
			}
			else
			{
				bodyReadConfiguration.HtmlFlags &= ~HtmlStreamingFlags.FilterHtml;
			}
			if (!BodyProperty.IsFakedId(xsoItem))
			{
				xsoItem.Load(StoreObjectSchema.ContentConversionProperties);
				if (!string.IsNullOrEmpty(itemResponseShape.InlineImageUrlTemplate) || itemResponseShape.AddBlankTargetToLinks || itemResponseShape.BlockExternalImages)
				{
					BodyReadConfiguration bodyReadConfiguration2 = bodyReadConfiguration;
					HtmlBodyCallback htmlBodyCallback = new HtmlBodyCallback(xsoItem, null, false);
					htmlBodyCallback.AddBlankTargetToLinks = itemResponseShape.AddBlankTargetToLinks;
					htmlBodyCallback.InlineImageUrlTemplate = itemResponseShape.InlineImageUrlTemplate;
					htmlBodyCallback.InlineImageUrlOnLoadTemplate = itemResponseShape.InlineImageUrlOnLoadTemplate;
					htmlBodyCallback.InlineImageCustomDataTemplate = itemResponseShape.InlineImageCustomDataTemplate;
					htmlBodyCallback.IsBodyFragment = false;
					htmlBodyCallback.BlockExternalImages = itemResponseShape.BlockExternalImages;
					htmlBodyCallback.HasBlockedImagesAction = delegate(bool value)
					{
						EWSSettings.ItemHasBlockedImages = new bool?(value);
					};
					htmlBodyCallback.InlineAttachmentIdAction = this.inlineAttachmentAction;
					bodyReadConfiguration2.ConversionCallback = htmlBodyCallback;
				}
				else
				{
					bodyReadConfiguration.ConversionCallback = new DefaultHtmlCallbacks(xsoItem, true);
				}
			}
			try
			{
				Body effectiveBody = Util.GetEffectiveBody(xsoItem);
				if (itemResponseShape.UseSafeHtml)
				{
					bodyReadConfiguration.HtmlFlags = HtmlStreamingFlags.None;
					this.WriteBodyFromSafeHtml(writer, bodyReadConfiguration, effectiveBody, itemResponseShape.CssScopeClassName);
				}
				else if (string.IsNullOrEmpty(itemResponseShape.CssScopeClassName))
				{
					this.WriteHtmlBodyContent(writer, bodyReadConfiguration, effectiveBody);
				}
				else
				{
					this.WriteBodyWithScopedStyleSheets(writer, itemResponseShape, bodyReadConfiguration, effectiveBody);
				}
				FaultInjection.GenerateFault((FaultInjection.LIDs)3842387261U);
			}
			catch (TextConvertersException innerException)
			{
				throw new PropertyRequestFailedException(CoreResources.IDs.ErrorItemPropertyRequestFailed, this.commandContext.PropertyInformation.PropertyPath, innerException);
			}
			if (!BodyProperty.IsFakedId(xsoItem))
			{
				Dictionary<AttachmentId, AttachmentType> attachmentInformation = EWSSettings.AttachmentInformation;
				if (attachmentInformation != null)
				{
					foreach (AttachmentLink attachmentLink in bodyReadConfiguration.ConversionCallback.AttachmentLinks)
					{
						AttachmentType attachmentType;
						if (attachmentInformation.TryGetValue(attachmentLink.AttachmentId, out attachmentType))
						{
							attachmentType.ContentId = attachmentLink.ContentId;
						}
					}
				}
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00026E64 File Offset: 0x00025064
		private void WriteHtmlBodyContent(TextWriter writer, BodyReadConfiguration configuration, Body body)
		{
			using (Stream stream = body.OpenReadStream(configuration))
			{
				using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
				{
					BodyProperty.CopyContent(streamReader, writer, this.charBuffer);
				}
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00026EC8 File Offset: 0x000250C8
		private static bool IsFakedId(Item xsoItem)
		{
			return !PropertyCommand.InMemoryProcessOnly && (xsoItem.Id == null || xsoItem.Id.ObjectId.IsFakeId);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00026EF0 File Offset: 0x000250F0
		private static void CopyContent(TextReader reader, TextWriter writer, char[] buffer)
		{
			bool flag = false;
			int index = 0;
			int num = buffer.Length;
			int num2;
			while ((num2 = reader.Read(buffer, index, num)) > 0)
			{
				bool flag2 = false;
				if (num2 == num)
				{
					flag2 = char.IsHighSurrogate(buffer[buffer.Length - 1]);
				}
				if (flag)
				{
					num2++;
				}
				if (flag2)
				{
					BodyProperty.WriteChars(writer, buffer, num2 - 1);
					flag = true;
					buffer[0] = buffer[buffer.Length - 1];
					index = 1;
					num = buffer.Length - 1;
				}
				else
				{
					BodyProperty.WriteChars(writer, buffer, num2);
					flag = false;
					index = 0;
					num = buffer.Length;
				}
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00026F6C File Offset: 0x0002516C
		private static void WriteChars(TextWriter writer, char[] copyBuffer, int bytesRead)
		{
			try
			{
				writer.Write(copyBuffer, 0, bytesRead);
			}
			catch (ArgumentException ex)
			{
				ex.Data["NeverGenerateWatson"] = null;
				throw;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00026FA8 File Offset: 0x000251A8
		private static Item GetXsoItem(object item)
		{
			Item item2 = item as Item;
			if (item2 == null)
			{
				AttachmentHierarchy attachmentHierarchy = (AttachmentHierarchy)item;
				item2 = attachmentHierarchy.LastAsXsoItem;
			}
			return item2;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00026FD0 File Offset: 0x000251D0
		protected virtual BodyFormat ComputeBodyFormat(BodyResponseType bodyType, Item item)
		{
			switch (bodyType)
			{
			case BodyResponseType.Best:
				return Util.GetEffectiveBody(item).Format;
			case BodyResponseType.HTML:
				return BodyFormat.TextHtml;
			}
			return BodyFormat.TextPlain;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00027004 File Offset: 0x00025204
		private void SetProperty(ServiceObject serviceObject, Item item, bool isAppend, IFeaturesManager featuresManager)
		{
			BodyContentType valueOrDefault = serviceObject.GetValueOrDefault<BodyContentType>(this.commandContext.PropertyInformation);
			string body = string.IsNullOrEmpty(valueOrDefault.Value) ? string.Empty : valueOrDefault.Value;
			this.SetProperty(body, valueOrDefault.BodyType, item, isAppend, featuresManager);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00027050 File Offset: 0x00025250
		private void SetProperty(string body, BodyType bodyType, Item item, bool isAppend, IFeaturesManager featuresManager)
		{
			if (bodyType == BodyType.HTML)
			{
				if (isAppend)
				{
					this.ValidateBodyFormat(item, BodyFormat.TextHtml);
					Body body2 = IrmUtils.GetBody(item);
					string str;
					using (TextReader textReader = body2.OpenTextReader(BodyFormat.TextHtml))
					{
						str = textReader.ReadToEnd();
					}
					body = str + body;
				}
				if (featuresManager != null && featuresManager.IsFeatureSupported("AttachmentsFilePicker"))
				{
					body = AttachmentLinksBuilder.InsertReferenceAttachmentLinks(body, item);
				}
				base.ValidateDataSize((long)body.Length);
				HtmlUpdateBodyCallback htmlUpdateBodyCallback = null;
				using (TextWriter textWriter = this.CreateHtmlTextWriter(item, out htmlUpdateBodyCallback))
				{
					textWriter.Write(body);
				}
				if (htmlUpdateBodyCallback != null)
				{
					htmlUpdateBodyCallback.SaveChanges();
					return;
				}
			}
			else
			{
				Body body3 = IrmUtils.GetBody(item);
				if (isAppend)
				{
					this.ValidateBodyFormat(item, BodyFormat.TextPlain);
					string str2;
					using (TextReader textReader2 = body3.OpenTextReader(BodyFormat.TextPlain))
					{
						str2 = textReader2.ReadToEnd();
					}
					body = str2 + body;
				}
				base.ValidateDataSize((long)body.Length);
				using (TextWriter textWriter2 = body3.OpenTextWriter(BodyFormat.TextPlain))
				{
					textWriter2.Write(body);
				}
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00027194 File Offset: 0x00025394
		private TextWriter CreateHtmlTextWriter(Item item, out HtmlUpdateBodyCallback htmlUpdateBodyCallback)
		{
			Body body = IrmUtils.GetBody(item);
			BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(BodyFormat.TextHtml);
			if (item.CharsetDetector != null && item.CharsetDetector.DetectionOptions != null && item.CharsetDetector.DetectionOptions.PreferredCharset != null)
			{
				bodyWriteConfiguration.SetTargetFormat(BodyFormat.TextHtml, item.CharsetDetector.DetectionOptions.PreferredCharset, item.CharsetDetector.CharsetFlags);
			}
			if (this.commandContext.CommandSettings is UpdateCommandSettings)
			{
				htmlUpdateBodyCallback = new HtmlUpdateBodyCallback(item);
			}
			else
			{
				htmlUpdateBodyCallback = null;
			}
			bodyWriteConfiguration.SetHtmlOptions(HtmlStreamingFlags.None, htmlUpdateBodyCallback);
			return body.OpenTextWriter(bodyWriteConfiguration);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00027228 File Offset: 0x00025428
		private void ValidateBodyFormat(Item item, BodyFormat expectedFormat)
		{
			Body body = IrmUtils.GetBody(item);
			if (body.Format != expectedFormat)
			{
				throw new InvalidPropertyAppendException(CoreResources.IDs.ErrorAppendBodyTypeMismatch, this.commandContext.PropertyInformation.PropertyPath);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00027268 File Offset: 0x00025468
		private void WriteBodyFromSafeHtml(TextWriter writer, BodyReadConfiguration configuration, Body body, string cssScopeClassName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = new StringWriter(stringBuilder))
			{
				this.WriteHtmlBodyContent(textWriter, configuration, body);
			}
			string text = BodyProperty.GetSafeHtml(stringBuilder.ToString(), cssScopeClassName);
			text = string.Concat(new string[]
			{
				"<div class=\"",
				cssScopeClassName,
				"\">\r\n",
				text,
				"</div>"
			});
			using (TextReader textReader = new StringReader(text))
			{
				BodyProperty.CopyContent(textReader, writer, this.charBuffer);
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00027318 File Offset: 0x00025518
		public static string GetSafeHtml(string htmlContent, string cssScopeClassName)
		{
			FilteringFlags filteringFlags = -1442835872;
			SafeHtmlContext safeHtmlContext = default(SafeHtmlContext);
			safeHtmlContext.CssClassScopeName = (string.IsNullOrEmpty(cssScopeClassName) ? string.Empty : ("." + cssScopeClassName));
			SafeHtmlContext safeHtmlContext2 = safeHtmlContext;
			string text;
			string text2;
			return SafeHtml.GetSafeHtml(htmlContent, ref safeHtmlContext2, ref text, ref text2, filteringFlags, null);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00027365 File Offset: 0x00025565
		void IToXmlCommand.ToXml()
		{
			throw new InvalidOperationException("Do not call this. It's obsolete");
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00027374 File Offset: 0x00025574
		void ISetCommand.Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			Item item = (Item)commandSettings.StoreObject;
			XmlElement serviceProperty = commandSettings.ServiceProperty;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			if (serviceObject != null)
			{
				this.SetProperty(serviceObject, item, false, null);
				return;
			}
			this.SetProperty(serviceProperty, item, false);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000273BC File Offset: 0x000255BC
		private static BodyType ParseBodyTypeAttribute(XmlElement elem)
		{
			string attribute = elem.GetAttribute("BodyType");
			if (!string.IsNullOrEmpty(attribute) && attribute.Equals("HTML"))
			{
				return BodyType.HTML;
			}
			return BodyType.Text;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000273F0 File Offset: 0x000255F0
		private void SetProperty(XmlElement serviceProperty, Item item, bool isAppend)
		{
			string xmlTextNodeValue = ServiceXml.GetXmlTextNodeValue(serviceProperty);
			BodyType bodyType = BodyProperty.ParseBodyTypeAttribute(serviceProperty);
			this.SetProperty(xmlTextNodeValue, bodyType, item, isAppend, null);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00027418 File Offset: 0x00025618
		private void ScopeStyleSheets(TextReader textReader, TextWriter textWriter, string className)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (HtmlReader htmlReader = new HtmlReader(textReader))
			{
				htmlReader.NormalizeHtml = true;
				htmlReader.InputEncoding = new UTF8Encoding();
				using (HtmlWriter htmlWriter = new HtmlWriter(textWriter))
				{
					htmlWriter.WriteStartTag(HtmlTagId.Div);
					htmlWriter.WriteAttribute(HtmlAttributeId.Class, className);
					bool flag = false;
					while (htmlReader.ReadNextToken())
					{
						switch (htmlReader.TokenKind)
						{
						case HtmlTokenKind.Text:
							if (flag)
							{
								BodyProperty.AppendText(this.charBuffer, stringBuilder, htmlReader);
							}
							else
							{
								htmlWriter.WriteText(htmlReader);
							}
							break;
						case HtmlTokenKind.StartTag:
							if (htmlReader.TagId == HtmlTagId.Style)
							{
								flag = true;
							}
							else if (htmlReader.TagId == HtmlTagId.Body)
							{
								HtmlAttributeReader attributeReader = htmlReader.AttributeReader;
								htmlWriter.WriteStartTag(HtmlTagId.Div);
								while (attributeReader.ReadNext())
								{
									string key = attributeReader.ReadName();
									if (attributeReader.HasValue && BodyProperty.bodyAttrsToCopy.ContainsKey(key))
									{
										htmlWriter.WriteAttribute(BodyProperty.bodyAttrsToCopy[key], attributeReader.ReadValue());
									}
								}
							}
							else if (!BodyProperty.tagsToIgnore.Contains(htmlReader.TagId))
							{
								htmlWriter.WriteTag(htmlReader);
							}
							break;
						case HtmlTokenKind.EndTag:
							if (htmlReader.TagId == HtmlTagId.Style)
							{
								CssStyleSheet cssStyleSheet = CssParser.Parse(stringBuilder.ToString());
								cssStyleSheet.SanitizeRules();
								cssStyleSheet.ScopeRulesToClass(className);
								BodyProperty.WriteStyleSheet(htmlWriter, cssStyleSheet);
								flag = false;
							}
							else if (!BodyProperty.tagsToIgnore.Contains(htmlReader.TagId))
							{
								htmlWriter.WriteTag(htmlReader);
							}
							break;
						case HtmlTokenKind.EmptyElementTag:
							if (!BodyProperty.tagsToIgnore.Contains(htmlReader.TagId))
							{
								htmlWriter.WriteTag(htmlReader);
							}
							break;
						}
					}
					htmlWriter.WriteEndTag(HtmlTagId.Div);
				}
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000275FC File Offset: 0x000257FC
		private static void AppendText(char[] buffer, StringBuilder stringBuilder, HtmlReader reader)
		{
			int num;
			do
			{
				num = reader.ReadText(buffer, 0, buffer.Length);
				if (num > 0)
				{
					stringBuilder.Append(buffer, 0, num);
				}
			}
			while (num > 0);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00027629 File Offset: 0x00025829
		private static void WriteStyleSheet(HtmlWriter writer, CssStyleSheet styleSheet)
		{
			writer.WriteStartTag(HtmlTagId.Style);
			writer.WriteAttribute(HtmlAttributeId.Type, "text/css");
			writer.WriteText(string.Format("<!-- {0} -->", styleSheet.ToString()));
			writer.WriteEndTag(HtmlTagId.Style);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00027660 File Offset: 0x00025860
		private void WriteBodyWithScopedStyleSheets(TextWriter writer, ItemResponseShape itemResponseShape, BodyReadConfiguration configuration, Body body)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = new StringWriter(stringBuilder))
			{
				this.WriteHtmlBodyContent(textWriter, configuration, body);
			}
			new StringBuilder();
			using (TextReader textReader = new StringReader(stringBuilder.ToString()))
			{
				this.ScopeStyleSheets(textReader, writer, itemResponseShape.CssScopeClassName);
			}
		}

		// Token: 0x04000704 RID: 1796
		private const string HtmlEntityPlusSign = "&#43;";

		// Token: 0x04000705 RID: 1797
		private char[] charBuffer;

		// Token: 0x04000706 RID: 1798
		private Action<string> inlineAttachmentAction;

		// Token: 0x04000707 RID: 1799
		private static readonly HashSet<HtmlTagId> tagsToIgnore = new HashSet<HtmlTagId>
		{
			HtmlTagId.Html,
			HtmlTagId.Head,
			HtmlTagId.Title,
			HtmlTagId.Base,
			HtmlTagId.Link,
			HtmlTagId.Meta,
			HtmlTagId.Script
		};

		// Token: 0x04000708 RID: 1800
		private static readonly Dictionary<string, HtmlAttributeId> bodyAttrsToCopy = new Dictionary<string, HtmlAttributeId>
		{
			{
				"style",
				HtmlAttributeId.Style
			},
			{
				"dir",
				HtmlAttributeId.Dir
			},
			{
				"lang",
				HtmlAttributeId.Lang
			}
		};
	}
}
