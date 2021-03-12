using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200013F RID: 319
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlAssignedLicense : DirectoryPropertyXml
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000205A2 File Offset: 0x0001E7A2
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x000205AA File Offset: 0x0001E7AA
		[XmlElement("Value")]
		public XmlValueAssignedLicense[] Value
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

		// Token: 0x04000460 RID: 1120
		private XmlValueAssignedLicense[] valueField;
	}
}
