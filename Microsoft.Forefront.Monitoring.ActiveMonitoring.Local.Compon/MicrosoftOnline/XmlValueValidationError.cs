using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000D8 RID: 216
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueValidationError
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001F60B File Offset: 0x0001D80B
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0001F613 File Offset: 0x0001D813
		public ValidationErrorValue ErrorInfo
		{
			get
			{
				return this.errorInfoField;
			}
			set
			{
				this.errorInfoField = value;
			}
		}

		// Token: 0x0400036F RID: 879
		private ValidationErrorValue errorInfoField;
	}
}
