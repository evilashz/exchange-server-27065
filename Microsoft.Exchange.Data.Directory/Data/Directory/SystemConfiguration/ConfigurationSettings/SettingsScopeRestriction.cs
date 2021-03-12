using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000687 RID: 1671
	[Serializable]
	public class SettingsScopeRestriction : XMLSerializableBase
	{
		// Token: 0x06004DB6 RID: 19894 RVA: 0x0011EB89 File Offset: 0x0011CD89
		public SettingsScopeRestriction()
		{
		}

		// Token: 0x06004DB7 RID: 19895 RVA: 0x0011EB91 File Offset: 0x0011CD91
		public SettingsScopeRestriction(string nameMatch, string minVersion, string maxVersion) : this(null, nameMatch, minVersion, maxVersion)
		{
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x0011EB9D File Offset: 0x0011CD9D
		public SettingsScopeRestriction(Guid guidMatch) : this(null, guidMatch.ToString(), null, null)
		{
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x0011EBB5 File Offset: 0x0011CDB5
		public SettingsScopeRestriction(string subType, string nameMatch) : this(subType, nameMatch, null, null)
		{
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x0011EBC1 File Offset: 0x0011CDC1
		private SettingsScopeRestriction(string subType, string nameMatch, string minVersion, string maxVersion)
		{
			this.SubType = subType;
			this.NameMatch = nameMatch;
			this.MinVersion = minVersion;
			this.MaxVersion = maxVersion;
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x06004DBB RID: 19899 RVA: 0x0011EBE6 File Offset: 0x0011CDE6
		// (set) Token: 0x06004DBC RID: 19900 RVA: 0x0011EBEE File Offset: 0x0011CDEE
		[XmlAttribute(AttributeName = "SbT")]
		public string SubType { get; set; }

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x0011EBF7 File Offset: 0x0011CDF7
		// (set) Token: 0x06004DBE RID: 19902 RVA: 0x0011EBFF File Offset: 0x0011CDFF
		[XmlAttribute(AttributeName = "NmM")]
		public string NameMatch { get; set; }

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x06004DBF RID: 19903 RVA: 0x0011EC08 File Offset: 0x0011CE08
		// (set) Token: 0x06004DC0 RID: 19904 RVA: 0x0011EC10 File Offset: 0x0011CE10
		[XmlAttribute(AttributeName = "MinV")]
		public string MinVersion
		{
			get
			{
				return this.minVersion;
			}
			set
			{
				this.minVersion = value;
				this.minServerVersion = null;
				this.minExchangeVersion = null;
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x06004DC1 RID: 19905 RVA: 0x0011EC27 File Offset: 0x0011CE27
		// (set) Token: 0x06004DC2 RID: 19906 RVA: 0x0011EC2F File Offset: 0x0011CE2F
		[XmlAttribute(AttributeName = "MaxV")]
		public string MaxVersion
		{
			get
			{
				return this.maxVersion;
			}
			set
			{
				this.maxVersion = value;
				this.maxServerVersion = null;
				this.maxExchangeVersion = null;
			}
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x06004DC3 RID: 19907 RVA: 0x0011EC48 File Offset: 0x0011CE48
		internal Guid? GuidMatch
		{
			get
			{
				Guid guid;
				if (Guid.TryParse(this.NameMatch, out guid) && guid != Guid.Empty)
				{
					return new Guid?(guid);
				}
				return null;
			}
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x06004DC4 RID: 19908 RVA: 0x0011EC81 File Offset: 0x0011CE81
		internal ServerVersion MinServerVersion
		{
			get
			{
				if (this.minServerVersion == null && this.minVersion != null)
				{
					this.minServerVersion = SettingsScopeRestriction.ParseServerVersion(this.minVersion);
				}
				return this.minServerVersion;
			}
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x0011ECB0 File Offset: 0x0011CEB0
		internal ServerVersion MaxServerVersion
		{
			get
			{
				if (this.maxServerVersion == null && this.maxVersion != null)
				{
					this.maxServerVersion = SettingsScopeRestriction.ParseServerVersion(this.maxVersion);
				}
				return this.maxServerVersion;
			}
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x0011ECDF File Offset: 0x0011CEDF
		internal ExchangeObjectVersion MinExchangeVersion
		{
			get
			{
				if (this.minExchangeVersion == null && this.minVersion != null)
				{
					this.minExchangeVersion = SettingsScopeRestriction.ParseExchangeVersion(this.minVersion);
				}
				return this.minExchangeVersion;
			}
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x06004DC7 RID: 19911 RVA: 0x0011ED0E File Offset: 0x0011CF0E
		internal ExchangeObjectVersion MaxExchangeVersion
		{
			get
			{
				if (this.maxExchangeVersion == null && this.maxVersion != null)
				{
					this.maxExchangeVersion = SettingsScopeRestriction.ParseExchangeVersion(this.maxVersion);
				}
				return this.maxExchangeVersion;
			}
		}

		// Token: 0x06004DC8 RID: 19912 RVA: 0x0011ED3D File Offset: 0x0011CF3D
		public static void ValidateNameMatch(string expression)
		{
			SettingsScopeRestriction.CreateRegex(expression);
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x0011ED46 File Offset: 0x0011CF46
		public static void ValidateAsServerVersion(string version)
		{
			SettingsScopeRestriction.ValidateAsServerVersion(SettingsScopeRestriction.ParseServerVersion(version));
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x0011ED53 File Offset: 0x0011CF53
		public static void ValidateAsServerVersion(int version)
		{
			SettingsScopeRestriction.ValidateAsServerVersion(new ServerVersion(version));
		}

		// Token: 0x06004DCB RID: 19915 RVA: 0x0011ED60 File Offset: 0x0011CF60
		public static void ValidateAsServerVersion(ServerVersion serverVersion)
		{
			if (serverVersion.Major < 15)
			{
				throw new ConfigurationSettingsUnsupportedVersionException(serverVersion.ToString());
			}
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x0011ED78 File Offset: 0x0011CF78
		public static void ValidateAsExchangeVersion(string version)
		{
			SettingsScopeRestriction.ValidateAsExchangeVersion(SettingsScopeRestriction.ParseExchangeVersion(version));
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x0011ED85 File Offset: 0x0011CF85
		public static void ValidateAsExchangeVersion(ExchangeObjectVersion exchangeVersion)
		{
			if (exchangeVersion.ExchangeBuild < ExchangeObjectVersion.Exchange2012.ExchangeBuild)
			{
				throw new ConfigurationSettingsUnsupportedVersionException(exchangeVersion.ToString());
			}
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x0011EDAC File Offset: 0x0011CFAC
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.SubType))
			{
				return string.Format("{0}={1}", this.SubType, this.NameMatch);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Match={0}", this.NameMatch);
			if (!string.IsNullOrEmpty(this.MinVersion))
			{
				stringBuilder.AppendFormat(",MinVer={0}", this.MinVersion);
			}
			if (!string.IsNullOrEmpty(this.MaxVersion))
			{
				stringBuilder.AppendFormat(",MaxVer={0}", this.MaxVersion);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x0011EE3C File Offset: 0x0011D03C
		private static Regex CreateRegex(string expression)
		{
			Regex result;
			try
			{
				result = new Regex(expression, RegexOptions.IgnoreCase);
			}
			catch (ArgumentException innerException)
			{
				throw new ConfigurationSettingsInvalidMatchException(expression, innerException);
			}
			return result;
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x0011EE70 File Offset: 0x0011D070
		private static ExchangeObjectVersion ParseExchangeVersion(string version)
		{
			ExchangeObjectVersion result;
			if (!SettingsScopeRestriction.TryParseExchangeVersion(version, out result))
			{
				throw new ConfigurationSettingsUnsupportedVersionException(version);
			}
			return result;
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x0011EE90 File Offset: 0x0011D090
		private static bool TryParseExchangeVersion(string version, out ExchangeObjectVersion exchangeVersion)
		{
			exchangeVersion = null;
			bool result;
			try
			{
				exchangeVersion = ExchangeObjectVersion.Parse(version);
				result = true;
			}
			catch (ArgumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x0011EEC4 File Offset: 0x0011D0C4
		private static ServerVersion ParseServerVersion(string version)
		{
			ServerVersion result;
			if (!SettingsScopeRestriction.TryParseServerVersion(version, out result))
			{
				throw new ConfigurationSettingsUnsupportedVersionException(version);
			}
			return result;
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x0011EEE4 File Offset: 0x0011D0E4
		private static bool TryParseServerVersion(string version, out ServerVersion serverVersion)
		{
			serverVersion = null;
			if (string.IsNullOrEmpty(version))
			{
				return false;
			}
			int versionNumber;
			if (int.TryParse(version, out versionNumber))
			{
				serverVersion = new ServerVersion(versionNumber);
				return true;
			}
			return ServerVersion.TryParseFromSerialNumber(version, out serverVersion);
		}

		// Token: 0x040034E6 RID: 13542
		private string minVersion;

		// Token: 0x040034E7 RID: 13543
		private string maxVersion;

		// Token: 0x040034E8 RID: 13544
		private ExchangeObjectVersion minExchangeVersion;

		// Token: 0x040034E9 RID: 13545
		private ExchangeObjectVersion maxExchangeVersion;

		// Token: 0x040034EA RID: 13546
		private ServerVersion minServerVersion;

		// Token: 0x040034EB RID: 13547
		private ServerVersion maxServerVersion;
	}
}
