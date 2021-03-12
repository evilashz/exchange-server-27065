using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000013 RID: 19
	internal sealed class ExecuteDispatchTask : ExchangeDispatchTask
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00004834 File Offset: 0x00002A34
		public ExecuteDispatchTask(IExchangeDispatch exchangeDispatch, CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, int flags, ArraySegment<byte> segmentRopIn, ArraySegment<byte> segmentRopOut, ArraySegment<byte> segmentAuxIn, ArraySegment<byte> segmentAuxOut) : base("ExecuteDispatchTask", exchangeDispatch, protocolRequestInfo, asyncCallback, asyncState)
		{
			this.contextHandleIn = contextHandle;
			this.contextHandleOut = contextHandle;
			this.flags = flags;
			this.segmentRopIn = segmentRopIn;
			this.segmentRopOut = segmentRopOut;
			this.segmentAuxIn = segmentAuxIn;
			this.segmentAuxOut = segmentAuxOut;
			this.ropOutData = Array<byte>.EmptySegment;
			this.auxOutData = Array<byte>.EmptySegment;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000489F File Offset: 0x00002A9F
		internal override IntPtr ContextHandle
		{
			get
			{
				return this.contextHandleIn;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000048A8 File Offset: 0x00002AA8
		internal override int? InternalExecute()
		{
			bool flag = false;
			int? result;
			try
			{
				int value = base.ExchangeDispatch.Execute(base.ProtocolRequestInfo, ref this.contextHandleOut, this.flags, this.segmentRopIn, this.segmentRopOut, out this.ropOutData, this.segmentAuxIn, this.segmentAuxOut, out this.auxOutData);
				flag = true;
				result = new int?(value);
			}
			finally
			{
				if (!flag)
				{
					this.ropOutData = Array<byte>.EmptySegment;
					this.auxOutData = Array<byte>.EmptySegment;
				}
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004930 File Offset: 0x00002B30
		public int End(out IntPtr contextHandle, out ArraySegment<byte> segmentRopOut, out ArraySegment<byte> segmentAuxOut)
		{
			int result = base.CheckCompletion();
			contextHandle = this.contextHandleOut;
			segmentRopOut = this.ropOutData;
			segmentAuxOut = this.auxOutData;
			return result;
		}

		// Token: 0x04000076 RID: 118
		private readonly int flags;

		// Token: 0x04000077 RID: 119
		private readonly ArraySegment<byte> segmentRopIn;

		// Token: 0x04000078 RID: 120
		private readonly ArraySegment<byte> segmentAuxIn;

		// Token: 0x04000079 RID: 121
		private readonly ArraySegment<byte> segmentRopOut;

		// Token: 0x0400007A RID: 122
		private readonly ArraySegment<byte> segmentAuxOut;

		// Token: 0x0400007B RID: 123
		private IntPtr contextHandleIn;

		// Token: 0x0400007C RID: 124
		private IntPtr contextHandleOut;

		// Token: 0x0400007D RID: 125
		private ArraySegment<byte> ropOutData;

		// Token: 0x0400007E RID: 126
		private ArraySegment<byte> auxOutData;
	}
}
