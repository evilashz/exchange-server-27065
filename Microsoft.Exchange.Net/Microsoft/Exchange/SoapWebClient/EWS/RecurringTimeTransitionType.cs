using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003DE RID: 990
	[XmlInclude(typeof(RecurringDateTransitionType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(RecurringDayTransitionType))]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public abstract class RecurringTimeTransitionType : TransitionType
	{
		// Token: 0x04001589 RID: 5513
		[XmlElement(DataType = "duration")]
		public string TimeOffset;

		// Token: 0x0400158A RID: 5514
		public int Month;
	}
}
