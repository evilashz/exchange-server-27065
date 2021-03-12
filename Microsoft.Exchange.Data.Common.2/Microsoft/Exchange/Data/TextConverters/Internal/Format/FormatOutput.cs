using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020001BD RID: 445
	internal abstract class FormatOutput : IDisposable
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00086D47 File Offset: 0x00084F47
		public virtual bool OutputCodePageSameAsInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (set) Token: 0x060012F8 RID: 4856 RVA: 0x00086D4A File Offset: 0x00084F4A
		public virtual Encoding OutputEncoding
		{
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00086D51 File Offset: 0x00084F51
		public virtual bool CanAcceptMoreOutput
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00086D54 File Offset: 0x00084F54
		protected FormatStore FormatStore
		{
			get
			{
				return this.formatStore;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00086D5C File Offset: 0x00084F5C
		protected SourceFormat SourceFormat
		{
			get
			{
				return this.sourceFormat;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00086D64 File Offset: 0x00084F64
		protected string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00086D6C File Offset: 0x00084F6C
		protected FormatNode CurrentNode
		{
			get
			{
				return this.currentOutputLevel.Node;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00086D79 File Offset: 0x00084F79
		protected int CurrentNodeIndex
		{
			get
			{
				return this.currentOutputLevel.Index;
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00086D86 File Offset: 0x00084F86
		protected FormatOutput(Stream formatOutputTraceStream)
		{
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00086D99 File Offset: 0x00084F99
		public virtual void Initialize(FormatStore store, SourceFormat sourceFormat, string comment)
		{
			this.sourceFormat = sourceFormat;
			this.comment = comment;
			this.formatStore = store;
			this.Restart(this.formatStore.RootNode);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00086DC1 File Offset: 0x00084FC1
		public void Restart(FormatNode rootNode)
		{
			this.outputStackTop = 0;
			this.currentOutputLevel.Node = rootNode;
			this.currentOutputLevel.State = FormatOutput.OutputState.NotStarted;
			this.rootNode = rootNode;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00086DE9 File Offset: 0x00084FE9
		protected void Restart()
		{
			this.Restart(this.rootNode);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00086DF7 File Offset: 0x00084FF7
		public bool HaveSomethingToFlush()
		{
			return this.currentOutputLevel.Node.CanFlush;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00086E09 File Offset: 0x00085009
		public FlagProperties GetEffectiveFlags()
		{
			return this.propertyState.GetEffectiveFlags();
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00086E16 File Offset: 0x00085016
		public FlagProperties GetDistinctFlags()
		{
			return this.propertyState.GetDistinctFlags();
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00086E23 File Offset: 0x00085023
		public PropertyValue GetEffectiveProperty(PropertyId id)
		{
			return this.propertyState.GetEffectiveProperty(id);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00086E31 File Offset: 0x00085031
		public PropertyValue GetDistinctProperty(PropertyId id)
		{
			return this.propertyState.GetDistinctProperty(id);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00086E3F File Offset: 0x0008503F
		public void SubtractDefaultContainerPropertiesFromDistinct(FlagProperties flags, Property[] properties)
		{
			this.propertyState.SubtractDefaultFromDistinct(flags, properties);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00086E50 File Offset: 0x00085050
		public virtual bool Flush()
		{
			while (this.CanAcceptMoreOutput && this.currentOutputLevel.State != FormatOutput.OutputState.Ended)
			{
				if (this.currentOutputLevel.State == FormatOutput.OutputState.NotStarted)
				{
					if (this.StartCurrentLevel())
					{
						this.PushFirstChild();
					}
					else
					{
						this.PopPushNextSibling();
					}
				}
				else if (this.currentOutputLevel.State == FormatOutput.OutputState.Started)
				{
					if (this.ContinueCurrentLevel())
					{
						this.currentOutputLevel.State = FormatOutput.OutputState.EndPending;
					}
				}
				else
				{
					this.EndCurrentLevel();
					this.currentOutputLevel.State = FormatOutput.OutputState.Ended;
					if (this.outputStackTop != 0)
					{
						this.PopPushNextSibling();
					}
				}
			}
			return this.currentOutputLevel.State == FormatOutput.OutputState.Ended;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00086EEC File Offset: 0x000850EC
		public void OutputFragment(FormatNode fragmentNode)
		{
			this.Restart(fragmentNode);
			this.FlushFragment();
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00086EFC File Offset: 0x000850FC
		public void OutputFragment(FormatNode beginNode, uint beginTextPosition, FormatNode endNode, uint endTextPosition)
		{
			this.Restart(this.rootNode);
			FormatNode formatNode = beginNode;
			int num = 0;
			while (formatNode != this.rootNode)
			{
				num++;
				formatNode = formatNode.Parent;
			}
			if (this.outputStack == null)
			{
				this.outputStack = new FormatOutput.OutputStackEntry[Math.Max(32, num)];
			}
			else if (this.outputStack.Length < num)
			{
				if (this.outputStackTop >= 4096)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				this.outputStack = new FormatOutput.OutputStackEntry[Math.Max(this.outputStack.Length * 2, num)];
			}
			formatNode = beginNode;
			int i = num - 1;
			while (formatNode != this.rootNode)
			{
				this.outputStack[i--].Node = formatNode;
				formatNode = formatNode.Parent;
			}
			for (i = 0; i < num; i++)
			{
				if (!this.StartCurrentLevel())
				{
					this.PopPushNextSibling();
					break;
				}
				this.currentOutputLevel.State = FormatOutput.OutputState.Started;
				this.Push(this.outputStack[i].Node);
			}
			bool flag = false;
			while (this.currentOutputLevel.State != FormatOutput.OutputState.Ended)
			{
				if (this.currentOutputLevel.State == FormatOutput.OutputState.NotStarted)
				{
					if (this.StartCurrentLevel())
					{
						this.PushFirstChild();
					}
					else
					{
						this.PopPushNextSibling();
					}
				}
				else if (this.currentOutputLevel.State == FormatOutput.OutputState.Started)
				{
					uint num2 = (this.currentOutputLevel.Node == beginNode) ? beginTextPosition : this.currentOutputLevel.Node.BeginTextPosition;
					uint num3 = (this.currentOutputLevel.Node == endNode) ? endTextPosition : this.currentOutputLevel.Node.EndTextPosition;
					if (num2 <= num3)
					{
						this.ContinueText(num2, num3);
					}
					this.currentOutputLevel.State = FormatOutput.OutputState.EndPending;
				}
				else
				{
					this.EndCurrentLevel();
					this.currentOutputLevel.State = FormatOutput.OutputState.Ended;
					if (this.outputStackTop != 0)
					{
						if (!flag && this.currentOutputLevel.Node != endNode && (this.currentOutputLevel.Node.NextSibling.IsNull || this.currentOutputLevel.Node.NextSibling != endNode || (this.currentOutputLevel.Node.NextSibling.NodeType == FormatContainerType.Text && this.currentOutputLevel.Node.NextSibling.BeginTextPosition < endTextPosition)))
						{
							this.PopPushNextSibling();
						}
						else
						{
							this.Pop();
							this.currentOutputLevel.State = FormatOutput.OutputState.EndPending;
							flag = true;
						}
					}
				}
			}
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00087188 File Offset: 0x00085388
		private void FlushFragment()
		{
			while (this.currentOutputLevel.State != FormatOutput.OutputState.Ended)
			{
				if (this.currentOutputLevel.State == FormatOutput.OutputState.NotStarted)
				{
					if (this.StartCurrentLevel())
					{
						this.PushFirstChild();
					}
					else
					{
						this.PopPushNextSibling();
					}
				}
				else if (this.currentOutputLevel.State == FormatOutput.OutputState.Started)
				{
					if (this.ContinueCurrentLevel())
					{
						this.currentOutputLevel.State = FormatOutput.OutputState.EndPending;
					}
				}
				else
				{
					this.EndCurrentLevel();
					this.currentOutputLevel.State = FormatOutput.OutputState.Ended;
					if (this.outputStackTop != 0)
					{
						this.PopPushNextSibling();
					}
				}
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00087210 File Offset: 0x00085410
		private bool StartCurrentLevel()
		{
			FormatContainerType nodeType = this.currentOutputLevel.Node.NodeType;
			switch (nodeType)
			{
			case FormatContainerType.TableContainer:
				return this.StartTableContainer();
			case FormatContainerType.TableDefinition:
				return this.StartTableDefinition();
			case FormatContainerType.TableColumnGroup:
				return this.StartTableColumnGroup();
			case FormatContainerType.TableColumn:
				this.StartEndTableColumn();
				return false;
			case (FormatContainerType)11:
			case (FormatContainerType)12:
			case (FormatContainerType)13:
			case (FormatContainerType)14:
			case (FormatContainerType)15:
			case (FormatContainerType)16:
			case (FormatContainerType)17:
			case (FormatContainerType)21:
			case (FormatContainerType)23:
			case (FormatContainerType)35:
				break;
			case FormatContainerType.Inline:
				return this.StartInline();
			case FormatContainerType.HyperLink:
				return this.StartHyperLink();
			case FormatContainerType.Bookmark:
				return this.StartBookmark();
			case FormatContainerType.Area:
				this.StartEndArea();
				return false;
			case FormatContainerType.BaseFont:
				this.StartEndBaseFont();
				return false;
			case FormatContainerType.Form:
				return this.StartForm();
			case FormatContainerType.FieldSet:
				return this.StartFieldSet();
			case FormatContainerType.Label:
				return this.StartLabel();
			case FormatContainerType.Input:
				return this.StartInput();
			case FormatContainerType.Button:
				return this.StartButton();
			case FormatContainerType.Legend:
				return this.StartLegend();
			case FormatContainerType.TextArea:
				return this.StartTextArea();
			case FormatContainerType.Select:
				return this.StartSelect();
			case FormatContainerType.OptionGroup:
				return this.StartOptionGroup();
			case FormatContainerType.Option:
				return this.StartOption();
			case FormatContainerType.Text:
				return this.StartText();
			default:
				if (nodeType == FormatContainerType.Image)
				{
					this.StartEndImage();
					return false;
				}
				switch (nodeType)
				{
				case FormatContainerType.Root:
					return this.StartRoot();
				case FormatContainerType.Document:
					return this.StartDocument();
				case FormatContainerType.Fragment:
					return this.StartFragment();
				case FormatContainerType.Block:
					return this.StartBlock();
				case FormatContainerType.BlockQuote:
					return this.StartBlockQuote();
				case FormatContainerType.HorizontalLine:
					this.StartEndHorizontalLine();
					return false;
				case FormatContainerType.TableCaption:
					return this.StartTableCaption();
				case FormatContainerType.TableExtraContent:
					return this.StartTableExtraContent();
				case FormatContainerType.Table:
					return this.StartTable();
				case FormatContainerType.TableRow:
					return this.StartTableRow();
				case FormatContainerType.TableCell:
					return this.StartTableCell();
				case FormatContainerType.List:
					return this.StartList();
				case FormatContainerType.ListItem:
					return this.StartListItem();
				case FormatContainerType.Map:
					return this.StartMap();
				}
				break;
			}
			return true;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0008741E File Offset: 0x0008561E
		private bool ContinueCurrentLevel()
		{
			return this.ContinueText(this.currentOutputLevel.Node.BeginTextPosition, this.currentOutputLevel.Node.EndTextPosition);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00087448 File Offset: 0x00085648
		private void EndCurrentLevel()
		{
			FormatContainerType nodeType = this.currentOutputLevel.Node.NodeType;
			switch (nodeType)
			{
			case FormatContainerType.TableContainer:
				this.EndTableContainer();
				return;
			case FormatContainerType.TableDefinition:
				this.EndTableDefinition();
				return;
			case FormatContainerType.TableColumnGroup:
				this.EndTableColumnGroup();
				return;
			case FormatContainerType.TableColumn:
			case (FormatContainerType)11:
			case (FormatContainerType)12:
			case (FormatContainerType)13:
			case (FormatContainerType)14:
			case (FormatContainerType)15:
			case (FormatContainerType)16:
			case (FormatContainerType)17:
			case (FormatContainerType)21:
			case FormatContainerType.Area:
			case (FormatContainerType)23:
			case FormatContainerType.BaseFont:
			case (FormatContainerType)35:
				break;
			case FormatContainerType.Inline:
				this.EndInline();
				return;
			case FormatContainerType.HyperLink:
				this.EndHyperLink();
				return;
			case FormatContainerType.Bookmark:
				this.EndBookmark();
				return;
			case FormatContainerType.Form:
				this.EndForm();
				return;
			case FormatContainerType.FieldSet:
				this.EndFieldSet();
				return;
			case FormatContainerType.Label:
				this.EndLabel();
				return;
			case FormatContainerType.Input:
				this.EndInput();
				return;
			case FormatContainerType.Button:
				this.EndButton();
				return;
			case FormatContainerType.Legend:
				this.EndLegend();
				return;
			case FormatContainerType.TextArea:
				this.EndTextArea();
				return;
			case FormatContainerType.Select:
				this.EndSelect();
				return;
			case FormatContainerType.OptionGroup:
				this.EndOptionGroup();
				return;
			case FormatContainerType.Option:
				this.EndOption();
				return;
			case FormatContainerType.Text:
				this.EndText();
				break;
			default:
				switch (nodeType)
				{
				case FormatContainerType.Root:
					this.EndRoot();
					return;
				case FormatContainerType.Document:
					this.EndDocument();
					return;
				case FormatContainerType.Fragment:
					this.EndFragment();
					return;
				case FormatContainerType.Block:
					this.EndBlock();
					return;
				case FormatContainerType.BlockQuote:
					this.EndBlockQuote();
					return;
				case FormatContainerType.HorizontalLine:
				case (FormatContainerType)135:
				case (FormatContainerType)136:
				case (FormatContainerType)137:
				case (FormatContainerType)138:
				case (FormatContainerType)146:
				case (FormatContainerType)147:
				case (FormatContainerType)148:
				case (FormatContainerType)149:
				case (FormatContainerType)150:
					break;
				case FormatContainerType.TableCaption:
					this.EndTableCaption();
					return;
				case FormatContainerType.TableExtraContent:
					this.EndTableExtraContent();
					return;
				case FormatContainerType.Table:
					this.EndTable();
					return;
				case FormatContainerType.TableRow:
					this.EndTableRow();
					return;
				case FormatContainerType.TableCell:
					this.EndTableCell();
					return;
				case FormatContainerType.List:
					this.EndList();
					return;
				case FormatContainerType.ListItem:
					this.EndListItem();
					return;
				case FormatContainerType.Map:
					this.EndMap();
					return;
				default:
					return;
				}
				break;
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00087620 File Offset: 0x00085820
		private void PushFirstChild()
		{
			FormatNode firstChild = this.currentOutputLevel.Node.FirstChild;
			if (!firstChild.IsNull)
			{
				this.currentOutputLevel.State = FormatOutput.OutputState.Started;
				this.Push(firstChild);
				return;
			}
			if (this.currentOutputLevel.Node.IsText)
			{
				this.currentOutputLevel.State = FormatOutput.OutputState.Started;
				return;
			}
			this.currentOutputLevel.State = FormatOutput.OutputState.EndPending;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00087688 File Offset: 0x00085888
		private void PopPushNextSibling()
		{
			FormatNode nextSibling = this.currentOutputLevel.Node.NextSibling;
			this.Pop();
			this.currentOutputLevel.ChildIndex = this.currentOutputLevel.ChildIndex + 1;
			if (!nextSibling.IsNull)
			{
				this.Push(nextSibling);
				return;
			}
			this.currentOutputLevel.State = FormatOutput.OutputState.EndPending;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000876DC File Offset: 0x000858DC
		private void Push(FormatNode node)
		{
			if (this.outputStack == null)
			{
				this.outputStack = new FormatOutput.OutputStackEntry[32];
			}
			else if (this.outputStackTop == this.outputStack.Length)
			{
				if (this.outputStackTop >= 4096)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				FormatOutput.OutputStackEntry[] destinationArray = new FormatOutput.OutputStackEntry[this.outputStack.Length * 2];
				Array.Copy(this.outputStack, 0, destinationArray, 0, this.outputStackTop);
				this.outputStack = destinationArray;
			}
			this.outputStack[this.outputStackTop++] = this.currentOutputLevel;
			this.currentOutputLevel.Node = node;
			this.currentOutputLevel.State = FormatOutput.OutputState.NotStarted;
			this.currentOutputLevel.Index = this.currentOutputLevel.ChildIndex;
			this.currentOutputLevel.ChildIndex = 0;
			this.currentOutputLevel.PropertyUndoLevel = this.propertyState.ApplyProperties(node.FlagProperties, node.Properties, FormatStoreData.GlobalInheritanceMasks[node.InheritanceMaskIndex].FlagProperties, FormatStoreData.GlobalInheritanceMasks[node.InheritanceMaskIndex].PropertyMask);
			node.SetOnLeftEdge();
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0008780C File Offset: 0x00085A0C
		private void Pop()
		{
			if (this.outputStackTop != 0)
			{
				this.currentOutputLevel.Node.ResetOnLeftEdge();
				this.propertyState.UndoProperties(this.currentOutputLevel.PropertyUndoLevel);
				this.currentOutputLevel = this.outputStack[--this.outputStackTop];
			}
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0008786E File Offset: 0x00085A6E
		protected virtual bool StartRoot()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00087876 File Offset: 0x00085A76
		protected virtual void EndRoot()
		{
			this.EndBlockContainer();
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0008787E File Offset: 0x00085A7E
		protected virtual bool StartDocument()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00087886 File Offset: 0x00085A86
		protected virtual void EndDocument()
		{
			this.EndBlockContainer();
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0008788E File Offset: 0x00085A8E
		protected virtual bool StartFragment()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00087896 File Offset: 0x00085A96
		protected virtual void EndFragment()
		{
			this.EndBlockContainer();
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0008789E File Offset: 0x00085A9E
		protected virtual void StartEndBaseFont()
		{
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000878A0 File Offset: 0x00085AA0
		protected virtual bool StartBlock()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000878A8 File Offset: 0x00085AA8
		protected virtual void EndBlock()
		{
			this.EndBlockContainer();
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000878B0 File Offset: 0x00085AB0
		protected virtual bool StartBlockQuote()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000878B8 File Offset: 0x00085AB8
		protected virtual void EndBlockQuote()
		{
			this.EndBlockContainer();
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000878C0 File Offset: 0x00085AC0
		protected virtual bool StartTableContainer()
		{
			return true;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000878C3 File Offset: 0x00085AC3
		protected virtual void EndTableContainer()
		{
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000878C5 File Offset: 0x00085AC5
		protected virtual bool StartTableDefinition()
		{
			return true;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000878C8 File Offset: 0x00085AC8
		protected virtual void EndTableDefinition()
		{
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x000878CA File Offset: 0x00085ACA
		protected virtual bool StartTableColumnGroup()
		{
			return true;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000878CD File Offset: 0x00085ACD
		protected virtual void EndTableColumnGroup()
		{
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x000878CF File Offset: 0x00085ACF
		protected virtual void StartEndTableColumn()
		{
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x000878D1 File Offset: 0x00085AD1
		protected virtual bool StartTableCaption()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x000878D9 File Offset: 0x00085AD9
		protected virtual void EndTableCaption()
		{
			this.EndBlockContainer();
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000878E1 File Offset: 0x00085AE1
		protected virtual bool StartTableExtraContent()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000878E9 File Offset: 0x00085AE9
		protected virtual void EndTableExtraContent()
		{
			this.EndBlockContainer();
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x000878F1 File Offset: 0x00085AF1
		protected virtual bool StartTable()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000878F9 File Offset: 0x00085AF9
		protected virtual void EndTable()
		{
			this.EndBlockContainer();
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00087901 File Offset: 0x00085B01
		protected virtual bool StartTableRow()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00087909 File Offset: 0x00085B09
		protected virtual void EndTableRow()
		{
			this.EndBlockContainer();
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00087911 File Offset: 0x00085B11
		protected virtual bool StartTableCell()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00087919 File Offset: 0x00085B19
		protected virtual void EndTableCell()
		{
			this.EndBlockContainer();
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00087921 File Offset: 0x00085B21
		protected virtual bool StartList()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00087929 File Offset: 0x00085B29
		protected virtual void EndList()
		{
			this.EndBlockContainer();
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00087931 File Offset: 0x00085B31
		protected virtual bool StartListItem()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00087939 File Offset: 0x00085B39
		protected virtual void EndListItem()
		{
			this.EndBlockContainer();
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00087941 File Offset: 0x00085B41
		protected virtual bool StartHyperLink()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00087949 File Offset: 0x00085B49
		protected virtual void EndHyperLink()
		{
			this.EndInlineContainer();
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00087951 File Offset: 0x00085B51
		protected virtual bool StartBookmark()
		{
			return true;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00087954 File Offset: 0x00085B54
		protected virtual void EndBookmark()
		{
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00087956 File Offset: 0x00085B56
		protected virtual void StartEndImage()
		{
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00087958 File Offset: 0x00085B58
		protected virtual void StartEndHorizontalLine()
		{
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0008795A File Offset: 0x00085B5A
		protected virtual bool StartInline()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00087962 File Offset: 0x00085B62
		protected virtual void EndInline()
		{
			this.EndInlineContainer();
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0008796A File Offset: 0x00085B6A
		protected virtual bool StartMap()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00087972 File Offset: 0x00085B72
		protected virtual void EndMap()
		{
			this.EndBlockContainer();
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0008797A File Offset: 0x00085B7A
		protected virtual void StartEndArea()
		{
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0008797C File Offset: 0x00085B7C
		protected virtual bool StartForm()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00087984 File Offset: 0x00085B84
		protected virtual void EndForm()
		{
			this.EndInlineContainer();
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0008798C File Offset: 0x00085B8C
		protected virtual bool StartFieldSet()
		{
			return this.StartBlockContainer();
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00087994 File Offset: 0x00085B94
		protected virtual void EndFieldSet()
		{
			this.EndBlockContainer();
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0008799C File Offset: 0x00085B9C
		protected virtual bool StartLabel()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000879A4 File Offset: 0x00085BA4
		protected virtual void EndLabel()
		{
			this.EndInlineContainer();
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000879AC File Offset: 0x00085BAC
		protected virtual bool StartInput()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000879B4 File Offset: 0x00085BB4
		protected virtual void EndInput()
		{
			this.EndInlineContainer();
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000879BC File Offset: 0x00085BBC
		protected virtual bool StartButton()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000879C4 File Offset: 0x00085BC4
		protected virtual void EndButton()
		{
			this.EndInlineContainer();
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000879CC File Offset: 0x00085BCC
		protected virtual bool StartLegend()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x000879D4 File Offset: 0x00085BD4
		protected virtual void EndLegend()
		{
			this.EndInlineContainer();
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x000879DC File Offset: 0x00085BDC
		protected virtual bool StartTextArea()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x000879E4 File Offset: 0x00085BE4
		protected virtual void EndTextArea()
		{
			this.EndInlineContainer();
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000879EC File Offset: 0x00085BEC
		protected virtual bool StartSelect()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000879F4 File Offset: 0x00085BF4
		protected virtual void EndSelect()
		{
			this.EndInlineContainer();
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000879FC File Offset: 0x00085BFC
		protected virtual bool StartOptionGroup()
		{
			return true;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000879FF File Offset: 0x00085BFF
		protected virtual void EndOptionGroup()
		{
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00087A01 File Offset: 0x00085C01
		protected virtual bool StartOption()
		{
			return true;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00087A04 File Offset: 0x00085C04
		protected virtual void EndOption()
		{
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00087A06 File Offset: 0x00085C06
		protected virtual bool StartText()
		{
			return this.StartInlineContainer();
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00087A0E File Offset: 0x00085C0E
		protected virtual bool ContinueText(uint beginTextPosition, uint endTextPosition)
		{
			return true;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00087A11 File Offset: 0x00085C11
		protected virtual void EndText()
		{
			this.EndInlineContainer();
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00087A19 File Offset: 0x00085C19
		private static string Indent(int level)
		{
			return "                                                  ".Substring(0, Math.Min("                                                  ".Length, level * 2));
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00087A38 File Offset: 0x00085C38
		protected virtual bool StartBlockContainer()
		{
			return true;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00087A3B File Offset: 0x00085C3B
		protected virtual void EndBlockContainer()
		{
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00087A3D File Offset: 0x00085C3D
		protected virtual bool StartInlineContainer()
		{
			return true;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00087A40 File Offset: 0x00085C40
		protected virtual void EndInlineContainer()
		{
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00087A42 File Offset: 0x00085C42
		protected virtual void Dispose(bool disposing)
		{
			this.currentOutputLevel.Node = FormatNode.Null;
			this.outputStack = null;
			this.formatStore = null;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00087A62 File Offset: 0x00085C62
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400134A RID: 4938
		private SourceFormat sourceFormat;

		// Token: 0x0400134B RID: 4939
		private string comment;

		// Token: 0x0400134C RID: 4940
		private FormatStore formatStore;

		// Token: 0x0400134D RID: 4941
		private FormatNode rootNode;

		// Token: 0x0400134E RID: 4942
		private FormatOutput.OutputStackEntry currentOutputLevel;

		// Token: 0x0400134F RID: 4943
		private FormatOutput.OutputStackEntry[] outputStack;

		// Token: 0x04001350 RID: 4944
		private int outputStackTop;

		// Token: 0x04001351 RID: 4945
		protected ScratchBuffer scratchBuffer;

		// Token: 0x04001352 RID: 4946
		protected ScratchBuffer scratchValueBuffer;

		// Token: 0x04001353 RID: 4947
		private PropertyState propertyState = new PropertyState();

		// Token: 0x020001BE RID: 446
		private enum OutputState : byte
		{
			// Token: 0x04001355 RID: 4949
			NotStarted,
			// Token: 0x04001356 RID: 4950
			Started,
			// Token: 0x04001357 RID: 4951
			EndPending,
			// Token: 0x04001358 RID: 4952
			Ended
		}

		// Token: 0x020001BF RID: 447
		private struct OutputStackEntry
		{
			// Token: 0x04001359 RID: 4953
			public FormatOutput.OutputState State;

			// Token: 0x0400135A RID: 4954
			public FormatNode Node;

			// Token: 0x0400135B RID: 4955
			public int Index;

			// Token: 0x0400135C RID: 4956
			public int ChildIndex;

			// Token: 0x0400135D RID: 4957
			public int PropertyUndoLevel;
		}
	}
}
