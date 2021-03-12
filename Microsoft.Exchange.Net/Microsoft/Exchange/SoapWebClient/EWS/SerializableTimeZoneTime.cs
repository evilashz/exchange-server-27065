using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200035D RID: 861
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class SerializableTimeZoneTime
	{
		// Token: 0x04001432 RID: 5170
		public int Bias;

		// Token: 0x04001433 RID: 5171
		public string Time;

		// Token: 0x04001434 RID: 5172
		public short DayOrder;

		// Token: 0x04001435 RID: 5173
		public short Month;

		// Token: 0x04001436 RID: 5174
		public string DayOfWeek;

		// Token: 0x04001437 RID: 5175
		public string Year;
	}
}
