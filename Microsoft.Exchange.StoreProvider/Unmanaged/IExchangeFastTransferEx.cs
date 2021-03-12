using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200004B RID: 75
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Guid("1AD3079C-5325-4b68-A57E-E8FF2BD58E53")]
	[ComImport]
	internal interface IExchangeFastTransferEx
	{
		// Token: 0x060001AF RID: 431
		[PreserveSig]
		int Config(int ulFlags, int ulTransferMethod);

		// Token: 0x060001B0 RID: 432
		[PreserveSig]
		int TransferBuffer(int cbData, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] byte[] data, out int cbProcessed);

		// Token: 0x060001B1 RID: 433
		[PreserveSig]
		bool IsInterfaceOk(int ulTransferMethod, ref Guid refiid, IntPtr lpPropTagArray, int ulFlags);

		// Token: 0x060001B2 RID: 434
		[PreserveSig]
		int GetObjectType(out Guid iid);

		// Token: 0x060001B3 RID: 435
		[PreserveSig]
		int GetLastLowLevelError(out int lowLevelError);

		// Token: 0x060001B4 RID: 436
		[PreserveSig]
		unsafe int GetServerVersion(int cbBufferSize, byte* pBuffer, out int cbBuffer);

		// Token: 0x060001B5 RID: 437
		[PreserveSig]
		unsafe int TellPartnerVersion(int cbBuffer, byte* pBuffer);

		// Token: 0x060001B6 RID: 438
		[PreserveSig]
		bool IsPrivateLogon();

		// Token: 0x060001B7 RID: 439
		[PreserveSig]
		int StartMdbEventsImport();

		// Token: 0x060001B8 RID: 440
		[PreserveSig]
		int FinishMdbEventsImport(bool bSuccess);

		// Token: 0x060001B9 RID: 441
		[PreserveSig]
		int SetWatermarks(int cWMs, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] MDBEVENTWMRAW[] WMs);

		// Token: 0x060001BA RID: 442
		[PreserveSig]
		int AddMdbEvents([MarshalAs(UnmanagedType.U4)] [In] int cbRequest, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] byte[] pbRequest);

		// Token: 0x060001BB RID: 443
		[PreserveSig]
		int SetReceiveFolder(int cbEntryId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] byte[] entryId, [MarshalAs(UnmanagedType.LPStr)] [In] string messageClass);

		// Token: 0x060001BC RID: 444
		[PreserveSig]
		unsafe int SetPerUser(ref MapiLtidNative pltid, Guid* guidReplica, int lib, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] [In] byte[] pb, int cb, bool fLast);

		// Token: 0x060001BD RID: 445
		[PreserveSig]
		unsafe int SetProps(int cValues, SPropValue* lpPropArray);
	}
}
