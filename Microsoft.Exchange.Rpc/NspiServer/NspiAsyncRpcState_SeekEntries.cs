using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000304 RID: 772
	internal class NspiAsyncRpcState_SeekEntries : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_SeekEntries,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000E19 RID: 3609 RVA: 0x000386BC File Offset: 0x00037ABC
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiSeekEntriesFlags flags, IntPtr pState, IntPtr pTarget, IntPtr pRestriction, IntPtr pPropTags, IntPtr ppRows)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pState = pState;
			this.pTarget = pTarget;
			this.pRestriction = pRestriction;
			this.pPropTags = pPropTags;
			this.ppRows = ppRows;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00035A4C File Offset: 0x00034E4C
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiSeekEntriesFlags.None;
			this.pState = IntPtr.Zero;
			this.pTarget = IntPtr.Zero;
			this.pRestriction = IntPtr.Zero;
			this.pPropTags = IntPtr.Zero;
			this.ppRows = IntPtr.Zero;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00038708 File Offset: 0x00037B08
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			int[] restriction = null;
			PropertyTag[] propertyTags = null;
			PropertyValue? target = null;
			if (this.ppRows != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppRows, IntPtr.Zero);
			}
			if (this.pState != IntPtr.Zero)
			{
				state = new NspiState(this.pState);
			}
			if (this.pTarget != IntPtr.Zero)
			{
				target = MarshalHelper.ConvertSPropValueToPropertyValue(this.pTarget, MarshalHelper.GetString8CodePage(state));
			}
			if (this.pRestriction != IntPtr.Zero)
			{
				restriction = NspiHelper.ConvertCountedIntArrayFromNative(this.pRestriction);
			}
			if (this.pPropTags != IntPtr.Zero)
			{
				propertyTags = MarshalHelper.ConvertSPropTagArrayToPropertyTagArray(this.pPropTags);
			}
			base.AsyncDispatch.BeginSeekEntries(null, this.contextHandle, this.flags, state, target, restriction, propertyTags, asyncCallback, this);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003B2FC File Offset: 0x0003A6FC
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			PropertyValue[][] array = null;
			NspiStatus result = NspiStatus.Success;
			NspiState nspiState = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			try
			{
				array = null;
				result = base.AsyncDispatch.EndSeekEntries(asyncResult, out nspiState, out array);
				if (this.pState != IntPtr.Zero && nspiState != null)
				{
					nspiState.MarshalToNative(this.pState);
				}
				if (this.ppRows != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle = MarshalHelper.ConvertPropertyValueArraysToSRowSet(array, MarshalHelper.GetString8CodePage(nspiState));
					if (safeRpcMemoryHandle != null)
					{
						IntPtr val = safeRpcMemoryHandle.Detach();
						Marshal.WriteIntPtr(this.ppRows, val);
					}
				}
			}
			finally
			{
				if (safeRpcMemoryHandle != null)
				{
					((IDisposable)safeRpcMemoryHandle).Dispose();
				}
			}
			return (int)result;
		}

		// Token: 0x04000E37 RID: 3639
		private IntPtr contextHandle;

		// Token: 0x04000E38 RID: 3640
		private NspiSeekEntriesFlags flags;

		// Token: 0x04000E39 RID: 3641
		private IntPtr pState;

		// Token: 0x04000E3A RID: 3642
		private IntPtr pTarget;

		// Token: 0x04000E3B RID: 3643
		private IntPtr pRestriction;

		// Token: 0x04000E3C RID: 3644
		private IntPtr pPropTags;

		// Token: 0x04000E3D RID: 3645
		private IntPtr ppRows;
	}
}
