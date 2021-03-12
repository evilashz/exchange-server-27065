using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009EB RID: 2539
	[XmlType(Namespace = "http://microsoft.com/DRM/ServerService")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServiceLocationRequest
	{
		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x0008CD11 File Offset: 0x0008AF11
		// (set) Token: 0x0600376F RID: 14191 RVA: 0x0008CD19 File Offset: 0x0008AF19
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

		// Token: 0x04002F23 RID: 12067
		private ServiceType typeField;
	}
}
