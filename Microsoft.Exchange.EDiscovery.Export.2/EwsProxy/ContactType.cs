using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000181 RID: 385
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ContactType : EntityType
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00023C55 File Offset: 0x00021E55
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00023C5D File Offset: 0x00021E5D
		public string PersonName
		{
			get
			{
				return this.personNameField;
			}
			set
			{
				this.personNameField = value;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00023C66 File Offset: 0x00021E66
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x00023C6E File Offset: 0x00021E6E
		public string BusinessName
		{
			get
			{
				return this.businessNameField;
			}
			set
			{
				this.businessNameField = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00023C77 File Offset: 0x00021E77
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x00023C7F File Offset: 0x00021E7F
		[XmlArrayItem("Phone", IsNullable = false)]
		public PhoneType[] PhoneNumbers
		{
			get
			{
				return this.phoneNumbersField;
			}
			set
			{
				this.phoneNumbersField = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00023C88 File Offset: 0x00021E88
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x00023C90 File Offset: 0x00021E90
		[XmlArrayItem("Url", IsNullable = false)]
		public string[] Urls
		{
			get
			{
				return this.urlsField;
			}
			set
			{
				this.urlsField = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00023C99 File Offset: 0x00021E99
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x00023CA1 File Offset: 0x00021EA1
		[XmlArrayItem("EmailAddress", IsNullable = false)]
		public string[] EmailAddresses
		{
			get
			{
				return this.emailAddressesField;
			}
			set
			{
				this.emailAddressesField = value;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00023CAA File Offset: 0x00021EAA
		// (set) Token: 0x060010B2 RID: 4274 RVA: 0x00023CB2 File Offset: 0x00021EB2
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

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00023CBB File Offset: 0x00021EBB
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00023CC3 File Offset: 0x00021EC3
		public string ContactString
		{
			get
			{
				return this.contactStringField;
			}
			set
			{
				this.contactStringField = value;
			}
		}

		// Token: 0x04000B5B RID: 2907
		private string personNameField;

		// Token: 0x04000B5C RID: 2908
		private string businessNameField;

		// Token: 0x04000B5D RID: 2909
		private PhoneType[] phoneNumbersField;

		// Token: 0x04000B5E RID: 2910
		private string[] urlsField;

		// Token: 0x04000B5F RID: 2911
		private string[] emailAddressesField;

		// Token: 0x04000B60 RID: 2912
		private string[] addressesField;

		// Token: 0x04000B61 RID: 2913
		private string contactStringField;
	}
}
