using System;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200002B RID: 43
	public sealed class MapiHttpVersion : IComparable
	{
		// Token: 0x0600014A RID: 330 RVA: 0x0000861B File Offset: 0x0000681B
		public MapiHttpVersion(ushort major, ushort minor, ushort buildMajor, ushort buildMinor)
		{
			this.Major = major;
			this.Minor = minor;
			this.BuildMajor = buildMajor;
			this.BuildMinor = buildMinor;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00008640 File Offset: 0x00006840
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00008648 File Offset: 0x00006848
		public ushort Major { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00008651 File Offset: 0x00006851
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00008659 File Offset: 0x00006859
		public ushort Minor { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00008662 File Offset: 0x00006862
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000866A File Offset: 0x0000686A
		public ushort BuildMajor { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00008673 File Offset: 0x00006873
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000867B File Offset: 0x0000687B
		public ushort BuildMinor { get; private set; }

		// Token: 0x06000153 RID: 339 RVA: 0x00008684 File Offset: 0x00006884
		public static bool TryParse(string versionString, out MapiHttpVersion version)
		{
			version = null;
			string[] array = versionString.Split(new char[]
			{
				'.'
			});
			if (array.Length != 4)
			{
				return false;
			}
			ushort[] array2 = new ushort[4];
			for (int i = 0; i < array.Length; i++)
			{
				if (!ushort.TryParse(array[i], out array2[i]))
				{
					return false;
				}
			}
			version = new MapiHttpVersion(array2[0], array2[1], array2[2], array2[3]);
			return true;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000086EC File Offset: 0x000068EC
		public static bool operator ==(MapiHttpVersion v1, MapiHttpVersion v2)
		{
			return object.ReferenceEquals(v1, v2) || (v1 != null && v2 != null && v1.CompareTo(v2) == 0);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000870B File Offset: 0x0000690B
		public static bool operator !=(MapiHttpVersion v1, MapiHttpVersion v2)
		{
			return !object.ReferenceEquals(v1, v2) && (v1 == null || v2 == null || v1.CompareTo(v2) != 0);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000872D File Offset: 0x0000692D
		public static bool operator <(MapiHttpVersion v1, MapiHttpVersion v2)
		{
			return v1.CompareTo(v2) < 0;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008739 File Offset: 0x00006939
		public static bool operator >(MapiHttpVersion v1, MapiHttpVersion v2)
		{
			return v1.CompareTo(v2) > 0;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008748 File Offset: 0x00006948
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				this.Major,
				this.Minor,
				this.BuildMajor,
				this.BuildMinor
			});
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000087A0 File Offset: 0x000069A0
		public ushort[] ToQuartet()
		{
			return new ushort[]
			{
				this.Major,
				this.Minor,
				this.BuildMajor,
				this.BuildMinor
			};
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000087D9 File Offset: 0x000069D9
		public override int GetHashCode()
		{
			return (int)this.Major << 24 | (int)this.Minor << 16 | (int)this.BuildMajor << 8 | (int)this.BuildMinor;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000087FE File Offset: 0x000069FE
		public long GetComparableValue()
		{
			return (long)((int)this.Major << 16 | (int)this.Minor | (int)this.BuildMajor << 16 | (int)this.BuildMinor);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008822 File Offset: 0x00006A22
		public override bool Equals(object obj)
		{
			return obj is MapiHttpVersion && this.CompareTo(obj) == 0;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008838 File Offset: 0x00006A38
		public int CompareTo(object obj)
		{
			if (!(obj is MapiHttpVersion))
			{
				throw new ArgumentException("A MapiHttpVersion object is required for comparison.");
			}
			long comparableValue = this.GetComparableValue();
			long comparableValue2 = ((MapiHttpVersion)obj).GetComparableValue();
			if (comparableValue < comparableValue2)
			{
				return -1;
			}
			if (comparableValue > comparableValue2)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x040000D0 RID: 208
		public static readonly MapiHttpVersion Version14 = new MapiHttpVersion(14, 0, 0, 0);

		// Token: 0x040000D1 RID: 209
		public static readonly MapiHttpVersion Version15 = new MapiHttpVersion(15, 0, 0, 0);
	}
}
