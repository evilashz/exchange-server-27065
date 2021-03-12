using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000180 RID: 384
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PhoneEntityType : EntityType
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x00023C1A File Offset: 0x00021E1A
		// (set) Token: 0x060010A1 RID: 4257 RVA: 0x00023C22 File Offset: 0x00021E22
		public string OriginalPhoneString
		{
			get
			{
				return this.originalPhoneStringField;
			}
			set
			{
				this.originalPhoneStringField = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00023C2B File Offset: 0x00021E2B
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x00023C33 File Offset: 0x00021E33
		public string PhoneString
		{
			get
			{
				return this.phoneStringField;
			}
			set
			{
				this.phoneStringField = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00023C3C File Offset: 0x00021E3C
		// (set) Token: 0x060010A5 RID: 4261 RVA: 0x00023C44 File Offset: 0x00021E44
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

		// Token: 0x04000B58 RID: 2904
		private string originalPhoneStringField;

		// Token: 0x04000B59 RID: 2905
		private string phoneStringField;

		// Token: 0x04000B5A RID: 2906
		private string typeField;
	}
}
