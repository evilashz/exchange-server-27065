using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000306 RID: 774
	internal class NspiAsyncRpcState_GetMatches : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x00038834 File Offset: 0x00037C34
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiGetMatchesFlags flags, IntPtr pState, IntPtr pInDNTList, int interfaceOptions, IntPtr pRestriction, IntPtr pPropName, int maxRequested, IntPtr ppDNTList, IntPtr pPropTags, IntPtr ppRows)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pState = pState;
			this.pInDNTList = pInDNTList;
			this.interfaceOptions = interfaceOptions;
			this.pRestriction = pRestriction;
			this.pPropName = pPropName;
			this.maxRequested = maxRequested;
			this.ppDNTList = ppDNTList;
			this.pPropTags = pPropTags;
			this.ppRows = ppRows;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00035AA4 File Offset: 0x00034EA4
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiGetMatchesFlags.None;
			this.pState = IntPtr.Zero;
			this.pInDNTList = IntPtr.Zero;
			this.interfaceOptions = 0;
			this.pRestriction = IntPtr.Zero;
			this.pPropName = IntPtr.Zero;
			this.maxRequested = 0;
			this.ppDNTList = IntPtr.Zero;
			this.pPropTags = IntPtr.Zero;
			this.ppRows = IntPtr.Zero;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003B3C0 File Offset: 0x0003A7C0
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			int[] mids = null;
			PropertyTag[] propertyTags = null;
			Restriction restriction = null;
			if (this.ppRows != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppRows, IntPtr.Zero);
			}
			if (this.ppDNTList != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppDNTList, IntPtr.Zero);
			}
			if (this.pState != IntPtr.Zero)
			{
				state = new NspiState(this.pState);
			}
			if (this.pInDNTList != IntPtr.Zero)
			{
				mids = NspiHelper.ConvertCountedIntArrayFromNative(this.pInDNTList);
			}
			if (this.pPropTags != IntPtr.Zero)
			{
				propertyTags = MarshalHelper.ConvertSPropTagArrayToPropertyTagArray(this.pPropTags);
			}
			if (this.pRestriction != IntPtr.Zero)
			{
				restriction = MarshalHelper.ConvertSRestrictionToRestriction(this.pRestriction, MarshalHelper.GetString8CodePage(state));
			}
			base.AsyncDispatch.BeginGetMatches(null, this.contextHandle, this.flags, state, mids, this.interfaceOptions, restriction, this.pPropName, this.maxRequested, propertyTags, asyncCallback, this);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003B510 File Offset: 0x0003A910
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			int[] array = null;
			PropertyValue[][] array2 = null;
			NspiStatus result = NspiStatus.Success;
			NspiState nspiState = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle2 = null;
			try
			{
				array = null;
				array2 = null;
				result = base.AsyncDispatch.EndGetMatches(asyncResult, out nspiState, out array, out array2);
				if (this.pState != IntPtr.Zero && nspiState != null)
				{
					nspiState.MarshalToNative(this.pState);
				}
				if (this.ppDNTList != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(array, true);
					if (safeRpcMemoryHandle != null)
					{
						IntPtr val = safeRpcMemoryHandle.Detach();
						Marshal.WriteIntPtr(this.ppDNTList, val);
					}
				}
				if (this.ppRows != IntPtr.Zero && array2 != null)
				{
					safeRpcMemoryHandle2 = MarshalHelper.ConvertPropertyValueArraysToSRowSet(array2, MarshalHelper.GetString8CodePage(nspiState));
					if (safeRpcMemoryHandle2 != null)
					{
						IntPtr val2 = safeRpcMemoryHandle2.Detach();
						Marshal.WriteIntPtr(this.ppRows, val2);
					}
				}
			}
			finally
			{
				if (safeRpcMemoryHandle2 != null)
				{
					((IDisposable)safeRpcMemoryHandle2).Dispose();
				}
				if (safeRpcMemoryHandle != null)
				{
					((IDisposable)safeRpcMemoryHandle).Dispose();
				}
			}
			return (int)result;
		}

		// Token: 0x04000E46 RID: 3654
		private IntPtr contextHandle;

		// Token: 0x04000E47 RID: 3655
		private NspiGetMatchesFlags flags;

		// Token: 0x04000E48 RID: 3656
		private IntPtr pState;

		// Token: 0x04000E49 RID: 3657
		private IntPtr pInDNTList;

		// Token: 0x04000E4A RID: 3658
		private int interfaceOptions;

		// Token: 0x04000E4B RID: 3659
		private IntPtr pRestriction;

		// Token: 0x04000E4C RID: 3660
		private IntPtr pPropName;

		// Token: 0x04000E4D RID: 3661
		private int maxRequested;

		// Token: 0x04000E4E RID: 3662
		private IntPtr ppDNTList;

		// Token: 0x04000E4F RID: 3663
		private IntPtr pPropTags;

		// Token: 0x04000E50 RID: 3664
		private IntPtr ppRows;
	}
}
