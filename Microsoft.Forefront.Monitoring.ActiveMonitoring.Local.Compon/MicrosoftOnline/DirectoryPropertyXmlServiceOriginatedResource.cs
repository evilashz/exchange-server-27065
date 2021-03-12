using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000128 RID: 296
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlServiceOriginatedResource : DirectoryPropertyXml
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x000203A7 File Offset: 0x0001E5A7
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x000203AF File Offset: 0x0001E5AF
		[XmlElement("Value")]
		public XmlValueServiceOriginatedResource[] Value
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

		// Token: 0x0400044D RID: 1101
		private XmlValueServiceOriginatedResource[] valueField;
	}
}
