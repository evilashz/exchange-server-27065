using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002B7 RID: 695
	internal struct FormatConverterContainer
	{
		// Token: 0x06001B92 RID: 7058 RVA: 0x000D4B12 File Offset: 0x000D2D12
		internal FormatConverterContainer(FormatConverter converter, int level)
		{
			this.converter = converter;
			this.level = level;
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000D4B22 File Offset: 0x000D2D22
		public bool IsNull
		{
			get
			{
				return this.converter == null;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000D4B2D File Offset: 0x000D2D2D
		public FormatContainerType Type
		{
			get
			{
				if (!this.IsNull)
				{
					return this.converter.BuildStack[this.level].Type;
				}
				return FormatContainerType.Null;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x000D4B54 File Offset: 0x000D2D54
		public HtmlNameIndex TagName
		{
			get
			{
				if (!this.IsNull)
				{
					return this.converter.BuildStack[this.level].TagName;
				}
				return HtmlNameIndex._NOTANAME;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x000D4B7B File Offset: 0x000D2D7B
		public FormatConverterContainer Parent
		{
			get
			{
				if (this.level > 1)
				{
					return new FormatConverterContainer(this.converter, this.level - 1);
				}
				return FormatConverterContainer.Null;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000D4B9F File Offset: 0x000D2D9F
		public FormatConverterContainer Child
		{
			get
			{
				if (this.level != this.converter.BuildStackTop - 1)
				{
					return new FormatConverterContainer(this.converter, this.level + 1);
				}
				return FormatConverterContainer.Null;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x000D4BCF File Offset: 0x000D2DCF
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x000D4BEC File Offset: 0x000D2DEC
		public FlagProperties FlagProperties
		{
			get
			{
				return this.converter.BuildStack[this.level].FlagProperties;
			}
			set
			{
				this.converter.BuildStack[this.level].FlagProperties = value;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x000D4C0A File Offset: 0x000D2E0A
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x000D4C27 File Offset: 0x000D2E27
		public PropertyBitMask PropertyMask
		{
			get
			{
				return this.converter.BuildStack[this.level].PropertyMask;
			}
			set
			{
				this.converter.BuildStack[this.level].PropertyMask = value;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x000D4C45 File Offset: 0x000D2E45
		public Property[] Properties
		{
			get
			{
				return this.converter.BuildStack[this.level].Properties;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x000D4C62 File Offset: 0x000D2E62
		public FormatNode Node
		{
			get
			{
				return new FormatNode(this.converter.Store, this.converter.BuildStack[this.level].Node);
			}
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000D4C8F File Offset: 0x000D2E8F
		public static bool operator ==(FormatConverterContainer x, FormatConverterContainer y)
		{
			return x.converter == y.converter && x.level == y.level;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000D4CB3 File Offset: 0x000D2EB3
		public static bool operator !=(FormatConverterContainer x, FormatConverterContainer y)
		{
			return x.converter != y.converter || x.level != y.level;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000D4CDC File Offset: 0x000D2EDC
		public void SetProperty(PropertyPrecedence propertyPrecedence, PropertyId propertyId, PropertyValue value)
		{
			if (value.IsString)
			{
				this.converter.GetStringValue(value).AddRef();
			}
			else if (value.IsMultiValue)
			{
				this.converter.GetMultiValue(value).AddRef();
			}
			this.converter.ContainerStyleBuildHelper.SetProperty((int)propertyPrecedence, propertyId, value);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x000D4D38 File Offset: 0x000D2F38
		public void SetProperties(PropertyPrecedence propertyPrecedence, Property[] properties, int propertyCount)
		{
			for (int i = 0; i < propertyCount; i++)
			{
				this.SetProperty(propertyPrecedence, properties[i].Id, properties[i].Value);
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x000D4D70 File Offset: 0x000D2F70
		public void SetStringProperty(PropertyPrecedence propertyPrecedence, PropertyId propertyId, StringValue value)
		{
			value.AddRef();
			this.converter.ContainerStyleBuildHelper.SetProperty((int)propertyPrecedence, propertyId, value.PropertyValue);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000D4D94 File Offset: 0x000D2F94
		public void SetStringProperty(PropertyPrecedence propertyPrecedence, PropertyId propertyId, string value)
		{
			StringValue stringValue = this.converter.RegisterStringValue(false, value);
			this.converter.ContainerStyleBuildHelper.SetProperty((int)propertyPrecedence, propertyId, stringValue.PropertyValue);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000D4DC8 File Offset: 0x000D2FC8
		public void SetStringProperty(PropertyPrecedence propertyPrecedence, PropertyId propertyId, BufferString value)
		{
			StringValue stringValue = this.converter.RegisterStringValue(false, value);
			this.converter.ContainerStyleBuildHelper.SetProperty((int)propertyPrecedence, propertyId, stringValue.PropertyValue);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x000D4DFC File Offset: 0x000D2FFC
		public void SetStringProperty(PropertyPrecedence propertyPrecedence, PropertyId propertyId, char[] buffer, int offset, int count)
		{
			StringValue stringValue = this.converter.RegisterStringValue(false, new BufferString(buffer, offset, count));
			this.converter.ContainerStyleBuildHelper.SetProperty((int)propertyPrecedence, propertyId, stringValue.PropertyValue);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000D4E39 File Offset: 0x000D3039
		public void SetMultiValueProperty(PropertyPrecedence propertyPrecedence, PropertyId propertyId, MultiValue value)
		{
			value.AddRef();
			this.converter.ContainerStyleBuildHelper.SetProperty((int)propertyPrecedence, propertyId, value.PropertyValue);
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x000D4E5C File Offset: 0x000D305C
		public void SetMultiValueProperty(PropertyPrecedence propertyPrecedence, PropertyId propertyId, out MultiValueBuilder multiValueBuilder)
		{
			MultiValue multiValue = this.converter.RegisterMultiValue(false, out multiValueBuilder);
			this.converter.ContainerStyleBuildHelper.SetProperty((int)propertyPrecedence, propertyId, multiValue.PropertyValue);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000D4E90 File Offset: 0x000D3090
		public void SetStyleReference(int stylePrecedence, int styleHandle)
		{
			this.converter.ContainerStyleBuildHelper.AddStyle(stylePrecedence, styleHandle);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000D4EA4 File Offset: 0x000D30A4
		public void SetStyleReference(int stylePrecedence, FormatStyle style)
		{
			this.converter.ContainerStyleBuildHelper.AddStyle(stylePrecedence, style.Handle);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x000D4EBE File Offset: 0x000D30BE
		public override bool Equals(object obj)
		{
			return obj is FormatConverterContainer && this.converter == ((FormatConverterContainer)obj).converter && this.level == ((FormatConverterContainer)obj).level;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x000D4EF0 File Offset: 0x000D30F0
		public override int GetHashCode()
		{
			return this.level;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x000D4EF8 File Offset: 0x000D30F8
		[Conditional("DEBUG")]
		private void AssertValidAndNotNull()
		{
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000D4EFA File Offset: 0x000D30FA
		[Conditional("DEBUG")]
		private void AssertValid()
		{
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000D4EFC File Offset: 0x000D30FC
		[Conditional("DEBUG")]
		private void AssertValidNotFlushed()
		{
		}

		// Token: 0x04002119 RID: 8473
		public static readonly FormatConverterContainer Null = new FormatConverterContainer(null, -1);

		// Token: 0x0400211A RID: 8474
		private FormatConverter converter;

		// Token: 0x0400211B RID: 8475
		private int level;
	}
}
