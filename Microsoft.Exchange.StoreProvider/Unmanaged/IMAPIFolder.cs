using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000051 RID: 81
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Guid("0002030c-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IMAPIFolder : IMAPIContainer, IMAPIProp
	{
		// Token: 0x06000202 RID: 514
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000203 RID: 515
		[PreserveSig]
		int SaveChanges(int ulFlags);

		// Token: 0x06000204 RID: 516
		[PreserveSig]
		int GetProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags, out int lpcValues, [PointerType("SPropValue*")] out SafeExLinkedMemoryHandle lppPropArray);

		// Token: 0x06000205 RID: 517
		[PreserveSig]
		int GetPropList(int ulFlags, out SafeExLinkedMemoryHandle propList);

		// Token: 0x06000206 RID: 518
		[PreserveSig]
		int OpenProperty(int propTag, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int interfaceOptions, int ulFlags, [MarshalAs(UnmanagedType.IUnknown)] out object obj);

		// Token: 0x06000207 RID: 519
		[PreserveSig]
		unsafe int SetProps(int cValues, SPropValue* lpPropArray, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000208 RID: 520
		[PreserveSig]
		int DeleteProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000209 RID: 521
		[PreserveSig]
		int CopyTo(int ciidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] rgiidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpExcludeProps, IntPtr ulUiParam, IntPtr lpProgress, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, IMAPIProp lpDestObj, int ulFlags, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x0600020A RID: 522
		[PreserveSig]
		int CopyProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps, IntPtr ulUIParam, IntPtr lpProgress, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, IMAPIProp lpDestObj, int ulFlags, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x0600020B RID: 523
		[PreserveSig]
		unsafe int GetNamesFromIDs(int** lppPropTagArray, Guid* lpGuid, int ulFlags, ref int cPropNames, [PointerType("SNameId**")] out SafeExLinkedMemoryHandle lppNames);

		// Token: 0x0600020C RID: 524
		[PreserveSig]
		unsafe int GetIDsFromNames(int cPropNames, SNameId** lppPropNames, int ulFlags, [PointerType("int*")] out SafeExLinkedMemoryHandle lpPropIDs);

		// Token: 0x0600020D RID: 525
		[PreserveSig]
		int GetContentsTable(int ulFlags, out IMAPITable iMAPITable);

		// Token: 0x0600020E RID: 526
		[PreserveSig]
		int GetHierarchyTable(int ulFlags, out IMAPITable iMAPITable);

		// Token: 0x0600020F RID: 527
		[PreserveSig]
		int OpenEntry(int cbEntryID, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int ulFlags, out int lpulObjType, [MarshalAs(UnmanagedType.IUnknown)] out object obj);

		// Token: 0x06000210 RID: 528
		[PreserveSig]
		unsafe int SetSearchCriteria([In] SRestriction* lpRestriction, _SBinaryArray* lpContainerList, int ulSearchFlags);

		// Token: 0x06000211 RID: 529
		[PreserveSig]
		int GetSearchCriteria(int ulFlags, [PointerType("SRestriction*")] out SafeExLinkedMemoryHandle lpRestriction, [PointerType("_SBinaryArray*")] out SafeExLinkedMemoryHandle lpContainerList, out int ulSearchState);

		// Token: 0x06000212 RID: 530
		[PreserveSig]
		int CreateMessage(IntPtr lpInterface, int ulFlags, [MarshalAs(UnmanagedType.Interface)] out IMessage iMessage);

		// Token: 0x06000213 RID: 531
		[PreserveSig]
		unsafe int CopyMessages(_SBinaryArray* sbinArray, IntPtr lpInterface, IMAPIFolder destFolder, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000214 RID: 532
		[PreserveSig]
		unsafe int DeleteMessages(_SBinaryArray* sbinArray, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000215 RID: 533
		[PreserveSig]
		int CreateFolder(int ulFolderType, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszFolderName, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszFolderComment, IntPtr lpInterface, int ulFlags, [MarshalAs(UnmanagedType.Interface)] out IMAPIFolder iMAPIFolder);

		// Token: 0x06000216 RID: 534
		[PreserveSig]
		int CopyFolder(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, IntPtr lpInterface, IMAPIFolder destFolder, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszNewFolderName, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000217 RID: 535
		[PreserveSig]
		int DeleteFolder(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000218 RID: 536
		[PreserveSig]
		unsafe int SetReadFlags(_SBinaryArray* sbinArray, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000219 RID: 537
		[PreserveSig]
		int GetMessageStatus(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, int ulFlags, out MessageStatus pulMessageStatus);

		// Token: 0x0600021A RID: 538
		[PreserveSig]
		int SetMessageStatus(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, MessageStatus ulNewStatus, MessageStatus ulNewStatusMask, out MessageStatus pulOldStatus);

		// Token: 0x0600021B RID: 539
		[PreserveSig]
		int Slot1c();

		// Token: 0x0600021C RID: 540
		[PreserveSig]
		int EmptyFolder(IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);
	}
}
