using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200012D RID: 301
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlProvisionedPlan : DirectoryPropertyXml
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00020424 File Offset: 0x0001E624
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x0002042C File Offset: 0x0001E62C
		[XmlElement("Value")]
		public XmlValueProvisionedPlan[] Value
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

		// Token: 0x04000452 RID: 1106
		private XmlValueProvisionedPlan[] valueField;
	}
}
