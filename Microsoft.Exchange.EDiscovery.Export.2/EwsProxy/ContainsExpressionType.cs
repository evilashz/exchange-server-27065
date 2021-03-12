using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000219 RID: 537
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ContainsExpressionType : SearchExpressionType
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00026223 File Offset: 0x00024423
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x0002622B File Offset: 0x0002442B
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
		[XmlElement("ExtendedFieldURI", typeof(PathToExtendedFieldType))]
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		public BasePathToElementType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00026234 File Offset: 0x00024434
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x0002623C File Offset: 0x0002443C
		public ConstantValueType Constant
		{
			get
			{
				return this.constantField;
			}
			set
			{
				this.constantField = value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00026245 File Offset: 0x00024445
		// (set) Token: 0x06001526 RID: 5414 RVA: 0x0002624D File Offset: 0x0002444D
		[XmlAttribute]
		public ContainmentModeType ContainmentMode
		{
			get
			{
				return this.containmentModeField;
			}
			set
			{
				this.containmentModeField = value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00026256 File Offset: 0x00024456
		// (set) Token: 0x06001528 RID: 5416 RVA: 0x0002625E File Offset: 0x0002445E
		[XmlIgnore]
		public bool ContainmentModeSpecified
		{
			get
			{
				return this.containmentModeFieldSpecified;
			}
			set
			{
				this.containmentModeFieldSpecified = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00026267 File Offset: 0x00024467
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x0002626F File Offset: 0x0002446F
		[XmlAttribute]
		public ContainmentComparisonType ContainmentComparison
		{
			get
			{
				return this.containmentComparisonField;
			}
			set
			{
				this.containmentComparisonField = value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00026278 File Offset: 0x00024478
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x00026280 File Offset: 0x00024480
		[XmlIgnore]
		public bool ContainmentComparisonSpecified
		{
			get
			{
				return this.containmentComparisonFieldSpecified;
			}
			set
			{
				this.containmentComparisonFieldSpecified = value;
			}
		}

		// Token: 0x04000E8A RID: 3722
		private BasePathToElementType itemField;

		// Token: 0x04000E8B RID: 3723
		private ConstantValueType constantField;

		// Token: 0x04000E8C RID: 3724
		private ContainmentModeType containmentModeField;

		// Token: 0x04000E8D RID: 3725
		private bool containmentModeFieldSpecified;

		// Token: 0x04000E8E RID: 3726
		private ContainmentComparisonType containmentComparisonField;

		// Token: 0x04000E8F RID: 3727
		private bool containmentComparisonFieldSpecified;
	}
}
