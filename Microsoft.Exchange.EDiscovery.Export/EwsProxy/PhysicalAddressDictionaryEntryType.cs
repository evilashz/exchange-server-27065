using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000152 RID: 338
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PhysicalAddressDictionaryEntryType
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00022D43 File Offset: 0x00020F43
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00022D4B File Offset: 0x00020F4B
		public string Street
		{
			get
			{
				return this.streetField;
			}
			set
			{
				this.streetField = value;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00022D54 File Offset: 0x00020F54
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00022D5C File Offset: 0x00020F5C
		public string City
		{
			get
			{
				return this.cityField;
			}
			set
			{
				this.cityField = value;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00022D65 File Offset: 0x00020F65
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x00022D6D File Offset: 0x00020F6D
		public string State
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00022D76 File Offset: 0x00020F76
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x00022D7E File Offset: 0x00020F7E
		public string CountryOrRegion
		{
			get
			{
				return this.countryOrRegionField;
			}
			set
			{
				this.countryOrRegionField = value;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00022D87 File Offset: 0x00020F87
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x00022D8F File Offset: 0x00020F8F
		public string PostalCode
		{
			get
			{
				return this.postalCodeField;
			}
			set
			{
				this.postalCodeField = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00022D98 File Offset: 0x00020F98
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x00022DA0 File Offset: 0x00020FA0
		[XmlAttribute]
		public PhysicalAddressKeyType Key
		{
			get
			{
				return this.keyField;
			}
			set
			{
				this.keyField = value;
			}
		}

		// Token: 0x04000A43 RID: 2627
		private string streetField;

		// Token: 0x04000A44 RID: 2628
		private string cityField;

		// Token: 0x04000A45 RID: 2629
		private string stateField;

		// Token: 0x04000A46 RID: 2630
		private string countryOrRegionField;

		// Token: 0x04000A47 RID: 2631
		private string postalCodeField;

		// Token: 0x04000A48 RID: 2632
		private PhysicalAddressKeyType keyField;
	}
}
