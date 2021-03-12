using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020003A2 RID: 930
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class RegionInfo
	{
		// Token: 0x06003026 RID: 12326 RVA: 0x000B8BE4 File Offset: 0x000B6DE4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public RegionInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoRegionInvariantCulture"));
			}
			this.m_cultureData = CultureData.GetCultureDataForRegion(name, true);
			if (this.m_cultureData == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidCultureName"), name), "name");
			}
			if (this.m_cultureData.IsNeutralCulture)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNeutralRegionName", new object[]
				{
					name
				}), "name");
			}
			this.SetName(name);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000B8C88 File Offset: 0x000B6E88
		[SecuritySafeCritical]
		public RegionInfo(int culture)
		{
			if (culture == 127)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoRegionInvariantCulture"));
			}
			if (culture == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_CultureIsNeutral", new object[]
				{
					culture
				}), "culture");
			}
			if (culture == 3072)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_CustomCultureCannotBePassedByNumber", new object[]
				{
					culture
				}), "culture");
			}
			this.m_cultureData = CultureData.GetCultureData(culture, true);
			this.m_name = this.m_cultureData.SREGIONNAME;
			if (this.m_cultureData.IsNeutralCulture)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_CultureIsNeutral", new object[]
				{
					culture
				}), "culture");
			}
			this.m_cultureId = culture;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000B8D59 File Offset: 0x000B6F59
		[SecuritySafeCritical]
		internal RegionInfo(CultureData cultureData)
		{
			this.m_cultureData = cultureData;
			this.m_name = this.m_cultureData.SREGIONNAME;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000B8D79 File Offset: 0x000B6F79
		[SecurityCritical]
		private void SetName(string name)
		{
			this.m_name = (name.Equals(this.m_cultureData.SREGIONNAME, StringComparison.OrdinalIgnoreCase) ? this.m_cultureData.SREGIONNAME : this.m_cultureData.CultureName);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000B8DB0 File Offset: 0x000B6FB0
		[SecurityCritical]
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_name == null)
			{
				this.m_cultureId = RegionInfo.IdFromEverettRegionInfoDataItem[this.m_dataItem];
			}
			if (this.m_cultureId == 0)
			{
				this.m_cultureData = CultureData.GetCultureDataForRegion(this.m_name, true);
			}
			else
			{
				this.m_cultureData = CultureData.GetCultureData(this.m_cultureId, true);
			}
			if (this.m_cultureData == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidCultureName"), this.m_name), "m_name");
			}
			if (this.m_cultureId == 0)
			{
				this.SetName(this.m_name);
				return;
			}
			this.m_name = this.m_cultureData.SREGIONNAME;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000B8E58 File Offset: 0x000B7058
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600302C RID: 12332 RVA: 0x000B8E5C File Offset: 0x000B705C
		[__DynamicallyInvokable]
		public static RegionInfo CurrentRegion
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				RegionInfo regionInfo = RegionInfo.s_currentRegionInfo;
				if (regionInfo == null)
				{
					regionInfo = new RegionInfo(CultureInfo.CurrentCulture.m_cultureData);
					regionInfo.m_name = regionInfo.m_cultureData.SREGIONNAME;
					RegionInfo.s_currentRegionInfo = regionInfo;
				}
				return regionInfo;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x000B8E9E File Offset: 0x000B709E
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000B8EA6 File Offset: 0x000B70A6
		[__DynamicallyInvokable]
		public virtual string EnglishName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SENGCOUNTRY;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x000B8EB3 File Offset: 0x000B70B3
		[__DynamicallyInvokable]
		public virtual string DisplayName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SLOCALIZEDCOUNTRY;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06003030 RID: 12336 RVA: 0x000B8EC0 File Offset: 0x000B70C0
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual string NativeName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SNATIVECOUNTRY;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000B8ECD File Offset: 0x000B70CD
		[__DynamicallyInvokable]
		public virtual string TwoLetterISORegionName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SISO3166CTRYNAME;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x000B8EDA File Offset: 0x000B70DA
		public virtual string ThreeLetterISORegionName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SISO3166CTRYNAME2;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x000B8EE7 File Offset: 0x000B70E7
		public virtual string ThreeLetterWindowsRegionName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SABBREVCTRYNAME;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x000B8EF4 File Offset: 0x000B70F4
		[__DynamicallyInvokable]
		public virtual bool IsMetric
		{
			[__DynamicallyInvokable]
			get
			{
				int imeasure = this.m_cultureData.IMEASURE;
				return imeasure == 0;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x000B8F11 File Offset: 0x000B7111
		[ComVisible(false)]
		public virtual int GeoId
		{
			get
			{
				return this.m_cultureData.IGEOID;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06003036 RID: 12342 RVA: 0x000B8F1E File Offset: 0x000B711E
		[ComVisible(false)]
		public virtual string CurrencyEnglishName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SENGLISHCURRENCY;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000B8F2B File Offset: 0x000B712B
		[ComVisible(false)]
		public virtual string CurrencyNativeName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SNATIVECURRENCY;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x000B8F38 File Offset: 0x000B7138
		[__DynamicallyInvokable]
		public virtual string CurrencySymbol
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SCURRENCY;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06003039 RID: 12345 RVA: 0x000B8F45 File Offset: 0x000B7145
		[__DynamicallyInvokable]
		public virtual string ISOCurrencySymbol
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SINTLSYMBOL;
			}
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000B8F54 File Offset: 0x000B7154
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			RegionInfo regionInfo = value as RegionInfo;
			return regionInfo != null && this.Name.Equals(regionInfo.Name);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000B8F7E File Offset: 0x000B717E
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000B8F8B File Offset: 0x000B718B
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04001458 RID: 5208
		internal string m_name;

		// Token: 0x04001459 RID: 5209
		[NonSerialized]
		internal CultureData m_cultureData;

		// Token: 0x0400145A RID: 5210
		internal static volatile RegionInfo s_currentRegionInfo;

		// Token: 0x0400145B RID: 5211
		[OptionalField(VersionAdded = 2)]
		private int m_cultureId;

		// Token: 0x0400145C RID: 5212
		[OptionalField(VersionAdded = 2)]
		internal int m_dataItem;

		// Token: 0x0400145D RID: 5213
		private static readonly int[] IdFromEverettRegionInfoDataItem = new int[]
		{
			14337,
			1052,
			1067,
			11274,
			3079,
			3081,
			1068,
			2060,
			1026,
			15361,
			2110,
			16394,
			1046,
			1059,
			10249,
			3084,
			9225,
			2055,
			13322,
			2052,
			9226,
			5130,
			1029,
			1031,
			1030,
			7178,
			5121,
			12298,
			1061,
			3073,
			1027,
			1035,
			1080,
			1036,
			2057,
			1079,
			1032,
			4106,
			3076,
			18442,
			1050,
			1038,
			1057,
			6153,
			1037,
			1081,
			2049,
			1065,
			1039,
			1040,
			8201,
			11265,
			1041,
			1089,
			1088,
			1042,
			13313,
			1087,
			12289,
			5127,
			1063,
			4103,
			1062,
			4097,
			6145,
			6156,
			1071,
			1104,
			5124,
			1125,
			2058,
			1086,
			19466,
			1043,
			1044,
			5129,
			8193,
			6154,
			10250,
			13321,
			1056,
			1045,
			20490,
			2070,
			15370,
			16385,
			1048,
			1049,
			1025,
			1053,
			4100,
			1060,
			1051,
			2074,
			17418,
			1114,
			1054,
			7169,
			1055,
			11273,
			1028,
			1058,
			1033,
			14346,
			1091,
			8202,
			1066,
			9217,
			1078,
			12297
		};
	}
}
