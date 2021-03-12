using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200013A RID: 314
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryPropertyXmlCompanyVerifiedDomain : DirectoryPropertyXml
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00020536 File Offset: 0x0001E736
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0002053E File Offset: 0x0001E73E
		[XmlElement("Value")]
		public XmlValueCompanyVerifiedDomain[] Value
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

		// Token: 0x0400045C RID: 1116
		private XmlValueCompanyVerifiedDomain[] valueField;
	}
}
