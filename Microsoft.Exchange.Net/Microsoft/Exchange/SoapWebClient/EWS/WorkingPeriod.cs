using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200035A RID: 858
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class WorkingPeriod
	{
		// Token: 0x0400142A RID: 5162
		public string DayOfWeek;

		// Token: 0x0400142B RID: 5163
		public int StartTimeInMinutes;

		// Token: 0x0400142C RID: 5164
		public int EndTimeInMinutes;
	}
}
