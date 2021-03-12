using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000310 RID: 784
	[XmlInclude(typeof(BaseObjectChangedEventType))]
	[DesignerCategory("code")]
	[XmlInclude(typeof(MovedCopiedEventType))]
	[XmlInclude(typeof(ModifiedEventType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class BaseNotificationEventType
	{
		// Token: 0x04001309 RID: 4873
		public string Watermark;
	}
}
