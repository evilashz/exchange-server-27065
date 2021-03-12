using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002B7 RID: 695
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExFastTransferExHandle : SafeExInterfaceHandle, IExFastTransferEx, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x00034341 File Offset: 0x00032541
		protected SafeExFastTransferExHandle()
		{
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00034349 File Offset: 0x00032549
		internal SafeExFastTransferExHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00034352 File Offset: 0x00032552
		internal SafeExFastTransferExHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0003435B File Offset: 0x0003255B
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExFastTransferExHandle>(this);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00034363 File Offset: 0x00032563
		public int Config(int ulFlags, int ulTransferMethod)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_Config(this.handle, ulFlags, ulTransferMethod);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00034372 File Offset: 0x00032572
		public int TransferBuffer(int cbData, byte[] data, out int cbProcessed)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_TransferBuffer(this.handle, cbData, data, out cbProcessed);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00034382 File Offset: 0x00032582
		public int IsInterfaceOk(int ulTransferMethod, ref Guid refiid, IntPtr lpPropTagArray, int ulFlags)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_IsInterfaceOk(this.handle, ulTransferMethod, ref refiid, lpPropTagArray, ulFlags);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00034394 File Offset: 0x00032594
		public int GetObjectType(out Guid iid)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_GetObjectType(this.handle, out iid);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000343A2 File Offset: 0x000325A2
		public int GetLastLowLevelError(out int lowLevelError)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_GetLastLowLevelError(this.handle, out lowLevelError);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000343B0 File Offset: 0x000325B0
		public unsafe int GetServerVersion(int cbBufferSize, byte* pBuffer, out int cbBuffer)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_GetServerVersion(this.handle, cbBufferSize, pBuffer, out cbBuffer);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x000343C0 File Offset: 0x000325C0
		public unsafe int TellPartnerVersion(int cbBuffer, byte* pBuffer)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_TellPartnerVersion(this.handle, cbBuffer, pBuffer);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x000343CF File Offset: 0x000325CF
		public int IsPrivateLogon()
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_IsPrivateLogon(this.handle);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000343DC File Offset: 0x000325DC
		public int StartMdbEventsImport()
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_StartMdbEventsImport(this.handle);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000343E9 File Offset: 0x000325E9
		public int FinishMdbEventsImport(bool bSuccess)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_FinishMdbEventsImport(this.handle, bSuccess);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000343F7 File Offset: 0x000325F7
		public int SetWatermarks(int cWMs, MDBEVENTWMRAW[] WMs)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_SetWatermarks(this.handle, cWMs, WMs);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00034406 File Offset: 0x00032606
		public int AddMdbEvents(int cbRequest, byte[] pbRequest)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_AddMdbEvents(this.handle, cbRequest, pbRequest);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00034415 File Offset: 0x00032615
		public int SetReceiveFolder(int cbEntryId, byte[] entryId, string messageClass)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_SetReceiveFolder(this.handle, cbEntryId, entryId, messageClass);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00034425 File Offset: 0x00032625
		public unsafe int SetPerUser(ref MapiLtidNative pltid, Guid* guidReplica, int lib, byte[] pb, int cb, bool fLast)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_SetPerUser(this.handle, ref pltid, guidReplica, lib, pb, cb, fLast);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0003443B File Offset: 0x0003263B
		public unsafe int SetProps(int cValues, SPropValue* lpPropArray)
		{
			return SafeExFastTransferExHandle.IExchangeFastTransferEx_SetProps(this.handle, cValues, lpPropArray);
		}

		// Token: 0x06000CDB RID: 3291
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_Config(IntPtr iExchangeFastTransferEx, int ulFlags, int ulTransferMethod);

		// Token: 0x06000CDC RID: 3292
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_TransferBuffer(IntPtr iExchangeFastTransferEx, int cbData, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] byte[] data, out int cbProcessed);

		// Token: 0x06000CDD RID: 3293
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_IsInterfaceOk(IntPtr iExchangeFastTransferEx, int ulTransferMethod, ref Guid refiid, IntPtr lpPropTagArray, int ulFlags);

		// Token: 0x06000CDE RID: 3294
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_GetObjectType(IntPtr iExchangeFastTransferEx, out Guid iid);

		// Token: 0x06000CDF RID: 3295
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_GetLastLowLevelError(IntPtr iExchangeFastTransferEx, out int lowLevelError);

		// Token: 0x06000CE0 RID: 3296
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeFastTransferEx_GetServerVersion(IntPtr iExchangeFastTransferEx, int cbBufferSize, byte* pBuffer, out int cbBuffer);

		// Token: 0x06000CE1 RID: 3297
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeFastTransferEx_TellPartnerVersion(IntPtr iExchangeFastTransferEx, int cbBuffer, byte* pBuffer);

		// Token: 0x06000CE2 RID: 3298
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_IsPrivateLogon(IntPtr iExchangeFastTransferEx);

		// Token: 0x06000CE3 RID: 3299
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_StartMdbEventsImport(IntPtr iExchangeFastTransferEx);

		// Token: 0x06000CE4 RID: 3300
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_FinishMdbEventsImport(IntPtr iExchangeFastTransferEx, bool bSuccess);

		// Token: 0x06000CE5 RID: 3301
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_SetWatermarks(IntPtr iExchangeFastTransferEx, int cWMs, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] MDBEVENTWMRAW[] WMs);

		// Token: 0x06000CE6 RID: 3302
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_AddMdbEvents(IntPtr iExchangeFastTransferEx, [MarshalAs(UnmanagedType.U4)] [In] int cbRequest, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] byte[] pbRequest);

		// Token: 0x06000CE7 RID: 3303
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeFastTransferEx_SetReceiveFolder(IntPtr iExchangeFastTransferEx, int cbEntryId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] byte[] entryId, [MarshalAs(UnmanagedType.LPStr)] [In] string messageClass);

		// Token: 0x06000CE8 RID: 3304
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeFastTransferEx_SetPerUser(IntPtr iExchangeFastTransferEx, ref MapiLtidNative pltid, Guid* guidReplica, int lib, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] [In] byte[] pb, int cb, bool fLast);

		// Token: 0x06000CE9 RID: 3305
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeFastTransferEx_SetProps(IntPtr iExchangeFastTransferEx, int cValues, SPropValue* lpPropArray);
	}
}
