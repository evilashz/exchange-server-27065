using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002EC RID: 748
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PermissionType : BasePermissionType
	{
		// Token: 0x040012AB RID: 4779
		public PermissionReadAccessType ReadItems;

		// Token: 0x040012AC RID: 4780
		[XmlIgnore]
		public bool ReadItemsSpecified;

		// Token: 0x040012AD RID: 4781
		public PermissionLevelType PermissionLevel;
	}
}
