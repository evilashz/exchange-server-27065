using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200092F RID: 2351
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class StsAddressValue
	{
		// Token: 0x170027B2 RID: 10162
		// (get) Token: 0x06006FAC RID: 28588 RVA: 0x00176DD6 File Offset: 0x00174FD6
		// (set) Token: 0x06006FAD RID: 28589 RVA: 0x00176DDE File Offset: 0x00174FDE
		[XmlAttribute]
		public StsAddressType AddressType
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

		// Token: 0x170027B3 RID: 10163
		// (get) Token: 0x06006FAE RID: 28590 RVA: 0x00176DE7 File Offset: 0x00174FE7
		// (set) Token: 0x06006FAF RID: 28591 RVA: 0x00176DEF File Offset: 0x00174FEF
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

		// Token: 0x04004876 RID: 18550
		private StsAddressType addressTypeField;

		// Token: 0x04004877 RID: 18551
		private string addressField;
	}
}
