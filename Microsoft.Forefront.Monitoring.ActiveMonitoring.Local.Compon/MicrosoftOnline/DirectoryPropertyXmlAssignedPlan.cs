using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200013E RID: 318
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlAssignedPlan : DirectoryPropertyXml
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00020589 File Offset: 0x0001E789
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x00020591 File Offset: 0x0001E791
		[XmlElement("Value")]
		public XmlValueAssignedPlan[] Value
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

		// Token: 0x0400045F RID: 1119
		private XmlValueAssignedPlan[] valueField;
	}
}
