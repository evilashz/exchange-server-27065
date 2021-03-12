using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.HA.Services
{
	// Token: 0x0200032B RID: 811
	public static class NativeMethods
	{
		// Token: 0x0600213A RID: 8506
		[DllImport("iphlpapi.dll")]
		private static extern int GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize, bool bOrder, uint ulAf, NativeMethods.TCP_TABLE_CLASS TableClass, uint reserved);

		// Token: 0x0600213B RID: 8507
		[DllImport("iphlpapi.dll")]
		private static extern int SetTcpEntry(IntPtr pTcprow);

		// Token: 0x0600213C RID: 8508
		[DllImport("wsock32.dll")]
		private static extern int ntohs(int netshort);

		// Token: 0x0600213D RID: 8509
		[DllImport("wsock32.dll")]
		private static extern int htons(int netshort);

		// Token: 0x0600213E RID: 8510 RVA: 0x00099D4C File Offset: 0x00097F4C
		public static NativeMethods.SocketData GetOpenSocketByPort(int port)
		{
			List<NativeMethods.SocketData> list = (from socket in NativeMethods.GetOpenedSockets()
			where socket.LocalPort == port
			select socket).ToList<NativeMethods.SocketData>();
			if (list.Count > 0)
			{
				return list.First<NativeMethods.SocketData>();
			}
			return null;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00099D94 File Offset: 0x00097F94
		public static List<NativeMethods.SocketData> GetOpenedSockets()
		{
			List<NativeMethods.SocketData> list = new List<NativeMethods.SocketData>();
			list.AddRange(NativeMethods.GetIPv4OpenedSockets());
			list.AddRange(NativeMethods.GetIPv6OpenedSockets());
			return list;
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00099DC0 File Offset: 0x00097FC0
		private static List<NativeMethods.SocketData> GetIPv4OpenedSockets()
		{
			return NativeMethods.ListActiveSockets<NativeMethods.MIB_TCPROW_OWNER_PID>(default(NativeMethods.MIB_TCPROW_OWNER_PID));
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00099DDC File Offset: 0x00097FDC
		private static List<NativeMethods.SocketData> GetIPv6OpenedSockets()
		{
			return NativeMethods.ListActiveSockets<NativeMethods.MIB_TCP6ROW_OWNER_PID>(default(NativeMethods.MIB_TCP6ROW_OWNER_PID));
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x00099DF8 File Offset: 0x00097FF8
		private static List<NativeMethods.SocketData> ListActiveSockets<T>(T tcpStructure)
		{
			if (typeof(T) != typeof(NativeMethods.MIB_TCPROW_OWNER_PID) && typeof(T) != typeof(NativeMethods.MIB_TCP6ROW_OWNER_PID))
			{
				throw new ArgumentException(typeof(T).FullName + " not supported for method GetSocketsOpenedByProcess<T>");
			}
			IntPtr intPtr = IntPtr.Zero;
			List<NativeMethods.SocketData> list = new List<NativeMethods.SocketData>();
			List<NativeMethods.SocketData> result;
			try
			{
				int cb = 0;
				uint ulAf;
				if (typeof(T) == typeof(NativeMethods.MIB_TCPROW_OWNER_PID))
				{
					ulAf = 2U;
				}
				else
				{
					ulAf = 23U;
				}
				NativeMethods.GetExtendedTcpTable(IntPtr.Zero, ref cb, true, ulAf, NativeMethods.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0U);
				intPtr = Marshal.AllocCoTaskMem(cb);
				NativeMethods.GetExtendedTcpTable(intPtr, ref cb, true, ulAf, NativeMethods.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0U);
				int num = Marshal.ReadInt32(intPtr);
				int offset = Marshal.SizeOf(tcpStructure);
				IntPtr intPtr2 = IntPtr.Add(intPtr, 4);
				for (int i = 0; i < num; i++)
				{
					tcpStructure = (T)((object)Marshal.PtrToStructure(intPtr2, tcpStructure.GetType()));
					NativeMethods.SocketData item = NativeMethods.CreateSocketData(tcpStructure);
					list.Add(item);
					intPtr2 = IntPtr.Add(intPtr2, offset);
				}
				result = list;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return result;
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00099F48 File Offset: 0x00098148
		private static NativeMethods.SocketData CreateSocketData(object tcpStructure)
		{
			NativeMethods.SocketData socketData = new NativeMethods.SocketData();
			if (tcpStructure.GetType() == typeof(NativeMethods.MIB_TCPROW_OWNER_PID))
			{
				NativeMethods.MIB_TCPROW_OWNER_PID mib_TCPROW_OWNER_PID = (NativeMethods.MIB_TCPROW_OWNER_PID)tcpStructure;
				socketData.LocalAddress = new IPAddress((long)((ulong)mib_TCPROW_OWNER_PID.dwLocalAddr));
				socketData.LocalPort = NativeMethods.ntohs(mib_TCPROW_OWNER_PID.dwLocalPort);
				socketData.RemoteAddress = new IPAddress((long)((ulong)mib_TCPROW_OWNER_PID.dwRemoteAddr));
				socketData.RemotePort = NativeMethods.ntohs(mib_TCPROW_OWNER_PID.dwRemotePort);
				socketData.OwnerPid = mib_TCPROW_OWNER_PID.dwOwningPid;
				socketData.State = mib_TCPROW_OWNER_PID.dwState;
			}
			else
			{
				if (!(tcpStructure.GetType() == typeof(NativeMethods.MIB_TCP6ROW_OWNER_PID)))
				{
					throw new ArgumentException("Type not supported: " + tcpStructure.GetType().FullName);
				}
				NativeMethods.MIB_TCP6ROW_OWNER_PID mib_TCP6ROW_OWNER_PID = (NativeMethods.MIB_TCP6ROW_OWNER_PID)tcpStructure;
				socketData.LocalAddress = new IPAddress(mib_TCP6ROW_OWNER_PID.ucLocalAddr, (long)((ulong)mib_TCP6ROW_OWNER_PID.dwLocalScopeId));
				socketData.LocalPort = NativeMethods.ntohs(mib_TCP6ROW_OWNER_PID.dwLocalPort);
				socketData.RemoteAddress = new IPAddress(mib_TCP6ROW_OWNER_PID.ucRemoteAddr, (long)((ulong)mib_TCP6ROW_OWNER_PID.dwRemoteScopeId));
				socketData.RemotePort = NativeMethods.ntohs(mib_TCP6ROW_OWNER_PID.dwRemotePort);
				socketData.OwnerPid = mib_TCP6ROW_OWNER_PID.dwOwningPid;
				socketData.State = mib_TCP6ROW_OWNER_PID.dwState;
			}
			return socketData;
		}

		// Token: 0x04000D6F RID: 3439
		private const uint AF_INET = 2U;

		// Token: 0x04000D70 RID: 3440
		private const uint AF_INET6 = 23U;

		// Token: 0x0200032C RID: 812
		public struct MIB_TCPROW_OWNER_PID
		{
			// Token: 0x04000D71 RID: 3441
			public NativeMethods.State dwState;

			// Token: 0x04000D72 RID: 3442
			public uint dwLocalAddr;

			// Token: 0x04000D73 RID: 3443
			public int dwLocalPort;

			// Token: 0x04000D74 RID: 3444
			public uint dwRemoteAddr;

			// Token: 0x04000D75 RID: 3445
			public int dwRemotePort;

			// Token: 0x04000D76 RID: 3446
			public int dwOwningPid;
		}

		// Token: 0x0200032D RID: 813
		public struct MIB_TCP6ROW_OWNER_PID
		{
			// Token: 0x04000D77 RID: 3447
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] ucLocalAddr;

			// Token: 0x04000D78 RID: 3448
			public uint dwLocalScopeId;

			// Token: 0x04000D79 RID: 3449
			public int dwLocalPort;

			// Token: 0x04000D7A RID: 3450
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] ucRemoteAddr;

			// Token: 0x04000D7B RID: 3451
			public uint dwRemoteScopeId;

			// Token: 0x04000D7C RID: 3452
			public int dwRemotePort;

			// Token: 0x04000D7D RID: 3453
			public NativeMethods.State dwState;

			// Token: 0x04000D7E RID: 3454
			public int dwOwningPid;
		}

		// Token: 0x0200032E RID: 814
		private enum TCP_TABLE_CLASS
		{
			// Token: 0x04000D80 RID: 3456
			TCP_TABLE_BASIC_LISTENER,
			// Token: 0x04000D81 RID: 3457
			TCP_TABLE_BASIC_CONNECTIONS,
			// Token: 0x04000D82 RID: 3458
			TCP_TABLE_BASIC_ALL,
			// Token: 0x04000D83 RID: 3459
			TCP_TABLE_OWNER_PID_LISTENER,
			// Token: 0x04000D84 RID: 3460
			TCP_TABLE_OWNER_PID_CONNECTIONS,
			// Token: 0x04000D85 RID: 3461
			TCP_TABLE_OWNER_PID_ALL,
			// Token: 0x04000D86 RID: 3462
			TCP_TABLE_OWNER_MODULE_LISTENER,
			// Token: 0x04000D87 RID: 3463
			TCP_TABLE_OWNER_MODULE_CONNECTIONS,
			// Token: 0x04000D88 RID: 3464
			TCP_TABLE_OWNER_MODULE_ALL
		}

		// Token: 0x0200032F RID: 815
		public enum State : uint
		{
			// Token: 0x04000D8A RID: 3466
			All,
			// Token: 0x04000D8B RID: 3467
			Closed,
			// Token: 0x04000D8C RID: 3468
			Listen,
			// Token: 0x04000D8D RID: 3469
			Syn_Sent,
			// Token: 0x04000D8E RID: 3470
			Syn_Rcvd,
			// Token: 0x04000D8F RID: 3471
			Established,
			// Token: 0x04000D90 RID: 3472
			Fin_Wait1,
			// Token: 0x04000D91 RID: 3473
			Fin_Wait2,
			// Token: 0x04000D92 RID: 3474
			Close_Wait,
			// Token: 0x04000D93 RID: 3475
			Closing,
			// Token: 0x04000D94 RID: 3476
			Last_Ack,
			// Token: 0x04000D95 RID: 3477
			Time_Wait,
			// Token: 0x04000D96 RID: 3478
			Delete_TCB
		}

		// Token: 0x02000330 RID: 816
		public class SocketData
		{
			// Token: 0x170008B2 RID: 2226
			// (get) Token: 0x06002144 RID: 8516 RVA: 0x0009A090 File Offset: 0x00098290
			public uint LocalAddressInt
			{
				get
				{
					return NativeMethods.SocketData.GetIntRepresentationOfByteArray(this.LocalAddress.GetAddressBytes());
				}
			}

			// Token: 0x170008B3 RID: 2227
			// (get) Token: 0x06002145 RID: 8517 RVA: 0x0009A0A2 File Offset: 0x000982A2
			public uint RemoteAddressInt
			{
				get
				{
					return NativeMethods.SocketData.GetIntRepresentationOfByteArray(this.RemoteAddress.GetAddressBytes());
				}
			}

			// Token: 0x06002146 RID: 8518 RVA: 0x0009A0B4 File Offset: 0x000982B4
			private static uint GetIntRepresentationOfByteArray(byte[] bytes)
			{
				uint num = 0U;
				for (int i = bytes.Length - 1; i >= 0; i--)
				{
					num = (num << 8) + (uint)bytes[i];
				}
				return num;
			}

			// Token: 0x04000D97 RID: 3479
			public IPAddress LocalAddress;

			// Token: 0x04000D98 RID: 3480
			public int LocalPort;

			// Token: 0x04000D99 RID: 3481
			public IPAddress RemoteAddress;

			// Token: 0x04000D9A RID: 3482
			public int RemotePort;

			// Token: 0x04000D9B RID: 3483
			public int OwnerPid;

			// Token: 0x04000D9C RID: 3484
			public NativeMethods.State State;
		}
	}
}
