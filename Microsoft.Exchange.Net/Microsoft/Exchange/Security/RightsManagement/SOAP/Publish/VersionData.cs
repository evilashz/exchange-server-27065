using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Publish
{
	// Token: 0x020009D5 RID: 2517
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/PublishingService")]
	[XmlRoot(Namespace = "http://microsoft.com/DRM/PublishingService", IsNullable = false)]
	[Serializable]
	public class VersionData : SoapHeader
	{
		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x0008C47A File Offset: 0x0008A67A
		// (set) Token: 0x060036F9 RID: 14073 RVA: 0x0008C482 File Offset: 0x0008A682
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

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x0008C48B File Offset: 0x0008A68B
		// (set) Token: 0x060036FB RID: 14075 RVA: 0x0008C493 File Offset: 0x0008A693
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

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x0008C49C File Offset: 0x0008A69C
		// (set) Token: 0x060036FD RID: 14077 RVA: 0x0008C4A4 File Offset: 0x0008A6A4
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

		// Token: 0x04002EE3 RID: 12003
		private string minimumVersionField;

		// Token: 0x04002EE4 RID: 12004
		private string maximumVersionField;

		// Token: 0x04002EE5 RID: 12005
		private XmlAttribute[] anyAttrField;
	}
}
