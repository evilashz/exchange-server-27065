using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000166 RID: 358
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class InternetHeaderType
	{
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0002393A File Offset: 0x00021B3A
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x00023942 File Offset: 0x00021B42
		[XmlAttribute]
		public string HeaderName
		{
			get
			{
				return this.headerNameField;
			}
			set
			{
				this.headerNameField = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0002394B File Offset: 0x00021B4B
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x00023953 File Offset: 0x00021B53
		[XmlText]
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

		// Token: 0x04000B2F RID: 2863
		private string headerNameField;

		// Token: 0x04000B30 RID: 2864
		private string valueField;
	}
}
