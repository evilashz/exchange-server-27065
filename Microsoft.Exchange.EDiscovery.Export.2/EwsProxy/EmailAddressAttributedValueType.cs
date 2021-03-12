using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000F8 RID: 248
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class EmailAddressAttributedValueType
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00021016 File Offset: 0x0001F216
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0002101E File Offset: 0x0001F21E
		public EmailAddressType Value
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

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00021027 File Offset: 0x0001F227
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0002102F File Offset: 0x0001F22F
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

		// Token: 0x0400083E RID: 2110
		private EmailAddressType valueField;

		// Token: 0x0400083F RID: 2111
		private string[] attributionsField;
	}
}
