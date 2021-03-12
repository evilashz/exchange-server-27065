using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008BF RID: 2239
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AddressesAndDomainsType
	{
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x0006CA45 File Offset: 0x0006AC45
		// (set) Token: 0x06002FFF RID: 12287 RVA: 0x0006CA4D File Offset: 0x0006AC4D
		[XmlArrayItem("Address", IsNullable = false)]
		public string[] Addresses
		{
			get
			{
				return this.addressesField;
			}
			set
			{
				this.addressesField = value;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x0006CA56 File Offset: 0x0006AC56
		// (set) Token: 0x06003001 RID: 12289 RVA: 0x0006CA5E File Offset: 0x0006AC5E
		[XmlArrayItem("Domain", IsNullable = false)]
		public string[] Domains
		{
			get
			{
				return this.domainsField;
			}
			set
			{
				this.domainsField = value;
			}
		}

		// Token: 0x04002973 RID: 10611
		private string[] addressesField;

		// Token: 0x04002974 RID: 10612
		private string[] domainsField;
	}
}
