using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.ServerCertification
{
	// Token: 0x020009DF RID: 2527
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/CertificationService")]
	[XmlRoot(Namespace = "http://microsoft.com/DRM/CertificationService", IsNullable = false)]
	[Serializable]
	public class VersionData : SoapHeader
	{
		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x0008C716 File Offset: 0x0008A916
		// (set) Token: 0x06003724 RID: 14116 RVA: 0x0008C71E File Offset: 0x0008A91E
		public string MinimumVersion
		{
			get
			{
				return this.minimumVersionField;
			}
			set
			{
				this.minimumVersionField = value;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x0008C727 File Offset: 0x0008A927
		// (set) Token: 0x06003726 RID: 14118 RVA: 0x0008C72F File Offset: 0x0008A92F
		public string MaximumVersion
		{
			get
			{
				return this.maximumVersionField;
			}
			set
			{
				this.maximumVersionField = value;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x0008C738 File Offset: 0x0008A938
		// (set) Token: 0x06003728 RID: 14120 RVA: 0x0008C740 File Offset: 0x0008A940
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x04002EEF RID: 12015
		private string minimumVersionField;

		// Token: 0x04002EF0 RID: 12016
		private string maximumVersionField;

		// Token: 0x04002EF1 RID: 12017
		private XmlAttribute[] anyAttrField;
	}
}
