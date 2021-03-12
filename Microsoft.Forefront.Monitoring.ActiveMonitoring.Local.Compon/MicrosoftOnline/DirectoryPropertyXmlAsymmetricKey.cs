using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000119 RID: 281
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlAsymmetricKey : DirectoryPropertyXml
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00020252 File Offset: 0x0001E452
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x0002025A File Offset: 0x0001E45A
		[XmlElement("Value")]
		public XmlValueAsymmetricKey[] Value
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

		// Token: 0x04000440 RID: 1088
		private XmlValueAsymmetricKey[] valueField;
	}
}
