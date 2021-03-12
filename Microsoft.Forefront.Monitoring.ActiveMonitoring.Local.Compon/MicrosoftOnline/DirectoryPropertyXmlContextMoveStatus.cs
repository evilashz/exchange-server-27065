using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000138 RID: 312
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyXmlContextMoveStatusSingle))]
	[Serializable]
	public class DirectoryPropertyXmlContextMoveStatus : DirectoryPropertyXml
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00020515 File Offset: 0x0001E715
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x0002051D File Offset: 0x0001E71D
		[XmlElement("Value")]
		public XmlValueContextMoveStatus[] Value
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

		// Token: 0x0400045B RID: 1115
		private XmlValueContextMoveStatus[] valueField;
	}
}
