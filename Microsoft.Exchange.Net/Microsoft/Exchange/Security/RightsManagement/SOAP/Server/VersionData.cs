using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009E6 RID: 2534
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlRoot(Namespace = "http://microsoft.com/DRM/ServerService", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/ServerService")]
	[Serializable]
	public class VersionData : SoapHeader
	{
		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x0008CC82 File Offset: 0x0008AE82
		// (set) Token: 0x0600375E RID: 14174 RVA: 0x0008CC8A File Offset: 0x0008AE8A
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

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x0008CC93 File Offset: 0x0008AE93
		// (set) Token: 0x06003760 RID: 14176 RVA: 0x0008CC9B File Offset: 0x0008AE9B
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

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x0008CCA4 File Offset: 0x0008AEA4
		// (set) Token: 0x06003762 RID: 14178 RVA: 0x0008CCAC File Offset: 0x0008AEAC
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

		// Token: 0x04002F01 RID: 12033
		private string minimumVersionField;

		// Token: 0x04002F02 RID: 12034
		private string maximumVersionField;

		// Token: 0x04002F03 RID: 12035
		private XmlAttribute[] anyAttrField;
	}
}
