using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002BA RID: 698
	[Serializable]
	public sealed class CountryList : ADConfigurationObject
	{
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x0008D4E4 File Offset: 0x0008B6E4
		public static string[] UMAllowedCountryList
		{
			get
			{
				return new string[]
				{
					"AF",
					"AX",
					"AL",
					"DZ",
					"AS",
					"AD",
					"AO",
					"AI",
					"AQ",
					"AG",
					"AR",
					"AM",
					"AW",
					"AU",
					"AT",
					"AZ",
					"BS",
					"BH",
					"BD",
					"BB",
					"BY",
					"BE",
					"BZ",
					"BJ",
					"BM",
					"BT",
					"BO",
					"BQ",
					"BA",
					"BW",
					"BV",
					"BR",
					"IO",
					"BN",
					"BG",
					"BF",
					"BI",
					"KH",
					"CM",
					"CA",
					"CV",
					"KY",
					"CF",
					"TD",
					"CL",
					"CN",
					"CX",
					"CC",
					"CO",
					"KM",
					"CG",
					"CD",
					"CK",
					"CR",
					"CI",
					"HR",
					"CW",
					"CY",
					"CZ",
					"DK",
					"DJ",
					"DM",
					"DO",
					"EC",
					"EG",
					"SV",
					"GQ",
					"ER",
					"EE",
					"ET",
					"FK",
					"FO",
					"FJ",
					"FI",
					"FR",
					"GF",
					"PF",
					"TF",
					"GA",
					"GM",
					"GE",
					"DE",
					"GH",
					"GI",
					"GR",
					"GL",
					"GD",
					"GP",
					"GU",
					"GT",
					"GG",
					"GN",
					"GW",
					"GY",
					"HT",
					"HM",
					"VA",
					"HN",
					"HK",
					"HU",
					"IS",
					"ID",
					"IQ",
					"IE",
					"IM",
					"IL",
					"IT",
					"JM",
					"JP",
					"JE",
					"JO",
					"KZ",
					"KE",
					"KI",
					"KR",
					"KW",
					"KG",
					"LA",
					"LV",
					"LB",
					"LS",
					"LR",
					"LY",
					"LI",
					"LT",
					"LU",
					"MK",
					"MG",
					"MW",
					"MY",
					"MV",
					"ML",
					"MT",
					"MH",
					"MQ",
					"MR",
					"MU",
					"YT",
					"MX",
					"FM",
					"MD",
					"MC",
					"MN",
					"ME",
					"MS",
					"MA",
					"MZ",
					"NA",
					"NR",
					"NP",
					"NL",
					"NC",
					"NZ",
					"NI",
					"NE",
					"NG",
					"NU",
					"NF",
					"MP",
					"NO",
					"OM",
					"PK",
					"PW",
					"PS",
					"PA",
					"PG",
					"PY",
					"PE",
					"PH",
					"PN",
					"PL",
					"PT",
					"PR",
					"QA",
					"RE",
					"RO",
					"RU",
					"RW",
					"BL",
					"SH",
					"KN",
					"LC",
					"MF",
					"PM",
					"VC",
					"WS",
					"SM",
					"ST",
					"SA",
					"SN",
					"RS",
					"SC",
					"SL",
					"SG",
					"SX",
					"SK",
					"SI",
					"SB",
					"SO",
					"ZA",
					"GS",
					"ES",
					"LK",
					"SR",
					"SJ",
					"SZ",
					"SE",
					"CH",
					"TW",
					"TJ",
					"TZ",
					"TH",
					"TL",
					"TG",
					"TK",
					"TO",
					"TT",
					"TN",
					"TR",
					"TM",
					"TC",
					"TV",
					"UG",
					"UA",
					"AE",
					"GB",
					"US",
					"UM",
					"UY",
					"UZ",
					"VU",
					"VE",
					"VN",
					"VG",
					"VI",
					"WF",
					"EH",
					"YE",
					"ZM",
					"ZW"
				};
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0008DEB4 File Offset: 0x0008C0B4
		internal override ADObjectSchema Schema
		{
			get
			{
				return CountryList.schema;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x0008DEBB File Offset: 0x0008C0BB
		internal override string MostDerivedObjectClass
		{
			get
			{
				return CountryList.MostDerivedClass;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x0008DEC2 File Offset: 0x0008C0C2
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return CountryListSchema.ObjectVersion;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x0008DEC9 File Offset: 0x0008C0C9
		// (set) Token: 0x06002005 RID: 8197 RVA: 0x0008DEDB File Offset: 0x0008C0DB
		[Parameter]
		public MultiValuedProperty<CountryInfo> Countries
		{
			get
			{
				return (MultiValuedProperty<CountryInfo>)this[CountryListSchema.Countries];
			}
			set
			{
				this[CountryListSchema.Countries] = value;
			}
		}

		// Token: 0x04001337 RID: 4919
		private static readonly CountryListSchema schema = ObjectSchema.GetInstance<CountryListSchema>();

		// Token: 0x04001338 RID: 4920
		internal static readonly ADObjectId RdnContainer = new ADObjectId("CN=Country Lists,CN=RBAC");

		// Token: 0x04001339 RID: 4921
		public static readonly string UMAllowedCountryListName = "UMAllowedCountryList";

		// Token: 0x0400133A RID: 4922
		public static readonly string MostDerivedClass = "msExchCountryList";
	}
}
