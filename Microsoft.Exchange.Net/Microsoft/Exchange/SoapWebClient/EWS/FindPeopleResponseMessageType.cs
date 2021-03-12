using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200033E RID: 830
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FindPeopleResponseMessageType : ResponseMessageType
	{
		// Token: 0x040013A9 RID: 5033
		[XmlArrayItem("Persona", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public PersonaType[] People;

		// Token: 0x040013AA RID: 5034
		public int TotalNumberOfPeopleInView;

		// Token: 0x040013AB RID: 5035
		[XmlIgnore]
		public bool TotalNumberOfPeopleInViewSpecified;

		// Token: 0x040013AC RID: 5036
		public int FirstMatchingRowIndex;

		// Token: 0x040013AD RID: 5037
		[XmlIgnore]
		public bool FirstMatchingRowIndexSpecified;

		// Token: 0x040013AE RID: 5038
		public int FirstLoadedRowIndex;

		// Token: 0x040013AF RID: 5039
		[XmlIgnore]
		public bool FirstLoadedRowIndexSpecified;
	}
}
