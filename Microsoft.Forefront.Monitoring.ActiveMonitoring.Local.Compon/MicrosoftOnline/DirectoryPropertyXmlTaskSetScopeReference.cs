using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000124 RID: 292
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlTaskSetScopeReference : DirectoryPropertyXml
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00020343 File Offset: 0x0001E543
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x0002034B File Offset: 0x0001E54B
		[XmlElement("Value")]
		public XmlValueTaskSetScopeReference[] Value
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

		// Token: 0x04000449 RID: 1097
		private XmlValueTaskSetScopeReference[] valueField;
	}
}
