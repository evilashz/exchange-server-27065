using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000EF RID: 239
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class PersonaPhoneNumberType
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00020E60 File Offset: 0x0001F060
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x00020E68 File Offset: 0x0001F068
		public string Number
		{
			get
			{
				return this.numberField;
			}
			set
			{
				this.numberField = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00020E71 File Offset: 0x0001F071
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x00020E79 File Offset: 0x0001F079
		public string Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x040007FD RID: 2045
		private string numberField;

		// Token: 0x040007FE RID: 2046
		private string typeField;
	}
}
