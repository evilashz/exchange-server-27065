using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000140 RID: 320
	[XmlInclude(typeof(NumberedRecurrenceRangeType))]
	[XmlInclude(typeof(NoEndRecurrenceRangeType))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(EndDateRecurrenceRangeType))]
	[Serializable]
	public abstract class RecurrenceRangeBaseType
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00022725 File Offset: 0x00020925
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x0002272D File Offset: 0x0002092D
		[XmlElement(DataType = "date")]
		public DateTime StartDate
		{
			get
			{
				return this.startDateField;
			}
			set
			{
				this.startDateField = value;
			}
		}

		// Token: 0x040009CA RID: 2506
		private DateTime startDateField;
	}
}
