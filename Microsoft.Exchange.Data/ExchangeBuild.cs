using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000007 RID: 7
	[ImmutableObject(true)]
	[Serializable]
	public struct ExchangeBuild : IFormattable, IComparable, IComparable<ExchangeBuild>
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000023F6 File Offset: 0x000005F6
		public ExchangeBuild(byte major, byte minor, ushort build, ushort buildRevision)
		{
			this.Major = major;
			this.Minor = minor;
			this.Build = build;
			this.BuildRevision = (buildRevision & 1023);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000241C File Offset: 0x0000061C
		public ExchangeBuild(long encodedBuildNumber)
		{
			this.Major = (byte)(encodedBuildNumber >> 34 & 255L);
			this.Minor = (byte)(encodedBuildNumber >> 26 & 255L);
			this.Build = (ushort)(encodedBuildNumber >> 10 & 65535L);
			this.BuildRevision = (ushort)(encodedBuildNumber & 1023L);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002470 File Offset: 0x00000670
		public static ExchangeBuild Parse(string versionString)
		{
			if (string.IsNullOrEmpty(versionString))
			{
				throw new ArgumentException(DataStrings.EmptyExchangeBuild, "ExchangeBuild");
			}
			string[] array = versionString.Split(new char[]
			{
				'.'
			});
			if (4 != array.Length)
			{
				throw new ArgumentException(DataStrings.FormatExchangeBuildWrong, "ExchangeBuild");
			}
			byte major;
			if (!byte.TryParse(array[0], out major))
			{
				throw new ArgumentException(DataStrings.FormatExchangeBuildWrong, "ExchangeBuild");
			}
			byte minor;
			if (!byte.TryParse(array[1], out minor))
			{
				throw new ArgumentException(DataStrings.FormatExchangeBuildWrong, "ExchangeBuild");
			}
			ushort build;
			if (!ushort.TryParse(array[2], out build))
			{
				throw new ArgumentException(DataStrings.FormatExchangeBuildWrong, "ExchangeBuild");
			}
			ushort buildRevision;
			if (!ushort.TryParse(array[3], out buildRevision))
			{
				throw new ArgumentException(DataStrings.FormatExchangeBuildWrong, "ExchangeBuild");
			}
			return new ExchangeBuild(major, minor, build, buildRevision);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000255A File Offset: 0x0000075A
		public static bool operator ==(ExchangeBuild objA, ExchangeBuild objB)
		{
			return object.Equals(objA, objB);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000256D File Offset: 0x0000076D
		public static bool operator !=(ExchangeBuild objA, ExchangeBuild objB)
		{
			return !object.Equals(objA, objB);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002583 File Offset: 0x00000783
		public static bool operator >(ExchangeBuild objA, ExchangeBuild objB)
		{
			return objA.CompareTo(objB) > 0;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002590 File Offset: 0x00000790
		public static bool operator >=(ExchangeBuild objA, ExchangeBuild objB)
		{
			return objA.CompareTo(objB) >= 0;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025A0 File Offset: 0x000007A0
		public static bool operator <(ExchangeBuild objA, ExchangeBuild objB)
		{
			return objA.CompareTo(objB) < 0;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025AD File Offset: 0x000007AD
		public static bool operator <=(ExchangeBuild objA, ExchangeBuild objB)
		{
			return objA.CompareTo(objB) <= 0;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025BD File Offset: 0x000007BD
		public long ToInt64()
		{
			return (long)((ulong)this.Major << 34 | (ulong)this.Minor << 26 | (ulong)this.Build << 10 | (ulong)this.BuildRevision);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025E8 File Offset: 0x000007E8
		public int ToExchange2003FormatInt32()
		{
			return ((int)Math.Min(this.Major, 15) << 28 | (int)Math.Min(this.Minor, 15) << 24 | (int)Math.Min(this.BuildRevision, 15) << 20 | (int)this.Build) + 30000;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002634 File Offset: 0x00000834
		public override bool Equals(object obj)
		{
			return obj is ExchangeBuild && this.Equals((ExchangeBuild)obj);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000264C File Offset: 0x0000084C
		public bool Equals(ExchangeBuild other)
		{
			return this.ToInt64() == other.ToInt64();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000265D File Offset: 0x0000085D
		public override int GetHashCode()
		{
			return this.ToExchange2003FormatInt32();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002665 File Offset: 0x00000865
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002670 File Offset: 0x00000870
		public string ToString(string format, IFormatProvider fp)
		{
			string text = null;
			if (format == "N")
			{
				if (this.Major < 8)
				{
					text = DataStrings.Exchange2003.ToString();
				}
				if (this.Major == 8 && this.Minor == 0 && this.Build < 900)
				{
					text = DataStrings.Exchange2007.ToString();
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					this.Major,
					this.Minor,
					this.Build,
					this.BuildRevision
				});
			}
			return text;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000272F File Offset: 0x0000092F
		public bool IsOlderThan(ExchangeBuild other)
		{
			return this.Major < other.Major || (this.Major == other.Major && this.Minor < other.Minor);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002762 File Offset: 0x00000962
		public bool IsNewerThan(ExchangeBuild other)
		{
			return this.Major > other.Major || (this.Major == other.Major && this.Minor > other.Minor);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002795 File Offset: 0x00000995
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is ExchangeBuild))
			{
				throw new ArgumentException(DataStrings.InvalidTypeArgumentException("obj", obj.GetType(), typeof(ExchangeBuild)));
			}
			return this.CompareTo((ExchangeBuild)obj);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027D8 File Offset: 0x000009D8
		public int CompareTo(ExchangeBuild other)
		{
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

		// Token: 0x04000006 RID: 6
		public const ushort MaxBuildRevision = 1023;

		// Token: 0x04000007 RID: 7
		internal const int LegacyBuildOffset = 30000;

		// Token: 0x04000008 RID: 8
		public readonly byte Major;

		// Token: 0x04000009 RID: 9
		public readonly byte Minor;

		// Token: 0x0400000A RID: 10
		public readonly ushort Build;

		// Token: 0x0400000B RID: 11
		public readonly ushort BuildRevision;
	}
}
