using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000130 RID: 304
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyXmlLicenseUnitsDetailSingle))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public abstract class DirectoryPropertyXmlLicenseUnitsDetail : DirectoryPropertyXml
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0002046F File Offset: 0x0001E66F
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00020477 File Offset: 0x0001E677
		[XmlElement("Value")]
		public XmlValueLicenseUnitsDetail[] Value
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

		// Token: 0x04000455 RID: 1109
		private XmlValueLicenseUnitsDetail[] valueField;
	}
}
