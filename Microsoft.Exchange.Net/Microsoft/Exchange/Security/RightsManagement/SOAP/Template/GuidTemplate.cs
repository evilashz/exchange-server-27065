using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Template
{
	// Token: 0x020009F5 RID: 2549
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://microsoft.com/DRM/TemplateDistributionService")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GuidTemplate
	{
		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x0008D0F1 File Offset: 0x0008B2F1
		// (set) Token: 0x060037A2 RID: 14242 RVA: 0x0008D0F9 File Offset: 0x0008B2F9
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

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x0008D102 File Offset: 0x0008B302
		// (set) Token: 0x060037A4 RID: 14244 RVA: 0x0008D10A File Offset: 0x0008B30A
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

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x0008D113 File Offset: 0x0008B313
		// (set) Token: 0x060037A6 RID: 14246 RVA: 0x0008D11B File Offset: 0x0008B31B
		public string Template
		{
			get
			{
				return this.templateField;
			}
			set
			{
				this.templateField = value;
			}
		}

		// Token: 0x04002F30 RID: 12080
		private string guidField;

		// Token: 0x04002F31 RID: 12081
		private string hashField;

		// Token: 0x04002F32 RID: 12082
		private string templateField;
	}
}
