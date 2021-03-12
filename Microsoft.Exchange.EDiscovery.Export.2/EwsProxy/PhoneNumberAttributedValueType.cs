using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000F7 RID: 247
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PhoneNumberAttributedValueType
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00020FEC File Offset: 0x0001F1EC
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x00020FF4 File Offset: 0x0001F1F4
		public PersonaPhoneNumberType Value
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

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00020FFD File Offset: 0x0001F1FD
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x00021005 File Offset: 0x0001F205
		[XmlArrayItem("Attribution", IsNullable = false)]
		public string[] Attributions
		{
			get
			{
				return this.attributionsField;
			}
			set
			{
				this.attributionsField = value;
			}
		}

		// Token: 0x0400083C RID: 2108
		private PersonaPhoneNumberType valueField;

		// Token: 0x0400083D RID: 2109
		private string[] attributionsField;
	}
}
