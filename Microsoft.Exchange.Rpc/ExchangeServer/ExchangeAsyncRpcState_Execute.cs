using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x02000203 RID: 515
	internal class ExchangeAsyncRpcState_Execute : BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Execute,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x0001E22C File Offset: 0x0001D62C
		private void FreeLeasedBuffers()
		{
			byte[] array = this.leasedIn;
			if (array != null)
			{
				AsyncBufferPools.ReleaseBuffer(array);
				this.leasedIn = null;
			}
			byte[] array2 = this.leasedOut;
			if (array2 != null)
			{
				AsyncBufferPools.ReleaseBuffer(array2);
				this.leasedOut = null;
			}
			byte[] array3 = this.leasedAuxIn;
			if (array3 != null)
			{
				AsyncBufferPools.ReleaseBuffer(array3);
				this.leasedAuxIn = null;
			}
			byte[] array4 = this.leasedAuxOut;
			if (array4 != null)
			{
				AsyncBufferPools.ReleaseBuffer(array4);
				this.leasedAuxOut = null;
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001FBC0 File Offset: 0x0001EFC0
		public void Initialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_EMSMDB asyncServer, IntPtr pContextHandle, uint flagsIn, IntPtr pIn, uint sizeIn, IntPtr pOut, IntPtr pSizeOut, uint maxOut, IntPtr pAuxIn, uint sizeAuxIn, IntPtr pAuxOut, IntPtr pSizeAuxOut, uint maxAuxOut, IntPtr pTransTime, uint startTime)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.pContextHandle = pContextHandle;
			this.flagsIn = flagsIn;
			this.pIn = pIn;
			this.sizeIn = sizeIn;
			this.pOut = pOut;
			this.pSizeOut = pSizeOut;
			this.maxOut = maxOut;
			this.pAuxIn = pAuxIn;
			this.sizeAuxIn = sizeAuxIn;
			this.pAuxOut = pAuxOut;
			this.pSizeAuxOut = pSizeAuxOut;
			this.maxAuxOut = maxAuxOut;
			this.pTransTime = pTransTime;
			this.startTime = startTime;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0001E298 File Offset: 0x0001D698
		public override void InternalReset()
		{
			this.pContextHandle = IntPtr.Zero;
			this.flagsIn = 0U;
			this.pIn = IntPtr.Zero;
			this.sizeIn = 0U;
			this.pOut = IntPtr.Zero;
			this.pSizeOut = IntPtr.Zero;
			this.maxOut = 0U;
			this.pAuxIn = IntPtr.Zero;
			this.sizeAuxIn = 0U;
			this.pAuxOut = IntPtr.Zero;
			this.pSizeAuxOut = IntPtr.Zero;
			this.maxAuxOut = 0U;
			this.pTransTime = IntPtr.Zero;
			this.startTime = 0U;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001FC44 File Offset: 0x0001F044
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			bool flag = false;
			try
			{
				IntPtr contextHandle = Marshal.ReadIntPtr(this.pContextHandle);
				ArraySegment<byte> segmentExtendedRopIn = base.EmptyByteArraySegment;
				if (this.pIn != IntPtr.Zero)
				{
					byte[] buffer = AsyncBufferPools.GetBuffer((int)this.sizeIn);
					this.leasedIn = buffer;
					Marshal.Copy(this.pIn, buffer, 0, (int)this.sizeIn);
					ArraySegment<byte> arraySegment = new ArraySegment<byte>(this.leasedIn, 0, (int)this.sizeIn);
					segmentExtendedRopIn = arraySegment;
				}
				ArraySegment<byte> segmentExtendedRopOut = base.EmptyByteArraySegment;
				if (this.pOut != IntPtr.Zero)
				{
					byte[] buffer2 = AsyncBufferPools.GetBuffer((int)this.maxOut);
					this.leasedOut = buffer2;
					ArraySegment<byte> arraySegment2 = new ArraySegment<byte>(buffer2, 0, (int)this.maxOut);
					segmentExtendedRopOut = arraySegment2;
				}
				ArraySegment<byte> segmentExtendedAuxIn = base.EmptyByteArraySegment;
				if (this.pAuxIn != IntPtr.Zero)
				{
					byte[] buffer3 = AsyncBufferPools.GetBuffer((int)this.sizeAuxIn);
					this.leasedAuxIn = buffer3;
					Marshal.Copy(this.pAuxIn, buffer3, 0, (int)this.sizeAuxIn);
					ArraySegment<byte> arraySegment3 = new ArraySegment<byte>(this.leasedAuxIn, 0, (int)this.sizeAuxIn);
					segmentExtendedAuxIn = arraySegment3;
				}
				ArraySegment<byte> segmentExtendedAuxOut = base.EmptyByteArraySegment;
				if (this.pAuxOut != IntPtr.Zero)
				{
					byte[] buffer4 = AsyncBufferPools.GetBuffer((int)this.maxAuxOut);
					this.leasedAuxOut = buffer4;
					ArraySegment<byte> arraySegment4 = new ArraySegment<byte>(buffer4, 0, (int)this.maxAuxOut);
					segmentExtendedAuxOut = arraySegment4;
				}
				base.AsyncDispatch.BeginExecute(null, contextHandle, (int)this.flagsIn, segmentExtendedRopIn, segmentExtendedRopOut, segmentExtendedAuxIn, segmentExtendedAuxOut, asyncCallback, this);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.FreeLeasedBuffers();
				}
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001FE04 File Offset: 0x0001F204
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			int result;
			try
			{
				IntPtr zero = IntPtr.Zero;
				ArraySegment<byte> arraySegment = default(ArraySegment<byte>);
				ArraySegment<byte> arraySegment2 = default(ArraySegment<byte>);
				int num = base.AsyncDispatch.EndExecute(asyncResult, out zero, out arraySegment, out arraySegment2);
				Marshal.WriteIntPtr(this.pContextHandle, zero);
				if (this.pOut != IntPtr.Zero && arraySegment.Count > 0)
				{
					Marshal.Copy(arraySegment.Array, arraySegment.Offset, this.pOut, arraySegment.Count);
					Marshal.WriteInt32(this.pSizeOut, arraySegment.Count);
				}
				else
				{
					Marshal.WriteInt32(this.pSizeOut, 0);
				}
				if (this.pAuxOut != IntPtr.Zero && arraySegment2.Count > 0)
				{
					Marshal.Copy(arraySegment2.Array, arraySegment2.Offset, this.pAuxOut, arraySegment2.Count);
					Marshal.WriteInt32(this.pSizeAuxOut, arraySegment2.Count);
				}
				else
				{
					Marshal.WriteInt32(this.pSizeAuxOut, 0);
				}
				int val = <Module>.GetTickCount() - this.startTime;
				Marshal.WriteInt32(this.pTransTime, val);
				result = num;
			}
			finally
			{
				this.FreeLeasedBuffers();
			}
			return result;
		}

		// Token: 0x04000C2D RID: 3117
		private uint flagsIn;

		// Token: 0x04000C2E RID: 3118
		private IntPtr pIn;

		// Token: 0x04000C2F RID: 3119
		private uint sizeIn;

		// Token: 0x04000C30 RID: 3120
		private uint maxOut;

		// Token: 0x04000C31 RID: 3121
		private IntPtr pAuxIn;

		// Token: 0x04000C32 RID: 3122
		private uint sizeAuxIn;

		// Token: 0x04000C33 RID: 3123
		private uint maxAuxOut;

		// Token: 0x04000C34 RID: 3124
		private uint startTime;

		// Token: 0x04000C35 RID: 3125
		private IntPtr pContextHandle;

		// Token: 0x04000C36 RID: 3126
		private IntPtr pTransTime;

		// Token: 0x04000C37 RID: 3127
		private IntPtr pOut;

		// Token: 0x04000C38 RID: 3128
		private IntPtr pSizeOut;

		// Token: 0x04000C39 RID: 3129
		private IntPtr pAuxOut;

		// Token: 0x04000C3A RID: 3130
		private IntPtr pSizeAuxOut;

		// Token: 0x04000C3B RID: 3131
		private byte[] leasedIn;

		// Token: 0x04000C3C RID: 3132
		private byte[] leasedOut;

		// Token: 0x04000C3D RID: 3133
		private byte[] leasedAuxIn;

		// Token: 0x04000C3E RID: 3134
		private byte[] leasedAuxOut;
	}
}
