using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.TextConverters.Internal.Css;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001D5 RID: 469
	internal class HtmlToHtmlConverter : IProducerConsumer, IRestartable, IDisposable
	{
		// Token: 0x0600148D RID: 5261 RVA: 0x00092AF0 File Offset: 0x00090CF0
		public HtmlToHtmlConverter(IHtmlParser parser, HtmlWriter writer, bool convertFragment, bool outputFragment, bool filterHtml, HtmlTagCallback callback, bool truncateForCallback, bool hasTailInjection, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum, int smallCssBlockThreshold, bool preserveDisplayNoneStyle, IProgressMonitor progressMonitor)
		{
			this.writer = writer;
			this.normalizedInput = (parser is HtmlNormalizingParser);
			this.progressMonitor = progressMonitor;
			this.convertFragment = convertFragment;
			this.outputFragment = outputFragment;
			this.filterForFragment = (outputFragment || convertFragment);
			this.filterHtml = (filterHtml || this.filterForFragment);
			this.callback = callback;
			this.parser = parser;
			if (!convertFragment)
			{
				this.parser.SetRestartConsumer(this);
			}
			this.truncateForCallback = truncateForCallback;
			this.hasTailInjection = hasTailInjection;
			this.smallCssBlockThreshold = smallCssBlockThreshold;
			this.preserveDisplayNoneStyle = preserveDisplayNoneStyle;
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00092B9A File Offset: 0x00090D9A
		internal HtmlToken InternalToken
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00092BA2 File Offset: 0x00090DA2
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x00092BAA File Offset: 0x00090DAA
		private HtmlToHtmlConverter.CopyPendingState CopyPendingStateFlag
		{
			get
			{
				return this.copyPendingState;
			}
			set
			{
				this.writer.SetCopyPending(value != HtmlToHtmlConverter.CopyPendingState.NotPending);
				this.copyPendingState = value;
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00092BC8 File Offset: 0x00090DC8
		void IDisposable.Dispose()
		{
			if (this.parser != null && this.parser is IDisposable)
			{
				((IDisposable)this.parser).Dispose();
			}
			if (!this.convertFragment && this.writer != null && this.writer != null)
			{
				((IDisposable)this.writer).Dispose();
			}
			if (this.token != null && this.token is IDisposable)
			{
				((IDisposable)this.token).Dispose();
			}
			this.parser = null;
			this.writer = null;
			this.token = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00092C60 File Offset: 0x00090E60
		public void Run()
		{
			if (!this.endOfFile)
			{
				HtmlTokenId htmlTokenId = this.parser.Parse();
				if (htmlTokenId != HtmlTokenId.None)
				{
					this.Process(htmlTokenId);
				}
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00092C8B File Offset: 0x00090E8B
		public bool Flush()
		{
			if (!this.endOfFile)
			{
				this.Run();
			}
			return this.endOfFile;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00092CA1 File Offset: 0x00090EA1
		public void Initialize(string fragment, bool preformatedText)
		{
			if (this.parser is HtmlNormalizingParser)
			{
				((HtmlNormalizingParser)this.parser).Initialize(fragment, preformatedText);
			}
			else
			{
				((HtmlParser)this.parser).Initialize(fragment, preformatedText);
			}
			((IRestartable)this).Restart();
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00092CDC File Offset: 0x00090EDC
		bool IRestartable.CanRestart()
		{
			return this.writer != null && ((IRestartable)this.writer).CanRestart();
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00092CF4 File Offset: 0x00090EF4
		void IRestartable.Restart()
		{
			if (this.writer != null && !this.convertFragment)
			{
				((IRestartable)this.writer).Restart();
			}
			this.endOfFile = false;
			this.token = null;
			this.styleIsCSS = true;
			this.insideCSS = false;
			this.headDivUnterminated = false;
			this.tagDropped = false;
			this.justTruncated = false;
			this.tagCallbackRequested = false;
			this.endTagCallbackRequested = false;
			this.ignoreAttrCallback = false;
			this.attrContinuationAction = HtmlFilterData.FilterAction.Unknown;
			this.currentLevel = 0;
			this.currentLevelDelta = 0;
			this.dropLevel = int.MaxValue;
			this.endTagActionStackTop = 0;
			this.copyPendingState = HtmlToHtmlConverter.CopyPendingState.NotPending;
			this.metaInjected = false;
			this.insideHtml = false;
			this.insideHead = false;
			this.insideBody = false;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00092DAC File Offset: 0x00090FAC
		void IRestartable.DisableRestart()
		{
			if (this.writer != null)
			{
				((IRestartable)this.writer).DisableRestart();
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00092DC1 File Offset: 0x00090FC1
		internal static void RefreshConfiguration()
		{
			HtmlToHtmlConverter.textConvertersConfigured = false;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00092DC9 File Offset: 0x00090FC9
		internal static bool TestSafeUrlSchema(string schema)
		{
			if (schema.Length < 2 || schema.Length > 20)
			{
				return false;
			}
			if (!HtmlToHtmlConverter.textConvertersConfigured)
			{
				HtmlToHtmlConverter.ConfigureTextConverters();
			}
			return HtmlToHtmlConverter.safeUrlDictionary.ContainsKey(schema);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00092DFC File Offset: 0x00090FFC
		internal static bool IsUrlSafe(string url, bool callbackRequested)
		{
			char[] array = url.ToCharArray();
			switch (HtmlToHtmlConverter.CheckUrl(array, array.Length, callbackRequested))
			{
			case HtmlToHtmlConverter.CheckUrlResult.Inconclusive:
			case HtmlToHtmlConverter.CheckUrlResult.Safe:
			case HtmlToHtmlConverter.CheckUrlResult.LocalHyperlink:
				return true;
			}
			return false;
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00092E38 File Offset: 0x00091038
		internal void CopyInputTag(bool copyTagAttributes)
		{
			if (this.token.IsTagBegin)
			{
				this.writer.WriteTagBegin(HtmlDtd.tags[(int)this.tagIndex].NameIndex, null, this.token.IsEndTag, this.token.IsAllowWspLeft, this.token.IsAllowWspRight);
			}
			if (this.tagIndex <= HtmlTagIndex.Unknown)
			{
				if (this.tagIndex < HtmlTagIndex.Unknown)
				{
					this.token.UnstructuredContent.WriteTo(this.writer.WriteUnstructuredTagContent());
					if (this.token.IsTagEnd)
					{
						this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.NotPending;
						return;
					}
					this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.TagCopyPending;
					return;
				}
				else if (this.token.HasNameFragment)
				{
					this.token.Name.WriteTo(this.writer.WriteTagName());
					if (!this.token.IsTagNameEnd && !copyTagAttributes)
					{
						this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.TagNameCopyPending;
						return;
					}
				}
			}
			if (!copyTagAttributes)
			{
				this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.NotPending;
				return;
			}
			if (this.attributeCount != 0)
			{
				this.CopyInputTagAttributes();
			}
			if (this.token.IsTagEnd)
			{
				this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.NotPending;
				return;
			}
			this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.TagCopyPending;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00092F58 File Offset: 0x00091158
		internal void CopyInputAttribute(int index)
		{
			HtmlToHtmlConverter.AttributeIndirectKind attributeIndirectKind = this.GetAttributeIndirectKind(index);
			if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.FilteredStyle)
			{
				if (!this.tagCallbackRequested)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Style);
					if (!this.cssParserInput.IsEmpty)
					{
						this.FlushCssInStyleAttribute(this.writer);
						return;
					}
					this.writer.WriteAttributeValueInternal(string.Empty);
					return;
				}
				else
				{
					this.VirtualizeFilteredStyle(index);
					attributeIndirectKind = HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle;
				}
			}
			bool flag = true;
			if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle)
			{
				this.writer.WriteAttributeName(HtmlNameIndex.Style);
				int attributeVirtualEntryIndex = this.GetAttributeVirtualEntryIndex(index);
				if (this.attributeVirtualList[attributeVirtualEntryIndex].Length != 0)
				{
					this.writer.WriteAttributeValueInternal(this.attributeVirtualScratch.Buffer, this.attributeVirtualList[attributeVirtualEntryIndex].Offset, this.attributeVirtualList[attributeVirtualEntryIndex].Length);
				}
				else
				{
					this.writer.WriteAttributeValueInternal(string.Empty);
				}
			}
			else
			{
				HtmlAttribute attribute = this.GetAttribute(index);
				if (attribute.IsAttrBegin && attribute.NameIndex != HtmlNameIndex.Unknown)
				{
					this.writer.WriteAttributeName(attribute.NameIndex);
				}
				if (attribute.NameIndex == HtmlNameIndex.Unknown && (attribute.HasNameFragment || attribute.IsAttrBegin))
				{
					attribute.Name.WriteTo(this.writer.WriteAttributeName());
				}
				if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.NameOnlyFragment)
				{
					flag = false;
				}
				else if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.EmptyValue)
				{
					this.writer.WriteAttributeValueInternal(string.Empty);
				}
				else if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.Virtual)
				{
					int attributeVirtualEntryIndex2 = this.GetAttributeVirtualEntryIndex(index);
					if (this.attributeVirtualList[attributeVirtualEntryIndex2].Length != 0)
					{
						this.writer.WriteAttributeValueInternal(this.attributeVirtualScratch.Buffer, this.attributeVirtualList[attributeVirtualEntryIndex2].Offset, this.attributeVirtualList[attributeVirtualEntryIndex2].Length);
					}
					else
					{
						this.writer.WriteAttributeValueInternal(string.Empty);
					}
				}
				else
				{
					if (attribute.HasValueFragment)
					{
						attribute.Value.WriteTo(this.writer.WriteAttributeValue());
					}
					flag = attribute.IsAttrEnd;
				}
			}
			if (flag)
			{
				this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.NotPending;
				return;
			}
			this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.AttributeCopyPending;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00093170 File Offset: 0x00091370
		internal void CopyInputAttributeName(int index)
		{
			HtmlToHtmlConverter.AttributeIndirectKind attributeIndirectKind = this.GetAttributeIndirectKind(index);
			if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.FilteredStyle || attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle)
			{
				this.writer.WriteAttributeName(HtmlNameIndex.Style);
				return;
			}
			HtmlAttribute attribute = this.GetAttribute(index);
			if (attribute.IsAttrBegin && attribute.NameIndex != HtmlNameIndex.Unknown)
			{
				this.writer.WriteAttributeName(attribute.NameIndex);
			}
			if (attribute.NameIndex == HtmlNameIndex.Unknown && (attribute.HasNameFragment || attribute.IsAttrBegin))
			{
				attribute.Name.WriteTo(this.writer.WriteAttributeName());
			}
			if (attribute.IsAttrNameEnd)
			{
				this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.NotPending;
				return;
			}
			this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.AttributeNameCopyPending;
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00093218 File Offset: 0x00091418
		internal void CopyInputAttributeValue(int index)
		{
			HtmlToHtmlConverter.AttributeIndirectKind attributeIndirectKind = this.GetAttributeIndirectKind(index);
			bool flag = true;
			if (attributeIndirectKind != HtmlToHtmlConverter.AttributeIndirectKind.PassThrough)
			{
				if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.FilteredStyle)
				{
					if (!this.tagCallbackRequested)
					{
						if (!this.cssParserInput.IsEmpty)
						{
							this.FlushCssInStyleAttribute(this.writer);
							return;
						}
						this.writer.WriteAttributeValueInternal(string.Empty);
						return;
					}
					else
					{
						this.VirtualizeFilteredStyle(index);
						attributeIndirectKind = HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle;
					}
				}
				if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.Virtual || attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle)
				{
					int attributeVirtualEntryIndex = this.GetAttributeVirtualEntryIndex(index);
					if (this.attributeVirtualList[attributeVirtualEntryIndex].Length != 0)
					{
						this.writer.WriteAttributeValueInternal(this.attributeVirtualScratch.Buffer, this.attributeVirtualList[attributeVirtualEntryIndex].Offset, this.attributeVirtualList[attributeVirtualEntryIndex].Length);
					}
					else
					{
						this.writer.WriteAttributeValueInternal(string.Empty);
					}
				}
				else if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.NameOnlyFragment)
				{
					flag = false;
				}
				else if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.EmptyValue)
				{
					this.writer.WriteAttributeValueInternal(string.Empty);
				}
			}
			else
			{
				HtmlAttribute attribute = this.GetAttribute(index);
				if (attribute.HasValueFragment)
				{
					attribute.Value.WriteTo(this.writer.WriteAttributeValue());
				}
				flag = attribute.IsAttrEnd;
			}
			if (flag)
			{
				this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.NotPending;
				return;
			}
			this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.AttributeValueCopyPending;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00093348 File Offset: 0x00091548
		internal HtmlAttributeId GetAttributeNameId(int index)
		{
			HtmlToHtmlConverter.AttributeIndirectKind attributeIndirectKind = this.GetAttributeIndirectKind(index);
			if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.FilteredStyle || attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle)
			{
				return HtmlAttributeId.Style;
			}
			HtmlAttribute attribute = this.GetAttribute(index);
			return HtmlNameData.Names[(int)attribute.NameIndex].PublicAttributeId;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00093388 File Offset: 0x00091588
		internal HtmlAttributeParts GetAttributeParts(int index)
		{
			HtmlToHtmlConverter.AttributeIndirectKind attributeIndirectKind = this.GetAttributeIndirectKind(index);
			if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.FilteredStyle || attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle)
			{
				return HtmlToHtmlConverter.CompleteAttributeParts;
			}
			HtmlAttribute attribute = this.GetAttribute(index);
			if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.NameOnlyFragment)
			{
				return new HtmlAttributeParts(attribute.MajorPart, attribute.MinorPart & (HtmlToken.AttrPartMinor)199);
			}
			if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.EmptyValue || attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.Virtual)
			{
				return new HtmlAttributeParts(attribute.MajorPart | HtmlToken.AttrPartMajor.End, attribute.MinorPart | HtmlToken.AttrPartMinor.CompleteValue);
			}
			return new HtmlAttributeParts(attribute.MajorPart, attribute.MinorPart);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0009340C File Offset: 0x0009160C
		internal string GetAttributeName(int index)
		{
			HtmlToHtmlConverter.AttributeIndirectKind attributeIndirectKind = this.GetAttributeIndirectKind(index);
			if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.FilteredStyle || attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle)
			{
				return HtmlNameData.Names[40].Name;
			}
			HtmlAttribute attribute = this.GetAttribute(index);
			if (attribute.NameIndex > HtmlNameIndex.Unknown)
			{
				if (!attribute.IsAttrBegin)
				{
					return string.Empty;
				}
				return HtmlNameData.Names[(int)attribute.NameIndex].Name;
			}
			else
			{
				if (attribute.HasNameFragment)
				{
					return attribute.Name.GetString(int.MaxValue);
				}
				if (!attribute.IsAttrBegin)
				{
					return string.Empty;
				}
				return "?";
			}
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x000934A8 File Offset: 0x000916A8
		internal string GetAttributeValue(int index)
		{
			HtmlToHtmlConverter.AttributeIndirectKind attributeIndirectKind = this.GetAttributeIndirectKind(index);
			if (attributeIndirectKind != HtmlToHtmlConverter.AttributeIndirectKind.PassThrough)
			{
				if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.FilteredStyle)
				{
					this.VirtualizeFilteredStyle(index);
					attributeIndirectKind = HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle;
				}
				if (attributeIndirectKind != HtmlToHtmlConverter.AttributeIndirectKind.Virtual && attributeIndirectKind != HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle)
				{
					return string.Empty;
				}
				int attributeVirtualEntryIndex = this.GetAttributeVirtualEntryIndex(index);
				if (this.attributeVirtualList[attributeVirtualEntryIndex].Length != 0)
				{
					return new string(this.attributeVirtualScratch.Buffer, this.attributeVirtualList[attributeVirtualEntryIndex].Offset, this.attributeVirtualList[attributeVirtualEntryIndex].Length);
				}
				return string.Empty;
			}
			else
			{
				HtmlAttribute attribute = this.GetAttribute(index);
				if (!attribute.HasValueFragment)
				{
					return string.Empty;
				}
				return attribute.Value.GetString(int.MaxValue);
			}
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0009355C File Offset: 0x0009175C
		internal int ReadAttributeValue(int index, char[] buffer, int offset, int count)
		{
			HtmlToHtmlConverter.AttributeIndirectKind attributeIndirectKind = this.GetAttributeIndirectKind(index);
			if (attributeIndirectKind != HtmlToHtmlConverter.AttributeIndirectKind.PassThrough)
			{
				if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.FilteredStyle)
				{
					this.VirtualizeFilteredStyle(index);
					attributeIndirectKind = HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle;
				}
				if (attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.Virtual || attributeIndirectKind == HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle)
				{
					int attributeVirtualEntryIndex = this.GetAttributeVirtualEntryIndex(index);
					int num = Math.Min(this.attributeVirtualList[attributeVirtualEntryIndex].Length - this.attributeVirtualList[attributeVirtualEntryIndex].Position, count);
					if (num != 0)
					{
						Buffer.BlockCopy(this.attributeVirtualScratch.Buffer, 2 * (this.attributeVirtualList[attributeVirtualEntryIndex].Offset + this.attributeVirtualList[attributeVirtualEntryIndex].Position), buffer, offset, 2 * num);
						HtmlToHtmlConverter.AttributeVirtualEntry[] array = this.attributeVirtualList;
						int num2 = attributeVirtualEntryIndex;
						array[num2].Position = array[num2].Position + num;
					}
					return num;
				}
				return 0;
			}
			else
			{
				HtmlAttribute attribute = this.GetAttribute(index);
				if (!attribute.HasValueFragment)
				{
					return 0;
				}
				return attribute.Value.Read(buffer, offset, count);
			}
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00093643 File Offset: 0x00091843
		internal void WriteTag(bool copyTagAttributes)
		{
			this.CopyInputTag(copyTagAttributes);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0009364C File Offset: 0x0009184C
		internal void WriteAttribute(int index, bool writeName, bool writeValue)
		{
			if (!writeName)
			{
				if (writeValue)
				{
					this.CopyInputAttributeValue(index);
				}
				return;
			}
			if (writeValue)
			{
				this.CopyInputAttribute(index);
				return;
			}
			this.CopyInputAttributeName(index);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00093670 File Offset: 0x00091870
		private static int WhitespaceLength(char[] buffer, int offset, int remainingLength)
		{
			int num = 0;
			while (remainingLength != 0 && ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(buffer[offset++])))
			{
				num++;
				remainingLength--;
			}
			return num;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000936A4 File Offset: 0x000918A4
		private static int NonWhitespaceLength(char[] buffer, int offset, int remainingLength)
		{
			int num = 0;
			while (remainingLength != 0 && !ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(buffer[offset++])))
			{
				num++;
				remainingLength--;
			}
			return num;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000936E0 File Offset: 0x000918E0
		private static void ConfigureTextConverters()
		{
			lock (HtmlToHtmlConverter.lockObject)
			{
				if (!HtmlToHtmlConverter.textConvertersConfigured)
				{
					HtmlToHtmlConverter.safeUrlDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
					bool flag2 = false;
					bool flag3 = true;
					CtsConfigurationSetting simpleConfigurationSetting = ApplicationServices.GetSimpleConfigurationSetting("TextConverters", "SafeUrlScheme");
					if (simpleConfigurationSetting != null)
					{
						if (simpleConfigurationSetting.Arguments.Count != 1 || (!simpleConfigurationSetting.Arguments[0].Name.Equals("Add", StringComparison.OrdinalIgnoreCase) && !simpleConfigurationSetting.Arguments[0].Name.Equals("Override", StringComparison.OrdinalIgnoreCase)))
						{
							ApplicationServices.Provider.LogConfigurationErrorEvent();
						}
						else
						{
							flag3 = simpleConfigurationSetting.Arguments[0].Name.Equals("Add", StringComparison.OrdinalIgnoreCase);
							string[] array = simpleConfigurationSetting.Arguments[0].Value.Split(new char[]
							{
								',',
								' ',
								';',
								':'
							}, StringSplitOptions.RemoveEmptyEntries);
							bool flag4 = false;
							foreach (string text in array)
							{
								string text2 = text.Trim().ToLower();
								bool flag5 = false;
								foreach (char c in text2)
								{
									if (c > '\u007f' || (!char.IsLetterOrDigit(c) && c != '_' && c != '-' && c != '+'))
									{
										flag4 = true;
										flag5 = true;
										break;
									}
								}
								if (!flag5 && !HtmlToHtmlConverter.safeUrlDictionary.ContainsKey(text2))
								{
									HtmlToHtmlConverter.safeUrlDictionary.Add(text2, null);
								}
							}
							if (flag4)
							{
								ApplicationServices.Provider.LogConfigurationErrorEvent();
							}
							flag2 = true;
						}
					}
					if (!flag2 || flag3)
					{
						HtmlToHtmlConverter.safeUrlDictionary["http"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["https"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["ftp"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["file"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["mailto"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["news"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["gopher"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["about"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["wais"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["cid"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["mhtml"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["ipp"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["msdaipp"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["meet"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["tel"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["sip"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["conf"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["im"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["callto"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["notes"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["onenote"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["groove"] = null;
						HtmlToHtmlConverter.safeUrlDictionary["mms"] = null;
					}
					HtmlToHtmlConverter.textConvertersConfigured = true;
				}
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00093A30 File Offset: 0x00091C30
		private static bool SafeUrlSchema(char[] urlBuffer, int schemaLength)
		{
			if (schemaLength < 2 || schemaLength > 20)
			{
				return false;
			}
			if (!HtmlToHtmlConverter.textConvertersConfigured)
			{
				HtmlToHtmlConverter.ConfigureTextConverters();
			}
			return HtmlToHtmlConverter.safeUrlDictionary.ContainsKey(new string(urlBuffer, 0, schemaLength));
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00093A60 File Offset: 0x00091C60
		private static HtmlToHtmlConverter.CheckUrlResult CheckUrl(char[] urlBuffer, int urlLength, bool callbackRequested)
		{
			if (urlLength > 0 && urlBuffer[0] == '#')
			{
				return HtmlToHtmlConverter.CheckUrlResult.LocalHyperlink;
			}
			if (urlLength > 10)
			{
				BufferString bufferString = new BufferString(urlBuffer, 0, 10);
				if (bufferString.EqualsToLowerCaseStringIgnoreCase("data:image"))
				{
					if (!callbackRequested)
					{
						return HtmlToHtmlConverter.CheckUrlResult.Unsafe;
					}
					return HtmlToHtmlConverter.CheckUrlResult.Safe;
				}
			}
			int i = 0;
			while (i < urlLength)
			{
				if (urlBuffer[i] == '/' || urlBuffer[i] == '\\')
				{
					if (i != 0 || urlLength <= 1 || (urlBuffer[1] != '/' && urlBuffer[1] != '\\'))
					{
						return HtmlToHtmlConverter.CheckUrlResult.Safe;
					}
					if (!callbackRequested)
					{
						return HtmlToHtmlConverter.CheckUrlResult.Unsafe;
					}
					return HtmlToHtmlConverter.CheckUrlResult.Safe;
				}
				else
				{
					if (urlBuffer[i] == '?' || urlBuffer[i] == '#' || urlBuffer[i] == ';')
					{
						return HtmlToHtmlConverter.CheckUrlResult.Safe;
					}
					if (urlBuffer[i] == ':')
					{
						if (HtmlToHtmlConverter.SafeUrlSchema(urlBuffer, i))
						{
							return HtmlToHtmlConverter.CheckUrlResult.Safe;
						}
						if (callbackRequested)
						{
							if (i == 1 && urlLength > 2 && ParseSupport.AlphaCharacter(ParseSupport.GetCharClass(urlBuffer[0])) && (urlBuffer[2] == '/' || urlBuffer[2] == '\\'))
							{
								return HtmlToHtmlConverter.CheckUrlResult.Safe;
							}
							BufferString bufferString2 = new BufferString(urlBuffer, 0, urlLength);
							if (bufferString2.EqualsToLowerCaseStringIgnoreCase("objattph://") || bufferString2.EqualsToLowerCaseStringIgnoreCase("rtfimage://"))
							{
								return HtmlToHtmlConverter.CheckUrlResult.Safe;
							}
						}
						return HtmlToHtmlConverter.CheckUrlResult.Unsafe;
					}
					else
					{
						i++;
					}
				}
			}
			return HtmlToHtmlConverter.CheckUrlResult.Inconclusive;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00093B5C File Offset: 0x00091D5C
		private static void CopyInputCssProperty(CssProperty property, ITextSinkEx sink)
		{
			if (property.IsPropertyBegin && property.NameId != CssNameIndex.Unknown)
			{
				sink.Write(CssData.names[(int)property.NameId].Name);
			}
			if (property.NameId == CssNameIndex.Unknown && property.HasNameFragment)
			{
				property.Name.WriteOriginalTo(sink);
			}
			if (property.IsPropertyNameEnd)
			{
				sink.Write(":");
			}
			if (property.HasValueFragment)
			{
				property.Value.WriteEscapedOriginalTo(sink);
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00093BE8 File Offset: 0x00091DE8
		private void Process(HtmlTokenId tokenId)
		{
			this.token = this.parser.Token;
			if (!this.metaInjected && !this.InjectMetaTagIfNecessary())
			{
				return;
			}
			switch (tokenId)
			{
			case HtmlTokenId.EndOfFile:
				this.ProcessEof();
				break;
			case HtmlTokenId.Text:
				this.ProcessText();
				return;
			case HtmlTokenId.EncodingChange:
				if (this.writer.HasEncoding && this.writer.CodePageSameAsInput)
				{
					this.writer.Encoding = this.token.TokenEncoding;
					return;
				}
				break;
			case HtmlTokenId.Tag:
				if (!this.token.IsEndTag)
				{
					this.ProcessStartTag();
					return;
				}
				this.ProcessEndTag();
				return;
			case HtmlTokenId.Restart:
				break;
			case HtmlTokenId.OverlappedClose:
				this.ProcessOverlappedClose();
				return;
			case HtmlTokenId.OverlappedReopen:
				this.ProcessOverlappedReopen();
				return;
			case HtmlTokenId.InjectionBegin:
				this.ProcessInjectionBegin();
				return;
			case HtmlTokenId.InjectionEnd:
				this.ProcessInjectionEnd();
				return;
			default:
				return;
			}
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00093CBC File Offset: 0x00091EBC
		private void ProcessStartTag()
		{
			HtmlToHtmlConverter.AvailableTagParts availableTagParts = HtmlToHtmlConverter.AvailableTagParts.None;
			if (this.insideCSS && this.token.TagIndex == HtmlTagIndex._COMMENT && this.filterHtml)
			{
				this.AppendCssFromTokenText();
				return;
			}
			if (this.token.IsTagBegin)
			{
				this.currentLevel++;
				this.tagIndex = this.token.TagIndex;
				this.tagDropped = false;
				this.justTruncated = false;
				this.endTagCallbackRequested = false;
				this.PreProcessStartTag();
				if (this.currentLevel >= this.dropLevel)
				{
					this.tagDropped = true;
				}
				else if (!this.tagDropped)
				{
					this.tagCallbackRequested = false;
					this.ignoreAttrCallback = false;
					if (this.filterHtml || this.callback != null)
					{
						HtmlFilterData.FilterAction filterAction = this.filterForFragment ? HtmlFilterData.filterInstructions[(int)this.token.NameIndex].tagFragmentAction : HtmlFilterData.filterInstructions[(int)this.token.NameIndex].tagAction;
						if (this.callback != null && (byte)(filterAction & HtmlFilterData.FilterAction.Callback) != 0)
						{
							this.tagCallbackRequested = true;
						}
						else if (this.filterHtml)
						{
							this.ignoreAttrCallback = (0 != (byte)(filterAction & HtmlFilterData.FilterAction.IgnoreAttrCallbacks));
							switch ((byte)(filterAction & HtmlFilterData.FilterAction.ActionMask))
							{
							case 1:
								this.tagDropped = true;
								this.dropLevel = this.currentLevel;
								break;
							case 2:
								this.tagDropped = true;
								break;
							case 3:
								this.dropLevel = this.currentLevel + 1;
								break;
							}
						}
					}
					if (!this.tagDropped)
					{
						this.attributeTriggeredCallback = false;
						this.tagHasFilteredStyleAttribute = false;
						availableTagParts = HtmlToHtmlConverter.AvailableTagParts.TagBegin;
					}
				}
			}
			if (!this.tagDropped)
			{
				HtmlToken.TagPartMinor tagPartMinor = this.token.MinorPart;
				if (this.token.IsTagEnd)
				{
					availableTagParts |= HtmlToHtmlConverter.AvailableTagParts.TagEnd;
				}
				if (this.tagIndex < HtmlTagIndex.Unknown)
				{
					availableTagParts |= HtmlToHtmlConverter.AvailableTagParts.UnstructuredContent;
					this.attributeCount = 0;
				}
				else
				{
					if (this.token.HasNameFragment || this.token.IsTagNameEnd)
					{
						availableTagParts |= HtmlToHtmlConverter.AvailableTagParts.TagName;
					}
					this.ProcessTagAttributes();
					if (this.attributeCount != 0)
					{
						availableTagParts |= HtmlToHtmlConverter.AvailableTagParts.Attributes;
					}
				}
				if (availableTagParts != HtmlToHtmlConverter.AvailableTagParts.None)
				{
					if (this.CopyPendingStateFlag != HtmlToHtmlConverter.CopyPendingState.NotPending)
					{
						switch (this.CopyPendingStateFlag)
						{
						case HtmlToHtmlConverter.CopyPendingState.TagCopyPending:
							this.CopyInputTag(true);
							if (this.tagCallbackRequested && (byte)(availableTagParts & HtmlToHtmlConverter.AvailableTagParts.TagEnd) != 0)
							{
								this.attributeCount = 0;
								this.token.Name.MakeEmpty();
								availableTagParts &= ~HtmlToHtmlConverter.AvailableTagParts.TagEnd;
							}
							else
							{
								availableTagParts = HtmlToHtmlConverter.AvailableTagParts.None;
							}
							break;
						case HtmlToHtmlConverter.CopyPendingState.TagNameCopyPending:
							this.token.Name.WriteTo(this.writer.WriteTagName());
							if (this.token.IsTagNameEnd)
							{
								this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.NotPending;
							}
							this.token.Name.MakeEmpty();
							availableTagParts &= ~HtmlToHtmlConverter.AvailableTagParts.TagName;
							tagPartMinor &= (HtmlToken.TagPartMinor)248;
							break;
						case HtmlToHtmlConverter.CopyPendingState.AttributeCopyPending:
							this.CopyInputAttribute(0);
							this.attributeSkipCount = 1;
							this.attributeCount--;
							if (this.attributeCount == 0)
							{
								availableTagParts &= ~HtmlToHtmlConverter.AvailableTagParts.Attributes;
							}
							tagPartMinor &= (HtmlToken.TagPartMinor)199;
							break;
						case HtmlToHtmlConverter.CopyPendingState.AttributeNameCopyPending:
							this.CopyInputAttributeName(0);
							if (1 == this.attributeCount && (byte)(this.token.Attributes[0].MinorPart & HtmlToken.AttrPartMinor.ContinueValue) == 0)
							{
								this.attributeSkipCount = 1;
								this.attributeCount--;
								availableTagParts &= ~HtmlToHtmlConverter.AvailableTagParts.Attributes;
								tagPartMinor &= (HtmlToken.TagPartMinor)199;
							}
							else
							{
								this.token.Attributes[0].Name.MakeEmpty();
								this.token.Attributes[0].SetMinorPart(this.token.Attributes[0].MinorPart & (HtmlToken.AttrPartMinor)248);
							}
							break;
						case HtmlToHtmlConverter.CopyPendingState.AttributeValueCopyPending:
							this.CopyInputAttributeValue(0);
							this.attributeSkipCount = 1;
							this.attributeCount--;
							if (this.attributeCount == 0)
							{
								availableTagParts &= ~HtmlToHtmlConverter.AvailableTagParts.Attributes;
							}
							tagPartMinor &= (HtmlToken.TagPartMinor)199;
							break;
						}
					}
					if (availableTagParts != HtmlToHtmlConverter.AvailableTagParts.None)
					{
						if (this.tagCallbackRequested)
						{
							if (this.callbackContext == null)
							{
								this.callbackContext = new HtmlToHtmlTagContext(this);
							}
							if (this.token.IsTagBegin || this.attributeTriggeredCallback)
							{
								this.callbackContext.InitializeTag(false, HtmlDtd.tags[(int)this.tagIndex].NameIndex, false);
								this.attributeTriggeredCallback = false;
							}
							this.callbackContext.InitializeFragment(this.token.IsEmptyScope, this.attributeCount, new HtmlTagParts(this.token.MajorPart, this.token.MinorPart));
							this.callback(this.callbackContext, this.writer);
							this.callbackContext.UninitializeFragment();
							if (this.token.IsTagEnd || this.truncateForCallback)
							{
								if (this.callbackContext.IsInvokeCallbackForEndTag)
								{
									this.endTagCallbackRequested = true;
								}
								if (this.callbackContext.IsDeleteInnerContent)
								{
									this.dropLevel = this.currentLevel + 1;
								}
								if (this.token.IsTagBegin && this.callbackContext.IsDeleteEndTag)
								{
									this.tagDropped = true;
								}
								if (!this.tagDropped && !this.token.IsTagEnd)
								{
									this.tagDropped = true;
									this.justTruncated = true;
									this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.NotPending;
								}
							}
						}
						else
						{
							if (this.token.IsTagBegin)
							{
								this.CopyInputTag(false);
							}
							if (this.attributeCount != 0)
							{
								this.CopyInputTagAttributes();
							}
							if (this.token.IsTagEnd && this.tagIndex == HtmlTagIndex.Unknown)
							{
								this.writer.WriteTagEnd(this.token.IsEmptyScope);
							}
						}
					}
				}
			}
			if (this.token.IsTagEnd)
			{
				if (this.writer.IsTagOpen)
				{
					this.writer.WriteTagEnd();
				}
				if (!this.token.IsEmptyScope && this.tagIndex > HtmlTagIndex.Unknown)
				{
					if (this.normalizedInput && this.currentLevel < this.dropLevel && ((this.tagDropped && !this.justTruncated) || this.endTagCallbackRequested))
					{
						if (this.endTagActionStack == null)
						{
							this.endTagActionStack = new HtmlToHtmlConverter.EndTagActionEntry[4];
						}
						else if (this.endTagActionStack.Length == this.endTagActionStackTop)
						{
							HtmlToHtmlConverter.EndTagActionEntry[] destinationArray = new HtmlToHtmlConverter.EndTagActionEntry[this.endTagActionStack.Length * 2];
							Array.Copy(this.endTagActionStack, 0, destinationArray, 0, this.endTagActionStackTop);
							this.endTagActionStack = destinationArray;
						}
						this.endTagActionStack[this.endTagActionStackTop].TagLevel = this.currentLevel;
						this.endTagActionStack[this.endTagActionStackTop].Drop = (this.tagDropped && !this.justTruncated);
						this.endTagActionStack[this.endTagActionStackTop].Callback = this.endTagCallbackRequested;
						this.endTagActionStackTop++;
					}
					this.currentLevel++;
					this.PostProcessStartTag();
					return;
				}
				this.currentLevel--;
				if (this.dropLevel != 2147483647 && this.currentLevel < this.dropLevel)
				{
					this.dropLevel = int.MaxValue;
				}
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0009440C File Offset: 0x0009260C
		private void ProcessEndTag()
		{
			HtmlToHtmlConverter.AvailableTagParts availableTagParts = HtmlToHtmlConverter.AvailableTagParts.None;
			if (this.token.IsTagBegin)
			{
				if (this.currentLevel > 0)
				{
					this.currentLevel--;
				}
				this.tagIndex = this.token.TagIndex;
				this.tagDropped = false;
				this.tagCallbackRequested = false;
				this.tagHasFilteredStyleAttribute = false;
				availableTagParts = HtmlToHtmlConverter.AvailableTagParts.TagBegin;
				this.PreProcessEndTag();
				if (this.currentLevel >= this.dropLevel)
				{
					this.tagDropped = true;
				}
				else
				{
					if (this.endTagActionStackTop != 0 && this.tagIndex > HtmlTagIndex.Unknown && this.endTagActionStack[this.endTagActionStackTop - 1].TagLevel >= this.currentLevel)
					{
						if (this.endTagActionStack[this.endTagActionStackTop - 1].TagLevel == this.currentLevel)
						{
							this.endTagActionStackTop--;
							this.tagDropped = this.endTagActionStack[this.endTagActionStackTop].Drop;
							this.tagCallbackRequested = this.endTagActionStack[this.endTagActionStackTop].Callback;
						}
						else
						{
							int i = this.endTagActionStackTop;
							while (i > 0 && this.endTagActionStack[i - 1].TagLevel > this.currentLevel)
							{
								i--;
							}
							for (int j = i; j < this.endTagActionStackTop; j++)
							{
								HtmlToHtmlConverter.EndTagActionEntry[] array = this.endTagActionStack;
								int num = j;
								array[num].TagLevel = array[num].TagLevel - 2;
							}
							if (i > 0 && this.endTagActionStack[i - 1].TagLevel == this.currentLevel)
							{
								this.tagDropped = this.endTagActionStack[i - 1].Drop;
								this.tagCallbackRequested = this.endTagActionStack[i - 1].Callback;
								while (i < this.endTagActionStackTop)
								{
									this.endTagActionStack[i - 1] = this.endTagActionStack[i];
									i++;
								}
								this.endTagActionStackTop--;
							}
						}
					}
					if (this.token.Argument == 1 && this.tagIndex == HtmlTagIndex.Unknown)
					{
						this.tagDropped = true;
					}
				}
			}
			if (!this.tagDropped)
			{
				HtmlToken.TagPartMinor tagPartMinor = this.token.MinorPart & (HtmlToken.TagPartMinor)71;
				if (this.token.IsTagEnd)
				{
					availableTagParts |= HtmlToHtmlConverter.AvailableTagParts.TagEnd;
				}
				if (this.token.HasNameFragment)
				{
					availableTagParts |= HtmlToHtmlConverter.AvailableTagParts.TagName;
				}
				if (this.CopyPendingStateFlag == HtmlToHtmlConverter.CopyPendingState.TagNameCopyPending)
				{
					this.token.Name.WriteTo(this.writer.WriteTagName());
					if (this.token.IsTagNameEnd)
					{
						this.CopyPendingStateFlag = HtmlToHtmlConverter.CopyPendingState.NotPending;
					}
					this.token.Name.MakeEmpty();
					availableTagParts &= ~HtmlToHtmlConverter.AvailableTagParts.TagName;
					tagPartMinor &= (HtmlToken.TagPartMinor)248;
				}
				if (availableTagParts != HtmlToHtmlConverter.AvailableTagParts.None)
				{
					if (this.tagCallbackRequested)
					{
						if (this.token.IsTagBegin)
						{
							this.callbackContext.InitializeTag(true, HtmlDtd.tags[(int)this.tagIndex].NameIndex, false);
						}
						this.callbackContext.InitializeFragment(false, 0, new HtmlTagParts(this.token.MajorPart, tagPartMinor));
						this.callback(this.callbackContext, this.writer);
						this.callbackContext.UninitializeFragment();
					}
					else if (this.token.IsTagBegin)
					{
						this.CopyInputTag(false);
					}
				}
			}
			else if (this.tagCallbackRequested)
			{
				HtmlToken.TagPartMinor tagPartMinor = this.token.MinorPart & (HtmlToken.TagPartMinor)71;
				if (this.token.IsTagBegin)
				{
					this.callbackContext.InitializeTag(true, HtmlDtd.tags[(int)this.tagIndex].NameIndex, true);
				}
				this.callbackContext.InitializeFragment(false, 0, new HtmlTagParts(this.token.MajorPart, tagPartMinor));
				this.callback(this.callbackContext, this.writer);
				this.callbackContext.UninitializeFragment();
			}
			if (this.token.IsTagEnd)
			{
				if (this.writer.IsTagOpen)
				{
					this.writer.WriteTagEnd();
				}
				if (this.tagIndex > HtmlTagIndex.Unknown)
				{
					if (this.currentLevel > 0)
					{
						this.currentLevel--;
					}
					if (this.dropLevel != 2147483647 && this.currentLevel < this.dropLevel)
					{
						this.dropLevel = int.MaxValue;
						return;
					}
				}
				else if (this.currentLevel > 0)
				{
					this.currentLevel++;
				}
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00094879 File Offset: 0x00092A79
		private void ProcessOverlappedClose()
		{
			this.currentLevelDelta = this.token.Argument * 2;
			this.currentLevel -= this.currentLevelDelta;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000948A1 File Offset: 0x00092AA1
		private void ProcessOverlappedReopen()
		{
			this.currentLevel += this.token.Argument * 2;
			this.currentLevelDelta = 0;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000948C4 File Offset: 0x00092AC4
		private void ProcessText()
		{
			if (this.currentLevel >= this.dropLevel)
			{
				return;
			}
			if (this.insideCSS && this.filterHtml)
			{
				this.AppendCssFromTokenText();
				return;
			}
			if (this.token.Argument == 1)
			{
				this.writer.WriteCollapsedWhitespace();
				return;
			}
			if (this.token.Runs.MoveNext(true))
			{
				this.token.Text.WriteTo(this.writer.WriteText());
			}
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00094945 File Offset: 0x00092B45
		private void ProcessInjectionBegin()
		{
			if (this.token.Argument == 0 && this.headDivUnterminated)
			{
				this.writer.WriteEndTag(HtmlNameIndex.Div);
				this.writer.WriteAutoNewLine(true);
				this.headDivUnterminated = false;
			}
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0009497C File Offset: 0x00092B7C
		private void ProcessInjectionEnd()
		{
			if (this.token.Argument != 0)
			{
				this.writer.WriteAutoNewLine(true);
				this.writer.WriteStartTag(HtmlNameIndex.Div);
				this.headDivUnterminated = true;
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000949AC File Offset: 0x00092BAC
		private void ProcessEof()
		{
			this.writer.SetCopyPending(false);
			if (this.headDivUnterminated && this.dropLevel != 0)
			{
				this.writer.WriteEndTag(HtmlNameIndex.Div);
				this.writer.WriteAutoNewLine(true);
				this.headDivUnterminated = false;
			}
			if (this.outputFragment && !this.insideBody)
			{
				this.writer.WriteStartTag(HtmlNameIndex.Div);
				this.writer.WriteEndTag(HtmlNameIndex.Div);
				this.writer.WriteAutoNewLine(true);
			}
			if (!this.convertFragment)
			{
				this.writer.Flush();
			}
			this.endOfFile = true;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00094A48 File Offset: 0x00092C48
		private void PreProcessStartTag()
		{
			if (this.tagIndex > HtmlTagIndex.Unknown)
			{
				if (this.tagIndex == HtmlTagIndex.Body)
				{
					if (this.outputFragment)
					{
						this.insideBody = true;
						this.tagIndex = HtmlTagIndex.Div;
						return;
					}
				}
				else if (this.tagIndex == HtmlTagIndex.Meta)
				{
					if (!this.filterHtml)
					{
						this.token.Attributes.Rewind();
						foreach (HtmlAttribute htmlAttribute in this.token.Attributes)
						{
							if (htmlAttribute.NameIndex == HtmlNameIndex.HttpEquiv)
							{
								if (htmlAttribute.Value.CaseInsensitiveCompareEqual("content-type") || htmlAttribute.Value.CaseInsensitiveCompareEqual("charset"))
								{
									this.tagDropped = true;
									return;
								}
							}
							else if (htmlAttribute.NameIndex == HtmlNameIndex.Charset)
							{
								this.tagDropped = true;
								return;
							}
						}
						return;
					}
				}
				else if (this.tagIndex == HtmlTagIndex.Style)
				{
					this.styleIsCSS = true;
					if (this.token.Attributes.Find(HtmlNameIndex.Type))
					{
						HtmlAttribute htmlAttribute2 = this.token.Attributes.Current;
						if (!htmlAttribute2.Value.CaseInsensitiveCompareEqual("text/css"))
						{
							this.styleIsCSS = false;
							return;
						}
					}
				}
				else
				{
					if (this.tagIndex == HtmlTagIndex.TC)
					{
						this.tagDropped = true;
						return;
					}
					if (this.tagIndex == HtmlTagIndex.PlainText || this.tagIndex == HtmlTagIndex.Xmp)
					{
						if (this.filterHtml || (this.hasTailInjection && this.tagIndex == HtmlTagIndex.PlainText))
						{
							this.tagDropped = true;
							this.writer.WriteAutoNewLine(true);
							this.writer.WriteStartTag(HtmlNameIndex.TT);
							this.writer.WriteStartTag(HtmlNameIndex.Pre);
							this.writer.WriteAutoNewLine();
							return;
						}
					}
					else if (this.tagIndex == HtmlTagIndex.Image && this.filterHtml)
					{
						this.tagIndex = HtmlTagIndex.Img;
					}
				}
			}
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00094C2C File Offset: 0x00092E2C
		private void ProcessTagAttributes()
		{
			this.attributeSkipCount = 0;
			HtmlToken.AttributeEnumerator attributes = this.token.Attributes;
			if (this.filterHtml)
			{
				this.attributeCount = 0;
				this.attributeIndirect = true;
				this.attributeVirtualCount = 0;
				this.attributeVirtualScratch.Reset();
				if (this.attributeIndirectIndex == null)
				{
					this.attributeIndirectIndex = new HtmlToHtmlConverter.AttributeIndirectEntry[Math.Max(attributes.Count + 1, 32)];
				}
				else if (this.attributeIndirectIndex.Length <= attributes.Count)
				{
					this.attributeIndirectIndex = new HtmlToHtmlConverter.AttributeIndirectEntry[Math.Max(this.attributeIndirectIndex.Length * 2, attributes.Count + 1)];
				}
				for (int i = 0; i < attributes.Count; i++)
				{
					HtmlAttribute htmlAttribute = attributes[i];
					HtmlFilterData.FilterAction filterAction;
					if (htmlAttribute.IsAttrBegin)
					{
						filterAction = (this.filterForFragment ? HtmlFilterData.filterInstructions[(int)htmlAttribute.NameIndex].attrFragmentAction : HtmlFilterData.filterInstructions[(int)htmlAttribute.NameIndex].attrAction);
						if ((byte)(filterAction & HtmlFilterData.FilterAction.HasExceptions) != 0 && (byte)(HtmlFilterData.filterInstructions[(int)this.token.NameIndex].tagAction & HtmlFilterData.FilterAction.HasExceptions) != 0)
						{
							for (int j = 0; j < HtmlFilterData.filterExceptions.Length; j++)
							{
								if (HtmlFilterData.filterExceptions[j].tagNameIndex == this.token.NameIndex && HtmlFilterData.filterExceptions[j].attrNameIndex == htmlAttribute.NameIndex)
								{
									filterAction = (this.filterForFragment ? HtmlFilterData.filterExceptions[j].fragmentAction : HtmlFilterData.filterExceptions[j].action);
									break;
								}
							}
						}
						if (htmlAttribute.AttributeValueContainsDangerousCharacter)
						{
							if (htmlAttribute.AttributeValueContainsBackquote)
							{
								filterAction = HtmlFilterData.FilterAction.Drop;
							}
							if (htmlAttribute.AttributeValueContainsBackslash && (byte)(filterAction & HtmlFilterData.FilterAction.SanitizeUrl) == 0)
							{
								filterAction = HtmlFilterData.FilterAction.Drop;
							}
						}
						if (!this.outputFragment && (filterAction == HtmlFilterData.FilterAction.PrefixName || filterAction == HtmlFilterData.FilterAction.PrefixNameList))
						{
							filterAction = HtmlFilterData.FilterAction.Keep;
						}
						if (this.callback != null && !this.ignoreAttrCallback && (byte)(filterAction & HtmlFilterData.FilterAction.Callback) != 0)
						{
							if (this.token.IsTagBegin || !this.truncateForCallback)
							{
								this.attributeTriggeredCallback = (this.attributeTriggeredCallback || !this.tagCallbackRequested);
								this.tagCallbackRequested = true;
							}
							else
							{
								filterAction = HtmlFilterData.FilterAction.KeepDropContent;
							}
						}
						filterAction &= HtmlFilterData.FilterAction.ActionMask;
						if (!htmlAttribute.IsAttrEnd)
						{
							this.attrContinuationAction = filterAction;
						}
					}
					else
					{
						filterAction = this.attrContinuationAction;
						if (htmlAttribute.AttributeValueContainsDangerousCharacter)
						{
							if (htmlAttribute.AttributeValueContainsBackquote)
							{
								filterAction = HtmlFilterData.FilterAction.Drop;
							}
							if (htmlAttribute.AttributeValueContainsBackslash && (byte)(filterAction & HtmlFilterData.FilterAction.SanitizeUrl) == 0)
							{
								filterAction = HtmlFilterData.FilterAction.Drop;
							}
						}
					}
					if (filterAction != HtmlFilterData.FilterAction.Drop)
					{
						if (filterAction == HtmlFilterData.FilterAction.Keep)
						{
							this.attributeIndirectIndex[this.attributeCount].Index = (short)i;
							this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.PassThrough;
							this.attributeCount++;
						}
						else if (filterAction == HtmlFilterData.FilterAction.KeepDropContent)
						{
							this.attrContinuationAction = HtmlFilterData.FilterAction.Drop;
							this.attributeIndirectIndex[this.attributeCount].Index = (short)i;
							this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.EmptyValue;
							this.attributeCount++;
						}
						else if (filterAction == HtmlFilterData.FilterAction.FilterStyleAttribute)
						{
							if (htmlAttribute.IsAttrBegin)
							{
								if (this.tagHasFilteredStyleAttribute)
								{
									this.AppendCss(";");
								}
								this.tagHasFilteredStyleAttribute = true;
							}
							this.AppendCssFromAttribute(htmlAttribute);
						}
						else if (filterAction == HtmlFilterData.FilterAction.ConvertBgcolorIntoStyle)
						{
							if (htmlAttribute.IsAttrBegin)
							{
								if (this.tagHasFilteredStyleAttribute)
								{
									this.AppendCss(";");
								}
								this.tagHasFilteredStyleAttribute = true;
							}
							this.AppendCss("background-color:");
							this.AppendCssFromAttribute(htmlAttribute);
						}
						else
						{
							if (htmlAttribute.IsAttrBegin)
							{
								this.attributeLeadingSpaces = true;
							}
							if (this.attributeLeadingSpaces)
							{
								if (!htmlAttribute.Value.SkipLeadingWhitespace() && !htmlAttribute.IsAttrEnd)
								{
									if (htmlAttribute.IsAttrBegin || htmlAttribute.HasNameFragment)
									{
										this.attributeIndirectIndex[this.attributeCount].Index = (short)i;
										this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.NameOnlyFragment;
										this.attributeCount++;
										goto IL_93E;
									}
									goto IL_93E;
								}
								else
								{
									this.attributeLeadingSpaces = false;
									this.attributeActionScratch.Reset();
								}
							}
							bool flag = false;
							if (!this.attributeActionScratch.AppendHtmlAttributeValue(htmlAttribute, 4096))
							{
								flag = true;
							}
							if (!htmlAttribute.IsAttrEnd && !flag)
							{
								if (htmlAttribute.IsAttrBegin || htmlAttribute.HasNameFragment)
								{
									this.attributeIndirectIndex[this.attributeCount].Index = (short)i;
									this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.NameOnlyFragment;
									this.attributeCount++;
								}
							}
							else
							{
								this.attrContinuationAction = HtmlFilterData.FilterAction.Drop;
								if (filterAction == HtmlFilterData.FilterAction.SanitizeUrl)
								{
									int length;
									int num2;
									int num3;
									switch (HtmlToHtmlConverter.CheckUrl(this.attributeActionScratch.Buffer, this.attributeActionScratch.Length, this.tagCallbackRequested))
									{
									case HtmlToHtmlConverter.CheckUrlResult.Inconclusive:
										if (this.attributeActionScratch.Length > 256 || !htmlAttribute.IsAttrEnd)
										{
											goto IL_6AD;
										}
										break;
									case HtmlToHtmlConverter.CheckUrlResult.Unsafe:
										goto IL_6AD;
									case HtmlToHtmlConverter.CheckUrlResult.Safe:
										break;
									case HtmlToHtmlConverter.CheckUrlResult.LocalHyperlink:
										if (this.outputFragment)
										{
											int num = HtmlToHtmlConverter.NonWhitespaceLength(this.attributeActionScratch.Buffer, 1, this.attributeActionScratch.Length - 1);
											if (num != 0)
											{
												length = this.attributeVirtualScratch.Length;
												num2 = 0;
												num2 += this.attributeVirtualScratch.Append('#', int.MaxValue);
												num2 += this.attributeVirtualScratch.Append(HtmlToHtmlConverter.NamePrefix, int.MaxValue);
												num2 += this.attributeVirtualScratch.Append(this.attributeActionScratch.Buffer, 1, num, int.MaxValue);
												num3 = this.AllocateVirtualEntry(i, length, num2);
												this.attributeIndirectIndex[this.attributeCount].Index = (short)num3;
												this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.Virtual;
												this.attributeCount++;
												goto IL_93E;
											}
											goto IL_6AD;
										}
										break;
									default:
										goto IL_6AD;
									}
									if (htmlAttribute.IsCompleteAttr)
									{
										this.attributeIndirectIndex[this.attributeCount].Index = (short)i;
										this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.PassThrough;
										this.attributeCount++;
										goto IL_93E;
									}
									length = this.attributeVirtualScratch.Length;
									num2 = this.attributeVirtualScratch.Append(this.attributeActionScratch.Buffer, 0, this.attributeActionScratch.Length, int.MaxValue);
									num3 = this.AllocateVirtualEntry(i, length, num2);
									this.attributeIndirectIndex[this.attributeCount].Index = (short)num3;
									this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.Virtual;
									this.attributeCount++;
									goto IL_93E;
									IL_6AD:
									this.attrContinuationAction = HtmlFilterData.FilterAction.Drop;
									this.attributeIndirectIndex[this.attributeCount].Index = (short)i;
									this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.EmptyValue;
									this.attributeCount++;
								}
								else if (filterAction == HtmlFilterData.FilterAction.PrefixName)
								{
									int length = this.attributeVirtualScratch.Length;
									int num2 = 0;
									int num4 = HtmlToHtmlConverter.NonWhitespaceLength(this.attributeActionScratch.Buffer, 0, this.attributeActionScratch.Length);
									if (num4 != 0)
									{
										num2 += this.attributeVirtualScratch.Append(HtmlToHtmlConverter.NamePrefix, int.MaxValue);
										num2 += this.attributeVirtualScratch.Append(this.attributeActionScratch.Buffer, 0, num4, int.MaxValue);
									}
									int num3 = this.AllocateVirtualEntry(i, length, num2);
									this.attributeIndirectIndex[this.attributeCount].Index = (short)num3;
									this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.Virtual;
									this.attributeCount++;
								}
								else if (filterAction == HtmlFilterData.FilterAction.PrefixNameList)
								{
									int length = this.attributeVirtualScratch.Length;
									int num2 = 0;
									int num5 = 0;
									int num6 = HtmlToHtmlConverter.NonWhitespaceLength(this.attributeActionScratch.Buffer, num5, this.attributeActionScratch.Length - num5);
									if (num6 != 0)
									{
										do
										{
											num2 += this.attributeVirtualScratch.Append(HtmlToHtmlConverter.NamePrefix, int.MaxValue);
											num2 += this.attributeVirtualScratch.Append(this.attributeActionScratch.Buffer, num5, num6, int.MaxValue);
											num5 += num6;
											num5 += HtmlToHtmlConverter.WhitespaceLength(this.attributeActionScratch.Buffer, num5, this.attributeActionScratch.Length - num5);
											num6 = HtmlToHtmlConverter.NonWhitespaceLength(this.attributeActionScratch.Buffer, num5, this.attributeActionScratch.Length - num5);
											if (num6 != 0)
											{
												num2 += this.attributeVirtualScratch.Append(' ', int.MaxValue);
											}
										}
										while (num6 != 0);
									}
									int num3 = this.AllocateVirtualEntry(i, length, num2);
									this.attributeIndirectIndex[this.attributeCount].Index = (short)num3;
									this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.Virtual;
									this.attributeCount++;
								}
								else
								{
									this.attrContinuationAction = HtmlFilterData.FilterAction.Drop;
									this.attributeIndirectIndex[this.attributeCount].Index = (short)i;
									this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.EmptyValue;
									this.attributeCount++;
								}
							}
						}
					}
					IL_93E:;
				}
				if (this.tagHasFilteredStyleAttribute && (this.token.IsTagEnd || (this.tagCallbackRequested && this.truncateForCallback)))
				{
					this.attributeIndirectIndex[this.attributeCount].Index = -1;
					this.attributeIndirectIndex[this.attributeCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.FilteredStyle;
					this.attributeCount++;
					return;
				}
			}
			else
			{
				this.attributeCount = attributes.Count;
				this.attributeIndirect = false;
				if (this.callback != null && !this.tagCallbackRequested && !this.ignoreAttrCallback)
				{
					for (int k = 0; k < attributes.Count; k++)
					{
						HtmlAttribute htmlAttribute = attributes[k];
						if (htmlAttribute.IsAttrBegin)
						{
							HtmlFilterData.FilterAction filterAction = HtmlFilterData.filterInstructions[(int)htmlAttribute.NameIndex].attrAction;
							if ((byte)(filterAction & HtmlFilterData.FilterAction.HasExceptions) != 0 && (byte)(HtmlFilterData.filterInstructions[(int)this.token.NameIndex].tagAction & HtmlFilterData.FilterAction.HasExceptions) != 0)
							{
								for (int l = 0; l < HtmlFilterData.filterExceptions.Length; l++)
								{
									if (HtmlFilterData.filterExceptions[l].tagNameIndex == this.token.NameIndex && HtmlFilterData.filterExceptions[l].attrNameIndex == htmlAttribute.NameIndex)
									{
										filterAction = HtmlFilterData.filterExceptions[l].action;
										break;
									}
								}
							}
							if ((byte)(filterAction & HtmlFilterData.FilterAction.Callback) != 0 && (this.token.IsTagBegin || !this.truncateForCallback))
							{
								this.attributeTriggeredCallback = (this.attributeTriggeredCallback || !this.tagCallbackRequested);
								this.tagCallbackRequested = true;
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00095741 File Offset: 0x00093941
		private void PostProcessStartTag()
		{
			if (this.tagIndex == HtmlTagIndex.Style && this.styleIsCSS)
			{
				this.insideCSS = true;
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0009575C File Offset: 0x0009395C
		private void PreProcessEndTag()
		{
			if (this.tagIndex > HtmlTagIndex.Unknown)
			{
				if ((byte)(HtmlDtd.tags[(int)this.tagIndex].Literal & HtmlDtd.Literal.Entities) != 0)
				{
					if (this.tagIndex == HtmlTagIndex.Style && this.insideCSS && this.filterHtml)
					{
						this.FlushCssInStyleTag();
					}
					this.insideCSS = false;
					this.styleIsCSS = true;
				}
				if (this.tagIndex == HtmlTagIndex.PlainText || this.tagIndex == HtmlTagIndex.Xmp)
				{
					if (this.filterHtml || (this.hasTailInjection && this.tagIndex == HtmlTagIndex.PlainText))
					{
						this.tagDropped = true;
						this.writer.WriteEndTag(HtmlNameIndex.Pre);
						this.writer.WriteEndTag(HtmlNameIndex.TT);
						return;
					}
					if (this.tagIndex == HtmlTagIndex.PlainText && this.normalizedInput)
					{
						this.tagDropped = true;
						this.dropLevel = 0;
						this.endTagActionStackTop = 0;
						return;
					}
				}
				else if (this.tagIndex == HtmlTagIndex.Body)
				{
					if (this.headDivUnterminated && this.dropLevel != 0)
					{
						this.writer.WriteEndTag(HtmlNameIndex.Div);
						this.writer.WriteAutoNewLine(true);
						this.headDivUnterminated = false;
					}
					if (this.outputFragment)
					{
						this.tagIndex = HtmlTagIndex.Div;
						return;
					}
				}
				else
				{
					if (this.tagIndex == HtmlTagIndex.TC)
					{
						this.tagDropped = true;
						return;
					}
					if (this.tagIndex == HtmlTagIndex.Image && this.filterHtml)
					{
						this.tagIndex = HtmlTagIndex.Img;
						return;
					}
				}
			}
			else if (this.tagIndex == HtmlTagIndex.Unknown && this.filterHtml)
			{
				this.tagDropped = true;
			}
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x000958CC File Offset: 0x00093ACC
		private void CopyInputTagAttributes()
		{
			for (int i = 0; i < this.attributeCount; i++)
			{
				this.CopyInputAttribute(i);
			}
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x000958F4 File Offset: 0x00093AF4
		private int AllocateVirtualEntry(int index, int offset, int length)
		{
			if (this.attributeVirtualList == null)
			{
				this.attributeVirtualList = new HtmlToHtmlConverter.AttributeVirtualEntry[4];
			}
			else if (this.attributeVirtualList.Length == this.attributeVirtualCount)
			{
				HtmlToHtmlConverter.AttributeVirtualEntry[] destinationArray = new HtmlToHtmlConverter.AttributeVirtualEntry[this.attributeVirtualList.Length * 2];
				Array.Copy(this.attributeVirtualList, 0, destinationArray, 0, this.attributeVirtualCount);
				this.attributeVirtualList = destinationArray;
			}
			int num = this.attributeVirtualCount++;
			this.attributeVirtualList[num].Index = (short)index;
			this.attributeVirtualList[num].Offset = offset;
			this.attributeVirtualList[num].Length = length;
			this.attributeVirtualList[num].Position = 0;
			return num;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x000959B0 File Offset: 0x00093BB0
		private void VirtualizeFilteredStyle(int index)
		{
			int length = this.attributeVirtualScratch.Length;
			this.FlushCssInStyleAttributeToVirtualScratch();
			int length2 = this.attributeVirtualScratch.Length - length;
			int num = this.AllocateVirtualEntry((int)this.attributeIndirectIndex[index + this.attributeSkipCount].Index, length, length2);
			this.attributeIndirectIndex[index + this.attributeSkipCount].Index = (short)num;
			this.attributeIndirectIndex[index + this.attributeSkipCount].Kind = HtmlToHtmlConverter.AttributeIndirectKind.VirtualFilteredStyle;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00095A34 File Offset: 0x00093C34
		private bool InjectMetaTagIfNecessary()
		{
			if (this.filterForFragment || !this.writer.HasEncoding)
			{
				this.metaInjected = true;
			}
			else if (this.token.HtmlTokenId != HtmlTokenId.Restart && this.token.HtmlTokenId != HtmlTokenId.EncodingChange)
			{
				if (string.Compare(this.writer.Encoding.WebName, "utf-7", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.OutputMetaTag();
					this.metaInjected = true;
				}
				else if (this.token.HtmlTokenId == HtmlTokenId.Tag)
				{
					if (!this.insideHtml && this.token.TagIndex == HtmlTagIndex.Html)
					{
						if (this.token.IsTagEnd)
						{
							this.insideHtml = true;
						}
					}
					else if (!this.insideHead && this.token.TagIndex == HtmlTagIndex.Head)
					{
						if (this.token.IsTagEnd)
						{
							this.insideHead = true;
						}
					}
					else if (this.token.TagIndex > HtmlTagIndex._ASP)
					{
						if (this.insideHtml && !this.insideHead)
						{
							this.writer.WriteNewLine(true);
							this.writer.WriteStartTag(HtmlNameIndex.Head);
							this.writer.WriteNewLine(true);
							this.OutputMetaTag();
							this.writer.WriteEndTag(HtmlNameIndex.Head);
							this.writer.WriteNewLine(true);
						}
						else
						{
							if (this.insideHead)
							{
								this.writer.WriteNewLine(true);
							}
							this.OutputMetaTag();
						}
						this.metaInjected = true;
					}
				}
				else if (this.token.HtmlTokenId == HtmlTokenId.Text)
				{
					if (this.token.IsWhitespaceOnly)
					{
						return false;
					}
					this.token.Text.StripLeadingWhitespace();
					if (this.insideHtml && !this.insideHead)
					{
						this.writer.WriteNewLine(true);
						this.writer.WriteStartTag(HtmlNameIndex.Head);
						this.writer.WriteNewLine(true);
						this.OutputMetaTag();
						this.writer.WriteEndTag(HtmlNameIndex.Head);
						this.writer.WriteNewLine(true);
					}
					else
					{
						if (this.insideHead)
						{
							this.writer.WriteNewLine(true);
						}
						this.OutputMetaTag();
					}
					this.metaInjected = true;
				}
			}
			return true;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00095C64 File Offset: 0x00093E64
		private void OutputMetaTag()
		{
			Encoding encoding = this.writer.Encoding;
			if (string.Compare(encoding.WebName, "utf-7", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.writer.Encoding = CTSGlobals.AsciiEncoding;
			}
			this.writer.WriteStartTag(HtmlNameIndex.Meta);
			this.writer.WriteAttribute(HtmlNameIndex.HttpEquiv, "Content-Type");
			this.writer.WriteAttributeName(HtmlNameIndex.Content);
			this.writer.WriteAttributeValueInternal("text/html; charset=");
			this.writer.WriteAttributeValueInternal(Charset.GetCharset(encoding).Name);
			if (string.Compare(encoding.WebName, "utf-7", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.writer.WriteTagEnd();
				this.writer.Encoding = encoding;
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00095D1C File Offset: 0x00093F1C
		private HtmlToHtmlConverter.AttributeIndirectKind GetAttributeIndirectKind(int index)
		{
			if (!this.attributeIndirect)
			{
				return HtmlToHtmlConverter.AttributeIndirectKind.PassThrough;
			}
			return this.attributeIndirectIndex[index + this.attributeSkipCount].Kind;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00095D40 File Offset: 0x00093F40
		private int GetAttributeVirtualEntryIndex(int index)
		{
			return (int)this.attributeIndirectIndex[index + this.attributeSkipCount].Index;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00095D5C File Offset: 0x00093F5C
		private HtmlAttribute GetAttribute(int index)
		{
			if (!this.attributeIndirect)
			{
				return this.token.Attributes[index + this.attributeSkipCount];
			}
			if (this.attributeIndirectIndex[index + this.attributeSkipCount].Kind != HtmlToHtmlConverter.AttributeIndirectKind.Virtual)
			{
				return this.token.Attributes[(int)this.attributeIndirectIndex[index + this.attributeSkipCount].Index];
			}
			return this.token.Attributes[(int)this.attributeVirtualList[(int)this.attributeIndirectIndex[index + this.attributeSkipCount].Index].Index];
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00095E10 File Offset: 0x00094010
		private void AppendCssFromTokenText()
		{
			if (this.cssParserInput == null)
			{
				this.cssParserInput = new ConverterBufferInput(524288, this.progressMonitor);
				this.cssParser = new CssParser(this.cssParserInput, 4096, false);
			}
			this.token.Text.WriteTo(this.cssParserInput);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00095E6C File Offset: 0x0009406C
		private void AppendCss(string css)
		{
			if (this.cssParserInput == null)
			{
				this.cssParserInput = new ConverterBufferInput(524288, this.progressMonitor);
				this.cssParser = new CssParser(this.cssParserInput, 4096, false);
			}
			this.cssParserInput.Write(css);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00095EBC File Offset: 0x000940BC
		private void AppendCssFromAttribute(HtmlAttribute attribute)
		{
			if (this.cssParserInput == null)
			{
				this.cssParserInput = new ConverterBufferInput(524288, this.progressMonitor);
				this.cssParser = new CssParser(this.cssParserInput, 4096, false);
			}
			attribute.Value.Rewind();
			attribute.Value.WriteTo(this.cssParserInput);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00095F24 File Offset: 0x00094124
		private void FlushCssInStyleTag()
		{
			if (this.cssParserInput != null)
			{
				this.writer.WriteNewLine();
				this.writer.WriteMarkupText("<!--");
				this.writer.WriteNewLine();
				bool agressiveFiltering = false;
				if (this.smallCssBlockThreshold != -1 && this.cssParserInput.MaxTokenSize > this.smallCssBlockThreshold)
				{
					agressiveFiltering = true;
				}
				this.cssParser.SetParseMode(CssParseMode.StyleTag);
				bool flag = true;
				ITextSinkEx textSinkEx = this.writer.WriteText();
				CssTokenId cssTokenId;
				do
				{
					cssTokenId = this.cssParser.Parse();
					if ((CssTokenId.RuleSet == cssTokenId || CssTokenId.AtRule == cssTokenId) && this.cssParser.Token.Selectors.ValidCount != 0 && this.cssParser.Token.Properties.ValidCount != 0)
					{
						bool flag2 = this.CopyInputCssSelectors(this.cssParser.Token.Selectors, textSinkEx, agressiveFiltering);
						if (flag2)
						{
							if (this.cssParser.Token.IsPropertyListBegin)
							{
								textSinkEx.Write("\r\n\t{");
							}
							this.CopyInputCssProperties(true, this.cssParser.Token.Properties, textSinkEx, ref flag);
							if (this.cssParser.Token.IsPropertyListEnd)
							{
								textSinkEx.Write("}\r\n");
								flag = true;
							}
						}
					}
				}
				while (CssTokenId.EndOfFile != cssTokenId);
				this.cssParserInput.Reset();
				this.cssParser.Reset();
				this.writer.WriteMarkupText("-->");
				this.writer.WriteNewLine();
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00096098 File Offset: 0x00094298
		private void FlushCssInStyleAttributeToVirtualScratch()
		{
			this.cssParser.SetParseMode(CssParseMode.StyleAttribute);
			if (this.virtualScratchSink == null)
			{
				this.virtualScratchSink = new HtmlToHtmlConverter.VirtualScratchSink(this, int.MaxValue);
			}
			bool flag = true;
			CssTokenId cssTokenId;
			do
			{
				cssTokenId = this.cssParser.Parse();
				if (CssTokenId.Declarations == cssTokenId && this.cssParser.Token.Properties.ValidCount != 0)
				{
					this.CopyInputCssProperties(false, this.cssParser.Token.Properties, this.virtualScratchSink, ref flag);
				}
			}
			while (CssTokenId.EndOfFile != cssTokenId);
			this.cssParserInput.Reset();
			this.cssParser.Reset();
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00096130 File Offset: 0x00094330
		private void FlushCssInStyleAttribute(HtmlWriter writer)
		{
			this.cssParser.SetParseMode(CssParseMode.StyleAttribute);
			ITextSinkEx sink = writer.WriteAttributeValue();
			bool flag = true;
			CssTokenId cssTokenId;
			do
			{
				cssTokenId = this.cssParser.Parse();
				if (CssTokenId.Declarations == cssTokenId && this.cssParser.Token.Properties.ValidCount != 0)
				{
					this.CopyInputCssProperties(false, this.cssParser.Token.Properties, sink, ref flag);
				}
			}
			while (CssTokenId.EndOfFile != cssTokenId);
			this.cssParserInput.Reset();
			this.cssParser.Reset();
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x000961B0 File Offset: 0x000943B0
		private bool CopyInputCssSelectors(CssToken.SelectorEnumerator selectors, ITextSinkEx sink, bool agressiveFiltering)
		{
			bool flag = false;
			bool flag2 = false;
			selectors.Rewind();
			foreach (CssSelector selector in selectors)
			{
				if (!selector.IsDeleted)
				{
					if (flag2)
					{
						if (selector.Combinator == CssSelectorCombinator.None)
						{
							sink.Write(", ");
						}
						else if (selector.Combinator == CssSelectorCombinator.Descendant)
						{
							sink.Write(32);
						}
						else if (selector.Combinator == CssSelectorCombinator.Adjacent)
						{
							sink.Write(" + ");
						}
						else
						{
							sink.Write(" > ");
						}
					}
					flag2 = this.CopyInputCssSelector(selector, sink, agressiveFiltering);
					flag = (flag || flag2);
				}
			}
			return flag;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00096250 File Offset: 0x00094450
		private bool CopyInputCssSelector(CssSelector selector, ITextSinkEx sink, bool agressiveFiltering)
		{
			if (this.filterForFragment && (!selector.HasClassFragment || (selector.ClassType != CssSelectorClassType.Regular && selector.ClassType != CssSelectorClassType.Hash)))
			{
				return false;
			}
			if (agressiveFiltering)
			{
				if (!selector.HasClassFragment || selector.ClassType != CssSelectorClassType.Regular)
				{
					return false;
				}
				string @string = selector.ClassName.GetString(256);
				if (!@string.Equals("MsoNormal", StringComparison.Ordinal))
				{
					return false;
				}
			}
			if (selector.NameId != HtmlNameIndex.Unknown && selector.NameId != HtmlNameIndex._NOTANAME)
			{
				sink.Write(HtmlNameData.Names[(int)selector.NameId].Name);
			}
			else if (selector.HasNameFragment)
			{
				selector.Name.WriteOriginalTo(sink);
			}
			if (selector.HasClassFragment)
			{
				if (selector.ClassType == CssSelectorClassType.Regular)
				{
					sink.Write(".");
				}
				else if (selector.ClassType == CssSelectorClassType.Hash)
				{
					sink.Write("#");
				}
				else if (selector.ClassType == CssSelectorClassType.Pseudo)
				{
					sink.Write(":");
				}
				if (this.outputFragment)
				{
					sink.Write(HtmlToHtmlConverter.NamePrefix);
				}
				selector.ClassName.WriteOriginalTo(sink);
			}
			return true;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00096378 File Offset: 0x00094578
		private void CopyInputCssProperties(bool inTag, CssToken.PropertyEnumerator properties, ITextSinkEx sink, ref bool firstProperty)
		{
			properties.Rewind();
			foreach (CssProperty property in properties)
			{
				if (property.IsPropertyBegin && !property.IsDeleted)
				{
					CssData.FilterAction filterAction = CssData.filterInstructions[(int)property.NameId].propertyAction;
					if (CssData.FilterAction.CheckContent == filterAction)
					{
						if (property.NameId == CssNameIndex.Display && property.HasValueFragment && property.Value.CaseInsensitiveContainsSubstring("none") && !this.preserveDisplayNoneStyle)
						{
							filterAction = CssData.FilterAction.Drop;
						}
						else if (property.NameId == CssNameIndex.Position && property.HasValueFragment && (property.Value.CaseInsensitiveContainsSubstring("absolute") || property.Value.CaseInsensitiveContainsSubstring("fixed") || property.Value.CaseInsensitiveContainsSubstring("relative")))
						{
							filterAction = CssData.FilterAction.Drop;
						}
						else if ((property.NameId == CssNameIndex.Margin || property.NameId == CssNameIndex.MarginTop || property.NameId == CssNameIndex.MarginLeft || property.NameId == CssNameIndex.MarginRight || property.NameId == CssNameIndex.MarginBottom) && this.HasNegativeMarginValue(property.Value.GetString(property.Value.Length)))
						{
							filterAction = CssData.FilterAction.Drop;
						}
						else
						{
							filterAction = CssData.FilterAction.Keep;
						}
					}
					if (CssData.FilterAction.Keep == filterAction)
					{
						if (firstProperty)
						{
							firstProperty = false;
						}
						else
						{
							sink.Write(inTag ? ";\r\n\t" : "; ");
						}
						HtmlToHtmlConverter.CopyInputCssProperty(property, sink);
					}
				}
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0009650C File Offset: 0x0009470C
		private bool HasNegativeMarginValue(string propValue)
		{
			string[] array = propValue.Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].StartsWith("-"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040013F8 RID: 5112
		private static readonly string NamePrefix = "x_";

		// Token: 0x040013F9 RID: 5113
		private static readonly HtmlAttributeParts CompleteAttributeParts = new HtmlAttributeParts(HtmlToken.AttrPartMajor.Complete, HtmlToken.AttrPartMinor.CompleteNameWithCompleteValue);

		// Token: 0x040013FA RID: 5114
		private static object lockObject = new object();

		// Token: 0x040013FB RID: 5115
		private static bool textConvertersConfigured;

		// Token: 0x040013FC RID: 5116
		private static Dictionary<string, string> safeUrlDictionary;

		// Token: 0x040013FD RID: 5117
		private HtmlToken token;

		// Token: 0x040013FE RID: 5118
		private HtmlWriter writer;

		// Token: 0x040013FF RID: 5119
		private bool convertFragment;

		// Token: 0x04001400 RID: 5120
		private bool outputFragment;

		// Token: 0x04001401 RID: 5121
		private bool filterForFragment;

		// Token: 0x04001402 RID: 5122
		private bool filterHtml;

		// Token: 0x04001403 RID: 5123
		private bool truncateForCallback;

		// Token: 0x04001404 RID: 5124
		private int smallCssBlockThreshold;

		// Token: 0x04001405 RID: 5125
		private bool preserveDisplayNoneStyle;

		// Token: 0x04001406 RID: 5126
		private bool hasTailInjection;

		// Token: 0x04001407 RID: 5127
		private IHtmlParser parser;

		// Token: 0x04001408 RID: 5128
		private bool endOfFile;

		// Token: 0x04001409 RID: 5129
		private bool normalizedInput;

		// Token: 0x0400140A RID: 5130
		private HtmlTagCallback callback;

		// Token: 0x0400140B RID: 5131
		private HtmlToHtmlTagContext callbackContext;

		// Token: 0x0400140C RID: 5132
		private bool headDivUnterminated;

		// Token: 0x0400140D RID: 5133
		private int currentLevel;

		// Token: 0x0400140E RID: 5134
		private int currentLevelDelta;

		// Token: 0x0400140F RID: 5135
		private bool insideCSS;

		// Token: 0x04001410 RID: 5136
		private int dropLevel = int.MaxValue;

		// Token: 0x04001411 RID: 5137
		private HtmlToHtmlConverter.EndTagActionEntry[] endTagActionStack;

		// Token: 0x04001412 RID: 5138
		private int endTagActionStackTop;

		// Token: 0x04001413 RID: 5139
		private bool tagDropped;

		// Token: 0x04001414 RID: 5140
		private bool justTruncated;

		// Token: 0x04001415 RID: 5141
		private bool tagCallbackRequested;

		// Token: 0x04001416 RID: 5142
		private bool attributeTriggeredCallback;

		// Token: 0x04001417 RID: 5143
		private bool endTagCallbackRequested;

		// Token: 0x04001418 RID: 5144
		private bool ignoreAttrCallback;

		// Token: 0x04001419 RID: 5145
		private bool styleIsCSS;

		// Token: 0x0400141A RID: 5146
		private HtmlFilterData.FilterAction attrContinuationAction;

		// Token: 0x0400141B RID: 5147
		private HtmlToHtmlConverter.CopyPendingState copyPendingState;

		// Token: 0x0400141C RID: 5148
		private HtmlTagIndex tagIndex;

		// Token: 0x0400141D RID: 5149
		private int attributeCount;

		// Token: 0x0400141E RID: 5150
		private int attributeSkipCount;

		// Token: 0x0400141F RID: 5151
		private bool attributeIndirect;

		// Token: 0x04001420 RID: 5152
		private HtmlToHtmlConverter.AttributeIndirectEntry[] attributeIndirectIndex;

		// Token: 0x04001421 RID: 5153
		private HtmlToHtmlConverter.AttributeVirtualEntry[] attributeVirtualList;

		// Token: 0x04001422 RID: 5154
		private int attributeVirtualCount;

		// Token: 0x04001423 RID: 5155
		private ScratchBuffer attributeVirtualScratch;

		// Token: 0x04001424 RID: 5156
		private ScratchBuffer attributeActionScratch;

		// Token: 0x04001425 RID: 5157
		private bool attributeLeadingSpaces;

		// Token: 0x04001426 RID: 5158
		private bool metaInjected;

		// Token: 0x04001427 RID: 5159
		private bool insideHtml;

		// Token: 0x04001428 RID: 5160
		private bool insideHead;

		// Token: 0x04001429 RID: 5161
		private bool insideBody;

		// Token: 0x0400142A RID: 5162
		private bool tagHasFilteredStyleAttribute;

		// Token: 0x0400142B RID: 5163
		private CssParser cssParser;

		// Token: 0x0400142C RID: 5164
		private ConverterBufferInput cssParserInput;

		// Token: 0x0400142D RID: 5165
		private HtmlToHtmlConverter.VirtualScratchSink virtualScratchSink;

		// Token: 0x0400142E RID: 5166
		private IProgressMonitor progressMonitor;

		// Token: 0x020001D6 RID: 470
		internal enum CopyPendingState : byte
		{
			// Token: 0x04001430 RID: 5168
			NotPending,
			// Token: 0x04001431 RID: 5169
			TagCopyPending,
			// Token: 0x04001432 RID: 5170
			TagContentCopyPending,
			// Token: 0x04001433 RID: 5171
			TagNameCopyPending,
			// Token: 0x04001434 RID: 5172
			AttributeCopyPending,
			// Token: 0x04001435 RID: 5173
			AttributeNameCopyPending,
			// Token: 0x04001436 RID: 5174
			AttributeValueCopyPending
		}

		// Token: 0x020001D7 RID: 471
		[Flags]
		private enum AvailableTagParts : byte
		{
			// Token: 0x04001438 RID: 5176
			None = 0,
			// Token: 0x04001439 RID: 5177
			TagBegin = 1,
			// Token: 0x0400143A RID: 5178
			TagEnd = 2,
			// Token: 0x0400143B RID: 5179
			TagName = 4,
			// Token: 0x0400143C RID: 5180
			Attributes = 8,
			// Token: 0x0400143D RID: 5181
			UnstructuredContent = 16
		}

		// Token: 0x020001D8 RID: 472
		private enum AttributeIndirectKind
		{
			// Token: 0x0400143F RID: 5183
			PassThrough,
			// Token: 0x04001440 RID: 5184
			EmptyValue,
			// Token: 0x04001441 RID: 5185
			FilteredStyle,
			// Token: 0x04001442 RID: 5186
			Virtual,
			// Token: 0x04001443 RID: 5187
			VirtualFilteredStyle,
			// Token: 0x04001444 RID: 5188
			NameOnlyFragment
		}

		// Token: 0x020001D9 RID: 473
		private enum CheckUrlResult
		{
			// Token: 0x04001446 RID: 5190
			Inconclusive,
			// Token: 0x04001447 RID: 5191
			Unsafe,
			// Token: 0x04001448 RID: 5192
			Safe,
			// Token: 0x04001449 RID: 5193
			LocalHyperlink
		}

		// Token: 0x020001DA RID: 474
		private struct AttributeIndirectEntry
		{
			// Token: 0x0400144A RID: 5194
			public HtmlToHtmlConverter.AttributeIndirectKind Kind;

			// Token: 0x0400144B RID: 5195
			public short Index;
		}

		// Token: 0x020001DB RID: 475
		private struct AttributeVirtualEntry
		{
			// Token: 0x0400144C RID: 5196
			public short Index;

			// Token: 0x0400144D RID: 5197
			public int Offset;

			// Token: 0x0400144E RID: 5198
			public int Length;

			// Token: 0x0400144F RID: 5199
			public int Position;
		}

		// Token: 0x020001DC RID: 476
		private struct EndTagActionEntry
		{
			// Token: 0x04001450 RID: 5200
			public int TagLevel;

			// Token: 0x04001451 RID: 5201
			public bool Drop;

			// Token: 0x04001452 RID: 5202
			public bool Callback;
		}

		// Token: 0x020001DD RID: 477
		internal class VirtualScratchSink : ITextSinkEx, ITextSink
		{
			// Token: 0x060014CC RID: 5324 RVA: 0x00096572 File Offset: 0x00094772
			public VirtualScratchSink(HtmlToHtmlConverter converter, int maxLength)
			{
				this.converter = converter;
				this.maxLength = maxLength;
			}

			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x060014CD RID: 5325 RVA: 0x00096588 File Offset: 0x00094788
			public bool IsEnough
			{
				get
				{
					return this.converter.attributeVirtualScratch.Length >= this.maxLength;
				}
			}

			// Token: 0x060014CE RID: 5326 RVA: 0x000965A5 File Offset: 0x000947A5
			public void Write(char[] buffer, int offset, int count)
			{
				this.converter.attributeVirtualScratch.Append(buffer, offset, count, this.maxLength);
			}

			// Token: 0x060014CF RID: 5327 RVA: 0x000965C4 File Offset: 0x000947C4
			public void Write(int ucs32Char)
			{
				if (Token.LiteralLength(ucs32Char) == 1)
				{
					this.converter.attributeVirtualScratch.Append((char)ucs32Char, this.maxLength);
					return;
				}
				this.converter.attributeVirtualScratch.Append(Token.LiteralFirstChar(ucs32Char), this.maxLength);
				if (!this.IsEnough)
				{
					this.converter.attributeVirtualScratch.Append(Token.LiteralLastChar(ucs32Char), this.maxLength);
				}
			}

			// Token: 0x060014D0 RID: 5328 RVA: 0x00096636 File Offset: 0x00094836
			public void Write(string value)
			{
				this.converter.attributeVirtualScratch.Append(value, this.maxLength);
			}

			// Token: 0x060014D1 RID: 5329 RVA: 0x00096650 File Offset: 0x00094850
			public void WriteNewLine()
			{
				this.converter.attributeVirtualScratch.Append('\r', this.maxLength);
				if (!this.IsEnough)
				{
					this.converter.attributeVirtualScratch.Append('\n', this.maxLength);
				}
			}

			// Token: 0x04001453 RID: 5203
			private HtmlToHtmlConverter converter;

			// Token: 0x04001454 RID: 5204
			private int maxLength;
		}
	}
}
