using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200011A RID: 282
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlEncryptedSecretKey : DirectoryPropertyXml
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0002026B File Offset: 0x0001E46B
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00020273 File Offset: 0x0001E473
		[XmlElement("Value")]
		public XmlValueEncryptedSecretKey[] Value
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

		// Token: 0x04000441 RID: 1089
		private XmlValueEncryptedSecretKey[] valueField;
	}
}
