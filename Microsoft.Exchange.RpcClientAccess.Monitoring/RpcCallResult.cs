using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MapiHttp;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RpcCallResult : CallResult
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000024CF File Offset: 0x000006CF
		protected RpcCallResult(Exception exception, ErrorCode errorCode, ExceptionTraceAuxiliaryBlock remoteExceptionTrace) : this(exception, errorCode, remoteExceptionTrace, null)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024DC File Offset: 0x000006DC
		protected RpcCallResult(Exception exception, ErrorCode errorCode, ExceptionTraceAuxiliaryBlock remoteExceptionTrace, MonitoringActivityAuxiliaryBlock activityContext)
		{
			this.exception = exception;
			this.errorCode = errorCode;
			this.remoteExceptionTrace = remoteExceptionTrace;
			this.activityContext = activityContext;
			RpcException ex = this.exception as RpcException;
			ProtocolException ex2 = this.exception as ProtocolException;
			if (ex != null)
			{
				this.statusCode = ex.ErrorCode;
				return;
			}
			if (ex2 == null)
			{
				this.statusCode = 0;
				return;
			}
			if (ex2 is ServiceTooBusyException)
			{
				this.statusCode = 1723;
				return;
			}
			if (ex2 is ServiceUnavailableException)
			{
				this.statusCode = 1722;
				return;
			}
			this.statusCode = 1726;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002572 File Offset: 0x00000772
		public override bool IsSuccessful
		{
			get
			{
				return this.StatusCode == 0 && this.errorCode == ErrorCode.None;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002587 File Offset: 0x00000787
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000258F File Offset: 0x0000078F
		public ErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002597 File Offset: 0x00000797
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000259F File Offset: 0x0000079F
		public string RemoteExceptionTrace
		{
			get
			{
				if (this.remoteExceptionTrace == null)
				{
					return string.Empty;
				}
				return this.remoteExceptionTrace.ExceptionTrace;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025BA File Offset: 0x000007BA
		public string ActivityContext
		{
			get
			{
				if (this.activityContext != null)
				{
					return this.activityContext.ActivityContent;
				}
				return null;
			}
		}

		// Token: 0x0400000E RID: 14
		private const int RpcSServerUnavailable = 1722;

		// Token: 0x0400000F RID: 15
		private const int RpcSServerTooBusy = 1723;

		// Token: 0x04000010 RID: 16
		private const int RpcSCallFailed = 1726;

		// Token: 0x04000011 RID: 17
		private readonly Exception exception;

		// Token: 0x04000012 RID: 18
		private readonly ErrorCode errorCode;

		// Token: 0x04000013 RID: 19
		private readonly ExceptionTraceAuxiliaryBlock remoteExceptionTrace;

		// Token: 0x04000014 RID: 20
		private readonly MonitoringActivityAuxiliaryBlock activityContext;

		// Token: 0x04000015 RID: 21
		private readonly int statusCode;
	}
}
