using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000221 RID: 545
	[XmlInclude(typeof(IsLessThanType))]
	[XmlInclude(typeof(IsLessThanOrEqualToType))]
	[XmlInclude(typeof(IsGreaterThanOrEqualToType))]
	[XmlInclude(typeof(IsGreaterThanType))]
	[XmlInclude(typeof(IsNotEqualToType))]
	[XmlInclude(typeof(IsEqualToType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public abstract class TwoOperandExpressionType : SearchExpressionType
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x0002631F File Offset: 0x0002451F
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x00026327 File Offset: 0x00024527
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

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x00026330 File Offset: 0x00024530
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x00026338 File Offset: 0x00024538
		public FieldURIOrConstantType FieldURIOrConstant
		{
			get
			{
				return this.fieldURIOrConstantField;
			}
			set
			{
				this.fieldURIOrConstantField = value;
			}
		}

		// Token: 0x04000EA5 RID: 3749
		private BasePathToElementType itemField;

		// Token: 0x04000EA6 RID: 3750
		private FieldURIOrConstantType fieldURIOrConstantField;
	}
}
