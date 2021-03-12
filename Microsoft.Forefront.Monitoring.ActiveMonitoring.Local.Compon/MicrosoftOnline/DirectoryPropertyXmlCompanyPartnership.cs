using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200013B RID: 315
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(DirectoryPropertyXmlCompanyPartnershipSingle))]
	[Serializable]
	public class DirectoryPropertyXmlCompanyPartnership : DirectoryPropertyXml
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0002054F File Offset: 0x0001E74F
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x00020557 File Offset: 0x0001E757
		[XmlElement("Value")]
		public XmlValueCompanyPartnership[] Value
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

		// Token: 0x0400045D RID: 1117
		private XmlValueCompanyPartnership[] valueField;
	}
}
