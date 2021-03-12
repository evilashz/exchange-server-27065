using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x0200024B RID: 587
	internal class RtfFormatOutput : FormatOutput, IRestartable
	{
		// Token: 0x06001816 RID: 6166 RVA: 0x000BD768 File Offset: 0x000BB968
		public RtfFormatOutput(Stream destination, bool push, bool restartable, bool testBoundaryConditions, IResultsFeedback resultFeedback, ImageRenderingCallbackInternal imageRenderingCallback, Stream formatTraceStream, Stream formatOutputTraceStream, Encoding preferredEncoding) : base(formatOutputTraceStream)
		{
			this.output = new RtfOutput(destination, push, restartable);
			this.resultFeedback = resultFeedback;
			this.restartable = restartable;
			this.imageRenderingCallback = imageRenderingCallback;
			this.preferredEncoding = preferredEncoding;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x000BD7E0 File Offset: 0x000BB9E0
		public RtfFormatOutput(RtfOutput output, Stream formatTraceStream, Stream formatOutputTraceStream) : base(formatOutputTraceStream)
		{
			this.output = output;
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x000BD830 File Offset: 0x000BBA30
		public override bool CanAcceptMoreOutput
		{
			get
			{
				return this.output.CanAcceptMoreOutput;
			}
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000BD83D File Offset: 0x000BBA3D
		bool IRestartable.CanRestart()
		{
			return this.restartable && ((IRestartable)this.output).CanRestart();
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x000BD854 File Offset: 0x000BBA54
		void IRestartable.Restart()
		{
			((IRestartable)this.output).Restart();
			base.Restart();
			this.tableLevel = 0;
			this.listLevel = 0;
			this.startedBlock = false;
			this.fontNameDictionary.Clear();
			this.colorDictionary.Clear();
			this.fontsTop = 0;
			this.delayedBottomMargin = 0;
			this.restartable = false;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x000BD8B2 File Offset: 0x000BBAB2
		void IRestartable.DisableRestart()
		{
			((IRestartable)this.output).DisableRestart();
			this.restartable = false;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x000BD8C6 File Offset: 0x000BBAC6
		public override void Initialize(FormatStore store, SourceFormat sourceFormat, string comment)
		{
			base.Initialize(store, sourceFormat, comment);
			store.InitializeCodepageDetector();
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x000BD8D7 File Offset: 0x000BBAD7
		public void OutputColors(int nextColorIndex)
		{
			this.firstColorHandle = nextColorIndex;
			this.AddColor(new PropertyValue(new RGBT(192U, 192U, 192U)));
			this.BuildColorsTable(base.FormatStore.RootNode);
			this.OutputColorsTableEntries();
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x000BD918 File Offset: 0x000BBB18
		public void OutputFonts(int firstAvailableFontHandle)
		{
			int bestWindowsCodePage = base.FormatStore.GetBestWindowsCodePage();
			Encoding utf;
			if (!Charset.TryGetEncoding(bestWindowsCodePage, out utf))
			{
				utf = Encoding.UTF8;
			}
			this.output.SetEncoding(utf);
			int charset = RtfSupport.CharSetFromCodePage((ushort)bestWindowsCodePage);
			this.firstFontHandle = firstAvailableFontHandle;
			int num;
			if (!this.fontNameDictionary.TryGetValue("Symbol", out num))
			{
				this.fonts[this.fontsTop].Name = "Symbol";
				this.fonts[this.fontsTop].SymbolFont = true;
				this.fontNameDictionary.Add(this.fonts[this.fontsTop].Name, this.fontsTop);
				this.symbolFont = this.fontsTop;
				this.fontsTop++;
			}
			else
			{
				this.symbolFont = num;
			}
			this.BuildFontsTable(base.FormatStore.RootNode);
			this.OutputFontsTableEntries(charset);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000BDA04 File Offset: 0x000BBC04
		public void WriteText(string buffer)
		{
			this.WriteText(buffer, 0, buffer.Length);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000BDA14 File Offset: 0x000BBC14
		public void WriteText(string buffer, int offset, int count)
		{
			this.HtmlRtfOffReally();
			this.output.WriteText(buffer, offset, count);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000BDA2A File Offset: 0x000BBC2A
		public void WriteText(char[] buffer, int offset, int count)
		{
			this.HtmlRtfOffReally();
			this.output.WriteText(buffer, offset, count);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000BDA40 File Offset: 0x000BBC40
		public void WriteEncapsulatedMarkupText(char[] buffer, int offset, int count)
		{
			this.HtmlRtfOffReally();
			this.output.WriteEncapsulatedMarkupText(buffer, offset, count);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000BDA56 File Offset: 0x000BBC56
		public void WriteDoubleEscapedText(string buffer)
		{
			this.WriteDoubleEscapedText(buffer, 0, buffer.Length);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x000BDA66 File Offset: 0x000BBC66
		public void WriteDoubleEscapedText(string buffer, int offset, int count)
		{
			this.HtmlRtfOffReally();
			this.output.WriteDoubleEscapedText(buffer, offset, count);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000BDA7C File Offset: 0x000BBC7C
		protected override bool StartRoot()
		{
			return true;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000BDA7F File Offset: 0x000BBC7F
		protected override void EndRoot()
		{
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x000BDA84 File Offset: 0x000BBC84
		protected override bool StartDocument()
		{
			int num;
			if (this.preferredEncoding != null)
			{
				num = CodePageMap.GetCodePage(this.preferredEncoding);
				num = base.FormatStore.GetBestWindowsCodePage(num);
			}
			else
			{
				num = base.FormatStore.GetBestWindowsCodePage();
			}
			Encoding utf;
			if (!Charset.TryGetEncoding(num, out utf))
			{
				utf = Encoding.UTF8;
			}
			this.output.SetEncoding(utf);
			if (this.resultFeedback != null)
			{
				this.resultFeedback.Set(ConfigParameter.OutputEncoding, utf);
			}
			int charset = RtfSupport.CharSetFromCodePage((ushort)num);
			this.fonts[this.fontsTop].Name = "Times New Roman";
			this.fontNameDictionary.Add(this.fonts[this.fontsTop].Name, this.fontsTop);
			int value = this.fontsTop;
			this.fontsTop++;
			this.fonts[this.fontsTop].Name = "Symbol";
			this.fonts[this.fontsTop].SymbolFont = true;
			this.fontNameDictionary.Add(this.fonts[this.fontsTop].Name, this.fontsTop);
			this.symbolFont = this.fontsTop;
			this.fontsTop++;
			this.firstColorHandle = 1;
			this.AddColor(new PropertyValue(new RGBT(192U, 192U, 192U)));
			this.BuildTables(base.CurrentNode);
			this.WriteControlText("{\\rtf1\\ansi", true);
			this.WriteControlText("\\fbidis", true);
			this.WriteKeyword("\\ansicpg", num);
			this.WriteKeyword("\\deff", value);
			if (base.SourceFormat == SourceFormat.Text)
			{
				this.WriteControlText("\\deftab720\\fromtext", true);
			}
			else if (base.SourceFormat == SourceFormat.HtmlEncapsulateMarkup)
			{
				this.WriteControlText("\\fromhtml1", true);
			}
			this.WriteControlText("{\\fonttbl", true);
			this.OutputFontsTableEntries(charset);
			this.WriteControlText("}\r\n", false);
			this.output.RtfLineLength = 0;
			this.WriteControlText("{\\colortbl;", false);
			this.OutputColorsTableEntries();
			this.WriteControlText("}\r\n", false);
			this.output.RtfLineLength = 0;
			this.WriteControlText("{\\*\\generator Microsoft Exchange Server;}\r\n", false);
			this.output.RtfLineLength = 0;
			if (base.Comment != null)
			{
				this.WriteControlText("{\\*\\formatConverter ", false);
				this.WriteControlText(base.Comment, false);
				this.WriteControlText(";}\r\n", false);
				this.output.RtfLineLength = 0;
			}
			if (!base.CurrentNode.FirstChild.IsNull && base.CurrentNode.FirstChild == base.CurrentNode.LastChild && (byte)(base.CurrentNode.LastChild.NodeType & FormatContainerType.BlockFlag) != 0)
			{
				PropertyValue property = base.CurrentNode.FirstChild.GetProperty(PropertyId.BackColor);
				if (property.IsColor)
				{
					this.WriteControlText("{\\*\\background {\\shp{\\*\\shpinst{\\sp{\\sn fillColor}{\\sv ", false);
					this.WriteControlText(RtfSupport.RGB((int)property.Color.Red, (int)property.Color.Green, (int)property.Color.Blue).ToString(), false);
					this.WriteControlText("}}}}}", false);
				}
			}
			this.WriteControlText("\\viewkind5\\viewscale100\r\n", false);
			this.output.RtfLineLength = 0;
			this.HtmlRtfOn();
			this.WriteControlText("{\\*\\bkmkstart BM_BEGIN}", false);
			this.HtmlRtfOff();
			if (base.SourceFormat == SourceFormat.HtmlEncapsulateMarkup)
			{
				this.WriteControlText("{\\*\\htmltag64}", false);
				this.OutputNodeStartEncapsulatedMarkup();
			}
			return true;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x000BDE2D File Offset: 0x000BC02D
		protected override void EndDocument()
		{
			this.OutputNodeEndEncapsulatedMarkup();
			this.WriteControlText("}\r\n", false);
			this.output.RtfLineLength = 0;
			this.output.Flush();
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x000BDE58 File Offset: 0x000BC058
		protected override bool StartTable()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			if (this.startedBlock)
			{
				this.ReallyEndBlock();
			}
			this.tableLevel++;
			return true;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000BDE7D File Offset: 0x000BC07D
		protected override void EndTable()
		{
			this.tableLevel--;
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000BDE93 File Offset: 0x000BC093
		protected override bool StartTableRow()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			if (this.tableLevel == 1)
			{
				this.HtmlRtfOn();
				this.OutputRowProps();
				this.HtmlRtfOff();
			}
			return true;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000BDEB8 File Offset: 0x000BC0B8
		protected override void EndTableRow()
		{
			this.HtmlRtfOn();
			this.OutputTableLevel();
			if (this.tableLevel > 1)
			{
				this.WriteControlText("{\\*\\nesttableprops", true);
				this.OutputRowProps();
				this.WriteControlText("\\nestrow}{\\nonesttables\\par}\r\n", false);
				this.textPosition += 2;
				this.output.RtfLineLength = 0;
			}
			else
			{
				this.WriteControlText("\\row\r\n", false);
				this.textPosition += 2;
				this.output.RtfLineLength = 0;
			}
			this.HtmlRtfOff();
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000BDF46 File Offset: 0x000BC146
		protected override bool StartTableCell()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			this.textPosition += 5;
			return true;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x000BDF60 File Offset: 0x000BC160
		protected override void EndTableCell()
		{
			this.HtmlRtfOn();
			if (!this.startedBlock)
			{
				this.OutputTableLevel();
				this.OutputBlockProps();
			}
			if (this.tableLevel > 1)
			{
				this.WriteControlText("\\nestcell{\\nonesttables\\tab}", false);
			}
			else
			{
				this.WriteControlText("\\cell", true);
			}
			this.textPosition++;
			this.startedBlock = false;
			this.HtmlRtfOff();
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000BDFCC File Offset: 0x000BC1CC
		protected override bool StartTableCaption()
		{
			if (base.CurrentNode.Parent.NodeType == FormatContainerType.Table)
			{
				this.tableLevel--;
			}
			return this.StartBlockContainer();
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x000BE00C File Offset: 0x000BC20C
		protected override void EndTableCaption()
		{
			this.EndBlockContainer();
			if (this.startedBlock)
			{
				this.ReallyEndBlock();
			}
			if (base.CurrentNode.Parent.NodeType == FormatContainerType.Table)
			{
				this.tableLevel++;
			}
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x000BE058 File Offset: 0x000BC258
		protected override bool StartList()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			if (this.startedBlock)
			{
				this.ReallyEndBlock();
			}
			if (this.listStack == null)
			{
				this.listStack = new RtfFormatOutput.ListLevel[8];
			}
			else if (this.listStack.Length == this.listLevel)
			{
				RtfFormatOutput.ListLevel[] destinationArray = new RtfFormatOutput.ListLevel[this.listStack.Length * 2];
				Array.Copy(this.listStack, 0, destinationArray, 0, this.listLevel);
				this.listStack = destinationArray;
			}
			if (this.listLevel == -1)
			{
				this.listLevel = 0;
			}
			this.listStack[this.listLevel].Reset();
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.ListStyle);
			if (!effectiveProperty.IsNull)
			{
				this.listStack[this.listLevel].ListType = (RtfNumbering)effectiveProperty.Enum;
			}
			effectiveProperty = base.GetEffectiveProperty(PropertyId.ListStart);
			if (!effectiveProperty.IsNull)
			{
				this.listStack[this.listLevel].NextIndex = (short)effectiveProperty.Integer;
			}
			this.listLevel++;
			return true;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x000BE15F File Offset: 0x000BC35F
		protected override void EndList()
		{
			this.listLevel--;
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000BE175 File Offset: 0x000BC375
		protected override bool StartListItem()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			if (this.startedBlock)
			{
				this.ReallyEndBlock();
			}
			if (this.listLevel == 0)
			{
				this.listLevel = -1;
			}
			return true;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000BE19C File Offset: 0x000BC39C
		protected override void EndListItem()
		{
			if (base.CurrentNode.FirstChild.IsNull)
			{
				this.ReallyStartAppropriateBlock();
			}
			if (this.startedBlock)
			{
				this.ReallyEndBlock();
			}
			if (this.listLevel > 0)
			{
				RtfFormatOutput.ListLevel[] array = this.listStack;
				int num = this.listLevel - 1;
				array[num].NextIndex = array[num].NextIndex + 1;
			}
			else
			{
				this.listLevel = 0;
			}
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x000BE210 File Offset: 0x000BC410
		protected override bool StartHyperLink()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			this.HtmlRtfOn();
			if (!this.startedBlock)
			{
				this.ReallyStartAppropriateBlock();
			}
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.HyperlinkUrl);
			if (!effectiveProperty.IsNull)
			{
				this.WriteControlText("{\\field{\\*\\fldinst HYPERLINK ", false);
				string text = base.FormatStore.GetStringValue(effectiveProperty).GetString();
				bool flag = false;
				if (text[0] == '#')
				{
					this.WriteControlText("\\\\l ", false);
					if (text.Length > 1)
					{
						if (!char.IsLetter(text[1]))
						{
							text = "BM_" + text.Substring(1);
						}
						else
						{
							text = text.Substring(1);
						}
					}
					else
					{
						text = string.Empty;
					}
					flag = true;
				}
				this.WriteControlText("\"", false);
				if (text.Length != 0)
				{
					this.WriteDoubleEscapedText(text);
				}
				else if (flag)
				{
					this.WriteControlText("BM_BEGIN", false);
				}
				else
				{
					this.WriteControlText("http://", false);
				}
				this.WriteControlText("\" }{\\fldrslt", true);
			}
			this.HtmlRtfOff();
			return true;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x000BE310 File Offset: 0x000BC510
		protected override void EndHyperLink()
		{
			this.HtmlRtfOn();
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.HyperlinkUrl);
			if (!effectiveProperty.IsNull)
			{
				this.WriteControlText("}}", false);
				string @string = base.FormatStore.GetStringValue(effectiveProperty).GetString();
				this.textPosition += @string.Length + 2;
			}
			FormatNode nextSibling = base.CurrentNode.NextSibling;
			if (!nextSibling.IsNull && (nextSibling.NodeType == FormatContainerType.Block || nextSibling.NodeType == FormatContainerType.List || nextSibling.NodeType == FormatContainerType.Table || nextSibling.NodeType == FormatContainerType.HorizontalLine))
			{
				this.EndBlockContainer();
			}
			this.HtmlRtfOff();
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x000BE3D0 File Offset: 0x000BC5D0
		protected override bool StartBookmark()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			this.HtmlRtfOn();
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.BookmarkName);
			if (!effectiveProperty.IsNull)
			{
				string @string = base.FormatStore.GetStringValue(effectiveProperty).GetString();
				if (@string != "BM_BEGIN")
				{
					this.WriteControlText("{\\*\\bkmkstart", true);
					if (!char.IsLetter(@string[0]))
					{
						this.WriteText("BM_");
					}
					this.WriteText(@string);
					this.WriteControlText("}", false);
				}
			}
			this.HtmlRtfOff();
			return true;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x000BE45C File Offset: 0x000BC65C
		protected override void EndBookmark()
		{
			this.HtmlRtfOn();
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.BookmarkName);
			if (!effectiveProperty.IsNull)
			{
				string @string = base.FormatStore.GetStringValue(effectiveProperty).GetString();
				if (@string != "BM_BEGIN")
				{
					this.WriteControlText("{\\*\\bkmkend", true);
					this.WriteText(@string);
					this.WriteControlText("}", false);
				}
			}
			this.HtmlRtfOff();
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x000BE4D0 File Offset: 0x000BC6D0
		protected override void StartEndImage()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			this.HtmlRtfOn();
			if (!this.startedBlock)
			{
				this.ReallyStartAppropriateBlock();
			}
			PropertyValue propertyValue = base.GetEffectiveProperty(PropertyId.Width);
			PropertyValue propertyValue2 = base.GetEffectiveProperty(PropertyId.Height);
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.ImageUrl);
			PropertyValue effectiveProperty2 = base.GetEffectiveProperty(PropertyId.ImageAltText);
			string text = null;
			if (!effectiveProperty.IsNull)
			{
				text = base.FormatStore.GetStringValue(effectiveProperty).GetString();
			}
			string text2 = null;
			if (!effectiveProperty2.IsNull)
			{
				text2 = base.FormatStore.GetStringValue(effectiveProperty2).GetString();
			}
			bool flag = false;
			if (this.imageRenderingCallback != null && text != null)
			{
				flag = this.imageRenderingCallback(text, this.textPosition);
			}
			if (flag)
			{
				this.WriteControlText("\\objattph  ", false);
				this.textPosition++;
			}
			else
			{
				if (propertyValue.IsNull || (!propertyValue.IsAbsLength && !propertyValue.IsPixels))
				{
					propertyValue = PropertyValue.Null;
				}
				else if (propertyValue.TwipsInteger == 0)
				{
					propertyValue.Set(LengthUnits.Pixels, 1f);
				}
				if (propertyValue2.IsNull || (!propertyValue2.IsAbsLength && !propertyValue2.IsPixels))
				{
					propertyValue2 = PropertyValue.Null;
				}
				else if (propertyValue2.TwipsInteger == 0)
				{
					propertyValue2.Set(LengthUnits.Pixels, 1f);
				}
				if (text != null)
				{
					this.WriteControlText("{\\field{\\*\\fldinst INCLUDEPICTURE \"", false);
					this.WriteDoubleEscapedText(text);
					this.WriteControlText("\" \\\\d \\\\* MERGEFORMAT}{\\fldrslt", true);
				}
				this.WriteControlText("{\\pict{\\*\\picprop{\\sp{\\sn fillColor}{\\sv 14286846}}{\\sp{\\sn fillOpacity}{\\sv 16384}}{\\sp{\\sn fFilled}{\\sv 1}}", false);
				if (text2 != null)
				{
					this.WriteControlText("{\\sp{\\sn wzDescription}{\\sv ", false);
					this.WriteText(text2);
					this.WriteControlText("}}", false);
				}
				this.WriteControlText("}", false);
				this.WriteControlText("\\brdrt\\brdrs\\brdrw10", true);
				this.WriteKeyword("\\brdrcf", this.firstColorHandle);
				this.WriteControlText("\\brdrl\\brdrs\\brdrw10", true);
				this.WriteKeyword("\\brdrcf", this.firstColorHandle);
				this.WriteControlText("\\brdrb\\brdrs\\brdrw10", true);
				this.WriteKeyword("\\brdrcf", this.firstColorHandle);
				this.WriteControlText("\\brdrr\\brdrs\\brdrw10", true);
				this.WriteKeyword("\\brdrcf", this.firstColorHandle);
				if (!propertyValue.IsNull)
				{
					this.WriteKeyword("\\picwgoal", propertyValue.TwipsInteger);
				}
				if (!propertyValue2.IsNull)
				{
					this.WriteKeyword("\\pichgoal", propertyValue2.TwipsInteger);
				}
				this.WriteControlText("\\wmetafile8 0100090000032100000000000500000000000400000003010800050000000b0200000000050000000c0202000200030000001e00040000002701ffff030000000000}", false);
				if (text != null)
				{
					this.WriteControlText("}}", false);
				}
				if ((propertyValue2.IsNull || propertyValue2.PixelsInteger >= 8) && (propertyValue.IsNull || propertyValue.PixelsInteger >= 8) && text != null)
				{
					this.textPosition += text.Length + 2;
				}
			}
			FormatNode nextSibling = base.CurrentNode.NextSibling;
			if (!nextSibling.IsNull && (nextSibling.NodeType == FormatContainerType.Block || nextSibling.NodeType == FormatContainerType.List || nextSibling.NodeType == FormatContainerType.Table || nextSibling.NodeType == FormatContainerType.HorizontalLine))
			{
				this.EndBlockContainer();
			}
			this.HtmlRtfOff();
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000BE7E4 File Offset: 0x000BC9E4
		protected override void StartEndHorizontalLine()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			this.HtmlRtfOn();
			if (this.startedBlock)
			{
				this.WriteControlText("\\par\r\n", false);
				this.textPosition += 2;
				this.output.RtfLineLength = 0;
				this.startedBlock = false;
			}
			this.OutputTableLevel();
			this.WriteControlText("\\plain", true);
			this.WriteControlText("{\\f0\\qc\\qd\\cf1\\ulth\\~ ________________________________ \\~\\par}\r\n", false);
			this.textPosition += 36;
			this.output.RtfLineLength = 0;
			this.HtmlRtfOff();
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x000BE877 File Offset: 0x000BCA77
		protected override void StartEndArea()
		{
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x000BE879 File Offset: 0x000BCA79
		protected override bool StartOption()
		{
			return true;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000BE87C File Offset: 0x000BCA7C
		protected override bool StartText()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			this.HtmlRtfOn();
			if (!this.startedBlock)
			{
				this.ReallyStartAppropriateBlock();
			}
			this.WriteControlText("{", false);
			this.OutputTextProps();
			this.HtmlRtfOff();
			return true;
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x000BE8B1 File Offset: 0x000BCAB1
		protected override bool ContinueText(uint beginTextPosition, uint endTextPosition)
		{
			this.OutputTextRuns(beginTextPosition, endTextPosition, false);
			return true;
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000BE8BD File Offset: 0x000BCABD
		protected override void EndText()
		{
			this.HtmlRtfOn();
			this.WriteControlText("}", false);
			this.HtmlRtfOff();
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x000BE8E0 File Offset: 0x000BCAE0
		protected override bool StartBlockContainer()
		{
			this.OutputNodeStartEncapsulatedMarkup();
			if (this.startedBlock)
			{
				this.ReallyEndBlock();
			}
			if (base.CurrentNode.FirstChild.IsNull || (byte)(base.CurrentNode.FirstChild.NodeType & FormatContainerType.BlockFlag) == 0)
			{
				this.ReallyStartAppropriateBlock();
			}
			return true;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x000BE940 File Offset: 0x000BCB40
		protected override void EndBlockContainer()
		{
			if (this.startedBlock && base.CurrentNode != base.CurrentNode.Parent.LastChild)
			{
				this.ReallyEndBlock();
			}
			this.OutputNodeEndEncapsulatedMarkup();
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000BE984 File Offset: 0x000BCB84
		protected override void Dispose(bool disposing)
		{
			if (this.output != null && this.output != null)
			{
				((IDisposable)this.output).Dispose();
			}
			this.fonts = null;
			this.fontNameDictionary = null;
			this.colors = null;
			this.colorDictionary = null;
			this.output = null;
			base.Dispose(disposing);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000BE9D8 File Offset: 0x000BCBD8
		private static bool IsTaggedFontName(string name, out int lengthWithoutTag)
		{
			lengthWithoutTag = name.Length;
			for (int i = 0; i < RtfFormatOutput.FontSuffixes.Length; i++)
			{
				if (RtfFormatOutput.FontSuffixes[i].Length < name.Length && name.EndsWith(RtfFormatOutput.FontSuffixes[i], StringComparison.OrdinalIgnoreCase))
				{
					lengthWithoutTag -= RtfFormatOutput.FontSuffixes[i].Length;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000BEA38 File Offset: 0x000BCC38
		private static int ConvertLengthToHalfPoints(PropertyValue pv)
		{
			switch (pv.Type)
			{
			case PropertyType.Percentage:
				return 0;
			case PropertyType.AbsLength:
				return pv.BaseUnits / 8 / 10;
			case PropertyType.RelLength:
				return 0;
			case PropertyType.Pixels:
				return pv.Value / 8 / 10;
			case PropertyType.Ems:
				return pv.Value / 8 / 10;
			case PropertyType.Exs:
				return pv.Value / 8 / 10;
			case PropertyType.HtmlFontUnits:
				return PropertyValue.ConvertHtmlFontUnitsToTwips(pv.HtmlFontUnits) / 10;
			case PropertyType.RelHtmlFontUnits:
				return 0;
			default:
				return 0;
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000BEAC3 File Offset: 0x000BCCC3
		private void HtmlRtfOn()
		{
			if (base.SourceFormat == SourceFormat.HtmlEncapsulateMarkup)
			{
				if (!this.htmlRtfOn)
				{
					this.htmlRtfOn = true;
					this.WriteControlText("\\htmlrtf", true);
					return;
				}
				if (this.htmlRtfOnOff)
				{
					this.htmlRtfOnOff = false;
				}
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000BEAF9 File Offset: 0x000BCCF9
		private void HtmlRtfOff()
		{
			if (this.htmlRtfOn)
			{
				this.htmlRtfOnOff = true;
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000BEB0A File Offset: 0x000BCD0A
		private void HtmlRtfOffReally()
		{
			if (this.htmlRtfOnOff && this.outOfOrderNesting == 0)
			{
				this.htmlRtfOnOff = false;
				this.htmlRtfOn = false;
				this.WriteControlText("\\htmlrtf0", true);
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x000BEB38 File Offset: 0x000BCD38
		private void OutputTextRuns(uint start, uint end, bool encapsulatedMarkup)
		{
			if (start != end)
			{
				TextRun textRun = base.FormatStore.GetTextRun(start);
				do
				{
					int num = textRun.EffectiveLength;
					TextRunType type = textRun.Type;
					if (type <= TextRunType.NbSp)
					{
						if (type != TextRunType.Markup)
						{
							if (type != TextRunType.NonSpace)
							{
								if (type == TextRunType.NbSp)
								{
									this.textPosition += num;
									int num2 = 0;
									do
									{
										this.WriteText(" ");
									}
									while (++num2 < num);
								}
							}
							else
							{
								this.textPosition += num;
								int num2 = 0;
								do
								{
									char[] buffer;
									int offset;
									int num3;
									textRun.GetChunk(num2, out buffer, out offset, out num3);
									this.WriteText(buffer, offset, num3);
									num2 += num3;
								}
								while (num2 < num);
							}
						}
						else if (this.outOfOrderNesting == 0)
						{
							if (!encapsulatedMarkup)
							{
								this.WriteControlText("{\\*\\htmltag0 ", false);
							}
							int num2 = 0;
							do
							{
								char[] buffer;
								int offset;
								int num3;
								textRun.GetChunk(num2, out buffer, out offset, out num3);
								this.WriteEncapsulatedMarkupText(buffer, offset, num3);
								num2 += num3;
							}
							while (num2 < num);
							if (!encapsulatedMarkup)
							{
								this.WriteControlText("}", false);
							}
						}
					}
					else if (type != TextRunType.Space)
					{
						if (type != TextRunType.Tabulation)
						{
							if (type == TextRunType.NewLine)
							{
								this.textPosition += num;
								if (!encapsulatedMarkup)
								{
									this.HtmlRtfOn();
									int num2 = 0;
									do
									{
										this.WriteControlText("\\line\r\n", false);
									}
									while (++num2 < num);
									this.output.RtfLineLength = 0;
									this.HtmlRtfOff();
								}
								if (base.SourceFormat == SourceFormat.HtmlEncapsulateMarkup && this.outOfOrderNesting == 0)
								{
									if (!encapsulatedMarkup)
									{
										this.WriteControlText("{\\*\\htmltag0", true);
									}
									do
									{
										this.WriteControlText("\\par", true);
									}
									while (0 < --num);
									if (!encapsulatedMarkup)
									{
										this.WriteControlText("}", false);
									}
								}
							}
						}
						else
						{
							this.textPosition += num;
							if (encapsulatedMarkup)
							{
								this.HtmlRtfOn();
								int num2 = 0;
								do
								{
									this.WriteControlText("\\tab", true);
								}
								while (++num2 < num);
								this.HtmlRtfOff();
							}
							if (base.SourceFormat == SourceFormat.HtmlEncapsulateMarkup && this.outOfOrderNesting == 0)
							{
								if (!encapsulatedMarkup)
								{
									this.WriteControlText("{\\*\\htmltag0 ", false);
								}
								do
								{
									this.WriteText("\t");
								}
								while (0 < --num);
								if (!encapsulatedMarkup)
								{
									this.WriteControlText("}", false);
								}
							}
						}
					}
					else
					{
						this.textPosition += num;
						do
						{
							this.WriteText(" ");
						}
						while (0 < --num);
					}
					textRun.MoveNext();
				}
				while (textRun.Position < end);
			}
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000BEDBC File Offset: 0x000BCFBC
		private void OutputNodeStartEncapsulatedMarkup()
		{
			if (base.SourceFormat == SourceFormat.HtmlEncapsulateMarkup)
			{
				if (!base.CurrentNode.IsInOrder)
				{
					this.outOfOrderNesting++;
					return;
				}
				if (base.CurrentNode.IsText || this.outOfOrderNesting != 0)
				{
					return;
				}
				uint beginTextPosition = base.CurrentNode.BeginTextPosition;
				FormatNode formatNode = base.CurrentNode.FirstChild;
				while (!formatNode.IsNull && !formatNode.IsInOrder)
				{
					formatNode = formatNode.NextSibling;
				}
				uint num;
				if (!formatNode.IsNull)
				{
					num = formatNode.BeginTextPosition;
				}
				else
				{
					num = base.CurrentNode.EndTextPosition;
				}
				if (beginTextPosition != num)
				{
					this.WriteControlText("{\\*\\htmltag0 ", false);
					this.OutputTextRuns(beginTextPosition, num, true);
					this.WriteControlText("}", false);
					this.runningPosition = num;
				}
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000BEE9C File Offset: 0x000BD09C
		private void OutputNodeEndEncapsulatedMarkup()
		{
			if (base.SourceFormat == SourceFormat.HtmlEncapsulateMarkup)
			{
				if (this.outOfOrderNesting > 0)
				{
					if (!base.CurrentNode.IsInOrder)
					{
						this.outOfOrderNesting--;
					}
					return;
				}
				uint endTextPosition = base.CurrentNode.EndTextPosition;
				FormatNode nextSibling = base.CurrentNode.NextSibling;
				while (!nextSibling.IsNull && !nextSibling.IsInOrder)
				{
					nextSibling = nextSibling.NextSibling;
				}
				uint num;
				if (!nextSibling.IsNull)
				{
					num = nextSibling.BeginTextPosition;
				}
				else
				{
					num = base.CurrentNode.Parent.EndTextPosition;
				}
				if (endTextPosition < num && num >= this.runningPosition)
				{
					if (endTextPosition < this.runningPosition)
					{
						endTextPosition = this.runningPosition;
					}
					if (endTextPosition != num)
					{
						this.WriteControlText("{\\*\\htmltag0 ", false);
						this.OutputTextRuns(endTextPosition, num, true);
						this.WriteControlText("}", false);
						this.runningPosition = num;
					}
				}
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000BEF90 File Offset: 0x000BD190
		private void ReallyStartAppropriateBlock()
		{
			this.OutputTableLevel();
			FormatNode currentNode = base.CurrentNode;
			if (currentNode.NodeType != FormatContainerType.Document)
			{
				this.OutputBlockProps();
			}
			if (this.listLevel != 0)
			{
				this.OutputListProperties();
			}
			this.WriteControlText("\\plain", true);
			if (currentNode.GetProperty(PropertyId.FontFace).IsNull)
			{
				this.WriteControlText("\\f0", true);
			}
			this.startedBlock = true;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000BEFFE File Offset: 0x000BD1FE
		private void ReallyEndBlock()
		{
			this.HtmlRtfOn();
			this.WriteControlText("\\par\r\n", false);
			this.textPosition += 2;
			this.output.RtfLineLength = 0;
			this.startedBlock = false;
			this.HtmlRtfOff();
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000BF03C File Offset: 0x000BD23C
		private void BuildTables(FormatNode node)
		{
			foreach (FormatNode formatNode in node.Subtree)
			{
				this.AddFont(formatNode.GetProperty(PropertyId.FontFace));
				this.AddColor(formatNode.GetProperty(PropertyId.FontColor));
				this.AddColor(formatNode.GetProperty(PropertyId.BackColor));
				this.AddColor(formatNode.GetProperty(PropertyId.BorderColors));
				this.AddColor(formatNode.GetProperty(PropertyId.RightBorderColor));
				this.AddColor(formatNode.GetProperty(PropertyId.BottomBorderColor));
				this.AddColor(formatNode.GetProperty(PropertyId.LeftBorderColor));
			}
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000BF0F8 File Offset: 0x000BD2F8
		private void BuildFontsTable(FormatNode node)
		{
			foreach (FormatNode formatNode in node.Subtree)
			{
				this.AddFont(formatNode.GetProperty(PropertyId.FontFace));
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x000BF158 File Offset: 0x000BD358
		private void BuildColorsTable(FormatNode node)
		{
			foreach (FormatNode formatNode in node.Subtree)
			{
				this.AddFont(formatNode.GetProperty(PropertyId.FontFace));
				this.AddColor(formatNode.GetProperty(PropertyId.FontColor));
				this.AddColor(formatNode.GetProperty(PropertyId.BackColor));
				this.AddColor(formatNode.GetProperty(PropertyId.BorderColors));
				this.AddColor(formatNode.GetProperty(PropertyId.RightBorderColor));
				this.AddColor(formatNode.GetProperty(PropertyId.BottomBorderColor));
				this.AddColor(formatNode.GetProperty(PropertyId.LeftBorderColor));
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000BF214 File Offset: 0x000BD414
		private void OutputFontsTableEntries(int charset)
		{
			for (int i = 0; i < this.fontsTop; i++)
			{
				this.WriteKeyword("{\\f", i + this.firstFontHandle);
				this.WriteControlText("\\fswiss", true);
				if (!this.fonts[i].SymbolFont)
				{
					this.WriteKeyword("\\fcharset", charset);
				}
				else
				{
					this.WriteKeyword("\\fcharset", 2);
				}
				int count;
				if (RtfFormatOutput.IsTaggedFontName(this.fonts[i].Name, out count))
				{
					this.WriteControlText("{\\fname", true);
					this.WriteText(this.fonts[i].Name);
					this.WriteControlText(";}", false);
					this.WriteText(this.fonts[i].Name, 0, count);
					this.WriteControlText(";}", false);
				}
				else
				{
					this.WriteText(this.fonts[i].Name);
					this.WriteControlText(";}", false);
				}
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x000BF318 File Offset: 0x000BD518
		private void OutputColorsTableEntries()
		{
			for (int i = 0; i < this.colorsTop; i++)
			{
				this.WriteKeyword("\\red", (int)this.colors[i].Red);
				this.WriteKeyword("\\green", (int)this.colors[i].Green);
				this.WriteKeyword("\\blue", (int)this.colors[i].Blue);
				this.WriteControlText(";", false);
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x000BF398 File Offset: 0x000BD598
		private void AddColor(PropertyValue pv)
		{
			if (pv.IsEnum)
			{
				pv = HtmlSupport.TranslateSystemColor(pv);
			}
			if (pv.IsColor && !this.colorDictionary.ContainsKey(pv.Color.RawValue) && this.colorsTop < this.colors.Length)
			{
				this.colors[this.colorsTop] = pv.Color;
				this.colorDictionary.Add(pv.Color.RawValue, this.colorsTop);
				this.colorsTop++;
			}
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x000BF438 File Offset: 0x000BD638
		private int FindColorHandle(PropertyValue pv)
		{
			if (pv.IsEnum)
			{
				pv = HtmlSupport.TranslateSystemColor(pv);
			}
			int num = 0;
			if (pv.IsColor && !pv.Color.IsTransparent && this.colorDictionary.TryGetValue(pv.Color.RawValue, out num))
			{
				return num + this.firstColorHandle;
			}
			return 0;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x000BF49C File Offset: 0x000BD69C
		private void AddFont(PropertyValue pv)
		{
			if (!pv.IsNull && this.fontsTop < this.fonts.Length)
			{
				string text = null;
				if (pv.IsString)
				{
					text = base.FormatStore.GetStringValue(pv).GetString();
				}
				else if (pv.IsMultiValue)
				{
					MultiValue multiValue = base.FormatStore.GetMultiValue(pv);
					if (multiValue.Length > 0)
					{
						text = multiValue.GetStringValue(0).GetString();
					}
				}
				if (text != null)
				{
					int num;
					if (!this.fontNameDictionary.TryGetValue(text, out num))
					{
						num = this.fontsTop;
						this.fonts[num].Name = text;
						this.fontNameDictionary.Add(text, num);
						foreach (string value in RtfFormatOutput.symbolFonts)
						{
							if (text.Equals(value, StringComparison.OrdinalIgnoreCase))
							{
								this.fonts[num].SymbolFont = true;
								break;
							}
						}
						this.fontsTop++;
					}
					RtfFormatOutput.OutputFont[] array2 = this.fonts;
					int num2 = num;
					array2[num2].Count = array2[num2].Count + 1;
				}
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000BF5C4 File Offset: 0x000BD7C4
		private int FindFontHandle(PropertyValue pv, out bool symbolFont)
		{
			int num = 0;
			symbolFont = false;
			if (!pv.IsNull)
			{
				string text = null;
				if (pv.IsString)
				{
					text = base.FormatStore.GetStringValue(pv).GetString();
				}
				else if (pv.IsMultiValue)
				{
					MultiValue multiValue = base.FormatStore.GetMultiValue(pv);
					if (multiValue.Length > 0)
					{
						text = multiValue.GetStringValue(0).GetString();
					}
				}
				if (text != null && this.fontNameDictionary.TryGetValue(text, out num))
				{
					symbolFont = this.fonts[num].SymbolFont;
					return num + this.firstFontHandle;
				}
			}
			return 0;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x000BF666 File Offset: 0x000BD866
		private void OutputTableLevel()
		{
			this.WriteControlText("\\pard", true);
			if (this.tableLevel > 0)
			{
				this.WriteControlText("\\intbl", true);
				if (this.tableLevel > 1)
				{
					this.WriteKeyword("\\itap", this.tableLevel);
				}
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000BF6A4 File Offset: 0x000BD8A4
		private void OutputListProperties()
		{
			bool flag = false;
			FormatNode x = base.CurrentNode;
			while (x.NodeType != FormatContainerType.Root && x.NodeType != FormatContainerType.ListItem)
			{
				if ((byte)(x.NodeType & FormatContainerType.BlockFlag) != 0 && !x.PreviousSibling.IsNull)
				{
					flag = true;
				}
				x = x.Parent;
			}
			if (x.NodeType != FormatContainerType.Root)
			{
				bool flag2 = x == x.Parent.FirstChild;
				bool flag3 = x == x.Parent.LastChild;
				x = x.Parent;
				if (!x.IsNull)
				{
					if (flag2)
					{
						PropertyValue property = x.GetProperty(PropertyId.Margins);
						if (property.IsAbsRelLength)
						{
							this.WriteKeyword("\\sb", property.TwipsInteger);
						}
					}
					if (flag3)
					{
						PropertyValue property2 = x.GetProperty(PropertyId.BottomMargin);
						if (property2.IsAbsRelLength)
						{
							this.WriteKeyword("\\sa", property2.TwipsInteger);
						}
					}
				}
			}
			if (this.listLevel == 1 && this.listStack[this.listLevel - 1].ListType == RtfNumbering.Arabic)
			{
				if (flag)
				{
					this.WriteControlText("\\pnlvlcont", true);
					this.WriteControlText("\\pnindent360", true);
					return;
				}
				this.WriteControlText("{\\pntext", true);
				string text = this.listStack[this.listLevel - 1].NextIndex.ToString();
				this.WriteText(text);
				this.WriteControlText(". ", false);
				if (text.Length == 1)
				{
					this.WriteControlText(" ", false);
				}
				this.WriteControlText("}", false);
				this.WriteControlText("{\\*\\pn", true);
				this.WriteControlText("\\pnlvlbody", true);
				this.WriteControlText("\\pndec", true);
				this.WriteKeyword("\\pnstart", (int)this.listStack[this.listLevel - 1].NextIndex);
				this.WriteControlText("\\pnindent360", true);
				this.WriteControlText("\\pnql", true);
				this.WriteControlText("{\\pntxta.}}", false);
				return;
			}
			else
			{
				if (flag)
				{
					this.WriteControlText("\\pnlvlcont", true);
					this.WriteControlText("\\pnindent240", true);
					return;
				}
				this.WriteControlText("{\\pntext", true);
				this.WriteControlText("*   }", false);
				this.WriteControlText("{\\*\\pn", true);
				this.WriteControlText("\\pnlvlblt", true);
				this.WriteKeyword("\\pnf", this.firstFontHandle + this.symbolFont);
				this.WriteControlText("\\pnindent240", true);
				this.WriteControlText("\\pnql", true);
				this.WriteControlText("{\\pntxtb\\'B7}}", false);
				return;
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000BF944 File Offset: 0x000BDB44
		private void OutputTextProps()
		{
			PropertyValue pv = base.GetEffectiveProperty(PropertyId.FontFace);
			if (!pv.IsNull)
			{
				bool flag;
				int value = this.FindFontHandle(pv, out flag);
				if (flag)
				{
					this.WriteKeyword("\\loch\\af", value);
					this.WriteControlText("\\dbch\\af0\\hich", true);
				}
				this.WriteKeyword("\\f", value);
			}
			pv = base.GetEffectiveProperty(PropertyId.RightToLeft);
			if (pv.IsBool)
			{
				this.WriteControlText(pv.Bool ? "\\rtlch" : "\\ltrch", true);
			}
			pv = base.GetEffectiveProperty(PropertyId.Language);
			if (pv.IsInteger)
			{
				this.WriteKeyword("\\lang", pv.Integer);
			}
			pv = base.GetEffectiveProperty(PropertyId.FontColor);
			if (pv.IsColor)
			{
				this.WriteKeyword("\\cf", this.FindColorHandle(pv));
			}
			pv = base.GetDistinctProperty(PropertyId.BackColor);
			if (pv.IsColor)
			{
				this.WriteKeyword("\\highlight", this.FindColorHandle(pv));
			}
			pv = base.GetEffectiveProperty(PropertyId.FontSize);
			if (!pv.IsNull)
			{
				int num = RtfFormatOutput.ConvertLengthToHalfPoints(pv);
				if (num != 0)
				{
					this.WriteKeyword("\\fs", num);
				}
			}
			pv = base.GetEffectiveProperty(PropertyId.FirstFlag);
			if (pv.IsBool)
			{
				this.WriteControlText(pv.Bool ? "\\b" : "\\b0", true);
			}
			pv = base.GetEffectiveProperty(PropertyId.Italic);
			if (pv.IsBool)
			{
				this.WriteControlText(pv.Bool ? "\\i" : "\\i0", true);
			}
			pv = base.GetEffectiveProperty(PropertyId.Underline);
			if (pv.IsBool)
			{
				this.WriteControlText(pv.Bool ? "\\ul" : "\\ul0", true);
			}
			pv = base.GetEffectiveProperty(PropertyId.Subscript);
			if (pv.IsBool)
			{
				this.WriteControlText(pv.Bool ? "\\sub" : "\\sub0", true);
			}
			pv = base.GetEffectiveProperty(PropertyId.Superscript);
			if (pv.IsBool)
			{
				this.WriteControlText(pv.Bool ? "\\super" : "\\super0", true);
			}
			pv = base.GetEffectiveProperty(PropertyId.Strikethrough);
			if (pv.IsBool)
			{
				this.WriteControlText(pv.Bool ? "\\strike" : "\\strike0", true);
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000BFB64 File Offset: 0x000BDD64
		private void OutputBlockProps()
		{
			int num = 0;
			int num2 = this.delayedBottomMargin;
			this.delayedBottomMargin = 0;
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.BackColor);
			if (effectiveProperty.IsColor)
			{
				this.WriteKeyword("\\cbpat", this.FindColorHandle(effectiveProperty));
			}
			effectiveProperty = base.GetEffectiveProperty(PropertyId.TextAlignment);
			if (effectiveProperty.IsEnum)
			{
				switch (effectiveProperty.Enum)
				{
				case 1:
					this.WriteControlText("\\qc", true);
					break;
				case 3:
					this.WriteControlText("\\ql", true);
					break;
				case 4:
					this.WriteControlText("\\qr", true);
					break;
				case 6:
					this.WriteControlText("\\qj", true);
					break;
				}
			}
			effectiveProperty = base.GetEffectiveProperty(PropertyId.Margins);
			if (effectiveProperty.IsAbsRelLength && (base.CurrentNode.Parent.IsNull || base.CurrentNode.Parent.FirstChild != base.CurrentNode))
			{
				num = effectiveProperty.TwipsInteger;
			}
			effectiveProperty = base.GetEffectiveProperty(PropertyId.BottomMargin);
			if (effectiveProperty.IsAbsRelLength)
			{
				this.delayedBottomMargin = effectiveProperty.TwipsInteger;
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			if (this.listLevel != 0)
			{
				if (this.listLevel == 1 && this.listStack[this.listLevel - 1].ListType == RtfNumbering.Arabic)
				{
					num3 = this.listLevel * 600;
					num5 = -360;
				}
				else
				{
					num3 = this.listLevel * 600;
					num5 = -240;
				}
			}
			effectiveProperty = base.GetEffectiveProperty(PropertyId.LeftMargin);
			if (effectiveProperty.IsAbsRelLength)
			{
				num3 += effectiveProperty.TwipsInteger;
			}
			effectiveProperty = base.GetEffectiveProperty(PropertyId.RightMargin);
			if (effectiveProperty.IsAbsRelLength)
			{
				num4 += effectiveProperty.TwipsInteger;
			}
			effectiveProperty = base.GetEffectiveProperty(PropertyId.FirstLineIndent);
			if (effectiveProperty.IsAbsRelLength)
			{
				num5 = effectiveProperty.TwipsInteger;
			}
			if (num3 != 0)
			{
				this.WriteKeyword("\\li", num3);
			}
			if (num4 != 0)
			{
				this.WriteKeyword("\\ri", num4);
			}
			if (num5 != 0)
			{
				this.WriteKeyword("\\fi", num5);
			}
			if (num2 != 0 || num != 0)
			{
				int num6;
				if (num < 0 != num2 < 0)
				{
					num6 = num + num2;
				}
				else
				{
					num6 = ((num >= 0) ? Math.Max(num, num2) : Math.Min(num, num2));
				}
				if (num6 != 0)
				{
					this.WriteKeyword("\\sb", num6);
				}
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000BFDB8 File Offset: 0x000BDFB8
		private void OutputRowProps()
		{
			this.WriteControlText("\\trowd", true);
			this.WriteKeyword("\\irow", base.CurrentNodeIndex);
			this.WriteKeyword("\\irowband", base.CurrentNodeIndex);
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.RightToLeft);
			if (effectiveProperty.IsBool && effectiveProperty.Bool)
			{
				this.WriteControlText("\\rtlrow", true);
			}
			effectiveProperty = base.GetEffectiveProperty(PropertyId.BackColor);
			if (effectiveProperty.IsColor)
			{
				this.WriteKeyword("\\trcbpat", this.FindColorHandle(effectiveProperty));
			}
			effectiveProperty = base.GetEffectiveProperty(PropertyId.Width);
			if (effectiveProperty.IsPercentage)
			{
				this.WriteControlText("\\trftsWidth2", true);
				this.WriteKeyword("\\trwWidth", effectiveProperty.Percentage10K / 200);
			}
			effectiveProperty = base.GetEffectiveProperty(PropertyId.Height);
			if (effectiveProperty.IsAbsLength || effectiveProperty.IsPixels)
			{
				this.WriteKeyword("\\trrh", effectiveProperty.TwipsInteger);
			}
			int num = 8856;
			FormatNode node = base.CurrentNode.FirstChild;
			if (!node.IsNull)
			{
				int num2 = 0;
				while (!node.IsNull)
				{
					num2++;
					node = node.NextSibling;
				}
				int num3 = num / num2;
				int num4 = 0;
				int num5 = 0;
				int num6 = num2;
				node = base.CurrentNode.FirstChild;
				while (!node.IsNull)
				{
					PropertyValue propertyValue;
					PropertyValue propertyValue2;
					this.OutputCellProps(node, out propertyValue, out propertyValue2);
					if (num2 == 1 && !propertyValue2.IsNull && node.FirstChild.IsNull)
					{
						this.WriteKeyword("\\fs", 2);
						if (propertyValue2.IsAbsLength || propertyValue2.IsPixels)
						{
							this.WriteKeyword("\\trrh", propertyValue2.TwipsInteger);
						}
					}
					int num7;
					if (num6 == 1)
					{
						num7 = ((num - num4 > 360) ? (num - num4) : num3);
					}
					else
					{
						num7 = num3;
						if (!propertyValue.IsNull)
						{
							if (propertyValue.IsPercentage)
							{
								num7 = num * (propertyValue.Percentage10K / 100) / 100 / 100;
							}
							else if (propertyValue.IsAbsLength || propertyValue.IsPixels)
							{
								num7 = propertyValue.TwipsInteger;
							}
						}
					}
					num4 += num7;
					this.WriteKeyword("\\cellx", num4);
					node = node.NextSibling;
					num5++;
					num6--;
				}
				return;
			}
			this.WriteKeyword("\\cellx", num);
			this.WriteControlText("\\cell", true);
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000C000C File Offset: 0x000BE20C
		private void OutputCellProps(FormatNode node, out PropertyValue cellWidth, out PropertyValue cellHeight)
		{
			bool flag = false;
			cellWidth = PropertyValue.Null;
			cellHeight = PropertyValue.Null;
			using (NodePropertiesEnumerator propertiesEnumerator = node.PropertiesEnumerator)
			{
				foreach (Property property in propertiesEnumerator)
				{
					switch (property.Id)
					{
					case PropertyId.BlockAlignment:
						switch (property.Value.Enum)
						{
						case 1:
							this.WriteControlText("\\clvertalc", true);
							break;
						case 2:
							this.WriteControlText("\\clvertalb", true);
							break;
						}
						break;
					case PropertyId.BackColor:
						this.WriteKeyword("\\clcbpat", this.FindColorHandle(property.Value));
						flag = true;
						break;
					case PropertyId.Width:
						cellWidth = property.Value;
						break;
					case PropertyId.Height:
						cellHeight = property.Value;
						break;
					case PropertyId.Paddings:
						if (property.Value.IsAbsRelLength)
						{
							this.WriteKeyword("\\clpadl", property.Value.TwipsInteger);
							this.WriteControlText("\\clpadfl3", true);
						}
						break;
					case PropertyId.RightPadding:
						if (property.Value.IsAbsRelLength)
						{
							this.WriteKeyword("\\clpadr", property.Value.TwipsInteger);
							this.WriteControlText("\\clpadfr3", true);
						}
						break;
					case PropertyId.BottomPadding:
						if (property.Value.IsAbsRelLength)
						{
							this.WriteKeyword("\\clpadb", property.Value.TwipsInteger);
							this.WriteControlText("\\clpadfb3", true);
						}
						break;
					case PropertyId.LeftPadding:
						if (property.Value.IsAbsRelLength)
						{
							this.WriteKeyword("\\clpadt", property.Value.TwipsInteger);
							this.WriteControlText("\\clpadft3", true);
						}
						break;
					}
				}
			}
			if (!flag)
			{
				PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.BackColor);
				if (!effectiveProperty.IsNull)
				{
					this.WriteKeyword("\\clcbpat", this.FindColorHandle(effectiveProperty));
				}
			}
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x000C02A8 File Offset: 0x000BE4A8
		private void WriteKeyword(string keyword, int value)
		{
			this.HtmlRtfOffReally();
			this.output.WriteKeyword(keyword, value);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x000C02BD File Offset: 0x000BE4BD
		private void WriteControlText(string controlText, bool lastKeyword)
		{
			this.HtmlRtfOffReally();
			this.output.WriteControlText(controlText, lastKeyword);
		}

		// Token: 0x04001B67 RID: 7015
		private static readonly string[] FontSuffixes = new string[]
		{
			" CE",
			" Cyr",
			" Greek",
			" Tur",
			" Baltic",
			" UPC"
		};

		// Token: 0x04001B68 RID: 7016
		private static string[] symbolFonts = new string[]
		{
			"Symbol",
			"Wingdings",
			"Wingdings 2",
			"Wingdings 3",
			"Webdings",
			"Marlett",
			"Map Symbols",
			"ZapfDingbats",
			"Monotype Sorts",
			"MT Extra",
			"Bookshelf Symbol 1",
			"Bookshelf Symbol 2",
			"Bookshelf Symbol 3",
			"Sign Language",
			"Shapes1",
			"Shapes2",
			"Bullets1",
			"Bullets2",
			"Bullets3",
			"Common Bullets",
			"Geographic Symbols",
			"Carta",
			"MICR",
			"Musical Symbols",
			"Sonata",
			"Almanac MT",
			"Bon Apetit MT",
			"Directions MT",
			"Holidays MT",
			"Keystrokes MT",
			"MS Outlook",
			"Parties MT",
			"Signs MT",
			"Sports Three MT",
			"Sports Two MT",
			"Transport MT",
			"Vacation MT"
		};

		// Token: 0x04001B69 RID: 7017
		private bool startedBlock;

		// Token: 0x04001B6A RID: 7018
		private IResultsFeedback resultFeedback;

		// Token: 0x04001B6B RID: 7019
		private bool restartable;

		// Token: 0x04001B6C RID: 7020
		private RtfOutput output;

		// Token: 0x04001B6D RID: 7021
		private int tableLevel;

		// Token: 0x04001B6E RID: 7022
		private RtfFormatOutput.ListLevel[] listStack;

		// Token: 0x04001B6F RID: 7023
		private int listLevel;

		// Token: 0x04001B70 RID: 7024
		private int textPosition;

		// Token: 0x04001B71 RID: 7025
		private Dictionary<string, int> fontNameDictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001B72 RID: 7026
		private Dictionary<uint, int> colorDictionary = new Dictionary<uint, int>();

		// Token: 0x04001B73 RID: 7027
		private RtfFormatOutput.OutputFont[] fonts = new RtfFormatOutput.OutputFont[100];

		// Token: 0x04001B74 RID: 7028
		private int fontsTop;

		// Token: 0x04001B75 RID: 7029
		private int firstFontHandle;

		// Token: 0x04001B76 RID: 7030
		private int symbolFont;

		// Token: 0x04001B77 RID: 7031
		private RGBT[] colors = new RGBT[100];

		// Token: 0x04001B78 RID: 7032
		private int colorsTop;

		// Token: 0x04001B79 RID: 7033
		private int firstColorHandle;

		// Token: 0x04001B7A RID: 7034
		private Encoding preferredEncoding;

		// Token: 0x04001B7B RID: 7035
		private int delayedBottomMargin;

		// Token: 0x04001B7C RID: 7036
		private ImageRenderingCallbackInternal imageRenderingCallback;

		// Token: 0x04001B7D RID: 7037
		private bool htmlRtfOn;

		// Token: 0x04001B7E RID: 7038
		private bool htmlRtfOnOff;

		// Token: 0x04001B7F RID: 7039
		private int outOfOrderNesting;

		// Token: 0x04001B80 RID: 7040
		private uint runningPosition;

		// Token: 0x0200024C RID: 588
		private struct ListLevel
		{
			// Token: 0x0600185F RID: 6239 RVA: 0x000C0470 File Offset: 0x000BE670
			public void Reset()
			{
				this.ListType = RtfNumbering.Bullet;
				this.Restart = false;
				this.NextIndex = 1;
			}

			// Token: 0x04001B81 RID: 7041
			public RtfNumbering ListType;

			// Token: 0x04001B82 RID: 7042
			public bool Restart;

			// Token: 0x04001B83 RID: 7043
			public short NextIndex;
		}

		// Token: 0x0200024D RID: 589
		private struct OutputFont
		{
			// Token: 0x04001B84 RID: 7044
			public string Name;

			// Token: 0x04001B85 RID: 7045
			public int Count;

			// Token: 0x04001B86 RID: 7046
			public bool SymbolFont;
		}
	}
}
