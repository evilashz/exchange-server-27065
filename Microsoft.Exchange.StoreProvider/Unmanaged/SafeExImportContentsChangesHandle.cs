using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002B9 RID: 697
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExImportContentsChangesHandle : SafeExInterfaceHandle, IExImportContentsChanges, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x000344DB File Offset: 0x000326DB
		protected SafeExImportContentsChangesHandle()
		{
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000344E3 File Offset: 0x000326E3
		internal SafeExImportContentsChangesHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000344EC File Offset: 0x000326EC
		internal SafeExImportContentsChangesHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000344F5 File Offset: 0x000326F5
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExImportContentsChangesHandle>(this);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000344FD File Offset: 0x000326FD
		public int Config(IStream iStream, int ulFlags)
		{
			return SafeExImportContentsChangesHandle.IExchangeImportContentsChanges_Config(this.handle, iStream, ulFlags);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0003450C File Offset: 0x0003270C
		public int UpdateState(IStream iStream)
		{
			return SafeExImportContentsChangesHandle.IExchangeImportContentsChanges_UpdateState(this.handle, iStream);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0003451A File Offset: 0x0003271A
		public unsafe int ImportMessageChange(int cpvalChanges, SPropValue* ppvalChanges, int ulFlags, out SafeExMapiMessageHandle iMessage)
		{
			return SafeExImportContentsChangesHandle.IExchangeImportContentsChanges_ImportMessageChange(this.handle, cpvalChanges, ppvalChanges, ulFlags, out iMessage);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0003452C File Offset: 0x0003272C
		public unsafe int ImportMessageDeletion(int ulFlags, _SBinaryArray* lpSrcEntryList)
		{
			return SafeExImportContentsChangesHandle.IExchangeImportContentsChanges_ImportMessageDeletion(this.handle, ulFlags, lpSrcEntryList);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0003453B File Offset: 0x0003273B
		public unsafe int ImportPerUserReadStateChange(int cElements, _ReadState* lpReadState)
		{
			return SafeExImportContentsChangesHandle.IExchangeImportContentsChanges_ImportPerUserReadStateChange(this.handle, cElements, lpReadState);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0003454C File Offset: 0x0003274C
		public int ImportMessageMove(int cbSourceKeySrcFolder, byte[] pbSourceKeySrcFolder, int cbSourceKeySrcMessage, byte[] pbSourceKeySrcMessage, int cbPCLMessage, byte[] pbPCLMessage, int cbSourceKeyDestMessage, byte[] pbSourceKeyDestMessage, int cbChangeNumDestMessage, byte[] pbChangeNumDestMessage)
		{
			return SafeExImportContentsChangesHandle.IExchangeImportContentsChanges_ImportMessageMove(this.handle, cbSourceKeySrcFolder, pbSourceKeySrcFolder, cbSourceKeySrcMessage, pbSourceKeySrcMessage, cbPCLMessage, pbPCLMessage, cbSourceKeyDestMessage, pbSourceKeyDestMessage, cbChangeNumDestMessage, pbChangeNumDestMessage);
		}

		// Token: 0x06000CFF RID: 3327
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeImportContentsChanges_Config(IntPtr iExchangeImportContentsChanges, IStream iStream, int ulFlags);

		// Token: 0x06000D00 RID: 3328
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeImportContentsChanges_UpdateState(IntPtr iExchangeImportContentsChanges, IStream iStream);

		// Token: 0x06000D01 RID: 3329
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeImportContentsChanges_ImportMessageChange(IntPtr iExchangeImportContentsChanges, int cpvalChanges, SPropValue* ppvalChanges, int ulFlags, out SafeExMapiMessageHandle iMessage);

		// Token: 0x06000D02 RID: 3330
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeImportContentsChanges_ImportMessageDeletion(IntPtr iExchangeImportContentsChanges, int ulFlags, _SBinaryArray* lpSrcEntryList);

		// Token: 0x06000D03 RID: 3331
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeImportContentsChanges_ImportPerUserReadStateChange(IntPtr iExchangeImportContentsChanges, int cElements, _ReadState* lpReadState);

		// Token: 0x06000D04 RID: 3332
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeImportContentsChanges_ImportMessageMove(IntPtr iExchangeImportContentsChanges, int cbSourceKeySrcFolder, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeySrcFolder, int cbSourceKeySrcMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeySrcMessage, int cbPCLMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbPCLMessage, int cbSourceKeyDestMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeyDestMessage, int cbChangeNumDestMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbChangeNumDestMessage);
	}
}
