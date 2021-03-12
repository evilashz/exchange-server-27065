using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000122 RID: 290
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlAuthorizedParty : DirectoryPropertyXml
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00020311 File Offset: 0x0001E511
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x00020319 File Offset: 0x0001E519
		[XmlElement("Value")]
		public XmlValueAuthorizedParty[] Value
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

		// Token: 0x04000447 RID: 1095
		private XmlValueAuthorizedParty[] valueField;
	}
}
