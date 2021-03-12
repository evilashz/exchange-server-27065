using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000306 RID: 774
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SeekToConditionPageViewType : BasePagingType
	{
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x00028848 File Offset: 0x00026A48
		// (set) Token: 0x060019AB RID: 6571 RVA: 0x00028850 File Offset: 0x00026A50
		public RestrictionType Condition
		{
			get
			{
				return this.conditionField;
			}
			set
			{
				this.conditionField = value;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x00028859 File Offset: 0x00026A59
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x00028861 File Offset: 0x00026A61
		[XmlAttribute]
		public IndexBasePointType BasePoint
		{
			get
			{
				return this.basePointField;
			}
			set
			{
				this.basePointField = value;
			}
		}

		// Token: 0x04001147 RID: 4423
		private RestrictionType conditionField;

		// Token: 0x04001148 RID: 4424
		private IndexBasePointType basePointField;
	}
}
