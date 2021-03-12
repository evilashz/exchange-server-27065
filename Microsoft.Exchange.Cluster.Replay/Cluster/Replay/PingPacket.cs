using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000264 RID: 612
	internal class PingPacket
	{
		// Token: 0x060017F6 RID: 6134 RVA: 0x00063138 File Offset: 0x00061338
		public static byte[] FormPacket()
		{
			PingPacket pingPacket = new PingPacket();
			pingPacket.Type = 8;
			pingPacket.SubCode = 0;
			pingPacket.CheckSum = 0;
			pingPacket.Identifier = 45;
			pingPacket.SequenceNumber = 0;
			for (int i = 0; i < 32; i++)
			{
				pingPacket.Data[i] = 35;
			}
			byte[] value = PingPacket.Serialize(pingPacket);
			ushort[] array = new ushort[20];
			for (int j = 0; j < 20; j++)
			{
				array[j] = BitConverter.ToUInt16(value, j * 2);
			}
			pingPacket.CheckSum = PingPacket.Checksum(array, 20);
			return PingPacket.Serialize(pingPacket);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000631C8 File Offset: 0x000613C8
		public static byte[] Serialize(PingPacket packetInfo)
		{
			byte[] array = new byte[40];
			array[0] = packetInfo.Type;
			array[1] = packetInfo.SubCode;
			int num = 2;
			byte[] bytes = BitConverter.GetBytes(packetInfo.CheckSum);
			Array.Copy(bytes, 0, array, num, bytes.Length);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(packetInfo.Identifier);
			Array.Copy(bytes, 0, array, num, bytes.Length);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(packetInfo.SequenceNumber);
			Array.Copy(bytes, 0, array, num, bytes.Length);
			num += bytes.Length;
			Array.Copy(packetInfo.Data, 0, array, num, packetInfo.Data.Length);
			num += packetInfo.Data.Length;
			return array;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00063270 File Offset: 0x00061470
		public static ushort Checksum(ushort[] buffer, int size)
		{
			int num = 0;
			int num2 = 0;
			while (size > 0)
			{
				num += Convert.ToInt32(buffer[num2]);
				num2++;
				size--;
			}
			num = (num >> 16) + (num & 65535);
			num += num >> 16;
			return (ushort)(~(ushort)num);
		}

		// Token: 0x0400097B RID: 2427
		private const int ICMP_ECHO = 8;

		// Token: 0x0400097C RID: 2428
		private const int PacketSize = 40;

		// Token: 0x0400097D RID: 2429
		private const int PacketDataLen = 32;

		// Token: 0x0400097E RID: 2430
		public byte Type;

		// Token: 0x0400097F RID: 2431
		public byte SubCode;

		// Token: 0x04000980 RID: 2432
		public ushort CheckSum;

		// Token: 0x04000981 RID: 2433
		public ushort Identifier;

		// Token: 0x04000982 RID: 2434
		public ushort SequenceNumber;

		// Token: 0x04000983 RID: 2435
		public byte[] Data = new byte[32];
	}
}
