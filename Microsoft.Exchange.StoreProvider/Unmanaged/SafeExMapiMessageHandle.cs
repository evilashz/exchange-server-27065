using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002C5 RID: 709
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExMapiMessageHandle : SafeExMapiPropHandle, IExMapiMessage, IExMapiProp, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x00036118 File Offset: 0x00034318
		protected SafeExMapiMessageHandle()
		{
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00036120 File Offset: 0x00034320
		internal SafeExMapiMessageHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00036129 File Offset: 0x00034329
		internal SafeExMapiMessageHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00036132 File Offset: 0x00034332
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExMapiMessageHandle>(this);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003613C File Offset: 0x0003433C
		public int GetAttachmentTable(int ulFlags, out IExMapiTable iMAPITable)
		{
			SafeExMapiTableHandle safeExMapiTableHandle = null;
			int result = SafeExMapiMessageHandle.IMessage_GetAttachmentTable(this.handle, ulFlags, out safeExMapiTableHandle);
			iMAPITable = safeExMapiTableHandle;
			return result;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00036160 File Offset: 0x00034360
		public int OpenAttach(int attachmentNumber, Guid lpInterface, int ulFlags, out IExMapiAttach iAttach)
		{
			SafeExMapiAttachHandle safeExMapiAttachHandle = null;
			int result = SafeExMapiMessageHandle.IMessage_OpenAttach(this.handle, attachmentNumber, lpInterface, ulFlags, out safeExMapiAttachHandle);
			iAttach = safeExMapiAttachHandle;
			return result;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00036188 File Offset: 0x00034388
		public int CreateAttach(Guid lpInterface, int ulFlags, out int attachmentNumber, out IExMapiAttach iAttach)
		{
			SafeExMapiAttachHandle safeExMapiAttachHandle = null;
			int result = SafeExMapiMessageHandle.IMessage_CreateAttach(this.handle, lpInterface, ulFlags, out attachmentNumber, out safeExMapiAttachHandle);
			iAttach = safeExMapiAttachHandle;
			return result;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000361AD File Offset: 0x000343AD
		public int DeleteAttach(int attachmentNumber, IntPtr ulUiParam, IntPtr lpProgress, int ulFlags)
		{
			return SafeExMapiMessageHandle.IMessage_DeleteAttach(this.handle, attachmentNumber, ulUiParam, lpProgress, ulFlags);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000361C0 File Offset: 0x000343C0
		public int GetRecipientTable(int ulFlags, out IExMapiTable iMAPITable)
		{
			SafeExMapiTableHandle safeExMapiTableHandle = null;
			int result = SafeExMapiMessageHandle.IMessage_GetRecipientTable(this.handle, ulFlags, out safeExMapiTableHandle);
			iMAPITable = safeExMapiTableHandle;
			return result;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000361E2 File Offset: 0x000343E2
		public int ModifyRecipients(int ulFlags, AdrEntry[] padrList)
		{
			return this.InternalModifyRecipients(ulFlags, padrList);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x000361EC File Offset: 0x000343EC
		private unsafe int InternalModifyRecipients(int ulFlags, AdrEntry[] padrList)
		{
			int bytesToMarshal = AdrList.GetBytesToMarshal(padrList);
			fixed (byte* ptr = new byte[bytesToMarshal])
			{
				AdrList.MarshalToNative(ptr, padrList);
				return SafeExMapiMessageHandle.IMessage_ModifyRecipients(this.handle, ulFlags, (_AdrList*)ptr);
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00036236 File Offset: 0x00034436
		public int SubmitMessage(int ulFlags)
		{
			return SafeExMapiMessageHandle.IMessage_SubmitMessage(this.handle, ulFlags);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00036244 File Offset: 0x00034444
		public int SetReadFlag(int ulFlags)
		{
			return SafeExMapiMessageHandle.IMessage_SetReadFlag(this.handle, ulFlags);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00036252 File Offset: 0x00034452
		public int Deliver(int ulFlags)
		{
			return SafeExMapiMessageHandle.IExRpcMessage_Deliver(this.handle, ulFlags);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00036260 File Offset: 0x00034460
		public int DoneWithMessage()
		{
			return SafeExMapiMessageHandle.IExRpcMessage_DoneWithMessage(this.handle);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0003626D File Offset: 0x0003446D
		public int DuplicateDeliveryCheck(string internetMessageId, long submitTimeAsLong)
		{
			return SafeExMapiMessageHandle.IExRpcMessage_DuplicateDeliveryCheck(this.handle, internetMessageId, submitTimeAsLong);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0003627C File Offset: 0x0003447C
		public int TransportSendMessage(out PropValue[] lppPropArray)
		{
			return this.InternalTransportSendMessage(out lppPropArray);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00036288 File Offset: 0x00034488
		private unsafe int InternalTransportSendMessage(out PropValue[] lppPropArray)
		{
			lppPropArray = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num = 0;
				int num2 = SafeExMapiMessageHandle.IExRpcMessage_TransportSendMessage(this.handle, out num, out safeExLinkedMemoryHandle);
				if (num2 == 0)
				{
					PropValue[] array = new PropValue[num];
					SPropValue* ptr = (SPropValue*)safeExLinkedMemoryHandle.DangerousGetHandle().ToPointer();
					for (int i = 0; i < num; i++)
					{
						array[i] = new PropValue(ptr + i);
					}
					lppPropArray = array;
				}
				result = num2;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0003631C File Offset: 0x0003451C
		public int SubmitMessageEx(int ulFlags)
		{
			return SafeExMapiMessageHandle.IExRpcMessage_SubmitMessageEx(this.handle, ulFlags);
		}

		// Token: 0x06000DD0 RID: 3536
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMessage_GetAttachmentTable(IntPtr iMessage, int ulFlags, out SafeExMapiTableHandle iMAPITable);

		// Token: 0x06000DD1 RID: 3537
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMessage_OpenAttach(IntPtr iMessage, int attachmentNumber, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int ulFlags, out SafeExMapiAttachHandle iAttach);

		// Token: 0x06000DD2 RID: 3538
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMessage_CreateAttach(IntPtr iMessage, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int ulFlags, out int attachmentNumber, out SafeExMapiAttachHandle iAttach);

		// Token: 0x06000DD3 RID: 3539
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMessage_DeleteAttach(IntPtr iMessage, int attachmentNumber, IntPtr ulUiParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000DD4 RID: 3540
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMessage_GetRecipientTable(IntPtr iMessage, int ulFlags, out SafeExMapiTableHandle iMAPITable);

		// Token: 0x06000DD5 RID: 3541
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IMessage_ModifyRecipients(IntPtr iMessage, int ulFlags, _AdrList* padrList);

		// Token: 0x06000DD6 RID: 3542
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMessage_SubmitMessage(IntPtr iMessage, int ulFlags);

		// Token: 0x06000DD7 RID: 3543
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMessage_SetReadFlag(IntPtr iMessage, int ulFlags);

		// Token: 0x06000DD8 RID: 3544
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMessage_Deliver(IntPtr iMessage, int ulFlags);

		// Token: 0x06000DD9 RID: 3545
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMessage_DoneWithMessage(IntPtr iMessage);

		// Token: 0x06000DDA RID: 3546
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMessage_DuplicateDeliveryCheck(IntPtr iMessage, [MarshalAs(UnmanagedType.LPStr)] string lpszInetMessageId, long submitTimeAsLong);

		// Token: 0x06000DDB RID: 3547
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMessage_TransportSendMessage(IntPtr iMessage, out int lpcValues, out SafeExLinkedMemoryHandle lppPropArray);

		// Token: 0x06000DDC RID: 3548
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMessage_SubmitMessageEx(IntPtr iMessage, int ulFlags);
	}
}
