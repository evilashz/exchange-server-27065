using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000487 RID: 1159
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UploadItemsType : BaseRequestType
	{
		// Token: 0x040017AB RID: 6059
		[XmlArrayItem("Item", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UploadItemType[] Items;
	}
}
