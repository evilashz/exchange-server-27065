using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002B6 RID: 694
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ReminderType
	{
		// Token: 0x040011ED RID: 4589
		public string Subject;

		// Token: 0x040011EE RID: 4590
		public string Location;

		// Token: 0x040011EF RID: 4591
		public DateTime ReminderTime;

		// Token: 0x040011F0 RID: 4592
		public DateTime StartDate;

		// Token: 0x040011F1 RID: 4593
		public DateTime EndDate;

		// Token: 0x040011F2 RID: 4594
		public ItemIdType ItemId;

		// Token: 0x040011F3 RID: 4595
		public ItemIdType RecurringMasterItemId;

		// Token: 0x040011F4 RID: 4596
		public ReminderGroupType ReminderGroup;

		// Token: 0x040011F5 RID: 4597
		[XmlIgnore]
		public bool ReminderGroupSpecified;

		// Token: 0x040011F6 RID: 4598
		public string UID;
	}
}
