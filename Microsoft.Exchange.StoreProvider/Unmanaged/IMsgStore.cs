using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200005E RID: 94
	[Guid("00020306-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IMsgStore : IMAPIProp
	{
		// Token: 0x06000283 RID: 643
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000284 RID: 644
		[PreserveSig]
		int SaveChanges(int ulFlags);

		// Token: 0x06000285 RID: 645
		[PreserveSig]
		int GetProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags, out int lpcValues, [PointerType("SPropValue*")] out SafeExLinkedMemoryHandle lppPropArray);

		// Token: 0x06000286 RID: 646
		[PreserveSig]
		int GetPropList(int ulFlags, out SafeExLinkedMemoryHandle propList);

		// Token: 0x06000287 RID: 647
		[PreserveSig]
		int OpenProperty(int propTag, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int interfaceOptions, int ulFlags, [MarshalAs(UnmanagedType.IUnknown)] out object obj);

		// Token: 0x06000288 RID: 648
		[PreserveSig]
		unsafe int SetProps(int cValues, SPropValue* lpPropArray, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000289 RID: 649
		[PreserveSig]
		int DeleteProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x0600028A RID: 650
		[PreserveSig]
		int CopyTo(int ciidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] rgiidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpExcludeProps, IntPtr ulUiParam, IntPtr lpProgress, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, IMAPIProp lpDestObj, int ulFlags, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x0600028B RID: 651
		[PreserveSig]
		int CopyProps([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps, IntPtr ulUIParam, IntPtr lpProgress, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, IMAPIProp lpDestObj, int ulFlags, [PointerType("SPropProblemArray*")] out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x0600028C RID: 652
		[PreserveSig]
		unsafe int GetNamesFromIDs(int** lppPropTagArray, Guid* lpGuid, int ulFlags, ref int cPropNames, [PointerType("SNameId**")] out SafeExLinkedMemoryHandle lppNames);

		// Token: 0x0600028D RID: 653
		[PreserveSig]
		unsafe int GetIDsFromNames(int cPropNames, SNameId** lppPropNames, int ulFlags, [PointerType("int*")] out SafeExLinkedMemoryHandle lpPropIDs);

		// Token: 0x0600028E RID: 654
		[PreserveSig]
		int Advise(int cbEntryID, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, AdviseFlags ulEventMask, IMAPIAdviseSink lpAdviseSink, out IntPtr piConnection);

		// Token: 0x0600028F RID: 655
		[PreserveSig]
		int Unadvise(IntPtr iConnection);

		// Token: 0x06000290 RID: 656
		[PreserveSig]
		int Slot10();

		// Token: 0x06000291 RID: 657
		[PreserveSig]
		int OpenEntry(int cbEntryID, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int ulFlags, out int lpulObjType, [MarshalAs(UnmanagedType.IUnknown)] out object obj);

		// Token: 0x06000292 RID: 658
		[PreserveSig]
		int SetReceiveFolder([MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszMessageClass, int ulFlags, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID);

		// Token: 0x06000293 RID: 659
		[PreserveSig]
		int GetReceiveFolder([MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszMessageClass, int ulFlags, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId, out SafeExLinkedMemoryHandle lppszExplicitClass);

		// Token: 0x06000294 RID: 660
		[PreserveSig]
		int Slot14();

		// Token: 0x06000295 RID: 661
		[PreserveSig]
		int StoreLogoff(ref int ulFlags);

		// Token: 0x06000296 RID: 662
		[PreserveSig]
		int AbortSubmit(int cbEntryID, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, int ulFlags);

		// Token: 0x06000297 RID: 663
		[PreserveSig]
		int Slot17();

		// Token: 0x06000298 RID: 664
		[PreserveSig]
		int Slot18();

		// Token: 0x06000299 RID: 665
		[PreserveSig]
		int Slot19();

		// Token: 0x0600029A RID: 666
		[PreserveSig]
		int Slot1a();
	}
}
