using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000278 RID: 632
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("DCBB456B-FBDA-4c0c-BCF2-90EEF6BDCC07")]
	[ComImport]
	internal interface IExRpcConnection
	{
		// Token: 0x06000B3E RID: 2878
		[PreserveSig]
		int OpenMsgStore(int ulFlags, long ullOpenFlags, [MarshalAs(UnmanagedType.LPStr)] string lpszMailboxDN, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbMailboxGuid, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbMdbGuid, IMsgStore lpMDBPrivate, out SafeExMemoryHandle lppszWrongServerDN, IntPtr hToken, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pSidUser, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pSidPrimaryGroup, [MarshalAs(UnmanagedType.LPStr)] string lpszUserDN, int ulLcidString, int ulLcidSort, int ulCpid, [MarshalAs(UnmanagedType.LPStr)] string lpszApplicationId, out IMsgStore iMsgStore);

		// Token: 0x06000B3F RID: 2879
		[PreserveSig]
		int SendAuxBuffer(int ulFlags, int cbAuxBuffer, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbAuxBuffer, int fForceSend);

		// Token: 0x06000B40 RID: 2880
		[PreserveSig]
		int FlushRPCBuffer([MarshalAs(UnmanagedType.Bool)] bool fForceSend);

		// Token: 0x06000B41 RID: 2881
		[PreserveSig]
		int GetServerVersion(out int pulVersionMajor, out int pulVersionMinor, out int pulBuildMajor, out int pulBuildMinor);

		// Token: 0x06000B42 RID: 2882
		[PreserveSig]
		int IsDead([MarshalAs(UnmanagedType.Bool)] out bool pfDead);

		// Token: 0x06000B43 RID: 2883
		[PreserveSig]
		int RpcSentToServer([MarshalAs(UnmanagedType.Bool)] out bool pfRpcSent);

		// Token: 0x06000B44 RID: 2884
		[PreserveSig]
		int IsMapiMT([MarshalAs(UnmanagedType.Bool)] out bool pfMapiMT);

		// Token: 0x06000B45 RID: 2885
		[PreserveSig]
		int IsConnectedToMapiServer([MarshalAs(UnmanagedType.Bool)] out bool pfConnectedToMapiServer);
	}
}
