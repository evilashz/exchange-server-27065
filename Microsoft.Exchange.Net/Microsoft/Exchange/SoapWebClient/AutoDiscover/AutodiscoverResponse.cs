using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000120 RID: 288
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[XmlInclude(typeof(DomainResponse))]
	[XmlInclude(typeof(GetDomainSettingsResponse))]
	[XmlInclude(typeof(UserResponse))]
	[XmlInclude(typeof(GetUserSettingsResponse))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlInclude(typeof(GetOrganizationRelationshipSettingsResponse))]
	[XmlInclude(typeof(GetFederationInformationResponse))]
	[Serializable]
	public class AutodiscoverResponse
	{
		// Token: 0x040005CF RID: 1487
		public ErrorCode ErrorCode;

		// Token: 0x040005D0 RID: 1488
		[XmlIgnore]
		public bool ErrorCodeSpecified;

		// Token: 0x040005D1 RID: 1489
		[XmlElement(IsNullable = true)]
		public string ErrorMessage;
	}
}
