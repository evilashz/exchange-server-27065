using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000127 RID: 295
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlValidationError : DirectoryPropertyXml
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0002038E File Offset: 0x0001E58E
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x00020396 File Offset: 0x0001E596
		[XmlElement("Value")]
		public XmlValueValidationError[] Value
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

		// Token: 0x0400044C RID: 1100
		private XmlValueValidationError[] valueField;
	}
}
