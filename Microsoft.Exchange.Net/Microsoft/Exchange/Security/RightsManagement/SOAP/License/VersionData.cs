using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.License
{
	// Token: 0x020009CB RID: 2507
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/LicensingService")]
	[XmlRoot(Namespace = "http://microsoft.com/DRM/LicensingService", IsNullable = false)]
	[Serializable]
	public class VersionData : SoapHeader
	{
		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x0008BFEA File Offset: 0x0008A1EA
		// (set) Token: 0x060036B8 RID: 14008 RVA: 0x0008BFF2 File Offset: 0x0008A1F2
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

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060036B9 RID: 14009 RVA: 0x0008BFFB File Offset: 0x0008A1FB
		// (set) Token: 0x060036BA RID: 14010 RVA: 0x0008C003 File Offset: 0x0008A203
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

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060036BB RID: 14011 RVA: 0x0008C00C File Offset: 0x0008A20C
		// (set) Token: 0x060036BC RID: 14012 RVA: 0x0008C014 File Offset: 0x0008A214
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

		// Token: 0x04002ECE RID: 11982
		private string minimumVersionField;

		// Token: 0x04002ECF RID: 11983
		private string maximumVersionField;

		// Token: 0x04002ED0 RID: 11984
		private XmlAttribute[] anyAttrField;
	}
}
