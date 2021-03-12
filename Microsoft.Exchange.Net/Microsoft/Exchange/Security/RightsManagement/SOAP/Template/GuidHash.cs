using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Template
{
	// Token: 0x020009F6 RID: 2550
	[XmlType(Namespace = "http://microsoft.com/DRM/TemplateDistributionService")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GuidHash
	{
		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x0008D12C File Offset: 0x0008B32C
		// (set) Token: 0x060037A9 RID: 14249 RVA: 0x0008D134 File Offset: 0x0008B334
		public string Guid
		{
			get
			{
				return this.guidField;
			}
			set
			{
				this.guidField = value;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x0008D13D File Offset: 0x0008B33D
		// (set) Token: 0x060037AB RID: 14251 RVA: 0x0008D145 File Offset: 0x0008B345
		public string Hash
		{
			get
			{
				return this.hashField;
			}
			set
			{
				this.hashField = value;
			}
		}

		// Token: 0x04002F33 RID: 12083
		private string guidField;

		// Token: 0x04002F34 RID: 12084
		private string hashField;
	}
}
