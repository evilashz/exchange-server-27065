using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Mapi.Common;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public sealed class MapiEntryId : IEquatable<MapiEntryId>, IComparable<MapiEntryId>, IComparable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static MapiEntryId Parse(string input)
		{
			return new MapiEntryId(input);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public static explicit operator byte[](MapiEntryId source)
		{
			return (byte[])((null == source) ? null : source.binaryEntryId.Clone());
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F6 File Offset: 0x000002F6
		public static bool operator ==(MapiEntryId operand1, MapiEntryId operand2)
		{
			return object.Equals(operand1, operand2);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FF File Offset: 0x000002FF
		public static bool operator !=(MapiEntryId operand1, MapiEntryId operand2)
		{
			return !object.Equals(operand1, operand2);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210C File Offset: 0x0000030C
		public bool Equals(MapiEntryId other)
		{
			if (object.ReferenceEquals(null, other) || this.binaryEntryId.Length != other.binaryEntryId.Length)
			{
				return false;
			}
			if (this.binaryEntryId == other.binaryEntryId)
			{
				return true;
			}
			int num = 0;
			while (this.binaryEntryId.Length > num)
			{
				if (this.binaryEntryId[num] != other.binaryEntryId[num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002170 File Offset: 0x00000370
		public int CompareTo(MapiEntryId other)
		{
			if (null == other)
			{
				return 1;
			}
			int num = 0;
			while (num < this.binaryEntryId.Length && num < other.binaryEntryId.Length)
			{
				if (this.binaryEntryId[num] != other.binaryEntryId[num])
				{
					return (int)(this.binaryEntryId[num] - other.binaryEntryId[num]);
				}
				num++;
			}
			return this.binaryEntryId.Length - other.binaryEntryId.Length;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021DB File Offset: 0x000003DB
		int IComparable.CompareTo(object obj)
		{
			return this.CompareTo(obj as MapiEntryId);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E9 File Offset: 0x000003E9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MapiEntryId);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021F8 File Offset: 0x000003F8
		public override int GetHashCode()
		{
			if (this.hashCode == null)
			{
				this.hashCode = new int?(this.binaryEntryId.Length);
				int num = 0;
				while (this.binaryEntryId.Length > num)
				{
					int num2;
					if ((num2 = (num & 3)) == 0)
					{
						this.hashCode = (this.hashCode << 13 | (int)((uint)this.hashCode.Value >> 19));
					}
					this.hashCode ^= (int)this.binaryEntryId[num] << (num2 << 3);
					num++;
				}
			}
			return this.hashCode.Value;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002300 File Offset: 0x00000500
		public override string ToString()
		{
			if (this.literalEntryId == null)
			{
				StringBuilder stringBuilder = new StringBuilder(this.binaryEntryId.Length << 1);
				int num = 0;
				while (this.binaryEntryId.Length > num)
				{
					stringBuilder.AppendFormat("{0:X2}", this.binaryEntryId[num]);
					num++;
				}
				this.literalEntryId = stringBuilder.ToString();
			}
			return this.literalEntryId;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002363 File Offset: 0x00000563
		public MapiEntryId(byte[] binaryEntryId)
		{
			if (binaryEntryId == null || binaryEntryId.Length == 0)
			{
				throw new FormatException(Strings.ExceptionFormatNotSupported);
			}
			this.binaryEntryId = (byte[])binaryEntryId.Clone();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002394 File Offset: 0x00000594
		public MapiEntryId(string literalEntryId)
		{
			if (literalEntryId == null)
			{
				throw new FormatException(Strings.ExceptionFormatNotSupported);
			}
			literalEntryId = literalEntryId.Trim();
			if (string.IsNullOrEmpty(literalEntryId))
			{
				throw new FormatException(Strings.ExceptionFormatNotSupported);
			}
			if ((1 & literalEntryId.Length) != 0)
			{
				throw new FormatException(Strings.ExceptionFormatNotSupported);
			}
			byte[] array = new byte[literalEntryId.Length >> 1];
			int num = 0;
			while (array.Length > num)
			{
				if (!byte.TryParse(literalEntryId.Substring(num << 1, 2), NumberStyles.HexNumber, null, out array[num]))
				{
					throw new FormatException(Strings.ExceptionFormatNotSupported);
				}
				num++;
			}
			this.literalEntryId = literalEntryId;
			this.binaryEntryId = array;
		}

		// Token: 0x04000001 RID: 1
		private readonly byte[] binaryEntryId;

		// Token: 0x04000002 RID: 2
		private string literalEntryId;

		// Token: 0x04000003 RID: 3
		private int? hashCode;
	}
}
