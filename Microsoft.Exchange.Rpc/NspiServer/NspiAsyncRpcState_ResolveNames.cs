using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000322 RID: 802
	internal class NspiAsyncRpcState_ResolveNames : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_ResolveNames,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000F54 RID: 3924 RVA: 0x00039870 File Offset: 0x00038C70
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

		// Token: 0x06000F55 RID: 3925 RVA: 0x00035E60 File Offset: 0x00035260
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

		// Token: 0x06000F56 RID: 3926 RVA: 0x000398C4 File Offset: 0x00038CC4
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			PropertyTag[] propertyTags = null;
			byte[][] names = null;
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
				names = NspiHelper.ConvertCountedByteStringArrayFromNative(this.pNames);
			}
			base.AsyncDispatch.BeginResolveNames(null, this.contextHandle, this.flags, state, propertyTags, names, asyncCallback, this);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003B880 File Offset: 0x0003AC80
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
				result = base.AsyncDispatch.EndResolveNames(asyncResult, out num, out array, out array2);
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

		// Token: 0x04000F06 RID: 3846
		private IntPtr contextHandle;

		// Token: 0x04000F07 RID: 3847
		private NspiResolveNamesFlags flags;

		// Token: 0x04000F08 RID: 3848
		private IntPtr pState;

		// Token: 0x04000F09 RID: 3849
		private IntPtr pPropTags;

		// Token: 0x04000F0A RID: 3850
		private IntPtr pNames;

		// Token: 0x04000F0B RID: 3851
		private IntPtr ppMids;

		// Token: 0x04000F0C RID: 3852
		private IntPtr ppRows;

		// Token: 0x04000F0D RID: 3853
		private int codePage;
	}
}
