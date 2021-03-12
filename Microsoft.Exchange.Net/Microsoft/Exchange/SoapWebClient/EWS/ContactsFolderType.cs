using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002F0 RID: 752
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ContactsFolderType : BaseFolderType
	{
		// Token: 0x040012CD RID: 4813
		public PermissionReadAccessType SharingEffectiveRights;

		// Token: 0x040012CE RID: 4814
		[XmlIgnore]
		public bool SharingEffectiveRightsSpecified;

		// Token: 0x040012CF RID: 4815
		public PermissionSetType PermissionSet;
	}
}
