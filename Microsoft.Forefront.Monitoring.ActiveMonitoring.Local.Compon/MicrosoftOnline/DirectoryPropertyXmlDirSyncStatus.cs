using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000133 RID: 307
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlDirSyncStatus : DirectoryPropertyXml
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000204A9 File Offset: 0x0001E6A9
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x000204B1 File Offset: 0x0001E6B1
		[XmlElement("Value")]
		public XmlValueDirSyncStatus[] Value
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

		// Token: 0x04000457 RID: 1111
		private XmlValueDirSyncStatus[] valueField;
	}
}
