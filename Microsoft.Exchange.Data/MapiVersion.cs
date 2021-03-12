using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200023A RID: 570
	internal struct MapiVersion : IEquatable<MapiVersion>, IComparable<MapiVersion>
	{
		// Token: 0x06001397 RID: 5015 RVA: 0x0003BEF8 File Offset: 0x0003A0F8
		public MapiVersion(ushort productMajor, ushort productMinor, ushort buildMajor, ushort buildMinor)
		{
			this.value = MapiVersion.EnsureConvertible(productMajor, productMinor, buildMajor, buildMinor);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0003BF0C File Offset: 0x0003A10C
		public MapiVersion(ushort s0, ushort s1, ushort s2)
		{
			ushort productMajor;
			ushort productMinor;
			ushort buildMajor;
			if ((s1 & 32768) != 0)
			{
				productMajor = (ushort)(s0 >> 8);
				productMinor = (s0 & 255);
				buildMajor = (s1 & 32767);
			}
			else
			{
				productMajor = s0;
				productMinor = 0;
				buildMajor = s1;
			}
			this.value = MapiVersion.EnsureConvertible(productMajor, productMinor, buildMajor, s2);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0003BF50 File Offset: 0x0003A150
		private MapiVersion(ulong internalValue)
		{
			this.value = internalValue;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0003BF5C File Offset: 0x0003A15C
		public static MapiVersion Parse(string wireFormat)
		{
			string[] array = wireFormat.Split(new char[]
			{
				'.'
			});
			if (array.Length != 3)
			{
				throw new FormatException("Version specification should have 3 parts");
			}
			ushort[] array2 = new ushort[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!ushort.TryParse(array[i], out array2[i]))
				{
					throw new ArgumentOutOfRangeException("wireFormat", array[i], "Version number part should be between 0 and 65535");
				}
			}
			return new MapiVersion(MapiVersion.EnsureConvertible(array2[0], 0, array2[1], array2[2]));
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0003BFDD File Offset: 0x0003A1DD
		internal ulong Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0003BFE5 File Offset: 0x0003A1E5
		public static bool operator ==(MapiVersion left, MapiVersion right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0003BFEF File Offset: 0x0003A1EF
		public static bool operator !=(MapiVersion left, MapiVersion right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0003BFFC File Offset: 0x0003A1FC
		public static bool operator <(MapiVersion left, MapiVersion right)
		{
			return left.value < right.value;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0003C00E File Offset: 0x0003A20E
		public static bool operator <=(MapiVersion left, MapiVersion right)
		{
			return left.value <= right.value;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0003C023 File Offset: 0x0003A223
		public static bool operator >=(MapiVersion left, MapiVersion right)
		{
			return left.value >= right.value;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0003C038 File Offset: 0x0003A238
		public static bool operator >(MapiVersion left, MapiVersion right)
		{
			return left.value > right.value;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0003C04A File Offset: 0x0003A24A
		public override string ToString()
		{
			return MapiVersion.ConvertToQuartetString(this.value);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0003C057 File Offset: 0x0003A257
		public override bool Equals(object obj)
		{
			return obj is MapiVersion && this.Equals((MapiVersion)obj);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0003C070 File Offset: 0x0003A270
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0003C08C File Offset: 0x0003A28C
		public ushort[] ToTriplet()
		{
			ushort[] array = this.ToQuartet();
			return new ushort[]
			{
				(ushort)((int)array[0] << 8 | (int)array[1]),
				array[2] | 32768,
				array[3]
			};
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0003C0C8 File Offset: 0x0003A2C8
		public ushort[] ToQuartet()
		{
			return MapiVersion.ToQuartet(this.value);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0003C0D8 File Offset: 0x0003A2D8
		internal static string ConvertToQuartetString(ulong versionValue)
		{
			ushort[] array = MapiVersion.ToQuartet(versionValue);
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				array[0],
				array[1],
				array[2],
				array[3]
			});
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0003C12C File Offset: 0x0003A32C
		private static ushort[] ToQuartet(ulong value)
		{
			return new ushort[]
			{
				(ushort)(value >> 48),
				(ushort)(value >> 32 & 65535UL),
				(ushort)(value >> 16 & 65535UL),
				(ushort)(value & 65535UL)
			};
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0003C173 File Offset: 0x0003A373
		private static ulong EnsureConvertible(ushort productMajor, ushort productMinor, ushort buildMajor, ushort buildMinor)
		{
			if (productMajor < 128 && productMinor < 256 && buildMajor < 32768)
			{
				return (ulong)productMajor << 48 | (ulong)productMinor << 32 | (ulong)buildMajor << 16 | (ulong)buildMinor;
			}
			throw new ArgumentOutOfRangeException("value", "One or more of the version parts is out of range. Constraints are: productMajor < 128, productMinor < 256, buildMajor < 32768");
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0003C1B1 File Offset: 0x0003A3B1
		public bool Equals(MapiVersion other)
		{
			return this.value == other.value;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0003C1C4 File Offset: 0x0003A3C4
		public int CompareTo(MapiVersion other)
		{
			return this.value.CompareTo(other.Value);
		}

		// Token: 0x04000B7E RID: 2942
		private const ulong VerificationMask = 18410995654303154176UL;

		// Token: 0x04000B7F RID: 2943
		public static MapiVersion Min = new MapiVersion(0, 0, 0, 0);

		// Token: 0x04000B80 RID: 2944
		public static MapiVersion Max = new MapiVersion(127, 255, 32767, ushort.MaxValue);

		// Token: 0x04000B81 RID: 2945
		public static MapiVersion Outlook11 = new MapiVersion(11, 0, 0, 0);

		// Token: 0x04000B82 RID: 2946
		public static MapiVersion Outlook12 = new MapiVersion(12, 0, 0, 0);

		// Token: 0x04000B83 RID: 2947
		public static MapiVersion Outlook14 = new MapiVersion(14, 0, 0, 0);

		// Token: 0x04000B84 RID: 2948
		public static MapiVersion Outlook15 = new MapiVersion(15, 0, 0, 0);

		// Token: 0x04000B85 RID: 2949
		public static MapiVersion MRS14SP1 = new MapiVersion(14, 1, 180, 1);

		// Token: 0x04000B86 RID: 2950
		private readonly ulong value;
	}
}
