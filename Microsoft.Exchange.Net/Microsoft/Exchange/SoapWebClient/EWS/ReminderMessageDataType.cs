using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000203 RID: 515
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ReminderMessageDataType
	{
		// Token: 0x04000D65 RID: 3429
		public string ReminderText;

		// Token: 0x04000D66 RID: 3430
		public string Location;

		// Token: 0x04000D67 RID: 3431
		public DateTime StartTime;

		// Token: 0x04000D68 RID: 3432
		[XmlIgnore]
		public bool StartTimeSpecified;

		// Token: 0x04000D69 RID: 3433
		public DateTime EndTime;

		// Token: 0x04000D6A RID: 3434
		[XmlIgnore]
		public bool EndTimeSpecified;

		// Token: 0x04000D6B RID: 3435
		public ItemIdType AssociatedCalendarItemId;
	}
}
