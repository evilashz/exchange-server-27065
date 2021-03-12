using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000611 RID: 1553
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class PhysicalAddressDictionaryEntryType
	{
		// Token: 0x0600309C RID: 12444 RVA: 0x000B66BA File Offset: 0x000B48BA
		public PhysicalAddressDictionaryEntryType()
		{
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000B66C2 File Offset: 0x000B48C2
		public PhysicalAddressDictionaryEntryType(string street, string city, string state, string countryOrRegion, string postalCode)
		{
			this.Street = street;
			this.City = city;
			this.State = state;
			this.CountryOrRegion = countryOrRegion;
			this.PostalCode = postalCode;
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000B66EF File Offset: 0x000B48EF
		// (set) Token: 0x0600309F RID: 12447 RVA: 0x000B66F7 File Offset: 0x000B48F7
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string Street { get; set; }

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000B6700 File Offset: 0x000B4900
		// (set) Token: 0x060030A1 RID: 12449 RVA: 0x000B6708 File Offset: 0x000B4908
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string City { get; set; }

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x000B6711 File Offset: 0x000B4911
		// (set) Token: 0x060030A3 RID: 12451 RVA: 0x000B6719 File Offset: 0x000B4919
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string State { get; set; }

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000B6722 File Offset: 0x000B4922
		// (set) Token: 0x060030A5 RID: 12453 RVA: 0x000B672A File Offset: 0x000B492A
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string CountryOrRegion { get; set; }

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x000B6733 File Offset: 0x000B4933
		// (set) Token: 0x060030A7 RID: 12455 RVA: 0x000B673B File Offset: 0x000B493B
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public string PostalCode { get; set; }

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x000B6744 File Offset: 0x000B4944
		// (set) Token: 0x060030A9 RID: 12457 RVA: 0x000B674C File Offset: 0x000B494C
		[XmlAttribute]
		[IgnoreDataMember]
		public PhysicalAddressKeyType Key { get; set; }

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000B6755 File Offset: 0x000B4955
		// (set) Token: 0x060030AB RID: 12459 RVA: 0x000B6762 File Offset: 0x000B4962
		[DataMember(Name = "Key", EmitDefaultValue = false, Order = 0)]
		[XmlIgnore]
		public string KeyString
		{
			get
			{
				return EnumUtilities.ToString<PhysicalAddressKeyType>(this.Key);
			}
			set
			{
				this.Key = EnumUtilities.Parse<PhysicalAddressKeyType>(value);
			}
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000B6770 File Offset: 0x000B4970
		internal bool IsSet()
		{
			return !string.IsNullOrEmpty(this.Street) || !string.IsNullOrEmpty(this.City) || !string.IsNullOrEmpty(this.State) || !string.IsNullOrEmpty(this.PostalCode) || !string.IsNullOrEmpty(this.CountryOrRegion);
		}
	}
}
