using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000136 RID: 310
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyXmlContextMoveWatermarksSingle))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlContextMoveWatermarks : DirectoryPropertyXml
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x000204F4 File Offset: 0x0001E6F4
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x000204FC File Offset: 0x0001E6FC
		[XmlElement("Value")]
		public XmlValueContextMoveWatermarks[] Value
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

		// Token: 0x0400045A RID: 1114
		private XmlValueContextMoveWatermarks[] valueField;
	}
}
