using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020001BA RID: 442
	internal abstract class FormatConverter : IProgressMonitor
	{
		// Token: 0x060012B6 RID: 4790 RVA: 0x00084FD8 File Offset: 0x000831D8
		internal FormatConverter(Stream formatConverterTraceStream)
		{
			this.Store = new FormatStore();
			this.BuildStack = new FormatConverter.BuildStackEntry[16];
			this.ContainerStyleBuildHelper = new StyleBuildHelper(this.Store);
			this.StyleBuildHelper = new StyleBuildHelper(this.Store);
			this.MultiValueBuildHelper = new MultiValueBuildHelper(this.Store);
			this.FontFaceDictionary = new Dictionary<string, PropertyValue>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00085048 File Offset: 0x00083248
		internal FormatConverter(FormatStore formatStore, Stream formatConverterTraceStream)
		{
			this.Store = formatStore;
			this.BuildStack = new FormatConverter.BuildStackEntry[16];
			this.ContainerStyleBuildHelper = new StyleBuildHelper(this.Store);
			this.StyleBuildHelper = new StyleBuildHelper(this.Store);
			this.MultiValueBuildHelper = new MultiValueBuildHelper(this.Store);
			this.FontFaceDictionary = new Dictionary<string, PropertyValue>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000850B2 File Offset: 0x000832B2
		public FormatConverterContainer Root
		{
			get
			{
				return new FormatConverterContainer(this, 0);
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000850BB File Offset: 0x000832BB
		public FormatConverterContainer Last
		{
			get
			{
				return new FormatConverterContainer(this, this.EmptyContainer ? this.BuildStackTop : (this.BuildStackTop - 1));
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x000850DB File Offset: 0x000832DB
		public FormatNode LastNode
		{
			get
			{
				return new FormatNode(this.Store, this.LastNodeInternal);
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x000850EE File Offset: 0x000832EE
		public FormatConverterContainer LastNonEmpty
		{
			get
			{
				return new FormatConverterContainer(this, this.BuildStackTop - 1);
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x000850FE File Offset: 0x000832FE
		public bool EndOfFile
		{
			get
			{
				return this.endOfFile;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x00085106 File Offset: 0x00083306
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x0008510E File Offset: 0x0008330E
		protected bool MustFlush
		{
			get
			{
				return this.mustFlush;
			}
			set
			{
				this.mustFlush = value;
			}
		}

		// Token: 0x060012BF RID: 4799
		public abstract void Run();

		// Token: 0x060012C0 RID: 4800 RVA: 0x00085118 File Offset: 0x00083318
		public FormatConverterContainer OpenContainer(FormatContainerType nodeType, bool empty)
		{
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.EmptyContainer ? this.BuildStackTop : (this.BuildStackTop - 1));
			}
			if (this.EmptyContainer)
			{
				this.PrepareToCloseContainer(this.BuildStackTop);
			}
			int level = this.PushContainer(nodeType, empty, 1);
			return new FormatConverterContainer(this, level);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00085170 File Offset: 0x00083370
		public FormatConverterContainer OpenContainer(FormatContainerType nodeType, bool empty, int inheritanceMaskIndex, FormatStyle baseStyle, HtmlNameIndex tagName)
		{
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.EmptyContainer ? this.BuildStackTop : (this.BuildStackTop - 1));
			}
			if (this.EmptyContainer)
			{
				this.PrepareToCloseContainer(this.BuildStackTop);
			}
			int num = this.PushContainer(nodeType, empty, inheritanceMaskIndex);
			if (!baseStyle.IsNull)
			{
				baseStyle.AddRef();
				this.ContainerStyleBuildHelper.AddStyle(10, baseStyle.Handle);
			}
			this.BuildStack[num].TagName = tagName;
			return new FormatConverterContainer(this, num);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00085200 File Offset: 0x00083400
		public FormatConverterContainer OpenContainer(FormatContainerType nodeType, bool empty, int inheritanceMaskIndex, FormatStyle baseStyle, HtmlTagIndex tagIndex)
		{
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.EmptyContainer ? this.BuildStackTop : (this.BuildStackTop - 1));
			}
			if (this.EmptyContainer)
			{
				this.PrepareToCloseContainer(this.BuildStackTop);
			}
			int num = this.PushContainer(nodeType, empty, inheritanceMaskIndex);
			if (!baseStyle.IsNull)
			{
				baseStyle.AddRef();
				this.ContainerStyleBuildHelper.AddStyle(10, baseStyle.Handle);
			}
			this.BuildStack[num].TagIndex = tagIndex;
			return new FormatConverterContainer(this, num);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00085290 File Offset: 0x00083490
		public void OpenTextContainer()
		{
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.EmptyContainer ? this.BuildStackTop : (this.BuildStackTop - 1));
			}
			if (this.EmptyContainer)
			{
				this.PrepareToCloseContainer(this.BuildStackTop);
			}
			this.PrepareToAddText();
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x000852E0 File Offset: 0x000834E0
		public void CloseContainer()
		{
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.EmptyContainer ? this.BuildStackTop : (this.BuildStackTop - 1));
			}
			if (this.EmptyContainer)
			{
				this.PrepareToCloseContainer(this.BuildStackTop);
			}
			this.PopContainer();
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00085330 File Offset: 0x00083530
		public void CloseOverlappingContainer(int countLevelsToKeepOpen)
		{
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.EmptyContainer ? this.BuildStackTop : (this.BuildStackTop - 1));
			}
			if (this.EmptyContainer)
			{
				this.PrepareToCloseContainer(this.BuildStackTop);
			}
			this.PopContainer(this.BuildStackTop - 1 - countLevelsToKeepOpen);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00085388 File Offset: 0x00083588
		public void CloseAllContainersAndSetEOF()
		{
			while (this.BuildStackTop > 1)
			{
				this.CloseContainer();
			}
			this.Store.GetNode(this.BuildStack[0].Node).PrepareToClose(this.Store.CurrentTextPosition);
			this.mustFlush = true;
			this.endOfFile = true;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x000853E4 File Offset: 0x000835E4
		public void AddNonSpaceText(char[] buffer, int offset, int count)
		{
			this.PrepareToAddText();
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.BuildStackTop);
			}
			this.newLine = false;
			if (this.textQuotingExpected)
			{
				if (buffer[offset] == '>')
				{
					do
					{
						this.Store.AddText(buffer, offset, 1);
						offset++;
						count--;
					}
					while (count != 0 && buffer[offset] == '>');
					if (count == 0)
					{
						return;
					}
				}
				this.Store.SetTextBoundary();
				this.textQuotingExpected = false;
			}
			this.Store.AddText(buffer, offset, count);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00085467 File Offset: 0x00083667
		public void AddSpace(int count)
		{
			this.PrepareToAddText();
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.BuildStackTop);
			}
			this.Store.AddSpace(count);
			this.newLine = false;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00085498 File Offset: 0x00083698
		public void AddLineBreak(int count)
		{
			this.PrepareToAddText();
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.BuildStackTop);
			}
			if (!this.newLine)
			{
				this.Store.AddLineBreak(1);
				this.Store.SetTextBoundary();
				if (count > 1)
				{
					this.Store.AddLineBreak(count - 1);
				}
				this.newLine = true;
				this.textQuotingExpected = true;
				return;
			}
			this.Store.AddLineBreak(count);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0008550B File Offset: 0x0008370B
		public void AddNbsp(int count)
		{
			this.PrepareToAddText();
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.BuildStackTop);
			}
			this.Store.AddNbsp(count);
			this.newLine = false;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0008553A File Offset: 0x0008373A
		public void AddTabulation(int count)
		{
			this.PrepareToAddText();
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.BuildStackTop);
			}
			this.Store.AddTabulation(count);
			this.newLine = false;
			this.textQuotingExpected = false;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00085570 File Offset: 0x00083770
		public StringValue RegisterStringValue(bool isStatic, string value)
		{
			return this.Store.AllocateStringValue(isStatic, value);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00085580 File Offset: 0x00083780
		public StringValue RegisterStringValue(bool isStatic, string str, int offset, int count)
		{
			string value = str;
			if (offset != 0 || count != str.Length)
			{
				value = str.Substring(offset, count);
			}
			return this.Store.AllocateStringValue(isStatic, value);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000855B3 File Offset: 0x000837B3
		public StringValue RegisterStringValue(bool isStatic, BufferString value)
		{
			return this.Store.AllocateStringValue(isStatic, value.ToString());
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000855CE File Offset: 0x000837CE
		public PropertyValue RegisterFaceName(bool isStatic, BufferString value)
		{
			if (value.Length == 0)
			{
				return PropertyValue.Null;
			}
			return this.RegisterFaceName(isStatic, value.ToString());
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x000855F4 File Offset: 0x000837F4
		public PropertyValue RegisterFaceName(bool isStatic, string faceName)
		{
			if (string.IsNullOrEmpty(faceName))
			{
				return PropertyValue.Null;
			}
			PropertyValue propertyValue;
			if (this.FontFaceDictionary.TryGetValue(faceName, out propertyValue))
			{
				if (propertyValue.IsString)
				{
					this.Store.AddRefValue(propertyValue);
				}
				return propertyValue;
			}
			StringValue stringValue = this.RegisterStringValue(isStatic, faceName);
			propertyValue = stringValue.PropertyValue;
			if (this.FontFaceDictionary.Count < 100)
			{
				stringValue.AddRef();
				this.FontFaceDictionary.Add(faceName, propertyValue);
			}
			return propertyValue;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0008566C File Offset: 0x0008386C
		public MultiValue RegisterMultiValue(bool isStatic, out MultiValueBuilder builder)
		{
			MultiValue result = this.Store.AllocateMultiValue(isStatic);
			builder = new MultiValueBuilder(this, result.Handle);
			return result;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0008569C File Offset: 0x0008389C
		public FormatStyle RegisterStyle(bool isStatic, out StyleBuilder builder)
		{
			FormatStyle result = this.Store.AllocateStyle(isStatic);
			builder = new StyleBuilder(this, result.Handle);
			return result;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000856CA File Offset: 0x000838CA
		public FormatStyle GetStyle(int styleHandle)
		{
			return this.Store.GetStyle(styleHandle);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x000856D8 File Offset: 0x000838D8
		public StringValue GetStringValue(PropertyValue pv)
		{
			return this.Store.GetStringValue(pv);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x000856E6 File Offset: 0x000838E6
		public MultiValue GetMultiValue(PropertyValue pv)
		{
			return this.Store.GetMultiValue(pv);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000856F4 File Offset: 0x000838F4
		public void ReleasePropertyValue(PropertyValue pv)
		{
			this.Store.ReleaseValue(pv);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00085702 File Offset: 0x00083902
		void IProgressMonitor.ReportProgress()
		{
			this.madeProgress = true;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0008570C File Offset: 0x0008390C
		internal FormatNode InitializeDocument()
		{
			this.Initialize();
			return this.OpenContainer(FormatContainerType.Document, false).Node;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00085734 File Offset: 0x00083934
		internal FormatNode InitializeFragment()
		{
			this.Initialize();
			FormatConverterContainer formatConverterContainer = this.OpenContainer(FormatContainerType.Fragment, false);
			this.OpenContainer(FormatContainerType.PropertyContainer, false);
			this.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontFace, this.RegisterFaceName(false, "Times New Roman"));
			this.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Points, 11));
			return formatConverterContainer.Node;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0008579C File Offset: 0x0008399C
		protected void CloseContainer(FormatContainerType containerType)
		{
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.EmptyContainer ? this.BuildStackTop : (this.BuildStackTop - 1));
			}
			if (this.EmptyContainer)
			{
				this.PrepareToCloseContainer(this.BuildStackTop);
			}
			for (int i = this.BuildStackTop - 1; i > 0; i--)
			{
				if (this.BuildStack[i].Type == containerType)
				{
					this.PopContainer(i);
					return;
				}
			}
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00085814 File Offset: 0x00083A14
		protected void CloseContainer(HtmlNameIndex tagName)
		{
			if (!this.ContainerFlushed)
			{
				this.FlushContainer(this.EmptyContainer ? this.BuildStackTop : (this.BuildStackTop - 1));
			}
			if (this.EmptyContainer)
			{
				this.PrepareToCloseContainer(this.BuildStackTop);
			}
			for (int i = this.BuildStackTop - 1; i > 0; i--)
			{
				if (this.BuildStack[i].TagName == tagName)
				{
					this.PopContainer(i);
					return;
				}
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0008588C File Offset: 0x00083A8C
		protected FormatNode CreateNode(FormatContainerType type)
		{
			FormatNode result = this.Store.AllocateNode(type);
			result.EndTextPosition = result.BeginTextPosition;
			result.SetOutOfOrder();
			return result;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000858BC File Offset: 0x00083ABC
		protected virtual FormatContainerType FixContainerType(FormatContainerType type, StyleBuildHelper styleBuilderWithContainerProperties)
		{
			return type;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000858BF File Offset: 0x00083ABF
		protected virtual FormatNode GetParentForNewNode(FormatNode node, FormatNode defaultParent, int stackPos, out int propContainerInheritanceStopLevel)
		{
			propContainerInheritanceStopLevel = this.DefaultPropContainerInheritanceStopLevel(stackPos);
			return defaultParent;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x000858CC File Offset: 0x00083ACC
		protected int DefaultPropContainerInheritanceStopLevel(int stackPos)
		{
			int num = stackPos - 1;
			while (num >= 0 && this.BuildStack[num].Node == 0)
			{
				num--;
			}
			return num + 1;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x000858FD File Offset: 0x00083AFD
		private static string Indent(int level)
		{
			return "                                                  ".Substring(0, Math.Min("                                                  ".Length, level * 2));
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0008591C File Offset: 0x00083B1C
		private void Initialize()
		{
			this.BuildStackTop = 0;
			this.ContainerStyleBuildHelper.Clean();
			this.StyleBuildHelper.Clean();
			this.StyleBuildHelperLocked = false;
			this.MultiValueBuildHelper.Cancel();
			this.MultiValueBuildHelperLocked = false;
			this.FontFaceDictionary.Clear();
			this.LastNodeInternal = this.Store.RootNode.Handle;
			this.BuildStack[this.BuildStackTop].Type = FormatContainerType.Root;
			this.BuildStack[this.BuildStackTop].Node = this.Store.RootNode.Handle;
			this.BuildStackTop++;
			this.EmptyContainer = false;
			this.ContainerFlushed = true;
			this.mustFlush = false;
			this.endOfFile = false;
			this.newLine = true;
			this.textQuotingExpected = true;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000859FF File Offset: 0x00083BFF
		public void AddMarkupText(char[] buffer, int offset, int count)
		{
			this.Store.AddMarkupText(buffer, offset, count);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00085A10 File Offset: 0x00083C10
		private void PrepareToAddText()
		{
			if (!this.EmptyContainer || !this.BuildStack[this.BuildStackTop].IsText)
			{
				if (!this.ContainerFlushed)
				{
					this.FlushContainer(this.EmptyContainer ? this.BuildStackTop : (this.BuildStackTop - 1));
				}
				if (this.EmptyContainer)
				{
					this.PrepareToCloseContainer(this.BuildStackTop);
				}
				this.PushContainer(FormatContainerType.Text, true, 5);
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00085A84 File Offset: 0x00083C84
		private void FlushContainer(int stackPos)
		{
			FormatContainerType formatContainerType = this.FixContainerType(this.BuildStack[stackPos].Type, this.ContainerStyleBuildHelper);
			if (formatContainerType != this.BuildStack[stackPos].Type)
			{
				this.BuildStack[stackPos].Type = formatContainerType;
			}
			this.ContainerStyleBuildHelper.GetPropertyList(out this.BuildStack[stackPos].Properties, out this.BuildStack[stackPos].FlagProperties, out this.BuildStack[stackPos].PropertyMask);
			if (!this.BuildStack[stackPos].IsPropertyContainerOrNull)
			{
				if (!this.newLine && (byte)(this.BuildStack[stackPos].Type & FormatContainerType.BlockFlag) != 0)
				{
					this.Store.AddBlockBoundary();
					this.newLine = true;
					this.textQuotingExpected = true;
				}
				FormatNode formatNode;
				if (formatContainerType == FormatContainerType.Document)
				{
					formatNode = this.Store.AllocateNode(this.BuildStack[stackPos].Type, 0U);
				}
				else
				{
					formatNode = this.Store.AllocateNode(this.BuildStack[stackPos].Type);
				}
				formatNode.SetOnRightEdge();
				if ((byte)(this.BuildStack[stackPos].Type & FormatContainerType.InlineObjectFlag) != 0)
				{
					this.Store.AddInlineObject();
				}
				FormatNode node = this.Store.GetNode(this.LastNodeInternal);
				int num;
				this.GetParentForNewNode(formatNode, node, stackPos, out num).AppendChild(formatNode);
				this.BuildStack[stackPos].Node = formatNode.Handle;
				this.LastNodeInternal = formatNode.Handle;
				FlagProperties flagProperties2;
				Property[] properties;
				PropertyBitMask propertyMask;
				if (num < stackPos)
				{
					FlagProperties flagProperties = FlagProperties.AllOn;
					PropertyBitMask propertyBitMask = PropertyBitMask.AllOn;
					int num2 = stackPos;
					while (num2 >= num && (!flagProperties.IsClear || !propertyBitMask.IsClear))
					{
						if (num2 == stackPos || this.BuildStack[num2].Type == FormatContainerType.PropertyContainer)
						{
							flagProperties2 = (this.BuildStack[num2].FlagProperties & flagProperties);
							this.ContainerStyleBuildHelper.AddProperties(11, flagProperties2, propertyBitMask, this.BuildStack[num2].Properties);
							flagProperties &= ~this.BuildStack[num2].FlagProperties;
							propertyBitMask &= ~this.BuildStack[num2].PropertyMask;
							flagProperties &= FormatStoreData.GlobalInheritanceMasks[this.BuildStack[num2].InheritanceMaskIndex].FlagProperties;
							propertyBitMask &= FormatStoreData.GlobalInheritanceMasks[this.BuildStack[num2].InheritanceMaskIndex].PropertyMask;
						}
						num2--;
					}
					this.ContainerStyleBuildHelper.GetPropertyList(out properties, out flagProperties2, out propertyMask);
				}
				else
				{
					flagProperties2 = this.BuildStack[stackPos].FlagProperties;
					propertyMask = this.BuildStack[stackPos].PropertyMask;
					properties = this.BuildStack[stackPos].Properties;
					if (properties != null)
					{
						for (int i = 0; i < properties.Length; i++)
						{
							if (properties[i].Value.IsRefCountedHandle)
							{
								this.Store.AddRefValue(properties[i].Value);
							}
						}
					}
				}
				formatNode.SetProps(flagProperties2, propertyMask, properties, this.BuildStack[stackPos].InheritanceMaskIndex);
			}
			this.ContainerStyleBuildHelper.Clean();
			this.ContainerFlushed = true;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00085E18 File Offset: 0x00084018
		private int PushContainer(FormatContainerType type, bool empty, int inheritanceMaskIndex)
		{
			int buildStackTop = this.BuildStackTop;
			if (buildStackTop == this.BuildStack.Length)
			{
				if (this.BuildStack.Length >= 4096)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				int num = (2048 > this.BuildStack.Length) ? (this.BuildStack.Length * 2) : 4096;
				FormatConverter.BuildStackEntry[] array = new FormatConverter.BuildStackEntry[num];
				Array.Copy(this.BuildStack, 0, array, 0, this.BuildStackTop);
				this.BuildStack = array;
			}
			this.Store.SetTextBoundary();
			this.BuildStack[buildStackTop].Type = type;
			this.BuildStack[buildStackTop].TagName = HtmlNameIndex._NOTANAME;
			this.BuildStack[buildStackTop].InheritanceMaskIndex = inheritanceMaskIndex;
			this.BuildStack[buildStackTop].TagIndex = HtmlTagIndex._NULL;
			this.BuildStack[buildStackTop].FlagProperties.ClearAll();
			this.BuildStack[buildStackTop].PropertyMask.ClearAll();
			this.BuildStack[buildStackTop].Properties = null;
			this.BuildStack[buildStackTop].Node = 0;
			if (!empty)
			{
				this.BuildStackTop++;
			}
			this.EmptyContainer = empty;
			this.ContainerFlushed = false;
			return buildStackTop;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00085F59 File Offset: 0x00084159
		private void PopContainer()
		{
			this.PrepareToCloseContainer(this.BuildStackTop - 1);
			this.BuildStackTop--;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00085F77 File Offset: 0x00084177
		private void PopContainer(int level)
		{
			this.PrepareToCloseContainer(level);
			Array.Copy(this.BuildStack, level + 1, this.BuildStack, level, this.BuildStackTop - level - 1);
			this.BuildStackTop--;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00085FB0 File Offset: 0x000841B0
		private void PrepareToCloseContainer(int stackPosition)
		{
			if (this.BuildStack[stackPosition].Properties != null)
			{
				for (int i = 0; i < this.BuildStack[stackPosition].Properties.Length; i++)
				{
					if (this.BuildStack[stackPosition].Properties[i].Value.IsRefCountedHandle)
					{
						this.Store.ReleaseValue(this.BuildStack[stackPosition].Properties[i].Value);
					}
				}
				this.BuildStack[stackPosition].Properties = null;
			}
			if (this.BuildStack[stackPosition].Node != 0)
			{
				FormatNode node = this.Store.GetNode(this.BuildStack[stackPosition].Node);
				if (!this.newLine && (byte)(node.NodeType & FormatContainerType.BlockFlag) != 0)
				{
					this.Store.AddBlockBoundary();
					this.newLine = true;
					this.textQuotingExpected = true;
				}
				node.PrepareToClose(this.Store.CurrentTextPosition);
				if (!node.Parent.IsNull && node.Parent.NodeType == FormatContainerType.TableContainer)
				{
					node.Parent.PrepareToClose(this.Store.CurrentTextPosition);
				}
				if (this.BuildStack[stackPosition].Node == this.LastNodeInternal)
				{
					for (int j = stackPosition - 1; j >= 0; j--)
					{
						if (this.BuildStack[j].Node != 0)
						{
							this.LastNodeInternal = this.BuildStack[j].Node;
							break;
						}
					}
				}
			}
			this.Store.SetTextBoundary();
			this.EmptyContainer = false;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00086168 File Offset: 0x00084368
		public virtual FormatStore ConvertToStore()
		{
			long num = 0L;
			while (!this.endOfFile)
			{
				this.Run();
				if (this.madeProgress)
				{
					this.madeProgress = false;
					num = 0L;
				}
				else
				{
					long num2 = 200000L;
					long num3 = num;
					num = num3 + 1L;
					if (num2 == num3)
					{
						throw new TextConvertersException(TextConvertersStrings.TooManyIterationsToProduceOutput);
					}
				}
			}
			return this.Store;
		}

		// Token: 0x04001324 RID: 4900
		internal FormatStore Store;

		// Token: 0x04001325 RID: 4901
		internal FormatConverter.BuildStackEntry[] BuildStack;

		// Token: 0x04001326 RID: 4902
		internal int BuildStackTop;

		// Token: 0x04001327 RID: 4903
		internal int LastNodeInternal;

		// Token: 0x04001328 RID: 4904
		internal bool EmptyContainer;

		// Token: 0x04001329 RID: 4905
		internal bool ContainerFlushed;

		// Token: 0x0400132A RID: 4906
		internal StyleBuildHelper ContainerStyleBuildHelper;

		// Token: 0x0400132B RID: 4907
		internal StyleBuildHelper StyleBuildHelper;

		// Token: 0x0400132C RID: 4908
		internal bool StyleBuildHelperLocked;

		// Token: 0x0400132D RID: 4909
		internal MultiValueBuildHelper MultiValueBuildHelper;

		// Token: 0x0400132E RID: 4910
		internal bool MultiValueBuildHelperLocked;

		// Token: 0x0400132F RID: 4911
		internal Dictionary<string, PropertyValue> FontFaceDictionary;

		// Token: 0x04001330 RID: 4912
		protected bool madeProgress;

		// Token: 0x04001331 RID: 4913
		private bool mustFlush;

		// Token: 0x04001332 RID: 4914
		private bool endOfFile;

		// Token: 0x04001333 RID: 4915
		private bool newLine;

		// Token: 0x04001334 RID: 4916
		private bool textQuotingExpected;

		// Token: 0x020001BB RID: 443
		internal struct BuildStackEntry
		{
			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x060012EA RID: 4842 RVA: 0x000861BB File Offset: 0x000843BB
			public bool IsText
			{
				get
				{
					return this.Type == FormatContainerType.Text;
				}
			}

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x060012EB RID: 4843 RVA: 0x000861C7 File Offset: 0x000843C7
			public bool IsPropertyContainer
			{
				get
				{
					return this.Type == FormatContainerType.PropertyContainer;
				}
			}

			// Token: 0x17000529 RID: 1321
			// (get) Token: 0x060012EC RID: 4844 RVA: 0x000861D3 File Offset: 0x000843D3
			public bool IsPropertyContainerOrNull
			{
				get
				{
					return this.Type == FormatContainerType.PropertyContainer || this.Type == FormatContainerType.Null;
				}
			}

			// Token: 0x1700052A RID: 1322
			// (get) Token: 0x060012ED RID: 4845 RVA: 0x000861EA File Offset: 0x000843EA
			// (set) Token: 0x060012EE RID: 4846 RVA: 0x000861F2 File Offset: 0x000843F2
			public FormatContainerType NodeType
			{
				get
				{
					return this.Type;
				}
				set
				{
					this.Type = value;
				}
			}

			// Token: 0x060012EF RID: 4847 RVA: 0x000861FB File Offset: 0x000843FB
			public void Clean()
			{
				this = default(FormatConverter.BuildStackEntry);
			}

			// Token: 0x04001335 RID: 4917
			internal FormatContainerType Type;

			// Token: 0x04001336 RID: 4918
			internal HtmlNameIndex TagName;

			// Token: 0x04001337 RID: 4919
			internal HtmlTagIndex TagIndex;

			// Token: 0x04001338 RID: 4920
			internal int Node;

			// Token: 0x04001339 RID: 4921
			internal int InheritanceMaskIndex;

			// Token: 0x0400133A RID: 4922
			internal Property[] Properties;

			// Token: 0x0400133B RID: 4923
			internal FlagProperties FlagProperties;

			// Token: 0x0400133C RID: 4924
			internal PropertyBitMask PropertyMask;
		}
	}
}
