using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000932 RID: 2354
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class AppAddressValue
	{
		// Token: 0x170027B5 RID: 10165
		// (get) Token: 0x06006FB4 RID: 28596 RVA: 0x00176E19 File Offset: 0x00175019
		// (set) Token: 0x06006FB5 RID: 28597 RVA: 0x00176E21 File Offset: 0x00175021
		[XmlAttribute]
		public AddressType AddressType
		{
			get
			{
				return this.addressTypeField;
			}
			set
			{
				this.addressTypeField = value;
			}
		}

		// Token: 0x170027B6 RID: 10166
		// (get) Token: 0x06006FB6 RID: 28598 RVA: 0x00176E2A File Offset: 0x0017502A
		// (set) Token: 0x06006FB7 RID: 28599 RVA: 0x00176E32 File Offset: 0x00175032
		[XmlAttribute(DataType = "anyURI")]
		public string Address
		{
			get
			{
				return this.addressField;
			}
			set
			{
				this.addressField = value;
			}
		}

		// Token: 0x04004881 RID: 18561
		private AddressType addressTypeField;

		// Token: 0x04004882 RID: 18562
		private string addressField;
	}
}
