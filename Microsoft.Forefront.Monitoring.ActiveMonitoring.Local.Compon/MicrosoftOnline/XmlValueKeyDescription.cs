using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000BA RID: 186
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueKeyDescription
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001F149 File Offset: 0x0001D349
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0001F151 File Offset: 0x0001D351
		public KeyDescriptionValue KeyDescription
		{
			get
			{
				return this.keyDescriptionField;
			}
			set
			{
				this.keyDescriptionField = value;
			}
		}

		// Token: 0x04000331 RID: 817
		private KeyDescriptionValue keyDescriptionField;
	}
}
