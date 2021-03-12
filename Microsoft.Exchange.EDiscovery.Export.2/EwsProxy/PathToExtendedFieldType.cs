using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000DE RID: 222
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class PathToExtendedFieldType : BasePathToElementType
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x000205E5 File Offset: 0x0001E7E5
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x000205ED File Offset: 0x0001E7ED
		[XmlAttribute]
		public DistinguishedPropertySetType DistinguishedPropertySetId
		{
			get
			{
				return this.distinguishedPropertySetIdField;
			}
			set
			{
				this.distinguishedPropertySetIdField = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x000205F6 File Offset: 0x0001E7F6
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x000205FE File Offset: 0x0001E7FE
		[XmlIgnore]
		public bool DistinguishedPropertySetIdSpecified
		{
			get
			{
				return this.distinguishedPropertySetIdFieldSpecified;
			}
			set
			{
				this.distinguishedPropertySetIdFieldSpecified = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00020607 File Offset: 0x0001E807
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x0002060F File Offset: 0x0001E80F
		[XmlAttribute]
		public string PropertySetId
		{
			get
			{
				return this.propertySetIdField;
			}
			set
			{
				this.propertySetIdField = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00020618 File Offset: 0x0001E818
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x00020620 File Offset: 0x0001E820
		[XmlAttribute]
		public string PropertyTag
		{
			get
			{
				return this.propertyTagField;
			}
			set
			{
				this.propertyTagField = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00020629 File Offset: 0x0001E829
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x00020631 File Offset: 0x0001E831
		[XmlAttribute]
		public string PropertyName
		{
			get
			{
				return this.propertyNameField;
			}
			set
			{
				this.propertyNameField = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0002063A File Offset: 0x0001E83A
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x00020642 File Offset: 0x0001E842
		[XmlAttribute]
		public int PropertyId
		{
			get
			{
				return this.propertyIdField;
			}
			set
			{
				this.propertyIdField = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002064B File Offset: 0x0001E84B
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x00020653 File Offset: 0x0001E853
		[XmlIgnore]
		public bool PropertyIdSpecified
		{
			get
			{
				return this.propertyIdFieldSpecified;
			}
			set
			{
				this.propertyIdFieldSpecified = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002065C File Offset: 0x0001E85C
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x00020664 File Offset: 0x0001E864
		[XmlAttribute]
		public MapiPropertyTypeType PropertyType
		{
			get
			{
				return this.propertyTypeField;
			}
			set
			{
				this.propertyTypeField = value;
			}
		}

		// Token: 0x040005E7 RID: 1511
		private DistinguishedPropertySetType distinguishedPropertySetIdField;

		// Token: 0x040005E8 RID: 1512
		private bool distinguishedPropertySetIdFieldSpecified;

		// Token: 0x040005E9 RID: 1513
		private string propertySetIdField;

		// Token: 0x040005EA RID: 1514
		private string propertyTagField;

		// Token: 0x040005EB RID: 1515
		private string propertyNameField;

		// Token: 0x040005EC RID: 1516
		private int propertyIdField;

		// Token: 0x040005ED RID: 1517
		private bool propertyIdFieldSpecified;

		// Token: 0x040005EE RID: 1518
		private MapiPropertyTypeType propertyTypeField;
	}
}
