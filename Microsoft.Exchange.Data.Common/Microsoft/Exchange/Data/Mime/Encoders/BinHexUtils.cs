using System;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x02000092 RID: 146
	internal class BinHexUtils
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x000227CC File Offset: 0x000209CC
		private BinHexUtils()
		{
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000227D4 File Offset: 0x000209D4
		internal static ushort CalculateHeaderCrc(byte[] bytes, int count)
		{
			ushort num = 0;
			for (int i = 0; i < count; i++)
			{
				byte b = bytes[i];
				for (int j = 0; j < 8; j++)
				{
					ushort num2 = num & 32768;
					num = (ushort)((int)num << 1 | b >> 7);
					if (num2 != 0)
					{
						num ^= 4129;
					}
					b = (byte)(b << 1);
				}
			}
			for (int k = 0; k < 2; k++)
			{
				for (int l = 0; l < 8; l++)
				{
					ushort num3 = num & 32768;
					num = (ushort)(num << 1);
					if (num3 != 0)
					{
						num ^= 4129;
					}
				}
			}
			return num;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00022864 File Offset: 0x00020A64
		internal static ushort CalculateCrc(byte[] bytes, int index, int size, ushort seed)
		{
			ushort num = seed;
			for (int i = index; i < index + size; i++)
			{
				byte b = bytes[i];
				for (int j = 0; j < 8; j++)
				{
					ushort num2 = num & 32768;
					num = (ushort)((int)num << 1 | b >> 7);
					if (num2 != 0)
					{
						num ^= 4129;
					}
					b = (byte)(b << 1);
				}
			}
			return num;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000228B8 File Offset: 0x00020AB8
		internal static ushort CalculateCrc(byte data, int count, ushort seed)
		{
			ushort num = seed;
			while (0 < count--)
			{
				byte b = data;
				for (int i = 0; i < 8; i++)
				{
					ushort num2 = num & 32768;
					num = (ushort)((int)num << 1 | b >> 7);
					if (num2 != 0)
					{
						num ^= 4129;
					}
					b = (byte)(b << 1);
				}
			}
			return num;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00022904 File Offset: 0x00020B04
		internal static ushort CalculateCrc(ushort seed)
		{
			byte[] array = new byte[2];
			byte[] array2 = array;
			return BinHexUtils.CalculateCrc(array2, 0, array2.Length, seed);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00022928 File Offset: 0x00020B28
		internal static int MarshalInt32(byte[] array, int offset, long value)
		{
			array[offset] = (byte)((value & (long)((ulong)-16777216)) >> 24);
			array[offset + 1] = (byte)((value & 16711680L) >> 16);
			array[offset + 2] = (byte)((value & 65280L) >> 8);
			array[offset + 3] = (byte)(value & 255L);
			return 4;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00022974 File Offset: 0x00020B74
		internal static int UnmarshalInt32(byte[] array, int index)
		{
			uint num = (uint)array[index];
			num = (num << 8 | (uint)array[index + 1]);
			num = (num << 8 | (uint)array[index + 2]);
			return (int)(num << 8 | (uint)array[index + 3]);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000229A4 File Offset: 0x00020BA4
		internal static int MarshalUInt16(byte[] array, int offset, ushort value)
		{
			array[offset] = (byte)((value & 65280) >> 8);
			array[offset + 1] = (byte)(value & 255);
			return 2;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000229C4 File Offset: 0x00020BC4
		internal static ushort UnmarshalUInt16(byte[] array, int index)
		{
			uint num = (uint)array[index];
			num = (num << 8 | (uint)array[index + 1]);
			return (ushort)num;
		}

		// Token: 0x02000093 RID: 147
		internal enum State
		{
			// Token: 0x04000471 RID: 1137
			Starting,
			// Token: 0x04000472 RID: 1138
			Started,
			// Token: 0x04000473 RID: 1139
			Prefix,
			// Token: 0x04000474 RID: 1140
			HdrFileSize,
			// Token: 0x04000475 RID: 1141
			Header,
			// Token: 0x04000476 RID: 1142
			HeaderCRC,
			// Token: 0x04000477 RID: 1143
			Data,
			// Token: 0x04000478 RID: 1144
			DataCRC,
			// Token: 0x04000479 RID: 1145
			Resource,
			// Token: 0x0400047A RID: 1146
			ResourceCRC,
			// Token: 0x0400047B RID: 1147
			Ending,
			// Token: 0x0400047C RID: 1148
			Ended
		}
	}
}
