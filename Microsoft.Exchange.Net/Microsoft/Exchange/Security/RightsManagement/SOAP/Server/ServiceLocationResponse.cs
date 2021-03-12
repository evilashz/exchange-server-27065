using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009E9 RID: 2537
	[XmlType(Namespace = "http://microsoft.com/DRM/ServerService")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ServiceLocationResponse
	{
		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003769 RID: 14185 RVA: 0x0008CCE7 File Offset: 0x0008AEE7
		// (set) Token: 0x0600376A RID: 14186 RVA: 0x0008CCEF File Offset: 0x0008AEEF
		public string URL
		{
			get
			{
				return this.uRLField;
			}
			set
			{
				this.uRLField = value;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x0008CCF8 File Offset: 0x0008AEF8
		// (set) Token: 0x0600376C RID: 14188 RVA: 0x0008CD00 File Offset: 0x0008AF00
		public ServiceType Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x04002F0B RID: 12043
		private string uRLField;

		// Token: 0x04002F0C RID: 12044
		private ServiceType typeField;
	}
}
