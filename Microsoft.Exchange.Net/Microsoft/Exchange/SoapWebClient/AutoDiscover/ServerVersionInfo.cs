using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200010E RID: 270
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class ServerVersionInfo : SoapHeader
	{
		// Token: 0x0400058C RID: 1420
		public int MajorVersion;

		// Token: 0x0400058D RID: 1421
		[XmlIgnore]
		public bool MajorVersionSpecified;

		// Token: 0x0400058E RID: 1422
		public int MinorVersion;

		// Token: 0x0400058F RID: 1423
		[XmlIgnore]
		public bool MinorVersionSpecified;

		// Token: 0x04000590 RID: 1424
		public int MajorBuildNumber;

		// Token: 0x04000591 RID: 1425
		[XmlIgnore]
		public bool MajorBuildNumberSpecified;

		// Token: 0x04000592 RID: 1426
		public int MinorBuildNumber;

		// Token: 0x04000593 RID: 1427
		[XmlIgnore]
		public bool MinorBuildNumberSpecified;

		// Token: 0x04000594 RID: 1428
		[XmlElement(IsNullable = true)]
		public string Version;
	}
}
