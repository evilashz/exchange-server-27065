using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200026B RID: 619
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DistributionListType : ItemType
	{
		// Token: 0x04001015 RID: 4117
		public string DisplayName;

		// Token: 0x04001016 RID: 4118
		public string FileAs;

		// Token: 0x04001017 RID: 4119
		public ContactSourceType ContactSource;

		// Token: 0x04001018 RID: 4120
		[XmlIgnore]
		public bool ContactSourceSpecified;

		// Token: 0x04001019 RID: 4121
		[XmlArrayItem("Member", IsNullable = false)]
		public MemberType[] Members;
	}
}
