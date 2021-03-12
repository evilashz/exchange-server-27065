using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000053 RID: 83
	[Guid("00020307-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IMessage : IMAPIProp
	{
		// Token: 0x0600022E RID: 558
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x0600022F RID: 559
		[PreserveSig]
		int SaveChanges(int ulFlags);

		// Token: 0x06000230 RID: 560
		[PreserveSig]
		int GetProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags, out int lpcValues, [PointerType("SPropValue*")] out SafeExLinkedMemoryHandle lppPropArray);

		// Token: 0x06000231 RID: 561
		[PreserveSig]
		int GetPropList(int ulFlags, out SafeExLinkedMemoryHandle propList);

		// Token: 0x06000232 RID: 562
		[PreserveSig]
		int OpenProperty(int propTag, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int interfaceOptions, int ulFlags, [MarshalAs(UnmanagedType.IUnknown)] out object obj);

		// Token: 0x06000233 RID: 563
		[PreserveSig]
		unsafe int SetProps(int cValues, SPropValue* lpPropArray, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000234 RID: 564
		[PreserveSig]
		int DeleteProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000235 RID: 565
		[PreserveSig]
		int CopyTo(int ciidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] rgiidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpExcludeProps, IntPtr ulUiParam, IntPtr lpProgress, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, IMAPIProp lpDestObj, int ulFlags, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000236 RID: 566
		[PreserveSig]
		int CopyProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps, IntPtr ulUIParam, IntPtr lpProgress, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, IMAPIProp lpDestObj, int ulFlags, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000237 RID: 567
		[PreserveSig]
		unsafe int GetNamesFromIDs(int** lppPropTagArray, Guid* lpGuid, int ulFlags, ref int cPropNames, [PointerType("SNameId**")] out SafeExLinkedMemoryHandle lppNames);

		// Token: 0x06000238 RID: 568
		[PreserveSig]
		unsafe int GetIDsFromNames(int cPropNames, SNameId** lppPropNames, int ulFlags, [PointerType("int*")] out SafeExLinkedMemoryHandle lpPropIDs);

		// Token: 0x06000239 RID: 569
		[PreserveSig]
		int GetAttachmentTable(int ulFlags, out IMAPITable iMAPITable);

		// Token: 0x0600023A RID: 570
		[PreserveSig]
		int OpenAttach(int attachmentNumber, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int ulFlags, out IAttach iAttach);

		// Token: 0x0600023B RID: 571
		[PreserveSig]
		int CreateAttach([MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int ulFlags, out int attachmentNumber, [MarshalAs(UnmanagedType.Interface)] out IAttach iAttach);

		// Token: 0x0600023C RID: 572
		[PreserveSig]
		int DeleteAttach(int attachmentNumber, IntPtr ulUiParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x0600023D RID: 573
		[PreserveSig]
		int GetRecipientTable(int ulFlags, out IMAPITable iMAPITable);

		// Token: 0x0600023E RID: 574
		[PreserveSig]
		unsafe int ModifyRecipients(int ulFlags, _AdrList* padrList);

		// Token: 0x0600023F RID: 575
		[PreserveSig]
		int SubmitMessage(int ulFlags);

		// Token: 0x06000240 RID: 576
		[PreserveSig]
		int SetReadFlag(int ulFlags);
	}
}
