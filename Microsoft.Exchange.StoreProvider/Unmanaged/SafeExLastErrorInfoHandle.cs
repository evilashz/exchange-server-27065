using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002BE RID: 702
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExLastErrorInfoHandle : SafeExInterfaceHandle, IExLastErrorInfo, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000D22 RID: 3362 RVA: 0x00034698 File Offset: 0x00032898
		protected SafeExLastErrorInfoHandle()
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000346A0 File Offset: 0x000328A0
		internal SafeExLastErrorInfoHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000346A9 File Offset: 0x000328A9
		internal SafeExLastErrorInfoHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x000346B2 File Offset: 0x000328B2
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExLastErrorInfoHandle>(this);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000346BC File Offset: 0x000328BC
		public int GetLastError(int hResult, out int lpMapiError)
		{
			lpMapiError = 0;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num = SafeExLastErrorInfoHandle.ILastErrorInfo_GetLastError(this.handle, hResult, out safeExLinkedMemoryHandle);
				if (num == 0 && !safeExLinkedMemoryHandle.IsInvalid)
				{
					lpMapiError = Marshal.ReadInt32(safeExLinkedMemoryHandle.DangerousGetHandle(), MAPIERROR.LowLevelErrorOffset);
				}
				result = num;
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

		// Token: 0x06000D27 RID: 3367 RVA: 0x0003471C File Offset: 0x0003291C
		public int GetExtendedErrorInfo(out DiagnosticContext pExtendedErrorInfo)
		{
			return this.InternalGetExtendedErrorInfo(out pExtendedErrorInfo);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00034728 File Offset: 0x00032928
		private unsafe int InternalGetExtendedErrorInfo(out DiagnosticContext pExtendedErrorInfo)
		{
			pExtendedErrorInfo = null;
			SafeExMemoryHandle safeExMemoryHandle = null;
			int result;
			try
			{
				int num = SafeExLastErrorInfoHandle.ILastErrorInfo_GetExtendedErrorInfo(this.handle, out safeExMemoryHandle);
				if (num == 0)
				{
					pExtendedErrorInfo = new DiagnosticContext((THREAD_DIAG_CONTEXT*)safeExMemoryHandle.DangerousGetHandle().ToPointer());
				}
				else
				{
					pExtendedErrorInfo = new DiagnosticContext(null);
				}
				result = num;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D29 RID: 3369
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int ILastErrorInfo_GetLastError(IntPtr iLastErrorInfo, int hResult, out SafeExLinkedMemoryHandle lpMapiError);

		// Token: 0x06000D2A RID: 3370
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int ILastErrorInfo_GetExtendedErrorInfo(IntPtr iLastErrorInfo, out SafeExMemoryHandle pExtendedErrorInfo);
	}
}
