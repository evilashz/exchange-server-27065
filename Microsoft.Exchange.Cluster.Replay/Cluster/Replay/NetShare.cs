using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Common.Cluster;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200036F RID: 879
	internal static class NetShare
	{
		// Token: 0x0600232E RID: 9006 RVA: 0x000A4740 File Offset: 0x000A2940
		public static void AddShare(string serverName, string scopeName, string shareName, string sharePath, string shareRemark, byte[] securityDescriptor, out int paramNumber)
		{
			IntPtr intPtr = IntPtr.Zero;
			paramNumber = 0;
			try
			{
				if (securityDescriptor != null && securityDescriptor.Length > 0)
				{
					intPtr = Marshal.AllocHGlobal(securityDescriptor.Length);
					Marshal.Copy(securityDescriptor, 0, intPtr, securityDescriptor.Length);
				}
				uint num = 87U;
				if (NetShare.IsLonghornAndAbove())
				{
					SHARE_INFO_503 share_INFO_ = default(SHARE_INFO_503);
					share_INFO_.Netname = shareName;
					share_INFO_.Type = 0U;
					share_INFO_.Remark = shareRemark;
					share_INFO_.Permissions = 1;
					share_INFO_.Max_uses = -1;
					share_INFO_.Current_uses = 0;
					share_INFO_.Path = sharePath;
					share_INFO_.Passwd = null;
					share_INFO_.Servername = scopeName;
					share_INFO_.Reserved = 0;
					share_INFO_.Security_descriptor = intPtr;
					num = NetShare.NetShareAdd(serverName, 503, ref share_INFO_, out paramNumber);
				}
				if (num == 87U)
				{
					NetShare.SHARE_INFO_502 share_INFO_2 = default(NetShare.SHARE_INFO_502);
					share_INFO_2.Netname = shareName;
					share_INFO_2.Type = 0U;
					share_INFO_2.Remark = shareRemark;
					share_INFO_2.Permissions = 1;
					share_INFO_2.Max_uses = -1;
					share_INFO_2.Current_uses = 0;
					share_INFO_2.Path = sharePath;
					share_INFO_2.Passwd = null;
					share_INFO_2.Reserved = 0;
					share_INFO_2.Security_descriptor = intPtr;
					num = NetShare.NetShareAdd(serverName, 502, ref share_INFO_2, out paramNumber);
				}
				if (num != 0U)
				{
					ExTraceGlobals.NetShareTracer.TraceError(0L, "NetShareAdd error status: {0}, paramNumber: {1} for serverName={2}, scopeName={3}, shareName={4}, sharePath={5}", new object[]
					{
						num,
						paramNumber,
						serverName,
						scopeName,
						shareName,
						sharePath
					});
					throw new Win32Exception((int)num);
				}
				ExTraceGlobals.NetShareTracer.TraceDebug(0L, "NetShareAdd success status: {0}, paramNumber: {1} for serverName={2}, scopeName={3}, shareName={4}, sharePath={5}", new object[]
				{
					num,
					paramNumber,
					serverName,
					scopeName,
					shareName,
					sharePath
				});
				ExTraceGlobals.PFDTracer.TracePfd(0L, "PFD CRS {0} NetShareAdd success status: {1}, paramNumber: {2} for serverName={3}, scopeName={4}, shareName={5}, sharePath={6}", new object[]
				{
					26587,
					num,
					paramNumber,
					serverName,
					scopeName,
					shareName,
					sharePath
				});
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000A497C File Offset: 0x000A2B7C
		public static void DeleteShare(string serverName, string shareName)
		{
			uint num = 0U;
			NetShare.SERVER_TRANSPORT_INFO2[] serverTransports = NetShare.GetServerTransports(serverName);
			uint num2 = NetShare.NetShareDel(serverName, shareName, 0);
			if (num2 == 0U)
			{
				num += 1U;
			}
			else if (num2 != 2310U)
			{
				ExTraceGlobals.NetShareTracer.TraceError(0L, "NetShareDel error status: {0} for {1} {2} ({3} shares deleted)", new object[]
				{
					num2,
					serverName ?? "<null>",
					shareName,
					num
				});
				throw new Win32Exception((int)num2);
			}
			if (serverTransports != null)
			{
				foreach (NetShare.SERVER_TRANSPORT_INFO2 server_TRANSPORT_INFO in serverTransports)
				{
					if ((server_TRANSPORT_INFO.Flags & 4U) != 0U)
					{
						uint num3;
						num2 = NetShare.DeleteShareOnNetworkTransport(server_TRANSPORT_INFO.Networkaddress, shareName, out num3);
						if (num2 == 0U)
						{
							num += num3;
						}
						else if (num2 != 2310U)
						{
							ExTraceGlobals.NetShareTracer.TraceError(0L, "NetShareDel error status: {0} for scope={1} server={2} share={3} ({4} shares deleted)", new object[]
							{
								num2,
								server_TRANSPORT_INFO.Networkaddress,
								serverName ?? "<null>",
								shareName,
								num
							});
							throw new Win32Exception((int)num2);
						}
					}
				}
			}
			if (num2 == 0U)
			{
				ExTraceGlobals.NetShareTracer.TraceDebug(0L, "DeleteShare success status: {0} for {1} {2} ({3} shares deleted)", new object[]
				{
					num2,
					serverName ?? "<null>",
					shareName,
					num
				});
				ExTraceGlobals.PFDTracer.TracePfd(0L, "PFD CRS {0} DeleteShare success status: {1} for {2} {3} ({4} shares deleted)", new object[]
				{
					22491,
					num2,
					serverName ?? "<null>",
					shareName,
					num
				});
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000A4B38 File Offset: 0x000A2D38
		public static void GetShareInfo(string serverName, string shareName, out string sharePath, out string shareRemark, out uint shareType, out int sharePermissions, out int shareMaxUses)
		{
			byte[] array;
			NetShare.GetShareInfoWithSecurity(serverName, shareName, out sharePath, out shareRemark, out shareType, out sharePermissions, out shareMaxUses, out array);
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000A4B58 File Offset: 0x000A2D58
		public static void GetShareInfoWithSecurity(string serverName, string shareName, out string sharePath, out string shareRemark, out uint shareType, out int sharePermissions, out int shareMaxUses, out byte[] securityDescriptor)
		{
			IntPtr zero = IntPtr.Zero;
			securityDescriptor = null;
			try
			{
				uint num = NetShare.NetShareGetInfo(serverName, shareName, 502, out zero);
				if (num != 0U)
				{
					ExTraceGlobals.NetShareTracer.TraceError<uint, string, string>(0L, "NetShareGetInfo error status: {0} for {1} {2}", num, serverName ?? "<null>", shareName);
					throw new Win32Exception((int)num);
				}
				ExTraceGlobals.NetShareTracer.TraceDebug<uint, string, string>(0L, "GetShareInfo success status: {0} for {1} {2}", num, serverName ?? "<null>", shareName);
				NetShare.SHARE_INFO_502 share_INFO_ = (NetShare.SHARE_INFO_502)Marshal.PtrToStructure(zero, typeof(NetShare.SHARE_INFO_502));
				sharePath = share_INFO_.Path;
				shareRemark = share_INFO_.Remark;
				shareType = share_INFO_.Type;
				sharePermissions = share_INFO_.Permissions;
				shareMaxUses = share_INFO_.Max_uses;
				if (share_INFO_.Security_descriptor != IntPtr.Zero)
				{
					uint securityDescriptorLength = NetShare.GetSecurityDescriptorLength(share_INFO_.Security_descriptor);
					if (!NetShare.IsValidSecurityDescriptor(share_INFO_.Security_descriptor))
					{
						ExTraceGlobals.NetShareTracer.TraceDebug<string, string>(0L, "GetShareInfo : \\\\{1}\\{2} has an invalid security descriptor", serverName, shareName);
					}
					securityDescriptor = new byte[securityDescriptorLength];
					Marshal.Copy(share_INFO_.Security_descriptor, securityDescriptor, 0, (int)securityDescriptorLength);
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					uint num = NetShare.NetApiBufferFree(zero);
					if (num != 0U)
					{
						throw new Win32Exception((int)num);
					}
				}
			}
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000A4C98 File Offset: 0x000A2E98
		public static void SetShareInfo(string serverName, string shareName, string sharePath, string shareRemark, uint shareType, int sharePermissions, int shareMaxUses, byte[] securityDescriptor)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			NetShare.SHARE_INFO_502 share_INFO_;
			share_INFO_.Netname = null;
			share_INFO_.Type = shareType;
			share_INFO_.Remark = shareRemark;
			share_INFO_.Permissions = sharePermissions;
			share_INFO_.Max_uses = shareMaxUses;
			share_INFO_.Current_uses = 0;
			share_INFO_.Path = sharePath;
			share_INFO_.Passwd = null;
			share_INFO_.Reserved = 0;
			share_INFO_.Security_descriptor = IntPtr.Zero;
			try
			{
				if (securityDescriptor != null && securityDescriptor.Length > 0)
				{
					intPtr2 = Marshal.AllocHGlobal(securityDescriptor.Length);
					Marshal.Copy(securityDescriptor, 0, intPtr2, securityDescriptor.Length);
				}
				share_INFO_.Security_descriptor = intPtr2;
				int cb = Marshal.SizeOf(typeof(NetShare.SHARE_INFO_502));
				intPtr = Marshal.AllocHGlobal(cb);
				Marshal.StructureToPtr(share_INFO_, intPtr, false);
				int num2;
				int num = NetShare.NetShareSetInfo(serverName, shareName, 502, intPtr, out num2);
				if (num != 0)
				{
					ExTraceGlobals.NetShareTracer.TraceError(0L, "NetShareSetInfo error status: {0} for {1} {2} (parmError={3}", new object[]
					{
						num,
						serverName,
						shareName,
						num2
					});
					throw new Win32Exception(num);
				}
				ExTraceGlobals.NetShareTracer.TraceDebug<int, string, string>(0L, "GetShareInfo success status: {0} for {1} {2}", num, serverName, shareName);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
			}
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000A4DE0 File Offset: 0x000A2FE0
		internal static uint EnumScopedShares(string networkAddress, out SHARE_INFO_503[] shareInfos)
		{
			uint num = 0U;
			IntPtr zero = IntPtr.Zero;
			shareInfos = null;
			try
			{
				uint num2 = 0U;
				uint num3;
				uint num4;
				num = NetShare.NetShareEnum(networkAddress, 503U, out zero, uint.MaxValue, out num3, out num4, ref num2);
				if (num == 124U)
				{
					shareInfos = new SHARE_INFO_503[0];
					return num;
				}
				if (num != 0U)
				{
					ExTraceGlobals.NetShareTracer.TraceError<uint, string>(0L, "NetShareEnum() error status: {0} for {1}", num, networkAddress);
					throw new Win32Exception((int)num);
				}
				shareInfos = NetShare.PtrToShareInfo503(zero, num3);
				ExTraceGlobals.NetShareTracer.TraceDebug<string, uint>(0L, "NetShareEnum( networkAddress={0} ) found {1} shares.", networkAddress, num3);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					num = NetShare.NetApiBufferFree(zero);
					if (num != 0U)
					{
						throw new Win32Exception((int)num);
					}
				}
			}
			return num;
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000A4E90 File Offset: 0x000A3090
		internal static SHARE_INFO_503[] GetScopedShareInfo(string serverName, string shareName)
		{
			NetShare.SERVER_TRANSPORT_INFO2[] serverTransports = NetShare.GetServerTransports(serverName);
			if (serverTransports != null)
			{
				Dictionary<string, SHARE_INFO_503> dictionary = new Dictionary<string, SHARE_INFO_503>(3);
				foreach (NetShare.SERVER_TRANSPORT_INFO2 server_TRANSPORT_INFO in serverTransports)
				{
					if ((server_TRANSPORT_INFO.Flags & 4U) != 0U)
					{
						SHARE_INFO_503[] array2;
						uint num = NetShare.EnumScopedShares(server_TRANSPORT_INFO.Networkaddress, out array2);
						if (num != 0U)
						{
							ExTraceGlobals.NetShareTracer.TraceError<uint, string>(0L, "EnumScopedShares() error status: {0} for {1}", num, server_TRANSPORT_INFO.Networkaddress);
							throw new Win32Exception((int)num);
						}
						foreach (SHARE_INFO_503 value in array2)
						{
							if (Cluster.StringIEquals(value.Netname, shareName))
							{
								dictionary[value.Servername] = value;
							}
						}
					}
				}
				if (dictionary.Count != 0)
				{
					Dictionary<string, SHARE_INFO_503>.ValueCollection values = dictionary.Values;
					SHARE_INFO_503[] array4 = new SHARE_INFO_503[values.Count];
					values.CopyTo(array4, 0);
					return array4;
				}
			}
			string path;
			string remark;
			uint type;
			int permissions;
			int max_uses;
			NetShare.GetShareInfo(serverName, shareName, out path, out remark, out type, out permissions, out max_uses);
			return new SHARE_INFO_503[]
			{
				new SHARE_INFO_503
				{
					Netname = shareName,
					Type = type,
					Remark = remark,
					Permissions = permissions,
					Max_uses = max_uses,
					Current_uses = 0,
					Path = path,
					Passwd = null,
					Servername = "*",
					Reserved = 0,
					Security_descriptor = IntPtr.Zero
				}
			};
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000A5014 File Offset: 0x000A3214
		internal static NetShare.SERVER_TRANSPORT_INFO2[] GetServerTransports(string serverName)
		{
			IntPtr zero = IntPtr.Zero;
			uint num = 0U;
			NetShare.SERVER_TRANSPORT_INFO2[] result;
			try
			{
				uint entriesRead;
				uint num3;
				uint num2 = NetShare.NetServerTransportEnum(serverName, 2U, out zero, uint.MaxValue, out entriesRead, out num3, ref num);
				if (num2 == 124U)
				{
					return null;
				}
				if (num2 != 0U)
				{
					ExTraceGlobals.NetShareTracer.TraceError<uint, string>(0L, "NetServerTransportEnum error status: {0} for {1}", num2, serverName);
					throw new Win32Exception((int)num2);
				}
				result = NetShare.PtrToTransportInfo2(zero, entriesRead);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					uint num2 = NetShare.NetApiBufferFree(zero);
					if (num2 != 0U)
					{
						throw new Win32Exception((int)num2);
					}
				}
			}
			return result;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000A50A0 File Offset: 0x000A32A0
		private static uint DeleteShareOnNetworkTransport(string networkAddress, string shareName, out uint sharesDeletedOnThisTransport)
		{
			SHARE_INFO_503[] array = null;
			sharesDeletedOnThisTransport = 0U;
			ExTraceGlobals.NetShareTracer.TraceDebug<string, string>(0L, "DeleteShareOnNetworkTransport( addr={0}, shareName={1}, ... ) called.", networkAddress, shareName);
			uint num = NetShare.EnumScopedShares(networkAddress, out array);
			if (num != 0U)
			{
				ExTraceGlobals.NetShareTracer.TraceError<uint, string>(0L, "EnumScopedShares() error status: {0} for {1}", num, networkAddress);
				throw new Win32Exception((int)num);
			}
			foreach (SHARE_INFO_503 share_INFO_ in array)
			{
				if (Cluster.StringIEquals(share_INFO_.Netname, shareName) && Cluster.StringIEquals(share_INFO_.Servername, networkAddress))
				{
					num = NetShare.NetShareDel(networkAddress, shareName, 0);
					if (num == 0U)
					{
						sharesDeletedOnThisTransport += 1U;
					}
				}
			}
			ExTraceGlobals.NetShareTracer.TraceDebug<string, string, uint>(0L, "DeleteShareOnNetworkTransport( addr={0}, shareName={1}, ... ) deleted {2} shares.", networkAddress, shareName, sharesDeletedOnThisTransport);
			return num;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000A5158 File Offset: 0x000A3358
		private static NetShare.SERVER_TRANSPORT_INFO2[] PtrToTransportInfo2(IntPtr transportInfoPtr, uint entriesRead)
		{
			NetShare.SERVER_TRANSPORT_INFO2[] array = new NetShare.SERVER_TRANSPORT_INFO2[entriesRead];
			int num = Marshal.SizeOf(typeof(NetShare.SERVER_TRANSPORT_INFO2));
			int num2 = 0;
			while ((long)num2 < (long)((ulong)entriesRead))
			{
				IntPtr ptr = (IntPtr)(transportInfoPtr.ToInt64() + (long)(num2 * num));
				array[num2] = (NetShare.SERVER_TRANSPORT_INFO2)Marshal.PtrToStructure(ptr, typeof(NetShare.SERVER_TRANSPORT_INFO2));
				num2++;
			}
			return array;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000A51B6 File Offset: 0x000A33B6
		public static bool IsLonghornAndAbove()
		{
			return Environment.OSVersion.Version.Major >= 6;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000A51D0 File Offset: 0x000A33D0
		private static SHARE_INFO_503[] PtrToShareInfo503(IntPtr shareInfoPtr, uint entriesRead)
		{
			SHARE_INFO_503[] array = new SHARE_INFO_503[entriesRead];
			int num = Marshal.SizeOf(typeof(SHARE_INFO_503));
			int num2 = 0;
			while ((long)num2 < (long)((ulong)entriesRead))
			{
				IntPtr ptr = (IntPtr)(shareInfoPtr.ToInt64() + (long)(num2 * num));
				array[num2] = (SHARE_INFO_503)Marshal.PtrToStructure(ptr, typeof(SHARE_INFO_503));
				num2++;
			}
			return array;
		}

		// Token: 0x0600233A RID: 9018
		[DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint NetServerTransportEnum(string servername, uint level, out IntPtr bufptr, uint prefmaxlen, out uint entriesread, out uint entriestotal, ref uint resumehandle);

		// Token: 0x0600233B RID: 9019
		[DllImport("Netapi32", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint NetShareAdd([MarshalAs(UnmanagedType.LPWStr)] string servername, int level, ref NetShare.SHARE_INFO_502 buf, out int paramNumber);

		// Token: 0x0600233C RID: 9020
		[DllImport("Netapi32", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint NetShareAdd([MarshalAs(UnmanagedType.LPWStr)] string servername, int level, ref SHARE_INFO_503 buf, out int paramNumber);

		// Token: 0x0600233D RID: 9021
		[DllImport("Netapi32", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint NetShareDel([MarshalAs(UnmanagedType.LPWStr)] string servername, [MarshalAs(UnmanagedType.LPWStr)] string netname, int reserved);

		// Token: 0x0600233E RID: 9022
		[DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint NetShareEnum([MarshalAs(UnmanagedType.LPWStr)] string servername, uint level, out IntPtr bufptr, uint prefmaxlen, out uint entriesread, out uint totalentries, ref uint resume_handle);

		// Token: 0x0600233F RID: 9023
		[DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint NetShareGetInfo([MarshalAs(UnmanagedType.LPWStr)] string serverName, [MarshalAs(UnmanagedType.LPWStr)] string netName, int level, out IntPtr bufPtr);

		// Token: 0x06002340 RID: 9024
		[DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int NetShareSetInfo([MarshalAs(UnmanagedType.LPWStr)] string serverName, [MarshalAs(UnmanagedType.LPWStr)] string netName, int level, IntPtr bufPtr, out int parmError);

		// Token: 0x06002341 RID: 9025
		[DllImport("NetApi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint NetApiBufferFree(IntPtr buffer);

		// Token: 0x06002342 RID: 9026
		[DllImport("advapi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsValidSecurityDescriptor(IntPtr pSecurityDescriptor);

		// Token: 0x06002343 RID: 9027
		[DllImport("advapi32.dll")]
		private static extern uint GetSecurityDescriptorLength(IntPtr pSecurityDescriptor);

		// Token: 0x04000EF6 RID: 3830
		public const uint ShareTypeDiskTree = 0U;

		// Token: 0x04000EF7 RID: 3831
		public const int PermissionsFileRead = 1;

		// Token: 0x04000EF8 RID: 3832
		public const int ShareUsesUnlimited = -1;

		// Token: 0x04000EF9 RID: 3833
		private const int ShareInfo502Level = 502;

		// Token: 0x04000EFA RID: 3834
		private const int ShareInfo503Level = 503;

		// Token: 0x04000EFB RID: 3835
		private const uint MAX_PREFERRED_LENGTH = 4294967295U;

		// Token: 0x02000370 RID: 880
		private struct SHARE_INFO_502
		{
			// Token: 0x04000EFC RID: 3836
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Netname;

			// Token: 0x04000EFD RID: 3837
			internal uint Type;

			// Token: 0x04000EFE RID: 3838
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Remark;

			// Token: 0x04000EFF RID: 3839
			internal int Permissions;

			// Token: 0x04000F00 RID: 3840
			internal int Max_uses;

			// Token: 0x04000F01 RID: 3841
			internal int Current_uses;

			// Token: 0x04000F02 RID: 3842
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Path;

			// Token: 0x04000F03 RID: 3843
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Passwd;

			// Token: 0x04000F04 RID: 3844
			internal int Reserved;

			// Token: 0x04000F05 RID: 3845
			internal IntPtr Security_descriptor;
		}

		// Token: 0x02000371 RID: 881
		[StructLayout(LayoutKind.Sequential)]
		internal class SERVER_TRANSPORT_INFO2
		{
			// Token: 0x04000F06 RID: 3846
			internal uint Numberofvcs;

			// Token: 0x04000F07 RID: 3847
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Transportname;

			// Token: 0x04000F08 RID: 3848
			internal IntPtr Transportaddress;

			// Token: 0x04000F09 RID: 3849
			internal uint Transportaddresslength;

			// Token: 0x04000F0A RID: 3850
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Networkaddress;

			// Token: 0x04000F0B RID: 3851
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Domain;

			// Token: 0x04000F0C RID: 3852
			internal uint Flags;
		}
	}
}
