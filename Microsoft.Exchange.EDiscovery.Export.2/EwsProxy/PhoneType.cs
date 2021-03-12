using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000182 RID: 386
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PhoneType
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00023CD4 File Offset: 0x00021ED4
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x00023CDC File Offset: 0x00021EDC
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

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00023CE5 File Offset: 0x00021EE5
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x00023CED File Offset: 0x00021EED
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

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x00023CF6 File Offset: 0x00021EF6
		// (set) Token: 0x060010BB RID: 4283 RVA: 0x00023CFE File Offset: 0x00021EFE
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

		// Token: 0x04000B62 RID: 2914
		private string originalPhoneStringField;

		// Token: 0x04000B63 RID: 2915
		private string phoneStringField;

		// Token: 0x04000B64 RID: 2916
		private string typeField;
	}
}
