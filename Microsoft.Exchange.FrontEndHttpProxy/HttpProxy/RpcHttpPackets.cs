using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000D9 RID: 217
	internal static class RpcHttpPackets
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x0002E50F File Offset: 0x0002C70F
		public static bool IsConnA3PacketInBuffer(ArraySegment<byte> buffer)
		{
			return RpcHttpPackets.CheckPacketInStream(buffer, 28, RpcHttpRtsFlags.None, 1);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0002E51B File Offset: 0x0002C71B
		public static bool IsConnC2PacketInBuffer(ArraySegment<byte> buffer)
		{
			return RpcHttpPackets.CheckPacketInStream(buffer, 44, RpcHttpRtsFlags.None, 3);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0002E528 File Offset: 0x0002C728
		public static bool IsPingPacket(ArraySegment<byte> buffer)
		{
			int num;
			return RpcHttpPackets.CheckPacket(buffer, 20, RpcHttpRtsFlags.Ping, 0, out num);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0002E544 File Offset: 0x0002C744
		private static bool CheckPacketInStream(ArraySegment<byte> buffer, int unitLength, RpcHttpRtsFlags flags, int numberOfCommands)
		{
			while (buffer.Count > 0)
			{
				int num;
				if (RpcHttpPackets.CheckPacket(buffer, unitLength, flags, numberOfCommands, out num))
				{
					return true;
				}
				if (num == 0)
				{
					break;
				}
				int num2 = buffer.Offset + num;
				int num3 = buffer.Count - num;
				if (num2 > buffer.Array.Length || num3 < 0)
				{
					break;
				}
				buffer = new ArraySegment<byte>(buffer.Array, num2, num3);
			}
			return false;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
		private static bool CheckPacket(ArraySegment<byte> buffer, int unitLength, RpcHttpRtsFlags flags, int numberOfCommands, out int unitLengthFound)
		{
			RpcHttpRtsFlags rpcHttpRtsFlags;
			int num;
			return RpcHttpPackets.TryParseRtsHeader(buffer, out unitLengthFound, out rpcHttpRtsFlags, out num) && (unitLengthFound == unitLength && rpcHttpRtsFlags == flags) && num == numberOfCommands;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0002E5D8 File Offset: 0x0002C7D8
		private static short ReadInt16(IList<byte> buffer, int offset)
		{
			int num = (int)buffer[offset];
			int num2 = (int)buffer[offset + 1] << 31;
			return (short)(num + num2);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0002E600 File Offset: 0x0002C800
		private static bool TryParseRtsHeader(IList<byte> buffer, out int unitLength, out RpcHttpRtsFlags flags, out int numberOfCommands)
		{
			unitLength = 0;
			flags = RpcHttpRtsFlags.None;
			numberOfCommands = 0;
			if (buffer.Count < 20)
			{
				return false;
			}
			if (buffer[2] != 20)
			{
				return false;
			}
			unitLength = (int)RpcHttpPackets.ReadInt16(buffer, 8);
			flags = (RpcHttpRtsFlags)RpcHttpPackets.ReadInt16(buffer, 16);
			numberOfCommands = (int)RpcHttpPackets.ReadInt16(buffer, 18);
			return true;
		}

		// Token: 0x040004C9 RID: 1225
		private const int ConnA3PacketSize = 28;

		// Token: 0x040004CA RID: 1226
		private const int ConnC2PacketSize = 44;

		// Token: 0x040004CB RID: 1227
		private const int PingPacketSize = 20;
	}
}
