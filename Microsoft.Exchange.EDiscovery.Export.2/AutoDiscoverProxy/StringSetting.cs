using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200008B RID: 139
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class StringSetting : UserSetting
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0001F775 File Offset: 0x0001D975
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x0001F77D File Offset: 0x0001D97D
		[XmlElement(IsNullable = true)]
		public string Value
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

		// Token: 0x04000333 RID: 819
		private string valueField;
	}
}
