using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000490 RID: 1168
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ServerVersionInfo : SoapHeader
	{
		// Token: 0x040017B7 RID: 6071
		[XmlAttribute]
		public int MajorVersion;

		// Token: 0x040017B8 RID: 6072
		[XmlIgnore]
		public bool MajorVersionSpecified;

		// Token: 0x040017B9 RID: 6073
		[XmlAttribute]
		public int MinorVersion;

		// Token: 0x040017BA RID: 6074
		[XmlIgnore]
		public bool MinorVersionSpecified;

		// Token: 0x040017BB RID: 6075
		[XmlAttribute]
		public int MajorBuildNumber;

		// Token: 0x040017BC RID: 6076
		[XmlIgnore]
		public bool MajorBuildNumberSpecified;

		// Token: 0x040017BD RID: 6077
		[XmlAttribute]
		public int MinorBuildNumber;

		// Token: 0x040017BE RID: 6078
		[XmlIgnore]
		public bool MinorBuildNumberSpecified;

		// Token: 0x040017BF RID: 6079
		[XmlAttribute]
		public string Version;
	}
}
