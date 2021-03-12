using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000141 RID: 321
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class EndDateRecurrenceRangeType : RecurrenceRangeBaseType
	{
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0002273E File Offset: 0x0002093E
		// (set) Token: 0x06000E2A RID: 3626 RVA: 0x00022746 File Offset: 0x00020946
		[XmlElement(DataType = "date")]
		public DateTime EndDate
		{
			get
			{
				return this.endDateField;
			}
			set
			{
				this.endDateField = value;
			}
		}

		// Token: 0x040009CB RID: 2507
		private DateTime endDateField;
	}
}
