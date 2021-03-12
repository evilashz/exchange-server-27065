using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Template
{
	// Token: 0x020009F4 RID: 2548
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlRoot(Namespace = "http://microsoft.com/DRM/TemplateDistributionService", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/TemplateDistributionService")]
	[Serializable]
	public class VersionData : SoapHeader
	{
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x0008D0B6 File Offset: 0x0008B2B6
		// (set) Token: 0x0600379B RID: 14235 RVA: 0x0008D0BE File Offset: 0x0008B2BE
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

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x0008D0C7 File Offset: 0x0008B2C7
		// (set) Token: 0x0600379D RID: 14237 RVA: 0x0008D0CF File Offset: 0x0008B2CF
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

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x0008D0D8 File Offset: 0x0008B2D8
		// (set) Token: 0x0600379F RID: 14239 RVA: 0x0008D0E0 File Offset: 0x0008B2E0
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

		// Token: 0x04002F2D RID: 12077
		private string minimumVersionField;

		// Token: 0x04002F2E RID: 12078
		private string maximumVersionField;

		// Token: 0x04002F2F RID: 12079
		private XmlAttribute[] anyAttrField;
	}
}
