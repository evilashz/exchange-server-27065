using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200013D RID: 317
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlCompanyUnverifiedDomain : DirectoryPropertyXml
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00020570 File Offset: 0x0001E770
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x00020578 File Offset: 0x0001E778
		[XmlElement("Value")]
		public XmlValueCompanyUnverifiedDomain[] Value
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

		// Token: 0x0400045E RID: 1118
		private XmlValueCompanyUnverifiedDomain[] valueField;
	}
}
