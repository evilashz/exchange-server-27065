using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003E2 RID: 994
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ArrayOfTransitionsType
	{
		// Token: 0x0400158F RID: 5519
		[XmlElement("RecurringDayTransition", typeof(RecurringDayTransitionType))]
		[XmlElement("RecurringDateTransition", typeof(RecurringDateTransitionType))]
		[XmlElement("AbsoluteDateTransition", typeof(AbsoluteDateTransitionType))]
		[XmlElement("Transition", typeof(TransitionType))]
		public TransitionType[] Items;

		// Token: 0x04001590 RID: 5520
		[XmlAttribute]
		public string Id;
	}
}
