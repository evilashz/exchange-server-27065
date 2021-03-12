using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000011 RID: 17
	internal sealed class ConnectDispatchTask : ExchangeDispatchTask
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000045DC File Offset: 0x000027DC
		public ConnectDispatchTask(IExchangeDispatch exchangeDispatch, CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, string userDn, int flags, int connectionModulus, int codePage, int stringLocale, int sortLocale, short[] clientVersion, ArraySegment<byte> segmentAuxIn, ArraySegment<byte> segmentAuxOut, IStandardBudget budget) : base("ConnectDispatchTask", exchangeDispatch, protocolRequestInfo, asyncCallback, asyncState)
		{
			this.clientBinding = clientBinding;
			this.userDn = userDn;
			this.flags = flags;
			this.connectionModulus = connectionModulus;
			this.codePage = codePage;
			this.stringLocale = stringLocale;
			this.sortLocale = sortLocale;
			this.clientVersion = clientVersion;
			this.segmentAuxIn = segmentAuxIn;
			this.segmentAuxOut = segmentAuxOut;
			this.auxOutData = Array<byte>.EmptySegment;
			this.budget = budget;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000465C File Offset: 0x0000285C
		internal override IntPtr ContextHandle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004664 File Offset: 0x00002864
		internal override int? InternalExecute()
		{
			bool flag = false;
			int? result;
			try
			{
				int value = base.ExchangeDispatch.Connect(base.ProtocolRequestInfo, this.clientBinding, out this.contextHandle, this.userDn, this.flags, this.connectionModulus, this.codePage, this.stringLocale, this.sortLocale, out this.pollsMax, out this.retryCount, out this.retryDelay, out this.dnPrefix, out this.displayName, this.clientVersion, out this.serverVersion, this.segmentAuxIn, this.segmentAuxOut, out this.auxOutData, this.budget);
				flag = true;
				result = new int?(value);
			}
			finally
			{
				if (!flag)
				{
					this.auxOutData = Array<byte>.EmptySegment;
					this.serverVersion = Microsoft.Exchange.RpcClientAccess.Server.ExchangeDispatch.ExchangeServerVersion;
				}
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004730 File Offset: 0x00002930
		public int End(out IntPtr contextHandle, out TimeSpan pollsMax, out int retryCount, out TimeSpan retryDelay, out string dnPrefix, out string displayName, out short[] serverVersion, out ArraySegment<byte> segmentAuxOut)
		{
			bool flag = true;
			int result;
			try
			{
				int num = base.CheckCompletion();
				contextHandle = this.contextHandle;
				pollsMax = this.pollsMax;
				retryCount = this.retryCount;
				retryDelay = this.retryDelay;
				dnPrefix = this.dnPrefix;
				displayName = this.displayName;
				serverVersion = this.serverVersion;
				segmentAuxOut = this.auxOutData;
				if (num == 0)
				{
					flag = false;
				}
				result = num;
			}
			finally
			{
				if (flag)
				{
					Util.DisposeIfPresent(this.budget);
				}
			}
			return result;
		}

		// Token: 0x04000061 RID: 97
		private readonly ClientBinding clientBinding;

		// Token: 0x04000062 RID: 98
		private readonly string userDn;

		// Token: 0x04000063 RID: 99
		private readonly int flags;

		// Token: 0x04000064 RID: 100
		private readonly int connectionModulus;

		// Token: 0x04000065 RID: 101
		private readonly int codePage;

		// Token: 0x04000066 RID: 102
		private readonly int stringLocale;

		// Token: 0x04000067 RID: 103
		private readonly int sortLocale;

		// Token: 0x04000068 RID: 104
		private readonly short[] clientVersion;

		// Token: 0x04000069 RID: 105
		private readonly ArraySegment<byte> segmentAuxIn;

		// Token: 0x0400006A RID: 106
		private readonly IStandardBudget budget;

		// Token: 0x0400006B RID: 107
		private readonly ArraySegment<byte> segmentAuxOut;

		// Token: 0x0400006C RID: 108
		private IntPtr contextHandle;

		// Token: 0x0400006D RID: 109
		private TimeSpan pollsMax;

		// Token: 0x0400006E RID: 110
		private int retryCount;

		// Token: 0x0400006F RID: 111
		private TimeSpan retryDelay;

		// Token: 0x04000070 RID: 112
		private string dnPrefix;

		// Token: 0x04000071 RID: 113
		private string displayName;

		// Token: 0x04000072 RID: 114
		private short[] serverVersion;

		// Token: 0x04000073 RID: 115
		private ArraySegment<byte> auxOutData;
	}
}
