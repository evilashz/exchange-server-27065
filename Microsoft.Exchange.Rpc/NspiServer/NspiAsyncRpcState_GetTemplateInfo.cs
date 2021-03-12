using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000316 RID: 790
	internal class NspiAsyncRpcState_GetTemplateInfo : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x00039160 File Offset: 0x00038560
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiGetTemplateInfoFlags flags, int type, IntPtr pDN, int codePage, int locale, IntPtr ppRow)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.type = type;
			this.pDN = pDN;
			this.codePage = codePage;
			this.locale = locale;
			this.ppRow = ppRow;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00035CE4 File Offset: 0x000350E4
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiGetTemplateInfoFlags.None;
			this.type = 0;
			this.pDN = IntPtr.Zero;
			this.codePage = 0;
			this.locale = 0;
			this.ppRow = IntPtr.Zero;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000391AC File Offset: 0x000385AC
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			string dn = null;
			if (this.ppRow != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppRow, IntPtr.Zero);
			}
			if (this.pDN != IntPtr.Zero)
			{
				dn = Marshal.PtrToStringAnsi(this.pDN);
			}
			base.AsyncDispatch.BeginGetTemplateInfo(null, this.contextHandle, this.flags, this.type, dn, this.codePage, this.locale, asyncCallback, this);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003B7CC File Offset: 0x0003ABCC
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			PropertyValue[] array = null;
			NspiStatus result = NspiStatus.Success;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			try
			{
				int num = 0;
				array = null;
				result = base.AsyncDispatch.EndGetTemplateInfo(asyncResult, out num, out array);
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
				if (this.ppRow != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle = MarshalHelper.ConvertPropertyValueArrayToSRow(array, num);
					if (safeRpcMemoryHandle != null)
					{
						IntPtr val = safeRpcMemoryHandle.Detach();
						Marshal.WriteIntPtr(this.ppRow, val);
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

		// Token: 0x04000EB6 RID: 3766
		private IntPtr contextHandle;

		// Token: 0x04000EB7 RID: 3767
		private NspiGetTemplateInfoFlags flags;

		// Token: 0x04000EB8 RID: 3768
		private int type;

		// Token: 0x04000EB9 RID: 3769
		private IntPtr pDN;

		// Token: 0x04000EBA RID: 3770
		private int codePage;

		// Token: 0x04000EBB RID: 3771
		private int locale;

		// Token: 0x04000EBC RID: 3772
		private IntPtr ppRow;
	}
}
