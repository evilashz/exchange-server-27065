using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000050 RID: 80
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Guid("0002030b-0000-0000-C000-000000000046")]
	[ComImport]
	internal interface IMAPIContainer : IMAPIProp
	{
		// Token: 0x060001F2 RID: 498
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x060001F3 RID: 499
		[PreserveSig]
		int SaveChanges(int ulFlags);

		// Token: 0x060001F4 RID: 500
		[PreserveSig]
		int GetProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags, out int lpcValues, [PointerType("SPropValue*")] out SafeExLinkedMemoryHandle lppPropArray);

		// Token: 0x060001F5 RID: 501
		[PreserveSig]
		int GetPropList(int ulFlags, out SafeExLinkedMemoryHandle propList);

		// Token: 0x060001F6 RID: 502
		[PreserveSig]
		int OpenProperty(int propTag, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int interfaceOptions, int ulFlags, [MarshalAs(UnmanagedType.IUnknown)] out object obj);

		// Token: 0x060001F7 RID: 503
		[PreserveSig]
		unsafe int SetProps(int cValues, SPropValue* lpPropArray, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x060001F8 RID: 504
		[PreserveSig]
		int DeleteProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x060001F9 RID: 505
		[PreserveSig]
		int CopyTo(int ciidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] rgiidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpExcludeProps, IntPtr ulUiParam, IntPtr lpProgress, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, IMAPIProp lpDestObj, int ulFlags, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x060001FA RID: 506
		[PreserveSig]
		int CopyProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps, IntPtr ulUIParam, IntPtr lpProgress, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, IMAPIProp lpDestObj, int ulFlags, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x060001FB RID: 507
		[PreserveSig]
		unsafe int GetNamesFromIDs(int** lppPropTagArray, Guid* lpGuid, int ulFlags, ref int cPropNames, [PointerType("SNameId**")] out SafeExLinkedMemoryHandle lppNames);

		// Token: 0x060001FC RID: 508
		[PreserveSig]
		unsafe int GetIDsFromNames(int cPropNames, SNameId** lppPropNames, int ulFlags, [PointerType("int*")] out SafeExLinkedMemoryHandle lpPropIDs);

		// Token: 0x060001FD RID: 509
		[PreserveSig]
		int GetContentsTable(int ulFlags, out IMAPITable iMAPITable);

		// Token: 0x060001FE RID: 510
		[PreserveSig]
		int GetHierarchyTable(int ulFlags, out IMAPITable iMAPITable);

		// Token: 0x060001FF RID: 511
		[PreserveSig]
		int OpenEntry(int cbEntryID, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int ulFlags, out int lpulObjType, [MarshalAs(UnmanagedType.IUnknown)] out object obj);

		// Token: 0x06000200 RID: 512
		[PreserveSig]
		unsafe int SetSearchCriteria([In] SRestriction* lpRestriction, _SBinaryArray* lpContainerList, int ulSearchFlags);

		// Token: 0x06000201 RID: 513
		[PreserveSig]
		int GetSearchCriteria(int ulFlags, [PointerType("SRestriction*")] out SafeExLinkedMemoryHandle lpRestriction, [PointerType("_SBinaryArray*")] out SafeExLinkedMemoryHandle lpContainerList, out int ulSearchState);
	}
}
