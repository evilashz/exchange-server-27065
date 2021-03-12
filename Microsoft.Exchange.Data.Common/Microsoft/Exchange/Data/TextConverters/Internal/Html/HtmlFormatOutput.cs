using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001CD RID: 461
	internal class HtmlFormatOutput : FormatOutput, IRestartable
	{
		// Token: 0x060013F3 RID: 5107 RVA: 0x0008DFD3 File Offset: 0x0008C1D3
		public HtmlFormatOutput(HtmlWriter writer, HtmlInjection injection, bool outputFragment, Stream formatTraceStream, Stream formatOutputTraceStream, bool filterHtml, HtmlTagCallback callback, bool recognizeHyperlinks) : base(formatOutputTraceStream)
		{
			this.writer = writer;
			this.injection = injection;
			this.outputFragment = outputFragment;
			this.filterHtml = filterHtml;
			this.callback = callback;
			this.recognizeHyperlinks = recognizeHyperlinks;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0008E00A File Offset: 0x0008C20A
		private static bool IsHyperLinkStartDelimiter(char c)
		{
			return c == '<' || c == '"' || c == '\'' || c == '(' || c == '[';
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0008E027 File Offset: 0x0008C227
		private static bool IsHyperLinkEndDelimiter(char c)
		{
			return c == '>' || c == '"' || c == '\'' || c == ')' || c == ']';
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0008E044 File Offset: 0x0008C244
		private void WriteIdAttribute(bool saveToCallbackContext)
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Id);
			if (!distinctProperty.IsNull)
			{
				string @string = base.FormatStore.GetStringValue(distinctProperty).GetString();
				if (saveToCallbackContext)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Id, @string);
					return;
				}
				this.writer.WriteAttributeName(HtmlNameIndex.Id);
				this.writer.WriteAttributeValue(@string);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0008E0A3 File Offset: 0x0008C2A3
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x0008E0AB File Offset: 0x0008C2AB
		internal HtmlWriter Writer
		{
			get
			{
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0008E0B4 File Offset: 0x0008C2B4
		public override bool OutputCodePageSameAsInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x0008E0B7 File Offset: 0x0008C2B7
		public override Encoding OutputEncoding
		{
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0008E0BE File Offset: 0x0008C2BE
		public override bool CanAcceptMoreOutput
		{
			get
			{
				return this.writer.CanAcceptMore;
			}
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0008E0CB File Offset: 0x0008C2CB
		bool IRestartable.CanRestart()
		{
			return this.writer != null && ((IRestartable)this.writer).CanRestart();
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0008E0E2 File Offset: 0x0008C2E2
		void IRestartable.Restart()
		{
			((IRestartable)this.writer).Restart();
			base.Restart();
			if (this.injection != null)
			{
				this.injection.Reset();
			}
			this.hyperlinkLevel = 0;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0008E10F File Offset: 0x0008C30F
		void IRestartable.DisableRestart()
		{
			if (this.writer != null)
			{
				((IRestartable)this.writer).DisableRestart();
			}
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0008E124 File Offset: 0x0008C324
		public override bool Flush()
		{
			if (!base.Flush())
			{
				return false;
			}
			this.writer.Flush();
			return true;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0008E13C File Offset: 0x0008C33C
		internal void SetWriter(HtmlWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0008E145 File Offset: 0x0008C345
		protected override bool StartRoot()
		{
			return true;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0008E148 File Offset: 0x0008C348
		protected override void EndRoot()
		{
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0008E14C File Offset: 0x0008C34C
		protected override bool StartDocument()
		{
			if (!this.outputFragment)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				this.writer.WriteStartTag(HtmlNameIndex.Html);
				if (this.callback != null)
				{
					if (this.callbackContext == null)
					{
						this.callbackContext = new HtmlFormatOutputCallbackContext(this);
					}
					this.callbackContext.InitializeTag(false, HtmlNameIndex.Head, false);
				}
				else
				{
					this.writer.WriteStartTag(HtmlNameIndex.Head);
				}
				if (this.callback != null)
				{
					this.callbackContext.InitializeFragment(false);
					this.callback(this.callbackContext, this.writer);
					this.callbackContext.UninitializeFragment();
					if (this.callbackContext.IsInvokeCallbackForEndTag)
					{
						flag = true;
					}
					if (this.callbackContext.IsDeleteInnerContent)
					{
						flag2 = true;
					}
					if (this.callbackContext.IsDeleteEndTag)
					{
						flag3 = true;
					}
				}
				if (!flag2)
				{
					if (this.writer.HasEncoding)
					{
						this.writer.WriteStartTag(HtmlNameIndex.Meta);
						this.writer.WriteAttribute(HtmlNameIndex.HttpEquiv, "Content-Type");
						this.writer.WriteAttributeName(HtmlNameIndex.Content);
						this.writer.WriteAttributeValueInternal("text/html; charset=");
						this.writer.WriteAttributeValue(Charset.GetCharset(this.writer.Encoding).Name);
						this.writer.WriteNewLine(true);
					}
					this.writer.WriteStartTag(HtmlNameIndex.Meta);
					this.writer.WriteAttribute(HtmlNameIndex.Name, "Generator");
					this.writer.WriteAttribute(HtmlNameIndex.Content, "Microsoft Exchange Server");
					this.writer.WriteNewLine(true);
					if (base.Comment != null)
					{
						this.writer.WriteMarkupText("<!-- " + base.Comment + " -->");
						this.writer.WriteNewLine(true);
					}
					this.writer.WriteStartTag(HtmlNameIndex.Style);
					this.writer.WriteMarkupText("<!-- .EmailQuote { margin-left: 1pt; padding-left: 4pt; border-left: #800000 2px solid; } -->");
					this.writer.WriteEndTag(HtmlNameIndex.Style);
				}
				if (flag)
				{
					this.callbackContext.InitializeTag(true, HtmlNameIndex.Head, flag3);
					this.callbackContext.InitializeFragment(false);
					this.callback(this.callbackContext, this.writer);
					this.callbackContext.UninitializeFragment();
				}
				else if (!flag3)
				{
					this.writer.WriteEndTag(HtmlNameIndex.Head);
					this.writer.WriteNewLine(true);
				}
				this.writer.WriteStartTag(HtmlNameIndex.Body);
				this.writer.WriteNewLine(true);
			}
			else
			{
				this.writer.WriteStartTag(HtmlNameIndex.Div);
				this.writer.WriteAttribute(HtmlNameIndex.Class, "BodyFragment");
				this.writer.WriteNewLine(true);
			}
			if (this.injection != null && this.injection.HaveHead)
			{
				this.injection.Inject(true, this.writer);
			}
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0008E404 File Offset: 0x0008C604
		protected override void EndDocument()
		{
			this.RevertCharFormat();
			if (this.injection != null && this.injection.HaveTail)
			{
				this.injection.Inject(false, this.writer);
			}
			if (!this.outputFragment)
			{
				this.writer.WriteNewLine(true);
				this.writer.WriteEndTag(HtmlNameIndex.Body);
				this.writer.WriteNewLine(true);
				this.writer.WriteEndTag(HtmlNameIndex.Html);
			}
			else
			{
				this.writer.WriteNewLine(true);
				this.writer.WriteEndTag(HtmlNameIndex.Div);
			}
			this.writer.WriteNewLine(true);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0008E4A5 File Offset: 0x0008C6A5
		protected override void StartEndBaseFont()
		{
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0008E4A8 File Offset: 0x0008C6A8
		protected override bool StartTable()
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.FontFace);
			if (!distinctProperty.IsNull)
			{
				this.writer.WriteStartTag(HtmlNameIndex.Font);
				this.writer.WriteAttributeName(HtmlNameIndex.Face);
				if (distinctProperty.IsMultiValue)
				{
					MultiValue multiValue = base.FormatStore.GetMultiValue(distinctProperty);
					for (int i = 0; i < multiValue.Length; i++)
					{
						string @string = multiValue.GetStringValue(i).GetString();
						if (i != 0)
						{
							this.writer.WriteAttributeValue(",");
						}
						this.writer.WriteAttributeValue(@string);
					}
				}
				else
				{
					string @string = base.FormatStore.GetStringValue(distinctProperty).GetString();
					this.writer.WriteAttributeValue(@string);
				}
			}
			this.writer.WriteNewLine(true);
			this.writer.WriteStartTag(HtmlNameIndex.Table);
			this.OutputTableTagAttributes();
			bool flag = false;
			this.OutputTableCssProperties(ref flag);
			this.OutputBlockCssProperties(ref flag);
			this.writer.WriteNewLine(true);
			return true;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0008E5AC File Offset: 0x0008C7AC
		protected override void EndTable()
		{
			this.writer.WriteNewLine(true);
			this.writer.WriteEndTag(HtmlNameIndex.Table);
			this.writer.WriteNewLine(true);
			if (!base.GetDistinctProperty(PropertyId.FontFace).IsNull)
			{
				this.writer.WriteEndTag(HtmlNameIndex.Font);
			}
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0008E604 File Offset: 0x0008C804
		protected override bool StartTableColumnGroup()
		{
			this.writer.WriteNewLine(true);
			this.writer.WriteStartTag(HtmlNameIndex.ColGroup);
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Width);
			if (!distinctProperty.IsNull && distinctProperty.IsAbsRelLength)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Width, distinctProperty.PixelsInteger.ToString());
			}
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.NumColumns);
			if (!distinctProperty2.IsNull && distinctProperty2.IsAbsRelLength)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Span, distinctProperty2.Integer.ToString());
			}
			bool flag = false;
			this.OutputTableColumnCssProperties(ref flag);
			return true;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0008E6A7 File Offset: 0x0008C8A7
		protected override void EndTableColumnGroup()
		{
			this.writer.WriteEndTag(HtmlNameIndex.ColGroup);
			this.writer.WriteNewLine(true);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0008E6C8 File Offset: 0x0008C8C8
		protected override void StartEndTableColumn()
		{
			this.writer.WriteStartTag(HtmlNameIndex.Col);
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Width);
			if (!distinctProperty.IsNull && distinctProperty.IsAbsRelLength)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Width, distinctProperty.PixelsInteger.ToString());
			}
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.NumColumns);
			if (!distinctProperty2.IsNull && distinctProperty2.IsAbsRelLength)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Span, distinctProperty2.Integer.ToString());
			}
			bool flag = false;
			this.OutputTableColumnCssProperties(ref flag);
			this.writer.WriteNewLine(true);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0008E76C File Offset: 0x0008C96C
		protected override bool StartTableCaption()
		{
			this.writer.WriteNewLine(true);
			if (!base.CurrentNode.Parent.IsNull && base.CurrentNode.Parent.NodeType == FormatContainerType.Table)
			{
				this.writer.WriteStartTag(HtmlNameIndex.Caption);
				FormatStyle style = base.FormatStore.GetStyle(13);
				base.SubtractDefaultContainerPropertiesFromDistinct(style.FlagProperties, style.PropertyList);
				PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.BlockAlignment);
				if (!distinctProperty.IsNull)
				{
					string blockAlignmentString = HtmlSupport.GetBlockAlignmentString(distinctProperty);
					if (blockAlignmentString != null)
					{
						this.writer.WriteAttribute(HtmlNameIndex.Align, blockAlignmentString);
					}
				}
				this.writer.WriteNewLine(true);
			}
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0008E834 File Offset: 0x0008CA34
		protected override void EndTableCaption()
		{
			this.RevertCharFormat();
			if (!base.CurrentNode.Parent.IsNull && base.CurrentNode.Parent.NodeType == FormatContainerType.Table)
			{
				this.writer.WriteNewLine(true);
				this.writer.WriteEndTag(HtmlNameIndex.Caption);
			}
			this.writer.WriteNewLine(true);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0008E8A4 File Offset: 0x0008CAA4
		protected override bool StartTableExtraContent()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0008E8AC File Offset: 0x0008CAAC
		protected override void EndTableExtraContent()
		{
			this.EndBlockContainer();
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0008E8B4 File Offset: 0x0008CAB4
		protected override bool StartTableRow()
		{
			this.writer.WriteNewLine(true);
			this.writer.WriteStartTag(HtmlNameIndex.TR);
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Height);
			if (!distinctProperty.IsNull && distinctProperty.IsAbsRelLength)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Height, distinctProperty.PixelsInteger.ToString());
			}
			bool flag = false;
			this.OutputBlockCssProperties(ref flag);
			this.writer.WriteNewLine(true);
			return true;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0008E92B File Offset: 0x0008CB2B
		protected override void EndTableRow()
		{
			this.writer.WriteNewLine(true);
			this.writer.WriteEndTag(HtmlNameIndex.TR);
			this.writer.WriteNewLine(true);
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0008E958 File Offset: 0x0008CB58
		protected override bool StartTableCell()
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.MergedCell);
			if (distinctProperty.IsNull || !distinctProperty.Bool)
			{
				this.writer.WriteNewLine(true);
				this.writer.WriteStartTag(HtmlNameIndex.TD);
				this.OutputTableCellTagAttributes();
				bool flag = false;
				this.OutputBlockCssProperties(ref flag);
				this.ApplyCharFormat();
			}
			return true;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0008E9B0 File Offset: 0x0008CBB0
		protected override void EndTableCell()
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.MergedCell);
			if (distinctProperty.IsNull || !distinctProperty.Bool)
			{
				this.RevertCharFormat();
				this.writer.WriteEndTag(HtmlNameIndex.TD);
				this.writer.WriteNewLine(true);
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0008E9F8 File Offset: 0x0008CBF8
		protected override bool StartList()
		{
			this.writer.WriteNewLine(true);
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.ListStyle);
			bool flag = true;
			if (effectiveProperty.IsNull || effectiveProperty.Enum == 1)
			{
				this.writer.WriteStartTag(HtmlNameIndex.UL);
			}
			else
			{
				this.writer.WriteStartTag(HtmlNameIndex.OL);
				flag = false;
			}
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.RightToLeft);
			if (!distinctProperty.IsNull)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Dir, distinctProperty.Bool ? "rtl" : "ltr");
			}
			if (!flag && effectiveProperty.Enum != 2)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Type, HtmlFormatOutput.listType[effectiveProperty.Enum]);
			}
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.ListStart);
			if (!flag && distinctProperty2.IsInteger && distinctProperty2.Integer != 1)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Start, distinctProperty2.Integer.ToString());
			}
			bool flag2 = false;
			this.OutputBlockCssProperties(ref flag2);
			this.writer.WriteNewLine(true);
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0008EB0C File Offset: 0x0008CD0C
		protected override void EndList()
		{
			this.RevertCharFormat();
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.ListStyle);
			this.writer.WriteNewLine(true);
			if (effectiveProperty.IsNull || effectiveProperty.Enum == 1)
			{
				this.writer.WriteEndTag(HtmlNameIndex.UL);
			}
			else
			{
				this.writer.WriteEndTag(HtmlNameIndex.OL);
			}
			this.writer.WriteNewLine(true);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0008EB78 File Offset: 0x0008CD78
		protected override bool StartListItem()
		{
			this.writer.WriteNewLine(true);
			this.writer.WriteStartTag(HtmlNameIndex.LI);
			bool flag = false;
			this.OutputBlockCssProperties(ref flag);
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0008EBAF File Offset: 0x0008CDAF
		protected override void EndListItem()
		{
			this.RevertCharFormat();
			this.writer.WriteEndTag(HtmlNameIndex.LI);
			this.writer.WriteNewLine(true);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0008EBD0 File Offset: 0x0008CDD0
		protected override bool StartHyperLink()
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			FlagProperties flags = default(FlagProperties);
			flags.Set(PropertyId.Underline, true);
			base.SubtractDefaultContainerPropertiesFromDistinct(flags, HtmlFormatOutput.defaultHyperlinkProperties);
			if (this.callback != null)
			{
				if (this.callbackContext == null)
				{
					this.callbackContext = new HtmlFormatOutputCallbackContext(this);
				}
				this.callbackContext.InitializeTag(false, HtmlNameIndex.A, false);
			}
			else
			{
				this.writer.WriteStartTag(HtmlNameIndex.A);
			}
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.HyperlinkUrl);
			if (!distinctProperty.IsNull)
			{
				string text = base.FormatStore.GetStringValue(distinctProperty).GetString();
				if (this.filterHtml && !HtmlToHtmlConverter.IsUrlSafe(text, this.callback != null))
				{
					text = string.Empty;
				}
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Href, text);
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Href);
					this.writer.WriteAttributeValue(text);
				}
				distinctProperty = base.GetDistinctProperty(PropertyId.HyperlinkTarget);
				if (!distinctProperty.IsNull)
				{
					string targetString = HtmlSupport.GetTargetString(distinctProperty);
					if (this.callback != null)
					{
						this.callbackContext.AddAttribute(HtmlNameIndex.Target, targetString);
					}
					else
					{
						this.writer.WriteAttributeName(HtmlNameIndex.Target);
						this.writer.WriteAttributeValue(targetString);
					}
				}
				this.WriteIdAttribute(this.callback != null);
			}
			if (this.callback != null)
			{
				this.callbackContext.InitializeFragment(false);
				this.callback(this.callbackContext, this.writer);
				this.callbackContext.UninitializeFragment();
				if (this.callbackContext.IsInvokeCallbackForEndTag)
				{
					flag3 = true;
				}
				if (this.callbackContext.IsDeleteInnerContent)
				{
					flag2 = true;
				}
				if (this.callbackContext.IsDeleteEndTag)
				{
					flag = true;
				}
				if (flag || flag3)
				{
					if (this.endTagActionStack == null)
					{
						this.endTagActionStack = new HtmlFormatOutput.EndTagActionEntry[4];
					}
					else if (this.endTagActionStack.Length == this.endTagActionStackTop)
					{
						HtmlFormatOutput.EndTagActionEntry[] destinationArray = new HtmlFormatOutput.EndTagActionEntry[this.endTagActionStack.Length * 2];
						Array.Copy(this.endTagActionStack, 0, destinationArray, 0, this.endTagActionStackTop);
						this.endTagActionStack = destinationArray;
					}
					this.endTagActionStack[this.endTagActionStackTop].TagLevel = this.hyperlinkLevel;
					this.endTagActionStack[this.endTagActionStackTop].Drop = flag;
					this.endTagActionStack[this.endTagActionStackTop].Callback = flag3;
					this.endTagActionStackTop++;
				}
			}
			this.hyperlinkLevel++;
			if (!flag2)
			{
				this.ApplyCharFormat();
			}
			else
			{
				this.CloseHyperLink();
			}
			if (this.writer.IsTagOpen)
			{
				this.writer.WriteTagEnd();
			}
			return !flag2;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0008EE7C File Offset: 0x0008D07C
		protected override void EndHyperLink()
		{
			this.hyperlinkLevel--;
			this.RevertCharFormat();
			this.CloseHyperLink();
			if (this.writer.IsTagOpen)
			{
				this.writer.WriteTagEnd();
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0008EEB0 File Offset: 0x0008D0B0
		protected override bool StartBookmark()
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.BookmarkName);
			if (!distinctProperty.IsNull)
			{
				this.writer.WriteStartTag(HtmlNameIndex.A);
				string @string = base.FormatStore.GetStringValue(distinctProperty).GetString();
				this.writer.WriteAttributeName(HtmlNameIndex.Name);
				this.writer.WriteAttributeValue(@string);
			}
			this.ApplyCharFormat();
			if (this.writer.IsTagOpen)
			{
				this.writer.WriteTagEnd();
			}
			return true;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0008EF2C File Offset: 0x0008D12C
		protected override void EndBookmark()
		{
			this.RevertCharFormat();
			if (!base.GetDistinctProperty(PropertyId.BookmarkName).IsNull)
			{
				this.writer.WriteEndTag(HtmlNameIndex.A);
			}
			if (this.writer.IsTagOpen)
			{
				this.writer.WriteTagEnd();
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0008EF78 File Offset: 0x0008D178
		protected override void StartEndImage()
		{
			if (this.callback != null)
			{
				if (this.callbackContext == null)
				{
					this.callbackContext = new HtmlFormatOutputCallbackContext(this);
				}
				this.callbackContext.InitializeTag(false, HtmlNameIndex.Img, false);
			}
			else
			{
				this.writer.WriteStartTag(HtmlNameIndex.Img);
			}
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Width);
			if (!distinctProperty.IsNull)
			{
				BufferString value = HtmlSupport.FormatPixelOrPercentageLength(ref this.scratchBuffer, distinctProperty);
				if (value.Length != 0)
				{
					if (this.callback != null)
					{
						this.callbackContext.AddAttribute(HtmlNameIndex.Width, value.ToString());
					}
					else
					{
						this.writer.WriteAttribute(HtmlNameIndex.Width, value);
					}
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.Height);
			if (!distinctProperty.IsNull)
			{
				BufferString value2 = HtmlSupport.FormatPixelOrPercentageLength(ref this.scratchBuffer, distinctProperty);
				if (value2.Length != 0)
				{
					if (this.callback != null)
					{
						this.callbackContext.AddAttribute(HtmlNameIndex.Height, value2.ToString());
					}
					else
					{
						this.writer.WriteAttribute(HtmlNameIndex.Height, value2);
					}
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.BlockAlignment);
			if (!distinctProperty.IsNull)
			{
				string blockAlignmentString = HtmlSupport.GetBlockAlignmentString(distinctProperty);
				if (blockAlignmentString != null)
				{
					if (this.callback != null)
					{
						this.callbackContext.AddAttribute(HtmlNameIndex.Align, blockAlignmentString);
					}
					else
					{
						this.writer.WriteAttribute(HtmlNameIndex.Align, blockAlignmentString);
					}
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.TableBorder);
			if (!distinctProperty.IsNull)
			{
				BufferString value3 = HtmlSupport.FormatPixelOrPercentageLength(ref this.scratchBuffer, distinctProperty);
				if (value3.Length != 0)
				{
					if (this.callback != null)
					{
						this.callbackContext.AddAttribute(HtmlNameIndex.Border, value3.ToString());
					}
					else
					{
						this.writer.WriteAttribute(HtmlNameIndex.Border, value3);
					}
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.ImageUrl);
			if (!distinctProperty.IsNull)
			{
				string text = base.FormatStore.GetStringValue(distinctProperty).GetString();
				if (this.filterHtml && !HtmlToHtmlConverter.IsUrlSafe(text, this.callback != null))
				{
					text = string.Empty;
				}
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Src, text);
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Src);
					this.writer.WriteAttributeValue(text);
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.BookmarkName);
			if (!distinctProperty.IsNull)
			{
				string text2 = base.FormatStore.GetStringValue(distinctProperty).GetString();
				if (this.filterHtml && !HtmlToHtmlConverter.IsUrlSafe(text2, this.callback != null))
				{
					text2 = string.Empty;
				}
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.UseMap, text2);
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.UseMap);
					this.writer.WriteAttributeValue(text2);
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.RightToLeft);
			if (distinctProperty.IsBool)
			{
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Dir, distinctProperty.Bool ? "rtl" : "ltr");
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Dir);
					this.writer.WriteAttributeValue(distinctProperty.Bool ? "rtl" : "ltr");
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.Language);
			Culture culture;
			if (distinctProperty.IsInteger && (Culture.TryGetCulture(distinctProperty.Integer, out culture) || string.IsNullOrEmpty(culture.Name)))
			{
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Lang, culture.Name);
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Lang);
					this.writer.WriteAttributeValue(culture.Name);
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.ImageAltText);
			if (!distinctProperty.IsNull)
			{
				string @string = base.FormatStore.GetStringValue(distinctProperty).GetString();
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Alt, @string);
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Alt);
					this.writer.WriteAttributeValue(@string);
				}
			}
			if (this.callback != null)
			{
				this.callbackContext.InitializeFragment(true);
				this.callback(this.callbackContext, this.writer);
				this.callbackContext.UninitializeFragment();
			}
			if (this.writer.IsTagOpen)
			{
				this.writer.WriteTagEnd();
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0008F3AC File Offset: 0x0008D5AC
		protected override void StartEndHorizontalLine()
		{
			this.writer.WriteNewLine(true);
			this.writer.WriteStartTag(HtmlNameIndex.HR);
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Width);
			if (!distinctProperty.IsNull)
			{
				BufferString value = HtmlSupport.FormatPixelOrPercentageLength(ref this.scratchBuffer, distinctProperty);
				if (value.Length != 0)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Width, value);
				}
			}
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.Height);
			if (!distinctProperty2.IsNull && distinctProperty2.IsAbsRelLength)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Size, distinctProperty2.PixelsInteger.ToString());
			}
			PropertyValue distinctProperty3 = base.GetDistinctProperty(PropertyId.BlockAlignment);
			if (!distinctProperty3.IsNull)
			{
				string horizontalAlignmentString = HtmlSupport.GetHorizontalAlignmentString(distinctProperty3);
				if (horizontalAlignmentString != null)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Align, horizontalAlignmentString);
				}
			}
			PropertyValue distinctProperty4 = base.GetDistinctProperty(PropertyId.FontColor);
			if (!distinctProperty4.IsNull)
			{
				BufferString value2 = HtmlSupport.FormatColor(ref this.scratchBuffer, distinctProperty4);
				if (value2.Length != 0)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Color, value2);
				}
			}
			if (!distinctProperty.IsNull)
			{
				this.writer.WriteAttributeName(HtmlNameIndex.Style);
				if (!distinctProperty.IsNull)
				{
					BufferString value3 = HtmlSupport.FormatLength(ref this.scratchBuffer, distinctProperty);
					if (value3.Length != 0)
					{
						this.writer.WriteAttributeValue("width:");
						this.writer.WriteAttributeValue(value3);
						this.writer.WriteAttributeValue(";");
					}
				}
			}
			if (this.writer.LiteralWhitespaceNesting == 0)
			{
				this.writer.WriteNewLine(true);
			}
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0008F529 File Offset: 0x0008D729
		protected override bool StartInline()
		{
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0008F532 File Offset: 0x0008D732
		protected override void EndInline()
		{
			this.RevertCharFormat();
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0008F53C File Offset: 0x0008D73C
		protected override bool StartMap()
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.BookmarkName);
			if (!distinctProperty.IsNull)
			{
				this.writer.WriteStartTag(HtmlNameIndex.Map);
				string text = base.FormatStore.GetStringValue(distinctProperty).GetString();
				if (this.filterHtml && !HtmlToHtmlConverter.IsUrlSafe(text, this.callback != null))
				{
					text = string.Empty;
				}
				this.writer.WriteAttributeName(HtmlNameIndex.Name);
				this.writer.WriteAttributeValue(text);
				this.writer.WriteNewLine(true);
			}
			return true;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0008F5C8 File Offset: 0x0008D7C8
		protected override void EndMap()
		{
			if (!base.GetDistinctProperty(PropertyId.BookmarkName).IsNull)
			{
				this.writer.WriteEndTag(HtmlNameIndex.Map);
				this.writer.WriteNewLine(true);
			}
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0008F600 File Offset: 0x0008D800
		protected override void StartEndArea()
		{
			if (this.callback != null)
			{
				if (this.callbackContext == null)
				{
					this.callbackContext = new HtmlFormatOutputCallbackContext(this);
				}
				this.callbackContext.InitializeTag(false, HtmlNameIndex.Area, false);
			}
			else
			{
				this.writer.WriteStartTag(HtmlNameIndex.Area);
			}
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.HyperlinkUrl);
			if (!distinctProperty.IsNull)
			{
				string text = base.FormatStore.GetStringValue(distinctProperty).GetString();
				if (this.filterHtml && !HtmlToHtmlConverter.IsUrlSafe(text, this.callback != null))
				{
					text = string.Empty;
				}
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Href, text);
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Href);
					this.writer.WriteAttributeValue(text);
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.HyperlinkTarget);
			if (!distinctProperty.IsNull)
			{
				string targetString = HtmlSupport.GetTargetString(distinctProperty);
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Target, targetString);
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Target);
					this.writer.WriteAttributeValue(targetString);
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.Shape);
			if (!distinctProperty.IsNull)
			{
				string areaShapeString = HtmlSupport.GetAreaShapeString(distinctProperty);
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Shape, areaShapeString);
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Shape);
					this.writer.WriteAttributeValue(areaShapeString);
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.Coords);
			if (!distinctProperty.IsNull)
			{
				string @string = base.FormatStore.GetStringValue(distinctProperty).GetString();
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Coords, @string);
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Coords);
					this.writer.WriteAttributeValue(@string);
				}
			}
			distinctProperty = base.GetDistinctProperty(PropertyId.ImageAltText);
			if (!distinctProperty.IsNull)
			{
				string string2 = base.FormatStore.GetStringValue(distinctProperty).GetString();
				if (this.callback != null)
				{
					this.callbackContext.AddAttribute(HtmlNameIndex.Alt, string2);
				}
				else
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Alt);
					this.writer.WriteAttributeValue(string2);
				}
			}
			if (this.callback != null)
			{
				this.callbackContext.InitializeFragment(true);
				this.callback(this.callbackContext, this.writer);
				this.callbackContext.UninitializeFragment();
			}
			if (this.writer.IsTagOpen)
			{
				this.writer.WriteTagEnd();
				this.writer.WriteNewLine(true);
			}
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0008F870 File Offset: 0x0008DA70
		protected override bool StartForm()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0008F878 File Offset: 0x0008DA78
		protected override void EndForm()
		{
			this.EndInlineContainer();
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0008F880 File Offset: 0x0008DA80
		protected override bool StartFieldSet()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0008F888 File Offset: 0x0008DA88
		protected override void EndFieldSet()
		{
			this.EndBlockContainer();
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0008F890 File Offset: 0x0008DA90
		protected override bool StartSelect()
		{
			return true;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0008F893 File Offset: 0x0008DA93
		protected override void EndSelect()
		{
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0008F895 File Offset: 0x0008DA95
		protected override bool StartOptionGroup()
		{
			return true;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0008F898 File Offset: 0x0008DA98
		protected override void EndOptionGroup()
		{
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0008F89A File Offset: 0x0008DA9A
		protected override bool StartOption()
		{
			return true;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0008F89D File Offset: 0x0008DA9D
		protected override void EndOption()
		{
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0008F89F File Offset: 0x0008DA9F
		protected override bool StartText()
		{
			this.ApplyCharFormat();
			this.writer.StartTextChunk();
			return true;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0008F8B4 File Offset: 0x0008DAB4
		protected override bool ContinueText(uint beginTextPosition, uint endTextPosition)
		{
			if (beginTextPosition != endTextPosition)
			{
				TextRun textRun = base.FormatStore.GetTextRun(beginTextPosition);
				for (;;)
				{
					int effectiveLength = textRun.EffectiveLength;
					TextRunType type = textRun.Type;
					if (type <= TextRunType.NbSp)
					{
						if (type != TextRunType.NonSpace)
						{
							if (type != TextRunType.NbSp)
							{
								goto IL_377;
							}
							this.writer.WriteNbsp(effectiveLength);
							goto IL_377;
						}
						else
						{
							int num = 0;
							if (this.recognizeHyperlinks && this.hyperlinkLevel == 0 && effectiveLength > 10 && effectiveLength < 4096)
							{
								int num2;
								int num3;
								bool flag2;
								bool flag3;
								bool flag = this.RecognizeHyperLink(textRun, out num2, out num3, out flag2, out flag3);
								if (flag)
								{
									if (num2 != 0)
									{
										this.writer.WriteTextInternal(this.scratchBuffer.Buffer, 0, num2);
									}
									if (this.callback != null)
									{
										if (this.callbackContext == null)
										{
											this.callbackContext = new HtmlFormatOutputCallbackContext(this);
										}
										this.callbackContext.InitializeTag(false, HtmlNameIndex.A, false);
										string text = new string(this.scratchBuffer.Buffer, num2, num3);
										if (flag3)
										{
											text = "http://" + text;
										}
										else if (flag2)
										{
											text = "file://" + text;
										}
										this.callbackContext.AddAttribute(HtmlNameIndex.Href, text);
										this.callbackContext.InitializeFragment(false);
										this.callback(this.callbackContext, this.writer);
										this.callbackContext.UninitializeFragment();
										if (this.writer.IsTagOpen)
										{
											this.writer.WriteTagEnd();
										}
										if (!this.callbackContext.IsDeleteInnerContent)
										{
											this.writer.WriteTextInternal(this.scratchBuffer.Buffer, num2, num3);
										}
										if (this.callbackContext.IsInvokeCallbackForEndTag)
										{
											this.callbackContext.InitializeTag(true, HtmlNameIndex.A, this.callbackContext.IsDeleteEndTag);
											this.callbackContext.InitializeFragment(false);
											this.callback(this.callbackContext, this.writer);
											this.callbackContext.UninitializeFragment();
										}
										else if (!this.callbackContext.IsDeleteEndTag)
										{
											this.writer.WriteEndTag(HtmlNameIndex.A);
										}
										if (this.writer.IsTagOpen)
										{
											this.writer.WriteTagEnd();
										}
									}
									else
									{
										this.writer.WriteStartTag(HtmlNameIndex.A);
										this.writer.WriteAttributeName(HtmlNameIndex.Href);
										if (flag3)
										{
											this.writer.WriteAttributeValue("http://");
										}
										else if (flag2)
										{
											this.writer.WriteAttributeValue("file://");
										}
										this.writer.WriteAttributeValue(this.scratchBuffer.Buffer, num2, num3);
										this.writer.WriteTagEnd();
										this.writer.WriteTextInternal(this.scratchBuffer.Buffer, num2, num3);
										this.writer.WriteEndTag(HtmlNameIndex.A);
									}
									num += num2 + num3;
									if (num == effectiveLength)
									{
										textRun.MoveNext();
										goto IL_37E;
									}
								}
							}
							for (;;)
							{
								char[] buffer;
								int index;
								int num4;
								textRun.GetChunk(num, out buffer, out index, out num4);
								this.writer.WriteTextInternal(buffer, index, num4);
								num += num4;
								if (num == effectiveLength)
								{
									goto IL_377;
								}
							}
						}
					}
					else
					{
						if (type == TextRunType.Space)
						{
							this.writer.WriteSpace(effectiveLength);
							goto IL_377;
						}
						if (type == TextRunType.Tabulation)
						{
							this.writer.WriteTabulation(effectiveLength);
							goto IL_377;
						}
						if (type != TextRunType.NewLine)
						{
							goto IL_377;
						}
						while (effectiveLength-- != 0)
						{
							if (this.writer.LiteralWhitespaceNesting == 0)
							{
								this.writer.WriteStartTag(HtmlNameIndex.BR);
							}
							this.writer.WriteNewLine(false);
						}
						goto IL_377;
					}
					IL_37E:
					if (textRun.Position >= endTextPosition)
					{
						break;
					}
					continue;
					IL_377:
					textRun.MoveNext();
					goto IL_37E;
				}
			}
			return true;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0008FC4D File Offset: 0x0008DE4D
		protected override void EndText()
		{
			this.writer.EndTextChunk();
			this.RevertCharFormat();
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0008FC60 File Offset: 0x0008DE60
		protected override bool StartBlockContainer()
		{
			this.writer.WriteNewLine(true);
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Preformatted);
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.QuotingLevelDelta);
			if (!distinctProperty.IsNull && distinctProperty.Bool)
			{
				FormatStyle style = base.FormatStore.GetStyle(14);
				base.SubtractDefaultContainerPropertiesFromDistinct(FlagProperties.AllOff, style.PropertyList);
				this.writer.WriteStartTag(HtmlNameIndex.Pre);
			}
			else if (!distinctProperty2.IsNull && distinctProperty2.Integer != 0)
			{
				for (int i = 0; i < distinctProperty2.Integer; i++)
				{
					this.writer.WriteStartTag(HtmlNameIndex.Div);
					this.writer.WriteAttribute(HtmlNameIndex.Class, "EmailQuote");
				}
			}
			else
			{
				if (base.SourceFormat == SourceFormat.Text)
				{
					this.ApplyCharFormat();
				}
				this.writer.WriteStartTag(HtmlNameIndex.Div);
				if (base.SourceFormat == SourceFormat.Text)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Class, "PlainText");
				}
			}
			this.OutputBlockTagAttributes();
			bool flag = false;
			this.OutputBlockCssProperties(ref flag);
			if (base.SourceFormat != SourceFormat.Text)
			{
				this.ApplyCharFormat();
			}
			if (base.CurrentNode.FirstChild.IsNull)
			{
				this.writer.WriteText('\u00a0');
			}
			else if (base.CurrentNode.FirstChild == base.CurrentNode.LastChild && base.CurrentNode.FirstChild.NodeType == FormatContainerType.Text)
			{
				FormatNode firstChild = base.CurrentNode.FirstChild;
				if (firstChild.BeginTextPosition + 1U == firstChild.EndTextPosition && base.FormatStore.GetTextRun(firstChild.BeginTextPosition).Type == TextRunType.Space)
				{
					this.writer.WriteText('\u00a0');
					this.EndBlockContainer();
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0008FE38 File Offset: 0x0008E038
		protected override void EndBlockContainer()
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Preformatted);
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.QuotingLevelDelta);
			if (base.SourceFormat != SourceFormat.Text)
			{
				this.RevertCharFormat();
			}
			if (!distinctProperty.IsNull && distinctProperty.Bool)
			{
				this.writer.WriteEndTag(HtmlNameIndex.Pre);
			}
			else if (!distinctProperty2.IsNull && distinctProperty2.Integer != 0)
			{
				for (int i = 0; i < distinctProperty2.Integer; i++)
				{
					this.writer.WriteEndTag(HtmlNameIndex.Div);
				}
			}
			else
			{
				this.writer.WriteEndTag(HtmlNameIndex.Div);
				if (base.SourceFormat == SourceFormat.Text)
				{
					this.RevertCharFormat();
				}
			}
			this.writer.WriteNewLine(true);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0008FEE3 File Offset: 0x0008E0E3
		protected override bool StartInlineContainer()
		{
			return true;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0008FEE6 File Offset: 0x0008E0E6
		protected override void EndInlineContainer()
		{
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0008FEE8 File Offset: 0x0008E0E8
		protected override void Dispose(bool disposing)
		{
			if (this.writer != null && this.writer != null)
			{
				((IDisposable)this.writer).Dispose();
			}
			this.writer = null;
			base.Dispose(disposing);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0008FF14 File Offset: 0x0008E114
		private void CloseHyperLink()
		{
			bool flag = false;
			bool flag2 = false;
			if (this.endTagActionStackTop != 0 && this.endTagActionStack[this.endTagActionStackTop - 1].TagLevel == this.hyperlinkLevel)
			{
				this.endTagActionStackTop--;
				flag = this.endTagActionStack[this.endTagActionStackTop].Drop;
				flag2 = this.endTagActionStack[this.endTagActionStackTop].Callback;
			}
			if (flag2)
			{
				this.callbackContext.InitializeTag(true, HtmlNameIndex.A, flag);
				this.callbackContext.InitializeFragment(false);
				this.callback(this.callbackContext, this.writer);
				this.callbackContext.UninitializeFragment();
				return;
			}
			if (!flag)
			{
				this.writer.WriteEndTag(HtmlNameIndex.A);
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0008FFDC File Offset: 0x0008E1DC
		private void ApplyCharFormat()
		{
			this.scratchBuffer.Reset();
			FlagProperties distinctFlags = base.GetDistinctFlags();
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.FontSize);
			if (!distinctProperty.IsNull && !distinctProperty.IsHtmlFontUnits && !distinctProperty.IsRelativeHtmlFontUnits)
			{
				this.scratchBuffer.Append("font-size:");
				HtmlSupport.AppendCssFontSize(ref this.scratchBuffer, distinctProperty);
				this.scratchBuffer.Append(';');
			}
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.BackColor);
			if (distinctProperty2.IsColor)
			{
				this.scratchBuffer.Append("background-color:");
				HtmlSupport.AppendColor(ref this.scratchBuffer, distinctProperty2);
				this.scratchBuffer.Append(';');
			}
			Culture culture = null;
			PropertyValue distinctProperty3 = base.GetDistinctProperty(PropertyId.Language);
			if (distinctProperty3.IsInteger && (!Culture.TryGetCulture(distinctProperty3.Integer, out culture) || string.IsNullOrEmpty(culture.Name)))
			{
				culture = null;
			}
			if ((byte)(base.CurrentNode.NodeType & FormatContainerType.BlockFlag) == 0)
			{
				PropertyValue distinctProperty4 = base.GetDistinctProperty(PropertyId.Display);
				PropertyValue distinctProperty5 = base.GetDistinctProperty(PropertyId.UnicodeBiDi);
				if (!distinctProperty4.IsNull)
				{
					string displayString = HtmlSupport.GetDisplayString(distinctProperty4);
					if (displayString != null)
					{
						this.scratchBuffer.Append("display:");
						this.scratchBuffer.Append(displayString);
						this.scratchBuffer.Append(";");
					}
				}
				if (distinctFlags.IsDefined(PropertyId.Visible))
				{
					this.scratchBuffer.Append(distinctFlags.IsOn(PropertyId.Visible) ? "visibility:visible;" : "visibility:hidden;");
				}
				if (!distinctProperty5.IsNull)
				{
					string unicodeBiDiString = HtmlSupport.GetUnicodeBiDiString(distinctProperty5);
					if (unicodeBiDiString != null)
					{
						this.scratchBuffer.Append("unicode-bidi:");
						this.scratchBuffer.Append(unicodeBiDiString);
						this.scratchBuffer.Append(";");
					}
				}
			}
			if (distinctFlags.IsDefinedAndOff(PropertyId.FirstFlag))
			{
				this.scratchBuffer.Append("font-weight:normal;");
			}
			if (distinctFlags.IsDefined(PropertyId.SmallCaps))
			{
				this.scratchBuffer.Append(distinctFlags.IsOn(PropertyId.SmallCaps) ? "font-variant:small-caps;" : "font-variant:normal;");
			}
			if (distinctFlags.IsDefined(PropertyId.Capitalize))
			{
				this.scratchBuffer.Append(distinctFlags.IsOn(PropertyId.Capitalize) ? "text-transform:uppercase;" : "text-transform:none;");
			}
			PropertyValue distinctProperty6 = base.GetDistinctProperty(PropertyId.FontFace);
			PropertyValue distinctProperty7 = base.GetDistinctProperty(PropertyId.FontColor);
			if (!distinctProperty6.IsNull || !distinctProperty.IsNull || !distinctProperty7.IsNull)
			{
				this.writer.WriteStartTag(HtmlNameIndex.Font);
				if (!distinctProperty6.IsNull)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Face);
					if (distinctProperty6.IsMultiValue)
					{
						MultiValue multiValue = base.FormatStore.GetMultiValue(distinctProperty6);
						for (int i = 0; i < multiValue.Length; i++)
						{
							string @string = multiValue.GetStringValue(i).GetString();
							if (i != 0)
							{
								this.writer.WriteAttributeValue(",");
							}
							this.writer.WriteAttributeValue(@string);
						}
					}
					else
					{
						string @string = base.FormatStore.GetStringValue(distinctProperty6).GetString();
						this.writer.WriteAttributeValue(@string);
					}
				}
				if (!distinctProperty.IsNull)
				{
					BufferString value = HtmlSupport.FormatFontSize(ref this.scratchValueBuffer, distinctProperty);
					if (value.Length != 0)
					{
						this.writer.WriteAttribute(HtmlNameIndex.Size, value);
					}
				}
				if (!distinctProperty7.IsNull)
				{
					BufferString value = HtmlSupport.FormatColor(ref this.scratchValueBuffer, distinctProperty7);
					if (value.Length != 0)
					{
						this.writer.WriteAttribute(HtmlNameIndex.Color, value);
					}
				}
			}
			if (this.scratchBuffer.Length != 0 || distinctFlags.IsDefined(PropertyId.RightToLeft) || culture != null)
			{
				this.writer.WriteStartTag(HtmlNameIndex.Span);
				if (this.scratchBuffer.Length != 0)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Style);
					this.writer.WriteAttributeValue(this.scratchBuffer.BufferString);
				}
				if (distinctFlags.IsDefined(PropertyId.RightToLeft))
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Dir);
					this.writer.WriteAttributeValue(distinctFlags.IsOn(PropertyId.RightToLeft) ? "rtl" : "ltr");
				}
				if (culture != null)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Lang);
					this.writer.WriteAttributeValue(culture.Name);
				}
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.FirstFlag))
			{
				this.writer.WriteStartTag(HtmlNameIndex.B);
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.Italic))
			{
				this.writer.WriteStartTag(HtmlNameIndex.I);
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.Underline))
			{
				this.writer.WriteStartTag(HtmlNameIndex.U);
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.Subscript))
			{
				this.writer.WriteStartTag(HtmlNameIndex.Sub);
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.Superscript))
			{
				this.writer.WriteStartTag(HtmlNameIndex.Sup);
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.Strikethrough))
			{
				this.writer.WriteStartTag(HtmlNameIndex.Strike);
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x000904AC File Offset: 0x0008E6AC
		private void RevertCharFormat()
		{
			FlagProperties distinctFlags = base.GetDistinctFlags();
			bool flag = false;
			bool flag2 = false;
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.FontSize);
			if (!distinctProperty.IsNull && !distinctProperty.IsHtmlFontUnits && !distinctProperty.IsRelativeHtmlFontUnits)
			{
				flag2 = true;
			}
			if (base.GetDistinctProperty(PropertyId.BackColor).IsColor)
			{
				flag2 = true;
			}
			Culture culture = null;
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.Language);
			if (distinctProperty2.IsInteger && Culture.TryGetCulture(distinctProperty2.Integer, out culture) && !string.IsNullOrEmpty(culture.Name))
			{
				flag2 = true;
			}
			if ((byte)(base.CurrentNode.NodeType & FormatContainerType.BlockFlag) == 0)
			{
				PropertyValue distinctProperty3 = base.GetDistinctProperty(PropertyId.Display);
				PropertyValue distinctProperty4 = base.GetDistinctProperty(PropertyId.UnicodeBiDi);
				if (!distinctProperty3.IsNull)
				{
					string displayString = HtmlSupport.GetDisplayString(distinctProperty3);
					if (displayString != null)
					{
						flag2 = true;
					}
				}
				if (distinctFlags.IsDefined(PropertyId.Visible))
				{
					flag2 = true;
				}
				if (!distinctProperty4.IsNull)
				{
					string unicodeBiDiString = HtmlSupport.GetUnicodeBiDiString(distinctProperty4);
					if (unicodeBiDiString != null)
					{
						flag2 = true;
					}
				}
			}
			if (distinctFlags.IsDefinedAndOff(PropertyId.FirstFlag))
			{
				flag2 = true;
			}
			if (distinctFlags.IsDefined(PropertyId.SmallCaps))
			{
				flag2 = true;
			}
			if (distinctFlags.IsDefined(PropertyId.Capitalize))
			{
				flag2 = true;
			}
			if (distinctFlags.IsDefined(PropertyId.RightToLeft))
			{
				flag2 = true;
			}
			PropertyValue distinctProperty5 = base.GetDistinctProperty(PropertyId.FontFace);
			PropertyValue distinctProperty6 = base.GetDistinctProperty(PropertyId.FontColor);
			if (!distinctProperty5.IsNull || !distinctProperty.IsNull || !distinctProperty6.IsNull)
			{
				flag = true;
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.Strikethrough))
			{
				this.writer.WriteEndTag(HtmlNameIndex.Strike);
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.Superscript))
			{
				this.writer.WriteEndTag(HtmlNameIndex.Sup);
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.Subscript))
			{
				this.writer.WriteEndTag(HtmlNameIndex.Sub);
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.Underline))
			{
				this.writer.WriteEndTag(HtmlNameIndex.U);
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.Italic))
			{
				this.writer.WriteEndTag(HtmlNameIndex.I);
			}
			if (distinctFlags.IsDefinedAndOn(PropertyId.FirstFlag))
			{
				this.writer.WriteEndTag(HtmlNameIndex.B);
			}
			if (flag2)
			{
				this.writer.WriteEndTag(HtmlNameIndex.Span);
			}
			if (flag)
			{
				this.writer.WriteEndTag(HtmlNameIndex.Font);
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x000906BC File Offset: 0x0008E8BC
		private void OutputBlockCssProperties(ref bool styleAttributeOpen)
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Display);
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.Visible);
			PropertyValue distinctProperty3 = base.GetDistinctProperty(PropertyId.Height);
			PropertyValue distinctProperty4 = base.GetDistinctProperty(PropertyId.Width);
			PropertyValue distinctProperty5 = base.GetDistinctProperty(PropertyId.UnicodeBiDi);
			PropertyValue distinctProperty6 = base.GetDistinctProperty(PropertyId.FirstLineIndent);
			PropertyValue distinctProperty7 = base.GetDistinctProperty(PropertyId.TextAlignment);
			PropertyValue distinctProperty8 = base.GetDistinctProperty(PropertyId.BackColor);
			PropertyValue distinctProperty9 = base.GetDistinctProperty(PropertyId.Margins);
			PropertyValue distinctProperty10 = base.GetDistinctProperty(PropertyId.RightMargin);
			PropertyValue distinctProperty11 = base.GetDistinctProperty(PropertyId.BottomMargin);
			PropertyValue distinctProperty12 = base.GetDistinctProperty(PropertyId.LeftMargin);
			PropertyValue distinctProperty13 = base.GetDistinctProperty(PropertyId.Paddings);
			PropertyValue distinctProperty14 = base.GetDistinctProperty(PropertyId.RightPadding);
			PropertyValue distinctProperty15 = base.GetDistinctProperty(PropertyId.BottomPadding);
			PropertyValue distinctProperty16 = base.GetDistinctProperty(PropertyId.LeftPadding);
			PropertyValue distinctProperty17 = base.GetDistinctProperty(PropertyId.BorderWidths);
			PropertyValue distinctProperty18 = base.GetDistinctProperty(PropertyId.RightBorderWidth);
			PropertyValue distinctProperty19 = base.GetDistinctProperty(PropertyId.BottomBorderWidth);
			PropertyValue distinctProperty20 = base.GetDistinctProperty(PropertyId.LeftBorderWidth);
			PropertyValue distinctProperty21 = base.GetDistinctProperty(PropertyId.BorderStyles);
			PropertyValue distinctProperty22 = base.GetDistinctProperty(PropertyId.RightBorderStyle);
			PropertyValue distinctProperty23 = base.GetDistinctProperty(PropertyId.BottomBorderStyle);
			PropertyValue distinctProperty24 = base.GetDistinctProperty(PropertyId.LeftBorderStyle);
			PropertyValue distinctProperty25 = base.GetDistinctProperty(PropertyId.BorderColors);
			PropertyValue distinctProperty26 = base.GetDistinctProperty(PropertyId.RightBorderColor);
			PropertyValue distinctProperty27 = base.GetDistinctProperty(PropertyId.BottomBorderColor);
			PropertyValue distinctProperty28 = base.GetDistinctProperty(PropertyId.LeftBorderColor);
			if (!distinctProperty2.IsNull || !distinctProperty.IsNull || !distinctProperty5.IsNull || !distinctProperty4.IsNull || !distinctProperty3.IsNull)
			{
				if (!styleAttributeOpen)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Style);
					styleAttributeOpen = true;
				}
				if (!distinctProperty.IsNull)
				{
					string displayString = HtmlSupport.GetDisplayString(distinctProperty);
					if (displayString != null)
					{
						this.scratchBuffer.Append("display:");
						this.scratchBuffer.Append(displayString);
						this.scratchBuffer.Append(";");
					}
				}
				if (!distinctProperty2.IsNull)
				{
					this.scratchBuffer.Append(distinctProperty2.Bool ? "visibility:visible;" : "visibility:hidden;");
				}
				if (!distinctProperty4.IsNull)
				{
					BufferString value = HtmlSupport.FormatLength(ref this.scratchBuffer, distinctProperty4);
					if (value.Length != 0)
					{
						this.writer.WriteAttributeValue("width:");
						this.writer.WriteAttributeValue(value);
						this.writer.WriteAttributeValue(";");
					}
				}
				if (!distinctProperty3.IsNull)
				{
					BufferString value2 = HtmlSupport.FormatLength(ref this.scratchBuffer, distinctProperty3);
					if (value2.Length != 0)
					{
						this.writer.WriteAttributeValue("height:");
						this.writer.WriteAttributeValue(value2);
						this.writer.WriteAttributeValue(";");
					}
				}
				if (!distinctProperty5.IsNull)
				{
					string unicodeBiDiString = HtmlSupport.GetUnicodeBiDiString(distinctProperty5);
					if (unicodeBiDiString != null)
					{
						this.writer.WriteAttributeValue("unicode-bidi:");
						this.writer.WriteAttributeValue(unicodeBiDiString);
						this.writer.WriteAttributeValue(";");
					}
				}
			}
			if (!distinctProperty6.IsNull || !distinctProperty7.IsNull || !distinctProperty8.IsNull)
			{
				if (!styleAttributeOpen)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Style);
					styleAttributeOpen = true;
				}
				if (!distinctProperty6.IsNull)
				{
					BufferString value3 = HtmlSupport.FormatLength(ref this.scratchBuffer, distinctProperty6);
					if (value3.Length != 0)
					{
						this.writer.WriteAttributeValue("text-indent:");
						this.writer.WriteAttributeValue(value3);
						this.writer.WriteAttributeValue(";");
					}
				}
				if (!distinctProperty7.IsNull && distinctProperty7.IsEnum && distinctProperty7.Enum < HtmlSupport.TextAlignmentEnumeration.Length)
				{
					this.writer.WriteAttributeValue("text-align:");
					this.writer.WriteAttributeValue(HtmlSupport.TextAlignmentEnumeration[distinctProperty7.Enum].Name);
					this.writer.WriteAttributeValue(";");
				}
				if (!distinctProperty8.IsNull)
				{
					BufferString value4 = HtmlSupport.FormatColor(ref this.scratchBuffer, distinctProperty8);
					if (value4.Length != 0)
					{
						this.writer.WriteAttributeValue("background-color:");
						this.writer.WriteAttributeValue(value4);
						this.writer.WriteAttributeValue(";");
					}
				}
			}
			if (!distinctProperty9.IsNull || !distinctProperty10.IsNull || !distinctProperty11.IsNull || !distinctProperty12.IsNull)
			{
				if (!styleAttributeOpen)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Style);
					styleAttributeOpen = true;
				}
				this.OutputMarginAndPaddingProperties("margin", distinctProperty9, distinctProperty10, distinctProperty11, distinctProperty12);
			}
			if (!distinctProperty13.IsNull || !distinctProperty14.IsNull || !distinctProperty15.IsNull || !distinctProperty16.IsNull)
			{
				if (!styleAttributeOpen)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Style);
					styleAttributeOpen = true;
				}
				this.OutputMarginAndPaddingProperties("padding", distinctProperty13, distinctProperty14, distinctProperty15, distinctProperty16);
			}
			if (!distinctProperty17.IsNull || !distinctProperty18.IsNull || !distinctProperty19.IsNull || !distinctProperty20.IsNull || !distinctProperty21.IsNull || !distinctProperty22.IsNull || !distinctProperty23.IsNull || !distinctProperty24.IsNull || !distinctProperty25.IsNull || !distinctProperty26.IsNull || !distinctProperty27.IsNull || !distinctProperty28.IsNull)
			{
				if (!styleAttributeOpen)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Style);
					styleAttributeOpen = true;
				}
				this.OutputBorderProperties(distinctProperty17, distinctProperty18, distinctProperty19, distinctProperty20, distinctProperty21, distinctProperty22, distinctProperty23, distinctProperty24, distinctProperty25, distinctProperty26, distinctProperty27, distinctProperty28);
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00090BD4 File Offset: 0x0008EDD4
		private void OutputMarginAndPaddingProperties(string name, PropertyValue topValue, PropertyValue rightValue, PropertyValue bottomValue, PropertyValue leftValue)
		{
			int num = 0;
			if (!topValue.IsNull)
			{
				num++;
			}
			if (!rightValue.IsNull)
			{
				num++;
			}
			if (!bottomValue.IsNull)
			{
				num++;
			}
			if (!leftValue.IsNull)
			{
				num++;
			}
			if (num == 4)
			{
				this.writer.WriteAttributeValue(name);
				this.writer.WriteAttributeValue(":");
				if (topValue == rightValue && topValue == bottomValue && topValue == leftValue)
				{
					this.OutputLengthPropertyValue(topValue);
				}
				else if (topValue == bottomValue && rightValue == leftValue)
				{
					this.OutputCompositeLengthPropertyValue(topValue, rightValue);
				}
				else
				{
					this.OutputCompositeLengthPropertyValue(topValue, rightValue, bottomValue, leftValue);
				}
				this.writer.WriteAttributeValue(";");
				return;
			}
			if (!topValue.IsNull)
			{
				this.writer.WriteAttributeValue(name);
				this.writer.WriteAttributeValue("-top:");
				this.OutputLengthPropertyValue(topValue);
				this.writer.WriteAttributeValue(";");
			}
			if (!rightValue.IsNull)
			{
				this.writer.WriteAttributeValue(name);
				this.writer.WriteAttributeValue("-right:");
				this.OutputLengthPropertyValue(rightValue);
				this.writer.WriteAttributeValue(";");
			}
			if (!bottomValue.IsNull)
			{
				this.writer.WriteAttributeValue(name);
				this.writer.WriteAttributeValue("-bottom:");
				this.OutputLengthPropertyValue(bottomValue);
				this.writer.WriteAttributeValue(";");
			}
			if (!leftValue.IsNull)
			{
				this.writer.WriteAttributeValue(name);
				this.writer.WriteAttributeValue("-left:");
				this.OutputLengthPropertyValue(leftValue);
				this.writer.WriteAttributeValue(";");
			}
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00090D8C File Offset: 0x0008EF8C
		private void OutputBorderProperties(PropertyValue topBorderWidth, PropertyValue rightBorderWidth, PropertyValue bottomBorderWidth, PropertyValue leftBorderWidth, PropertyValue topBorderStyle, PropertyValue rightBorderStyle, PropertyValue bottomBorderStyle, PropertyValue leftBorderStyle, PropertyValue topBorderColor, PropertyValue rightBorderColor, PropertyValue bottomBorderColor, PropertyValue leftBorderColor)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			if (!topBorderWidth.IsNull)
			{
				num++;
				num4++;
			}
			if (!rightBorderWidth.IsNull)
			{
				num++;
				num5++;
			}
			if (!bottomBorderWidth.IsNull)
			{
				num++;
				num6++;
			}
			if (!leftBorderWidth.IsNull)
			{
				num++;
				num7++;
			}
			if (!topBorderStyle.IsNull)
			{
				num2++;
				num4++;
			}
			if (!rightBorderStyle.IsNull)
			{
				num2++;
				num5++;
			}
			if (!bottomBorderStyle.IsNull)
			{
				num2++;
				num6++;
			}
			if (!leftBorderStyle.IsNull)
			{
				num2++;
				num7++;
			}
			if (!topBorderColor.IsNull)
			{
				num3++;
				num4++;
			}
			if (!rightBorderColor.IsNull)
			{
				num3++;
				num5++;
			}
			if (!bottomBorderColor.IsNull)
			{
				num3++;
				num6++;
			}
			if (!leftBorderColor.IsNull)
			{
				num3++;
				num7++;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			if (num == 4 && topBorderWidth == bottomBorderWidth && rightBorderWidth == leftBorderWidth)
			{
				flag2 = true;
				flag = (topBorderWidth == rightBorderWidth);
			}
			if (num2 == 4 && topBorderStyle == bottomBorderStyle && rightBorderStyle == leftBorderStyle)
			{
				flag4 = true;
				flag3 = (topBorderStyle == rightBorderStyle);
			}
			if (num3 == 4 && topBorderColor == bottomBorderColor && rightBorderColor == leftBorderColor)
			{
				flag6 = true;
				flag5 = (topBorderColor == rightBorderColor);
			}
			if (num != 4 || num2 != 4 || num3 != 4)
			{
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				bool flag11 = false;
				bool flag12 = false;
				bool flag13 = false;
				bool flag14 = false;
				bool flag15 = false;
				bool flag16 = false;
				bool flag17 = false;
				bool flag18 = false;
				if (num == 4 || num2 == 4 || num3 == 4)
				{
					if (num == 4)
					{
						this.writer.WriteAttributeValue("border-width:");
						if (flag)
						{
							this.OutputBorderWidthPropertyValue(topBorderWidth);
						}
						else if (flag2)
						{
							this.OutputCompositeBorderWidthPropertyValue(topBorderWidth, rightBorderWidth);
						}
						else
						{
							this.OutputCompositeBorderWidthPropertyValue(topBorderWidth, rightBorderWidth, bottomBorderWidth, leftBorderWidth);
						}
						this.writer.WriteAttributeValue(";");
						flag7 = true;
						flag8 = true;
						flag9 = true;
						flag10 = true;
					}
					if (num2 == 4)
					{
						this.writer.WriteAttributeValue("border-style:");
						if (flag3)
						{
							this.OutputBorderStylePropertyValue(topBorderStyle);
						}
						else if (flag4)
						{
							this.OutputCompositeBorderStylePropertyValue(topBorderStyle, rightBorderStyle);
						}
						else
						{
							this.OutputCompositeBorderStylePropertyValue(topBorderStyle, rightBorderStyle, bottomBorderStyle, leftBorderStyle);
						}
						this.writer.WriteAttributeValue(";");
						flag11 = true;
						flag12 = true;
						flag13 = true;
						flag14 = true;
					}
					if (num3 == 4)
					{
						this.writer.WriteAttributeValue("border-color:");
						if (flag5)
						{
							this.OutputBorderColorPropertyValue(topBorderColor);
						}
						else if (flag6)
						{
							this.OutputCompositeBorderColorPropertyValue(topBorderColor, rightBorderColor);
						}
						else
						{
							this.OutputCompositeBorderColorPropertyValue(topBorderColor, rightBorderColor, bottomBorderColor, leftBorderColor);
						}
						this.writer.WriteAttributeValue(";");
						flag15 = true;
						flag16 = true;
						flag17 = true;
						flag18 = true;
					}
				}
				else if (num4 == 3 || num5 == 3 || num6 == 3 || num7 == 3)
				{
					if (num4 == 3)
					{
						this.writer.WriteAttributeValue("border-top:");
						this.OutputCompositeBorderSidePropertyValue(topBorderWidth, topBorderStyle, topBorderColor);
						this.writer.WriteAttributeValue(";");
						flag7 = true;
						flag11 = true;
						flag15 = true;
					}
					if (num5 == 3)
					{
						this.writer.WriteAttributeValue("border-right:");
						this.OutputCompositeBorderSidePropertyValue(rightBorderWidth, rightBorderStyle, rightBorderColor);
						this.writer.WriteAttributeValue(";");
						flag8 = true;
						flag12 = true;
						flag16 = true;
					}
					if (num6 == 3)
					{
						this.writer.WriteAttributeValue("border-bottom:");
						this.OutputCompositeBorderSidePropertyValue(bottomBorderWidth, bottomBorderStyle, bottomBorderColor);
						this.writer.WriteAttributeValue(";");
						flag9 = true;
						flag13 = true;
						flag17 = true;
					}
					if (num7 == 3)
					{
						this.writer.WriteAttributeValue("border-left:");
						this.OutputCompositeBorderSidePropertyValue(leftBorderWidth, leftBorderStyle, leftBorderColor);
						this.writer.WriteAttributeValue(";");
						flag10 = true;
						flag14 = true;
						flag18 = true;
					}
				}
				if (!flag7 && !topBorderWidth.IsNull)
				{
					this.writer.WriteAttributeValue("border-top-width:");
					this.OutputBorderWidthPropertyValue(topBorderWidth);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag8 && !rightBorderWidth.IsNull)
				{
					this.writer.WriteAttributeValue("border-right-width:");
					this.OutputBorderWidthPropertyValue(rightBorderWidth);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag9 && !bottomBorderWidth.IsNull)
				{
					this.writer.WriteAttributeValue("border-bottom-width:");
					this.OutputBorderWidthPropertyValue(bottomBorderWidth);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag10 && !leftBorderWidth.IsNull)
				{
					this.writer.WriteAttributeValue("border-left-width:");
					this.OutputBorderWidthPropertyValue(leftBorderWidth);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag11 && !topBorderStyle.IsNull)
				{
					this.writer.WriteAttributeValue("border-top-style:");
					this.OutputBorderStylePropertyValue(topBorderStyle);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag12 && !rightBorderStyle.IsNull)
				{
					this.writer.WriteAttributeValue("border-right-style:");
					this.OutputBorderStylePropertyValue(rightBorderStyle);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag13 && !bottomBorderStyle.IsNull)
				{
					this.writer.WriteAttributeValue("border-bottom-style:");
					this.OutputBorderStylePropertyValue(bottomBorderStyle);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag14 && !leftBorderStyle.IsNull)
				{
					this.writer.WriteAttributeValue("border-left-style:");
					this.OutputBorderStylePropertyValue(leftBorderStyle);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag15 && !topBorderColor.IsNull)
				{
					this.writer.WriteAttributeValue("border-top-color:");
					this.OutputBorderColorPropertyValue(topBorderColor);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag16 && !rightBorderColor.IsNull)
				{
					this.writer.WriteAttributeValue("border-right-color:");
					this.OutputBorderColorPropertyValue(rightBorderColor);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag17 && !bottomBorderColor.IsNull)
				{
					this.writer.WriteAttributeValue("border-bottom-color:");
					this.OutputBorderColorPropertyValue(bottomBorderColor);
					this.writer.WriteAttributeValue(";");
				}
				if (!flag18 && !leftBorderColor.IsNull)
				{
					this.writer.WriteAttributeValue("border-left-color:");
					this.OutputBorderColorPropertyValue(leftBorderColor);
					this.writer.WriteAttributeValue(";");
				}
				return;
			}
			if (flag && flag3 && flag5)
			{
				this.writer.WriteAttributeValue("border:");
				this.OutputCompositeBorderSidePropertyValue(topBorderWidth, topBorderStyle, topBorderColor);
				this.writer.WriteAttributeValue(";");
				return;
			}
			this.writer.WriteAttributeValue("border-width:");
			if (flag)
			{
				this.OutputBorderWidthPropertyValue(topBorderWidth);
			}
			else if (flag2)
			{
				this.OutputCompositeBorderWidthPropertyValue(topBorderWidth, rightBorderWidth);
			}
			else
			{
				this.OutputCompositeBorderWidthPropertyValue(topBorderWidth, rightBorderWidth, bottomBorderWidth, leftBorderWidth);
			}
			this.writer.WriteAttributeValue(";");
			this.writer.WriteAttributeValue("border-style:");
			if (flag3)
			{
				this.OutputBorderStylePropertyValue(topBorderStyle);
			}
			else if (flag4)
			{
				this.OutputCompositeBorderStylePropertyValue(topBorderStyle, rightBorderStyle);
			}
			else
			{
				this.OutputCompositeBorderStylePropertyValue(topBorderStyle, rightBorderStyle, bottomBorderStyle, leftBorderStyle);
			}
			this.writer.WriteAttributeValue(";");
			this.writer.WriteAttributeValue("border-color:");
			if (flag5)
			{
				this.OutputBorderColorPropertyValue(topBorderColor);
			}
			else if (flag6)
			{
				this.OutputCompositeBorderColorPropertyValue(topBorderColor, rightBorderColor);
			}
			else
			{
				this.OutputCompositeBorderColorPropertyValue(topBorderColor, rightBorderColor, bottomBorderColor, leftBorderColor);
			}
			this.writer.WriteAttributeValue(";");
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x000914F5 File Offset: 0x0008F6F5
		private void OutputCompositeBorderSidePropertyValue(PropertyValue width, PropertyValue style, PropertyValue color)
		{
			this.OutputBorderWidthPropertyValue(width);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderStylePropertyValue(style);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderColorPropertyValue(color);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0009152C File Offset: 0x0008F72C
		private void OutputCompositeLengthPropertyValue(PropertyValue topBottom, PropertyValue rightLeft)
		{
			this.OutputLengthPropertyValue(topBottom);
			this.writer.WriteAttributeValue(" ");
			this.OutputLengthPropertyValue(rightLeft);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0009154C File Offset: 0x0008F74C
		private void OutputCompositeLengthPropertyValue(PropertyValue top, PropertyValue right, PropertyValue bottom, PropertyValue left)
		{
			this.OutputLengthPropertyValue(top);
			this.writer.WriteAttributeValue(" ");
			this.OutputLengthPropertyValue(right);
			this.writer.WriteAttributeValue(" ");
			this.OutputLengthPropertyValue(bottom);
			this.writer.WriteAttributeValue(" ");
			this.OutputLengthPropertyValue(left);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x000915A6 File Offset: 0x0008F7A6
		private void OutputCompositeBorderWidthPropertyValue(PropertyValue topBottom, PropertyValue rightLeft)
		{
			this.OutputBorderWidthPropertyValue(topBottom);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderWidthPropertyValue(rightLeft);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x000915C8 File Offset: 0x0008F7C8
		private void OutputCompositeBorderWidthPropertyValue(PropertyValue top, PropertyValue right, PropertyValue bottom, PropertyValue left)
		{
			this.OutputBorderWidthPropertyValue(top);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderWidthPropertyValue(right);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderWidthPropertyValue(bottom);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderWidthPropertyValue(left);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00091622 File Offset: 0x0008F822
		private void OutputCompositeBorderStylePropertyValue(PropertyValue topBottom, PropertyValue rightLeft)
		{
			this.OutputBorderStylePropertyValue(topBottom);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderStylePropertyValue(rightLeft);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x00091644 File Offset: 0x0008F844
		private void OutputCompositeBorderStylePropertyValue(PropertyValue top, PropertyValue right, PropertyValue bottom, PropertyValue left)
		{
			this.OutputBorderStylePropertyValue(top);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderStylePropertyValue(right);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderStylePropertyValue(bottom);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderStylePropertyValue(left);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0009169E File Offset: 0x0008F89E
		private void OutputCompositeBorderColorPropertyValue(PropertyValue topBottom, PropertyValue rightLeft)
		{
			this.OutputBorderColorPropertyValue(topBottom);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderColorPropertyValue(rightLeft);
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000916C0 File Offset: 0x0008F8C0
		private void OutputCompositeBorderColorPropertyValue(PropertyValue top, PropertyValue right, PropertyValue bottom, PropertyValue left)
		{
			this.OutputBorderColorPropertyValue(top);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderColorPropertyValue(right);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderColorPropertyValue(bottom);
			this.writer.WriteAttributeValue(" ");
			this.OutputBorderColorPropertyValue(left);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0009171C File Offset: 0x0008F91C
		private void OutputLengthPropertyValue(PropertyValue width)
		{
			BufferString value = HtmlSupport.FormatLength(ref this.scratchBuffer, width);
			if (value.Length != 0)
			{
				this.writer.WriteAttributeValue(value);
				return;
			}
			this.writer.WriteAttributeValue("0");
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0009175C File Offset: 0x0008F95C
		private void OutputBorderWidthPropertyValue(PropertyValue width)
		{
			BufferString value = HtmlSupport.FormatLength(ref this.scratchBuffer, width);
			if (value.Length != 0)
			{
				this.writer.WriteAttributeValue(value);
				return;
			}
			this.writer.WriteAttributeValue("medium");
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0009179C File Offset: 0x0008F99C
		private void OutputBorderStylePropertyValue(PropertyValue style)
		{
			string borderStyleString = HtmlSupport.GetBorderStyleString(style);
			if (borderStyleString != null)
			{
				this.writer.WriteAttributeValue(borderStyleString);
				return;
			}
			this.writer.WriteAttributeValue("solid");
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000917D0 File Offset: 0x0008F9D0
		private void OutputBorderColorPropertyValue(PropertyValue color)
		{
			BufferString value = HtmlSupport.FormatColor(ref this.scratchBuffer, color);
			if (value.Length != 0)
			{
				this.writer.WriteAttributeValue(value);
				return;
			}
			this.writer.WriteAttributeValue("black");
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00091810 File Offset: 0x0008FA10
		private void OutputTableCssProperties(ref bool styleAttributeOpen)
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Overloaded1);
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.Overloaded2);
			PropertyValue distinctProperty3 = base.GetDistinctProperty(PropertyId.TableShowEmptyCells);
			PropertyValue distinctProperty4 = base.GetDistinctProperty(PropertyId.TableCaptionSideTop);
			PropertyValue distinctProperty5 = base.GetDistinctProperty(PropertyId.TableBorderSpacingVertical);
			PropertyValue distinctProperty6 = base.GetDistinctProperty(PropertyId.TableBorderSpacingHorizontal);
			if (!distinctProperty.IsNull || !distinctProperty2.IsNull || !distinctProperty3.IsNull || !distinctProperty4.IsNull || !distinctProperty5.IsNull || !distinctProperty6.IsNull)
			{
				if (!styleAttributeOpen)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Style);
					styleAttributeOpen = true;
				}
				if (!distinctProperty.IsNull)
				{
					this.writer.WriteAttributeValue("table-layout:");
					this.writer.WriteAttributeValue(distinctProperty.Bool ? "fixed" : "auto");
					this.writer.WriteAttributeValue(";");
				}
				if (!distinctProperty2.IsNull)
				{
					this.writer.WriteAttributeValue("border-collapse:");
					this.writer.WriteAttributeValue(distinctProperty2.Bool ? "collapse" : "separate");
					this.writer.WriteAttributeValue(";");
				}
				if (!distinctProperty3.IsNull)
				{
					this.writer.WriteAttributeValue("empty-cells:");
					this.writer.WriteAttributeValue(distinctProperty3.Bool ? "show" : "hide");
					this.writer.WriteAttributeValue(";");
				}
				if (!distinctProperty4.IsNull)
				{
					this.writer.WriteAttributeValue("caption-side:");
					this.writer.WriteAttributeValue(distinctProperty4.Bool ? "top" : "bottom");
					this.writer.WriteAttributeValue(";");
				}
				if (!distinctProperty5.IsNull && !distinctProperty5.IsNull)
				{
					BufferString value = HtmlSupport.FormatLength(ref this.scratchBuffer, distinctProperty5);
					if (value.Length != 0)
					{
						this.writer.WriteAttributeValue("border-spacing:");
						this.writer.WriteAttributeValue(value);
						if (distinctProperty5 != distinctProperty6)
						{
							value = HtmlSupport.FormatLength(ref this.scratchBuffer, distinctProperty6);
							if (value.Length != 0)
							{
								this.writer.WriteAttributeValue(" ");
								this.writer.WriteAttributeValue(value);
							}
						}
						this.writer.WriteAttributeValue(";");
					}
				}
			}
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00091A64 File Offset: 0x0008FC64
		private void OutputTableColumnCssProperties(ref bool styleAttributeOpen)
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Width);
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.BackColor);
			if (!distinctProperty2.IsNull || !distinctProperty.IsNull)
			{
				if (!styleAttributeOpen)
				{
					this.writer.WriteAttributeName(HtmlNameIndex.Style);
					styleAttributeOpen = true;
				}
				if (!distinctProperty.IsNull)
				{
					BufferString value = HtmlSupport.FormatLength(ref this.scratchBuffer, distinctProperty);
					if (value.Length != 0)
					{
						this.writer.WriteAttributeValue("width:");
						this.writer.WriteAttributeValue(value);
						this.writer.WriteAttributeValue(";");
					}
				}
				if (!distinctProperty2.IsNull)
				{
					BufferString value2 = HtmlSupport.FormatColor(ref this.scratchBuffer, distinctProperty2);
					if (value2.Length != 0)
					{
						this.writer.WriteAttributeValue("background-color:");
						this.writer.WriteAttributeValue(value2);
					}
				}
			}
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00091B34 File Offset: 0x0008FD34
		private void OutputBlockTagAttributes()
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.RightToLeft);
			if (!distinctProperty.IsNull)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Dir, distinctProperty.Bool ? "rtl" : "ltr");
			}
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.TextAlignment);
			if (!distinctProperty2.IsNull)
			{
				string textAlignmentString = HtmlSupport.GetTextAlignmentString(distinctProperty2);
				if (textAlignmentString != null)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Align, textAlignmentString);
				}
			}
			this.WriteIdAttribute(false);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00091BAC File Offset: 0x0008FDAC
		private void OutputTableTagAttributes()
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.Width);
			if (!distinctProperty.IsNull)
			{
				BufferString value = HtmlSupport.FormatPixelOrPercentageLength(ref this.scratchBuffer, distinctProperty);
				if (value.Length != 0)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Width, value);
				}
			}
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.BlockAlignment);
			if (!distinctProperty2.IsNull)
			{
				string horizontalAlignmentString = HtmlSupport.GetHorizontalAlignmentString(distinctProperty2);
				if (horizontalAlignmentString != null)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Align, horizontalAlignmentString);
				}
			}
			PropertyValue distinctProperty3 = base.GetDistinctProperty(PropertyId.RightToLeft);
			if (!distinctProperty3.IsNull)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Dir, distinctProperty3.Bool ? "rtl" : "ltr");
			}
			PropertyValue distinctProperty4 = base.GetDistinctProperty(PropertyId.TableBorder);
			if (!distinctProperty4.IsNull)
			{
				BufferString value2 = HtmlSupport.FormatPixelOrPercentageLength(ref this.scratchBuffer, distinctProperty4);
				if (value2.Length != 0)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Border, value2);
				}
			}
			PropertyValue distinctProperty5 = base.GetDistinctProperty(PropertyId.TableFrame);
			if (!distinctProperty5.IsNull)
			{
				string tableFrameString = HtmlSupport.GetTableFrameString(distinctProperty5);
				if (tableFrameString != null)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Frame, tableFrameString);
				}
			}
			PropertyValue distinctProperty6 = base.GetDistinctProperty(PropertyId.TableRules);
			if (!distinctProperty6.IsNull)
			{
				string tableRulesString = HtmlSupport.GetTableRulesString(distinctProperty6);
				if (tableRulesString != null)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Rules, tableRulesString);
				}
			}
			PropertyValue distinctProperty7 = base.GetDistinctProperty(PropertyId.TableCellSpacing);
			if (!distinctProperty7.IsNull)
			{
				BufferString value3 = HtmlSupport.FormatPixelOrPercentageLength(ref this.scratchBuffer, distinctProperty7);
				if (value3.Length != 0)
				{
					this.writer.WriteAttribute(HtmlNameIndex.CellSpacing, value3);
				}
			}
			PropertyValue distinctProperty8 = base.GetDistinctProperty(PropertyId.TableCellPadding);
			if (!distinctProperty8.IsNull)
			{
				BufferString value4 = HtmlSupport.FormatPixelOrPercentageLength(ref this.scratchBuffer, distinctProperty8);
				if (value4.Length != 0)
				{
					this.writer.WriteAttribute(HtmlNameIndex.CellPadding, value4);
				}
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00091D64 File Offset: 0x0008FF64
		private void OutputTableCellTagAttributes()
		{
			PropertyValue distinctProperty = base.GetDistinctProperty(PropertyId.NumColumns);
			if (distinctProperty.IsInteger && distinctProperty.Integer != 1)
			{
				this.writer.WriteAttribute(HtmlNameIndex.ColSpan, distinctProperty.Integer.ToString());
			}
			PropertyValue distinctProperty2 = base.GetDistinctProperty(PropertyId.NumRows);
			if (distinctProperty2.IsInteger && distinctProperty2.Integer != 1)
			{
				this.writer.WriteAttribute(HtmlNameIndex.RowSpan, distinctProperty2.Integer.ToString());
			}
			PropertyValue distinctProperty3 = base.GetDistinctProperty(PropertyId.Width);
			if (!distinctProperty3.IsNull && distinctProperty3.IsAbsRelLength)
			{
				this.writer.WriteAttribute(HtmlNameIndex.Width, distinctProperty3.PixelsInteger.ToString());
			}
			PropertyValue distinctProperty4 = base.GetDistinctProperty(PropertyId.TextAlignment);
			if (!distinctProperty4.IsNull)
			{
				string textAlignmentString = HtmlSupport.GetTextAlignmentString(distinctProperty4);
				if (textAlignmentString != null)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Align, textAlignmentString);
				}
			}
			PropertyValue distinctProperty5 = base.GetDistinctProperty(PropertyId.BlockAlignment);
			if (!distinctProperty5.IsNull)
			{
				string verticalAlignmentString = HtmlSupport.GetVerticalAlignmentString(distinctProperty5);
				if (verticalAlignmentString != null)
				{
					this.writer.WriteAttribute(HtmlNameIndex.Valign, verticalAlignmentString);
				}
			}
			PropertyValue distinctProperty6 = base.GetDistinctProperty(PropertyId.TableCellNoWrap);
			if (!distinctProperty6.IsNull && distinctProperty6.Bool)
			{
				this.writer.WriteAttribute(HtmlNameIndex.NoWrap, string.Empty);
			}
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00091EB0 File Offset: 0x000900B0
		private bool RecognizeHyperLink(TextRun run, out int offset, out int length, out bool addFilePrefix, out bool addHttpPrefix)
		{
			this.scratchBuffer.Reset();
			int i = run.AppendFragment(0, ref this.scratchBuffer, 30);
			offset = 0;
			length = 0;
			bool flag = false;
			while (offset < Math.Min(i - 10, 20))
			{
				if (HtmlFormatOutput.IsHyperLinkStartDelimiter(this.scratchBuffer[offset]))
				{
					flag = true;
					break;
				}
				offset++;
			}
			if (!flag)
			{
				offset = 0;
			}
			bool flag2 = false;
			while (offset < i - 4 && HtmlFormatOutput.IsHyperLinkStartDelimiter(this.scratchBuffer[offset]))
			{
				flag2 = true;
				offset++;
			}
			bool flag3 = false;
			addHttpPrefix = false;
			addFilePrefix = false;
			if (this.scratchBuffer[offset] == '\\')
			{
				if (this.scratchBuffer[offset + 1] == '\\' && char.IsLetterOrDigit(this.scratchBuffer[offset + 2]))
				{
					flag3 = true;
					addFilePrefix = true;
				}
			}
			else if (i - offset > 4 && this.scratchBuffer[offset] == 'h')
			{
				if (this.scratchBuffer[offset + 1] == 't' && this.scratchBuffer[offset + 2] == 't' && this.scratchBuffer[offset + 3] == 'p' && (this.scratchBuffer[offset + 4] == ':' || (i - offset > 5 && this.scratchBuffer[offset + 4] == 's' && this.scratchBuffer[offset + 5] == ':')))
				{
					flag3 = true;
				}
			}
			else if (i - offset > 3 && this.scratchBuffer[offset] == 'f')
			{
				if (this.scratchBuffer[offset + 1] == 't' && this.scratchBuffer[offset + 2] == 'p' && this.scratchBuffer[offset + 3] == ':')
				{
					flag3 = true;
				}
				else if (i - offset > 6 && this.scratchBuffer[offset + 1] == 'i' && this.scratchBuffer[offset + 2] == 'l' && this.scratchBuffer[offset + 3] == 'e' && this.scratchBuffer[offset + 4] == ':' && this.scratchBuffer[offset + 5] == '/' && this.scratchBuffer[offset + 6] == '/')
				{
					flag3 = true;
				}
			}
			else if (i - offset > 6 && this.scratchBuffer[offset] == 'm')
			{
				if (this.scratchBuffer[offset + 1] == 'a' && this.scratchBuffer[offset + 2] == 'i' && this.scratchBuffer[offset + 3] == 'l' && this.scratchBuffer[offset + 4] == 't' && this.scratchBuffer[offset + 5] == 'o' && this.scratchBuffer[offset + 6] == ':')
				{
					flag3 = true;
				}
			}
			else if (i - offset > 3 && this.scratchBuffer[offset] == 'w')
			{
				if (this.scratchBuffer[offset + 1] == 'w' && this.scratchBuffer[offset + 2] == 'w' && this.scratchBuffer[offset + 3] == '.')
				{
					flag3 = true;
					addHttpPrefix = true;
				}
			}
			else if (i - offset > 7 && this.scratchBuffer[offset] == 'n' && this.scratchBuffer[offset + 1] == 'o' && this.scratchBuffer[offset + 2] == 't' && this.scratchBuffer[offset + 3] == 'e' && this.scratchBuffer[offset + 4] == 's' && this.scratchBuffer[offset + 5] == ':' && this.scratchBuffer[offset + 6] == '/' && this.scratchBuffer[offset + 7] == '/')
			{
				flag3 = true;
			}
			if (flag3)
			{
				i += run.AppendFragment(i, ref this.scratchBuffer, 4096 - i);
				if (flag2)
				{
					while (i > offset)
					{
						if (HtmlFormatOutput.IsHyperLinkEndDelimiter(this.scratchBuffer[i - 1]))
						{
							break;
						}
						i--;
					}
					while (i > offset)
					{
						if (!HtmlFormatOutput.IsHyperLinkEndDelimiter(this.scratchBuffer[i - 1]))
						{
							break;
						}
						i--;
					}
				}
				else
				{
					while (HtmlFormatOutput.IsHyperLinkEndDelimiter(this.scratchBuffer[i - 1]) || this.scratchBuffer[i - 1] == '.' || this.scratchBuffer[i - 1] == ',' || this.scratchBuffer[i - 1] == ';')
					{
						i--;
					}
				}
				length = i - offset;
			}
			return flag3;
		}

		// Token: 0x040013CC RID: 5068
		private const int MaxRecognizedHyperlinkLength = 4096;

		// Token: 0x040013CD RID: 5069
		private static string[] listType = new string[]
		{
			null,
			null,
			"1",
			"a",
			"A",
			"i",
			"I"
		};

		// Token: 0x040013CE RID: 5070
		private static Property[] defaultHyperlinkProperties = new Property[]
		{
			new Property(PropertyId.FontColor, new PropertyValue(new RGBT(0U, 0U, 255U)))
		};

		// Token: 0x040013CF RID: 5071
		private HtmlWriter writer;

		// Token: 0x040013D0 RID: 5072
		private HtmlInjection injection;

		// Token: 0x040013D1 RID: 5073
		private bool filterHtml;

		// Token: 0x040013D2 RID: 5074
		private HtmlTagCallback callback;

		// Token: 0x040013D3 RID: 5075
		private HtmlFormatOutputCallbackContext callbackContext;

		// Token: 0x040013D4 RID: 5076
		private bool outputFragment;

		// Token: 0x040013D5 RID: 5077
		private bool recognizeHyperlinks;

		// Token: 0x040013D6 RID: 5078
		private int hyperlinkLevel;

		// Token: 0x040013D7 RID: 5079
		private HtmlFormatOutput.EndTagActionEntry[] endTagActionStack;

		// Token: 0x040013D8 RID: 5080
		private int endTagActionStackTop;

		// Token: 0x020001CE RID: 462
		private struct EndTagActionEntry
		{
			// Token: 0x040013D9 RID: 5081
			public int TagLevel;

			// Token: 0x040013DA RID: 5082
			public bool Drop;

			// Token: 0x040013DB RID: 5083
			public bool Callback;
		}
	}
}
