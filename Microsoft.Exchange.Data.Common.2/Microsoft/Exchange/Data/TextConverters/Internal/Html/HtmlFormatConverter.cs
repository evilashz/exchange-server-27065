using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Css;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001C8 RID: 456
	internal class HtmlFormatConverter : FormatConverter, IProducerConsumer, IRestartable, IDisposable
	{
		// Token: 0x060013C6 RID: 5062 RVA: 0x0008BD64 File Offset: 0x00089F64
		public HtmlFormatConverter(HtmlNormalizingParser parser, FormatOutput output, bool testTreatNbspAsBreakable, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum, Stream formatConverterTraceStream, IProgressMonitor progressMonitor) : base(formatConverterTraceStream)
		{
			this.parser = parser;
			this.parser.SetRestartConsumer(this);
			this.progressMonitor = progressMonitor;
			this.output = output;
			if (this.output != null)
			{
				this.output.Initialize(this.Store, SourceFormat.Html, "converted from html");
			}
			this.treatNbspAsBreakable = testTreatNbspAsBreakable;
			base.InitializeDocument();
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0008BDD4 File Offset: 0x00089FD4
		public HtmlFormatConverter(HtmlNormalizingParser parser, FormatStore formatStore, bool fragment, bool testTreatNbspAsBreakable, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum, Stream formatConverterTraceStream, IProgressMonitor progressMonitor) : base(formatStore, formatConverterTraceStream)
		{
			this.parser = parser;
			this.parser.SetRestartConsumer(this);
			this.progressMonitor = progressMonitor;
			this.treatNbspAsBreakable = testTreatNbspAsBreakable;
			this.convertFragment = fragment;
			if (!fragment)
			{
				base.InitializeDocument();
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0008BE2A File Offset: 0x0008A02A
		private bool CanFlush
		{
			get
			{
				return this.output.CanAcceptMoreOutput;
			}
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0008BE38 File Offset: 0x0008A038
		public FormatNode Initialize(string fragment)
		{
			this.parser.Initialize(fragment, false);
			FormatNode result = base.InitializeFragment();
			this.Initialize();
			return result;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0008BE60 File Offset: 0x0008A060
		bool IRestartable.CanRestart()
		{
			return this.output == null || ((IRestartable)this.output).CanRestart();
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0008BE7C File Offset: 0x0008A07C
		void IRestartable.Restart()
		{
			if (this.output != null)
			{
				((IRestartable)this.output).Restart();
			}
			this.Store.Initialize();
			base.InitializeDocument();
			this.Initialize();
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0008BEAE File Offset: 0x0008A0AE
		void IRestartable.DisableRestart()
		{
			if (this.output != null)
			{
				((IRestartable)this.output).DisableRestart();
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0008BEC8 File Offset: 0x0008A0C8
		public override void Run()
		{
			if (this.output != null && base.MustFlush)
			{
				if (this.CanFlush)
				{
					this.FlushOutput();
					return;
				}
			}
			else if (!base.EndOfFile)
			{
				HtmlTokenId htmlTokenId = this.parser.Parse();
				if (htmlTokenId != HtmlTokenId.None)
				{
					this.Process(htmlTokenId);
				}
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0008BF13 File Offset: 0x0008A113
		public bool Flush()
		{
			this.Run();
			return base.EndOfFile && !base.MustFlush;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0008BF30 File Offset: 0x0008A130
		void IDisposable.Dispose()
		{
			if (this.parser != null)
			{
				((IDisposable)this.parser).Dispose();
			}
			if (this.token != null && this.token is IDisposable)
			{
				((IDisposable)this.token).Dispose();
			}
			this.parser = null;
			this.token = null;
			this.literalBuffer = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0008BF90 File Offset: 0x0008A190
		protected virtual void Process(HtmlTokenId tokenId)
		{
			this.token = this.parser.Token;
			switch (tokenId)
			{
			case HtmlTokenId.EndOfFile:
				base.CloseAllContainersAndSetEOF();
				break;
			case HtmlTokenId.Text:
				if (this.insideStyle)
				{
					this.token.Text.WriteTo(this.cssParserInput);
					return;
				}
				if (this.insideComment)
				{
					return;
				}
				if (this.insidePre)
				{
					this.ProcessPreformatedText();
					return;
				}
				this.ProcessText();
				return;
			case HtmlTokenId.EncodingChange:
				if (this.output != null && this.output.OutputCodePageSameAsInput)
				{
					this.output.OutputEncoding = this.token.TokenEncoding;
					return;
				}
				break;
			case HtmlTokenId.Tag:
				if (this.token.TagIndex <= HtmlTagIndex.Unknown)
				{
					if (this.insideStyle && this.token.TagIndex == HtmlTagIndex._COMMENT)
					{
						this.token.Text.WriteTo(this.cssParserInput);
						return;
					}
				}
				else
				{
					HtmlDtd.TagDefinition tagDefinition = HtmlFormatConverter.GetTagDefinition(this.token.TagIndex);
					if (!this.token.IsEndTag)
					{
						if (this.token.IsTagBegin)
						{
							this.PushElement(tagDefinition, this.token.IsEmptyScope);
						}
						this.ProcessStartTagAttributes(tagDefinition);
						return;
					}
					if (this.token.IsTagEnd)
					{
						this.PopElement(this.BuildStackTop - 1 - this.temporarilyClosedLevels, this.token.Argument != 1);
						return;
					}
				}
				break;
			case HtmlTokenId.Restart:
				break;
			case HtmlTokenId.OverlappedClose:
				this.temporarilyClosedLevels = this.token.Argument;
				return;
			case HtmlTokenId.OverlappedReopen:
				this.temporarilyClosedLevels = 0;
				return;
			default:
				return;
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0008C124 File Offset: 0x0008A324
		protected override FormatNode GetParentForNewNode(FormatNode node, FormatNode parent, int stackPos, out int propContainerInheritanceStopLevel)
		{
			FormatContainerType nodeType = node.NodeType;
			switch (nodeType)
			{
			case FormatContainerType.TableColumnGroup:
				break;
			case FormatContainerType.TableColumn:
			{
				FormatNode result = (parent.NodeType == FormatContainerType.TableColumnGroup) ? parent : this.FindStackParentForColumn(stackPos);
				if (!result.IsNull)
				{
					propContainerInheritanceStopLevel = stackPos;
					return result;
				}
				break;
			}
			default:
			{
				switch (nodeType)
				{
				case FormatContainerType.TableCaption:
				{
					propContainerInheritanceStopLevel = stackPos;
					FormatNode formatNode = (parent.NodeType == FormatContainerType.Table) ? parent : this.FindStackAncestor(stackPos, FormatContainerType.Table);
					FormatNode formatNode2 = formatNode.FirstChild;
					if (formatNode2.IsNull || formatNode2.NodeType != FormatContainerType.TableCaption)
					{
						formatNode2 = this.Store.AllocateNode(FormatContainerType.TableCaption);
						formatNode2.InheritanceMaskIndex = 1;
						formatNode2.SetOutOfOrder();
						formatNode.PrependChild(formatNode2);
					}
					return formatNode2;
				}
				case FormatContainerType.TableExtraContent:
					break;
				case FormatContainerType.Table:
					goto IL_321;
				case FormatContainerType.TableRow:
					if (parent.NodeType != FormatContainerType.Table)
					{
						parent = this.FindStackAncestor(stackPos, FormatContainerType.Table);
					}
					propContainerInheritanceStopLevel = stackPos;
					return parent;
				case FormatContainerType.TableCell:
					if (parent.NodeType != FormatContainerType.TableRow)
					{
						parent = this.FindStackAncestor(stackPos, FormatContainerType.TableRow);
					}
					propContainerInheritanceStopLevel = stackPos;
					return parent;
				default:
					goto IL_321;
				}
				IL_8B:
				propContainerInheritanceStopLevel = stackPos;
				FormatNode result2 = (parent.NodeType != FormatContainerType.Table && parent.NodeType != FormatContainerType.TableRow && parent.NodeType != FormatContainerType.TableColumnGroup) ? parent : this.FindStackParentForExtraContent(stackPos, out propContainerInheritanceStopLevel);
				if (result2.IsNull)
				{
					FormatNode newChildNode = (parent.NodeType == FormatContainerType.Table) ? parent : this.FindStackAncestor(stackPos, FormatContainerType.Table);
					FormatNode newSiblingNode = newChildNode.Parent;
					FormatNode formatNode3 = FormatNode.Null;
					if (newSiblingNode.NodeType != FormatContainerType.TableContainer)
					{
						newSiblingNode = this.Store.AllocateNode(FormatContainerType.TableContainer, newChildNode.BeginTextPosition);
						newSiblingNode.InheritanceMaskIndex = 1;
						newChildNode.InsertSiblingAfter(newSiblingNode);
						newChildNode.RemoveFromParent();
						newSiblingNode.AppendChild(newChildNode);
						newSiblingNode.SetOnRightEdge();
						formatNode3 = this.Store.AllocateNode(FormatContainerType.TableExtraContent);
						formatNode3.InheritanceMaskIndex = 1;
						formatNode3.SetOutOfOrder();
						if (newChildNode.OnLeftEdge)
						{
							newSiblingNode.SetOnLeftEdge();
							newSiblingNode.AppendChild(formatNode3);
						}
						else
						{
							newSiblingNode.PrependChild(formatNode3);
						}
					}
					else
					{
						foreach (FormatNode formatNode4 in newSiblingNode.Children)
						{
							if (formatNode4.NodeType == FormatContainerType.TableExtraContent)
							{
								formatNode3 = formatNode4;
							}
						}
					}
					result2 = formatNode3;
				}
				return result2;
				IL_321:
				if (parent.NodeType != FormatContainerType.Table && parent.NodeType != FormatContainerType.TableRow && parent.NodeType != FormatContainerType.TableColumnGroup)
				{
					propContainerInheritanceStopLevel = base.DefaultPropContainerInheritanceStopLevel(stackPos);
					return parent;
				}
				goto IL_8B;
			}
			}
			propContainerInheritanceStopLevel = stackPos;
			FormatNode formatNode5 = (parent.NodeType == FormatContainerType.Table) ? parent : this.FindStackAncestor(stackPos, FormatContainerType.Table);
			FormatNode formatNode6 = FormatNode.Null;
			FormatNode formatNode7 = formatNode5.FirstChild;
			if (!formatNode7.IsNull && formatNode7.NodeType == FormatContainerType.TableCaption)
			{
				formatNode6 = formatNode7;
				formatNode7 = formatNode7.NextSibling;
			}
			if (formatNode7.IsNull || formatNode7.NodeType != FormatContainerType.TableDefinition)
			{
				formatNode7 = this.Store.AllocateNode(FormatContainerType.TableDefinition);
				formatNode7.InheritanceMaskIndex = 1;
				formatNode7.SetOutOfOrder();
				if (formatNode6.IsNull)
				{
					formatNode5.PrependChild(formatNode7);
				}
				else
				{
					formatNode6.InsertSiblingAfter(formatNode7);
				}
			}
			parent = formatNode7;
			return parent;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0008C4A0 File Offset: 0x0008A6A0
		protected override FormatContainerType FixContainerType(FormatContainerType type, StyleBuildHelper styleBuilderWithContainerProperties)
		{
			PropertyValue property = styleBuilderWithContainerProperties.GetProperty(PropertyId.Display);
			if (property.IsEnum)
			{
				switch (property.Enum)
				{
				case 1:
					if (type == FormatContainerType.Block)
					{
						type = FormatContainerType.Inline;
					}
					break;
				case 2:
					if (type == FormatContainerType.PropertyContainer || type == FormatContainerType.Inline)
					{
						type = FormatContainerType.Block;
					}
					break;
				}
			}
			if (type == FormatContainerType.PropertyContainer)
			{
				PropertyValue property2 = styleBuilderWithContainerProperties.GetProperty(PropertyId.UnicodeBiDi);
				if (property2.IsEnum && (byte)property2.Enum != 0)
				{
					type = FormatContainerType.Inline;
				}
			}
			if (type == FormatContainerType.HyperLink && styleBuilderWithContainerProperties.GetProperty(PropertyId.HyperlinkUrl).IsNull)
			{
				type = FormatContainerType.Bookmark;
			}
			return type;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0008C53E File Offset: 0x0008A73E
		protected static HtmlDtd.TagDefinition GetTagDefinition(HtmlTagIndex tagIndex)
		{
			if (tagIndex == HtmlTagIndex._NULL)
			{
				return null;
			}
			return HtmlDtd.tags[(int)tagIndex];
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0008C54C File Offset: 0x0008A74C
		protected void ProcessStartTagAttributes(HtmlDtd.TagDefinition tagDef)
		{
			if (HtmlConverterData.tagInstructions[(int)this.token.TagIndex].ContainerType != FormatContainerType.Null)
			{
				this.token.Attributes.Rewind();
				foreach (HtmlAttribute attr in this.token.Attributes)
				{
					if (attr.NameIndex == HtmlNameIndex.Style)
					{
						this.ProcessStyleAttribute(tagDef, attr);
					}
					else if (attr.NameIndex == HtmlNameIndex.Id)
					{
						string @string = attr.Value.GetString(60);
						this.FindAndApplyStyle(new HtmlFormatConverter.StyleSelector(this.token.NameIndex, null, @string));
						this.FindAndApplyStyle(new HtmlFormatConverter.StyleSelector(HtmlNameIndex.Unknown, null, @string));
						this.ProcessNonStyleAttribute(attr);
					}
					else if (attr.NameIndex == HtmlNameIndex.Class)
					{
						string string2 = attr.Value.GetString(60);
						this.FindAndApplyStyle(new HtmlFormatConverter.StyleSelector(this.token.NameIndex, string2, null));
						this.FindAndApplyStyle(new HtmlFormatConverter.StyleSelector(HtmlNameIndex.Unknown, string2, null));
						if (!base.LastNonEmpty.Node.IsNull && string2.Equals("EmailQuote", StringComparison.OrdinalIgnoreCase))
						{
							base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.QuotingLevelDelta, new PropertyValue(PropertyType.Integer, 1));
						}
					}
					else if (attr.NameIndex != HtmlNameIndex.Unknown)
					{
						this.ProcessNonStyleAttribute(attr);
					}
				}
			}
			if (this.token.IsTagEnd && this.token.TagIndex == HtmlTagIndex.BR)
			{
				base.AddLineBreak(1);
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0008C6E0 File Offset: 0x0008A8E0
		protected void ProcessPreformatedText()
		{
			foreach (TokenRun run in this.token.Runs)
			{
				if (run.IsTextRun)
				{
					if (run.IsAnyWhitespace)
					{
						RunTextType textType = run.TextType;
						if (textType != RunTextType.Space)
						{
							if (textType == RunTextType.NewLine)
							{
								base.AddLineBreak(1);
								continue;
							}
							if (textType == RunTextType.Tabulation)
							{
								base.AddTabulation(run.Length);
								continue;
							}
						}
						base.AddSpace(run.Length);
					}
					else if (run.TextType == RunTextType.Nbsp)
					{
						if (this.treatNbspAsBreakable)
						{
							base.AddSpace(run.Length);
						}
						else
						{
							base.AddNbsp(run.Length);
						}
					}
					else
					{
						this.OutputNonspace(run);
					}
				}
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0008C7B4 File Offset: 0x0008A9B4
		protected void ProcessText()
		{
			foreach (TokenRun run in this.token.Runs)
			{
				if (run.IsTextRun)
				{
					if (run.IsAnyWhitespace)
					{
						base.AddSpace(1);
					}
					else if (run.TextType == RunTextType.Nbsp)
					{
						if (this.treatNbspAsBreakable)
						{
							base.AddSpace(run.Length);
						}
						else
						{
							base.AddNbsp(run.Length);
						}
					}
					else
					{
						this.OutputNonspace(run);
					}
				}
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0008C840 File Offset: 0x0008AA40
		protected int PushElement(HtmlDtd.TagDefinition tagDef, bool emptyScope)
		{
			int buildStackTop = this.BuildStackTop;
			FormatConverterContainer formatConverterContainer = FormatConverterContainer.Null;
			if (HtmlConverterData.tagInstructions[(int)tagDef.TagIndex].ContainerType != FormatContainerType.Null)
			{
				int defaultStyle = HtmlConverterData.tagInstructions[(int)tagDef.TagIndex].DefaultStyle;
				formatConverterContainer = base.OpenContainer(HtmlConverterData.tagInstructions[(int)tagDef.TagIndex].ContainerType, emptyScope, HtmlConverterData.tagInstructions[(int)tagDef.TagIndex].InheritanceMaskIndex, base.GetStyle(defaultStyle), tagDef.TagIndex);
			}
			else if (!emptyScope)
			{
				formatConverterContainer = base.OpenContainer(FormatContainerType.PropertyContainer, false, HtmlConverterData.tagInstructions[(int)tagDef.TagIndex].InheritanceMaskIndex, FormatStyle.Null, tagDef.TagIndex);
			}
			if (!formatConverterContainer.IsNull)
			{
				HtmlTagIndex tagIndex = tagDef.TagIndex;
				if (tagIndex <= HtmlTagIndex.Listing)
				{
					if (tagIndex <= HtmlTagIndex.Comment)
					{
						if (tagIndex == HtmlTagIndex.BlockQuote)
						{
							goto IL_208;
						}
						if (tagIndex != HtmlTagIndex.Comment)
						{
							goto IL_28A;
						}
					}
					else
					{
						if (tagIndex == HtmlTagIndex.DL)
						{
							goto IL_1ED;
						}
						switch (tagIndex)
						{
						case HtmlTagIndex.H1:
						case HtmlTagIndex.H2:
						case HtmlTagIndex.H3:
						case HtmlTagIndex.H4:
						case HtmlTagIndex.H5:
						case HtmlTagIndex.H6:
							goto IL_208;
						default:
							if (tagIndex != HtmlTagIndex.Listing)
							{
								goto IL_28A;
							}
							goto IL_1E4;
						}
					}
				}
				else if (tagIndex <= HtmlTagIndex.Script)
				{
					switch (tagIndex)
					{
					case HtmlTagIndex.OL:
						goto IL_1ED;
					case HtmlTagIndex.OptGroup:
					case HtmlTagIndex.Option:
					case HtmlTagIndex.Param:
						goto IL_28A;
					case HtmlTagIndex.P:
						goto IL_208;
					case HtmlTagIndex.PlainText:
					case HtmlTagIndex.Pre:
						goto IL_1E4;
					default:
						if (tagIndex != HtmlTagIndex.Script)
						{
							goto IL_28A;
						}
						break;
					}
				}
				else
				{
					if (tagIndex == HtmlTagIndex.Style)
					{
						if (this.cssParserInput == null)
						{
							this.cssParserInput = new ConverterBufferInput(524288, this.progressMonitor);
							this.cssParser = new CssParser(this.cssParserInput, 1024, false);
						}
						this.insideStyle = true;
						goto IL_28A;
					}
					if (tagIndex == HtmlTagIndex.Title)
					{
						this.insideComment = !this.token.IsEndTag;
						goto IL_28A;
					}
					switch (tagIndex)
					{
					case HtmlTagIndex.UL:
						goto IL_1ED;
					case HtmlTagIndex.Var:
					case HtmlTagIndex.Wbr:
						goto IL_28A;
					case HtmlTagIndex.Xmp:
						goto IL_1E4;
					case HtmlTagIndex.Xml:
						break;
					default:
						goto IL_28A;
					}
				}
				this.insideComment = true;
				goto IL_28A;
				IL_1E4:
				this.insidePre = true;
				goto IL_208;
				IL_1ED:
				int num = this.insideList;
				this.insideList++;
				if (num != 0)
				{
					goto IL_28A;
				}
				IL_208:
				if (!base.LastNode.FirstChild.IsNull || (base.LastNode.NodeType != FormatContainerType.Document && base.LastNode.NodeType != FormatContainerType.Fragment && base.LastNode.NodeType != FormatContainerType.TableCell))
				{
					formatConverterContainer.SetProperty(PropertyPrecedence.TagDefault, PropertyId.Margins, new PropertyValue(LengthUnits.Points, 14));
					formatConverterContainer.SetProperty(PropertyPrecedence.TagDefault, PropertyId.BottomMargin, new PropertyValue(LengthUnits.Points, 14));
				}
				IL_28A:
				this.FindAndApplyStyle(new HtmlFormatConverter.StyleSelector(tagDef.NameIndex, null, null));
			}
			return buildStackTop;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0008CAEC File Offset: 0x0008ACEC
		protected void PopElement(int stackPos, bool explicitClose)
		{
			FormatNode node = base.Last.Node;
			HtmlTagIndex tagIndex = this.BuildStack[stackPos].TagIndex;
			if (tagIndex <= HtmlTagIndex.OL)
			{
				if (tagIndex <= HtmlTagIndex.DL)
				{
					if (tagIndex != HtmlTagIndex.Comment)
					{
						if (tagIndex != HtmlTagIndex.DL)
						{
							goto IL_F6;
						}
						goto IL_E8;
					}
				}
				else
				{
					if (tagIndex == HtmlTagIndex.Listing)
					{
						goto IL_DF;
					}
					if (tagIndex != HtmlTagIndex.OL)
					{
						goto IL_F6;
					}
					goto IL_E8;
				}
			}
			else if (tagIndex <= HtmlTagIndex.Script)
			{
				switch (tagIndex)
				{
				case HtmlTagIndex.PlainText:
				case HtmlTagIndex.Pre:
					goto IL_DF;
				default:
					if (tagIndex != HtmlTagIndex.Script)
					{
						goto IL_F6;
					}
					break;
				}
			}
			else if (tagIndex != HtmlTagIndex.Style)
			{
				if (tagIndex == HtmlTagIndex.Title)
				{
					this.insideComment = !this.token.IsEndTag;
					goto IL_F6;
				}
				switch (tagIndex)
				{
				case HtmlTagIndex.UL:
					goto IL_E8;
				case HtmlTagIndex.Var:
				case HtmlTagIndex.Wbr:
					goto IL_F6;
				case HtmlTagIndex.Xmp:
					goto IL_DF;
				case HtmlTagIndex.Xml:
					break;
				default:
					goto IL_F6;
				}
			}
			else
			{
				if (this.insideStyle)
				{
					this.ProcessStylesheet();
					this.insideStyle = false;
					goto IL_F6;
				}
				goto IL_F6;
			}
			this.insideComment = false;
			goto IL_F6;
			IL_DF:
			this.insidePre = false;
			goto IL_F6;
			IL_E8:
			this.insideList--;
			IL_F6:
			if (stackPos == this.BuildStackTop - 1)
			{
				base.CloseContainer();
			}
			else
			{
				base.CloseOverlappingContainer(this.BuildStackTop - 1 - stackPos);
			}
			if (!node.IsNull && node.NodeType == FormatContainerType.Table)
			{
				while (!node.LastChild.IsNull && node.LastChild.NodeType == FormatContainerType.TableRow)
				{
					bool flag = true;
					if (!node.LastChild.GetProperty(PropertyId.Height).IsNull)
					{
						return;
					}
					foreach (FormatNode formatNode in node.LastChild.Children)
					{
						if (!formatNode.FirstChild.IsNull)
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						return;
					}
					node.LastChild.RemoveFromParent();
				}
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0008CCF8 File Offset: 0x0008AEF8
		private void Initialize()
		{
			this.insideComment = false;
			this.insidePre = false;
			this.insideStyle = false;
			this.insideList = 0;
			this.temporarilyClosedLevels = 0;
			if (this.styleDictionary != null)
			{
				this.styleDictionary.Clear();
			}
			if (this.cssParserInput != null)
			{
				this.cssParserInput.Reset();
				this.cssParser.Reset();
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0008CD59 File Offset: 0x0008AF59
		private bool FlushOutput()
		{
			if (this.output.Flush())
			{
				base.MustFlush = false;
				return true;
			}
			return false;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0008CD74 File Offset: 0x0008AF74
		private FormatNode FindStackAncestor(int stackPosOfNewContainer, FormatContainerType type)
		{
			for (int i = stackPosOfNewContainer - 1; i >= 0; i--)
			{
				if (this.BuildStack[i].Type == type)
				{
					return this.Store.GetNode(this.BuildStack[i].Node);
				}
			}
			return FormatNode.Null;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0008CDC8 File Offset: 0x0008AFC8
		private FormatNode FindStackParentForExtraContent(int stackPosOfNewContainer, out int ancestorContainerLevel)
		{
			bool flag = false;
			for (int i = stackPosOfNewContainer - 1; i >= 0; i--)
			{
				if (this.BuildStack[i].Node != 0)
				{
					if (this.BuildStack[i].Type == FormatContainerType.Table)
					{
						flag = true;
					}
					else if (this.BuildStack[i].Type != FormatContainerType.TableRow && this.BuildStack[i].Type != FormatContainerType.TableColumnGroup)
					{
						ancestorContainerLevel = i + 1;
						if (!flag)
						{
							return this.Store.GetNode(this.BuildStack[i].Node);
						}
						return FormatNode.Null;
					}
				}
			}
			ancestorContainerLevel = stackPosOfNewContainer;
			return FormatNode.Null;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0008CE7C File Offset: 0x0008B07C
		private FormatNode FindStackParentForColumn(int stackPosOfNewContainer)
		{
			for (int i = stackPosOfNewContainer - 1; i >= 0; i--)
			{
				if (this.BuildStack[i].Node != 0)
				{
					if (this.BuildStack[i].Type == FormatContainerType.Table)
					{
						break;
					}
					if (this.BuildStack[i].Type == FormatContainerType.TableColumnGroup)
					{
						return this.Store.GetNode(this.BuildStack[i].Node);
					}
				}
			}
			return FormatNode.Null;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0008CEFC File Offset: 0x0008B0FC
		private bool StartTagHasAttribute(HtmlNameIndex attributeNameIndex)
		{
			foreach (HtmlAttribute htmlAttribute in this.token.Attributes)
			{
				if (htmlAttribute.NameIndex == attributeNameIndex)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0008CF44 File Offset: 0x0008B144
		private void OutputNonspace(TokenRun run)
		{
			if (run.IsLiteral)
			{
				base.AddNonSpaceText(this.literalBuffer, 0, run.ReadLiteral(this.literalBuffer));
				return;
			}
			base.AddNonSpaceText(run.RawBuffer, run.RawOffset, run.RawLength);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0008CF90 File Offset: 0x0008B190
		private void ProcessStyleAttribute(HtmlDtd.TagDefinition tagDef, HtmlAttribute attr)
		{
			if (attr.IsAttrBegin)
			{
				if (this.cssParserInput == null)
				{
					this.cssParserInput = new ConverterBufferInput(524288, this.progressMonitor);
					this.cssParser = new CssParser(this.cssParserInput, 1024, false);
				}
				this.cssParser.SetParseMode(CssParseMode.StyleAttribute);
			}
			attr.Value.Rewind();
			attr.Value.WriteTo(this.cssParserInput);
			if (attr.IsAttrEnd)
			{
				CssTokenId cssTokenId;
				do
				{
					cssTokenId = this.cssParser.Parse();
					if (CssTokenId.Declarations == cssTokenId && this.cssParser.Token.Properties.ValidCount != 0)
					{
						this.cssParser.Token.Properties.Rewind();
						foreach (CssProperty prop in this.cssParser.Token.Properties)
						{
							if (prop.NameId != CssNameIndex.Unknown)
							{
								if (HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].ParsingMethod != null)
								{
									PropertyValue value;
									if (!prop.Value.IsEmpty && prop.Value.IsContiguous)
									{
										value = HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].ParsingMethod(prop.Value.ContiguousBufferString, this);
									}
									else
									{
										this.scratch.AppendCssPropertyValue(prop, 4096);
										value = HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].ParsingMethod(this.scratch.BufferString, this);
										this.scratch.Reset();
									}
									if (!value.IsNull)
									{
										PropertyId propertyId = HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].PropertyId;
										base.Last.SetProperty(PropertyPrecedence.InlineStyle, propertyId, value);
									}
								}
								else if (HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].MultiPropertyParsingMethod != null)
								{
									if (this.parsedProperties == null)
									{
										this.parsedProperties = new Property[12];
									}
									int num;
									if (!prop.Value.IsEmpty && prop.Value.IsContiguous)
									{
										HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].MultiPropertyParsingMethod(prop.Value.ContiguousBufferString, this, HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].PropertyId, this.parsedProperties, out num);
									}
									else
									{
										this.scratch.AppendCssPropertyValue(prop, 4096);
										HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].MultiPropertyParsingMethod(this.scratch.BufferString, this, HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].PropertyId, this.parsedProperties, out num);
										this.scratch.Reset();
									}
									if (num != 0)
									{
										base.Last.SetProperties(PropertyPrecedence.InlineStyle, this.parsedProperties, num);
									}
								}
							}
						}
					}
				}
				while (CssTokenId.EndOfFile != cssTokenId);
				this.cssParserInput.Reset();
				this.cssParser.Reset();
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0008D2CC File Offset: 0x0008B4CC
		private void ProcessNonStyleAttribute(HtmlAttribute attr)
		{
			if (attr.IsAttrBegin)
			{
				if (this.token.TagIndex == HtmlTagIndex.A && attr.NameIndex == HtmlNameIndex.Href)
				{
					base.Last.SetProperty(PropertyPrecedence.TagDefault, PropertyId.Underline, new PropertyValue(true));
					base.Last.SetProperty(PropertyPrecedence.TagDefault, PropertyId.FontColor, new PropertyValue(new RGBT(0U, 0U, 255U)));
				}
				this.attributeParsingMethod = null;
				if (HtmlConverterData.tagInstructions[(int)this.token.TagIndex].AttributeInstructions != null)
				{
					int i = 0;
					while (i < HtmlConverterData.tagInstructions[(int)this.token.TagIndex].AttributeInstructions.Length)
					{
						if (attr.NameIndex == HtmlConverterData.tagInstructions[(int)this.token.TagIndex].AttributeInstructions[i].AttributeNameId)
						{
							this.attributeParsingMethod = HtmlConverterData.tagInstructions[(int)this.token.TagIndex].AttributeInstructions[i].ParsingMethod;
							this.attributePropertyId = HtmlConverterData.tagInstructions[(int)this.token.TagIndex].AttributeInstructions[i].PropertyId;
							if (attr.IsAttrEnd && !attr.Value.IsEmpty && attr.Value.IsContiguous)
							{
								PropertyValue value = this.attributeParsingMethod(attr.Value.ContiguousBufferString, this);
								if (!value.IsNull)
								{
									base.Last.SetProperty(PropertyPrecedence.NonStyle, this.attributePropertyId, value);
								}
								return;
							}
							break;
						}
						else
						{
							i++;
						}
					}
				}
				PropertyValueParsingMethod propertyValueParsingMethod = this.attributeParsingMethod;
			}
			if (this.attributeParsingMethod == null)
			{
				return;
			}
			this.scratch.AppendHtmlAttributeValue(attr, 4096);
			if (attr.IsAttrEnd)
			{
				PropertyValue value2 = this.attributeParsingMethod(this.scratch.BufferString, this);
				if (!value2.IsNull)
				{
					base.Last.SetProperty(PropertyPrecedence.NonStyle, this.attributePropertyId, value2);
				}
				this.scratch.Reset();
			}
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0008D4F4 File Offset: 0x0008B6F4
		private void ProcessStylesheet()
		{
			this.cssParser.SetParseMode(CssParseMode.StyleTag);
			CssTokenId cssTokenId;
			do
			{
				cssTokenId = this.cssParser.Parse();
				if (CssTokenId.RuleSet == cssTokenId && this.cssParser.Token.Selectors.ValidCount != 0 && this.cssParser.Token.Properties.ValidCount != 0)
				{
					bool flag = false;
					bool flag2 = false;
					this.cssParser.Token.Selectors.Rewind();
					foreach (CssSelector cssSelector in this.cssParser.Token.Selectors)
					{
						if (cssSelector.IsSimple)
						{
							HtmlFormatConverter.StyleSelector key = default(HtmlFormatConverter.StyleSelector);
							bool flag3 = false;
							if (cssSelector.HasClassFragment)
							{
								if (cssSelector.ClassType == CssSelectorClassType.Regular || cssSelector.ClassType == CssSelectorClassType.Hash)
								{
									string @string = cssSelector.ClassName.GetString(60);
									if (cssSelector.ClassType == CssSelectorClassType.Regular)
									{
										key = new HtmlFormatConverter.StyleSelector(cssSelector.NameId, @string, null);
										flag3 = true;
										flag2 = (flag2 || @string.Equals("MsoNormal", StringComparison.OrdinalIgnoreCase));
									}
									else
									{
										key = new HtmlFormatConverter.StyleSelector(cssSelector.NameId, null, @string);
										flag3 = true;
									}
								}
							}
							else if (cssSelector.NameId != HtmlNameIndex.Unknown)
							{
								key = new HtmlFormatConverter.StyleSelector(cssSelector.NameId, null, null);
								flag3 = true;
							}
							if (flag3 && (flag || this.styleHandleIndexCount < 128))
							{
								if (!flag)
								{
									if (this.styleHandleIndex == null)
									{
										this.styleHandleIndex = new int[32];
									}
									else if (this.styleHandleIndexCount == this.styleHandleIndex.Length)
									{
										int[] destinationArray = new int[this.styleHandleIndexCount * 2];
										Array.Copy(this.styleHandleIndex, 0, destinationArray, 0, this.styleHandleIndexCount);
										this.styleHandleIndex = destinationArray;
									}
									this.styleHandleIndexCount++;
									flag = true;
								}
								if (this.styleDictionary == null)
								{
									this.styleDictionary = new Dictionary<HtmlFormatConverter.StyleSelector, int>(new HtmlFormatConverter.StyleSelectorComparer());
								}
								if (!this.styleDictionary.ContainsKey(key))
								{
									this.styleDictionary.Add(key, this.styleHandleIndexCount - 1);
								}
								else
								{
									this.styleDictionary[key] = this.styleHandleIndexCount - 1;
								}
							}
						}
					}
					if (flag)
					{
						StyleBuilder styleBuilder;
						FormatStyle formatStyle = base.RegisterStyle(false, out styleBuilder);
						this.cssParser.Token.Properties.Rewind();
						foreach (CssProperty prop in this.cssParser.Token.Properties)
						{
							if (prop.NameId != CssNameIndex.Unknown)
							{
								if (HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].ParsingMethod != null)
								{
									PropertyValue value;
									if (!prop.Value.IsEmpty && prop.Value.IsContiguous)
									{
										value = HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].ParsingMethod(prop.Value.ContiguousBufferString, this);
									}
									else
									{
										this.scratch.AppendCssPropertyValue(prop, 4096);
										value = HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].ParsingMethod(this.scratch.BufferString, this);
										this.scratch.Reset();
									}
									if (!value.IsNull)
									{
										styleBuilder.SetProperty(HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].PropertyId, value);
									}
								}
								else if (HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].MultiPropertyParsingMethod != null)
								{
									if (this.parsedProperties == null)
									{
										this.parsedProperties = new Property[12];
									}
									int num;
									if (!prop.Value.IsEmpty && prop.Value.IsContiguous)
									{
										HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].MultiPropertyParsingMethod(prop.Value.ContiguousBufferString, this, HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].PropertyId, this.parsedProperties, out num);
									}
									else
									{
										this.scratch.AppendCssPropertyValue(prop, 4096);
										HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].MultiPropertyParsingMethod(this.scratch.BufferString, this, HtmlConverterData.cssPropertyInstructions[(int)prop.NameId].PropertyId, this.parsedProperties, out num);
										this.scratch.Reset();
									}
									if (num != 0)
									{
										styleBuilder.SetProperties(this.parsedProperties, num);
									}
								}
							}
						}
						styleBuilder.Flush();
						if (formatStyle.IsEmpty)
						{
							formatStyle.Release();
							this.styleHandleIndex[this.styleHandleIndexCount - 1] = 0;
						}
						else
						{
							this.styleHandleIndex[this.styleHandleIndexCount - 1] = formatStyle.Handle;
						}
					}
				}
			}
			while (CssTokenId.EndOfFile != cssTokenId);
			this.cssParserInput.Reset();
			this.cssParser.Reset();
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0008D9F0 File Offset: 0x0008BBF0
		private void FindAndApplyStyle(HtmlFormatConverter.StyleSelector styleSelector)
		{
			int num;
			if (this.styleDictionary != null && this.styleDictionary.TryGetValue(styleSelector, out num) && this.styleHandleIndex[num] != 0)
			{
				base.Last.SetStyleReference(9 - styleSelector.Specificity, this.styleHandleIndex[num]);
			}
		}

		// Token: 0x040013B0 RID: 5040
		protected bool convertFragment;

		// Token: 0x040013B1 RID: 5041
		protected HtmlNormalizingParser parser;

		// Token: 0x040013B2 RID: 5042
		protected HtmlToken token;

		// Token: 0x040013B3 RID: 5043
		protected bool treatNbspAsBreakable;

		// Token: 0x040013B4 RID: 5044
		protected bool insideComment;

		// Token: 0x040013B5 RID: 5045
		protected bool insideStyle;

		// Token: 0x040013B6 RID: 5046
		protected bool insidePre;

		// Token: 0x040013B7 RID: 5047
		protected int insideList;

		// Token: 0x040013B8 RID: 5048
		protected int temporarilyClosedLevels;

		// Token: 0x040013B9 RID: 5049
		protected char[] literalBuffer = new char[2];

		// Token: 0x040013BA RID: 5050
		protected FormatOutput output;

		// Token: 0x040013BB RID: 5051
		private CssParser cssParser;

		// Token: 0x040013BC RID: 5052
		protected ConverterBufferInput cssParserInput;

		// Token: 0x040013BD RID: 5053
		private Dictionary<HtmlFormatConverter.StyleSelector, int> styleDictionary;

		// Token: 0x040013BE RID: 5054
		private int[] styleHandleIndex;

		// Token: 0x040013BF RID: 5055
		private int styleHandleIndexCount;

		// Token: 0x040013C0 RID: 5056
		private ScratchBuffer scratch;

		// Token: 0x040013C1 RID: 5057
		private Property[] parsedProperties;

		// Token: 0x040013C2 RID: 5058
		private PropertyValueParsingMethod attributeParsingMethod;

		// Token: 0x040013C3 RID: 5059
		private PropertyId attributePropertyId;

		// Token: 0x040013C4 RID: 5060
		private IProgressMonitor progressMonitor;

		// Token: 0x020001C9 RID: 457
		private struct StyleSelector
		{
			// Token: 0x060013E4 RID: 5092 RVA: 0x0008DA40 File Offset: 0x0008BC40
			public StyleSelector(HtmlNameIndex nameId, string cls, string id)
			{
				this.NameId = nameId;
				this.Cls = ((cls == null || cls.Length == 0 || cls.Equals("*")) ? null : cls);
				this.Id = ((string.IsNullOrEmpty(id) || id.Equals("*")) ? null : id);
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0008DA95 File Offset: 0x0008BC95
			public int Specificity
			{
				get
				{
					return ((this.Id == null) ? 0 : 4) + ((this.Cls == null) ? 0 : 2) + ((this.NameId == HtmlNameIndex.Unknown) ? 0 : 1);
				}
			}

			// Token: 0x040013C5 RID: 5061
			public HtmlNameIndex NameId;

			// Token: 0x040013C6 RID: 5062
			public string Cls;

			// Token: 0x040013C7 RID: 5063
			public string Id;
		}

		// Token: 0x020001CA RID: 458
		private class StyleSelectorComparer : IEqualityComparer<HtmlFormatConverter.StyleSelector>, IComparer<HtmlFormatConverter.StyleSelector>
		{
			// Token: 0x060013E6 RID: 5094 RVA: 0x0008DAC0 File Offset: 0x0008BCC0
			public int Compare(HtmlFormatConverter.StyleSelector x, HtmlFormatConverter.StyleSelector y)
			{
				int specificity = x.Specificity;
				if (specificity != y.Specificity)
				{
					return specificity - y.Specificity;
				}
				switch (specificity)
				{
				case 1:
					return (int)(x.NameId - y.NameId);
				case 2:
					return string.Compare(x.Cls, y.Cls, StringComparison.OrdinalIgnoreCase);
				case 3:
				{
					int result;
					if ((result = string.Compare(x.Cls, y.Cls, StringComparison.OrdinalIgnoreCase)) == 0)
					{
						return (int)(x.NameId - y.NameId);
					}
					return result;
				}
				case 4:
					return string.Compare(x.Id, y.Id, StringComparison.OrdinalIgnoreCase);
				case 5:
				{
					int result;
					if ((result = string.Compare(x.Id, y.Id, StringComparison.OrdinalIgnoreCase)) == 0)
					{
						return (int)(x.NameId - y.NameId);
					}
					return result;
				}
				case 6:
				{
					int result;
					if ((result = string.Compare(x.Id, y.Id, StringComparison.OrdinalIgnoreCase)) == 0)
					{
						return string.Compare(x.Cls, y.Cls, StringComparison.OrdinalIgnoreCase);
					}
					return result;
				}
				case 7:
				{
					int result;
					if ((result = string.Compare(x.Id, y.Id, StringComparison.OrdinalIgnoreCase)) != 0)
					{
						return result;
					}
					if ((result = string.Compare(x.Cls, y.Cls, StringComparison.OrdinalIgnoreCase)) == 0)
					{
						return (int)(x.NameId - y.NameId);
					}
					return result;
				}
				default:
					return 0;
				}
			}

			// Token: 0x060013E7 RID: 5095 RVA: 0x0008DC16 File Offset: 0x0008BE16
			public bool Equals(HtmlFormatConverter.StyleSelector x, HtmlFormatConverter.StyleSelector y)
			{
				return this.Compare(x, y) == 0;
			}

			// Token: 0x060013E8 RID: 5096 RVA: 0x0008DC24 File Offset: 0x0008BE24
			public int GetHashCode(HtmlFormatConverter.StyleSelector x)
			{
				int specificity = x.Specificity;
				return (int)x.NameId ^ (((specificity & 4) == 0) ? 0 : x.Id.GetHashCode()) ^ (((specificity & 2) == 0) ? 0 : x.Cls.GetHashCode());
			}
		}
	}
}
