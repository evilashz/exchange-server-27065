using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000314 RID: 788
	internal class NspiAsyncRpcState_GetHierarchyInfo : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetHierarchyInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003904C File Offset: 0x0003844C
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiGetHierarchyInfoFlags flags, IntPtr pState, IntPtr pVersion, IntPtr ppRowSet)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pState = pState;
			this.pVersion = pVersion;
			this.ppRowSet = ppRowSet;
			this.codePage = 0;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00035C9C File Offset: 0x0003509C
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiGetHierarchyInfoFlags.None;
			this.pState = IntPtr.Zero;
			this.pVersion = IntPtr.Zero;
			this.ppRowSet = IntPtr.Zero;
			this.codePage = 0;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00039090 File Offset: 0x00038490
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			if (this.ppRowSet != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppRowSet, IntPtr.Zero);
			}
			if (this.pState != IntPtr.Zero)
			{
				state = new NspiState(this.pState);
			}
			this.codePage = MarshalHelper.GetString8CodePage(state);
			int version = 0;
			if (this.pVersion != IntPtr.Zero)
			{
				version = Marshal.ReadInt32(this.pVersion);
			}
			base.AsyncDispatch.BeginGetHierarchyInfo(null, this.contextHandle, this.flags, state, version, asyncCallback, this);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003B6E8 File Offset: 0x0003AAE8
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			PropertyValue[][] array = null;
			NspiStatus result = NspiStatus.Success;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			try
			{
				int num = 0;
				int val = 0;
				array = null;
				result = base.AsyncDispatch.EndGetHierarchyInfo(asyncResult, out num, out val, out array);
				byte condition;
				if (array != null && num != this.codePage)
				{
					condition = 0;
				}
				else
				{
					condition = 1;
				}
				ExAssert.Assert(condition != 0, "Code page changed across dispatch layer.");
				if (this.pVersion != IntPtr.Zero)
				{
					Marshal.WriteInt32(this.pVersion, val);
				}
				if (this.ppRowSet != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle = MarshalHelper.ConvertPropertyValueArraysToSRowSet(array, num);
					if (safeRpcMemoryHandle != null)
					{
						IntPtr val2 = safeRpcMemoryHandle.Detach();
						Marshal.WriteIntPtr(this.ppRowSet, val2);
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

		// Token: 0x04000EA8 RID: 3752
		private IntPtr contextHandle;

		// Token: 0x04000EA9 RID: 3753
		private NspiGetHierarchyInfoFlags flags;

		// Token: 0x04000EAA RID: 3754
		private IntPtr pState;

		// Token: 0x04000EAB RID: 3755
		private IntPtr pVersion;

		// Token: 0x04000EAC RID: 3756
		private IntPtr ppRowSet;

		// Token: 0x04000EAD RID: 3757
		private int codePage;
	}
}
