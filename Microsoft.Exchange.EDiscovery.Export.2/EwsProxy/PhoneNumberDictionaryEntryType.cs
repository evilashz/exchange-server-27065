using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000154 RID: 340
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PhoneNumberDictionaryEntryType
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00022DB1 File Offset: 0x00020FB1
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x00022DB9 File Offset: 0x00020FB9
		[XmlAttribute]
		public PhoneNumberKeyType Key
		{
			get
			{
				return this.keyField;
			}
			set
			{
				this.keyField = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00022DC2 File Offset: 0x00020FC2
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x00022DCA File Offset: 0x00020FCA
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

		// Token: 0x04000A4D RID: 2637
		private PhoneNumberKeyType keyField;

		// Token: 0x04000A4E RID: 2638
		private string valueField;
	}
}
