using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002E5 RID: 741
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CalendarFolderType : BaseFolderType
	{
		// Token: 0x0400127E RID: 4734
		public CalendarPermissionReadAccessType SharingEffectiveRights;

		// Token: 0x0400127F RID: 4735
		[XmlIgnore]
		public bool SharingEffectiveRightsSpecified;

		// Token: 0x04001280 RID: 4736
		public CalendarPermissionSetType PermissionSet;
	}
}
