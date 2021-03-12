using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B38 RID: 2872
	internal static class NetworkShare
	{
		// Token: 0x06003DF5 RID: 15861 RVA: 0x000A2040 File Offset: 0x000A0240
		internal static void Create(string server, string path, string name, string description, DirectorySecurity securityDescriptor)
		{
			NetworkShare.NetShareNativeMethods.SHARE_INFO_502 share_INFO_ = default(NetworkShare.NetShareNativeMethods.SHARE_INFO_502);
			share_INFO_.netname = name;
			share_INFO_.type = 0U;
			share_INFO_.remark = description;
			share_INFO_.permissions = 0;
			share_INFO_.max_uses = -1;
			share_INFO_.current_uses = 0;
			share_INFO_.path = path;
			share_INFO_.passwd = null;
			share_INFO_.reserved = 0;
			byte[] securityDescriptorBinaryForm = securityDescriptor.GetSecurityDescriptorBinaryForm();
			GCHandle gchandle = GCHandle.Alloc(securityDescriptorBinaryForm, GCHandleType.Pinned);
			try
			{
				share_INFO_.security_descriptor = Marshal.UnsafeAddrOfPinnedArrayElement(securityDescriptorBinaryForm, 0);
				uint num2;
				int num = NetworkShare.NetShareNativeMethods.NetworkShareAdd(server, 502, ref share_INFO_, out num2);
				if (num == 2118)
				{
					num = NetworkShare.NetShareNativeMethods.NetworkShareSetInfo(server, name, 502, ref share_INFO_, out num2);
				}
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
			}
			finally
			{
				gchandle.Free();
			}
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x000A2110 File Offset: 0x000A0310
		internal static void Delete(string server, string name)
		{
			int num = NetworkShare.NetShareNativeMethods.NetworkShareDel(server, name, 0);
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000A2130 File Offset: 0x000A0330
		internal static bool AccessCheck(WindowsIdentity user, DirectorySecurity securityDescriptor, FileAccess fileAccess)
		{
			bool result = false;
			uint num = 256U;
			byte[] privilegeSet = new byte[num];
			NativeMethods.GENERIC_MAPPING generic_MAPPING;
			generic_MAPPING.GenericAll = 268435456U;
			generic_MAPPING.GenericRead = 2147483648U;
			generic_MAPPING.GenericWrite = 1073741824U;
			generic_MAPPING.GenericExecute = 536870912U;
			uint desiredAccess = 0U;
			uint num2 = 0U;
			switch (fileAccess)
			{
			case FileAccess.Read:
				desiredAccess = 1U;
				break;
			case FileAccess.Write:
				desiredAccess = 2U;
				break;
			case FileAccess.ReadWrite:
				desiredAccess = 3U;
				break;
			}
			byte[] securityDescriptorBinaryForm = securityDescriptor.GetSecurityDescriptorBinaryForm();
			NativeMethods.MapGenericMask(ref desiredAccess, ref generic_MAPPING);
			if (NetworkShare.NetShareNativeMethods.AccessCheck(securityDescriptorBinaryForm, user.Token, desiredAccess, ref generic_MAPPING, privilegeSet, ref num, out num2, out result) == 0)
			{
				throw new Win32Exception();
			}
			return result;
		}

		// Token: 0x02000B39 RID: 2873
		private static class NetShareNativeMethods
		{
			// Token: 0x06003DF8 RID: 15864
			[DllImport("netapi32.dll", EntryPoint = "NetShareAdd")]
			internal static extern int NetworkShareAdd([MarshalAs(UnmanagedType.LPWStr)] string wszServer, int dwLevel, ref NetworkShare.NetShareNativeMethods.SHARE_INFO_502 shareInfo, out uint parm_err);

			// Token: 0x06003DF9 RID: 15865
			[DllImport("netapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "NetShareSetInfo")]
			internal static extern int NetworkShareSetInfo(string wszServer, string strNetName, int dwLevel, ref NetworkShare.NetShareNativeMethods.SHARE_INFO_502 shareInfo, out uint parm_err);

			// Token: 0x06003DFA RID: 15866
			[DllImport("netapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "NetShareDel")]
			internal static extern int NetworkShareDel(string strServer, string strNetName, int reserved);

			// Token: 0x06003DFB RID: 15867
			[DllImport("advapi32.dll", SetLastError = true)]
			internal static extern int AccessCheck([MarshalAs(UnmanagedType.LPArray)] byte[] pSecurityDescriptor, IntPtr ClientToken, uint DesiredAccess, ref NativeMethods.GENERIC_MAPPING GenericMapping, [MarshalAs(UnmanagedType.LPArray)] byte[] PrivilegeSet, ref uint PrivilegeSetLength, out uint GrantedAccess, [MarshalAs(UnmanagedType.Bool)] out bool AccessStatus);

			// Token: 0x040035D6 RID: 13782
			private const string NETAPI32 = "netapi32.dll";

			// Token: 0x040035D7 RID: 13783
			private const string ADVAPI32 = "advapi32.dll";

			// Token: 0x040035D8 RID: 13784
			public const int FILE_READ_DATA = 1;

			// Token: 0x040035D9 RID: 13785
			public const int FILE_WRITE_DATA = 2;

			// Token: 0x040035DA RID: 13786
			internal const int NERR_BASE = 2100;

			// Token: 0x040035DB RID: 13787
			internal const int NERR_DuplicateShare = 2118;

			// Token: 0x02000B3A RID: 2874
			internal struct SHARE_INFO_502
			{
				// Token: 0x040035DC RID: 13788
				[MarshalAs(UnmanagedType.LPWStr)]
				internal string netname;

				// Token: 0x040035DD RID: 13789
				internal uint type;

				// Token: 0x040035DE RID: 13790
				[MarshalAs(UnmanagedType.LPWStr)]
				internal string remark;

				// Token: 0x040035DF RID: 13791
				internal int permissions;

				// Token: 0x040035E0 RID: 13792
				internal int max_uses;

				// Token: 0x040035E1 RID: 13793
				internal int current_uses;

				// Token: 0x040035E2 RID: 13794
				[MarshalAs(UnmanagedType.LPWStr)]
				internal string path;

				// Token: 0x040035E3 RID: 13795
				[MarshalAs(UnmanagedType.LPWStr)]
				internal string passwd;

				// Token: 0x040035E4 RID: 13796
				internal int reserved;

				// Token: 0x040035E5 RID: 13797
				internal IntPtr security_descriptor;
			}
		}
	}
}
