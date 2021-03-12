using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000C1 RID: 193
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class AppAddressValue
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001F23C File Offset: 0x0001D43C
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001F244 File Offset: 0x0001D444
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

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0001F24D File Offset: 0x0001D44D
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x0001F255 File Offset: 0x0001D455
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

		// Token: 0x0400033C RID: 828
		private AddressType addressTypeField;

		// Token: 0x0400033D RID: 829
		private string addressField;
	}
}
