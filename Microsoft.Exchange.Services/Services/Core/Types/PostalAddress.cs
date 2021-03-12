using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000848 RID: 2120
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "PersonaPostalAddress")]
	[XmlType(TypeName = "PersonaPostalAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PostalAddress
	{
		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x000D7110 File Offset: 0x000D5310
		// (set) Token: 0x06003D16 RID: 15638 RVA: 0x000D7118 File Offset: 0x000D5318
		[XmlElement]
		[DataMember(IsRequired = false, Order = 1)]
		public string Street { get; set; }

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003D17 RID: 15639 RVA: 0x000D7121 File Offset: 0x000D5321
		// (set) Token: 0x06003D18 RID: 15640 RVA: 0x000D7129 File Offset: 0x000D5329
		[XmlElement]
		[DataMember(IsRequired = false, Order = 2)]
		public string City { get; set; }

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x000D7132 File Offset: 0x000D5332
		// (set) Token: 0x06003D1A RID: 15642 RVA: 0x000D713A File Offset: 0x000D533A
		[DataMember(IsRequired = false, Order = 3)]
		[XmlElement]
		public string State { get; set; }

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x000D7143 File Offset: 0x000D5343
		// (set) Token: 0x06003D1C RID: 15644 RVA: 0x000D714B File Offset: 0x000D534B
		[DataMember(IsRequired = false, Order = 4)]
		[XmlElement]
		public string Country { get; set; }

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003D1D RID: 15645 RVA: 0x000D7154 File Offset: 0x000D5354
		// (set) Token: 0x06003D1E RID: 15646 RVA: 0x000D715C File Offset: 0x000D535C
		[DataMember(IsRequired = false, Order = 5)]
		[XmlElement]
		public string PostalCode { get; set; }

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x000D7165 File Offset: 0x000D5365
		// (set) Token: 0x06003D20 RID: 15648 RVA: 0x000D716D File Offset: 0x000D536D
		[DataMember(IsRequired = false, Order = 6)]
		[XmlElement]
		public string PostOfficeBox { get; set; }

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x000D7176 File Offset: 0x000D5376
		// (set) Token: 0x06003D22 RID: 15650 RVA: 0x000D717E File Offset: 0x000D537E
		[XmlIgnore]
		[IgnoreDataMember]
		public PostalAddressType Type { get; set; }

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000D7187 File Offset: 0x000D5387
		// (set) Token: 0x06003D24 RID: 15652 RVA: 0x000D7199 File Offset: 0x000D5399
		[XmlElement(ElementName = "Type")]
		[DataMember(Name = "Type", IsRequired = true, Order = 7)]
		public string TypeString
		{
			get
			{
				return this.Type.ToString();
			}
			set
			{
				this.Type = (PostalAddressType)Enum.Parse(typeof(PostalAddressType), value);
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003D25 RID: 15653 RVA: 0x000D71B6 File Offset: 0x000D53B6
		// (set) Token: 0x06003D26 RID: 15654 RVA: 0x000D71BE File Offset: 0x000D53BE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 8)]
		public double? Latitude { get; set; }

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x000D71C7 File Offset: 0x000D53C7
		// (set) Token: 0x06003D28 RID: 15656 RVA: 0x000D71CF File Offset: 0x000D53CF
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 9)]
		public double? Longitude { get; set; }

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003D29 RID: 15657 RVA: 0x000D71D8 File Offset: 0x000D53D8
		// (set) Token: 0x06003D2A RID: 15658 RVA: 0x000D71E0 File Offset: 0x000D53E0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public double? Accuracy { get; set; }

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003D2B RID: 15659 RVA: 0x000D71E9 File Offset: 0x000D53E9
		// (set) Token: 0x06003D2C RID: 15660 RVA: 0x000D71F1 File Offset: 0x000D53F1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 11)]
		public double? Altitude { get; set; }

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x000D71FA File Offset: 0x000D53FA
		// (set) Token: 0x06003D2E RID: 15662 RVA: 0x000D7202 File Offset: 0x000D5402
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 12)]
		public double? AltitudeAccuracy { get; set; }

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000D720B File Offset: 0x000D540B
		// (set) Token: 0x06003D30 RID: 15664 RVA: 0x000D7213 File Offset: 0x000D5413
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 13)]
		public string FormattedAddress { get; set; }

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x000D721C File Offset: 0x000D541C
		// (set) Token: 0x06003D32 RID: 15666 RVA: 0x000D7224 File Offset: 0x000D5424
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 14)]
		public string LocationUri { get; set; }

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003D33 RID: 15667 RVA: 0x000D722D File Offset: 0x000D542D
		// (set) Token: 0x06003D34 RID: 15668 RVA: 0x000D7235 File Offset: 0x000D5435
		[XmlElement]
		[IgnoreDataMember]
		public LocationSourceType LocationSource { get; set; }

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003D35 RID: 15669 RVA: 0x000D723E File Offset: 0x000D543E
		// (set) Token: 0x06003D36 RID: 15670 RVA: 0x000D7250 File Offset: 0x000D5450
		[DataMember(Name = "LocationSource", IsRequired = true, Order = 15)]
		[XmlIgnore]
		public string LocationSourceString
		{
			get
			{
				return this.LocationSource.ToString();
			}
			set
			{
				this.LocationSource = (LocationSourceType)Enum.Parse(typeof(LocationSourceType), value);
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000D7270 File Offset: 0x000D5470
		// (set) Token: 0x06003D38 RID: 15672 RVA: 0x000D728B File Offset: 0x000D548B
		[IgnoreDataMember]
		[XmlIgnore]
		public bool LatitudeSpecified
		{
			get
			{
				return this.Latitude != null;
			}
			set
			{
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003D39 RID: 15673 RVA: 0x000D7290 File Offset: 0x000D5490
		// (set) Token: 0x06003D3A RID: 15674 RVA: 0x000D72AB File Offset: 0x000D54AB
		[XmlIgnore]
		[IgnoreDataMember]
		public bool LongitudeSpecified
		{
			get
			{
				return this.Longitude != null;
			}
			set
			{
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x000D72B0 File Offset: 0x000D54B0
		// (set) Token: 0x06003D3C RID: 15676 RVA: 0x000D72CB File Offset: 0x000D54CB
		[XmlIgnore]
		[IgnoreDataMember]
		public bool AccuracySpecified
		{
			get
			{
				return this.Accuracy != null;
			}
			set
			{
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06003D3D RID: 15677 RVA: 0x000D72D0 File Offset: 0x000D54D0
		// (set) Token: 0x06003D3E RID: 15678 RVA: 0x000D72EB File Offset: 0x000D54EB
		[IgnoreDataMember]
		[XmlIgnore]
		public bool AltitudeSpecified
		{
			get
			{
				return this.Altitude != null;
			}
			set
			{
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x000D72F0 File Offset: 0x000D54F0
		// (set) Token: 0x06003D40 RID: 15680 RVA: 0x000D730B File Offset: 0x000D550B
		[XmlIgnore]
		[IgnoreDataMember]
		public bool AltitudeAccuracySpecified
		{
			get
			{
				return this.AltitudeAccuracy != null;
			}
			set
			{
			}
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x000D730D File Offset: 0x000D550D
		public PostalAddress()
		{
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x000D7318 File Offset: 0x000D5518
		public PostalAddress(string street, string city, string state, string country, string postalCode, string postOfficeBox, string formattedAddress, string locationUri, LocationSourceType locationSource, double? latitude, double? longitude, double? accuracy, double? altitude, double? altitudeAccuracy, PostalAddressType type)
		{
			this.Street = street;
			this.City = city;
			this.State = state;
			this.Country = country;
			this.PostalCode = postalCode;
			this.PostOfficeBox = postOfficeBox;
			this.FormattedAddress = formattedAddress;
			this.LocationUri = locationUri;
			this.LocationSource = locationSource;
			this.Latitude = latitude;
			this.Longitude = longitude;
			this.Accuracy = accuracy;
			this.Altitude = altitude;
			this.AltitudeAccuracy = altitudeAccuracy;
			this.Type = type;
		}
	}
}
