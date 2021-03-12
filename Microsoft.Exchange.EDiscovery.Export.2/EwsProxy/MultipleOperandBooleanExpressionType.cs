using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000217 RID: 535
	[XmlInclude(typeof(OrType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(AndType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public abstract class MultipleOperandBooleanExpressionType : SearchExpressionType
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00026202 File Offset: 0x00024402
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x0002620A File Offset: 0x0002440A
		[XmlElement("Excludes", typeof(ExcludesType))]
		[XmlElement("And", typeof(AndType))]
		[XmlElement("Contains", typeof(ContainsExpressionType))]
		[XmlElement("Exists", typeof(ExistsType))]
		[XmlElement("IsEqualTo", typeof(IsEqualToType))]
		[XmlElement("IsLessThanOrEqualTo", typeof(IsLessThanOrEqualToType))]
		[XmlElement("IsGreaterThan", typeof(IsGreaterThanType))]
		[XmlElement("Not", typeof(NotType))]
		[XmlElement("Or", typeof(OrType))]
		[XmlElement("IsGreaterThanOrEqualTo", typeof(IsGreaterThanOrEqualToType))]
		[XmlElement("SearchExpression", typeof(SearchExpressionType))]
		[XmlElement("IsLessThan", typeof(IsLessThanType))]
		[XmlElement("IsNotEqualTo", typeof(IsNotEqualToType))]
		public SearchExpressionType[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x04000E89 RID: 3721
		private SearchExpressionType[] itemsField;
	}
}
