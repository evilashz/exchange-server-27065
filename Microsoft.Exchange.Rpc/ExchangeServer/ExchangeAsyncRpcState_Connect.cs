using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x020001FD RID: 509
	internal class ExchangeAsyncRpcState_Connect : BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Connect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001E0D4 File Offset: 0x0001D4D4
		private void FreeLeasedBuffers()
		{
			byte[] array = this.leasedAuxIn;
			if (array != null)
			{
				AsyncBufferPools.ReleaseBuffer(array);
				this.leasedAuxIn = null;
			}
			byte[] array2 = this.leasedAuxOut;
			if (array2 != null)
			{
				AsyncBufferPools.ReleaseBuffer(array2);
				this.leasedAuxOut = null;
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001F674 File Offset: 0x0001EA74
		public void Initialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_EMSMDB asyncServer, IntPtr bindingHandle, IntPtr pContextHandle, IntPtr pUserDn, uint flags, uint conMod, uint cpid, uint lcidString, uint lcidSort, uint icxrLink, IntPtr pPollsMax, IntPtr pRetry, IntPtr pRetryDelay, IntPtr pIcxr, IntPtr pDNPrefix, IntPtr pDisplayName, IntPtr pClientVersion, IntPtr pServerVersion, IntPtr pTimeStamp, IntPtr pAuxIn, uint sizeAuxIn, IntPtr pAuxOut, IntPtr pSizeAuxOut, uint maxAuxOut)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.bindingHandle = bindingHandle;
			this.pContextHandle = pContextHandle;
			this.pUserDn = pUserDn;
			this.flags = flags;
			this.conMod = conMod;
			this.cpid = cpid;
			this.lcidString = lcidString;
			this.lcidSort = lcidSort;
			this.icxrLink = icxrLink;
			this.pPollsMax = pPollsMax;
			this.pRetry = pRetry;
			this.pRetryDelay = pRetryDelay;
			this.pIcxr = pIcxr;
			this.pDNPrefix = pDNPrefix;
			this.pDisplayName = pDisplayName;
			this.pClientVersion = pClientVersion;
			this.pServerVersion = pServerVersion;
			this.pTimeStamp = pTimeStamp;
			this.pAuxIn = pAuxIn;
			this.sizeAuxIn = sizeAuxIn;
			this.pAuxOut = pAuxOut;
			this.pSizeAuxOut = pSizeAuxOut;
			this.maxAuxOut = maxAuxOut;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001E110 File Offset: 0x0001D510
		public override void InternalReset()
		{
			this.bindingHandle = IntPtr.Zero;
			this.pContextHandle = IntPtr.Zero;
			this.pUserDn = IntPtr.Zero;
			this.flags = 0U;
			this.conMod = 0U;
			this.cpid = 0U;
			this.lcidString = 0U;
			this.lcidSort = 0U;
			this.icxrLink = 0U;
			this.pPollsMax = IntPtr.Zero;
			this.pRetry = IntPtr.Zero;
			this.pRetryDelay = IntPtr.Zero;
			this.pIcxr = IntPtr.Zero;
			this.pDNPrefix = IntPtr.Zero;
			this.pDisplayName = IntPtr.Zero;
			this.pClientVersion = IntPtr.Zero;
			this.pServerVersion = IntPtr.Zero;
			this.pTimeStamp = IntPtr.Zero;
			this.pAuxIn = IntPtr.Zero;
			this.sizeAuxIn = 0U;
			this.pAuxOut = IntPtr.Zero;
			this.pSizeAuxOut = IntPtr.Zero;
			this.maxAuxOut = 0U;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001F740 File Offset: 0x0001EB40
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			bool flag = false;
			try
			{
				string userDn = Marshal.PtrToStringAnsi(this.pUserDn);
				IntPtr binding = this.bindingHandle;
				short[] array = new short[4];
				MapiVersionConversion.Normalize(this.pClientVersion, array);
				ArraySegment<byte> segmentExtendedAuxIn = base.EmptyByteArraySegment;
				if (this.pAuxIn != IntPtr.Zero)
				{
					byte[] buffer = AsyncBufferPools.GetBuffer((int)this.sizeAuxIn);
					this.leasedAuxIn = buffer;
					Marshal.Copy(this.pAuxIn, buffer, 0, (int)this.sizeAuxIn);
					ArraySegment<byte> arraySegment = new ArraySegment<byte>(this.leasedAuxIn, 0, (int)this.sizeAuxIn);
					segmentExtendedAuxIn = arraySegment;
				}
				ArraySegment<byte> segmentExtendedAuxOut = base.EmptyByteArraySegment;
				if (this.pAuxOut != IntPtr.Zero)
				{
					byte[] buffer2 = AsyncBufferPools.GetBuffer((int)this.maxAuxOut);
					this.leasedAuxOut = buffer2;
					ArraySegment<byte> arraySegment2 = new ArraySegment<byte>(buffer2, 0, (int)this.maxAuxOut);
					segmentExtendedAuxOut = arraySegment2;
				}
				base.AsyncDispatch.BeginConnect(null, new RpcClientBinding(binding, base.AsyncState), userDn, (int)this.flags, (int)this.conMod, (int)this.cpid, (int)this.lcidString, (int)this.lcidSort, array, segmentExtendedAuxIn, segmentExtendedAuxOut, asyncCallback, this);
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

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001F898 File Offset: 0x0001EC98
		public unsafe override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			string text = null;
			string text2 = null;
			short[] array = null;
			int result;
			try
			{
				IntPtr zero = IntPtr.Zero;
				TimeSpan timeSpan = default(TimeSpan);
				TimeSpan timeSpan2 = default(TimeSpan);
				text = null;
				text2 = null;
				array = null;
				ArraySegment<byte> arraySegment = default(ArraySegment<byte>);
				int val;
				int num = base.AsyncDispatch.EndConnect(asyncResult, out zero, out timeSpan, out val, out timeSpan2, out text, out text2, out array, out arraySegment);
				Marshal.WriteIntPtr(this.pContextHandle, zero);
				Marshal.WriteInt32(this.pPollsMax, (int)timeSpan.TotalMilliseconds);
				Marshal.WriteInt32(this.pRetry, val);
				Marshal.WriteInt32(this.pRetryDelay, (int)timeSpan2.TotalMilliseconds);
				if (array != null && array.Length == 4)
				{
					short delta = (short)base.AsyncServer.GetVersionDelta();
					MapiVersionConversion.Legacy(array, this.pServerVersion, delta);
				}
				if (text != null)
				{
					IntPtr val2 = (IntPtr)((void*)<Module>.StringToUnmanagedMultiByte(text, 0U));
					Marshal.WriteIntPtr(this.pDNPrefix, val2);
				}
				if (text2 != null)
				{
					IntPtr val3 = (IntPtr)((void*)<Module>.StringToUnmanagedMultiByte(text2, 0U));
					Marshal.WriteIntPtr(this.pDisplayName, val3);
				}
				if (this.icxrLink == 4294967295U)
				{
					DateTime utcNow = DateTime.UtcNow;
					Marshal.WriteInt32(this.pTimeStamp, (int)utcNow.ToFileTime());
				}
				Marshal.WriteInt16(this.pIcxr, (short)((int)zero.ToInt64()));
				if (this.pAuxOut != IntPtr.Zero && arraySegment.Count > 0)
				{
					Marshal.Copy(arraySegment.Array, arraySegment.Offset, this.pAuxOut, arraySegment.Count);
					Marshal.WriteInt32(this.pSizeAuxOut, arraySegment.Count);
				}
				else
				{
					Marshal.WriteInt32(this.pSizeAuxOut, 0);
				}
				result = num;
			}
			finally
			{
				this.FreeLeasedBuffers();
			}
			return result;
		}

		// Token: 0x04000BFA RID: 3066
		private IntPtr bindingHandle;

		// Token: 0x04000BFB RID: 3067
		private IntPtr pUserDn;

		// Token: 0x04000BFC RID: 3068
		private uint flags;

		// Token: 0x04000BFD RID: 3069
		private uint conMod;

		// Token: 0x04000BFE RID: 3070
		private uint cpid;

		// Token: 0x04000BFF RID: 3071
		private uint lcidString;

		// Token: 0x04000C00 RID: 3072
		private uint lcidSort;

		// Token: 0x04000C01 RID: 3073
		private uint icxrLink;

		// Token: 0x04000C02 RID: 3074
		private IntPtr pClientVersion;

		// Token: 0x04000C03 RID: 3075
		private IntPtr pAuxIn;

		// Token: 0x04000C04 RID: 3076
		private uint sizeAuxIn;

		// Token: 0x04000C05 RID: 3077
		private uint maxAuxOut;

		// Token: 0x04000C06 RID: 3078
		private IntPtr pContextHandle;

		// Token: 0x04000C07 RID: 3079
		private IntPtr pPollsMax;

		// Token: 0x04000C08 RID: 3080
		private IntPtr pRetry;

		// Token: 0x04000C09 RID: 3081
		private IntPtr pRetryDelay;

		// Token: 0x04000C0A RID: 3082
		private IntPtr pIcxr;

		// Token: 0x04000C0B RID: 3083
		private IntPtr pDNPrefix;

		// Token: 0x04000C0C RID: 3084
		private IntPtr pDisplayName;

		// Token: 0x04000C0D RID: 3085
		private IntPtr pServerVersion;

		// Token: 0x04000C0E RID: 3086
		private IntPtr pTimeStamp;

		// Token: 0x04000C0F RID: 3087
		private IntPtr pAuxOut;

		// Token: 0x04000C10 RID: 3088
		private IntPtr pSizeAuxOut;

		// Token: 0x04000C11 RID: 3089
		private byte[] leasedAuxIn;

		// Token: 0x04000C12 RID: 3090
		private byte[] leasedAuxOut;
	}
}
