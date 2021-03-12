using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CE5 RID: 3301
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class XmlAttributePropertyDefinition : SimpleVirtualPropertyDefinition
	{
		// Token: 0x0600721B RID: 29211 RVA: 0x001F93AD File Offset: 0x001F75AD
		internal XmlAttributePropertyDefinition(string displayName, Type type, string xmlAttributeName, params PropertyDefinitionConstraint[] constraints) : this(displayName, type, xmlAttributeName, XmlAttributePropertyDefinition.noDefaultValue, constraints)
		{
		}

		// Token: 0x0600721C RID: 29212 RVA: 0x001F93BF File Offset: 0x001F75BF
		internal XmlAttributePropertyDefinition(string displayName, Type type, string xmlAttributeName, XmlAttributePropertyDefinition.GenerateDefaultValueDelegate generateDefaultValue, params PropertyDefinitionConstraint[] constraints) : base(PropertyTypeSpecifier.XmlNode, displayName, type, PropertyFlags.Automatic, constraints)
		{
			this.xmlAttributeName = xmlAttributeName;
			this.generateDefaultValue = generateDefaultValue;
		}

		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x0600721D RID: 29213 RVA: 0x001F93E0 File Offset: 0x001F75E0
		internal object DefaultValue
		{
			get
			{
				return this.generateDefaultValue();
			}
		}

		// Token: 0x17001E75 RID: 7797
		// (get) Token: 0x0600721E RID: 29214 RVA: 0x001F93ED File Offset: 0x001F75ED
		internal bool HasDefaultValue
		{
			get
			{
				return this.generateDefaultValue != XmlAttributePropertyDefinition.noDefaultValue;
			}
		}

		// Token: 0x17001E76 RID: 7798
		// (get) Token: 0x0600721F RID: 29215 RVA: 0x001F93FF File Offset: 0x001F75FF
		internal string XmlAttributeName
		{
			get
			{
				return this.xmlAttributeName;
			}
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x001F9407 File Offset: 0x001F7607
		private static object GenerateNoDefaultValue()
		{
			throw new NotSupportedException("Property doesn't have a default value");
		}

		// Token: 0x06007221 RID: 29217 RVA: 0x001F9413 File Offset: 0x001F7613
		protected override string GetPropertyDefinitionString()
		{
			return string.Format("XML: @{0}", this.xmlAttributeName);
		}

		// Token: 0x04004F7E RID: 20350
		private readonly string xmlAttributeName;

		// Token: 0x04004F7F RID: 20351
		private readonly XmlAttributePropertyDefinition.GenerateDefaultValueDelegate generateDefaultValue;

		// Token: 0x04004F80 RID: 20352
		private static readonly XmlAttributePropertyDefinition.GenerateDefaultValueDelegate noDefaultValue = new XmlAttributePropertyDefinition.GenerateDefaultValueDelegate(XmlAttributePropertyDefinition.GenerateNoDefaultValue);

		// Token: 0x02000CE6 RID: 3302
		// (Invoke) Token: 0x06007224 RID: 29220
		internal delegate object GenerateDefaultValueDelegate();
	}
}
