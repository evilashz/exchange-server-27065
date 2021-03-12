using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000135 RID: 309
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlCredential : DirectoryPropertyXml
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x000204DB File Offset: 0x0001E6DB
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x000204E3 File Offset: 0x0001E6E3
		[XmlElement("Value")]
		public XmlValueCredential[] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04000459 RID: 1113
		private XmlValueCredential[] valueField;
	}
}
