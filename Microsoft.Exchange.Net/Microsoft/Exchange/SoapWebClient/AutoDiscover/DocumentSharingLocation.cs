using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000115 RID: 277
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DocumentSharingLocation
	{
		// Token: 0x040005B3 RID: 1459
		public string ServiceUrl;

		// Token: 0x040005B4 RID: 1460
		public string LocationUrl;

		// Token: 0x040005B5 RID: 1461
		public string DisplayName;

		// Token: 0x040005B6 RID: 1462
		[XmlArrayItem("FileExtension", IsNullable = false)]
		public string[] SupportedFileExtensions;

		// Token: 0x040005B7 RID: 1463
		public bool ExternalAccessAllowed;

		// Token: 0x040005B8 RID: 1464
		public bool AnonymousAccessAllowed;

		// Token: 0x040005B9 RID: 1465
		public bool CanModifyPermissions;

		// Token: 0x040005BA RID: 1466
		public bool IsDefault;
	}
}
