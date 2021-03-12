using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000142 RID: 322
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class NumberedRecurrenceRangeType : RecurrenceRangeBaseType
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00022757 File Offset: 0x00020957
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x0002275F File Offset: 0x0002095F
		public int NumberOfOccurrences
		{
			get
			{
				return this.numberOfOccurrencesField;
			}
			set
			{
				this.numberOfOccurrencesField = value;
			}
		}

		// Token: 0x040009CC RID: 2508
		private int numberOfOccurrencesField;
	}
}
