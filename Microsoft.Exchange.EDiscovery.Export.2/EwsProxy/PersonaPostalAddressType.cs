using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000FA RID: 250
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PersonaPostalAddressType
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002106A File Offset: 0x0001F26A
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x00021072 File Offset: 0x0001F272
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

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002107B File Offset: 0x0001F27B
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x00021083 File Offset: 0x0001F283
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

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0002108C File Offset: 0x0001F28C
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x00021094 File Offset: 0x0001F294
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

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0002109D File Offset: 0x0001F29D
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x000210A5 File Offset: 0x0001F2A5
		public string Country
		{
			get
			{
				return this.countryField;
			}
			set
			{
				this.countryField = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x000210AE File Offset: 0x0001F2AE
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x000210B6 File Offset: 0x0001F2B6
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

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x000210BF File Offset: 0x0001F2BF
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x000210C7 File Offset: 0x0001F2C7
		public string PostOfficeBox
		{
			get
			{
				return this.postOfficeBoxField;
			}
			set
			{
				this.postOfficeBoxField = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x000210D0 File Offset: 0x0001F2D0
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x000210D8 File Offset: 0x0001F2D8
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

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x000210E1 File Offset: 0x0001F2E1
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x000210E9 File Offset: 0x0001F2E9
		public double Latitude
		{
			get
			{
				return this.latitudeField;
			}
			set
			{
				this.latitudeField = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x000210F2 File Offset: 0x0001F2F2
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x000210FA File Offset: 0x0001F2FA
		[XmlIgnore]
		public bool LatitudeSpecified
		{
			get
			{
				return this.latitudeFieldSpecified;
			}
			set
			{
				this.latitudeFieldSpecified = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00021103 File Offset: 0x0001F303
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0002110B File Offset: 0x0001F30B
		public double Longitude
		{
			get
			{
				return this.longitudeField;
			}
			set
			{
				this.longitudeField = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00021114 File Offset: 0x0001F314
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x0002111C File Offset: 0x0001F31C
		[XmlIgnore]
		public bool LongitudeSpecified
		{
			get
			{
				return this.longitudeFieldSpecified;
			}
			set
			{
				this.longitudeFieldSpecified = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x00021125 File Offset: 0x0001F325
		// (set) Token: 0x06000B8D RID: 2957 RVA: 0x0002112D File Offset: 0x0001F32D
		public double Accuracy
		{
			get
			{
				return this.accuracyField;
			}
			set
			{
				this.accuracyField = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00021136 File Offset: 0x0001F336
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x0002113E File Offset: 0x0001F33E
		[XmlIgnore]
		public bool AccuracySpecified
		{
			get
			{
				return this.accuracyFieldSpecified;
			}
			set
			{
				this.accuracyFieldSpecified = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00021147 File Offset: 0x0001F347
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0002114F File Offset: 0x0001F34F
		public double Altitude
		{
			get
			{
				return this.altitudeField;
			}
			set
			{
				this.altitudeField = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00021158 File Offset: 0x0001F358
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x00021160 File Offset: 0x0001F360
		[XmlIgnore]
		public bool AltitudeSpecified
		{
			get
			{
				return this.altitudeFieldSpecified;
			}
			set
			{
				this.altitudeFieldSpecified = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00021169 File Offset: 0x0001F369
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x00021171 File Offset: 0x0001F371
		public double AltitudeAccuracy
		{
			get
			{
				return this.altitudeAccuracyField;
			}
			set
			{
				this.altitudeAccuracyField = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002117A File Offset: 0x0001F37A
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x00021182 File Offset: 0x0001F382
		[XmlIgnore]
		public bool AltitudeAccuracySpecified
		{
			get
			{
				return this.altitudeAccuracyFieldSpecified;
			}
			set
			{
				this.altitudeAccuracyFieldSpecified = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002118B File Offset: 0x0001F38B
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x00021193 File Offset: 0x0001F393
		public string FormattedAddress
		{
			get
			{
				return this.formattedAddressField;
			}
			set
			{
				this.formattedAddressField = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002119C File Offset: 0x0001F39C
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x000211A4 File Offset: 0x0001F3A4
		public string LocationUri
		{
			get
			{
				return this.locationUriField;
			}
			set
			{
				this.locationUriField = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x000211AD File Offset: 0x0001F3AD
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x000211B5 File Offset: 0x0001F3B5
		public LocationSourceType LocationSource
		{
			get
			{
				return this.locationSourceField;
			}
			set
			{
				this.locationSourceField = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x000211BE File Offset: 0x0001F3BE
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x000211C6 File Offset: 0x0001F3C6
		[XmlIgnore]
		public bool LocationSourceSpecified
		{
			get
			{
				return this.locationSourceFieldSpecified;
			}
			set
			{
				this.locationSourceFieldSpecified = value;
			}
		}

		// Token: 0x04000842 RID: 2114
		private string streetField;

		// Token: 0x04000843 RID: 2115
		private string cityField;

		// Token: 0x04000844 RID: 2116
		private string stateField;

		// Token: 0x04000845 RID: 2117
		private string countryField;

		// Token: 0x04000846 RID: 2118
		private string postalCodeField;

		// Token: 0x04000847 RID: 2119
		private string postOfficeBoxField;

		// Token: 0x04000848 RID: 2120
		private string typeField;

		// Token: 0x04000849 RID: 2121
		private double latitudeField;

		// Token: 0x0400084A RID: 2122
		private bool latitudeFieldSpecified;

		// Token: 0x0400084B RID: 2123
		private double longitudeField;

		// Token: 0x0400084C RID: 2124
		private bool longitudeFieldSpecified;

		// Token: 0x0400084D RID: 2125
		private double accuracyField;

		// Token: 0x0400084E RID: 2126
		private bool accuracyFieldSpecified;

		// Token: 0x0400084F RID: 2127
		private double altitudeField;

		// Token: 0x04000850 RID: 2128
		private bool altitudeFieldSpecified;

		// Token: 0x04000851 RID: 2129
		private double altitudeAccuracyField;

		// Token: 0x04000852 RID: 2130
		private bool altitudeAccuracyFieldSpecified;

		// Token: 0x04000853 RID: 2131
		private string formattedAddressField;

		// Token: 0x04000854 RID: 2132
		private string locationUriField;

		// Token: 0x04000855 RID: 2133
		private LocationSourceType locationSourceField;

		// Token: 0x04000856 RID: 2134
		private bool locationSourceFieldSpecified;
	}
}
