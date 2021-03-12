using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200035B RID: 859
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class WorkingHours
	{
		// Token: 0x0400142D RID: 5165
		public SerializableTimeZone TimeZone;

		// Token: 0x0400142E RID: 5166
		[XmlArrayItem(IsNullable = false)]
		public WorkingPeriod[] WorkingPeriodArray;
	}
}
