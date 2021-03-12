using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200041D RID: 1053
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetNonIndexableItemStatisticsType : BaseRequestType
	{
		// Token: 0x04001628 RID: 5672
		[XmlArrayItem("LegacyDN", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Mailboxes;

		// Token: 0x04001629 RID: 5673
		public bool SearchArchiveOnly;

		// Token: 0x0400162A RID: 5674
		[XmlIgnore]
		public bool SearchArchiveOnlySpecified;
	}
}
