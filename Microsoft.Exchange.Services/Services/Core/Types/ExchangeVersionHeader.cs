using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000770 RID: 1904
	internal class ExchangeVersionHeader
	{
		// Token: 0x060038D5 RID: 14549 RVA: 0x000C8DDE File Offset: 0x000C6FDE
		public ExchangeVersionHeader(string headerValue)
		{
			this.rawValue = headerValue;
			this.parseError = null;
			this.Parse();
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000C8DFA File Offset: 0x000C6FFA
		public bool IsMissing
		{
			get
			{
				return !this.numericVersionFound && !this.enumVersionFound && this.parseError == null;
			}
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000C8E18 File Offset: 0x000C7018
		public ExchangeVersionType CheckAndGetRequestedVersion()
		{
			if (this.parseError != null)
			{
				throw new InvalidServerVersionException();
			}
			ExchangeVersionType result;
			try
			{
				if (this.enumVersionFound)
				{
					result = this.versionEnum;
				}
				else
				{
					string value;
					if (ExchangeVersionHeader.IsGreaterThanCurrent(this.major, this.minor))
					{
						value = string.Format("V{0}_{1}", ExchangeVersionHeader.MaxSupportedMajor.Member, ExchangeVersionHeader.MaxSupportedMinor.Member);
					}
					else
					{
						value = string.Format("V{0}_{1}", this.major, this.minor);
					}
					ExchangeVersionType exchangeVersionType = (ExchangeVersionType)Enum.Parse(typeof(ExchangeVersionType), value);
					this.CheckMinimumVersion();
					result = exchangeVersionType;
				}
			}
			catch (ArgumentException)
			{
				throw new InvalidServerVersionException();
			}
			return result;
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000C8EE0 File Offset: 0x000C70E0
		private void CheckMinimumVersion()
		{
			if (!this.minimumVersionFound)
			{
				return;
			}
			if (!this.numericVersionFound)
			{
				this.RecordParseError("minimum greater than requested");
				throw new InvalidServerVersionException();
			}
			if (this.minMajor > this.major || (this.minMajor == this.major && this.minMinor > this.minor))
			{
				this.RecordParseError("minimum greater than requested");
				throw new InvalidServerVersionException();
			}
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000C8F4C File Offset: 0x000C714C
		private void Parse()
		{
			if (string.IsNullOrWhiteSpace(this.rawValue))
			{
				return;
			}
			string[] array = this.rawValue.Trim().Split(new char[]
			{
				';'
			});
			switch (array.Length)
			{
			case 0:
				return;
			case 1:
				break;
			case 2:
			{
				string[] array2 = array[1].Trim().Split(new char[]
				{
					'='
				});
				if (array2 != null && array2.Length == 2 && array2[0].Equals("minimum", StringComparison.OrdinalIgnoreCase))
				{
					if (this.TryParseNumericVersion(array2[1].Trim(), out this.minMajor, out this.minMinor))
					{
						this.minimumVersionFound = true;
					}
					else
					{
						this.RecordParseError("second part didn't match x.y");
					}
				}
				else
				{
					this.RecordParseError("second part not recognized as minimum=x.y");
				}
				break;
			}
			default:
				this.RecordParseError("more than 2 semicolon-separated parts");
				return;
			}
			string text = array[0].Trim();
			if (this.TryParseNumericVersion(text, out this.major, out this.minor))
			{
				this.numericVersionFound = true;
				return;
			}
			if (this.TryParseEnumVersion(text, out this.versionEnum))
			{
				this.enumVersionFound = true;
				return;
			}
			this.RecordParseError("first part didn't match x.y or Exchange20xx");
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000C9074 File Offset: 0x000C7274
		private bool TryParseNumericVersion(string version, out int major, out int minor)
		{
			major = 0;
			minor = 0;
			string[] array = version.Split(new char[]
			{
				'.'
			});
			return array.Length == 2 && int.TryParse(array[0], out major) && int.TryParse(array[1], out minor);
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000C90BC File Offset: 0x000C72BC
		private bool TryParseEnumVersion(string v, out ExchangeVersionType enumVersion)
		{
			enumVersion = ExchangeVersionType.Exchange2007;
			bool result;
			try
			{
				if (!v.StartsWith("Exchange", StringComparison.OrdinalIgnoreCase))
				{
					result = false;
				}
				else
				{
					enumVersion = EnumUtilities.Parse<ExchangeVersionType>(v);
					result = (enumVersion <= ExchangeVersionType.Exchange2013);
				}
			}
			catch (ArgumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000C9108 File Offset: 0x000C7308
		private static bool IsGreaterThanCurrent(int major, int minor)
		{
			return major > ExchangeVersionHeader.MaxSupportedMajor.Member || (major == ExchangeVersionHeader.MaxSupportedMajor.Member && minor > ExchangeVersionHeader.MaxSupportedMinor.Member);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000C9135 File Offset: 0x000C7335
		private void RecordParseError(string s)
		{
			this.parseError = s;
		}

		// Token: 0x04001F88 RID: 8072
		private const string MinimumParameterName = "minimum";

		// Token: 0x04001F89 RID: 8073
		private const string ExchangeVersionTypeFormat = "V{0}_{1}";

		// Token: 0x04001F8A RID: 8074
		private const string EnumPrefix = "Exchange";

		// Token: 0x04001F8B RID: 8075
		private const char ParameterSeparator = ';';

		// Token: 0x04001F8C RID: 8076
		private const char MajorMinorSeparator = '.';

		// Token: 0x04001F8D RID: 8077
		private const char ParameterValueSeparator = '=';

		// Token: 0x04001F8E RID: 8078
		internal static LazyMember<string> MaxSupportedVersionString = new LazyMember<string>(() => ExchangeVersionHeader.MaxSupportedMajor.Member + "." + ExchangeVersionHeader.MaxSupportedMinor.Member);

		// Token: 0x04001F8F RID: 8079
		private static LazyMember<int> MaxSupportedMajor = new LazyMember<int>(delegate()
		{
			string text = ExchangeVersion.MaxSupportedVersion.Member.ToString();
			int num = 1;
			int num2 = num;
			while (num2 < text.Length && char.IsDigit(text, num2))
			{
				num2++;
			}
			return int.Parse(text.Substring(num, num2 - num));
		});

		// Token: 0x04001F90 RID: 8080
		private static LazyMember<int> MaxSupportedMinor = new LazyMember<int>(delegate()
		{
			string text = ExchangeVersion.MaxSupportedVersion.Member.ToString();
			int length = text.Length;
			int num = length;
			while (num > 0 && char.IsDigit(text, num - 1))
			{
				num--;
			}
			return int.Parse(text.Substring(num, length - num));
		});

		// Token: 0x04001F91 RID: 8081
		private readonly string rawValue;

		// Token: 0x04001F92 RID: 8082
		private int major;

		// Token: 0x04001F93 RID: 8083
		private int minor;

		// Token: 0x04001F94 RID: 8084
		private int minMajor;

		// Token: 0x04001F95 RID: 8085
		private int minMinor;

		// Token: 0x04001F96 RID: 8086
		private ExchangeVersionType versionEnum;

		// Token: 0x04001F97 RID: 8087
		private bool numericVersionFound;

		// Token: 0x04001F98 RID: 8088
		private bool enumVersionFound;

		// Token: 0x04001F99 RID: 8089
		private bool minimumVersionFound;

		// Token: 0x04001F9A RID: 8090
		private string parseError;
	}
}
