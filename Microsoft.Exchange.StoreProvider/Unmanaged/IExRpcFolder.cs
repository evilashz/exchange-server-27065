using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000279 RID: 633
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Guid("E9972C72-4A7D-464c-9350-ADD5ABABF6D8")]
	[ComImport]
	internal interface IExRpcFolder
	{
		// Token: 0x06000B46 RID: 2886
		[PreserveSig]
		int IsContentAvailable([MarshalAs(UnmanagedType.Bool)] out bool isContentAvailable);

		// Token: 0x06000B47 RID: 2887
		[PreserveSig]
		int GetReplicaServers(out uint numberOfServers, out SafeExLinkedMemoryHandle servers);

		// Token: 0x06000B48 RID: 2888
		[PreserveSig]
		int SetMessageFlags(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, uint ulStatus, uint ulMask);

		// Token: 0x06000B49 RID: 2889
		[PreserveSig]
		unsafe int CopyMessagesEx(_SBinaryArray* sbinArray, IMAPIFolder destFolder, int ulFlags, int cValues, SPropValue* lpPropArray);

		// Token: 0x06000B4A RID: 2890
		[PreserveSig]
		unsafe int SetPropsConditional([In] SRestriction* lpRes, int cValues, SPropValue* lpPropArray, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000B4B RID: 2891
		[PreserveSig]
		unsafe int CopyMessagesEID(_SBinaryArray* sbinArray, IMAPIFolder destFolder, int ulFlags, int cValues, SPropValue* lpPropArray, [PointerType("_SBinaryArray*")] out SafeExLinkedMemoryHandle lppEntryIds, [PointerType("_SBinaryArray*")] out SafeExLinkedMemoryHandle lppChangeNumbers);

		// Token: 0x06000B4C RID: 2892
		[PreserveSig]
		int CreateFolderEx(int ulFolderType, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszFolderName, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszFolderComment, int cbEntryId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] lpEntryId, IntPtr lpInterface, int ulFlags, [MarshalAs(UnmanagedType.Interface)] out IMAPIFolder iMAPIFolder);
	}
}
