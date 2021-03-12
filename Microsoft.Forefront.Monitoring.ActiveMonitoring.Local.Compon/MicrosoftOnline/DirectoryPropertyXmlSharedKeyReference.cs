using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000118 RID: 280
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlSharedKeyReference : DirectoryPropertyXml
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00020239 File Offset: 0x0001E439
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00020241 File Offset: 0x0001E441
		[XmlElement("Value")]
		public XmlValueSharedKeyReference[] Value
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

		// Token: 0x0400043F RID: 1087
		private XmlValueSharedKeyReference[] valueField;
	}
}
