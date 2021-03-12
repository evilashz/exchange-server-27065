using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001BA RID: 442
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ImItemListType
	{
		// Token: 0x04000A2E RID: 2606
		[XmlArrayItem("ImGroup", IsNullable = false)]
		public ImGroupType[] Groups;

		// Token: 0x04000A2F RID: 2607
		[XmlArrayItem("Persona", IsNullable = false)]
		public PersonaType[] Personas;
	}
}
