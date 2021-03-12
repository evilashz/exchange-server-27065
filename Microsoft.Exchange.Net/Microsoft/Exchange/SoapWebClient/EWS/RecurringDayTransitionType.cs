using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003DF RID: 991
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class RecurringDayTransitionType : RecurringTimeTransitionType
	{
		// Token: 0x0400158B RID: 5515
		public string DayOfWeek;

		// Token: 0x0400158C RID: 5516
		public int Occurrence;
	}
}
