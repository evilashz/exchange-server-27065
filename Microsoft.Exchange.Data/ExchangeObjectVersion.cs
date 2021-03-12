using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000008 RID: 8
	[ImmutableObject(true)]
	[Serializable]
	public class ExchangeObjectVersion : IComparable, IComparable<ExchangeObjectVersion>
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002803 File Offset: 0x00000A03
		public ExchangeObjectVersion(long encodedVersion)
		{
			this.Major = (byte)(encodedVersion >> 50 & 255L);
			this.Minor = (byte)(encodedVersion >> 42 & 255L);
			this.ExchangeBuild = new ExchangeBuild(encodedVersion);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000283B File Offset: 0x00000A3B
		public ExchangeObjectVersion(byte major, byte minor, byte majorBuild, byte minorBuild, ushort build, ushort buildRevision) : this(major, minor, new ExchangeBuild(majorBuild, minorBuild, build, buildRevision))
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002851 File Offset: 0x00000A51
		public ExchangeObjectVersion(byte major, byte minor, ExchangeBuild exchangeBuild)
		{
			this.Major = major;
			this.Minor = minor;
			this.ExchangeBuild = exchangeBuild;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000286E File Offset: 0x00000A6E
		public static ExchangeObjectVersion Current
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002875 File Offset: 0x00000A75
		public ExchangeObjectVersion NextMajorVersion
		{
			get
			{
				return new ExchangeObjectVersion(this.Major + 1, 0, 0, 0, 0, 0);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000288A File Offset: 0x00000A8A
		public ExchangeObjectVersion NextMinorVersion
		{
			get
			{
				return new ExchangeObjectVersion(this.Major, this.Minor + 1, 0, 0, 0, 0);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000028A4 File Offset: 0x00000AA4
		private int MajorMinor
		{
			get
			{
				return (int)this.Major << 8 | (int)this.Minor;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028B5 File Offset: 0x00000AB5
		public static bool operator ==(ExchangeObjectVersion objA, ExchangeObjectVersion objB)
		{
			return object.Equals(objA, objB);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028BE File Offset: 0x00000ABE
		public static bool operator !=(ExchangeObjectVersion objA, ExchangeObjectVersion objB)
		{
			return !object.Equals(objA, objB);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028CC File Offset: 0x00000ACC
		public static ExchangeObjectVersion Parse(string input)
		{
			if (input == null || string.IsNullOrEmpty(input = input.Trim()))
			{
				throw new ArgumentException(DataStrings.EmptyExchangeObjectVersion, "input");
			}
			long encodedVersion = 0L;
			if (long.TryParse(input, out encodedVersion))
			{
				return new ExchangeObjectVersion(encodedVersion);
			}
			if (ExchangeObjectVersion.parsingRegex == null)
			{
				lock (ExchangeObjectVersion.parsingRegexInitializationLock)
				{
					if (ExchangeObjectVersion.parsingRegex == null)
					{
						ExchangeObjectVersion.parsingRegex = new Regex("^(?<major>\\d{1,3})\\.(?<minor>\\d{1,3})\\s*\\((?<exbuild>.+)\\)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
					}
				}
			}
			Match match = ExchangeObjectVersion.parsingRegex.Match(input);
			if (!match.Success)
			{
				throw new ArgumentException(DataStrings.InvalidFormatExchangeObjectVersion);
			}
			byte major = 0;
			if (!byte.TryParse(match.Groups["major"].Value, out major))
			{
				throw new ArgumentException(DataStrings.InvalidFormatExchangeObjectVersion);
			}
			byte minor = 0;
			if (!byte.TryParse(match.Groups["minor"].Value, out minor))
			{
				throw new ArgumentException(DataStrings.InvalidFormatExchangeObjectVersion);
			}
			ExchangeBuild exchangeBuild = ExchangeBuild.Parse(match.Groups["exbuild"].Value);
			return new ExchangeObjectVersion(major, minor, exchangeBuild.Major, exchangeBuild.Minor, exchangeBuild.Build, exchangeBuild.BuildRevision);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A28 File Offset: 0x00000C28
		public long ToInt64()
		{
			return (long)((ulong)this.Major << 50 | (ulong)this.Minor << 42 | (ulong)this.ExchangeBuild.ToInt64());
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A59 File Offset: 0x00000C59
		public override bool Equals(object other)
		{
			return other is ExchangeObjectVersion && this.ToInt64() == ((ExchangeObjectVersion)other).ToInt64();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A78 File Offset: 0x00000C78
		public override int GetHashCode()
		{
			return this.ToInt64().GetHashCode();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A93 File Offset: 0x00000C93
		public bool IsOlderThan(ExchangeObjectVersion other)
		{
			if (null == other)
			{
				throw new ArgumentNullException("other");
			}
			return this.MajorMinor < other.MajorMinor;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002AB7 File Offset: 0x00000CB7
		public bool IsNewerThan(ExchangeObjectVersion other)
		{
			if (null == other)
			{
				throw new ArgumentNullException("other");
			}
			return this.MajorMinor > other.MajorMinor;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002ADB File Offset: 0x00000CDB
		public bool IsSameVersion(ExchangeObjectVersion other)
		{
			if (null == other)
			{
				throw new ArgumentNullException("other");
			}
			return this.MajorMinor == other.MajorMinor;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B00 File Offset: 0x00000D00
		public override string ToString()
		{
			return string.Format("{0}.{1} ({2})", this.Major, this.Minor, this.ExchangeBuild.ToString());
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B41 File Offset: 0x00000D41
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is ExchangeObjectVersion))
			{
				throw new ArgumentException(DataStrings.InvalidTypeArgumentException("obj", obj.GetType(), typeof(ExchangeObjectVersion)));
			}
			return this.CompareTo((ExchangeObjectVersion)obj);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B84 File Offset: 0x00000D84
		public int CompareTo(ExchangeObjectVersion other)
		{
			if (null == other)
			{
				return 1;
			}
			long num = this.ToInt64() - other.ToInt64();
			if (0L == num)
			{
				return 0;
			}
			if (0L <= num)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x0400000C RID: 12
		private const string GroupNameMajorInRegex = "major";

		// Token: 0x0400000D RID: 13
		private const string GroupNameMinorInRegex = "minor";

		// Token: 0x0400000E RID: 14
		private const string GroupNameExBuildInRegex = "exbuild";

		// Token: 0x0400000F RID: 15
		private const string ParsingRegexPattern = "^(?<major>\\d{1,3})\\.(?<minor>\\d{1,3})\\s*\\((?<exbuild>.+)\\)$";

		// Token: 0x04000010 RID: 16
		public static readonly ExchangeObjectVersion Exchange2003 = new ExchangeObjectVersion(0, 0, 6, 5, 6500, 0);

		// Token: 0x04000011 RID: 17
		public static readonly ExchangeObjectVersion Exchange2007 = new ExchangeObjectVersion(0, 1, 8, 0, 535, 0);

		// Token: 0x04000012 RID: 18
		public static readonly ExchangeObjectVersion Exchange2010 = new ExchangeObjectVersion(0, 10, 14, 0, 100, 0);

		// Token: 0x04000013 RID: 19
		public static readonly ExchangeObjectVersion Exchange2012 = new ExchangeObjectVersion(0, 20, 15, 0, 0, 0);

		// Token: 0x04000014 RID: 20
		public static readonly ExchangeObjectVersion Maximum = new ExchangeObjectVersion(0, byte.MaxValue, byte.MaxValue, 0, 0, 0);

		// Token: 0x04000015 RID: 21
		public readonly byte Major;

		// Token: 0x04000016 RID: 22
		public readonly byte Minor;

		// Token: 0x04000017 RID: 23
		public readonly ExchangeBuild ExchangeBuild;

		// Token: 0x04000018 RID: 24
		private static Regex parsingRegex;

		// Token: 0x04000019 RID: 25
		private static object parsingRegexInitializationLock = new object();
	}
}
