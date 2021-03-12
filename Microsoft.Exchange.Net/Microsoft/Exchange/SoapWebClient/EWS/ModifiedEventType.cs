using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000313 RID: 787
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ModifiedEventType : BaseObjectChangedEventType
	{
		// Token: 0x0400130F RID: 4879
		public int UnreadCount;

		// Token: 0x04001310 RID: 4880
		[XmlIgnore]
		public bool UnreadCountSpecified;
	}
}
