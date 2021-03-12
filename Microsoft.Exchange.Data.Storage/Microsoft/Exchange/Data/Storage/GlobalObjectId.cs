using System;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000843 RID: 2115
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GlobalObjectId : IEquatable<GlobalObjectId>
	{
		// Token: 0x06004E7A RID: 20090 RVA: 0x0014892C File Offset: 0x00146B2C
		public GlobalObjectId()
		{
			this.SetData(Guid.NewGuid().ToByteArray(), true);
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x00148953 File Offset: 0x00146B53
		public GlobalObjectId(byte[] globalObjectIdBytes)
		{
			this.SetBytes(globalObjectIdBytes);
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x00148964 File Offset: 0x00146B64
		public GlobalObjectId(string uid)
		{
			Util.ThrowOnNullOrEmptyArgument(uid, "uid");
			if (uid.StartsWith("040000008200E00074C5B7101A82E008"))
			{
				byte[] bytes = GlobalObjectId.HexStringToByteArray(uid);
				this.SetBytes(bytes);
				return;
			}
			string text = "vCal-Uid\u0001\0\0\0" + uid;
			this.SetData(GlobalObjectId.StringToByteArray(text, true), false);
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x001489B8 File Offset: 0x00146BB8
		public GlobalObjectId(Item item)
		{
			byte[] valueOrDefault = item.GetValueOrDefault<byte[]>(InternalSchema.GlobalObjectId);
			if (valueOrDefault == null)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidGlobalObjectId);
			}
			this.SetBytes(valueOrDefault);
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06004E7E RID: 20094 RVA: 0x001489EC File Offset: 0x00146BEC
		public string Uid
		{
			get
			{
				if (this.IsForeignUid())
				{
					int num = 40 + "vCal-Uid\u0001\0\0\0".Length;
					int num2 = this.globalObjectIdBytes.Length;
					int num3 = num2 - num;
					if (this.globalObjectIdBytes[num2 - 1] == 0)
					{
						num3--;
					}
					if (num3 > 0)
					{
						byte[] array = new byte[num3];
						Array.Copy(this.globalObjectIdBytes, num, array, 0, num3);
						return GlobalObjectId.ByteArrayToString(array);
					}
				}
				return GlobalObjectId.ByteArrayToHexString(this.globalObjectIdBytes);
			}
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06004E7F RID: 20095 RVA: 0x00148A58 File Offset: 0x00146C58
		// (set) Token: 0x06004E80 RID: 20096 RVA: 0x00148A60 File Offset: 0x00146C60
		public ExDateTime Date
		{
			get
			{
				return this.date;
			}
			set
			{
				this.date = value;
				GlobalObjectId.SetDateInBytes(this.globalObjectIdBytes, value);
			}
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06004E81 RID: 20097 RVA: 0x00148A75 File Offset: 0x00146C75
		public byte[] Bytes
		{
			get
			{
				if (this.globalObjectIdBytes == null)
				{
					return null;
				}
				return (byte[])this.globalObjectIdBytes.Clone();
			}
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x00148A94 File Offset: 0x00146C94
		public bool IsCleanGlobalObjectId
		{
			get
			{
				return ExDateTime.MinValue.Equals(this.date);
			}
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06004E83 RID: 20099 RVA: 0x00148AB4 File Offset: 0x00146CB4
		public byte[] CleanGlobalObjectIdBytes
		{
			get
			{
				byte[] bytes = this.Bytes;
				GlobalObjectId.SetDateInBytes(bytes, ExDateTime.MinValue);
				return bytes;
			}
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x00148AD4 File Offset: 0x00146CD4
		public static bool TryParse(string uid, out GlobalObjectId goid)
		{
			goid = null;
			if (!string.IsNullOrEmpty(uid))
			{
				try
				{
					goid = new GlobalObjectId(uid);
				}
				catch (CorruptDataException)
				{
				}
			}
			return goid != null;
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x00148B14 File Offset: 0x00146D14
		public static bool CompareCleanGlobalObjectIds(byte[] id1, byte[] id2)
		{
			return id1.Length == id2.Length && GlobalObjectId.CompareByteArrays(id1, id2, 0, Math.Min(id1.Length, 16)) && GlobalObjectId.CompareByteArrays(id1, id2, 20, id1.Length);
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x00148B40 File Offset: 0x00146D40
		public static bool Equals(GlobalObjectId id1, GlobalObjectId id2)
		{
			byte[] array = id1.globalObjectIdBytes;
			byte[] array2 = id2.globalObjectIdBytes;
			return array.Length == array2.Length && GlobalObjectId.CompareByteArrays(array, array2, 0, array.Length);
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x00148B70 File Offset: 0x00146D70
		public static string ByteArrayToHexString(byte[] array)
		{
			if (array == null)
			{
				return null;
			}
			byte[] array2 = new byte[array.Length * 2];
			int num = 0;
			foreach (byte b in array)
			{
				array2[num++] = GlobalObjectId.NibbleToHex[b >> 4];
				array2[num++] = GlobalObjectId.NibbleToHex[(int)(b & 15)];
			}
			return CTSGlobals.AsciiEncoding.GetString(array2, 0, array2.Length);
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x00148BD8 File Offset: 0x00146DD8
		public override string ToString()
		{
			if (this.globalObjectIdBytes == null)
			{
				return string.Empty;
			}
			return GlobalObjectId.ByteArrayToHexString(this.globalObjectIdBytes);
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x00148BF3 File Offset: 0x00146DF3
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GlobalObjectId);
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x00148C04 File Offset: 0x00146E04
		public override int GetHashCode()
		{
			byte[] array = new byte[4];
			for (int i = 0; i < this.globalObjectIdBytes.Length; i++)
			{
				byte[] array2 = array;
				int num = i % 4;
				array2[num] ^= this.globalObjectIdBytes[i];
			}
			int num2 = 0;
			for (int j = 0; j < 4; j++)
			{
				num2 |= (int)array[j] << 8 * j;
			}
			return num2;
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x00148C65 File Offset: 0x00146E65
		public bool Equals(GlobalObjectId other)
		{
			return other != null && GlobalObjectId.Equals(this, other);
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x00148C73 File Offset: 0x00146E73
		internal static bool HasInstanceDate(byte[] globalObjectIdBytes)
		{
			return globalObjectIdBytes != null && globalObjectIdBytes.Length >= 20;
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x00148C84 File Offset: 0x00146E84
		internal static void MakeGlobalObjectIdBytesToClean(byte[] globalObjectIdBytes)
		{
			if (globalObjectIdBytes != null)
			{
				GlobalObjectId.SetDateInBytes(globalObjectIdBytes, ExDateTime.MinValue);
			}
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x00148C94 File Offset: 0x00146E94
		internal static byte[] HexStringToByteArray(string value)
		{
			if (value.Length % 2 != 0)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidHexString(value));
			}
			int num = value.Length / 2;
			byte[] array = new byte[num];
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				byte b = GlobalObjectId.NumFromHex(value[num2++]);
				byte b2 = GlobalObjectId.NumFromHex(value[num2++]);
				array[i] = (byte)((int)b << 4 | (int)b2);
			}
			return array;
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x00148D05 File Offset: 0x00146F05
		internal static byte[] StringToByteArray(string text)
		{
			return GlobalObjectId.StringToByteArray(text, false);
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x00148D10 File Offset: 0x00146F10
		internal void SetData(byte[] data, bool setDateTime)
		{
			int num = 40 + data.Length;
			this.globalObjectIdBytes = new byte[num];
			this.nextByteIndex = 0;
			this.Append(GlobalObjectId.SPlusGuid);
			this.SkipBytes(4);
			if (setDateTime)
			{
				long num2 = ExDateTime.UtcNow.ToFileTime();
				byte[] bytes = BitConverter.GetBytes((uint)num2);
				byte[] bytes2 = BitConverter.GetBytes((uint)(num2 >> 32));
				this.Append(bytes);
				this.Append(bytes2);
				this.SkipBytes(8);
			}
			else
			{
				this.SkipBytes(16);
			}
			this.Append(BitConverter.GetBytes(data.Length));
			this.Append(data);
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x00148DA4 File Offset: 0x00146FA4
		internal bool IsForeignUid()
		{
			if (this.globalObjectIdBytes == null)
			{
				return false;
			}
			int num = 40 + GlobalObjectId.UidStampArray.Length;
			return this.globalObjectIdBytes.Length >= num && this.globalObjectIdBytes.Skip(40).Take(GlobalObjectId.UidStampArray.Length).SequenceEqual(GlobalObjectId.UidStampArray);
		}

		// Token: 0x06004E92 RID: 20114 RVA: 0x00148DFC File Offset: 0x00146FFC
		private static bool CompareByteArrays(byte[] array1, byte[] array2, int startIndex, int endIndex)
		{
			for (int i = startIndex; i < endIndex; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x00148E20 File Offset: 0x00147020
		private static void SetDateInBytes(byte[] bytes, ExDateTime date)
		{
			if (date != ExDateTime.MinValue)
			{
				bytes[16] = (byte)(date.Year >> 8);
				bytes[17] = (byte)(date.Year & 255);
				bytes[18] = (byte)date.Month;
				bytes[19] = (byte)date.Day;
				return;
			}
			bytes[16] = 0;
			bytes[17] = 0;
			bytes[18] = 0;
			bytes[19] = 0;
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x00148E88 File Offset: 0x00147088
		private static byte NumFromHex(char ch)
		{
			byte b = (ch < '\u0080') ? GlobalObjectId.HexCharToNum[(int)ch] : byte.MaxValue;
			if (b != 255)
			{
				return b;
			}
			throw new CorruptDataException(ServerStrings.ExInvalidHexCharacter(ch));
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x00148EC4 File Offset: 0x001470C4
		private static byte[] StringToByteArray(string text, bool needNullTerminated)
		{
			byte[] array;
			if (needNullTerminated)
			{
				array = new byte[text.Length + 1];
				array[text.Length] = 0;
			}
			else
			{
				array = new byte[text.Length];
			}
			try
			{
				for (int i = 0; i < text.Length; i++)
				{
					array[i] = Convert.ToByte(text[i]);
				}
			}
			catch (OverflowException innerException)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidGlobalObjectId, innerException);
			}
			return array;
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x00148F3C File Offset: 0x0014713C
		private static string ByteArrayToString(byte[] bytes)
		{
			if (bytes != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					if (bytes[i] == 0)
					{
						throw new CorruptDataException(ServerStrings.ExInvalidGlobalObjectId);
					}
					stringBuilder.Append(Convert.ToChar(bytes[i]));
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x00148F8B File Offset: 0x0014718B
		private static void CheckGlobalObjectId(byte[] globalObjectIdBytes)
		{
			if (globalObjectIdBytes == null || globalObjectIdBytes.Length < 40)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidGlobalObjectId);
			}
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x00148FA4 File Offset: 0x001471A4
		private void SetBytes(byte[] globalObjectIdBytes)
		{
			GlobalObjectId.CheckGlobalObjectId(globalObjectIdBytes);
			this.globalObjectIdBytes = (byte[])globalObjectIdBytes.Clone();
			int num = (int)this.globalObjectIdBytes[16] << 8 | (int)this.globalObjectIdBytes[17];
			int num2 = (int)this.globalObjectIdBytes[18];
			int num3 = (int)this.globalObjectIdBytes[19];
			if (num == 0 && num2 == 0)
			{
				if (num3 == 0)
				{
					goto IL_6C;
				}
			}
			try
			{
				this.date = new ExDateTime(ExTimeZone.UnspecifiedTimeZone, num, num2, num3);
				return;
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidGlobalObjectId, innerException);
			}
			IL_6C:
			this.date = ExDateTime.MinValue;
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x00149038 File Offset: 0x00147238
		private void Append(byte[] bytes)
		{
			Array.Copy(bytes, 0, this.globalObjectIdBytes, this.nextByteIndex, bytes.Length);
			this.nextByteIndex += bytes.Length;
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x00149060 File Offset: 0x00147260
		private void SkipBytes(int bytesToSkip)
		{
			this.nextByteIndex += bytesToSkip;
		}

		// Token: 0x04002AD1 RID: 10961
		private const string SPlusGuidAsString = "040000008200E00074C5B7101A82E008";

		// Token: 0x04002AD2 RID: 10962
		private const string UidStamp = "vCal-Uid\u0001\0\0\0";

		// Token: 0x04002AD3 RID: 10963
		private const int ClsidStart = 0;

		// Token: 0x04002AD4 RID: 10964
		private const int InstanceDateStart = 16;

		// Token: 0x04002AD5 RID: 10965
		private const int NowStart = 20;

		// Token: 0x04002AD6 RID: 10966
		private const int DataLenStart = 36;

		// Token: 0x04002AD7 RID: 10967
		private const int ClsidSize = 16;

		// Token: 0x04002AD8 RID: 10968
		private const int InstanceDateSize = 4;

		// Token: 0x04002AD9 RID: 10969
		private const int NowSize = 16;

		// Token: 0x04002ADA RID: 10970
		private const int DataLenSize = 4;

		// Token: 0x04002ADB RID: 10971
		private const int HeaderSize = 40;

		// Token: 0x04002ADC RID: 10972
		private static readonly byte[] NibbleToHex = CTSGlobals.AsciiEncoding.GetBytes("0123456789ABCDEF");

		// Token: 0x04002ADD RID: 10973
		private static readonly byte[] HexCharToNum = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			10,
			11,
			12,
			13,
			14,
			15,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			10,
			11,
			12,
			13,
			14,
			15,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x04002ADE RID: 10974
		private static readonly byte[] UidStampArray = GlobalObjectId.StringToByteArray("vCal-Uid\u0001\0\0\0");

		// Token: 0x04002ADF RID: 10975
		private static readonly byte[] SPlusGuid = new byte[]
		{
			4,
			0,
			0,
			0,
			130,
			0,
			224,
			0,
			116,
			197,
			183,
			16,
			26,
			130,
			224,
			8
		};

		// Token: 0x04002AE0 RID: 10976
		private byte[] globalObjectIdBytes;

		// Token: 0x04002AE1 RID: 10977
		private ExDateTime date;

		// Token: 0x04002AE2 RID: 10978
		private int nextByteIndex;
	}
}
