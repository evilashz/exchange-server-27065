using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000324 RID: 804
	internal class NspiAsyncRpcState_ResolveNamesW : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_ResolveNamesW,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000F69 RID: 3945 RVA: 0x000399EC File Offset: 0x00038DEC
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiResolveNamesFlags flags, IntPtr pState, IntPtr pPropTags, IntPtr pNames, IntPtr ppMids, IntPtr ppRows)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pState = pState;
			this.pPropTags = pPropTags;
			this.pNames = pNames;
			this.ppMids = ppMids;
			this.ppRows = ppRows;
			this.codePage = 0;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00035EC0 File Offset: 0x000352C0
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiResolveNamesFlags.None;
			this.pState = IntPtr.Zero;
			this.pPropTags = IntPtr.Zero;
			this.pNames = IntPtr.Zero;
			this.ppMids = IntPtr.Zero;
			this.ppRows = IntPtr.Zero;
			this.codePage = 0;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00039A40 File Offset: 0x00038E40
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			PropertyTag[] propertyTags = null;
			string[] names = null;
			if (this.ppMids != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppMids, IntPtr.Zero);
			}
			if (this.ppRows != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppRows, IntPtr.Zero);
			}
			if (this.pState != IntPtr.Zero)
			{
				state = new NspiState(this.pState);
			}
			this.codePage = MarshalHelper.GetString8CodePage(state);
			if (this.pPropTags != IntPtr.Zero)
			{
				propertyTags = MarshalHelper.ConvertSPropTagArrayToPropertyTagArray(this.pPropTags);
			}
			if (this.pNames != IntPtr.Zero)
			{
				names = NspiHelper.ConvertCountedStringArrayFromNative(this.pNames, false);
			}
			base.AsyncDispatch.BeginResolveNamesW(null, this.contextHandle, this.flags, state, propertyTags, names, asyncCallback, this);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0003B994 File Offset: 0x0003AD94
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			int[] array = null;
			PropertyValue[][] array2 = null;
			NspiStatus result = NspiStatus.Success;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle2 = null;
			try
			{
				int num = 0;
				array = null;
				array2 = null;
				result = base.AsyncDispatch.EndResolveNamesW(asyncResult, out num, out array, out array2);
				byte condition;
				if (array2 != null && num != this.codePage)
				{
					condition = 0;
				}
				else
				{
					condition = 1;
				}
				ExAssert.Assert(condition != 0, "Code page changed across dispatch layer.");
				if (this.ppMids != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(array, true);
					if (safeRpcMemoryHandle != null)
					{
						IntPtr val = safeRpcMemoryHandle.Detach();
						Marshal.WriteIntPtr(this.ppMids, val);
					}
				}
				if (this.ppRows != IntPtr.Zero && array2 != null)
				{
					safeRpcMemoryHandle2 = MarshalHelper.ConvertPropertyValueArraysToSRowSet(array2, num);
					if (safeRpcMemoryHandle2 != null)
					{
						IntPtr val2 = safeRpcMemoryHandle2.Detach();
						Marshal.WriteIntPtr(this.ppRows, val2);
					}
				}
			}
			finally
			{
				if (safeRpcMemoryHandle != null)
				{
					((IDisposable)safeRpcMemoryHandle).Dispose();
				}
				if (safeRpcMemoryHandle2 != null)
				{
					((IDisposable)safeRpcMemoryHandle2).Dispose();
				}
			}
			return (int)result;
		}

		// Token: 0x04000F16 RID: 3862
		private IntPtr contextHandle;

		// Token: 0x04000F17 RID: 3863
		private NspiResolveNamesFlags flags;

		// Token: 0x04000F18 RID: 3864
		private IntPtr pState;

		// Token: 0x04000F19 RID: 3865
		private IntPtr pPropTags;

		// Token: 0x04000F1A RID: 3866
		private IntPtr pNames;

		// Token: 0x04000F1B RID: 3867
		private IntPtr ppMids;

		// Token: 0x04000F1C RID: 3868
		private IntPtr ppRows;

		// Token: 0x04000F1D RID: 3869
		private int codePage;
	}
}
